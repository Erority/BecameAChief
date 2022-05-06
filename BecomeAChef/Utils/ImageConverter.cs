using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace BecomeAChef.Utils
{
    internal class ImageConverter
    {
        public BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            try
            {
                using (var mem = new MemoryStream(imageData))
                {
                    mem.Position = 0;
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = mem;
                    image.EndInit();
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            image.Freeze();
            return image;
        }

        public byte[] GetJPGFromImageControl(BitmapImage imageC)
        {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageC));
            encoder.Save(memStream);
            return memStream.ToArray();
        }

        public BitmapImage GetImageFromFileDialog()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            BitmapImage bitmap = new BitmapImage();

            if (openFile.ShowDialog() == true)
            {
                bitmap = new BitmapImage(new Uri(openFile.FileName));
            }

            return bitmap;
        }
    }
}
