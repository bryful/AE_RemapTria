# AE_Remap τρία
For After Effects Timeremap support<br>
AE_Remapシリーズの3世代目です。「えーいー りまっぷ とりゃー」って呼んでください。τρία（とりあ）が正式名ですがまぁ読みずらいので^^;<br>
<br>
C#の.NET6対応が第一目標です。.NETframework4.7で今まで作ってましたが、将来の事を考えて一番新しいものにしてます。ただ、作ってみて感じたのですが出来るアプリの完成度は.NETframework4.8で作ったものの方が良いように感じられます。<br>
まぁ、将来的にXamarin対応も考えているので.NET6で進めていきます。<br>
<br>
一応機能のほとんどは出来ました。バグがまだのこっています。今後はバグ取りがメインになります。<br>
ファイル保存系のダイアログをすべてシステム標準の物にもどしました。使ってるとなんか気分的に嫌な感じだったので。

# 変わった点
### ●データファイルの拡張子を変えました。<br>
".ardj.json"<br>
です。AEのプロジェクトファイルに読み込むことができます。収集で他のデータと一緒に集められます。<br>
また、内部情報も変更しましたが過去バージョンと互換性があります。

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

### Shiftキーを押しながら実行で初期化
たまに環境設定ファイルが壊れて困ったことがあったので、Shiftキー押しながら実行で環境設定ファイルを無視するようにしました。

# 必ずやる項目
* ！バグ 起動時ウィンドウがちらつくのを修正
* セーブを複数回繰り返すとEnabledFrame（抜きコマ）が消えるバグを消す
* 抜きコマを設定するとPDF出力が破綻する
* 使わなくなったコードの整理
* 画面は配色の見直し。TR_Colorの修正。

# 終わった項目

* Sheet設定ダイアログが使いにくい。
* ！バグ 一部の文字描画でFontが制御されていない箇所の特定
* ！バグ Func系が一部破綻してる。ショートカットキーが効かない等
* ！バグ Undo系の処理がおかしい(というか今処理が中途半端に無効になってる)
* さらなる画面の高速化
* 入力をReTAS形式から変更。普通にグリッドに入力部分を出す。
* セルの入れ替え Alt+左右キー
* AE_RemapTriaJson.jsxを追加
* セルとフレームの追加削除
* オフセットフレームの実装
* アプリ用のアイコン作成
* 必要なダイアログはほぼ作成完了
* キーコンフィグ
* Shiftキーを押しながら起動で設定の初期化を行うようにした。
* OpenSaveダイアログも自前で作成
* AE_RemapTriaCall.exeを廃止
* ~~高速化のためGrid表示を自前のダブルバッファーに。~~(効果なしとして中止)
* Altキー押しながらドラッグで番号のコピー
* メニューをカスタムコントロール化
* フォームのアクティブ状態をわかりやすく。
* 印刷に対応

# 今後の予定

* 簡単なマニュアルを作成
* フレーム表示設定を切り替えるメニューを作成
* TextBoxカスタムコントロールのカスタム描画
* バグだし。
* 描画の高速化
* 履歴保存の実装

# Install

* AE_RemapTria.exe
* AE_RemapTria.jsx
* AE_RemapTriaJson.jsx

上記の3個のファイルをScriptUI Panelsにコピーしてください。<br>
フローティングパネルにしたくないときは、適当な場所に3個のファイルを入れても大丈夫です。

<b>AE_RemapTriaCall.exeは必要なくなりました。</b>

# Usage
AE_RemapTria.jsxを実行してください。<br>

* call AE_Remap<br>AE_RemapTriaを起動させます。
* Get Cell Info<br>セル情報を獲得します。成功すると右にボタンが表示されます。
* Clear<br>読み込んだ情報を消します。
* Save Ardj<br>保存ダイアログを開きます。
* Open Ardj<br>読み込みダイアログを開きます。
* ドロップダウンでからセルの設定<br>〇ブロックディゾブル<br>〇リマップ最大値<br>〇不透明度
* inOutPoint<br>レイヤの前後の空セルはトリムします。
* Quit AE_Remap<br>AE_Remapを終了します。

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

