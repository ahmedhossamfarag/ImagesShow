using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;
using Windows.UI.Xaml.Controls;

namespace ImagesShow.Controllers
{
    internal static class Files
    {
        private static Windows.Foundation.IAsyncOperation<IReadOnlyList<StorageFile>> GetFilesAsync(StorageFolder folder, string[] exts)
        {
            var options = new QueryOptions(
                CommonFileQuery.DefaultQuery,
                exts);
            return folder.CreateFileQueryWithOptions(options).GetFilesAsync();
        }

        public static Windows.Foundation.IAsyncOperation<IReadOnlyList<StorageFile>> GetImagesAsync(StorageFolder folder)
        {
            return GetFilesAsync(folder, new string[] { ".jpg", ".jpeg", ".png" });
        }

        public static Windows.Foundation.IAsyncOperation<IReadOnlyList<StorageFile>> GetMusicsAsync(StorageFolder folder)
        {
            return GetFilesAsync(folder, new string[] { ".mp3" });
        }
    }
}
