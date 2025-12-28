using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace QuoterPlan
{
	internal abstract class Tool
	{
		public DrawingArea DrawArea
		{
			[CompilerGenerated]
			protected get
			{
				return this.<DrawArea>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DrawArea>k__BackingField = value;
			}
		}

		public virtual void ReleaseTool()
		{
		}

		public virtual void LoadCursor(Cursor cursor)
		{
		}

		public virtual void InitializeTool(Cursor cursor)
		{
		}

		public virtual void TrackMouse(Point location)
		{
		}

		public virtual void OnMouseDown(MouseEventArgs e)
		{
		}

		public virtual void OnMouseMove(MouseEventArgs e)
		{
		}

		public virtual void OnMouseUp(MouseEventArgs e)
		{
		}

		public virtual void OnKeyDown(KeyEventArgs e)
		{
		}

		protected Tool()
		{
		}

		[CompilerGenerated]
		private DrawingArea <DrawArea>k__BackingField;
	}
}
