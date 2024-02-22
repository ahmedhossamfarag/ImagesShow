using ImagesShow.Controllers;
using ImagesShow.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace ImagesShow.Pages
{
    public sealed partial class CollectionsPage : Page
    {
        private ObservableCollection<Collection> Collections { get; } = new ObservableCollection<Collection>();
        
        private StorageFolder Folder { get; set; }

        public CollectionsPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Folder = await Folders.GetCollectionsAsync();
            var collections = await Folder.GetFoldersAsync();
            foreach (var collection in collections)
            {
                var data = await collection.GetBasicPropertiesAsync();
                var span = data.DateModified - DateTime.Now;
                Collections.Add(new Collection { Folder = collection, LastModified = span.Days });
            }
        }

        private async void NewCollection(object sender, RoutedEventArgs e)
        {
            var collection = await Folder.CreateFolderAsync($"New Collection {Collections.Count}");
            Collections.Add(new Collection { Folder = collection });
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Frame.Navigate(typeof(CollectionViewPage), Collections[CollectionsList.SelectedIndex]);
        }
    }
}
