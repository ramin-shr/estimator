using QuoterPlanControls;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class DrawAngle : DrawPolyLine
    {
        public double Angle
        {
            get
            {
                double num = 0;
                if (base.PointArray.Count >= 3)
                {
                    Point item = (Point)base.PointArray[0];
                    Point point = (Point)base.PointArray[1];
                    Point item1 = (Point)base.PointArray[2];
                    num = base.Angle(item, point, item1);
                }
                return num;
            }
        }

        public DrawAngle.AngleTypeEnum AngleType
        {
            get;
            set;
        }

        public DrawAngle()
        {
            base.PointArray = new ArrayList();
            base.ZOrder = 0;
            base.FigureCompleted = false;
            base.Initialize();
        }

        public DrawAngle(int x1, int y1, int x2, int y2, PointF offset, string name, Color lineColor, int opacity, int lineWidth, DrawAngle.AngleTypeEnum angleType)
        {
            base.PointArray = new ArrayList();
            base.PointArray.Add(new Point(x1, y1));
            base.PointArray.Add(new Point(x2, y2));
            base.Offset = offset;
            base.Color = lineColor;
            base.Opacity = opacity;
            base.PenWidth = lineWidth;
            base.ZOrder = 0;
            base.Name = name;
            base.FigureCompleted = false;
            this.AngleType = angleType;
            base.Initialize();
        }

        public DrawAngle(int x1, int y1, int x2, int y2, int x3, int y3, PointF offset, string name, Color lineColor, int opacity, int lineWidth, DrawAngle.AngleTypeEnum angleType)
        {
            base.PointArray = new ArrayList();
            base.PointArray.Add(new Point(x1, y1));
            base.PointArray.Add(new Point(x2, y2));
            base.PointArray.Add(new Point(x3, y3));
            base.Offset = offset;
            base.Color = lineColor;
            base.Opacity = opacity;
            base.PenWidth = lineWidth;
            base.ZOrder = 0;
            base.Name = name;
            base.FigureCompleted = true;
            this.AngleType = angleType;
            base.Initialize();
        }

        public override DrawObject Clone()
        {
            DrawAngle drawAngle = new DrawAngle()
            {
                PointArray = new ArrayList()
            };
            for (int i = 0; i < base.PointArray.Count; i++)
            {
                Point item = (Point)base.PointArray[i];
                drawAngle.PointArray.Add(new Point(item.X, item.Y));
            }
            drawAngle.CloseFigure = false;
            drawAngle.FigureCompleted = true;
            drawAngle.AngleType = this.AngleType;
            drawAngle.DisplayName = new Utilities.DisplayName(drawAngle, "");
            base.FillDrawObjectFields(drawAngle);
            return drawAngle;
        }

        public override void Draw(Graphics g, int offsetX, int offsetY, bool printToScreen = true, MainForm.ImageQualityEnum imageQuality = 1)
        {
            Pen pen;
            TextRenderingHint textRenderingHint;
            SmoothingMode smoothingMode = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.HighQuality;
            TextRenderingHint textRenderingHint1 = g.TextRenderingHint;
            Graphics graphic = g;
            if (printToScreen)
            {
                textRenderingHint = (imageQuality == MainForm.ImageQualityEnum.QualityHigh ? TextRenderingHint.AntiAliasGridFit : TextRenderingHint.SingleBitPerPixel);
            }
            else
            {
                textRenderingHint = TextRenderingHint.ClearTypeGridFit;
            }
            graphic.TextRenderingHint = textRenderingHint;
            pen = (base.DrawPen != null ? (Pen)base.DrawPen.Clone() : new Pen(Color.FromArgb(base.Opacity + 30, base.Color), (float)base.PenWidth));
            pen.DashStyle = DashStyle.Dash;
            pen.StartCap = LineCap.ArrowAnchor;
            pen.EndCap = LineCap.ArrowAnchor;
            base.ClearTextArray();
            if (base.PointArray.Count >= 3)
            {
                string angleString = base.DrawArea.ToAngleString(this.Angle, this.AngleType);
                DrawText drawText = new DrawText(angleString, this.Center, 0);
                base.TextArray.Insert(0, drawText);
            }
            GraphicsPath graphicsPath = new GraphicsPath();
            for (int i = 0; i < base.PointArray.Count - 1; i++)
            {
                Point item = (Point)base.PointArray[i];
                Point x = (Point)base.PointArray[i + 1];
                item.X = item.X - offsetX;
                item.Y = item.Y - offsetY;
                x.X = x.X - offsetX;
                x.Y = x.Y - offsetY;
                graphicsPath.AddLine(item, x);
            }
            try
            {
                g.DrawPath(pen, graphicsPath);
            }
            catch
            {
            }
            graphicsPath.Dispose();
            pen.Dispose();
            g.TextRenderingHint = textRenderingHint1;
            g.SmoothingMode = smoothingMode;
        }

        public override void DrawText(Graphics g, int offsetX, int offsetY, bool printToScreen = true, MainForm.ImageQualityEnum imageQuality = 1, float defaultFontSize = 12f)
        {
            if (!base.ShowMeasure || base.TextArray.Count == 0)
            {
                return;
            }
            if (base.DrawingBoard.ZoomFactor >= 0.15)
            {
                ((DrawText)base.TextArray[0]).Draw(g, offsetX, offsetY, (float)base.DrawingBoard.ZoomFactor, base.Opacity, printToScreen, imageQuality, defaultFontSize);
            }
            base.TextDirty = false;
        }

        public override int GetHandleSize()
        {
            return 4;
        }

        public enum AngleTypeEnum
        {
            AngleDegree,
            AngleSlope,
            AngleEnumCount
        }
    }
}