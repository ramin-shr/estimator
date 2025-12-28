using System;
using System.Drawing.Drawing2D;

namespace QuoterPlanControls
{
	public class InterpolationModes
	{
		public InterpolationMode this[InterpolationModes.ZoomedImagesEnum index]
		{
			get
			{
				return this.m_InterpolationMode[(int)index];
			}
			set
			{
				this.m_InterpolationMode[(int)index] = value;
			}
		}

		public InterpolationModes()
		{
		}

		private InterpolationMode[] m_InterpolationMode = new InterpolationMode[]
		{
			InterpolationMode.High,
			InterpolationMode.High,
			InterpolationMode.High
		};

		public enum ZoomedImagesEnum
		{
			MoreThan50Percent,
			Between25and50Percents,
			Below25Percent
		}
	}
}
