using QuoterPlanImaging;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class Plan : BaseFileInfo
    {
        private readonly UndoManager undoManager;

        private int _imageWidth;

        private int _imageHeight;

        private int _brightness;

        private int _contrast;

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

        public bool CanRedo
        {
            get
            {
                if (this.undoManager == null)
                {
                    return false;
                }
                return this.undoManager.CanRedo;
            }
        }

        public bool CanUndo
        {
            get
            {
                if (this.undoManager == null)
                {
                    return false;
                }
                return this.undoManager.CanUndo;
            }
        }

        public string Comment
        {
            get;
            set;
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

        public Bookmark DefaultBookmark
        {
            get;
            private set;
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

        public Layers Layers
        {
            get;
            set;
        }

        public bool Pinned
        {
            get;
            set;
        }

        public int ReorderIndex
        {
            get;
            set;
        }

        public int SortIndex
        {
            get;
            set;
        }

        public Thumbnail Thumbnail
        {
            get;
            private set;
        }

        public UnitScale UnitScale
        {
            get;
            set;
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
            this.Thumbnail = new Thumbnail(0x100, 192);
            this.Brightness = brightness;
            this.Contrast = contrast;
            this.DefaultBookmark = new Bookmark("Default", 0, -1, new Point(0, 0));
            this.undoManager = new UndoManager();
        }

        public void AddCommandToHistory(Command command)
        {
            this.undoManager.AddCommandToHistory(command);
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

        public void ClearHistory()
        {
            this.undoManager.ClearHistory();
        }

        public void CreateCache(Bitmap image)
        {
            if (image == null)
            {
                return;
            }
            this.Save(image, string.Concat(base.FullFileName, "~"), Utilities.GetImageFormatFromExtension(base.FullFileName));
        }

        private void CreateDefaultLayer()
        {
            this.Layers.CreateDefaultLayer();
        }

        public void CreateDefaultLayers()
        {
            if (!this.LoadDefaultListOfLayers())
            {
                this.CreateDefaultLayer();
            }
        }

        public void CreateThumbnail(bool loadFromCacheOnly)
        {
            this.Thumbnail.Load(Utilities.GetThumbnailsFolder(), base.FullFileName, loadFromCacheOnly);
        }

        public void DeleteCache()
        {
            if (Utilities.FileExists(string.Concat(base.FullFileName, "~")))
            {
                Utilities.FileDelete(string.Concat(base.FullFileName, "~"), true);
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

        public void DeleteThumbnail()
        {
            this.Thumbnail.DeleteFile();
        }

        public void GetImageDimension()
        {
            float single = 0f;
            float single1 = 0f;
            Exception exception = null;
            Utilities.GetImageDimension(base.FullFileName, ref this._imageWidth, ref this._imageHeight, ref single, ref single1, ref exception);
            this.UnitScale.ReferenceDpiX = single;
            this.UnitScale.ReferenceDpiY = single1;
        }

        private bool LoadDefaultListOfLayers()
        {
            string defaultLayersFileName = Utilities.GetDefaultLayersFileName();
            if (!Utilities.FileExists(defaultLayersFileName))
            {
                return false;
            }
            return this.LoadListOfLayers(defaultLayersFileName);
        }

        public void LoadLayers(string fileName)
        {
            this.Layers.Clear(false);
            if (!this.LoadListOfLayers(fileName))
            {
                this.CreateDefaultLayers();
            }
        }

        private bool LoadListOfLayers(string fileName)
        {
            string str = Utilities.ReadToString(fileName);
            if (str == string.Empty)
            {
                return false;
            }
            char[] chrArray = new char[] { '\r', '\n' };
            string[] fields = Utilities.GetFields(str, chrArray);
            for (int i = 0; i < (int)fields.Length; i++)
            {
                string str1 = fields[i];
                int num = 150;
                string empty = string.Empty;
                string[] strArrays = Utilities.GetFields(str1, ';');
                if (strArrays.GetUpperBound(0) >= 0)
                {
                    empty = strArrays.GetValue(0).ToString();
                    if (strArrays.GetUpperBound(0) >= 1)
                    {
                        num = Utilities.ConvertToInt(strArrays.GetValue(1));
                        num = (num < 0 ? 0 : num);
                    }
                }
                if (empty != string.Empty)
                {
                    this.Layers.CreateNewLayer(empty, num);
                }
            }
            return this.Layers.Count > 0;
        }

        public void Redo()
        {
            this.undoManager.Redo();
        }

        public void ReloadThumbnail()
        {
            if (!this.Thumbnail.IsValid())
            {
                return;
            }
            this.Thumbnail.Reload();
        }

        public void RemoveCommandFromHistory(Command command)
        {
            this.undoManager.RemoveCommandFromHistory(command);
        }

        public void RenameLayerFromUndoManager(string oldName, string newName)
        {
            this.undoManager.RenameLayer(oldName, newName);
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

        public void SaveFromViewer(Bitmap image)
        {
            Exception exception = null;
            bool flag = Imaging.IsGrayScale(image, ref exception);
            if (exception != null)
            {
                Utilities.DisplaySystemError(exception);
                return;
            }
            if (flag)
            {
                this.SaveToGrayscale(image);
                return;
            }
            this.SaveToColor(image);
        }

        public bool SaveLayers(string fileName)
        {
            string empty = string.Empty;
            foreach (Layer collection in this.Layers.Collection)
            {
                object obj = empty;
                object[] name = new object[] { obj, collection.Name, ";", collection.Opacity, Environment.NewLine };
                empty = string.Concat(name);
            }
            return Utilities.SaveStringToFile(fileName, empty);
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

        private void SaveToGrayscale(Bitmap image)
        {
            if (image == null)
            {
                return;
            }
            Exception exception = null;
            using (Bitmap grayscale = Imaging.ColorToGrayscale(image, ref exception))
            {
                if (grayscale != null)
                {
                    grayscale.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                    this.Save(grayscale, base.FullFileName, Utilities.GetImageFormatFromExtension(base.FullFileName));
                }
                else if (exception != null)
                {
                    Utilities.DisplaySystemError(exception);
                }
            }
            GC.Collect();
        }

        public void Undo()
        {
            this.undoManager.Undo();
        }
    }
}