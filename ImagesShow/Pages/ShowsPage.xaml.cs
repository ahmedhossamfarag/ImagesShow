using ImagesShow.Controllers;
using ImagesShow.Models;
using System;
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
    
    public sealed partial class ShowsPage : Page
    {
        private ObservableCollection<Show> Shows { get; } = new ObservableCollection<Show>();

        private StorageFolder Folder { get; set; }

        public ShowsPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Folder = await Folders.GetShowsAsync();
            var shows = await Folder.GetFilesAsync();
            foreach (var show in shows)
            {
                Shows.Add(new Show { File = show });
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Frame.Navigate(typeof(ShowViewPage), Shows[ShowsList.SelectedIndex]);
        }
    }
}
