using System;
using System.Drawing;

namespace QuoterPlan
{
	public class SegmentF
	{
		public SegmentF()
		{
			this.startPoint = new PointF(0f, 0f);
			this.endPoint = new PointF(0f, 0f);
		}

		public SegmentF(PointF startPoint, PointF endPoint, object tag)
		{
			this.startPoint = startPoint;
			this.endPoint = endPoint;
			this.tag = tag;
		}

		public SegmentF(PointF startPoint, PointF endPoint)
		{
			this.startPoint = startPoint;
			this.endPoint = endPoint;
		}

		public void Reset()
		{
			this.startPoint.X = 0f;
			this.startPoint.Y = 0f;
			this.endPoint.X = 0f;
			this.endPoint.Y = 0f;
		}

		public PointF StartPoint
		{
			get
			{
				return this.startPoint;
			}
			set
			{
				this.startPoint = value;
			}
		}

		public PointF EndPoint
		{
			get
			{
				return this.endPoint;
			}
			set
			{
				this.endPoint = value;
			}
		}

		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		public bool IsEqualTo(PointF startPoint, PointF endPoint)
		{
			return (this.startPoint == startPoint && this.endPoint == endPoint) || (this.startPoint == endPoint && this.endPoint == startPoint);
		}

		public bool IsValid()
		{
			return this.startPoint.X != 0f && this.startPoint.Y != 0f && this.endPoint.X != 0f && this.endPoint.Y != 0f;
		}

		private PointF startPoint;

		private PointF endPoint;

		private object tag;
	}
}
