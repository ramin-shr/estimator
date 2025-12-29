using QuoterPlan.Properties;
using QuoterPlanControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace QuoterPlan
{
    public class DrawPolyLine : DrawLine
    {
        private ArrayList pointArray;

        private ArrayList deductionArray;

        private ArrayList dropArray;

        private Dictionary<string, CustomRenderingProperties> customRenderingArray = new Dictionary<string, CustomRenderingProperties>();

        private HatchStylePickerCombo.HatchStylePickerEnum pattern = HatchStylePickerCombo.HatchStylePickerEnum.Solid;

        private Segment selectedSegment = new Segment();

        private bool closeFigure;

        private bool figureCompleted;

        private bool editDeductions;

        private bool pointInObject;

        private bool pointIsOnOutline;

        private bool compressLeft;

        private bool compressTop;

        private bool expandLeft;

        private bool expandTop;

        private int toolTipMinThreshold = 50;

        private int toolTipMaxThreshold = 100;

        private string customRenderingTooltip = string.Empty;

        private UnitScale unitScale;

        [CompilerGenerated]
        // CS$<>9__CachedAnonymousMethodDelegated
        private static Func<Point, int> CSu0024u003cu003e9__CachedAnonymousMethodDelegated;

        [CompilerGenerated]
        // CS$<>9__CachedAnonymousMethodDelegatee
        private static Func<Point, int> CSu0024u003cu003e9__CachedAnonymousMethodDelegatee;

        public override int ActiveHandleCount
        {
            get
            {
                if (this.FigureCompleted)
                {
                    return this.pointArray.Count;
                }
                return this.pointArray.Count - 1;
            }
        }

        public override Rectangle BoundingRectangle
        {
            get
            {
                Point empty = Point.Empty;
                Point x = Point.Empty;
                Point y = Point.Empty;
                for (int i = 0; i < this.pointArray.Count; i++)
                {
                    empty = (Point)this.pointArray[i];
                    if (x != Point.Empty)
                    {
                        if (empty.X < x.X)
                        {
                            x.X = empty.X;
                        }
                        if (empty.Y < x.Y)
                        {
                            x.Y = empty.Y;
                        }
                    }
                    else
                    {
                        x = empty;
                    }
                    if (empty.X > y.X)
                    {
                        y.X = empty.X;
                    }
                    if (empty.Y > y.Y)
                    {
                        y.Y = empty.Y;
                    }
                }
                return new Rectangle(x.X, x.Y, Math.Abs(y.X - x.X), Math.Abs(y.Y - x.Y));
            }
        }

        public override Point Center
        {
            get
            {
                return this.ComputeCenter();
            }
        }

        public bool CloseFigure
        {
            get
            {
                return this.closeFigure;
            }
            set
            {
                this.closeFigure = value;
            }
        }

        public bool ComputingCustomRendering
        {
            get
            {
                return this.unitScale != null;
            }
        }

        public Dictionary<string, CustomRenderingProperties> CustomRenderingArray
        {
            get
            {
                return this.customRenderingArray;
            }
        }

        public string CustomRenderingTooltip
        {
            get
            {
                return this.customRenderingTooltip;
            }
            set
            {
                this.customRenderingTooltip = value;
            }
        }

        public ArrayList DeductionArray
        {
            get
            {
                return this.deductionArray;
            }
            set
            {
                this.deductionArray = value;
            }
        }

        public ArrayList DropArray
        {
            get
            {
                return this.dropArray;
            }
            set
            {
                this.dropArray = value;
            }
        }

        public bool EditDeductions
        {
            get
            {
                return this.editDeductions;
            }
            set
            {
                this.editDeductions = value;
            }
        }

        public new bool FigureCompleted
        {
            get
            {
                return this.figureCompleted;
            }
            set
            {
                this.figureCompleted = value;
            }
        }

        public override int HandleCount
        {
            get
            {
                return this.pointArray.Count;
            }
        }

        public HatchStylePickerCombo.HatchStylePickerEnum Pattern
        {
            get
            {
                return this.ValidateHatchStyle(this.pattern);
            }
            set
            {
                this.pattern = this.ValidateHatchStyle(value);
            }
        }

        public ArrayList PointArray
        {
            get
            {
                return this.pointArray;
            }
            set
            {
                this.pointArray = value;
            }
        }

        public bool PointIsOnOutline
        {
            get
            {
                return this.pointIsOnOutline;
            }
        }

        public override bool Selected
        {
            get
            {
                return this.selected;
            }
            set
            {
                if (!value)
                {
                    this.SelectedSegment.Reset();
                }
                this.selected = value;
            }
        }

        public Segment SelectedSegment
        {
            get
            {
                return this.selectedSegment;
            }
            set
            {
                this.selectedSegment = value;
            }
        }

        public GroupStats Stats
        {
            get
            {
                GroupStats groupStat = new GroupStats(base.ObjectType)
                {
                    GroupCount = 1,
                    Perimeter = this.ComputePerimeter(),
                    DeductionsCount = this.deductionArray.Count
                };
                foreach (DrawObject drawObject in this.deductionArray)
                {
                    if (drawObject.ObjectType != "Area")
                    {
                        if (drawObject.ObjectType != "Line")
                        {
                            continue;
                        }
                        GroupStats deductionPerimeter = groupStat;
                        deductionPerimeter.DeductionPerimeter = deductionPerimeter.DeductionPerimeter + (double)((DrawLine)drawObject).Distance2D(true);
                    }
                    else
                    {
                        GroupStats deductionArea = groupStat;
                        deductionArea.DeductionArea = deductionArea.DeductionArea + ((DrawPolyLine)drawObject).ComputeArea(true);
                        GroupStats deductionPerimeter1 = groupStat;
                        deductionPerimeter1.DeductionPerimeter = deductionPerimeter1.DeductionPerimeter + ((DrawPolyLine)drawObject).ComputePerimeter();
                    }
                }
                if (!this.closeFigure)
                {
                    groupStat.EndsCount = 2 + this.deductionArray.Count * 2;
                    groupStat.SegmentsCount = this.pointArray.Count - (1 + this.deductionArray.Count);
                    groupStat.CornersCount = this.CornersCount();
                }
                else
                {
                    groupStat.Area = this.ComputeArea(true);
                    groupStat.EndsCount = this.deductionArray.Count * 2;
                    groupStat.SegmentsCount = this.pointArray.Count - this.deductionArray.Count;
                    groupStat.CornersCount = this.CornersCount();
                }
                foreach (KeyValuePair<string, CustomRenderingProperties> customRenderingArray in this.CustomRenderingArray)
                {
                    CustomRenderingProperties value = customRenderingArray.Value;
                    Dictionary<string, CustomRenderingResult> strs = new Dictionary<string, CustomRenderingResult>();
                    foreach (KeyValuePair<string, CustomRenderingResult> resultsArray in value.ResultsArray)
                    {
                        strs.Add(resultsArray.Key, resultsArray.Value.Clone());
                    }
                    groupStat.CustomRenderingArray.Add(customRenderingArray.Key, strs);
                }
                return groupStat;
            }
        }

        public UnitScale UnitScale
        {
            get
            {
                if (this.unitScale != null)
                {
                    return this.unitScale;
                }
                return base.DrawArea.UnitScale;
            }
            set
            {
                this.unitScale = value;
            }
        }

        public DrawPolyLine()
        {
            this.pointArray = new ArrayList();
            this.deductionArray = new ArrayList();
            this.dropArray = new ArrayList();
            this.FigureCompleted = false;
            base.Initialize();
        }

        public DrawPolyLine(PointF offset, string name, int groupID, string comment, Color lineColor, int opacity, int lineWidth)
        {
            this.pointArray = new ArrayList();
            this.deductionArray = new ArrayList();
            this.dropArray = new ArrayList();
            base.Offset = offset;
            base.Color = lineColor;
            base.Opacity = opacity;
            base.Filled = false;
            this.CloseFigure = false;
            base.PenWidth = lineWidth;
            this.FigureCompleted = true;
            base.Name = name;
            base.GroupID = groupID;
            base.Comment = comment;
            base.Initialize();
        }

        public DrawPolyLine(PointF offset, string name, int groupID, string comment, Color lineColor, Color fillColor, HatchStylePickerCombo.HatchStylePickerEnum pattern, int opacity, int lineWidth)
        {
            this.pointArray = new ArrayList();
            this.deductionArray = new ArrayList();
            this.dropArray = new ArrayList();
            base.Offset = offset;
            base.Color = lineColor;
            base.FillColor = fillColor;
            this.Pattern = pattern;
            base.Opacity = opacity;
            base.Filled = true;
            this.CloseFigure = true;
            base.PenWidth = lineWidth;
            this.FigureCompleted = true;
            base.Name = name;
            base.GroupID = groupID;
            base.Comment = comment;
            base.Initialize();
        }

        public DrawPolyLine(int x1, int y1, int x2, int y2, PointF offset, string name, int groupID, string comment, Color lineColor, Color fillColor, HatchStylePickerCombo.HatchStylePickerEnum pattern, int opacity, bool filled, bool closeFigure, int lineWidth)
        {
            this.pointArray = new ArrayList();
            this.pointArray.Add(new Point(x1, y1));
            this.pointArray.Add(new Point(x2, y2));
            this.deductionArray = new ArrayList();
            this.dropArray = new ArrayList();
            base.Offset = offset;
            base.Color = lineColor;
            base.FillColor = fillColor;
            this.Pattern = pattern;
            base.Opacity = opacity;
            base.Filled = filled;
            this.CloseFigure = closeFigure;
            base.PenWidth = lineWidth;
            base.Name = name;
            base.GroupID = groupID;
            base.Comment = comment;
            this.FigureCompleted = false;
            base.Initialize();
        }

        [CompilerGenerated]
        // <DetermineWindingDirection>b__a
        private static int u003cDetermineWindingDirectionu003eb__a(Point p)
        {
            return p.X;
        }

        [CompilerGenerated]
        // <DetermineWindingDirection>b__b
        private static int u003cDetermineWindingDirectionu003eb__b(Point p)
        {
            return p.Y;
        }

        public void AddPoint(Point point)
        {
            this.pointArray.Add(point);
        }

        private Point AdjustSegmentToImage(Bitmap image, Point point1, Point point2, int angle, int normalizedAngle, int maxOffset)
        {
            Point x = new Point();
            ArrayList arrayLists = new ArrayList();
            ArrayList point = new ArrayList();
            for (int i = 1; i <= 100; i++)
            {
                float single = (float)i / 100f;
                x.X = (int)((float)point1.X + single * (float)(point2.X - point1.X));
                x.Y = (int)((float)point1.Y + single * (float)(point2.Y - point1.Y));
                Color pixel = image.GetPixel(x.X, x.Y);
                if (pixel.R != 0xff || pixel.G != 0xff || pixel.B != 0xff)
                {
                    arrayLists.Add(new Point(x.X, x.Y));
                }
                else
                {
                    point.Add(new Point(x.X, x.Y));
                }
            }
            Console.WriteLine(string.Concat("normalizedAngle = ", normalizedAngle));
            if (arrayLists.Count >= point.Count)
            {
                Console.WriteLine(string.Concat("hit % = ", (float)arrayLists.Count / 100f * 100f));
                int num = 0;
                Point[] pointArray = new Point[2];
                int color = this.MatchSegmentToColor(image, arrayLists, angle, normalizedAngle, Color.White, false, maxOffset, ref pointArray[0], ref pointArray[1], ref num);
                if (normalizedAngle == 0 || normalizedAngle == 0x10e)
                {
                    color -= 2;
                }
                else
                {
                    color += 2;
                }
                x = (normalizedAngle == 0 || normalizedAngle == 180 ? new Point(point1.X, color) : new Point(color, point1.Y));
                return x;
            }
            Console.WriteLine(string.Concat("miss % = ", (float)point.Count / 100f * 100f));
            int[] numArray = new int[2];
            int[] numArray1 = new int[2];
            Point[] pointArray1 = new Point[2];
            Point[] pointArray2 = new Point[2];
            numArray[0] = this.MatchSegmentToColor(image, point, angle, normalizedAngle, Color.Black, false, maxOffset, ref pointArray1[0], ref pointArray2[0], ref numArray1[0]);
            numArray[1] = this.MatchSegmentToColor(image, point, angle, normalizedAngle, Color.Black, true, maxOffset, ref pointArray1[1], ref pointArray2[1], ref numArray1[1]);
            int num1 = (numArray1[0] <= numArray1[1] ? 0 : 1);
            Console.WriteLine((num1 == 0 ? "Compress" : "Expand"));
            if (numArray1[num1] > 50)
            {
                x = new Point(0, 0);
            }
            else
            {
                for (int j = 0; j < point.Count; j++)
                {
                    if (normalizedAngle == 0 || normalizedAngle == 180)
                    {
                        Point item = (Point)point[j];
                        point[j] = new Point(item.X, numArray[num1]);
                    }
                    else
                    {
                        int num2 = numArray[num1];
                        Point item1 = (Point)point[j];
                        point[j] = new Point(num2, item1.Y);
                    }
                }
                numArray[num1] = this.MatchSegmentToColor(image, point, angle, normalizedAngle, Color.White, false, maxOffset, ref pointArray1[0], ref pointArray2[0], ref numArray1[num1]);
                if (normalizedAngle == 0 || normalizedAngle == 0x10e)
                {
                    numArray[num1] -= 2;
                }
                else
                {
                    numArray[num1] += 2;
                }
                x = (normalizedAngle == 0 || normalizedAngle == 180 ? new Point(point1.X, numArray[num1]) : new Point(numArray[num1], point1.Y));
            }
            return x;
        }

        public double Angle(Point a, Point b, Point c)
        {
            PointF pointF = new PointF((float)(b.X - a.X), (float)(b.Y - a.Y));
            PointF pointF1 = new PointF((float)(b.X - c.X), (float)(b.Y - c.Y));
            double x = (double)(pointF.X * pointF1.X + pointF.Y * pointF1.Y);
            double num = (double)(pointF.X * pointF1.Y - pointF.Y * pointF1.X);
            double num1 = Math.Atan2(num, x);
            return Math.Abs(num1 * 180 / 3.14159265358979 + 0.5);
        }

        public override bool AutoAdjust(Bitmap image, DrawPolyLine.AutoAdjustType autoAdjustType)
        {
            Point point;
            if (this.pointArray.Count < 4 || !this.FigureCompleted || !this.CloseFigure)
            {
                return false;
            }
            for (int i = 0; i < this.pointArray.Count; i++)
            {
                if (((Point)this.pointArray[i]).X < 0 || ((Point)this.pointArray[i]).Y < 0)
                {
                    string objetHorsCanvenas = Resources.Objet_hors_canvenas;
                    string str = string.Concat(Resources.Impossible_de_compléter_l_opération, "\n", Resources.Veuillez_déplacer_l_objet_sur_le_canvenas_et_recommencer);
                    Utilities.DisplayError(objetHorsCanvenas, str);
                    return false;
                }
            }
            Console.WriteLine("");
            Console.WriteLine("DrawPolyLine::AutoAdjust ENTER");
            bool flag = this.DetermineWindingDirection() == DrawPolyLine.WindingDirection.CounterClockWise;
            Point[] item = new Point[this.pointArray.Count + 1];
            this.pointArray.CopyTo(item, 0);
            item[this.pointArray.Count] = (Point)this.pointArray[0];
            for (int j = 0; j < this.pointArray.Count; j++)
            {
                Point[] pointArray = new Point[] { item[j], item[j + 1] };
                object[] x = new object[] { "(", pointArray[0].X, ",", pointArray[0].Y, ") - (", pointArray[1].X, ",", pointArray[1].Y, ")" };
                string.Concat(x);
                double num = DrawLine.Angle((double)pointArray[0].X, (double)pointArray[0].Y, (double)pointArray[1].X, (double)pointArray[1].Y);
                double num1 = base.RoundAngle(num, 10);
                if (num1 == 0 || num1 == 90 || num1 == 180 || num1 == 270 || autoAdjustType != DrawPolyLine.AutoAdjustType.ToZone)
                {
                    int num2 = (int)num1;
                    if (flag)
                    {
                        int num3 = (int)num1;
                        if (num3 <= 90)
                        {
                            if (num3 == 0)
                            {
                                num2 = 180;
                            }
                            else if (num3 == 90)
                            {
                                num2 = 0x10e;
                            }
                        }
                        else if (num3 == 180)
                        {
                            num2 = 0;
                        }
                        else if (num3 == 0x10e)
                        {
                            num2 = 90;
                        }
                    }
                    if (autoAdjustType != DrawPolyLine.AutoAdjustType.ToZone)
                    {
                        point = new Point(pointArray[0].X, pointArray[0].Y);
                    }
                    else
                    {
                        point = this.AdjustSegmentToImage(image, pointArray[0], pointArray[1], (int)num, num2, 50);
                        this.compressLeft = true;
                        this.compressTop = true;
                        this.expandLeft = true;
                        this.expandTop = true;
                    }
                    if (point.X > 0 && point.Y > 0)
                    {
                        int num4 = num2;
                        if (num4 <= 90)
                        {
                            if (num4 == 0)
                            {
                                switch (autoAdjustType)
                                {
                                    case DrawPolyLine.AutoAdjustType.Compress:
                                        {
                                            point.Y = point.Y + 1;
                                            goto case DrawPolyLine.AutoAdjustType.CompressLeftRight;
                                        }
                                    case DrawPolyLine.AutoAdjustType.CompressTopBottom:
                                        {
                                            if (!this.compressTop)
                                            {
                                                goto case DrawPolyLine.AutoAdjustType.CompressLeftRight;
                                            }
                                            point.Y = point.Y + 1;
                                            goto case DrawPolyLine.AutoAdjustType.CompressLeftRight;
                                        }
                                    case DrawPolyLine.AutoAdjustType.CompressLeftRight:
                                        {
                                            ArrayList arrayLists = this.pointArray;
                                            Point item1 = (Point)this.pointArray[j];
                                            arrayLists[j] = new Point(item1.X, point.Y);
                                            if (j + 1 >= this.pointArray.Count)
                                            {
                                                ArrayList point1 = this.pointArray;
                                                Point item2 = (Point)this.pointArray[0];
                                                point1[0] = new Point(item2.X, point.Y);
                                                break;
                                            }
                                            else
                                            {
                                                ArrayList arrayLists1 = this.pointArray;
                                                Point point2 = (Point)this.pointArray[j + 1];
                                                arrayLists1[j + 1] = new Point(point2.X, point.Y);
                                                break;
                                            }
                                        }
                                    case DrawPolyLine.AutoAdjustType.Expand:
                                        {
                                            point.Y = point.Y - 1;
                                            goto case DrawPolyLine.AutoAdjustType.CompressLeftRight;
                                        }
                                    case DrawPolyLine.AutoAdjustType.ExpandTopBottom:
                                        {
                                            if (!this.expandTop)
                                            {
                                                goto case DrawPolyLine.AutoAdjustType.CompressLeftRight;
                                            }
                                            point.Y = point.Y - 1;
                                            goto case DrawPolyLine.AutoAdjustType.CompressLeftRight;
                                        }
                                    default:
                                        {
                                            goto case DrawPolyLine.AutoAdjustType.CompressLeftRight;
                                        }
                                }
                            }
                            else if (num4 == 90)
                            {
                                switch (autoAdjustType)
                                {
                                    case DrawPolyLine.AutoAdjustType.Compress:
                                        {
                                            point.X = point.X - 1;
                                            goto case DrawPolyLine.AutoAdjustType.ExpandTopBottom;
                                        }
                                    case DrawPolyLine.AutoAdjustType.CompressTopBottom:
                                    case DrawPolyLine.AutoAdjustType.ExpandTopBottom:
                                        {
                                            ArrayList arrayLists2 = this.pointArray;
                                            int x1 = point.X;
                                            Point item3 = (Point)this.pointArray[j];
                                            arrayLists2[j] = new Point(x1, item3.Y);
                                            if (j + 1 >= this.pointArray.Count)
                                            {
                                                ArrayList point3 = this.pointArray;
                                                int x2 = point.X;
                                                Point item4 = (Point)this.pointArray[0];
                                                point3[0] = new Point(x2, item4.Y);
                                                break;
                                            }
                                            else
                                            {
                                                ArrayList arrayLists3 = this.pointArray;
                                                int x3 = point.X;
                                                Point point4 = (Point)this.pointArray[j + 1];
                                                arrayLists3[j + 1] = new Point(x3, point4.Y);
                                                break;
                                            }
                                        }
                                    case DrawPolyLine.AutoAdjustType.CompressLeftRight:
                                        {
                                            if (this.compressLeft)
                                            {
                                                goto case DrawPolyLine.AutoAdjustType.ExpandTopBottom;
                                            }
                                            point.X = point.X - 1;
                                            goto case DrawPolyLine.AutoAdjustType.ExpandTopBottom;
                                        }
                                    case DrawPolyLine.AutoAdjustType.Expand:
                                        {
                                            point.X = point.X + 1;
                                            goto case DrawPolyLine.AutoAdjustType.ExpandTopBottom;
                                        }
                                    case DrawPolyLine.AutoAdjustType.ExpandLeftRight:
                                        {
                                            if (this.expandLeft)
                                            {
                                                goto case DrawPolyLine.AutoAdjustType.ExpandTopBottom;
                                            }
                                            point.X = point.X + 1;
                                            goto case DrawPolyLine.AutoAdjustType.ExpandTopBottom;
                                        }
                                    default:
                                        {
                                            goto case DrawPolyLine.AutoAdjustType.ExpandTopBottom;
                                        }
                                }
                            }
                        }
                        else if (num4 == 180)
                        {
                            switch (autoAdjustType)
                            {
                                case DrawPolyLine.AutoAdjustType.Compress:
                                    {
                                        point.Y = point.Y - 1;
                                        goto case DrawPolyLine.AutoAdjustType.CompressLeftRight;
                                    }
                                case DrawPolyLine.AutoAdjustType.CompressTopBottom:
                                    {
                                        if (this.compressTop)
                                        {
                                            goto case DrawPolyLine.AutoAdjustType.CompressLeftRight;
                                        }
                                        point.Y = point.Y - 1;
                                        goto case DrawPolyLine.AutoAdjustType.CompressLeftRight;
                                    }
                                case DrawPolyLine.AutoAdjustType.CompressLeftRight:
                                    {
                                        ArrayList arrayLists4 = this.pointArray;
                                        Point item5 = (Point)this.pointArray[j];
                                        arrayLists4[j] = new Point(item5.X, point.Y);
                                        if (j + 1 >= this.pointArray.Count)
                                        {
                                            ArrayList point5 = this.pointArray;
                                            Point item6 = (Point)this.pointArray[0];
                                            point5[0] = new Point(item6.X, point.Y);
                                            break;
                                        }
                                        else
                                        {
                                            ArrayList arrayLists5 = this.pointArray;
                                            Point point6 = (Point)this.pointArray[j + 1];
                                            arrayLists5[j + 1] = new Point(point6.X, point.Y);
                                            break;
                                        }
                                    }
                                case DrawPolyLine.AutoAdjustType.Expand:
                                    {
                                        point.Y = point.Y + 1;
                                        goto case DrawPolyLine.AutoAdjustType.CompressLeftRight;
                                    }
                                case DrawPolyLine.AutoAdjustType.ExpandTopBottom:
                                    {
                                        if (this.expandTop)
                                        {
                                            goto case DrawPolyLine.AutoAdjustType.CompressLeftRight;
                                        }
                                        point.Y = point.Y + 1;
                                        goto case DrawPolyLine.AutoAdjustType.CompressLeftRight;
                                    }
                                default:
                                    {
                                        goto case DrawPolyLine.AutoAdjustType.CompressLeftRight;
                                    }
                            }
                        }
                        else if (num4 == 0x10e)
                        {
                            switch (autoAdjustType)
                            {
                                case DrawPolyLine.AutoAdjustType.Compress:
                                    {
                                        point.X = point.X + 1;
                                        goto case DrawPolyLine.AutoAdjustType.ExpandTopBottom;
                                    }
                                case DrawPolyLine.AutoAdjustType.CompressTopBottom:
                                case DrawPolyLine.AutoAdjustType.ExpandTopBottom:
                                    {
                                        ArrayList arrayLists6 = this.pointArray;
                                        int x4 = point.X;
                                        Point item7 = (Point)this.pointArray[j];
                                        arrayLists6[j] = new Point(x4, item7.Y);
                                        if (j + 1 >= this.pointArray.Count)
                                        {
                                            ArrayList point7 = this.pointArray;
                                            int x5 = point.X;
                                            Point item8 = (Point)this.pointArray[0];
                                            point7[0] = new Point(x5, item8.Y);
                                            break;
                                        }
                                        else
                                        {
                                            ArrayList arrayLists7 = this.pointArray;
                                            int num5 = point.X;
                                            Point point8 = (Point)this.pointArray[j + 1];
                                            arrayLists7[j + 1] = new Point(num5, point8.Y);
                                            break;
                                        }
                                    }
                                case DrawPolyLine.AutoAdjustType.CompressLeftRight:
                                    {
                                        if (!this.compressLeft)
                                        {
                                            goto case DrawPolyLine.AutoAdjustType.ExpandTopBottom;
                                        }
                                        point.X = point.X + 1;
                                        goto case DrawPolyLine.AutoAdjustType.ExpandTopBottom;
                                    }
                                case DrawPolyLine.AutoAdjustType.Expand:
                                    {
                                        point.X = point.X - 1;
                                        goto case DrawPolyLine.AutoAdjustType.ExpandTopBottom;
                                    }
                                case DrawPolyLine.AutoAdjustType.ExpandLeftRight:
                                    {
                                        if (!this.expandLeft)
                                        {
                                            goto case DrawPolyLine.AutoAdjustType.ExpandTopBottom;
                                        }
                                        point.X = point.X - 1;
                                        goto case DrawPolyLine.AutoAdjustType.ExpandTopBottom;
                                    }
                                default:
                                    {
                                        goto case DrawPolyLine.AutoAdjustType.ExpandTopBottom;
                                    }
                            }
                        }
                    }
                }
            }
            if (autoAdjustType == DrawPolyLine.AutoAdjustType.CompressLeftRight)
            {
                this.compressLeft = !this.compressLeft;
            }
            if (autoAdjustType == DrawPolyLine.AutoAdjustType.CompressTopBottom)
            {
                this.compressTop = !this.compressTop;
            }
            if (autoAdjustType == DrawPolyLine.AutoAdjustType.ExpandLeftRight)
            {
                this.expandLeft = !this.expandLeft;
            }
            if (autoAdjustType == DrawPolyLine.AutoAdjustType.ExpandTopBottom)
            {
                this.expandTop = !this.expandTop;
            }
            if (!base.Filled && this.deductionArray.Count > 0)
            {
                for (int k = 0; k < this.pointArray.Count; k++)
                {
                    Point[] pointArray1 = new Point[] { item[k], item[k + 1] };
                    DrawLine opening = this.GetOpening(pointArray1[0], pointArray1[1]);
                    if (opening != null)
                    {
                        if (!(pointArray1[0] == opening.StartPoint) || !(pointArray1[1] == opening.EndPoint))
                        {
                            int x6 = ((Point)this.pointArray[k]).X;
                            Point item9 = (Point)this.pointArray[k];
                            opening.EndPoint = new Point(x6, item9.Y);
                            Point point9 = (Point)this.pointArray[k + 1];
                            int num6 = point9.X;
                            Point item10 = (Point)this.pointArray[k + 1];
                            opening.StartPoint = new Point(num6, item10.Y);
                        }
                        else
                        {
                            int x7 = ((Point)this.pointArray[k]).X;
                            Point point10 = (Point)this.pointArray[k];
                            opening.StartPoint = new Point(x7, point10.Y);
                            Point item11 = (Point)this.pointArray[k + 1];
                            int num7 = item11.X;
                            Point point11 = (Point)this.pointArray[k + 1];
                            opening.EndPoint = new Point(num7, point11.Y);
                        }
                    }
                }
            }
            Console.WriteLine("DrawPolyLine::AutoAdjust EXIT");
            return true;
        }

        public override DrawObject Clone()
        {
            DrawPolyLine drawPolyLine = new DrawPolyLine()
            {
                StartPoint = new Point(base.StartPoint.X, base.StartPoint.Y),
                EndPoint = new Point(base.EndPoint.X, base.EndPoint.Y),
                pointArray = new ArrayList()
            };
            for (int i = 0; i < this.pointArray.Count; i++)
            {
                Point item = (Point)this.pointArray[i];
                drawPolyLine.pointArray.Add(new Point(item.X, item.Y));
            }
            for (int j = 0; j < this.deductionArray.Count; j++)
            {
                if (!base.Filled)
                {
                    drawPolyLine.deductionArray.Add(((DrawLine)this.deductionArray[j]).Clone());
                }
                else
                {
                    drawPolyLine.deductionArray.Add(((DrawPolyLine)this.deductionArray[j]).Clone());
                }
            }
            for (int k = 0; k < this.dropArray.Count; k++)
            {
                drawPolyLine.dropArray.Add(((DrawLine)this.dropArray[k]).Clone());
            }
            foreach (KeyValuePair<string, CustomRenderingProperties> customRenderingArray in this.CustomRenderingArray)
            {
                drawPolyLine.customRenderingArray.Add(customRenderingArray.Key, customRenderingArray.Value.Clone());
            }
            drawPolyLine.Pattern = this.pattern;
            drawPolyLine.CloseFigure = this.closeFigure;
            drawPolyLine.FigureCompleted = true;
            drawPolyLine.DisplayName = new Utilities.DisplayName(drawPolyLine, "");
            drawPolyLine.SetSlopeFactor(this.SlopeFactor.InternalValue, this.SlopeFactor.SlopeType, this.SlopeFactor.SlopeApplyType, this.SlopeFactor.HipValley);
            base.FillDrawObjectFields(drawPolyLine);
            return drawPolyLine;
        }

        private double ComputeArea(bool applySlopFactor)
        {
            if (this.pointArray.Count < 3)
            {
                return 0;
            }
            Point[] item = new Point[] { (Point)this.pointArray[0], (Point)this.pointArray[this.pointArray.Count - 1] };
            double determinant = this.GetDeterminant((double)item[1].X, (double)item[1].Y, (double)item[0].X, (double)item[0].Y);
            for (int i = 1; i < this.pointArray.Count; i++)
            {
                item[0] = (Point)this.pointArray[i];
                item[1] = (Point)this.pointArray[i - 1];
                determinant += this.GetDeterminant((double)item[1].X, (double)item[1].Y, (double)item[0].X, (double)item[0].Y);
            }
            determinant /= 2;
            determinant = Math.Abs(determinant);
            if (applySlopFactor && this.SlopeFactor.InternalValue > 0)
            {
                determinant = this.SlopeFactor.Apply(determinant, 0, 0);
            }
            return determinant;
        }

        private Point ComputeCenter()
        {
            if (this.pointArray.Count < 2)
            {
                return new Point(0, 0);
            }
            if (this.pointArray.Count == 2)
            {
                DrawLine drawLine = new DrawLine()
                {
                    StartPoint = (Point)this.pointArray[0],
                    EndPoint = (Point)this.pointArray[1]
                };
                return drawLine.Center;
            }
            double num = this.ComputeArea(false);
            if (num > 0)
            {
                return this.FindCentroid(num);
            }
            return new Point(0, 0);
        }

        private void ComputeCenterTooltip()
        {
            string areaString;
            string lengthString;
            if (this.pointArray.Count <= 2)
            {
                base.TextArray.Insert(0, new DrawText("", Point.Empty, 0));
                return;
            }
            GroupStats stats = this.Stats;
            string[] surface = new string[] { Resources.Surface, " = ", null, null, null, null, null };
            string[] strArrays = surface;
            if (base.DisplayInPixels)
            {
                double areaMinusDeduction = stats.AreaMinusDeduction;
                areaString = string.Concat(areaMinusDeduction.ToString(), " pixels ²");
            }
            else
            {
                areaString = base.DrawArea.ToAreaString((int)stats.AreaMinusDeduction);
            }
            strArrays[2] = areaString;
            surface[3] = "\n";
            surface[4] = Resources.Périmètre;
            surface[5] = " = ";
            string[] strArrays1 = surface;
            if (base.DisplayInPixels)
            {
                double perimeter = stats.Perimeter;
                lengthString = string.Concat(perimeter.ToString(), " pixels");
            }
            else
            {
                lengthString = base.DrawArea.ToLengthString((int)stats.Perimeter, false);
            }
            strArrays1[6] = lengthString;
            string str = string.Concat(string.Concat(surface), (this.customRenderingTooltip != string.Empty ? string.Concat("\n", this.customRenderingTooltip) : string.Empty));
            DrawText drawText = new DrawText(str, this.Center, 0);
            base.TextArray.Insert(0, drawText);
        }

        public void ComputeCustomRendering(Plan plan)
        {
            this.UnitScale = plan.UnitScale;
            Preset selectedPreset = base.Group.SelectedPreset;
            Preset selectedRenderingPreset = base.Group.SelectedRenderingPreset;
            foreach (Preset collection in base.Group.Presets.Collection)
            {
                if (collection.CustomRendering == null)
                {
                    continue;
                }
                base.Group.SelectedPreset = collection;
                collection.CustomRendering.ComputeResults(null, this, this.GetCustomRenderingProperties(collection.ID), plan.UnitScale);
            }
            base.Group.SelectedPreset = selectedPreset;
            base.Group.SelectedRenderingPreset = selectedRenderingPreset;
            this.UnitScale = null;
        }

        private double ComputePerimeter()
        {
            if (this.pointArray.Count < 2)
            {
                return 0;
            }
            int num = 0;
            double num1 = 0;
            Point[] item = new Point[2];
            for (int i = 0; i < this.pointArray.Count - 1; i++)
            {
                item[0] = (Point)this.pointArray[i];
                item[1] = (Point)this.pointArray[i + 1];
                num1 += (double)base.Distance2D(item[0].X, item[0].Y, item[1].X, item[1].Y, !base.Filled, ref num);
            }
            if (this.CloseFigure)
            {
                item[0] = (Point)this.pointArray[this.pointArray.Count - 1];
                item[1] = (Point)this.pointArray[0];
                num1 += (double)base.Distance2D(item[0].X, item[0].Y, item[1].X, item[1].Y, !base.Filled, ref num);
            }
            return num1;
        }

        private int CornersCount()
        {
            int num = 0;
            int num1 = 0;
            while (true)
            {
                if (num1 >= this.pointArray.Count - (this.closeFigure ? 0 : 2))
                {
                    break;
                }
                if (!this.IsHandlePartOfOpening((Point)this.pointArray[num1]))
                {
                    num++;
                }
                num1++;
            }
            return num;
        }

        public int CreateDeduction(DrawObject deduction)
        {
            deduction.DeductionParentID = base.ID;
            deduction.SetSlopeFactor(this.SlopeFactor);
            return this.deductionArray.Add(deduction);
        }

        public DrawLine CreateDrop(Point p, double length)
        {
            if (this.GetNearestPointIndex(p) == -1)
            {
                Console.WriteLine("CreateDrop::GetNearestPointIndex:failed");
                return null;
            }
            DrawLine drawLine = new DrawLine()
            {
                StartPoint = new Point(p.X, p.Y),
                Height = length
            };
            this.dropArray.Add(drawLine);
            return drawLine;
        }

        public void CreateOpening(DrawLine drawLine, Segment segment, bool selectOpening, bool createFromSegment = false)
        {
            int nearestPointIndex = this.GetNearestPointIndex(drawLine.StartPoint);
            if (nearestPointIndex == -1)
            {
                Console.WriteLine("CreateOpening::GetNearestPointIndex:failed");
                return;
            }
            if (!createFromSegment)
            {
                Point point = new Point(drawLine.StartPoint.X, drawLine.StartPoint.Y);
                Point point1 = new Point(drawLine.EndPoint.X, drawLine.EndPoint.Y);
                bool flag = DrawLine.LineIsReverse(point, point1);
                if (DrawLine.LineIsReverse(segment.StartPoint, segment.EndPoint))
                {
                    this.InsertPoint((!flag ? point1 : point), nearestPointIndex + 1);
                    this.InsertPoint((!flag ? point : point1), nearestPointIndex + 2);
                }
                else
                {
                    this.InsertPoint((!flag ? point : point1), nearestPointIndex + 1);
                    this.InsertPoint((!flag ? point1 : point), nearestPointIndex + 2);
                }
            }
            this.CreateDeduction(drawLine);
            if (selectOpening)
            {
                base.DrawArea.Clipboard.SelectedOpening = new Clipboard.Opening(drawLine, base.DrawArea.UnitScale);
            }
        }

        public void DeleteDeduction(DrawObject deduction)
        {
            for (int i = this.deductionArray.Count - 1; i >= 0; i--)
            {
                if (((DrawObject)this.deductionArray[i]).ID == deduction.ID)
                {
                    ((DrawObject)this.deductionArray[i]).ID = -1;
                    this.deductionArray.RemoveAt(i);
                    return;
                }
            }
        }

        public void DeleteDrop(Point p)
        {
            DrawLine drawLine = this.FindDrop(p);
            if (drawLine == null)
            {
                Console.WriteLine("DeleteDrop::FindDrop:failed");
                return;
            }
            this.dropArray.Remove(drawLine);
        }

        public void DeleteOpening(Point startPoint, Point endPoint)
        {
            try
            {
                DrawLine opening = this.GetOpening(startPoint, endPoint);
                if (opening != null)
                {
                    opening.ID = -1;
                    this.deductionArray.Remove(opening);
                }
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                Console.WriteLine("DeleteOpening failed.");
                Console.WriteLine(exception.Message);
            }
        }

        public DrawPolyLine.WindingDirection DetermineWindingDirection()
        {
            Point[] item = new Point[this.pointArray.Count + 1];
            this.pointArray.CopyTo(item, 0);
            item[this.pointArray.Count] = (Point)this.pointArray[0];
            float single = (float)((int)((IEnumerable<Point>)item).Average<Point>((Point p) => p.X));
            float single1 = (float)((int)((IEnumerable<Point>)item).Average<Point>((Point p) => p.Y));
            PointF pointF = new PointF(single, single1);
            double num = 0;
            List<PointF> list = (
                from point in item
                select new PointF((float)point.X - pointF.X, (float)point.Y - pointF.Y)).ToList<PointF>();
            for (int i = 0; i < list.Count; i++)
            {
                int num1 = (i + 1 == list.Count ? 0 : i + 1);
                double x = (double)list[i].X;
                double x1 = (double)list[num1].X;
                double y = (double)list[i].Y;
                double y1 = (double)list[num1].Y;
                if (y * y1 < 0)
                {
                    if (x + y * (x1 - x) / (y - y1) > 0)
                    {
                        if (y >= 0)
                        {
                            num -= 1;
                        }
                        else
                        {
                            num += 1;
                        }
                    }
                }
                else if (y == 0 && x > 0)
                {
                    if (y1 <= 0)
                    {
                        num -= 0.5;
                    }
                    else
                    {
                        num += 0.5;
                    }
                }
                else if (y1 == 0 && x1 > 0)
                {
                    if (y >= 0)
                    {
                        num -= 0.5;
                    }
                    else
                    {
                        num += 0.5;
                    }
                }
            }
            if (num <= 0)
            {
                return DrawPolyLine.WindingDirection.CounterClockWise;
            }
            return DrawPolyLine.WindingDirection.Clockwise;
        }

        public override void Draw(Graphics g, int offsetX, int offsetY, bool printToScreen = true, MainForm.ImageQualityEnum imageQuality = MainForm.ImageQualityEnum.QualityHigh)
        {
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
            Pen pen = this.SelectPen();
            Brush brush = this.SelectBrush();
            string empty = string.Empty;
            base.ClearTextArray();
            GraphicsPath graphicsPath = this.OutlinePath(offsetX, offsetY);
            if (base.Rotation != 0)
            {
                RectangleF bounds = graphicsPath.GetBounds();
                Matrix matrix = new Matrix();
                matrix.RotateAt((float)base.Rotation, new PointF(bounds.Left + bounds.Width / 2f, bounds.Top + bounds.Height / 2f), MatrixOrder.Append);
                graphicsPath.Transform(matrix);
            }
            Region region = null;
            if (base.Filled)
            {
                region = this.FilledRegion(g, pen, graphicsPath, offsetX, offsetY);
                g.FillRegion(brush, region);
            }
            if (!this.FigureCompleted || this.FigureCompleted && !this.selectedSegment.IsValid() && this.deductionArray.Count == 0)
            {
                g.DrawPath(pen, graphicsPath);
            }
            else
            {
                this.DrawOpeningOrSegment(g, pen, offsetX, offsetY);
            }
            if (this.DropArray.Count > 0)
            {
                this.DrawDrops(g, offsetX, offsetY);
            }
            if (this.Equals(base.DrawArea.CurrentlyCreatedObject) || this.Equals(base.DrawArea.CurrentlyResizedObject))
            {
                this.DrawAngleMarkers(g, offsetX, offsetY);
            }
            if (region != null)
            {
                if (this.UnitScale.Scale > 0f && !base.IsDeduction())
                {
                    this.DrawCustomRendering(g, region, offsetX, offsetY);
                }
                region.Dispose();
            }
            if (base.Filled)
            {
                this.ComputeCenterTooltip();
            }
            graphicsPath.Dispose();
            if (pen != null)
            {
                pen.Dispose();
            }
            brush.Dispose();
            base.Invalidate();
            g.TextRenderingHint = textRenderingHint1;
            g.SmoothingMode = smoothingMode;
        }

        private void DrawAngleMarkers(Graphics g, int offsetX, int offsetY)
        {
            if (this.pointArray.Count > 2)
            {
                for (int i = 0; i < this.pointArray.Count; i++)
                {
                    Point[] item = new Point[3];
                    if (i == 0)
                    {
                        item[0] = (Point)this.pointArray[this.pointArray.Count - 1];
                        item[1] = (Point)this.pointArray[0];
                        item[2] = (Point)this.pointArray[1];
                    }
                    else if (i != this.pointArray.Count - 1)
                    {
                        item[0] = (Point)this.pointArray[i - 1];
                        item[1] = (Point)this.pointArray[i];
                        item[2] = (Point)this.pointArray[i + 1];
                    }
                    else
                    {
                        item[0] = (Point)this.pointArray[this.pointArray.Count - 2];
                        item[1] = (Point)this.pointArray[this.pointArray.Count - 1];
                        item[2] = (Point)this.pointArray[0];
                    }
                    double num = this.Angle(item[0], item[1], item[2]);
                    if (num >= 89.5 && num <= 90.5)
                    {
                        int num1 = 50;
                        int num2 = 4;
                        DrawLine drawLine = new DrawLine();
                        item[0] = new Point(item[0].X - offsetX, item[0].Y - offsetY);
                        item[1] = new Point(item[1].X - offsetX, item[1].Y - offsetY);
                        item[2] = new Point(item[2].X - offsetX, item[2].Y - offsetY);
                        if (drawLine.Distance2D(item[0], item[1]) > num1 + 10 && drawLine.Distance2D(item[2], item[1]) > num1 + 10)
                        {
                            Point[] point = new Point[3];
                            double[] numArray = new double[] { DrawLine.Angle((double)item[1].X, (double)item[1].Y, (double)item[0].X, (double)item[0].Y), DrawLine.Angle((double)item[1].X, (double)item[1].Y, (double)item[2].X, (double)item[2].Y) };
                            point[1] = new Point(item[1].X, item[1].Y);
                            point[0] = DrawLine.GetPointAtAngle(point[1], numArray[0], (double)((float)num1 * 0.8f));
                            point[2] = DrawLine.GetPointAtAngle(point[1], numArray[1], (double)((float)num1 * 0.8f));
                            item[0] = DrawLine.GetPointAtAngle(item[1], numArray[0], (double)num1);
                            item[2] = DrawLine.GetPointAtAngle(item[1], numArray[1], (double)num1);
                            Pen pen = new Pen(Color.FromArgb(0xff, (base.Color == Color.Red ? Color.DarkRed : Color.Red)), (float)num2)
                            {
                                DashStyle = DashStyle.Solid,
                                EndCap = LineCap.Square,
                                Alignment = PenAlignment.Center
                            };
                            g.DrawLine(pen, item[0], item[1]);
                            g.DrawLine(pen, item[2], item[1]);
                            g.FillPolygon(new SolidBrush(Color.FromArgb(100, pen.Color)), point);
                            pen.Dispose();
                        }
                    }
                }
            }
        }

        private void DrawCustomRendering(Graphics g, Region region, int offsetX, int offsetY)
        {
            DrawObjectGroup group = base.Group;
            if (group != null && group.Presets.Count >= 0)
            {
                CustomRendering customRendering = this.SelectCurrentRendering();
                if (customRendering != null)
                {
                    Region clip = g.Clip;
                    g.Clip = region;
                    customRendering.Draw(g, region, offsetX, offsetY, this, this.GetCustomRenderingProperties(customRendering.Preset.ID), this.UnitScale);
                    g.Clip = clip;
                }
            }
        }

        private void DrawDrops(Graphics g, int offsetX, int offsetY)
        {
            PointF pointF;
            foreach (DrawLine drawLine in this.dropArray)
            {
                Pen pen = new Pen(Color.White, 1f);
                Point startPoint = drawLine.StartPoint;
                Point point = drawLine.StartPoint;
                g.DrawEllipse(pen, startPoint.X - offsetX - 10, point.Y - offsetY - 10, 20, 20);
                SolidBrush solidBrush = new SolidBrush(Color.FromArgb(base.Opacity + 30, 33, 33, 33));
                Point startPoint1 = drawLine.StartPoint;
                Point point1 = drawLine.StartPoint;
                g.FillEllipse(solidBrush, startPoint1.X - offsetX - 10, point1.Y - offsetY - 10, 20, 20);
                Point startPoint2 = drawLine.StartPoint;
                Point point2 = drawLine.StartPoint;
                Point startPoint3 = drawLine.StartPoint;
                Point point3 = drawLine.StartPoint;
                g.DrawLine(pen, startPoint2.X - offsetX, point2.Y - offsetY - 4, startPoint3.X - offsetX, point3.Y - offsetY + 4);
                Point startPoint4 = drawLine.StartPoint;
                Point point4 = drawLine.StartPoint;
                Point startPoint5 = drawLine.StartPoint;
                Point point5 = drawLine.StartPoint;
                g.DrawLine(pen, startPoint4.X - offsetX - 4, point4.Y - offsetY - 4, startPoint5.X - offsetX + 4, point5.Y - offsetY - 4);
                Point startPoint6 = drawLine.StartPoint;
                Point point6 = drawLine.StartPoint;
                Point startPoint7 = drawLine.StartPoint;
                Point point7 = drawLine.StartPoint;
                g.DrawLine(pen, startPoint6.X - offsetX - 4, point6.Y - offsetY + 4, startPoint7.X - offsetX + 4, point7.Y - offsetY + 4);
                pen.Dispose();
                Point empty = Point.Empty;
                Point empty1 = Point.Empty;
                double num = double.PositiveInfinity;
                int num1 = 0;
                while (true)
                {
                    if (num1 >= this.pointArray.Count - (this.closeFigure ? 0 : 1))
                    {
                        break;
                    }
                    Point[] item = new Point[2];
                    if (!this.closeFigure || num1 + 1 != this.pointArray.Count)
                    {
                        item[0] = (Point)this.pointArray[num1];
                        item[1] = (Point)this.pointArray[num1 + 1];
                    }
                    else
                    {
                        item[0] = (Point)this.pointArray[this.pointArray.Count - 1];
                        item[1] = (Point)this.pointArray[0];
                    }
                    double segment = DrawLine.FindDistanceToSegment(drawLine.StartPoint, item[0], item[1], out pointF);
                    if (segment < num)
                    {
                        num = segment;
                        empty = new Point(item[0].X, item[0].Y);
                        empty1 = new Point(item[1].X, item[1].Y);
                    }
                    num1++;
                }
                if (!(empty != Point.Empty) || !(empty1 != Point.Empty))
                {
                    continue;
                }
                string lengthStringFromUnitSystem = base.DrawArea.ToLengthStringFromUnitSystem(drawLine.Height, true);
                DrawText drawText = new DrawText(lengthStringFromUnitSystem, drawLine.StartPoint, base.TextAngle(empty, empty1), new Segment(empty, empty1));
                base.TextArray.Add(drawText);
            }
        }

        private void DrawOpeningOrSegment(Graphics g, Pen pen, int offsetX, int offsetY)
        {
            int num = 0;
            while (true)
            {
                if (num >= this.pointArray.Count - (this.closeFigure ? 0 : 1))
                {
                    break;
                }
                Point[] item = new Point[2];
                if (!this.closeFigure || num + 1 != this.pointArray.Count)
                {
                    item[0] = (Point)this.pointArray[num];
                    item[1] = (Point)this.pointArray[num + 1];
                }
                else
                {
                    item[0] = (Point)this.pointArray[this.pointArray.Count - 1];
                    item[1] = (Point)this.pointArray[0];
                }
                bool flag = this.IsOpening(item[0], item[1]);
                if (this.selectedSegment.IsEqualTo(item[0], item[1]))
                {
                    Pen pen1 = new Pen(Color.FromArgb(base.Opacity, Color.Black), (float)base.PenWidth)
                    {
                        DashStyle = (flag ? DashStyle.Dot : DashStyle.DashDot)
                    };
                    g.DrawLine(pen1, new Point(item[0].X - offsetX, item[0].Y - offsetY), new Point(item[1].X - offsetX, item[1].Y - offsetY));
                    pen1.Dispose();
                }
                else if (!flag)
                {
                    g.DrawLine(pen, new Point(item[0].X - offsetX, item[0].Y - offsetY), new Point(item[1].X - offsetX, item[1].Y - offsetY));
                }
                else
                {
                    Pen pen2 = new Pen(Color.FromArgb((base.Opacity > 50 ? 50 : base.Opacity), base.Color), (float)base.PenWidth)
                    {
                        DashStyle = DashStyle.Dot
                    };
                    g.DrawLine(pen2, new Point(item[0].X - offsetX, item[0].Y - offsetY), new Point(item[1].X - offsetX, item[1].Y - offsetY));
                    pen2.Dispose();
                }
                num++;
            }
        }

        public override void DrawText(Graphics g, int offsetX, int offsetY, bool printToScreen = true, MainForm.ImageQualityEnum imageQuality = MainForm.ImageQualityEnum.QualityHigh, float defaultFontSize = 12f)
        {
            int num;
            bool flag;
            if (!base.ShowMeasure || base.TextArray.Count == 0 || this.EditDeductions)
            {
                return;
            }
            double num1 = this.ComputeArea(false);
            if (base.DrawingBoard.ZoomFactor >= 0.15)
            {
                num = (this.Selected || !base.Filled ? base.TextArray.Count : 1);
            }
            else if (base.Filled)
            {
                num = (base.DrawingBoard.ZoomFactor >= 0.12 ? 1 : 0);
            }
            else
            {
                num = 0;
            }
            int num2 = num;
            for (int i = 0; i < num2; i++)
            {
                DrawText item = (DrawText)base.TextArray[i];
                if (i == 0 && (num1 * base.DrawingBoard.ZoomFactor >= 30000 || this.figureCompleted) || i > 0 && (num1 * base.DrawingBoard.ZoomFactor >= 30000 || !this.figureCompleted || !base.Filled) || !this.closeFigure)
                {
                    if (i != 0 || !base.Filled)
                    {
                        flag = (this.selectedSegment.IsValid() ? false : !base.Filled);
                    }
                    else
                    {
                        flag = true;
                    }
                    bool flag1 = flag;
                    if (!flag1)
                    {
                        Segment tag = (Segment)item.Tag;
                        flag1 = !this.selectedSegment.IsEqualTo(tag.StartPoint, tag.EndPoint);
                    }
                    if (flag1)
                    {
                        item.Draw(g, offsetX, offsetY, (float)base.DrawingBoard.ZoomFactor, (!base.IsDeduction() ? base.Opacity : 225), printToScreen, imageQuality, defaultFontSize);
                    }
                }
            }
            base.TextDirty = false;
        }

        private Region FilledRegion(Graphics g, Pen pen, GraphicsPath outlinePath, int offsetX, int offsetY)
        {
            Region region = new Region(outlinePath);
            if (this.deductionArray.Count > 0)
            {
                foreach (DrawPolyLine drawPolyLine in this.deductionArray)
                {
                    if (drawPolyLine.PointArray.Count <= 0)
                    {
                        continue;
                    }
                    Point[] item = new Point[drawPolyLine.PointArray.Count];
                    for (int i = 0; i < drawPolyLine.PointArray.Count; i++)
                    {
                        item[i] = (Point)drawPolyLine.PointArray[i];
                        ref Point x = ref item[i];
                        x.X = x.X - offsetX;
                        ref Point y = ref item[i];
                        y.Y = y.Y - offsetY;
                    }
                    GraphicsPath graphicsPath = new GraphicsPath();
                    graphicsPath.AddLines(item);
                    graphicsPath.CloseFigure();
                    region.Exclude(graphicsPath);
                    if (g != null && pen != null)
                    {
                        g.DrawPath(pen, graphicsPath);
                    }
                    graphicsPath.Dispose();
                }
            }
            return region;
        }

        public Point FindCentroid(double area)
        {
            int count = this.pointArray.Count;
            Point[] item = new Point[count + 1];
            this.pointArray.CopyTo(item, 0);
            item[count] = (Point)this.pointArray[0];
            double x = 0;
            double y = 0;
            for (int i = 0; i < count; i++)
            {
                Point[] pointArray = new Point[] { item[i], item[i + 1] };
                double num = (double)(pointArray[0].X * pointArray[1].Y - pointArray[1].X * pointArray[0].Y);
                x = x + (double)(pointArray[0].X + pointArray[1].X) * num;
                y = y + (double)(pointArray[0].Y + pointArray[1].Y) * num;
            }
            double num1 = area;
            x = x / (6 * num1);
            y = y / (6 * num1);
            if (x < 0)
            {
                x = -x;
                y = -y;
            }
            return new Point((int)x, (int)y);
        }

        public DrawLine FindDrop(Point p)
        {
            DrawLine item = null;
            int count = this.dropArray.Count - 1;
            while (count >= 0)
            {
                if (!((DrawLine)this.dropArray[count]).StartPoint.Equals(p))
                {
                    count--;
                }
                else
                {
                    item = (DrawLine)this.dropArray[count];
                    break;
                }
            }
            return item;
        }

        public CustomRenderingProperties GetCustomRenderingProperties(string extensionID)
        {
            if (this.customRenderingArray.ContainsKey(extensionID))
            {
                return this.customRenderingArray[extensionID];
            }
            CustomRenderingProperties customRenderingProperty = new CustomRenderingProperties();
            this.SetCustomRenderingProperties(extensionID, customRenderingProperty);
            return customRenderingProperty;
        }

        private double GetDeterminant(double x1, double y1, double x2, double y2)
        {
            return x1 * y2 - x2 * y1;
        }

        public double GetDropLength(Point dropPoint)
        {
            double height = 0;
            try
            {
                DrawLine drawLine = this.FindDrop(dropPoint);
                if (drawLine != null)
                {
                    height = drawLine.Height;
                }
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                Console.WriteLine("GetDropLength failed.");
                Console.WriteLine(exception.Message);
            }
            return height;
        }

        public override Point GetHandle(int handleNumber)
        {
            if (handleNumber < 1)
            {
                handleNumber = 1;
            }
            if (handleNumber > this.pointArray.Count)
            {
                handleNumber = this.pointArray.Count;
            }
            return (Point)this.pointArray[handleNumber - 1];
        }

        public override Cursor GetHandleCursor(int handleNumber)
        {
            return Cursors.SizeAll;
        }

        public override int GetHandleSize()
        {
            return 4;
        }

        public int GetNearestPointIndex(Point point)
        {
            PointF pointF;
            int count = -1;
            try
            {
                count = 0;
                double segment = DrawLine.FindDistanceToSegment(point, (Point)this.PointArray[0], (Point)this.PointArray[1], out pointF);
                for (int i = 0; i < this.PointArray.Count - 1; i++)
                {
                    double num = DrawLine.FindDistanceToSegment(point, (Point)this.PointArray[i], (Point)this.PointArray[i + 1], out pointF);
                    if (num <= segment)
                    {
                        segment = num;
                        count = i;
                    }
                }
                if (this.CloseFigure)
                {
                    double segment1 = DrawLine.FindDistanceToSegment(point, (Point)this.PointArray[this.PointArray.Count - 1], (Point)this.PointArray[0], out pointF);
                    if (segment1 <= segment)
                    {
                        segment = segment1;
                        count = this.PointArray.Count - 1;
                    }
                }
            }
            catch
            {
                count = -1;
            }
            return count;
        }

        public int GetNearestPointIndexOnSegment(Point point)
        {
            return this.GetNearestPointIndex(this.LocatePointOnSegment(point));
        }

        public DrawLine GetOpening(Point startPoint, Point endPoint)
        {
            DrawLine drawLine;
            try
            {
                foreach (DrawLine drawLine1 in this.deductionArray)
                {
                    if ((!(startPoint == drawLine1.StartPoint) || !(endPoint == drawLine1.EndPoint)) && (!(startPoint == drawLine1.EndPoint) || !(endPoint == drawLine1.StartPoint)))
                    {
                        continue;
                    }
                    drawLine = drawLine1;
                    return drawLine;
                }
                drawLine = null;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                Console.WriteLine("GetOpening failed.");
                Console.WriteLine(exception.Message);
                drawLine = null;
            }
            return drawLine;
        }

        public DrawLine GetOpeningFromHandle(Point handlePoint)
        {
            DrawLine drawLine;
            IEnumerator enumerator = this.deductionArray.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    DrawLine current = (DrawLine)enumerator.Current;
                    if (!(handlePoint == current.StartPoint) && !(handlePoint == current.EndPoint))
                    {
                        continue;
                    }
                    drawLine = current;
                    return drawLine;
                }
                return null;
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            return drawLine;
        }

        public double GetOpeningHeight(Point startPoint, Point endPoint)
        {
            double height = 0;
            try
            {
                DrawLine opening = this.GetOpening(startPoint, endPoint);
                if (opening != null)
                {
                    height = opening.Height;
                }
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                Console.WriteLine("GetOpeningHeight failed.");
                Console.WriteLine(exception.Message);
            }
            return height;
        }

        private int GetPointIndex(Point point)
        {
            for (int i = 0; i < this.pointArray.Count; i++)
            {
                if (((Point)this.pointArray[i]).X == point.X && ((Point)this.pointArray[i]).Y == point.Y)
                {
                    return i;
                }
            }
            Console.WriteLine(string.Concat("GetPointIndex::Unable to find point index:", point.ToString()));
            return -1;
        }

        public void InsertPoint(Point point, int index)
        {
            this.pointArray.Insert(index, point);
        }

        public override bool IntersectsWith(Rectangle rectangle, int offsetX, int offsetY)
        {
            if (this.IntersectWithLine(rectangle, offsetX, offsetY))
            {
                return true;
            }
            if (!base.Filled)
            {
                return false;
            }
            return this.PointInPolygon(new Point(rectangle.X + offsetX, rectangle.Y + offsetY));
        }

        private bool IntersectWithLine(Rectangle rectangle, int offsetX, int offsetY)
        {
            for (int i = 0; i < this.PointArray.Count - 1; i++)
            {
                if (DrawLine.LineIntersectsRectangle(new Rectangle(rectangle.X + offsetX, rectangle.Y + offsetY, rectangle.Width, rectangle.Height), (Point)this.PointArray[i], (Point)this.PointArray[i + 1]))
                {
                    return true;
                }
            }
            if (this.CloseFigure && DrawLine.LineIntersectsRectangle(new Rectangle(rectangle.X + offsetX, rectangle.Y + offsetY, rectangle.Width, rectangle.Height), (Point)this.PointArray[this.PointArray.Count - 1], (Point)this.PointArray[0]))
            {
                return true;
            }
            return false;
        }

        public bool IsHandlePartOfOpening(Point handlePoint)
        {
            bool flag;
            try
            {
                foreach (DrawLine drawLine in this.deductionArray)
                {
                    if (!(handlePoint == drawLine.StartPoint) && !(handlePoint == drawLine.EndPoint))
                    {
                        continue;
                    }
                    flag = true;
                    return flag;
                }
                flag = false;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                Console.WriteLine("HandleIsPartOfOpening failed.");
                Console.WriteLine(exception.Message);
                flag = false;
            }
            return flag;
        }

        public bool IsOpening(Point startPoint, Point endPoint)
        {
            if (base.Filled)
            {
                return false;
            }
            return this.GetOpening(startPoint, endPoint) != null;
        }

        public Point IsPartOfDrop(Point point, int offsetX, int offsetY)
        {
            Point startPoint;
            IEnumerator enumerator = this.dropArray.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    DrawLine current = (DrawLine)enumerator.Current;
                    Point startPoint1 = current.StartPoint;
                    Point point1 = current.StartPoint;
                    if (!DrawRectangle.GetNormalizedRectangle(new Rectangle(startPoint1.X - 10, point1.Y - 10, 20, 20)).Contains(point.X + offsetX, point.Y + offsetY))
                    {
                        continue;
                    }
                    startPoint = current.StartPoint;
                    return startPoint;
                }
                return Point.Empty;
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            return startPoint;
        }

        public Point LocatePointOnSegment(Point point)
        {
            return base.LocatePointOnLine(point, this.SelectedSegment.StartPoint, this.SelectedSegment.EndPoint);
        }

        private Point MatchPointToColor(
           Bitmap image,
           Point point,
           int angle,
           Color colorToMatch,
           bool directionOutward,
           int maxOffset)
        {
            Point current = new Point(point.X, point.Y);
            int steps = 0;

            while (true)
            {
                if (current.X < 0 || current.Y < 0 || current.X >= image.Width || current.Y >= image.Height)
                {
                    if (current.X < 0)
                        current.X = 0;

                    if (current.Y < 0)
                        current.Y = 0;

                    break;
                }

                Color pixel = image.GetPixel(current.X, current.Y);

                if (colorToMatch.R == 0 && colorToMatch.G == 0 && colorToMatch.B == 0)
                {
                    if (pixel.R != 0xFF && pixel.G != 0xFF && pixel.B != 0xFF)
                        break;
                }
                else
                {
                    if (pixel.R == colorToMatch.R && pixel.G == colorToMatch.G && pixel.B == colorToMatch.B)
                        break;
                }

                bool invalidAngle = false;

                if (angle <= 90)
                {
                    if (angle == 0)
                    {
                        current.Y += directionOutward ? -1 : +1;
                    }
                    else if (angle == 90)
                    {
                        current.X += directionOutward ? +1 : -1;
                    }
                    else
                    {
                        invalidAngle = true;
                    }
                }
                else
                {
                    if (angle == 180)
                    {
                        current.Y += directionOutward ? +1 : -1;
                    }
                    else if (angle == 0x10E)
                    {
                        current.X += directionOutward ? -1 : +1;
                    }
                    else
                    {
                        invalidAngle = true;
                    }
                }

                steps++;

                if (invalidAngle)
                    break;

                if (steps >= maxOffset)
                    break;
            }

            return current;
        }


        public bool MatchSegment(Segment segment, int tolerance)
        {
            Point[] item = new Point[2];
            for (int i = 0; i < this.pointArray.Count - 1; i++)
            {
                item[0] = (Point)this.pointArray[i];
                item[1] = (Point)this.pointArray[i + 1];
                if (Math.Abs(segment.StartPoint.X - item[0].X) <= tolerance && Math.Abs(segment.StartPoint.Y - item[0].Y) <= tolerance && Math.Abs(segment.EndPoint.X - item[1].X) <= tolerance && Math.Abs(segment.EndPoint.Y - item[1].Y) <= tolerance || Math.Abs(segment.StartPoint.X - item[1].X) <= tolerance && Math.Abs(segment.StartPoint.Y - item[1].Y) <= tolerance && Math.Abs(segment.EndPoint.X - item[0].X) <= tolerance && Math.Abs(segment.EndPoint.Y - item[0].Y) <= tolerance)
                {
                    return true;
                }
            }
            if (this.CloseFigure)
            {
                item[0] = (Point)this.pointArray[this.pointArray.Count - 1];
                item[1] = (Point)this.pointArray[0];
                if (Math.Abs(segment.StartPoint.X - item[0].X) <= tolerance && Math.Abs(segment.StartPoint.Y - item[0].Y) <= tolerance && Math.Abs(segment.EndPoint.X - item[1].X) <= tolerance && Math.Abs(segment.EndPoint.Y - item[1].Y) <= tolerance || Math.Abs(segment.StartPoint.X - item[1].X) <= tolerance && Math.Abs(segment.StartPoint.Y - item[1].Y) <= tolerance && Math.Abs(segment.EndPoint.X - item[0].X) <= tolerance && Math.Abs(segment.EndPoint.Y - item[0].Y) <= tolerance)
                {
                    return true;
                }
            }
            return false;
        }

        private int MatchSegmentToColor(Bitmap image, ArrayList inputArray, int angle, int normalizedAngle, Color colorToMatch, bool directionOutward, int maxOffset, ref Point pointA, ref Point pointB, ref int resultOffset)
        {
            int y;
            int x;
            int num;
            int num1 = 0;
            ArrayList arrayLists = new ArrayList();
            foreach (Point point in inputArray)
            {
                Point color = this.MatchPointToColor(image, point, normalizedAngle, colorToMatch, directionOutward, maxOffset);
                if (color.X <= 0 && color.Y <= 0)
                {
                    continue;
                }
                if (normalizedAngle == 0 || normalizedAngle == 180)
                {
                    y = color.Y;
                    x = color.X;
                    num = Math.Abs(point.Y - color.Y);
                }
                else
                {
                    y = color.X;
                    x = color.Y;
                    num = Math.Abs(point.X - color.X);
                }
                num1 += num;
                bool flag = false;
                foreach (DrawPolyLine.ValueCount arrayList in arrayLists)
                {
                    if (arrayList.@value != y)
                    {
                        continue;
                    }
                    arrayList.count++;
                    flag = true;
                    break;
                }
                if (flag)
                {
                    continue;
                }
                arrayLists.Add(new DrawPolyLine.ValueCount(y, x, num, 1));
            }
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            foreach (DrawPolyLine.ValueCount valueCount in arrayLists)
            {
                if (valueCount.count <= num4)
                {
                    continue;
                }
                num2 = valueCount.@value;
                num3 = valueCount.offset;
                num4 = valueCount.count;
            }
            resultOffset = num3;
            Console.WriteLine(string.Concat("Offset moyen = ", resultOffset));
            return num2;
        }

        public override void Move(int deltaX, int deltaY)
        {
            int count = this.pointArray.Count;
            for (int i = 0; i < count; i++)
            {
                Point item = (Point)this.pointArray[i];
                Point point = (Point)this.pointArray[i];
                Point point1 = new Point(item.X + deltaX, point.Y + deltaY);
                this.pointArray[i] = point1;
            }
            foreach (DrawObject drawObject in this.deductionArray)
            {
                if (!base.Filled)
                {
                    DrawLine drawLine = (DrawLine)drawObject;
                    Point startPoint = drawLine.StartPoint;
                    Point startPoint1 = drawLine.StartPoint;
                    drawLine.StartPoint = new Point(startPoint.X + deltaX, startPoint1.Y + deltaY);
                    Point endPoint = drawLine.EndPoint;
                    Point endPoint1 = drawLine.EndPoint;
                    drawLine.EndPoint = new Point(endPoint.X + deltaX, endPoint1.Y + deltaY);
                }
                else
                {
                    DrawPolyLine drawPolyLine = (DrawPolyLine)drawObject;
                    for (int j = 0; j < drawPolyLine.PointArray.Count; j++)
                    {
                        Point item1 = (Point)drawPolyLine.PointArray[j];
                        Point item2 = (Point)drawPolyLine.PointArray[j];
                        Point point2 = new Point(item1.X + deltaX, item2.Y + deltaY);
                        drawPolyLine.PointArray[j] = point2;
                    }
                }
            }
            foreach (DrawLine drawLine1 in this.dropArray)
            {
                Point startPoint2 = drawLine1.StartPoint;
                Point startPoint3 = drawLine1.StartPoint;
                drawLine1.StartPoint = new Point(startPoint2.X + deltaX, startPoint3.Y + deltaY);
            }
            base.Dirty = true;
            base.Invalidate();
        }

        public override void MoveHandleTo(Point point, int handleNumber)
        {
            if (handleNumber < 1)
            {
                handleNumber = 1;
            }
            if (handleNumber > this.pointArray.Count)
            {
                handleNumber = this.pointArray.Count;
            }
            Point item = (Point)this.pointArray[handleNumber - 1];
            Point x = new Point(point.X, point.Y);
            int num = x.X;
            PointF origin = base.DrawingBoard.Origin;
            x.X = num + (int)origin.X;
            int y = x.Y;
            PointF pointF = base.DrawingBoard.Origin;
            x.Y = y + (int)pointF.Y;
            this.pointArray[handleNumber - 1] = x;
            if (!base.Filled && this.deductionArray.Count > 0)
            {
                foreach (DrawLine drawLine in this.deductionArray)
                {
                    if (item != drawLine.StartPoint)
                    {
                        if (item != drawLine.EndPoint)
                        {
                            continue;
                        }
                        drawLine.EndPoint = new Point(x.X, x.Y);
                    }
                    else
                    {
                        drawLine.StartPoint = new Point(x.X, x.Y);
                    }
                }
            }
            this.ResetDrops();
            base.Dirty = true;
            base.Invalidate();
        }

        public override void Normalize()
        {
        }

        private GraphicsPath OutlinePath(int offsetX, int offsetY)
        {
            string str;
            Point[] pointArray = new Point[(this.pointArray.Count <= 1 || !this.FigureCompleted || !this.CloseFigure ? this.pointArray.Count : this.pointArray.Count + 1)];
            for (int i = 0; i < this.pointArray.Count; i++)
            {
                Point item = (Point)this.pointArray[i];
                pointArray[i] = item;
                ref Point x = ref pointArray[i];
                x.X = x.X - offsetX;
                ref Point y = ref pointArray[i];
                y.Y = y.Y - offsetY;
                if (!this.ComputingCustomRendering && i + 1 < this.pointArray.Count)
                {
                    int num = ((Point)this.pointArray[i]).X;
                    int y1 = ((Point)this.pointArray[i]).Y;
                    Point point = (Point)this.pointArray[i + 1];
                    int x1 = point.X;
                    Point item1 = (Point)this.pointArray[i + 1];
                    DrawLine drawLine = new DrawLine(num, y1, x1, item1.Y, new PointF(0f, 0f), "", 0, "");
                    drawLine.SetSlopeFactor(this.SlopeFactor);
                    int num1 = 0;
                    int x2 = drawLine.StartPoint.X;
                    int y2 = drawLine.StartPoint.Y;
                    int num2 = drawLine.EndPoint.X;
                    Point endPoint = drawLine.EndPoint;
                    int num3 = drawLine.Distance2D(x2, y2, num2, endPoint.Y, !base.Filled, ref num1);
                    if ((double)num1 * base.DrawingBoard.ZoomFactor >= (double)(this.toolTipMinThreshold + (base.DrawArea.UnitScaleIsImperial() ? 10 : 0)))
                    {
                        str = (base.DisplayInPixels ? string.Concat(num3.ToString(), " pixels") : base.DrawArea.ToLengthString(num3, (double)num1 * base.DrawingBoard.ZoomFactor <= (double)this.toolTipMaxThreshold));
                        if (this.IsOpening((Point)this.pointArray[i], (Point)this.pointArray[i + 1]))
                        {
                            double openingHeight = this.GetOpeningHeight((Point)this.pointArray[i], (Point)this.pointArray[i + 1]);
                            if (openingHeight > 0)
                            {
                                string str1 = str;
                                string[] hauteurCourt = new string[] { str1, "\n", Resources.Hauteur_court, " ", base.DrawArea.ToLengthStringFromUnitSystem(openingHeight, (double)num1 * base.DrawingBoard.ZoomFactor <= (double)this.toolTipMaxThreshold) };
                                str = string.Concat(hauteurCourt);
                            }
                        }
                        DrawText drawText = new DrawText(str, drawLine.Center, base.TextAngle((Point)this.pointArray[i], (Point)this.pointArray[i + 1]), new Segment((Point)this.pointArray[i], (Point)this.pointArray[i + 1]));
                        base.TextArray.Add(drawText);
                    }
                }
            }
            if (this.pointArray.Count > 1 && this.CloseFigure)
            {
                Point point1 = (Point)this.pointArray[0];
                if (!this.ComputingCustomRendering && this.pointArray.Count > 2)
                {
                    Point item2 = (Point)this.pointArray[this.pointArray.Count - 1];
                    int x3 = item2.X;
                    Point point2 = (Point)this.pointArray[this.pointArray.Count - 1];
                    int y3 = point2.Y;
                    int x4 = ((Point)this.pointArray[0]).X;
                    Point item3 = (Point)this.pointArray[0];
                    DrawLine drawLine1 = new DrawLine(x3, y3, x4, item3.Y, new PointF(0f, 0f), "", 0, "");
                    drawLine1.SetSlopeFactor(this.SlopeFactor);
                    int num4 = 0;
                    int x5 = drawLine1.StartPoint.X;
                    int y4 = drawLine1.StartPoint.Y;
                    int num5 = drawLine1.EndPoint.X;
                    Point endPoint1 = drawLine1.EndPoint;
                    int num6 = drawLine1.Distance2D(x5, y4, num5, endPoint1.Y, !base.Filled, ref num4);
                    if ((double)num4 * base.DrawingBoard.ZoomFactor >= (double)(this.toolTipMinThreshold + (base.DrawArea.UnitScaleIsImperial() ? 10 : 0)))
                    {
                        str = (base.DisplayInPixels ? string.Concat(num6.ToString(), " pixels") : base.DrawArea.ToLengthString(num6, (double)num4 * base.DrawingBoard.ZoomFactor <= (double)this.toolTipMaxThreshold));
                        if (this.IsOpening((Point)this.pointArray[this.pointArray.Count - 1], (Point)this.pointArray[0]))
                        {
                            double openingHeight1 = this.GetOpeningHeight((Point)this.pointArray[this.pointArray.Count - 1], (Point)this.pointArray[0]);
                            if (openingHeight1 > 0)
                            {
                                string str2 = str;
                                string[] strArrays = new string[] { str2, "\n", Resources.Hauteur_court, " ", base.DrawArea.ToLengthStringFromUnitSystem(openingHeight1, (double)num4 * base.DrawingBoard.ZoomFactor <= (double)this.toolTipMaxThreshold) };
                                str = string.Concat(strArrays);
                            }
                        }
                        DrawText drawText1 = new DrawText(str, drawLine1.Center, base.TextAngle((Point)this.pointArray[this.pointArray.Count - 1], (Point)this.pointArray[0]), new Segment((Point)this.pointArray[this.pointArray.Count - 1], (Point)this.pointArray[0]));
                        base.TextArray.Add(drawText1);
                    }
                }
                if (this.FigureCompleted)
                {
                    pointArray[this.pointArray.Count] = point1;
                    ref Point pointPointer = ref pointArray[this.pointArray.Count];
                    pointPointer.X = pointPointer.X - offsetX;
                    ref Point pointPointer1 = ref pointArray[this.pointArray.Count];
                    pointPointer1.Y = pointPointer1.Y - offsetY;
                }
            }
            byte[] numArray = new byte[(this.pointArray.Count <= 1 || !this.FigureCompleted || !this.CloseFigure ? this.pointArray.Count : this.pointArray.Count + 1)];
            int num7 = 0;
            while (true)
            {
                if (num7 >= (this.pointArray.Count <= 1 || !this.FigureCompleted || !this.CloseFigure ? this.pointArray.Count : this.pointArray.Count + 1))
                {
                    break;
                }
                numArray[num7] = 1;
                num7++;
            }
            return new GraphicsPath(pointArray, numArray);
        }

        protected override bool PointInObject(Point point, int offsetX, int offsetY)
        {
            this.pointIsOnOutline = this.PointInOutline(point, offsetX, offsetY);
            if (!this.pointIsOnOutline)
            {
                this.pointInObject = (base.Filled ? this.PointInPolygon(new Point(point.X + offsetX, point.Y + offsetY)) : false);
            }
            else
            {
                this.pointInObject = true;
            }
            return this.pointInObject;
        }

        public bool PointInOutline(Point point, int offsetX, int offsetY)
        {
            for (int i = 0; i < this.PointArray.Count - 1; i++)
            {
                double segment = DrawLine.FindDistanceToSegment(new Point(point.X + offsetX, point.Y + offsetY), (Point)this.PointArray[i], (Point)this.PointArray[i + 1]);
                if (segment >= 0 && segment <= (double)(base.PenWidth / 2))
                {
                    return true;
                }
            }
            if (this.CloseFigure)
            {
                double num = DrawLine.FindDistanceToSegment(new Point(point.X + offsetX, point.Y + offsetY), (Point)this.PointArray[this.PointArray.Count - 1], (Point)this.PointArray[0]);
                if (num >= 0 && num <= (double)(base.PenWidth / 2))
                {
                    return true;
                }
            }
            return false;
        }

        public bool PointInPolygon(Point p)
        {
            Point point;
            Point point1;
            bool flag = false;
            if (this.PointArray.Count < 3)
            {
                return flag;
            }
            Point item = (Point)this.PointArray[this.PointArray.Count - 1];
            int x = item.X;
            Point item1 = (Point)this.PointArray[this.PointArray.Count - 1];
            Point point2 = new Point(x, item1.Y);
            foreach (Point pointArray in this.PointArray)
            {
                Point point3 = new Point(pointArray.X, pointArray.Y);
                if (point3.X <= point2.X)
                {
                    point = point3;
                    point1 = point2;
                }
                else
                {
                    point = point2;
                    point1 = point3;
                }
                if (point3.X < p.X == p.X <= point2.X && ((long)p.Y - (long)point.Y) * (long)(point1.X - point.X) < ((long)point1.Y - (long)point.Y) * (long)(p.X - point.X))
                {
                    flag = !flag;
                }
                point2 = point3;
            }
            return flag;
        }

        public override Region Region(int offsetX, int offsetY, float zoomFactor)
        {
            Region region = new Region();
            GraphicsPath graphicsPath = new GraphicsPath();
            offsetX -= (int)((float)base.DrawArea.DrawingBoard.HorizontalOffset / zoomFactor);
            offsetY -= (int)((float)base.DrawArea.DrawingBoard.VerticalOffset / zoomFactor);
            int num = 0;
            int x = 0;
            int y = 0;
            IEnumerator enumerator = this.pointArray.GetEnumerator();
            if (enumerator.MoveNext())
            {
                x = ((Point)enumerator.Current).X;
                y = ((Point)enumerator.Current).Y;
                num++;
            }
            while (enumerator.MoveNext())
            {
                int x1 = ((Point)enumerator.Current).X;
                int y1 = ((Point)enumerator.Current).Y;
                num++;
                graphicsPath.AddLine((float)(x - offsetX) * zoomFactor, (float)(y - offsetY) * zoomFactor, (float)(x1 - offsetX) * zoomFactor, (float)(y1 - offsetY) * zoomFactor);
                x = x1;
                y = y1;
            }
            graphicsPath.CloseFigure();
            RectangleF bounds = graphicsPath.GetBounds();
            bounds.Inflate(new Size(20 + base.PenWidth, 20 + base.PenWidth));
            region.MakeEmpty();
            region.Union(bounds);
            if (base.ShowMeasure)
            {
                foreach (DrawText textArray in base.TextArray)
                {
                    Point point = textArray.Point;
                    Rectangle rectangle = textArray.Rectangle;
                    int num1 = point.X - offsetX - rectangle.Width / 2;
                    Point point1 = textArray.Point;
                    Rectangle rectangle1 = textArray.Rectangle;
                    int y2 = point1.Y - offsetY - rectangle1.Height / 2;
                    int width = textArray.Rectangle.Width;
                    Rectangle rectangle2 = textArray.Rectangle;
                    Rectangle rectangle3 = new Rectangle(num1, y2, width, rectangle2.Height);

                    rectangle3.X = (int)(rectangle3.X * zoomFactor);
                    rectangle3.Y = (int)(rectangle3.Y * zoomFactor);
                    rectangle3.Width = (int)(rectangle3.Width * zoomFactor);
                    rectangle3.Height = (int)(rectangle3.Height * zoomFactor);

                    rectangle3.Inflate(new Size(base.PenWidth, base.PenWidth));
                    region.Union(rectangle3);
                }
            }
            for (int i = 1; i <= this.pointArray.Count; i++)
            {
                Rectangle handleRectangle = this.GetHandleRectangle(i, offsetX, offsetY);
                handleRectangle.X = (int)((float)handleRectangle.X * zoomFactor);
                handleRectangle.Y = (int)((float)handleRectangle.Y * zoomFactor);
                handleRectangle.Width = (int)((float)handleRectangle.Width * zoomFactor);
                handleRectangle.Height = (int)((float)handleRectangle.Height * zoomFactor);
                region.Union(handleRectangle);
            }
            graphicsPath.Dispose();
            return region;
        }

        public void RemovePoint(int index)
        {
            this.pointArray.RemoveAt(index);
        }

        public void RemovePoint(Point point)
        {
            if (!base.Filled && this.deductionArray.Count > 0)
            {
                for (int i = this.deductionArray.Count - 1; i >= 0; i--)
                {
                    DrawLine item = (DrawLine)this.deductionArray[i];
                    if (item.StartPoint.Equals(point) || item.EndPoint.Equals(point))
                    {
                        this.deductionArray.RemoveAt(i);
                    }
                }
            }
            this.pointArray.Remove(point);
            this.ResetDrops();
        }

        public void ResetDrops()
        {
            PointF pointF;
            foreach (DrawLine point in this.dropArray)
            {
                PointF empty = PointF.Empty;
                double num = double.PositiveInfinity;
                int num1 = 0;
                while (true)
                {
                    if (num1 >= this.pointArray.Count - (this.closeFigure ? 0 : 1))
                    {
                        break;
                    }
                    Point[] item = new Point[2];
                    if (!this.closeFigure || num1 + 1 != this.pointArray.Count)
                    {
                        item[0] = (Point)this.pointArray[num1];
                        item[1] = (Point)this.pointArray[num1 + 1];
                    }
                    else
                    {
                        item[0] = (Point)this.pointArray[this.pointArray.Count - 1];
                        item[1] = (Point)this.pointArray[0];
                    }
                    double segment = DrawLine.FindDistanceToSegment(point.StartPoint, item[0], item[1], out pointF);
                    if (segment < num)
                    {
                        num = segment;
                        empty = new PointF(pointF.X, pointF.Y);
                    }
                    num1++;
                }
                if (empty == PointF.Empty)
                {
                    continue;
                }
                point.StartPoint = new Point((int)Math.Round((double)empty.X, MidpointRounding.ToEven), (int)Math.Round((double)empty.Y, MidpointRounding.ToEven));
            }
        }

        public override void Scale(float scaleX, float scaleY)
        {
            int count = this.pointArray.Count;
            for (int i = 0; i < count; i++)
            {
                Point item = (Point)this.pointArray[i];
                int x = (int)((float)item.X * scaleX);
                Point point = (Point)this.pointArray[i];
                Point point1 = new Point(x, (int)((float)point.Y * scaleY));
                this.pointArray[i] = point1;
            }
            foreach (DrawObject drawObject in this.deductionArray)
            {
                if (!base.Filled)
                {
                    DrawLine drawLine = (DrawLine)drawObject;
                    int num = (int)((float)drawLine.StartPoint.X * scaleX);
                    Point startPoint = drawLine.StartPoint;
                    drawLine.StartPoint = new Point(num, (int)((float)startPoint.Y * scaleY));
                    int x1 = (int)((float)drawLine.EndPoint.X * scaleX);
                    Point endPoint = drawLine.EndPoint;
                    drawLine.EndPoint = new Point(x1, (int)((float)endPoint.Y * scaleY));
                }
                else
                {
                    DrawPolyLine drawPolyLine = (DrawPolyLine)drawObject;
                    for (int j = 0; j < drawPolyLine.PointArray.Count; j++)
                    {
                        Point item1 = (Point)drawPolyLine.PointArray[j];
                        int num1 = (int)((float)item1.X * scaleX);
                        Point item2 = (Point)drawPolyLine.PointArray[j];
                        Point point2 = new Point(num1, (int)((float)item2.Y * scaleY));
                        drawPolyLine.PointArray[j] = point2;
                    }
                }
            }
        }

        public int SegmentHitTest(Point point, int offsetX, int offsetY)
        {
            if (this.IsPartOfDrop(point, offsetX, offsetY) != Point.Empty)
            {
                return -4;
            }
            int num = this.HitTest(point, offsetX, offsetY);
            if (num != 0)
            {
                return num;
            }
            if (!this.PointIsOnOutline)
            {
                return 0;
            }
            this.SetSelectedSegment(new Point(point.X + offsetX, point.Y + offsetY));
            if (!this.SelectedSegment.IsValid())
            {
                return 0;
            }
            if (this.IsOpening(this.SelectedSegment.StartPoint, this.SelectedSegment.EndPoint))
            {
                return -3;
            }
            return -2;
        }

        private Brush SelectBrush()
        {
            Brush hatchBrush;
            if (base.DeductionParentID != -1)
            {
                hatchBrush = new HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.FromArgb(30, base.FillColor));
            }
            else if (this.Pattern != HatchStylePickerCombo.HatchStylePickerEnum.Solid)
            {
                hatchBrush = new HatchBrush((HatchStyle)this.Pattern, Color.FromArgb(base.Opacity, base.FillColor), Color.FromArgb(base.Opacity, Color.White));
            }
            else
            {
                hatchBrush = new SolidBrush(Color.FromArgb(base.Opacity, base.FillColor));
            }
            return hatchBrush;
        }

        private CustomRendering SelectCurrentRendering()
        {
            CustomRendering customRendering;
            if (base.Group.SelectedPreset != null && base.Group.SelectedPreset.CustomRendering != null)
            {
                base.Group.SelectedRenderingPreset = base.Group.SelectedPreset;
                return base.Group.SelectedPreset.CustomRendering;
            }
            if (base.Group.SelectedRenderingPreset != null && base.Group.SelectedRenderingPreset.CustomRendering != null)
            {
                return base.Group.SelectedRenderingPreset.CustomRendering;
            }
            IEnumerator enumerator = base.Group.Presets.Collection.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    Preset current = (Preset)enumerator.Current;
                    if (current.CustomRendering == null)
                    {
                        continue;
                    }
                    base.Group.SelectedRenderingPreset = current;
                    customRendering = current.CustomRendering;
                    return customRendering;
                }
                base.Group.SelectedRenderingPreset = null;
                return null;
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            return customRendering;
        }

        private Pen SelectPen()
        {
            Pen pen;
            pen = (base.DrawPen != null ? base.DrawPen.Clone() as Pen : new Pen(Color.FromArgb(base.Opacity + 30, base.Color), (float)base.PenWidth)
            {
                StartCap = LineCap.Square
            });
            return pen;
        }

        public void SetCustomRenderingAngle(string extensionID, int angle)
        {
            CustomRenderingProperties customRenderingProperties = this.GetCustomRenderingProperties(extensionID);
            customRenderingProperties.Angle = angle;
            this.SetCustomRenderingProperties(extensionID, customRenderingProperties);
        }

        public void SetCustomRenderingOffsetX(string extensionID, int offsetX)
        {
            CustomRenderingProperties customRenderingProperties = this.GetCustomRenderingProperties(extensionID);
            customRenderingProperties.OffsetX = offsetX;
            this.SetCustomRenderingProperties(extensionID, customRenderingProperties);
        }

        public void SetCustomRenderingOffsetY(string extensionID, int offsetY)
        {
            CustomRenderingProperties customRenderingProperties = this.GetCustomRenderingProperties(extensionID);
            customRenderingProperties.OffsetY = offsetY;
            this.SetCustomRenderingProperties(extensionID, customRenderingProperties);
        }

        public void SetCustomRenderingProperties(string extensionID, CustomRenderingProperties renderingProperties)
        {
            if (!this.customRenderingArray.ContainsKey(extensionID))
            {
                this.customRenderingArray.Add(extensionID, renderingProperties);
                return;
            }
            this.customRenderingArray[extensionID].Angle = renderingProperties.Angle;
            this.customRenderingArray[extensionID].OffsetX = renderingProperties.OffsetX;
            this.customRenderingArray[extensionID].OffsetY = renderingProperties.OffsetY;
        }

        public void SetDropLength(Point dropPoint, double length)
        {
            try
            {
                DrawLine drawLine = this.FindDrop(dropPoint);
                if (drawLine != null)
                {
                    drawLine.Height = length;
                }
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                Console.WriteLine("SetDropLength failed.");
                Console.WriteLine(exception.Message);
            }
        }

        public void SetOpeningHeight(Point startPoint, Point endPoint, double height)
        {
            try
            {
                DrawLine opening = this.GetOpening(startPoint, endPoint);
                if (opening != null)
                {
                    opening.Height = height;
                }
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                Console.WriteLine("SetOpeningHeight failed.");
                Console.WriteLine(exception.Message);
            }
        }

        public void SetSelectedSegment(Point point)
        {
            int nearestPointIndex = this.GetNearestPointIndex(point);
            if (nearestPointIndex > -1)
            {
                this.SelectedSegment.StartPoint = (Point)this.PointArray[nearestPointIndex];
                this.SelectedSegment.EndPoint = (Point)this.PointArray[(nearestPointIndex + 1 == this.PointArray.Count ? 0 : nearestPointIndex + 1)];
            }
        }

        public override void SetSlopeFactor(SlopeFactor slopeFactor)
        {
            this.SlopeFactor.SetValues(slopeFactor);
            foreach (DrawObject deductionArray in this.DeductionArray)
            {
                deductionArray.SetSlopeFactor(slopeFactor);
            }
        }

        private HatchStylePickerCombo.HatchStylePickerEnum ValidateHatchStyle(HatchStylePickerCombo.HatchStylePickerEnum hatchStyle)
        {
            if (hatchStyle < HatchStylePickerCombo.HatchStylePickerEnum.Solid)
            {
                return HatchStylePickerCombo.HatchStylePickerEnum.Solid;
            }
            if (hatchStyle < HatchStylePickerCombo.HatchStylePickerEnum.SolidDiamond)
            {
                return hatchStyle;
            }
            return HatchStylePickerCombo.HatchStylePickerEnum.SolidDiamond;
        }

        [CompilerGenerated]
        // <>c__DisplayClassf
        private sealed class u003cu003ec__DisplayClassf
        {
            public PointF pointInPolygon;

            public u003cu003ec__DisplayClassf()
            {
            }

            // <DetermineWindingDirection>b__c
            public PointF u003cDetermineWindingDirectionu003eb__c(Point point)
            {
                PointF pointF = new PointF((float)point.X - this.pointInPolygon.X, (float)point.Y - this.pointInPolygon.Y);
                return pointF;
            }
        }

        public enum AutoAdjustType
        {
            ToZone,
            Compress,
            CompressTopBottom,
            CompressLeftRight,
            Expand,
            ExpandTopBottom,
            ExpandLeftRight
        }

        public enum HitTestEnum
        {
            HitOnDrop = -4,
            HitOnOpening = -3,
            HitOnSegment = -2,
            NoHit = -1,
            HitAnywhere = 0
        }

        private class ValueCount
        {
            public int @value;

            public int offset;

            public int otherValue;

            public int count;

            public ValueCount(int value, int otherValue, int offset, int count)
            {
                this.@value = value;
                this.otherValue = otherValue;
                this.offset = offset;
                this.count = count;
            }
        }

        public enum WindingDirection
        {
            Clockwise,
            CounterClockWise
        }
    }
}