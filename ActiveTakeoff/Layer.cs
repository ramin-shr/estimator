using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class Layer : BaseInfo
	{
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

		public DrawingObjects DrawingObjects
		{
			[CompilerGenerated]
			get
			{
				return this.<DrawingObjects>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DrawingObjects>k__BackingField = value;
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

		public Layer(string name, bool visible, bool active, int opacity)
		{
			base.Name = name;
			base.Visible = visible;
			base.Active = active;
			this.Opacity = opacity;
			this.DrawingObjects = new DrawingObjects();
		}

		public override void Clear()
		{
			base.Clear();
			if (this.DrawingObjects != null)
			{
				this.DrawingObjects.Clear();
			}
		}

		public Layer Clone()
		{
			return new Layer(base.Name, base.Visible, base.Active, this.Opacity);
		}

		[CompilerGenerated]
		private int <Opacity>k__BackingField;

		[CompilerGenerated]
		private DrawingObjects <DrawingObjects>k__BackingField;

		[CompilerGenerated]
		private int <SortIndex>k__BackingField;
	}
}
