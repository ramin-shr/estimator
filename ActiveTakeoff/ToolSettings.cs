using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class ToolSettings
	{
		public ToolSettings()
		{
			this.SlopeFactor = new SlopeFactor(SlopeFactor.HipValleyEnum.hipValleyUnavailable);
			this.SelectedSegment = new Segment();
			this.Visible = true;
			this.ShowMeasure = true;
			this.DrawFilled = false;
			this.CloseFigure = false;
			this.LineWidth = -1;
			this.CounterSize = 80;
			this.Shape = DrawCounter.CounterShapeTypeEnum.CounterShapeCircle;
			this.Opacity = 150;
			this.FillColor = Color.White;
			this.LineColor = Color.Black;
			this.Text = string.Empty;
			this.Comment = string.Empty;
			this.Name = string.Empty;
			this.GroupID = -1;
			this.IsDeduction = false;
		}

		public int GroupID
		{
			[CompilerGenerated]
			get
			{
				return this.<GroupID>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<GroupID>k__BackingField = value;
			}
		}

		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
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

		public string Text
		{
			[CompilerGenerated]
			get
			{
				return this.<Text>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Text>k__BackingField = value;
			}
		}

		public Color LineColor
		{
			[CompilerGenerated]
			get
			{
				return this.<LineColor>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LineColor>k__BackingField = value;
			}
		}

		public Color FillColor
		{
			[CompilerGenerated]
			get
			{
				return this.<FillColor>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<FillColor>k__BackingField = value;
			}
		}

		public HatchStylePickerCombo.HatchStylePickerEnum Pattern
		{
			[CompilerGenerated]
			get
			{
				return this.<Pattern>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Pattern>k__BackingField = value;
			}
		}

		public int Opacity
		{
			[CompilerGenerated]
			get
			{
				return this.<Opacity>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Opacity>k__BackingField = value;
			}
		}

		public int LineWidth
		{
			[CompilerGenerated]
			get
			{
				return this.<LineWidth>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LineWidth>k__BackingField = value;
			}
		}

		public int CounterSize
		{
			[CompilerGenerated]
			get
			{
				return this.<CounterSize>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CounterSize>k__BackingField = value;
			}
		}

		public DrawCounter.CounterShapeTypeEnum Shape
		{
			[CompilerGenerated]
			get
			{
				return this.<Shape>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Shape>k__BackingField = value;
			}
		}

		public bool CloseFigure
		{
			[CompilerGenerated]
			get
			{
				return this.<CloseFigure>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CloseFigure>k__BackingField = value;
			}
		}

		public bool DrawFilled
		{
			[CompilerGenerated]
			get
			{
				return this.<DrawFilled>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DrawFilled>k__BackingField = value;
			}
		}

		public bool ShowMeasure
		{
			[CompilerGenerated]
			get
			{
				return this.<ShowMeasure>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ShowMeasure>k__BackingField = value;
			}
		}

		public bool Visible
		{
			[CompilerGenerated]
			get
			{
				return this.<Visible>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Visible>k__BackingField = value;
			}
		}

		public bool IsDeduction
		{
			[CompilerGenerated]
			get
			{
				return this.<IsDeduction>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsDeduction>k__BackingField = value;
			}
		}

		public Point StartPoint
		{
			[CompilerGenerated]
			get
			{
				return this.<StartPoint>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<StartPoint>k__BackingField = value;
			}
		}

		public Segment SelectedSegment
		{
			[CompilerGenerated]
			get
			{
				return this.<SelectedSegment>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<SelectedSegment>k__BackingField = value;
			}
		}

		public SlopeFactor SlopeFactor
		{
			[CompilerGenerated]
			get
			{
				return this.<SlopeFactor>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<SlopeFactor>k__BackingField = value;
			}
		}

		[CompilerGenerated]
		private int <GroupID>k__BackingField;

		[CompilerGenerated]
		private string <Name>k__BackingField;

		[CompilerGenerated]
		private string <Comment>k__BackingField;

		[CompilerGenerated]
		private string <Text>k__BackingField;

		[CompilerGenerated]
		private Color <LineColor>k__BackingField;

		[CompilerGenerated]
		private Color <FillColor>k__BackingField;

		[CompilerGenerated]
		private HatchStylePickerCombo.HatchStylePickerEnum <Pattern>k__BackingField;

		[CompilerGenerated]
		private int <Opacity>k__BackingField;

		[CompilerGenerated]
		private int <LineWidth>k__BackingField;

		[CompilerGenerated]
		private int <CounterSize>k__BackingField;

		[CompilerGenerated]
		private DrawCounter.CounterShapeTypeEnum <Shape>k__BackingField;

		[CompilerGenerated]
		private bool <CloseFigure>k__BackingField;

		[CompilerGenerated]
		private bool <DrawFilled>k__BackingField;

		[CompilerGenerated]
		private bool <ShowMeasure>k__BackingField;

		[CompilerGenerated]
		private bool <Visible>k__BackingField;

		[CompilerGenerated]
		private bool <IsDeduction>k__BackingField;

		[CompilerGenerated]
		private Point <StartPoint>k__BackingField;

		[CompilerGenerated]
		private Segment <SelectedSegment>k__BackingField;

		[CompilerGenerated]
		private SlopeFactor <SlopeFactor>k__BackingField;
	}
}
