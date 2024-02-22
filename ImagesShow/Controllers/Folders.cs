using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ImagesShow.Controllers
{
    internal static class Folders
    {
        public static Windows.Foundation.IAsyncOperation<StorageFolder> GetCollectionsAsync()
        {
            return ApplicationData.Current.LocalFolder.CreateFolderAsync("Collections", CreationCollisionOption.OpenIfExists);
        }
        
        public static Windows.Foundation.IAsyncOperation<StorageFolder> GetMusicsAsync()
        {
            return ApplicationData.Current.LocalFolder.CreateFolderAsync("Musics", CreationCollisionOption.OpenIfExists);
        }
        
        public static Windows.Foundation.IAsyncOperation<StorageFolder> GetShowsAsync()
        {
            return ApplicationData.Current.LocalFolder.CreateFolderAsync("Shows", CreationCollisionOption.OpenIfExists);
        }


    }
}
