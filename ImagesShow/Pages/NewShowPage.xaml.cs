using ImagesShow.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Search;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ImagesShow.Controllers;


namespace ImagesShow.Pages
{
    public sealed partial class NewShowPage : Page
    {
        private Collection Collection;
        private ObservableCollection<string> Musics { get; } = new ObservableCollection<string>();
        
        public NewShowPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Collection = e.Parameter as Collection;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "New Show For: " + Collection.Folder.Name;
            var folder = await Folders.GetMusicsAsync();
            var musics = await Files.GetMusicsAsync(folder);
            MusicsComboBox.ItemsSource = musics.ToList().ConvertAll(m => m.Name);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if(MusicsComboBox.SelectedIndex >= 0)
            {
                Musics.Add(MusicsComboBox.SelectedItem.ToString());
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(MusicsList.SelectedIndex >= 0)
            {
                Musics.RemoveAt(MusicsList.SelectedIndex);
            }
        }

        private async void Go_Click(object sender, RoutedEventArgs e)
        {
            var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Shows", CreationCollisionOption.OpenIfExists);
            var file = await folder.CreateFileAsync(Collection.Folder.Name, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteLinesAsync(file, Musics);
            var show = new Show { File = file, Collection = Collection };
            Frame.Navigate(typeof(ShowViewPage), show);
        }
    }
}
