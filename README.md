# AE_Remap τρία
For After Effects Timeremap support<br>
AE_Remapシリーズの3世代目です。「えーいー りまっぷ とりゃー」って呼んでください。τρία（とりあ）が正式名ですがまぁ読みずらいので^^;<br>
<br>
C#の.NET6対応が第一目標です。.NETframework4.7で今まで作ってましたが、将来の事を考えて一番新しいものにしてます。ただ、作ってみて感じたのですが出来るアプリの完成度は.NETframework4.8で作ったものの方が良いように感じられます。<br>
まぁ、将来的にXamarin対応も考えているので.NET6で進めていきます。<br>



# 変わった点
### ●データファイルの拡張子を変えました。<br>
".ardj.json"<br>
です。AEのプロジェクトファイルに読み込むことができます。収集で他のデータと一緒に集められます。<br>
また、内部情報も変更しましたが過去バージョンと互換性があります。~~Beta2の段階で互換性が失われました。でも、頑張って互換性ありにするつもりです。~~

### ●キーの動作がかなり変わりました。
先日AEIOUを触って非常に使いやすかったので、<bstron>なるべくキーをAEIOUに合わせるようにしました。</strong>一部違いますので詳細は後でまとめようと思ってます。<br>
理想はAEIOUと全く同じにしたいのですが、仕様の違いがあるので悩んでいます。

### ●データ駆動型アニメーションのサポート
[https://helpx.adobe.com/jp/after-effects/using/data-driven-animations.html](https://helpx.adobe.com/jp/after-effects/using/data-driven-animations.html)<br>

データ駆動型アニメーションの仕組みを使ってリマップにキーを打たない仕組みを考えています。AEが直接ardj.jsonファイルを読みコマうちをします。<br>
コマの修正はardj.jsonを修正するだけですみます。<br>
あと、将来的にはBlenderとかnukeにも持っていけるようにしたいです。

### ●見た目
C#標準のパーツをほぼ使わずすべてカスタムコントロールで作成しました。標準コントロールの仕様に縛られない機能を実装。フォントは個人的に好きな[source han code jp](https://github.com/adobe-fonts/source-han-code-jp)を内蔵させました。

### .NET6で０から作成
C#のフレームワークを.NET6に変えました。今現在パフォーマンス的に劣りますが、２，３年後には解消される予定です。

### プロセス間通信を使ったデータ処理
今までは中間ファイルを通してAEとデータの通信を行っていましたが、直接行うようにしました。

### すべての機能をコマンドラインで操作可能
上記のプロセス間通信実装の副産物ですべての機能をコマンドラインから制御できます。バッチファイルを使ったxdtsからstsへの変換とか出来るようにするつもりです。

# 終わった項目

* セルの入れ替え Alt+左右キー
* AE_RemapTriaJson.jsxを追加
* ~~ファイルの互換性が失われた。~~ 対処しました。
* セルとフレームの追加削除
* オフセットフレームの実装
* アプリ用のアイコン作成

# 今後の予定

* バグだし。
* コマ入力補佐の機能
* 読み書きできるファイルを増やす。
* キーコンフィグ
* 描画の高速化
* タイムシートの印刷（一番後回しの予定）

# Install

* AE_RemapTria.exe
* AE_RemapTriaCall.exe
* AE_RemapTria.jsx
* AE_RemapTriaJson.jsx

上記の４個のファイルをScriptUI Panelsにコピーしてください。<br>
フローティングパネルにしたくないときは、適当な場所に4個のファイルを入れても大丈夫です。

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

