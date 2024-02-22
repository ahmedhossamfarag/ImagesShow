using ImagesShow.Controllers;
using ImagesShow.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
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
    public sealed partial class CollectionViewPage : Page
    {
        private Collection Collection {  get; set; }

        public CollectionViewPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Collection = e.Parameter as Collection;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await Collection?.LoadImagesAsync();
        }

        private async void NewImage(object sender, RoutedEventArgs e)
        {
            var photos = await FilesPicker.PickImagesAsync();
            if (photos != null)
            {
                foreach (var photo in photos)
                    await AddAsync(photo);
            }
        }

        private async void CameraImage(object sender, RoutedEventArgs e)
        {
            try
            {
                CameraCaptureUI captureUI = new CameraCaptureUI();
                captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
                captureUI.PhotoSettings.AllowCropping = false;
                StorageFile photo = await
                captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);
                if (photo != null)
                {
                    await AddAsync(photo);
                }
            }
            catch
            {

            }
        }

        private async Task AddAsync(StorageFile photo)
        {
            await photo.CopyAsync(Collection.Folder,
                $"Img-{DateTime.Now.ToString("%y-%M-%d-%H-%m-%s-%ff")}.jpeg");
            await Collection?.AddAsync(photo);
        }

        private async void Rename(object sender, RoutedEventArgs e)
        {
            await Collection.RenameAsync(CollectionName.Text);
            CollectionName.Text = Collection.Folder.Name;
        }

        private async void Delete(object sender, RoutedEventArgs e)
        {
            await Collection.DeleteAsync();
            Frame.Navigate(typeof(CollectionsPage));
        }

        private void CreateShow(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewShowPage), Collection);
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(ImagesGrid.SelectedIndex >= 0)
            {
                var img = Collection.Images[ImagesGrid.SelectedIndex];
                await img.DeleteAsync();
                Collection.Images.RemoveAt(ImagesGrid.SelectedIndex);
            }
        }
    }
}
