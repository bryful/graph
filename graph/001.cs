
// サイズ指定。trueであればmm、falseであればpoint
DXF gr = new DXF();
//XPen p = new XPen(XColors.Red, 1);
//XBrush b = new XSolidBrush(XColors.Red);
//基本の形
var w = 2;
var h = 8;

PointF[] dot = new PointF[]{
	new PointF(-w/2, -h/2),
	new PointF(w/2, -h/2),
	new PointF(w/2, h/2),
	new PointF(-w/2, h/2),
};

dot = MoveAry(dot, 0, -200);

var rot = 2;
var rotCount = 360 / rot;

for (var i = 0; i < rotCount; i++)
{
	var angle = rot * i;
	var pa = RotAry(dot, new PointF(0, 0), angle);
	gr.DrawPolygon( pa);
}

var fn = "Rots.dxf";
if (gr.Save(fn) == true)
{
	Console.WriteLine(fn + " created");
}
else
{
	Console.WriteLine(fn + " not created");
}