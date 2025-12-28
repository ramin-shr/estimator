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
			int width = resultBitmap.Width;
			int height = resultBitmap.Height;
			int width2 = originalBitmap.Width;
			int height2 = originalBitmap.Height;
			BitmapData bitmapData = originalBitmap.LockBits(new Rectangle(0, 0, width2, height2), ImageLockMode.ReadOnly, PixelFormat.Format32bppPArgb);
			BitmapData bitmapData2 = resultBitmap.LockBits(new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
			double num = (double)(width2 / width);
			double num2 = (double)(height2 / height * width2);
			int* ptr = (int*)bitmapData.Scan0.ToPointer();
			int* ptr2 = (int*)bitmapData2.Scan0.ToPointer();
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					int num3 = (int)(Math.Round((double)j * num) + Math.Round((double)i * num2));
					int num4 = j + i * width;
					ptr2[num4] = ptr[num3];
				}
			}
			originalBitmap.UnlockBits(bitmapData);
			resultBitmap.UnlockBits(bitmapData2);
		}

		public unsafe static void ApplyBrightness(Bitmap bitmap, double brightness)
		{
			try
			{
				byte[] array = new byte[256];
				for (int i = 0; i < 256; i++)
				{
					double num = (double)i;
					num += brightness;
					if (num < 0.0)
					{
						num = 0.0;
					}
					if (num > 255.0)
					{
						num = 255.0;
					}
					array[i] = (byte)num;
				}
				BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
				int num2 = 4;
				for (int j = 0; j < bitmapData.Height; j++)
				{
					byte* ptr = (byte*)((void*)bitmapData.Scan0) + (IntPtr)j * (IntPtr)bitmapData.Stride;
					for (int k = 0; k < bitmapData.Width; k++)
					{
						ptr[(IntPtr)k * (IntPtr)num2] = array[(int)ptr[(IntPtr)k * (IntPtr)num2]];
						ptr[k * num2 + 1] = array[(int)ptr[k * num2 + 1]];
						ptr[k * num2 + 2] = array[(int)ptr[k * num2 + 2]];
					}
				}
				bitmap.UnlockBits(bitmapData);
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
				byte[] array = new byte[256];
				double num = (100.0 + contrast) / 100.0;
				num *= num;
				for (int i = 0; i < 256; i++)
				{
					double num2 = (double)i;
					num2 /= 255.0;
					num2 -= 0.5;
					num2 *= num;
					num2 += 0.5;
					num2 *= 255.0;
					if (num2 < 0.0)
					{
						num2 = 0.0;
					}
					if (num2 > 255.0)
					{
						num2 = 255.0;
					}
					array[i] = (byte)num2;
				}
				BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
				int num3 = 4;
				for (int j = 0; j < bitmapData.Height; j++)
				{
					byte* ptr = (byte*)((void*)bitmapData.Scan0) + (IntPtr)j * (IntPtr)bitmapData.Stride;
					for (int k = 0; k < bitmapData.Width; k++)
					{
						ptr[(IntPtr)k * (IntPtr)num3] = array[(int)ptr[(IntPtr)k * (IntPtr)num3]];
						ptr[k * num3 + 1] = array[(int)ptr[k * num3 + 1]];
						ptr[k * num3 + 2] = array[(int)ptr[k * num3 + 2]];
					}
				}
				bitmap.UnlockBits(bitmapData);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
			}
		}

		public unsafe static Bitmap CloneBitmap(Bitmap bitmap, PixelFormat pixelFormat, ref Exception exception)
		{
			Bitmap bitmap2 = null;
			Bitmap result;
			try
			{
				int width = bitmap.Width;
				int height = bitmap.Height;
				bitmap2 = new Bitmap(width, height, pixelFormat);
				bitmap2.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);
				BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
				BitmapData bitmapData2 = bitmap2.LockBits(new Rectangle(0, 0, bitmap2.Width, bitmap2.Height), ImageLockMode.WriteOnly, bitmap2.PixelFormat);
				PixelFormat pixelFormat2 = bitmap.PixelFormat;
				if (pixelFormat2 <= PixelFormat.Format32bppRgb)
				{
					if (pixelFormat2 == PixelFormat.Format24bppRgb)
					{
						int num = 3;
						goto IL_B9;
					}
					if (pixelFormat2 == PixelFormat.Format32bppRgb)
					{
						int num = 4;
						goto IL_B9;
					}
				}
				else
				{
					if (pixelFormat2 == PixelFormat.Format32bppPArgb)
					{
						int num = 4;
						goto IL_B9;
					}
					if (pixelFormat2 == PixelFormat.Format32bppArgb)
					{
						int num = 4;
						goto IL_B9;
					}
				}
				throw new InvalidOperationException("Image format not supported");
				IL_B9:
				int height2 = bitmapData.Height;
				int width2 = bitmapData.Width;
				for (int i = 0; i < height2; i++)
				{
					int num2 = i;
					byte* value = (byte*)((void*)bitmapData2.Scan0) + (IntPtr)num2 * (IntPtr)bitmapData2.Stride;
					byte* value2 = (byte*)((void*)bitmapData.Scan0) + (IntPtr)i * (IntPtr)bitmapData.Stride;
					int num;
					Imaging.CopyMemory(new IntPtr((void*)value), new IntPtr((void*)value2), (uint)(width2 * num));
				}
				bitmap.UnlockBits(bitmapData);
				bitmap2.UnlockBits(bitmapData2);
				result = bitmap2;
			}
			catch (Exception ex)
			{
				if (bitmap2 != null)
				{
					bitmap2.Dispose();
					GC.Collect();
				}
				exception = ex;
				result = null;
			}
			return result;
		}

		public unsafe static Bitmap ColorToGrayscale(Bitmap bitmap, ref Exception exception)
		{
			Bitmap bitmap2 = null;
			Bitmap result;
			try
			{
				int width = bitmap.Width;
				int height = bitmap.Height;
				PixelFormat pixelFormat = bitmap.PixelFormat;
				bitmap2 = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
				bitmap2.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);
				ColorPalette palette = bitmap2.Palette;
				for (int i = 0; i < 256; i++)
				{
					Color.FromArgb(255, i, i, i);
					palette.Entries[i] = Color.FromArgb(255, i, i, i);
				}
				bitmap2.Palette = palette;
				if (pixelFormat == PixelFormat.Format8bppIndexed)
				{
					bitmap2 = (Bitmap)bitmap.Clone();
					bitmap2.Palette = palette;
					result = bitmap2;
				}
				else
				{
					PixelFormat pixelFormat2 = pixelFormat;
					int num;
					if (pixelFormat2 <= PixelFormat.Format32bppRgb)
					{
						if (pixelFormat2 == PixelFormat.Format24bppRgb)
						{
							num = 3;
							goto IL_108;
						}
						if (pixelFormat2 == PixelFormat.Format32bppRgb)
						{
							num = 4;
							goto IL_108;
						}
					}
					else
					{
						if (pixelFormat2 == PixelFormat.Format32bppPArgb)
						{
							num = 4;
							goto IL_108;
						}
						if (pixelFormat2 == PixelFormat.Format32bppArgb)
						{
							num = 4;
							goto IL_108;
						}
					}
					throw new InvalidOperationException("Image format not supported");
					IL_108:
					BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, pixelFormat);
					BitmapData bitmapData2 = bitmap2.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
					int stride = bitmapData.Stride;
					int stride2 = bitmapData2.Stride;
					byte* ptr = (byte*)bitmapData.Scan0.ToPointer();
					byte* ptr2 = (byte*)bitmapData2.Scan0.ToPointer();
					if (num == 3)
					{
						for (int j = 0; j < height; j++)
						{
							int num2;
							for (int k = num2 = 0; k < width; k++)
							{
								ptr2[j * stride2 + k] = (byte)((int)(0.299f * (float)ptr[j * stride + num2] + 0.587f * (float)ptr[j * stride + num2 + 1] + 0.114f * (float)ptr[j * stride + num2 + 2]));
								num2 += 3;
							}
						}
					}
					else
					{
						for (int j = 0; j < height; j++)
						{
							int num2;
							for (int k = num2 = 0; k < width; k++)
							{
								ptr2[j * stride2 + k] = (byte)((int)((float)ptr[j * stride + num2] / 255f * (0.299f * (float)ptr[j * stride + num2 + 1] + 0.587f * (float)ptr[j * stride + num2 + 2] + 0.114f * (float)ptr[j * stride + num2 + 3])));
								num2 += 4;
							}
						}
					}
					bitmap.UnlockBits(bitmapData);
					bitmap2.UnlockBits(bitmapData2);
					result = bitmap2;
				}
			}
			catch (Exception ex)
			{
				if (bitmap2 != null)
				{
					bitmap2.Dispose();
					GC.Collect();
				}
				exception = ex;
				result = null;
			}
			return result;
		}

		public unsafe static Bitmap GrayscaleToColor(Bitmap bitmap, ref Exception exception)
		{
			Bitmap bitmap2 = null;
			Bitmap result;
			try
			{
				int width = bitmap.Width;
				int height = bitmap.Height;
				int num = 4;
				PixelFormat pixelFormat = bitmap.PixelFormat;
				bitmap2 = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
				bitmap2.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);
				PixelFormat pixelFormat2 = pixelFormat;
				if (pixelFormat2 != PixelFormat.Format8bppIndexed)
				{
					throw new InvalidOperationException("Image format not supported");
				}
				int num2 = 1;
				BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, pixelFormat);
				BitmapData bitmapData2 = bitmap2.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
				int stride = bitmapData.Stride;
				int stride2 = bitmapData2.Stride;
				byte* ptr = (byte*)bitmapData.Scan0.ToPointer();
				byte* ptr2 = (byte*)bitmapData2.Scan0.ToPointer();
				for (int i = 0; i < height; i++)
				{
					int num3;
					for (int j = num3 = 0; j < width * num; j += num)
					{
						ptr2[i * stride2 + j] = ptr[i * stride + num3];
						ptr2[i * stride2 + j + 1] = ptr[i * stride + num3];
						ptr2[i * stride2 + j + 2] = ptr[i * stride + num3];
						ptr2[i * stride2 + j + 3] = byte.MaxValue;
						num3 += num2;
					}
				}
				bitmap.UnlockBits(bitmapData);
				bitmap2.UnlockBits(bitmapData2);
				result = bitmap2;
			}
			catch (Exception ex)
			{
				if (bitmap2 != null)
				{
					bitmap2.Dispose();
					GC.Collect();
				}
				exception = ex;
				result = null;
			}
			return result;
		}

		public unsafe static bool IsGrayScale(Image image, ref Exception exception)
		{
			bool flag = true;
			Bitmap bitmap = null;
			bool result;
			try
			{
				Bitmap bitmap2;
				bitmap = (bitmap2 = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb));
				try
				{
					using (Graphics graphics = Graphics.FromImage(bitmap))
					{
						graphics.DrawImage(image, 0, 0);
					}
					BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
					int* ptr = (int*)((void*)bitmapData.Scan0);
					for (int i = 0; i < bitmapData.Height * bitmapData.Width; i++)
					{
						Color color = Color.FromArgb(ptr[i]);
						if (color.A != 0 && (color.R != color.G || color.G != color.B))
						{
							flag = false;
							break;
						}
					}
					bitmap.UnlockBits(bitmapData);
				}
				finally
				{
					if (bitmap2 != null)
					{
						((IDisposable)bitmap2).Dispose();
					}
				}
				GC.Collect();
				result = flag;
			}
			catch (Exception ex)
			{
				if (bitmap != null)
				{
					bitmap.Dispose();
					GC.Collect();
				}
				exception = ex;
				result = false;
			}
			return result;
		}

		public Imaging()
		{
		}

		internal class FastBitmap : IDisposable
		{
			public int Row
			{
				get
				{
					return this.bitmapData.Height;
				}
			}

			public int ScanWidth
			{
				get
				{
					return this.bitmapData.Stride;
				}
			}

			public PixelFormat PixelFormat
			{
				get
				{
					return this.bitmapData.PixelFormat;
				}
			}

			public FastBitmap(Bitmap bitmap)
			{
				this.bitmap = bitmap;
				this.bitmapData = this.bitmap.LockBits(new Rectangle(new Point(0, 0), this.bitmap.Size), ImageLockMode.ReadWrite, this.bitmap.PixelFormat);
			}

			public unsafe byte* Pixel(int x, int y, int pixelSize, byte* value)
			{
				byte* ptr = this.Pixel(x, y, pixelSize);
				for (int i = 0; i < pixelSize; i++)
				{
					ptr[i] = value[i];
				}
				return ptr;
			}

			public unsafe byte* Pixel(int x, int y, int pixelSize)
			{
				return (byte*)((byte*)((void*)this.bitmapData.Scan0) + (IntPtr)y * (IntPtr)this.bitmapData.Stride) + (IntPtr)x * (IntPtr)pixelSize;
			}

			public unsafe Color Get(int x, int y)
			{
				Color result = Color.Black;
				byte* ptr = null;
				PixelFormat pixelFormat = this.PixelFormat;
				if (pixelFormat <= PixelFormat.Format1bppIndexed)
				{
					if (pixelFormat != PixelFormat.Format24bppRgb)
					{
						if (pixelFormat != PixelFormat.Format1bppIndexed)
						{
						}
					}
					else
					{
						ptr = (byte*)((byte*)((void*)this.bitmapData.Scan0) + (IntPtr)y * (IntPtr)this.bitmapData.Stride) + (IntPtr)x * 3;
						result = Color.FromArgb((int)ptr[2], (int)ptr[1], (int)(*ptr));
					}
				}
				else if (pixelFormat != PixelFormat.Format8bppIndexed && pixelFormat == PixelFormat.Format32bppArgb)
				{
					ptr = (byte*)((byte*)((void*)this.bitmapData.Scan0) + (IntPtr)y * (IntPtr)this.bitmapData.Stride) + (IntPtr)x * 4;
					result = Color.FromArgb((int)ptr[3], (int)ptr[2], (int)ptr[1], (int)(*ptr));
				}
				return result;
			}

			public unsafe void Set(int x, int y, Color c)
			{
				byte* ptr = null;
				PixelFormat pixelFormat = this.bitmap.PixelFormat;
				if (pixelFormat <= PixelFormat.Format1bppIndexed)
				{
					if (pixelFormat != PixelFormat.Format24bppRgb)
					{
						if (pixelFormat != PixelFormat.Format1bppIndexed)
						{
							return;
						}
					}
					else
					{
						ptr = (byte*)((byte*)((void*)this.bitmapData.Scan0) + (IntPtr)y * (IntPtr)this.bitmapData.Stride) + (IntPtr)x * 3;
						ptr[2] = c.R;
						ptr[1] = c.G;
						*ptr = c.B;
					}
				}
				else if (pixelFormat != PixelFormat.Format8bppIndexed)
				{
					if (pixelFormat != PixelFormat.Format32bppArgb)
					{
						return;
					}
					ptr = (byte*)((byte*)((void*)this.bitmapData.Scan0) + (IntPtr)y * (IntPtr)this.bitmapData.Stride) + (IntPtr)x * 4;
					ptr[3] = c.A;
					ptr[2] = c.R;
					ptr[1] = c.G;
					*ptr = c.B;
					return;
				}
			}

			~FastBitmap()
			{
				this.Dispose();
			}

			public void Dispose()
			{
				this.bitmap.UnlockBits(this.bitmapData);
				GC.Collect();
				GC.SuppressFinalize(this);
			}

			private Bitmap bitmap;

			private BitmapData bitmapData;
		}
	}
}
