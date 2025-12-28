using System;
using System.Drawing;

namespace QuoterPlan
{
	public class Segment
	{
		public Segment()
		{
			this.startPoint = new Point(0, 0);
			this.endPoint = new Point(0, 0);
		}

		public Segment(Point startPoint, Point endPoint, object tag)
		{
			this.startPoint = startPoint;
			this.endPoint = endPoint;
			this.tag = tag;
		}

		public Segment(Point startPoint, Point endPoint)
		{
			this.startPoint = startPoint;
			this.endPoint = endPoint;
		}

		public void Reset()
		{
			this.startPoint.X = 0;
			this.startPoint.Y = 0;
			this.endPoint.X = 0;
			this.endPoint.Y = 0;
		}

		public Point StartPoint
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

		public Point EndPoint
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

		public bool IsEqualTo(Point startPoint, Point endPoint)
		{
			return (this.startPoint == startPoint && this.endPoint == endPoint) || (this.startPoint == endPoint && this.endPoint == startPoint);
		}

		public bool IsValid()
		{
			bool flag = this.startPoint.X == 0 && this.startPoint.Y == 0 && this.endPoint.X == 0 && this.endPoint.Y == 0;
			return !flag;
		}

		private Point startPoint;

		private Point endPoint;

		private object tag;
	}
}
