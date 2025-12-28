using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class Bookmark
	{
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Name>k__BackingField = value;
			}
		}

		public int LayerIndex
		{
			[CompilerGenerated]
			get
			{
				return this.<LayerIndex>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LayerIndex>k__BackingField = value;
			}
		}

		public int Zoom
		{
			[CompilerGenerated]
			get
			{
				return this.<Zoom>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Zoom>k__BackingField = value;
			}
		}

		public Point Origin
		{
			[CompilerGenerated]
			get
			{
				return this.<Origin>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Origin>k__BackingField = value;
			}
		}

		public Bookmark(string name, int layerIndex, int zoom, Point origin)
		{
			this.Name = name;
			this.LayerIndex = layerIndex;
			this.Zoom = zoom;
			this.Origin = origin;
		}

		[CompilerGenerated]
		private string <Name>k__BackingField;

		[CompilerGenerated]
		private int <LayerIndex>k__BackingField;

		[CompilerGenerated]
		private int <Zoom>k__BackingField;

		[CompilerGenerated]
		private Point <Origin>k__BackingField;
	}
}
