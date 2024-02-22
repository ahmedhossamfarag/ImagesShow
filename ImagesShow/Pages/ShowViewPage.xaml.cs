using ImagesShow.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ImagesShow.Pages
{
    public sealed partial class ShowViewPage : Page
    {
        private Show Show {  get; set; }

        private static TimeSpan period = TimeSpan.FromSeconds(5);

        private ThreadPoolTimer PeriodicTimer;
        private int imgIndex;
        private int musicIndex;
        private MediaPlayerElement mediaPlayer;

        private bool playing;

        private bool canPlay => Show.Collection != null && Show.Collection.Images.Any();

        public ShowViewPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Show = e.Parameter as Show;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            PeriodicTimer?.Cancel();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await Show?.LoadShowAsync();
            PlayButton.IsEnabled = canPlay;
            if(Show.Musics.Any())
            {
                mediaPlayer = new MediaPlayerElement
                {
                    Source = Show.Musics[0].Source,
                };
                mediaPlayer.MediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
            }
        }

        private async void MediaPlayer_MediaEnded(Windows.Media.Playback.MediaPlayer sender, object args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.High,
            () =>
            {
                musicIndex++;
                musicIndex = musicIndex % Show.Musics.Count;
                mediaPlayer.MediaPlayer.Source = Show.Musics[musicIndex].Source;
                if (playing)
                {
                    mediaPlayer.MediaPlayer.Play();
                }
            });
        }

        private async void GoOn(ThreadPoolTimer timer)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.High, 
            () =>
            {
                Img.Source = Show.Collection.Images[imgIndex].BitmapSource;
                imgIndex++;
                imgIndex = imgIndex % Show.Collection.Images.Count;
            });
        }

        private void PlayClick(object sender, RoutedEventArgs e)
        {
            if (playing)
            {
                playing = false;
                PeriodicTimer.Cancel();
                mediaPlayer?.MediaPlayer.Pause();
                ShowControl.Symbol = Symbol.Play;
            }
            else
            {
                playing = true;
                GoOn(null);
                PeriodicTimer = ThreadPoolTimer.CreatePeriodicTimer(this.GoOn, period);
                mediaPlayer?.MediaPlayer.Play();
                ShowControl.Symbol = Symbol.Pause;
            }
            DeleteButton.Visibility = playing ? Visibility.Collapsed : Visibility.Visible;
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            await Show.DeleteAsync();
            Frame.Navigate(typeof(ShowsPage));
        }
    }
}
