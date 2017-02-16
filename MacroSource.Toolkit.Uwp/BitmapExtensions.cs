using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace MacroSource.Toolkit.Uwp
{
    public static class BitmapExtensions
    {
        /// <summary>
        /// 保存到文件
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="newFile"></param>
        /// <returns></returns>
        public static async Task SaveToFileAsync(this BitmapImage bitmap, StorageFile newFile)
        {
            if (bitmap.UriSource == null) throw new ArgumentNullException(nameof(bitmap.UriSource), "BitmapImage.UriSource is null.");
            using (IRandomAccessStream ras = await RandomAccessStreamReference.CreateFromUri(bitmap.UriSource).OpenReadAsync())
            {
                WriteableBitmap writeableBitmap = new WriteableBitmap(bitmap.PixelWidth, bitmap.PixelHeight);
                await writeableBitmap.SetSourceAsync(ras);
                await SaveToFileAsync(writeableBitmap, newFile);
            }
        }

        /// <summary>
        /// 保存到文件
        /// </summary>
        /// <param name="image"></param>
        /// <param name="newFile"></param>
        /// <returns></returns>
        public static async Task SaveToFileAsync(this WriteableBitmap image, StorageFile newFile)
        {

            if (image == null)
            {
                return;
            }
            Guid BitmapEncoderGuid = BitmapEncoder.JpegEncoderId;
            var path = newFile.Path;
            if (path.EndsWith("jpg"))
                BitmapEncoderGuid = BitmapEncoder.JpegEncoderId;
            else if (path.EndsWith("png"))
                BitmapEncoderGuid = BitmapEncoder.PngEncoderId;
            else if (path.EndsWith("bmp"))
                BitmapEncoderGuid = BitmapEncoder.BmpEncoderId;
            else if (path.EndsWith("tiff"))
                BitmapEncoderGuid = BitmapEncoder.TiffEncoderId;
            else if (path.EndsWith("gif"))
                BitmapEncoderGuid = BitmapEncoder.GifEncoderId;
            //var folder = await _local_folder.CreateFolderAsync("images_cache", CreationCollisionOption.OpenIfExists);
            //var file = await KnownFolders.PicturesLibrary.CreateFileAsync(newFile, CreationCollisionOption.GenerateUniqueName);

            using (IRandomAccessStream stream = await newFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoderGuid, stream);
                var pixelStream = image.PixelBuffer.AsStream();
                byte[] pixels = new byte[pixelStream.Length];
                await pixelStream.ReadAsync(pixels, 0, pixels.Length);
                encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore,
                          (uint)image.PixelWidth,
                          (uint)image.PixelHeight,
                          96.0,
                          96.0,
                          pixels);
                await encoder.FlushAsync();
            }

        }
    }
}
