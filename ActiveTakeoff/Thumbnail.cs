using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using QuoterPlan.Properties;
using QuoterPlanControls;
using QuoterPlanImaging;

namespace QuoterPlan
{
	public class Thumbnail
	{
		public string FileName
		{
			get
			{
				return this.fileName;
			}
			set
			{
				this.fileName = value;
			}
		}

		public bool Dirty
		{
			get
			{
				return !(this.fileName == "") && this.dirty;
			}
			set
			{
				this.dirty = value;
			}
		}

		public int Brightness
		{
			get
			{
				return this.brightness;
			}
			set
			{
				this.brightness = value;
			}
		}

		public int Contrast
		{
			get
			{
				return this.contrast;
			}
			set
			{
				this.contrast = value;
			}
		}

		public int Width
		{
			get
			{
				return this.width;
			}
		}

		public int Height
		{
			get
			{
				return this.height;
			}
		}

		public Bitmap ThumbImage
		{
			get
			{
				return this.thumbImage;
			}
		}

		public Thumbnail(int width, int height)
		{
			this.width = width;
			this.height = height;
			this.fileName = "";
		}

		public void Clear()
		{
			if (this.Dirty)
			{
				this.DeleteFile();
			}
			this.dirty = false;
			this.fileName = "";
			if (this.thumbImage != null)
			{
				this.thumbImage.Dispose();
				this.thumbImage = null;
			}
		}

		public bool IsValid()
		{
			return this.thumbImage != null;
		}

		private void ApplyFilters()
		{
			if (this.brightness == 0 && this.contrast == 0)
			{
				this.brightness = -40;
				this.contrast = 25;
			}
			Imaging.ApplyBrightness(this.thumbImage, (double)this.brightness);
			Imaging.ApplyContrast(this.thumbImage, (double)this.contrast);
		}

		private bool LoadFromCache()
		{
			string text = this.cacheFolder + "\\" + this.fileName + ".png";
			try
			{
				if (this.fileName != "" && File.Exists(text))
				{
					this.thumbImage = new Bitmap(Image.FromFile(text));
					this.ApplyFilters();
					return true;
				}
			}
			catch (Exception exception)
			{
				Utilities.DisplayFileOpenError(text, exception);
			}
			return false;
		}

		private bool CreateThumbnail(FileStream fs, ref bool canRetry)
		{
			bool result = false;
			try
			{
				using (Image image = Image.FromStream(fs))
				{
					Exception ex = null;
					if (DrawingBoard.ResizeImage(image, ref this.thumbImage, this.width, this.height, PixelFormat.Format24bppRgb, InterpolationMode.HighQualityBicubic, ref ex))
					{
						this.fileName = Utilities.GetUniqueFileName("png");
						string filename = this.cacheFolder + "\\" + this.fileName;
						try
						{
							this.thumbImage.Save(filename, ImageFormat.Png);
							this.fileName = this.fileName.Substring(0, this.fileName.Length - 4);
							this.ApplyFilters();
							this.dirty = true;
							result = true;
							goto IL_A3;
						}
						catch (Exception exception)
						{
							Utilities.DisplaySystemError(exception);
							goto IL_A3;
						}
					}
					canRetry = true;
					IL_A3:
					image.Dispose();
				}
			}
			catch (Exception)
			{
				canRetry = true;
			}
			GC.Collect();
			return result;
		}

		private bool DoCreateThumbnail(FileStream fs)
		{
			bool flag2;
			string impossible_d_effectuer_l_opération_désirée;
			string ressources_insuffisantes_pour_compléter_l_opération;
			do
			{
				bool flag = false;
				flag2 = this.CreateThumbnail(fs, ref flag);
				if (flag2 || !flag)
				{
					break;
				}
				impossible_d_effectuer_l_opération_désirée = Resources.Impossible_d_effectuer_l_opération_désirée;
				ressources_insuffisantes_pour_compléter_l_opération = Resources.Ressources_insuffisantes_pour_compléter_l_opération;
			}
			while (Utilities.DisplayWarningQuestionRetryCancel(impossible_d_effectuer_l_opération_désirée, ressources_insuffisantes_pour_compléter_l_opération) != DialogResult.Cancel);
			return flag2;
		}

		private bool CreateFromSource(string sourceFileName)
		{
			bool flag = false;
			try
			{
				using (FileStream fileStream = new FileStream(sourceFileName, FileMode.Open, FileAccess.Read))
				{
					flag = this.DoCreateThumbnail(fileStream);
				}
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
			}
			if (!flag)
			{
				if (this.thumbImage != null)
				{
					this.thumbImage.Dispose();
					this.thumbImage = null;
				}
				this.dirty = false;
				this.fileName = "";
			}
			return flag;
		}

		public void Reload()
		{
			if (this.thumbImage == null)
			{
				return;
			}
			this.thumbImage.Dispose();
			this.LoadFromCache();
		}

		public bool Load(string cacheFolder, string sourceFileName, bool loadFromCacheOnly)
		{
			this.cacheFolder = cacheFolder;
			if (this.thumbImage != null)
			{
				this.thumbImage.Dispose();
			}
			return this.LoadFromCache() || (!loadFromCacheOnly && this.CreateFromSource(sourceFileName));
		}

		public void DeleteFile()
		{
			Utilities.FileDelete(this.cacheFolder + "\\" + this.fileName + ".png", true);
		}

		private bool dirty;

		private int brightness = -40;

		private int contrast = 25;

		private int width;

		private int height;

		private string cacheFolder;

		private string fileName;

		private Bitmap thumbImage;
	}
}
