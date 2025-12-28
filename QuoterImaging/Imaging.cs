using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace QuoterPlanImaging
{
    public class Imaging
    {
        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
        private static extern void CopyMemory(IntPtr Destination, IntPtr Source, uint Length);

        public unsafe static void ResizeImage(Bitmap originalBitmap, Bitmap resultBitmap, Rectangle srcRect, Rectangle destRect)
        {
            int destWidth = resultBitmap.Width;
            int destHeight = resultBitmap.Height;

            int srcWidth = originalBitmap.Width;
            int srcHeight = originalBitmap.Height;

            BitmapData srcData = originalBitmap.LockBits(new Rectangle(0, 0, srcWidth, srcHeight), ImageLockMode.ReadOnly, PixelFormat.Format32bppPArgb);
            BitmapData dstData = resultBitmap.LockBits(new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);

            // Keep the original decompiled math/logic as-is (note: integer division happens before cast).
            double xScale = (double)(srcWidth / destWidth);
            double srcRowStep = (double)(srcHeight / destHeight * srcWidth);

            int* srcPixels = (int*)srcData.Scan0.ToPointer();
            int* dstPixels = (int*)dstData.Scan0.ToPointer();

            for (int y = 0; y < destHeight; y++)
            {
                for (int x = 0; x < destWidth; x++)
                {
                    int srcIndex = (int)(Math.Round((double)x * xScale) + Math.Round((double)y * srcRowStep));
                    int dstIndex = x + y * destWidth;
                    dstPixels[dstIndex] = srcPixels[srcIndex];
                }
            }

            originalBitmap.UnlockBits(srcData);
            resultBitmap.UnlockBits(dstData);
        }

        public unsafe static void ApplyBrightness(Bitmap bitmap, double brightness)
        {
            try
            {
                byte[] map = new byte[256];
                for (int i = 0; i < 256; i++)
                {
                    double v = i + brightness;
                    if (v < 0.0) v = 0.0;
                    if (v > 255.0) v = 255.0;
                    map[i] = (byte)v;
                }

                BitmapData data = bitmap.LockBits(
                    new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadWrite,
                    PixelFormat.Format32bppArgb);

                const int bytesPerPixel = 4;

                byte* basePtr = (byte*)data.Scan0.ToPointer();
                for (int y = 0; y < data.Height; y++)
                {
                    byte* row = basePtr + (y * data.Stride);
                    for (int x = 0; x < data.Width; x++)
                    {
                        int offset = x * bytesPerPixel;

                        row[offset] = map[row[offset]];           // B
                        row[offset + 1] = map[row[offset + 1]];   // G
                        row[offset + 2] = map[row[offset + 2]];   // R
                    }
                }

                bitmap.UnlockBits(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        public unsafe static void ApplyContrast(Bitmap bitmap, double contrast)
        {
            try
            {
                byte[] map = new byte[256];

                double factor = (100.0 + contrast) / 100.0;
                factor *= factor;

                for (int i = 0; i < 256; i++)
                {
                    double v = i / 255.0;
                    v -= 0.5;
                    v *= factor;
                    v += 0.5;
                    v *= 255.0;

                    if (v < 0.0) v = 0.0;
                    if (v > 255.0) v = 255.0;

                    map[i] = (byte)v;
                }

                BitmapData data = bitmap.LockBits(
                    new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadWrite,
                    PixelFormat.Format32bppArgb);

                const int bytesPerPixel = 4;

                byte* basePtr = (byte*)data.Scan0.ToPointer();
                for (int y = 0; y < data.Height; y++)
                {
                    byte* row = basePtr + (y * data.Stride);
                    for (int x = 0; x < data.Width; x++)
                    {
                        int offset = x * bytesPerPixel;

                        row[offset] = map[row[offset]];           // B
                        row[offset + 1] = map[row[offset + 1]];   // G
                        row[offset + 2] = map[row[offset + 2]];   // R
                    }
                }

                bitmap.UnlockBits(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        public unsafe static Bitmap CloneBitmap(Bitmap bitmap, PixelFormat pixelFormat, ref Exception exception)
        {
            Bitmap clone = null;

            try
            {
                int width = bitmap.Width;
                int height = bitmap.Height;

                clone = new Bitmap(width, height, pixelFormat);
                clone.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);

                BitmapData srcData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
                BitmapData dstData = clone.LockBits(new Rectangle(0, 0, clone.Width, clone.Height), ImageLockMode.WriteOnly, clone.PixelFormat);

                int bytesPerPixel;
                switch (bitmap.PixelFormat)
                {
                    case PixelFormat.Format24bppRgb:
                        bytesPerPixel = 3;
                        break;

                    case PixelFormat.Format32bppRgb:
                    case PixelFormat.Format32bppPArgb:
                    case PixelFormat.Format32bppArgb:
                        bytesPerPixel = 4;
                        break;

                    default:
                        throw new InvalidOperationException("Image format not supported");
                }

                int rows = srcData.Height;
                int cols = srcData.Width;

                for (int y = 0; y < rows; y++)
                {
                    byte* dstRow = (byte*)dstData.Scan0.ToPointer() + (y * dstData.Stride);
                    byte* srcRow = (byte*)srcData.Scan0.ToPointer() + (y * srcData.Stride);

                    uint byteCount = (uint)(cols * bytesPerPixel);
                    CopyMemory(new IntPtr(dstRow), new IntPtr(srcRow), byteCount);
                }

                bitmap.UnlockBits(srcData);
                clone.UnlockBits(dstData);

                return clone;
            }
            catch (Exception ex)
            {
                if (clone != null)
                {
                    clone.Dispose();
                    GC.Collect();
                }

                exception = ex;
                return null;
            }
        }

        public unsafe static Bitmap ColorToGrayscale(Bitmap bitmap, ref Exception exception)
        {
            Bitmap gray = null;

            try
            {
                int width = bitmap.Width;
                int height = bitmap.Height;

                PixelFormat srcFormat = bitmap.PixelFormat;

                gray = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
                gray.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);

                ColorPalette palette = gray.Palette;
                for (int i = 0; i < 256; i++)
                {
                    palette.Entries[i] = Color.FromArgb(255, i, i, i);
                }
                gray.Palette = palette;

                if (srcFormat == PixelFormat.Format8bppIndexed)
                {
                    gray = (Bitmap)bitmap.Clone();
                    gray.Palette = palette;
                    return gray;
                }

                int bytesPerPixel;
                switch (srcFormat)
                {
                    case PixelFormat.Format24bppRgb:
                        bytesPerPixel = 3;
                        break;

                    case PixelFormat.Format32bppRgb:
                    case PixelFormat.Format32bppPArgb:
                    case PixelFormat.Format32bppArgb:
                        bytesPerPixel = 4;
                        break;

                    default:
                        throw new InvalidOperationException("Image format not supported");
                }

                BitmapData srcData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, srcFormat);
                BitmapData dstData = gray.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

                int srcStride = srcData.Stride;
                int dstStride = dstData.Stride;

                byte* src = (byte*)srcData.Scan0.ToPointer();
                byte* dst = (byte*)dstData.Scan0.ToPointer();

                if (bytesPerPixel == 3)
                {
                    for (int y = 0; y < height; y++)
                    {
                        int srcOffset;
                        for (int x = srcOffset = 0; x < width; x++)
                        {
                            dst[y * dstStride + x] =
                                (byte)(int)(0.299f * src[y * srcStride + srcOffset] +
                                            0.587f * src[y * srcStride + srcOffset + 1] +
                                            0.114f * src[y * srcStride + srcOffset + 2]);

                            srcOffset += 3;
                        }
                    }
                }
                else
                {
                    for (int y = 0; y < height; y++)
                    {
                        int srcOffset;
                        for (int x = srcOffset = 0; x < width; x++)
                        {
                            // Keep the original decompiled math/logic as-is.
                            dst[y * dstStride + x] =
                                (byte)(int)((float)src[y * srcStride + srcOffset] / 255f *
                                            (0.299f * src[y * srcStride + srcOffset + 1] +
                                             0.587f * src[y * srcStride + srcOffset + 2] +
                                             0.114f * src[y * srcStride + srcOffset + 3]));

                            srcOffset += 4;
                        }
                    }
                }

                bitmap.UnlockBits(srcData);
                gray.UnlockBits(dstData);

                return gray;
            }
            catch (Exception ex)
            {
                if (gray != null)
                {
                    gray.Dispose();
                    GC.Collect();
                }

                exception = ex;
                return null;
            }
        }

        public unsafe static Bitmap GrayscaleToColor(Bitmap bitmap, ref Exception exception)
        {
            Bitmap color = null;

            try
            {
                int width = bitmap.Width;
                int height = bitmap.Height;

                const int dstBytesPerPixel = 4;

                PixelFormat srcFormat = bitmap.PixelFormat;

                color = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
                color.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);

                if (srcFormat != PixelFormat.Format8bppIndexed)
                    throw new InvalidOperationException("Image format not supported");

                const int srcBytesPerPixel = 1;

                BitmapData srcData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, srcFormat);
                BitmapData dstData = color.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);

                int srcStride = srcData.Stride;
                int dstStride = dstData.Stride;

                byte* src = (byte*)srcData.Scan0.ToPointer();
                byte* dst = (byte*)dstData.Scan0.ToPointer();

                for (int y = 0; y < height; y++)
                {
                    int srcOffset;
                    for (int dstOffset = srcOffset = 0; dstOffset < width * dstBytesPerPixel; dstOffset += dstBytesPerPixel)
                    {
                        byte g = src[y * srcStride + srcOffset];

                        dst[y * dstStride + dstOffset] = g;
                        dst[y * dstStride + dstOffset + 1] = g;
                        dst[y * dstStride + dstOffset + 2] = g;
                        dst[y * dstStride + dstOffset + 3] = byte.MaxValue;

                        srcOffset += srcBytesPerPixel;
                    }
                }

                bitmap.UnlockBits(srcData);
                color.UnlockBits(dstData);

                return color;
            }
            catch (Exception ex)
            {
                if (color != null)
                {
                    color.Dispose();
                    GC.Collect();
                }

                exception = ex;
                return null;
            }
        }

        public unsafe static bool IsGrayScale(Image image, ref Exception exception)
        {
            bool isGray = true;
            Bitmap tmp = null;

            try
            {
                tmp = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
                using (Graphics g = Graphics.FromImage(tmp))
                {
                    g.DrawImage(image, 0, 0);
                }

                BitmapData data = tmp.LockBits(new Rectangle(0, 0, tmp.Width, tmp.Height), ImageLockMode.ReadOnly, tmp.PixelFormat);

                int* pixels = (int*)data.Scan0.ToPointer();
                int count = data.Height * data.Width;

                for (int i = 0; i < count; i++)
                {
                    Color c = Color.FromArgb(pixels[i]);
                    if (c.A != 0 && (c.R != c.G || c.G != c.B))
                    {
                        isGray = false;
                        break;
                    }
                }

                tmp.UnlockBits(data);
                GC.Collect();

                return isGray;
            }
            catch (Exception ex)
            {
                if (tmp != null)
                {
                    tmp.Dispose();
                    GC.Collect();
                }

                exception = ex;
                return false;
            }
        }

        public Imaging()
        {
        }

        internal class FastBitmap : IDisposable
        {
            private Bitmap bitmap;
            private BitmapData bitmapData;
            private bool disposed;

            public int Row => bitmapData.Height;
            public int ScanWidth => bitmapData.Stride;
            public PixelFormat PixelFormat => bitmapData.PixelFormat;

            public FastBitmap(Bitmap bitmap)
            {
                this.bitmap = bitmap;
                this.bitmapData = this.bitmap.LockBits(
                    new Rectangle(new Point(0, 0), this.bitmap.Size),
                    ImageLockMode.ReadWrite,
                    this.bitmap.PixelFormat);
            }

            public unsafe byte* Pixel(int x, int y, int pixelSize, byte* value)
            {
                byte* p = Pixel(x, y, pixelSize);
                for (int i = 0; i < pixelSize; i++)
                {
                    p[i] = value[i];
                }
                return p;
            }

            public unsafe byte* Pixel(int x, int y, int pixelSize)
            {
                byte* basePtr = (byte*)bitmapData.Scan0.ToPointer();
                return basePtr + (y * bitmapData.Stride) + (x * pixelSize);
            }

            public unsafe Color Get(int x, int y)
            {
                switch (PixelFormat)
                {
                    case PixelFormat.Format24bppRgb:
                        {
                            byte* p = Pixel(x, y, 3);
                            return Color.FromArgb(p[2], p[1], p[0]);
                        }

                    case PixelFormat.Format32bppArgb:
                        {
                            byte* p = Pixel(x, y, 4);
                            return Color.FromArgb(p[3], p[2], p[1], p[0]);
                        }

                    default:
                        return Color.Black;
                }
            }

            public unsafe void Set(int x, int y, Color c)
            {
                switch (bitmap.PixelFormat)
                {
                    case PixelFormat.Format24bppRgb:
                        {
                            byte* p = Pixel(x, y, 3);
                            p[2] = c.R;
                            p[1] = c.G;
                            p[0] = c.B;
                            break;
                        }

                    case PixelFormat.Format32bppArgb:
                        {
                            byte* p = Pixel(x, y, 4);
                            p[3] = c.A;
                            p[2] = c.R;
                            p[1] = c.G;
                            p[0] = c.B;
                            break;
                        }

                    default:
                        return;
                }
            }

            ~FastBitmap()
            {
                Dispose();
            }

            public void Dispose()
            {
                if (disposed)
                    return;

                disposed = true;

                if (bitmap != null && bitmapData != null)
                {
                    bitmap.UnlockBits(bitmapData);
                    bitmapData = null;
                }

                GC.Collect();
                GC.SuppressFinalize(this);
            }
        }
    }
}
