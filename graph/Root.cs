using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace graph
{
	public class Root
	{
		private string[] _Args = new string[0];
		public string[] Args
		{
			get
			{
				return _Args;
			}
		}
		public PDF PDF = new PDF();
		public DXF DXF = new DXF();
		public Root()
		{
			_Args = Environment.GetCommandLineArgs();
		}
		public PointF[] RotAry(PointF[] pa, PointF cp, float angleDegrees)
		{
			PointF[] result = new PointF[pa.Length];
			double angleRadians = angleDegrees * Math.PI / 180.0;

			for (int i = 0; i < pa.Length; i++)
			{
				double dx = pa[i].X - cp.X;
				double dy = pa[i].Y - cp.Y;

				double rotatedX = dx * Math.Cos(angleRadians) - dy * Math.Sin(angleRadians);
				double rotatedY = dx * Math.Sin(angleRadians) + dy * Math.Cos(angleRadians);

				result[i] = new PointF((float)(rotatedX + cp.X), (float)(rotatedY + cp.Y));
			}

			return result;
		}
		public PointF[] RectToArray(RectangleF rect)
		{

			PointF[] pa = new PointF[4];
			pa[0] = new PointF(rect.Left, rect.Top);
			pa[1] = new PointF(rect.Right, rect.Top);
			pa[2] = new PointF(rect.Right, rect.Bottom);
			pa[3] = new PointF(rect.Left, rect.Bottom);

			return pa;
		}
		public PointF MirrorPoint(PointF point, PointF lineStart, PointF lineEnd)
		{
			if (lineStart == lineEnd)
				return point; // 線が無効な場合はそのまま返す

			float dx = lineEnd.X - lineStart.X;
			float dy = lineEnd.Y - lineStart.Y;
			float lenSq = dx * dx + dy * dy;

			float px = point.X - lineStart.X;
			float py = point.Y - lineStart.Y;

			float t = (px * dx + py * dy) / lenSq;

			float projX = lineStart.X + t * dx;
			float projY = lineStart.Y + t * dy;

			float mirrorX = 2 * projX - point.X;
			float mirrorY = 2 * projY - point.Y;

			return new PointF(mirrorX, mirrorY);
		}

		public PointF[] MirrorAry(PointF[] pa, PointF lineStart, PointF lineEnd)
		{
			if (pa.Length == 0)
				return pa; // 空の配列の場合はそのまま返す

			PointF[] result = new PointF[pa.Length];
			for (int i = 0; i < pa.Length; i++)
			{
				result[i] = MirrorPoint(pa[i], lineStart, lineEnd);
			}
			return result;
		}
		public PointF[] ScaleAry(PointF[] pa, PointF cp, float sx, float sy)
		{
			PointF[] result = new PointF[pa.Length];

			for (int i = 0; i < pa.Length; i++)
			{
				double dx = pa[i].X - cp.X;
				double dy = pa[i].Y - cp.Y;

				double sdX = dx * sx/100;
				double sdY = dx * sy/100;

				result[i] = new PointF((float)(sdX + cp.X), (float)(sdY + cp.Y));
			}

			return result;
		}
		public PointF[] MoveAry(PointF[] pa,  float dx, float dy)
		{
			PointF[] result = new PointF[pa.Length];

			for (int i = 0; i < pa.Length; i++)
			{

				result[i] = new PointF((float)(pa[i].X + dx), (float)(pa[i].Y + dy));
			}

			return result;
		}
		public List<PointF[]> ClippingRect(List<PointF[]> rs, RectangleF mask)
		{
			return PolygonClipper.MaskRect(rs, mask);
		}
	}
}