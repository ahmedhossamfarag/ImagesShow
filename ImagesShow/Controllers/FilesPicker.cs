using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesShow.Controllers
{
    internal static class FilesPicker
    {
        private static Windows.Foundation.IAsyncOperation<IReadOnlyList<Windows.Storage.StorageFile>> PickFilesAsync(string[] exts)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation =
            Windows.Storage.Pickers.PickerLocationId.MusicLibrary;
            foreach (var ext in exts)
                picker.FileTypeFilter.Add(ext);
            return picker.PickMultipleFilesAsync();
        }

        public static Windows.Foundation.IAsyncOperation<IReadOnlyList<Windows.Storage.StorageFile>> PickImagesAsync()
        {
            return PickFilesAsync(new string[] { ".png" , ".jpeg" , ".jpg" });
        }
        
        public static Windows.Foundation.IAsyncOperation<IReadOnlyList<Windows.Storage.StorageFile>> PickMusicsAsync()
        {
            return PickFilesAsync(new string[] { ".mp3" });
        }

    }
}
