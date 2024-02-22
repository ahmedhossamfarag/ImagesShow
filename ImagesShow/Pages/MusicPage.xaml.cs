using ImagesShow.Controllers;
using ImagesShow.Models;
using System;
using System.Collections.ObjectModel;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.Search;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;


namespace ImagesShow.Pages
{
    public sealed partial class MusicPage : Page
    {
        private ObservableCollection<Music> Musics { get; } = new ObservableCollection<Music>();

        private StorageFolder Folder { get; set; }

        private MediaCapture mediaCapture;
        private StorageFile _file;

        public MusicPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Folder = await Folders.GetMusicsAsync();
            var musics = await Files.GetMusicsAsync(Folder);
            foreach (var music in musics)
            {
                Add(music);
            }
        }

        private void Add(StorageFile music)
        {
            var mus = new Music { File = music };
            mus.CreateSource();
            Musics.Add(mus);
        }

        private async void NewMusic(object sender, RoutedEventArgs e)
        {
            var musics = await FilesPicker.PickMusicsAsync();
            if (musics != null)
            {
                foreach (var music in musics)
                {
                    await music.CopyAsync(Folder);
                    Add(music);
                }
            }
        }

        private async void RecordMusic(object sender, RoutedEventArgs e)
        {
            if(_file is  null)
            {
                try
                {
                    mediaCapture?.Dispose();
                    mediaCapture = new MediaCapture();
                    await mediaCapture.InitializeAsync();
                    _file = await Folder.CreateFileAsync($"audio{DateTime.Now.ToString("%y-%M-%d-%H-%m-%s")}.mp3", CreationCollisionOption.GenerateUniqueName);
                    await mediaCapture.StartRecordToStorageFileAsync(
                            MediaEncodingProfile.CreateMp3(AudioEncodingQuality.High), _file);
                    Record.Background = new SolidColorBrush(Colors.Blue);
                }
                catch { }
            }
            else
            {
                await mediaCapture?.StopRecordAsync();
                Record.Background = null;
                Add(_file);
                _file = null;
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(MusicList.SelectedIndex >= 0)
            {
                var music = Musics[MusicList.SelectedIndex];
                await music.DeleteAsync();
                Musics.RemoveAt(MusicList.SelectedIndex);
            }
        }
    }
}
