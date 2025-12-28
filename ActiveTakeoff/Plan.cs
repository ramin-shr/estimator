using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using QuoterPlanImaging;

namespace QuoterPlan
{
	public class Plan : BaseFileInfo
	{
		public bool Pinned
		{
			[CompilerGenerated]
			get
			{
				return this.<Pinned>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Pinned>k__BackingField = value;
			}
		}

		public string Comment
		{
			[CompilerGenerated]
			get
			{
				return this.<Comment>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Comment>k__BackingField = value;
			}
		}

		public Layers Layers
		{
			[CompilerGenerated]
			get
			{
				return this.<Layers>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Layers>k__BackingField = value;
			}
		}

		public UnitScale UnitScale
		{
			[CompilerGenerated]
			get
			{
				return this.<UnitScale>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UnitScale>k__BackingField = value;
			}
		}

		public Thumbnail Thumbnail
		{
			[CompilerGenerated]
			get
			{
				return this.<Thumbnail>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Thumbnail>k__BackingField = value;
			}
		}

		public Bookmark DefaultBookmark
		{
			[CompilerGenerated]
			get
			{
				return this.<DefaultBookmark>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<DefaultBookmark>k__BackingField = value;
			}
		}

		public int SortIndex
		{
			[CompilerGenerated]
			get
			{
				return this.<SortIndex>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SortIndex>k__BackingField = value;
			}
		}

		public int ReorderIndex
		{
			[CompilerGenerated]
			get
			{
				return this.<ReorderIndex>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ReorderIndex>k__BackingField = value;
			}
		}

		public int ImageWidth
		{
			get
			{
				return this._imageWidth;
			}
			set
			{
				this._imageWidth = value;
			}
		}

		public int ImageHeight
		{
			get
			{
				return this._imageHeight;
			}
			set
			{
				this._imageHeight = value;
			}
		}

		public int Brightness
		{
			get
			{
				return this._brightness;
			}
			set
			{
				this._brightness = value;
				this.Thumbnail.Brightness = value;
			}
		}

		public int Contrast
		{
			get
			{
				return this._contrast;
			}
			set
			{
				this._contrast = value;
				this.Thumbnail.Contrast = value;
			}
		}

		public bool CanUndo
		{
			get
			{
				return this.undoManager != null && this.undoManager.CanUndo;
			}
		}

		public bool CanRedo
		{
			get
			{
				return this.undoManager != null && this.undoManager.CanRedo;
			}
		}

		public void ClearHistory()
		{
			this.undoManager.ClearHistory();
		}

		public void AddCommandToHistory(Command command)
		{
			this.undoManager.AddCommandToHistory(command);
		}

		public void RemoveCommandFromHistory(Command command)
		{
			this.undoManager.RemoveCommandFromHistory(command);
		}

		public void RenameLayerFromUndoManager(string oldName, string newName)
		{
			this.undoManager.RenameLayer(oldName, newName);
		}

		public void Undo()
		{
			this.undoManager.Undo();
		}

		public void Redo()
		{
			this.undoManager.Redo();
		}

		public Plan(string name, string fileName, bool pinned, int brightness, int contrast)
		{
			base.Name = name;
			base.FullFileName = fileName;
			this.Pinned = pinned;
			this.Comment = "";
			this.ImageWidth = 0;
			this.ImageHeight = 0;
			this.Layers = new Layers();
			this.UnitScale = new UnitScale();
			this.Thumbnail = new Thumbnail(256, 192);
			this.Brightness = brightness;
			this.Contrast = contrast;
			this.DefaultBookmark = new Bookmark("Default", 0, -1, new Point(0, 0));
			this.undoManager = new UndoManager();
		}

		public override void Clear()
		{
			if (base.Dirty)
			{
				this.DeleteFile();
			}
			base.Clear();
			this.Pinned = false;
			this.ImageWidth = 0;
			this.ImageHeight = 0;
			this.Brightness = 0;
			this.Contrast = 0;
			this.Comment = "";
			this.Layers.Clear(false);
			this.Thumbnail.Clear();
			this.UnitScale.ReferenceDpiX = 96f;
			this.UnitScale.ReferenceDpiY = 96f;
			this.undoManager.ClearHistory();
		}

		private void CreateDefaultLayer()
		{
			this.Layers.CreateDefaultLayer();
		}

		private bool LoadListOfLayers(string fileName)
		{
			string text = Utilities.ReadToString(fileName);
			if (text != string.Empty)
			{
				string[] fields = Utilities.GetFields(text, new char[]
				{
					'\r',
					'\n'
				});
				foreach (string originalString in fields)
				{
					int num = 150;
					string text2 = string.Empty;
					string[] fields2 = Utilities.GetFields(originalString, ';');
					if (fields2.GetUpperBound(0) >= 0)
					{
						text2 = fields2.GetValue(0).ToString();
						if (fields2.GetUpperBound(0) >= 1)
						{
							num = Utilities.ConvertToInt(fields2.GetValue(1));
							num = ((num < 0) ? 0 : num);
						}
					}
					if (text2 != string.Empty)
					{
						this.Layers.CreateNewLayer(text2, num);
					}
				}
				return this.Layers.Count > 0;
			}
			return false;
		}

		private bool LoadDefaultListOfLayers()
		{
			string defaultLayersFileName = Utilities.GetDefaultLayersFileName();
			return Utilities.FileExists(defaultLayersFileName) && this.LoadListOfLayers(defaultLayersFileName);
		}

		public void CreateDefaultLayers()
		{
			if (!this.LoadDefaultListOfLayers())
			{
				this.CreateDefaultLayer();
			}
		}

		public bool SaveLayers(string fileName)
		{
			string text = string.Empty;
			foreach (object obj in this.Layers.Collection)
			{
				Layer layer = (Layer)obj;
				object obj2 = text;
				text = string.Concat(new object[]
				{
					obj2,
					layer.Name,
					";",
					layer.Opacity,
					Environment.NewLine
				});
			}
			return Utilities.SaveStringToFile(fileName, text);
		}

		public void LoadLayers(string fileName)
		{
			this.Layers.Clear(false);
			if (!this.LoadListOfLayers(fileName))
			{
				this.CreateDefaultLayers();
			}
		}

		public void GetImageDimension()
		{
			float referenceDpiX = 0f;
			float referenceDpiY = 0f;
			Exception ex = null;
			Utilities.GetImageDimension(base.FullFileName, ref this._imageWidth, ref this._imageHeight, ref referenceDpiX, ref referenceDpiY, ref ex);
			this.UnitScale.ReferenceDpiX = referenceDpiX;
			this.UnitScale.ReferenceDpiY = referenceDpiY;
		}

		public void ReloadThumbnail()
		{
			if (!this.Thumbnail.IsValid())
			{
				return;
			}
			this.Thumbnail.Reload();
		}

		public void CreateThumbnail(bool loadFromCacheOnly)
		{
			this.Thumbnail.Load(Utilities.GetThumbnailsFolder(), base.FullFileName, loadFromCacheOnly);
		}

		public void DeleteThumbnail()
		{
			this.Thumbnail.DeleteFile();
		}

		public void SaveFromViewer(Bitmap image)
		{
			Exception ex = null;
			bool flag = Imaging.IsGrayScale(image, ref ex);
			if (ex != null)
			{
				Utilities.DisplaySystemError(ex);
				return;
			}
			if (flag)
			{
				this.SaveToGrayscale(image);
				return;
			}
			this.SaveToColor(image);
		}

		private void SaveToGrayscale(Bitmap image)
		{
			if (image == null)
			{
				return;
			}
			Exception ex = null;
			using (Bitmap bitmap = Imaging.ColorToGrayscale(image, ref ex))
			{
				if (bitmap != null)
				{
					bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
					this.Save(bitmap, base.FullFileName, Utilities.GetImageFormatFromExtension(base.FullFileName));
				}
				else if (ex != null)
				{
					Utilities.DisplaySystemError(ex);
				}
			}
			GC.Collect();
		}

		private void SaveToColor(Bitmap image)
		{
			if (image == null)
			{
				return;
			}
			try
			{
				using (Bitmap bitmap = image.Clone(new Rectangle(0, 0, image.Width, image.Height), PixelFormat.Format24bppRgb))
				{
					if (bitmap != null)
					{
						bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
						this.Save(bitmap, base.FullFileName, Utilities.GetImageFormatFromExtension(base.FullFileName));
					}
				}
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
			}
			GC.Collect();
		}

		private void Save(Bitmap image, string fileName, ImageFormat imageFormat)
		{
			if (image == null)
			{
				return;
			}
			try
			{
				FileStream fileStream = new FileStream(fileName, FileMode.Create);
				image.Save(fileStream, imageFormat);
				fileStream.Close();
			}
			catch (Exception exception)
			{
				Utilities.DisplayFileSaveError(fileName, exception);
			}
		}

		public void CreateCache(Bitmap image)
		{
			if (image == null)
			{
				return;
			}
			this.Save(image, base.FullFileName + "~", Utilities.GetImageFormatFromExtension(base.FullFileName));
		}

		public void DeleteCache()
		{
			if (Utilities.FileExists(base.FullFileName + "~"))
			{
				Utilities.FileDelete(base.FullFileName + "~", true);
			}
		}

		public void DeleteFile()
		{
			if (Utilities.FileExists(base.FullFileName))
			{
				Utilities.FileDelete(base.FullFileName, true);
			}
			if (Utilities.DirectoryEmpty(base.FolderName))
			{
				Utilities.DirectoryDelete(base.FolderName);
			}
		}

		private readonly UndoManager undoManager;

		private int _imageWidth;

		private int _imageHeight;

		private int _brightness;

		private int _contrast;

		[CompilerGenerated]
		private bool <Pinned>k__BackingField;

		[CompilerGenerated]
		private string <Comment>k__BackingField;

		[CompilerGenerated]
		private Layers <Layers>k__BackingField;

		[CompilerGenerated]
		private UnitScale <UnitScale>k__BackingField;

		[CompilerGenerated]
		private Thumbnail <Thumbnail>k__BackingField;

		[CompilerGenerated]
		private Bookmark <DefaultBookmark>k__BackingField;

		[CompilerGenerated]
		private int <SortIndex>k__BackingField;

		[CompilerGenerated]
		private int <ReorderIndex>k__BackingField;
	}
}
