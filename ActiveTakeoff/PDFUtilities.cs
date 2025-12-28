using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Foxit.PDF.Rasterizer;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using TallComponents.PDF.Rasterizer;

namespace QuoterPlan
{
	public class PDFUtilities
	{
		public static PdfRasterizer OpenPDFFile(string fileName)
		{
			PdfRasterizer result;
			try
			{
				PdfRasterizer pdfRasterizer = new PdfRasterizer(fileName);
				result = pdfRasterizer;
			}
			catch (Exception exception)
			{
				Utilities.DisplayFileOpenError(fileName, exception);
				result = null;
			}
			return result;
		}

		public static int PagesCount(string fileName)
		{
			int result = 0;
			PdfRasterizer pdfRasterizer = PDFUtilities.OpenPDFFile(fileName);
			if (pdfRasterizer != null)
			{
				result = pdfRasterizer.Pages.Count;
			}
			GC.Collect();
			return result;
		}

		public static bool GetPageDimension(string fileName, int pageIndex, out double width, out double height)
		{
			bool result;
			try
			{
				try
				{
					using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
					{
						byte[] array = new byte[fileStream.Length];
						fileStream.Read(array, 0, array.Length);
						Document document = new Document(new BinaryReader(new MemoryStream(array)));
						Page page = document.Pages[pageIndex];
						height = page.Height;
						width = page.Width;
					}
				}
				catch (Exception exception)
				{
					Utilities.DisplayFileOpenError(fileName, exception);
					height = 0.0;
					width = 0.0;
					return false;
				}
				result = true;
			}
			catch (Exception exception2)
			{
				Utilities.DisplaySystemError(exception2);
				height = 0.0;
				width = 0.0;
				result = false;
			}
			return result;
		}

		private static bool GetPageScale(double width, double height, out float scale, int dpi, out double maxDimension)
		{
			scale = 0f;
			maxDimension = 0.0;
			bool result;
			try
			{
				scale = (float)dpi / 72f;
				maxDimension = width * (double)scale * (height * (double)scale);
				result = true;
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
				result = false;
			}
			return result;
		}

		public static bool ConvertPDFPageToImage(PdfRasterizer document, double width, double height, int pageIndex, string destinationFileName, double maxSize, int dpi, bool convertToGrayscale)
		{
			bool result;
			try
			{
				float num = 0f;
				double num2 = 0.0;
				int num3 = dpi;
				do
				{
					PDFUtilities.GetPageScale(height, width, out num, dpi, out num2);
					if (num2 <= maxSize)
					{
						break;
					}
					dpi--;
				}
				while (dpi >= 150);
				Console.WriteLine("Target DPI=" + num3.ToString() + ", Effective DPI=" + dpi.ToString());
				FixedImageSize imageSize = new FixedImageSize((float)((int)((double)num * width)), (float)((int)((double)num * height)));
				PdfRasterizerPage pdfRasterizerPage = document.Pages[pageIndex];
				Bitmap bitmap = pdfRasterizerPage.DrawToBitmap(convertToGrayscale ? BitmapColorFormat.Grayscale : BitmapColorFormat.Rgb, imageSize);
				bitmap.SetResolution((float)dpi, (float)dpi);
				bitmap.Save(destinationFileName, System.Drawing.Imaging.ImageFormat.Png);
				result = true;
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
				result = false;
			}
			return result;
		}

		public static string GetPageUniqueFileName(string fileName)
		{
			FileInfo fileInfo = Utilities.MakeUniqueFileName(fileName);
			if (fileInfo != null)
			{
				return fileInfo.FullName;
			}
			return "";
		}

		public static void SetPDFExportDocumentProperties(PdfDocument pdfDocument, string title, string author, string subject, string keywords, string creator)
		{
			pdfDocument.Info.Title = title;
			pdfDocument.Info.Author = author;
			pdfDocument.Info.Subject = subject;
			pdfDocument.Info.Keywords = keywords;
			pdfDocument.Info.Creator = creator;
		}

		public static bool AddPageToPDFExportDocument(PdfDocument pdfDocument, Image sourceImage)
		{
			XImage ximage = null;
			XGraphics xgraphics = null;
			bool result = false;
			try
			{
				PdfPage pdfPage = new PdfPage();
				ximage = XImage.FromGdiPlusImage(sourceImage);
				double num = (double)ximage.PixelWidth;
				double num2 = (double)ximage.PixelHeight;
				pdfPage.Width = num;
				pdfPage.Height = num2;
				pdfDocument.Pages.Add(pdfPage);
				xgraphics = XGraphics.FromPdfPage(pdfPage);
				xgraphics.DrawImage(ximage, 0.0, 0.0, num, num2);
				result = true;
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
			}
			if (ximage != null)
			{
				ximage.Dispose();
			}
			if (xgraphics != null)
			{
				xgraphics.Dispose();
			}
			GC.Collect();
			return result;
		}

		public PDFUtilities()
		{
		}
	}
}
