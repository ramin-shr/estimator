using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace QuoterPlan
{
	public class DockingWindow
	{
		public string Caption
		{
			[CompilerGenerated]
			get
			{
				return this.<Caption>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Caption>k__BackingField = value;
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

		public int Width
		{
			[CompilerGenerated]
			get
			{
				return this.<Width>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Width>k__BackingField = value;
			}
		}

		public int Height
		{
			[CompilerGenerated]
			get
			{
				return this.<Height>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Height>k__BackingField = value;
			}
		}

		public Bar ContainerBar
		{
			[CompilerGenerated]
			get
			{
				return this.<ContainerBar>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ContainerBar>k__BackingField = value;
			}
		}

		public DockContainerItem ContainerItem
		{
			[CompilerGenerated]
			get
			{
				return this.<ContainerItem>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ContainerItem>k__BackingField = value;
			}
		}

		public Control ContainerControl
		{
			[CompilerGenerated]
			get
			{
				return this.<ContainerControl>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ContainerControl>k__BackingField = value;
			}
		}

		public DockingWindow(string caption, bool visible, int width, int height, Bar containerBar, DockContainerItem containerItem, Control containerControl)
		{
			this.Caption = caption;
			this.Visible = visible;
			this.Width = width;
			this.Height = height;
			this.ContainerBar = containerBar;
			this.ContainerItem = containerItem;
			this.ContainerControl = containerControl;
			this.ContainerItem.DefaultFloatingSize = new Size(width, height);
		}

		[CompilerGenerated]
		private string <Caption>k__BackingField;

		[CompilerGenerated]
		private bool <Visible>k__BackingField;

		[CompilerGenerated]
		private int <Width>k__BackingField;

		[CompilerGenerated]
		private int <Height>k__BackingField;

		[CompilerGenerated]
		private Bar <ContainerBar>k__BackingField;

		[CompilerGenerated]
		private DockContainerItem <ContainerItem>k__BackingField;

		[CompilerGenerated]
		private Control <ContainerControl>k__BackingField;
	}
}
