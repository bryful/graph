/*
 * 円弧
 * 
 */
DXF dxf = new DXF();

PointF cp = new PointF(0, 0);
dxf.DrawSemiCircle(cp, 100, 0,270);
dxf.DrawEllipse(cp, 80);

var fn = "Ellipse.dxf";
if (dxf.Save(fn)==true)
{
	Console.WriteLine(fn +" created");
}
else
{
	Console.WriteLine(fn + " not created");
}