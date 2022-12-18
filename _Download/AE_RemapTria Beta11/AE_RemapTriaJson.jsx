// ***************************************************************************
/*
    AE_RemapTria をafter Effectsから制御するスクリプト
*/
// ***************************************************************************

//JSON関係
if ( typeof (FsJSON) !== "object"){//デバッグ時はコメントアウトする
	FsJSON = {};
}//デバッグ時はコメントアウトする

(function (me){
    //各種プロトタイプを設定
    String.prototype.trim = function(){
        if (this=="" ) return ""
        else return this.replace(/[\r\n]+$|^\s+|\s+$/g, "");
    }
    //親ディレクトリのパスを切りだす。
    String.prototype.getParent = function(){
        var r=this;var i=this.lastIndexOf("/");if(i>=0) r=this.substring(0,i);
        return r;
    }
    //ファイル名のみ取り出す（拡張子付き）
    String.prototype.getName = function(){
        var r=this;var i=this.lastIndexOf("/");if(i>=0) r=this.substring(i+1);
        return r;
    }
    //拡張子のみを取り出す。
    String.prototype.getExt = function(){
        var r="";var i=this.lastIndexOf(".");if (i>=0) r=this.substring(i);
        return r;
    }
    //指定した書拡張子に変更（dotを必ず入れること）空文字を入れれば拡張子の消去。
    String.prototype.changeExt=function(s){
        var i=this.lastIndexOf(".");
        if(i>=0){return this.substring(0,i)+s;}else{return this; }
    }
    //文字の置換。（全ての一致した部分を置換）
    String.prototype.replaceAll=function(s,d){ return this.split(s).join(d);}

    FootageItem.prototype.nameTrue = function(){ var b=this.name;this.name=""; var ret=this.name;this.name=b;return ret;}

    String.prototype.replaceAll=function(s,d){ return this.split(s).join(d);}


    //グローバルな変数
	var scriptName = File.decode($.fileName.getName().changeExt(""));
	var aeremapCallPath = File.decode($.fileName.getParent()+"/AE_RemapTriaCall.exe");

    //セル指定用のラジオボタン配列
    var rbtns = [];
    var ardj = null;
	//------------------------
    var savePref = function()
    {
        var f = new File(Folder.userData.fullName + "/ae_remapTriajson.pref");
        var obj = {};
        //obj.emptyCell = emptyCell;
        //obj.inOutPoint = inOutPoint;

        if (f.open("w")==true)
        {
            try{
                f.write(obj.toSource());
            }catch(e){
            }finally{
                f.close();
            }
        }
    }
    var loadPref = function()
    {
        var f = new File(Folder.userData.fullName + "/ae_remapTriajaon.pref");
        if (f.open("r")==true)
        {
            try{
                var s = f.read();
                var obj = eval(s);
                /*
                var n = obj.emptyCell;
                 if ( typeof(n)=="number")
                {
                     emptyCell = n;
                }else{
                    emptyCell = 1;
                }
                var b = obj.inOutPoint;
                 if ( typeof(n)=="boolean")
                {
                     inOutPoint = b;
                }else{
                    inOutPoint = true;
                }
                */
            }catch(e){
            }finally{
                f.close();
            }
        }
    }
    //loadPref();
	//-------------------------------------------------------------------------
    //json utils
	function toJSON(obj)
	{
		var ret = "";
		if (typeof(obj) === "boolean"){
			ret = obj.toString();
		}else if (typeof(obj)=== "number"){
			ret = obj.toString();
		}else if (typeof(obj)=== "string"){
			ret = "\""+ obj +"\"";
		}else if ( obj instanceof Array){
			var r = "";
			for ( var i=0; i<obj.length;i++){
				if ( r !== "") r +=",";
				r += toJSON(obj[i]);
			}
			ret = "[" + r + "]";
		}else{
			for ( var a in obj)
			{
				if ( ret !=="") ret +=",";
				ret += "\""+a + "\":" + toJSON(obj[a]);
			}
			ret = "{" + ret + "}";

		}
		if ( (ret[0] === "(")&&(ret[ret.length-1]===")"))
		{
			ret = ret.substring(1,ret.length-1);
		}
		return ret;
	}
	if ( typeof(FsJSON.toJSON) !== "function"){
		FsJSON.toJSON = toJSON;
	}
	//------------------------
	function parse(s)
	{
		var ret = null;
		if ( typeof(s) !== "string") return ret;
		//前後の空白を削除
		s = s.replace(/[\r\n]+$|^\s+|\s+$/g, "");
		s = s.split("\r").join("").split("\n").join("");
		if ( s.length<=0) return ret;

		var ss = "";
		var idx1 =0;
		var len = s.length;
		while(idx1<len)
		{
			var idx2 = -1;
			if ( s[idx1]==="\""){
				var idx2 = s.indexOf("\"",idx1+1);
				if ((idx2>idx1)&&(idx2<s.length)){
					if ( s[idx2+1] !== ":") idx2 = -1;
				}
			}
			if ( idx2 ==-1) {
				ss += s[idx1];
				idx1++;
			}else{
				ss += s.substring(idx1+1,idx2)+":";
				idx1 = idx2+2;
			}
		}
		if ( ss[0]=="{"){
			ss ="("+ss+")";
		}
		try{
			ret = eval(ss);
		}catch(e){
			ret = null;
		}
		return ret;
	}
	if ( typeof(FsJSON.parse) !== "function"){
		FsJSON.parse = parse;
	}
	// ********************************************************************************
	var getActiveComp = function()
	{
		var ret = null;
		ret = app.project.activeItem;

		if ( (ret instanceof CompItem)===false)
		{
			ret = null;
			alert("コンポをアクティブにしてください！");
		}
		return ret;
	}
	var getJsonFile = function()
	{
		var ret = null;
		var ftg = app.project.activeItem;

		if ( ((ftg instanceof FootageItem)===false)&&(ftg.file==null))
		{
            alert("フッテージを選んでください");
            return;
		}
        var f = ftg.file;
        var n = f.fullName.getName().toLowerCase();
        var idx = n.indexOf(".ardj.json");
        if(idx<0)
        {
            ret = null;
            return ret;
        }
        if (f.open("r")==true)
        {
            try{
                var s = f.read();
                var obj = parse(s);
                if((obj.header!=null)&&(obj.header!=undefined))
                {
                    if(obj.header=="ardjV2")
                    {
                        obj.jname = ftg.name;
                        obj.footage = ftg;
                        ret = obj;
                    }
                }
            }catch(e){
                alert("aaa\r\n"+e.toString());
            }finally{
                f.close();
            }
        }
		return ret;
	}
	// ********************************************************************************
    var getLayer = function(cmp)
	{
		var ret = null;
		if ( (cmp ==null)||(cmp ==undefined)||( (cmp instanceof CompItem)==false)) {
			var ac = getActiveComp();
			if (ac == null) return ret;
			cmp = ac;
		}
		var lyrs = cmp.selectedLayers;
		if(lyrs.length<=0){
            alert("レイヤを選んで")
        }else{
            ret = lyrs;
        }
		return ret;
	}
    // ********************************************************************************
    var getLayerOne = function(cmp)
	{
		var ret = null;
		if ( (cmp ==null)||(cmp ==undefined)||( (cmp instanceof CompItem)==false)) {
			var ac = getActiveComp();
			if (ac == null) return ret;
			cmp = ac;
		}
		var lyrs = cmp.selectedLayers;
		if(lyrs.length!=1){
            alert("レイヤを1個だけ選んで")
        }else{
            ret = lyrs[0];
        }
		return ret;
	}

    //-------------------------------------------------------------------------


    // ********************************************************************************************
    var winObj = (me instanceof Panel)? me : new Window('palette{text:"AE_RemapTriaJson",orientation : "column", properties : {resizeable : true} }');
    // ********************************************************************************************
    var res1 =
'Group{alignment: ["fill", "fill" ],orientation:"row",preferredSize:[500,300],\
g0:Group{alignment:["left","fill"],orientation:"column",preferredSize:[70,400],\
btnGetData:Button{alignment:["fill","top"],text:"Json情報獲得"},\
btnClear:Button{alignment:["fill","top"],text:"Clear"},\
btnMark:Button{alignment:["fill","top"],text:"marker"}},\
g1:Group{alignment:["fill","fill"],orientation:"column",\
edJson:EditText{alignment:["fill","top"]},\
pnlCells:Panel{alignment:["fill","fill"],orientation:"row",text:"Cells"}}\
}';
//btnApply:Button{alignment:["fill","top"],text:"適応"},\


    winObj.gr = winObj.add(res1);
    winObj.layout.layout();
    winObj.onResize = function()
    {
        winObj.layout.resize();
    }
    // ********************************************************************************************
	//-------------------------------------------------------------------------
    var clearRbtns = function()
    {
        if (rbtns.length>0){
            for (var i=rbtns.length-1; i>=0;i--){
                rbtns[i].visible = false;
                delete rbtns[i];
                rbtns[i] = null;
                rbtns.pop();
            }
            rbtns = [];
            winObj.layout.layout();
       }
    }
	//-------------------------------------------------------------------------
    var clearAll  = function()
    {
        winObj.gr.g1.edJson.text = "";
        clearRbtns();
        ardj = null;
    }
	//-------------------------------------------------------------------------

    winObj.gr.g0.btnClear.onClick=function()
    {
        clearAll();
    }

    var pnlCells = winObj.gr.g1.pnlCells;
	//-------------------------------------------------------------------------
    var makeRbtn = function(obj)
    {
        clearRbtns();
        if (obj.cell.length>0)
        {
            pnlCells.orientation = "column";
            pnlCells.alignment = ["fill","fill"];

            var bounds = pnlCells.bounds;
            var w = bounds[2] - bounds[0] -20;
            var cc = obj.rawCaption.length;
            if(cc<=0) return;
            for (var i=0; i<cc;i++)
            {
                var cap =obj.rawCaption[i];
                var b =[10,i*30+10,10+w,i*30+10+25];
                var btn = pnlCells.add(
                    "button",
                    b,
                    cap +" :Apply",
                    {alignment:["fill","top"],justify:"left"}
                    );
                btn.alignment= ["fill","top"];
                btn.justify = "left";
                btn.key = cap;
                btn.duration = obj.frameCount / obj.frameRate;
                btn.footage = obj.footage;
                btn.onClick=function(){
                    //alert(this.key+"\r\n"+this.jname);
                    applyCells(this.key,this.duration,this.footage);
                }
                rbtns.push(btn);
                winObj.layout.layout();
                winObj.layout.resize();
            }
        }
    }
    //-------------------------------------------------------------------------

    //-------------------------------------------------------------------------
	var getJsonDara = function()
	{
        var obj = getJsonFile();
        if(obj==null) return;
        if ( (obj.rawCaption==null)||(obj.rawCaption==undefined)||(obj.rawCaption.length<=0))
        {
            alert("古いアージョンのardjファイルです");
            return;
        }
        ardj = obj;
        winObj.gr.g1.edJson.text = obj.sheetName;
        makeRbtn(obj);
    }
    winObj.gr.g0.btnGetData.onClick = getJsonDara;

    //-------------------------------------------------------------------------
    var applyCells = function(key,duration,footage)
    {
        var lyr = getLayer();
        if((lyr==null)||(lyr.length<=0)) return;

        var comp = lyr[0].containingComp;
        if(comp.duration != duration)comp.duration = duration;

        var flg = false;
        for(var i=1;i<=comp.numLayers;i++)
        {
            if(comp.layers[i].source.file==null) continue;
            if(comp.layers[i].source.file.fullName==footage.file.fullName)
            {
                flg = true;
                break;
            }
        }
        if(flg==false)
        {
            comp.layers.add(footage);
        }



        app.beginUndoGroup("cell");
        for ( var i=0; i<lyr.length;i++)
        {
            var tl = lyr[i];
            tl.inPoint =0;
            tl.displayDtartTIme = 0;
            tl.outPoint = duration;
            var rp = tl.property(2);
            if (rp.numKeys>0) for ( var i=rp.numKeys; i>=1;i--) rp.removeKey(i);
            tl.timeRemapEnabled = true;
            if (rp.numKeys>0) for ( var i=rp.numKeys; i>1;i--) rp.removeKey(i);
            tl.startTime = 0;
            tl.inPoint = 0;
            tl.outPoint = comp.duration;


            var expStr=
            'try{\r\n'+
            'var key = "$1";\r\n'+
            'var jname= "$2";\r\n'+
            'var obj = thisComp.layer(jname).source.sourceData;\r\n'+
            'var tbl = obj.rawData[key];\r\n'+
            'var f = Math.floor(time*obj.frameRate);\r\n'+
            'var v=9999;\r\n'+
            'if((f>=0)&&(f<tbl.length))\r\n'+
            '{\r\n'+
            '	v = ((tbl[f] -1)/obj.frameRate);\r\n'+
            '	if(v<0) v=9999;\r\n'+
            '}\r\n'+
            'v;\r\n'+
            '}catch(e){\r\n'+
            'time;\r\n'+
            '}\r\n';
            expStr =expStr.split("$1").join(key);
            expStr =expStr.split("$2").join(footage.name);
            rp.expression = expStr;

            markerChk(tl);

        }
        app.endUndoGroup();

    }
    //btnApply.onClick = applyCells;

	//-------------------------------------------------------------------------
    var markerChk= function(tl)
    {
        var mk = tl.property("Marker");
        if(mk.numKeys>0) for(var i=mk.numKeys;i>=1;i--) mk.removeKey(i);
        var comp = tl.containingComp;
        var rp = tl.property(2);

        var fMax = Math.round(comp.duration * comp.frameRate);

        var maxV= Math.floor(rp.maxValue*comp.frameRate);

        var vv = Math.floor(rp.valueAtTime(0,false)*comp.frameRate);
        var s = "";
        if (vv>=maxV) { s = "x";} else {s = ""+(vv+1)}
        mk.setValueAtTime(0,new MarkerValue(s));

        for ( var i=1;i<fMax;i++)
        {
            var t = i/comp.frameRate;
            var v0 = rp.valueAtTime(t - 1/comp.frameRate,false);
            var v1 = rp.valueAtTime(t,false);
            if(v0!=v1)
            {
                vv = Math.floor(v1*comp.frameRate);
                if (vv>=maxV) { s = "x";} else {s = "" + (vv+1)}
                mk.setValueAtTime(t,new MarkerValue(s));
            }
        }
    }
    var markerCheck = function(key,duration,footage)
    {
        var lyr = getLayer();
        if((lyr==null)||(lyr.length<=0)) return;

        app.beginUndoGroup("cell");
        for ( var i=0; i<lyr.length;i++)
        {
            markerChk(lyr[i]);

        }
        app.endUndoGroup();

    }
    winObj.gr.g0.btnMark.onClick = markerCheck;

    //-------------------------------------------------------------------------
	if ( ( me instanceof Panel) == false){
		winObj.center();
		winObj.show();
	}
	//-------------------------------------------------------------------------
})(this);
