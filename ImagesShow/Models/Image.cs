using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace ImagesShow.Models
{
    internal class Image : FileClass
    {
        public SoftwareBitmapSource BitmapSource { get; set; }

        public async Task LoadImageAsync()
        {
            IRandomAccessStream stream = await File.OpenAsync(FileAccessMode.Read);
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
            SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();
            SoftwareBitmap softwareBitmapBGR8 = SoftwareBitmap.Convert(
                softwareBitmap,
                BitmapPixelFormat.Bgra8,
                BitmapAlphaMode.Premultiplied
                );
            BitmapSource = new SoftwareBitmapSource();
            await BitmapSource.SetBitmapAsync(softwareBitmapBGR8);
        }
    }
}
