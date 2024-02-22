using ImagesShow.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;

namespace ImagesShow.Models
{
    internal class Collection : FolderClass
    {

        public int LastModified { get; set; }

        public ObservableCollection<Image> Images { get; } = new ObservableCollection<Image>();

        public async Task LoadImagesAsync() 
        { 
            Images.Clear();
            var images = await Files.GetImagesAsync(Folder);
            foreach (var image in images)
            {
                await AddAsync(image);
            }
        }

        public async Task AddAsync(StorageFile photo)
        {
            var img = new Image { File = photo };
            await img.LoadImageAsync();
            Images.Add(img);
        }
    }
}
