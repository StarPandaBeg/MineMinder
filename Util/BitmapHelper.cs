using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace MineMinder.Recognizer
{
    public static class BitmapHelper
    {
        public static Bitmap Crop(Bitmap source, int startX, int startY, int width, int height)
        {
            var croppedBitmap = new Bitmap(width, height, source.PixelFormat);
            var srcRect = new Rectangle(startX, startY, width, height);
            var destRect = new Rectangle(0, 0, width, height);

            using var g = Graphics.FromImage(croppedBitmap);
            g.DrawImage(source, destRect, srcRect, GraphicsUnit.Pixel);

            return croppedBitmap;
        }

        public static Color GetColorSliced(Bitmap source, int cellWidth, int cellHeight, int x, int y, float u = 0.5f, float v = 0.5f) {
            var width = source.Width;
            var height = source.Height;
            var numCols = width / cellWidth;
            var numRows = height / cellHeight;

            var startX = cellWidth * x;
            var startY = cellHeight * y;
            var endX = cellWidth * (x + 1);
            var endY = cellHeight * (y + 1);

            var targetX = startX + (endX - startX) * u;
            var targetY = startY + (endY - startY) * v;
            
            return source.GetPixel((int)targetX, (int)targetY);
        }
        
        public static Bitmap[] Slice(Bitmap source, int cellWidth, int cellHeight)
        {
            var width = source.Width;
            var height = source.Height;
            var numCols = width / cellWidth;
            var numRows = height / cellHeight;

            var cells = new Bitmap[numCols * numRows];
            var sourceData = source.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, source.PixelFormat);

            try
            {
                var byteCount = sourceData.Stride * height;
                var pixels = new byte[byteCount];
                Marshal.Copy(sourceData.Scan0, pixels, 0, byteCount);

                var cellIndex = 0;
                
                for (var row = 0; row < numRows; row++)
                {
                    for (var col = 0; col < numCols; col++)
                    {
                        var startX = col * cellWidth;
                        var startY = row * cellHeight;
                        
                        var cellBitmap = new Bitmap(cellWidth, cellHeight);
                        var cellData = cellBitmap.LockBits(new Rectangle(0, 0, cellWidth, cellHeight),
                            ImageLockMode.WriteOnly, source.PixelFormat);

                        try
                        {
                            var cellByteCount = cellData.Stride * cellHeight;
                            var cellPixels = new byte[cellByteCount];

                            for (var y = 0; y < cellHeight; y++)
                            {
                                for (var x = 0; x < cellWidth; x++)
                                {
                                    var sourceX = startX + x;
                                    var sourceY = startY + y;

                                    var sourceIndex = (sourceY * sourceData.Stride) + (sourceX * 3);
                                    var targetIndex = (y * cellData.Stride) + (x * 3);

                                    cellPixels[targetIndex] = pixels[sourceIndex];
                                    cellPixels[targetIndex + 1] = pixels[sourceIndex + 1];
                                    cellPixels[targetIndex + 2] = pixels[sourceIndex + 2];
                                }
                            }

                            Marshal.Copy(cellPixels, 0, cellData.Scan0, cellByteCount);
                        }
                        finally
                        {
                            cellBitmap.UnlockBits(cellData);
                        }

                        cells[cellIndex] = cellBitmap;
                        cellIndex++;
                    }
                }
            }
            finally
            {
                source.UnlockBits(sourceData);
            }

            return cells;
        }
    }
}