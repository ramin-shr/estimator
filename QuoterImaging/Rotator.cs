using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace QuoterPlanImaging
{
	public static class Rotator
	{
		public static Image RotateImage(int rotationAngle, Bitmap originalBitmap)
		{
			if (rotationAngle != 0 && rotationAngle != 90 && rotationAngle != 180 && rotationAngle != 270)
			{
				throw new ArgumentException("The rotation angle must be one of 0, 90, 180 or 270", "rotationAngle");
			}
			bool flag = rotationAngle == 90 || rotationAngle == 270;
			int width = flag ? originalBitmap.Height : originalBitmap.Width;
			int height = flag ? originalBitmap.Width : originalBitmap.Height;
			Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
			Rotator.InternalRotateImage(rotationAngle, originalBitmap, bitmap);
			return bitmap;
		}

		private unsafe static void InternalRotateImage(int rotationAngle, Bitmap originalBitmap, Bitmap rotatedBitmap)
		{
			int width = rotatedBitmap.Width;
			int height = rotatedBitmap.Height;
			int width2 = originalBitmap.Width;
			int height2 = originalBitmap.Height;
			int num = width - 1;
			int num2 = height - 1;
			BitmapData bitmapData = originalBitmap.LockBits(new Rectangle(0, 0, width2, height2), ImageLockMode.ReadOnly, PixelFormat.Format32bppPArgb);
			BitmapData bitmapData2 = rotatedBitmap.LockBits(new Rectangle(0, 0, rotatedBitmap.Width, rotatedBitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
			int* ptr = (int*)bitmapData.Scan0.ToPointer();
			int* ptr2 = (int*)bitmapData2.Scan0.ToPointer();
			if (rotationAngle != 90)
			{
				if (rotationAngle != 180)
				{
					if (rotationAngle == 270)
					{
						for (int i = 0; i < height2; i++)
						{
							int num3 = i;
							for (int j = 0; j < width2; j++)
							{
								int num4 = j + i * width2;
								int num5 = num2 - j;
								int num6 = num3 + num5 * width;
								ptr2[num6] = ptr[num4];
							}
						}
					}
				}
				else
				{
					for (int k = 0; k < height2; k++)
					{
						int num7 = (num2 - k) * width;
						for (int l = 0; l < width2; l++)
						{
							int num8 = l + k * width2;
							int num9 = num - l;
							int num10 = num9 + num7;
							ptr2[num10] = ptr[num8];
						}
					}
				}
			}
			else
			{
				for (int m = 0; m < height2; m++)
				{
					int num11 = num - m;
					for (int n = 0; n < width2; n++)
					{
						int num12 = n + m * width2;
						int num13 = n;
						int num14 = num11 + num13 * width;
						ptr2[num14] = ptr[num12];
					}
				}
			}
			originalBitmap.UnlockBits(bitmapData);
			rotatedBitmap.UnlockBits(bitmapData2);
		}
	}
}
