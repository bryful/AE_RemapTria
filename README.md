# AE_Remap_Tria
For After Effects Timeremap support<br>

# 変わった点
### ●データファイルの拡張子を変えました。<br>
".ardj.json"<br>
です。AEのプロジェクトファイルに読み込むことができます。収集で他のデータと一緒に集められます。

### ●キーの動作がかなり変わりました。
先日AEIOUを触って非常に使いやすかったので、なるべくキーをAEIOUに合わせるようにしました。一部違いますので詳細は後でまとめようと思ってます。

### ●データ駆動型アニメーションのサポート（予定）
[https://helpx.adobe.com/jp/after-effects/using/data-driven-animations.html](https://helpx.adobe.com/jp/after-effects/using/data-driven-animations.html)<br>

データ駆動型アニメーションの仕組みを使ってリマップにキーを打たない仕組みを考えています。AEが直接ardj.jsonファイルを読みコマうちをします。<br>
コマの修正はardj.jsonを修正するだけですみます。

### ●見た目
C#標準のパーツをほぼ使わずすべてカスタムコントロールで作成しました。

# 今後の予定

* バグだし。
* コマ入力補佐の機能
* 読み書きできるファイルを増やす。
* キーコンフィグ
* 描画の高速化

# Install

* AE_RemapTria.exe
* AE_RemapTriaCall.exe
* AE_RemapTria.jsx

上記の３個のファイルをScriptUI Panelsにコピーしてください。<br>
フローティングパネルにしたくないときは、適当な場所に３個のファイルを入れても大丈夫です。

# Usage
AE_RemapTria.jsxを実行してください。<br>

* call AE_Remap<br>AE_RemapTriaを起動させます。
* Get Cell Info<br>セル情報を獲得します。成功すると右にボタンが表示されます。
* Clear<br>読み込んだ情報を消します。
* Save Ardj<br>保存ダイアログを開きます。
* Open Ardj<br>読み込みダイアログを開きます。
* ドロップダウンでからセルの設定<br>〇ブロックディゾブル<br>〇リマップ最大値<br>〇不透明度
* inOutPoint<br>レイヤの前後の空セルはトリムします。

レイヤを選択して、右のボタンを押せばコマうちされます。


# Dependency
Visual studio 2022 C#


# License
This software is released under the MIT License, see LICENSE

# Authors

bry-ful(Hiroshi Furuhashi)<br>
twitter:[bryful](https://twitter.com/bryful)<br>
bryful@gmail.com

# References

