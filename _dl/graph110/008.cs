/*
 * PDFを描画保存するサンプル
 * 
 */
// pdf作成
DXF dxf = new DXF();

//基本の形
// パスで四角形
var w = 2;
var h = 8;
PointF[] dot = new PointF[]{
	new PointF(-w/2, -h/2),
	new PointF(w/2, -h/2),
	new PointF(w/2, h/2),
	new PointF(-w/2, h/2),
};

//上に200px移動
dot = MoveAry(dot, 0, -200);


//回転角度2度で複製描画
var rot = 2;
var rotCount = 360 / rot;

for (var i=0; i < rotCount; i++)
{
	var angle = rot * i;
	var pa = RotAry(dot, new PointF(0, 0), angle);
	dxf.DrawPolygon(pa);
}

var fn = "Rots.dxf";
if (dxf.Save(fn)==true)
{
	Console.WriteLine(fn +" created");
}
else
{
	Console.WriteLine(fn + " not created");
}