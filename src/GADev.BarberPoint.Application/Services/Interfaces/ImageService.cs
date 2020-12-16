using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace GADev.BarberPoint.Application.Services.Interfaces
{
    public class ImageService : Interfaces.IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly String _pathFile;

        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _pathFile = Path.Combine(_webHostEnvironment.ContentRootPath, "logos");
        }

        public string GetImage(string nameFile)
        {
            try
            {
                string base64 = string.Empty;
                var pathFile = Path.Combine(_pathFile, nameFile);

                if (!File.Exists(pathFile)) return null;

                var bytes = File.ReadAllBytes(pathFile);
                base64 = Convert.ToBase64String(bytes);

                return base64;
            }
            catch
            {
                return null;
            }
        }

        public bool RemoveImage(string nameFile)
        {
            try
            {
                var pathFile = Path.Combine(_pathFile, nameFile);

                if (File.Exists(pathFile)) {
                    File.Delete(pathFile);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SaveImage(string base64, string nameFile)
        {
            try
            {
                var pathFile = Path.Combine(_pathFile, nameFile);
                var bytes = Convert.FromBase64String(base64);

                if (!Directory.Exists(_pathFile))
                {
                    Directory.CreateDirectory(_pathFile);
                }

                using (MemoryStream stream = new MemoryStream(bytes)) {
                    Image img = Image.FromStream(stream);
                    int MAX_WIDTH = 600;
                    int MAX_HEIGHT = 400;
                    int width = img.Width;
                    int height = img.Height;
                    
                    if (width > height) {
                        if (width > MAX_WIDTH) {
                            height = MAX_HEIGHT;
                            width = MAX_WIDTH;
                        }
                    } else {
                        if (height > MAX_HEIGHT) {
                            width = MAX_HEIGHT;
                            height = MAX_WIDTH;
                        }
                    }

                    var destRect = new Rectangle(0, 0, width, height);

                    using (Bitmap btm = new Bitmap(width, height)) {               
                        btm.SetResolution(img.HorizontalResolution, img.VerticalResolution);

                        using (var graphics = Graphics.FromImage(btm)) {
                            graphics.CompositingMode = CompositingMode.SourceCopy;
                            graphics.CompositingQuality = CompositingQuality.HighSpeed;
                            graphics.InterpolationMode = InterpolationMode.Low;
                            graphics.SmoothingMode = SmoothingMode.HighSpeed;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;

                            using (var wrapMode = new ImageAttributes())
                            {
                                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                                graphics.DrawImage(img, destRect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, wrapMode);
                            }
                        }

                        btm.Save(pathFile, System.Drawing.Imaging.ImageFormat.Png);                    
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }            
        }
    }
}