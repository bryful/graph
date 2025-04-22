﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using netDxf;
using netDxf.Entities;
using System.Drawing;
using System.IO;
using System.Windows;
namespace graph
{
	public class DXF
	{
		private DxfDocument dxf = new DxfDocument();
		public DXF() 
		{ 
		}
		public bool Save(string fn)
		{
			bool ret = false;
				try
				{
					if (File.Exists(fn))
					{
						File.Delete(fn);
					}
					ret =　dxf.Save(fn);
				}
				catch
				{
					ret = false;
				}
			return ret;
		}
		public Vector2[] ToVector2(PointF[] a)
		{
			var polygonPoints = new Vector2[a.Length];
			for (int i = 0; i < a.Length; i++)
			{
				polygonPoints[i] = new Vector2(
					(a[i].X),
					(-a[i].Y));

			}
			return polygonPoints;
		}
		public void DrawLine(double x0, double y0, double x1, double y1)
		{
			PointF[] pointFs = new PointF[2];
			pointFs[0] = new PointF((float)x0, (float)y0);
			pointFs[1] = new PointF((float)x1, (float)y1);

			Polyline2D t1 = new Polyline2D(ToVector2(pointFs));
			t1.IsClosed = false;
			dxf.Entities.Add(t1);
		}
		public void DrawLines(PointF[] pa)
		{
			if (pa.Length >= 0)
			{
				Polyline2D t1 = new Polyline2D(ToVector2(pa));
				t1.IsClosed = false;
				dxf.Entities.Add(t1);
			}
		}
		public void DrawLines(List<PointF[]> pa)
		{
			if (pa.Count >= 0)
			{
				for (int i = 0; i < pa.Count; i++)
				{
					DrawLines(pa[i]);
				}
			}
		}
		public void DrawPolygon(PointF[] pnts)
		{
			if (pnts.Length >= 0)
			{
				Polyline2D t1 = new Polyline2D(ToVector2(pnts));
				t1.IsClosed = true;
				dxf.Entities.Add(t1);
			}
		}
		public void DrawPolygon(List<PointF[]> pa)
		{
			if (pa.Count >= 0)
			{
				for (int i = 0; i < pa.Count; i++)
				{
					DrawPolygon(pa[i]);
				}
			}
		}
		public void DrawEllipse(RectangleF rct)
		{
			if (rct.Width > 0 && rct.Height > 0)
			{
				double centerX = rct.X + rct.Width / 2;
				double centerY = rct.Y + rct.Height / 2;
				double radiusX = rct.Width / 2;
				double radiusY = rct.Height / 2;
				Ellipse ellipse = new Ellipse(new Vector2(centerX, -centerY), radiusX, radiusY);
				dxf.Entities.Add(ellipse);
			}

		}
	}
}
