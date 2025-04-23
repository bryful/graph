# graph

コンソールアプリです。<br>
<p>
読み込んだスクリプトに従って描画を行いDXF/PDFに保存します。<br>
ただ、CSharpScriptを使ってnetDxf/PDFSharpライブラリを呼び出すものです。
</p>
DXF/PDFで大変なパスをコードで作成することを目的に作りましたが、<b>大抵のC#の機能が使えるので汎用のマクロインタープリターとして使えます。</b><br>
</p>
<p>
最初はPDF目当てで作りましたが、DXFの方が扱いが楽だったのでそちらをメインにしています。<br>
</p>

# 簡単な使い方

```
graph <filename1> <filename2> ...
```
ファイルは複数指定できます。ファイル順にアペンドされて実行されます。
拡張子".cs" ".csx" ".csinc"のみ読み込みます。
使いまわしの汎用の関数を別ファイルに書いて使うためのこんな仕様になっています。

# CSスクリプト

C#v6までの構文で。.netframeworkはv4.8です。大抵のものはusingしています。<br>

# DXFを書き出す
<p>
netDXFライブラリを呼び出しているだけですが、簡単に使えるようにラッパークラス<b>"DXF"</b>を作成しています。
</p>
以下が簡単なサンプルです。

```
// DXFクラスオブジェクトを作成
DXF dxf = new DXF();

//線分のデータ作成
PointF[] linedata = new PointF[]{
    new PointF(0,0),
    new PointF(0,-100),
    new PointF(100,-100),
};

//線を引く
dxf.DrawLine(linedata);

//保存する
dxf.Save("foo.dxf");



```
座標系はAfter EfefctsやC#のBitmapと同じ右がプラス、上がマイナスになります。

DXFクラスは以下の関数があります。
|                                                                  |                                        |
| ---------------------------------------------------------------- | -------------------------------------- |
| public void DrawLine(double x0, double y0, double x1, double y1) | 座標指定で線を描く                     |
| public void DrawLines(PointF[] pa)                               | 線分配列で線を描く                     |
| public void DrawPolygon(PointF[] pnts)                           | 線分配列で閉じた線（面の境界線）を描く |
| public void DrawEllipse(RectangleF rct)                          | 円を描く                               |

引数はList＜PointF[]＞の形式にも対応しています。

PDFファイルもPDFクラスで作成できます。基本的にDXFと同じ使い方です。

# サンプルの説明

以下のサンプルが含まれています。

|          |                                                   |
| -------- | ------------------------------------------------- |
| 000.cs   | 文字出力とアラート表示のサンプル                  |
| 001.cs   | 外部プログラムを呼び出すサンプル                  |
| 002.cs   | コマンドライン引数を受け取るサンプル              |
| 007.cs   | 時計の目盛りみたいな描画してPDFに書き出すサンプル |
| 008.cs   | 007.csのDXF版                                     |
| 009.cs   | ゼブラ模様を描いてDXFに書き出すサンプル           |
| 010.ｃｓ | C#のBitmapで描画を行いpngに保存するサンプル       |
# 内部関数

線はPointFの配列で指定します。配列処理のため以下の関数を実装しています。<br>
| 関数名                                                                                                                            | 説明                                     |                                                                                                                                                                                                                                                                                                                                                                                                                             |
| :-------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| public PointF[] RotAry(PointF[] pa, PointF cp, float angleDegrees)                                                                | 線分配列をcpをアンカーにして回転         |                                                                                                                                                                                                                                                                                                                                                                                                                             |
| public PointF[] MirrorAry(PointF[] pa, PointF lineStart, PointF lineEnd)                                                          | 線分配列を線分をもとにミラー反転         |                                                                                                                                                                                                                                                                                                                                                                                                                             |
| public PointF[] ScaleAry(PointF[] pa, PointF cp, float sx, float sy)                                                              | 線分配列をcpをアンカーにして拡大縮小     |                                                                                                                                                                                                                                                                                                                                                                                                                             |
| public PointF[] MoveAry(PointF[] pa,  float dx, float dy)                                                                         | 線分配列を移動                           |                                                                                                                                                                                                                                                                                                                                                                                                                             |
| public List<PointF[]> ClippingRect(List<PointF[]> rs, RectangleF mask)                                                            | 線分配列リストを矩形でクリッピング       |                                                                                                                                                                                                                                                                                                                                                                                                                             |
| public List<PointF[]> Clipping(<br>	List<PointF[]> subjectPolygons,<br>	List<PointF[]> clipPolygons,<br>	ClipOperation operation) | 線分リストをクリッピング                 | public enum ClipOperation<br>{<br>	/// <summary><br>	/// 交差 (2つの図形の重なり部分)<br>	/// </summary><br>	Intersection,<br>	/// <summary><br>	/// 和 (2つの図形の合成)<br>	/// </summary><br>	Union,<br>	/// <summary><br>	/// 差 (1つの図形からもう1つの図形を引いた部分)<br>	/// </summary><br>	Difference,<br>	/// <summary><br>	/// 排他的論理和 (2つの図形の重なり部分を除いた部分)<br>	/// </summary><br>	Xor<br>} |
| public string SaveFileDialog(string path ="",string title="SaveDialog", string filter="*.*&#124;*.*")                             | 保存ダイアログ                           |                                                                                                                                                                                                                                                                                                                                                                                                                             |
| public string OpenFileDialog(string path = "", string title = "OpenDialog", string filter = "*.*&#124;*.*")                       | 読み込みダイアログ                       |                                                                                                                                                                                                                                                                                                                                                                                                                             |
| public string InputDialog(string txt = "", string title = "InputDialog")                                                          | 入力ダイアログ                           |                                                                                                                                                                                                                                                                                                                                                                                                                             |
| public string Call(string[] args)                                                                                                 | コマンド呼び出し、標準出力を返値にかえす |                                                                                                                                                                                                                                                                                                                                                                                                                             |

# DXFファイル使用時の注意
DXFファイルは単位系の情報が保存されていないので、イラストレーター等で読み込むとき必ずそれを指定するダイアログが表示されます。

その時にかならず「元のサイズで」と単位を「ミリ」か「ポイント」「ピクセル」で意図しているもの指定します。<br>
イラストレータは単位を変更すると単位の値を適当に変えてしまうので注が必要です。

## Dependency
Visual studio 2022 C#

* CSharpScripting(Roslyn Analyzers)
* PDFSharp
* netDxf
* Clipper2


## License
This software is released under the MIT License, see LICENSE

## Authors

bry-ful(Hiroshi Furuhashi)<br>
twitter:[bryful](https://twitter.com/bryful)<br>
Mail: bryful@gmail.com<br>

