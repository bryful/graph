using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graph
{
	public class Tetrahedron
	{
		public float FrontRot { get; set; } = 0;
		public float RearRot { get; set; } = 0;
		public float RightRot { get; set; } = 0;
		public float LeftRot { get; set; } = 0;
		public PointF[] TopSurf = new PointF[4];
		public PointF[] FrontSurf = new PointF[4];
		public PointF[] RearSurf = new PointF[4];
		public PointF[] BottomSurf = new PointF[4];
		public PointF[] RightSurf = new PointF[4];
		public PointF[] LeftSurf = new PointF[4];
		public PointF[] TopHole = new PointF[4];
		public PointF[] RightHole = new PointF[4];
		public PointF[] LeftHole = new PointF[4];
		public Tetrahedron() 
		{
			Init();
		}
		private PointF[] CreateSurf()
		{
			PointF[] pnts = new PointF[4];
			pnts[0] = new PointF(0, 0);
			pnts[1] = new PointF(0, 0);
			pnts[2] = new PointF(0, 0);
			pnts[3] = new PointF(0, 0);
			return pnts;
		}
		public void Init()
		{
			FrontRot = 0;
			RearRot = 0;
			RightRot = 0;
			LeftRot = 0;
			TopSurf = CreateSurf();
			FrontSurf = CreateSurf();
			RearSurf = CreateSurf();
			BottomSurf = CreateSurf();
			RightSurf = CreateSurf();
			LeftSurf = CreateSurf();
			TopHole = CreateSurf();
			RightHole = CreateSurf();
			LeftHole = CreateSurf();
		}
		public void SetTopSurf(
			float width,
			float height,
			float depth,
			float frontrot,
			float rearrot,
			float rightrot,
			float leftrot
			)
		{
			
		}
	}
}
