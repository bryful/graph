using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Clipper2Lib;

namespace graph
{

	public enum ClipOperation
	{
		Intersection,
		Union,
		Difference,
		Xor
	}

	public class PolygonClipper
	{
		public static List<PointF[]> Execute(
			List<PointF[]> subjectPolygons,
			List<PointF[]> clipPolygons,
			ClipOperation operation,
			FillRule fillRule = FillRule.NonZero,
			int precision = 2)
		{
			var subject = ConvertToPathsD(subjectPolygons);
			var clip = ConvertToPathsD(clipPolygons);

			PathsD result = operation switch
			{
				ClipOperation.Intersection => Clipper.Intersect(subject, clip, fillRule, precision),
				ClipOperation.Union => Clipper.Union(subject, clip, fillRule, precision),
				ClipOperation.Difference => Clipper.Difference(subject, clip, fillRule, precision),
				ClipOperation.Xor => Clipper.Xor(subject, clip, fillRule, precision),
				_ => throw new ArgumentOutOfRangeException(nameof(operation), "Unsupported operation")
			};

			return ConvertToPointFArrays(result);
		}

		public static List<PointF[]> ConvertToPointFArrays(PathsD paths)
		{
			var results = new List<PointF[]>();
			foreach (var path in paths)
			{
				var points = new PointF[path.Count];
				for (int i = 0; i < path.Count; i++)
				{
					points[i] = new PointF((float)path[i].x, (float)path[i].y);
				}
				results.Add(points);
			}
			return results;
		}

		public static PathsD ConvertToPathsD(List<PointF[]> polygons)
		{
			var paths = new PathsD();
			foreach (var polygon in polygons)
			{
				var path = new PathD();
				foreach (var pt in polygon)
				{
					path.Add(new PointD(pt.X, pt.Y));
				}
				paths.Add(path);
			}
			return paths;
		}

		public static PointF[] CreatePolygon(params (float x, float y)[] points)
		{
			var list = new List<PointF>();
			foreach (var (x, y) in points)
			{
				list.Add(new PointF(x, y));
			}
			return list.ToArray();
		}

	
		public static List<PointF[]> MaskRect(
			List<PointF[]> subjectPolygons,
			RectangleF mask)
		{
			PointF[] maskPolygon = new PointF[4];
			maskPolygon[0] = new PointF(mask.Left, mask.Top);
			maskPolygon[1] = new PointF(mask.Right, mask.Top);
			maskPolygon[2] = new PointF(mask.Right, mask.Bottom);
			maskPolygon[3] = new PointF(mask.Left, mask.Bottom);
			List<PointF[]> clipPolygons = new List<PointF[]>();
			clipPolygons.Add(maskPolygon);
			List<PointF[]> result = Execute(subjectPolygons, clipPolygons, ClipOperation.Intersection);
			return result;
		}
	}
}