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
	var aeremapCallPath = File.decode($.fileName.getParent()+"/AE_RemapTria.exe");

    //読み込む出るデータ
    var cellData = null;
    var cellDataV = null;
    //セル指定用のラジオボタン配列
    var rbtns = [];
    // 空セル
    // 0:ブロックディゾルブ 1: リマップ最大値 2:不透明度
    var emptyCell = 0;
    // 前後のin点out点の処理
    var inOutPoint = true;
	//------------------------
    var savePref = function()
    {
        var f = new File(Folder.userData.fullName + "/ae_remapTria.pref");
        var obj = {};
        obj.emptyCell = emptyCell;
        obj.inOutPoint = inOutPoint;

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
        var f = new File(Folder.userData.fullName + "/ae_remapTria.pref");
        if (f.open("r")==true)
        {
            try{
                var s = f.read();
                var obj = eval(s);
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
            }catch(e){
            }finally{
                f.close();
            }
        }
    }
    loadPref();
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
    // objectをセルデータに
    var anlysisCellData = function(obj)
    {
        var ret = {};
        var c = obj.cellCount;
        var f = obj.frameCount;
        var fr = obj.frameRate;
        ret.frameCount = obj.frameCount;
        ret.frameRate = obj.frameRate;
        ret.caption = [];
        ret.cell = [];
        ret.duration = f/fr;

        for ( var i=0; i<c; i++)
        {
            var cd = obj.cell[i];
            if ((cd.length==1)&&(cd[0][0]==0)&&(cd[0][1]==0)) continue;
            ret.caption.push(obj.caption[i]);

            var times = [];
            var values = [];
            for ( var j=0; j<cd.length;j++)
            {
                times.push(cd[j][0]/fr);
                values.push((cd[j][1]-1)/fr);
            }
            var ary = [];
            ary.push(times);
            ary.push(values);
            ret.cell.push(ary);
        }
         return ret;
    }

    //-------------------------------------------------------------------------
    //AEを起動させる
	var execAE_Reamp = function()
	{
        var ret = false;
		var aeremapCall = new File(aeremapCallPath);
		var cmd =  "\"" + aeremapCall.fsName +"\"";
		try{
            if (aeremapCall.exists==true){
                var r = "";
                r = system.callSystem(cmd + " /exenow");

                r = r.trim().toLowerCase();
                if (r=="false") {
                    r = system.callSystem(cmd + " /call");
                    if (r.indexOf("err")>=0){
                        ret = false;
                    }else{
                        ret = true;
                    }
                }else{
                    r = system.callSystem(cmd + " /active");
                    ret = true;
                }

            }else{
                alert(cmd + " : がありません");
            }
        }catch(e){
            alert(e.toString());
        }

        return ret;
    }
    //-------------------------------------------------------------------------
	var openAE_Reamp = function()
	{
        var ret = false;
		var aeremapCall = new File(aeremapCallPath);
		var cmd =  "\"" + aeremapCall.fsName +"\"";
		try{
            if (aeremapCall.exists==true){
                var r = "";
                r = system.callSystem(cmd + " /exenow");

                r = r.trim().toLowerCase();
                if (r=="false")
                {
                    alert("AE_Remapが起動していません");
                    return ret;
                }
                system.callSystem(cmd + " /open");
            }else{
                alert(cmd + " : がありません");
            }
        }catch(e){
            alert(e.toString());
        }

        return ret;
    }
    //-------------------------------------------------------------------------
	var saveAsAE_Reamp = function()
	{
        var ret = false;
		var aeremapCall = new File(aeremapCallPath);
		var cmd =  "\"" + aeremapCall.fsName +"\"";
		try{
            if (aeremapCall.exists==true){
                var r = "";
                r = system.callSystem(cmd + " /exenow");

                r = r.trim().toLowerCase();
                if (r=="false")
                {
                    alert("AE_Remapが起動していません");
                    return ret;
                }
                system.callSystem(cmd + " /saveas");
            }else{
                alert(cmd + " : がありません");
            }
        }catch(e){
            alert(e.toString());
        }

        return ret;
    }
//-------------------------------------------------------------------------
	var closeAE_Reamp = function()
	{
        var ret = false;
		var aeremapCall = new File(aeremapCallPath);
		var cmd =  "\"" + aeremapCall.fsName +"\"";
		try{
            if (aeremapCall.exists==true){
                var r = "";
                r = system.callSystem(cmd + " /exenow");

                r = r.trim().toLowerCase();
                if (r=="false")
                {
                    return ret;
                }
                system.callSystem(cmd + " /quit");
            }else{
                alert(cmd + " : がありません");
            }
        }catch(e){
            alert(e.toString());
        }

        return ret;
    }
    // ********************************************************************************************
    var winObj = (me instanceof Panel)? me : new Window('palette{text:"AE_RemapTria",orientation : "column", properties : {resizeable : true} }');
    // ********************************************************************************************
    var res1 =
'Group{alignment: ["fill", "fill" ],orientation:"row",preferredSize:[500,300],\
g0:Group{alignment:["left","fill"],orientation:"column",preferredSize:[70,400],\
btnRun:Button{alignment:["fill","top"],preferredSize:[70,25],text:"Call AE_Remap"},\
btnGetData:Button{alignment:["fill","top"],text:"Get Cell Info"},\
btnClear:Button{alignment:["fill","top"],text:"Clear"},\
btnSaveAs:Button{alignment:["fill","top"],text:"Save Ardj"},\
btnOpen:Button{alignment:["fill","top"],text:"Open Ardj"},\
btnGetCellLayer:Button{alignment:["fill","top"],text:"GetCellLayer"},\
cmbOP:DropDownList{alignment:["fill","top"]},\
cbInOutPoint:Checkbox{alignment:["fill","top"],text:"InOutPoint"},\
btnClose:Button{alignment:["fill","top"],text:"Quit AE_Remap"}},\
g1:Group{alignment:["fill","fill"],orientation:"column",\
edSheetName:EditText{alignment:["fill","top"]},\
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

    winObj.gr.g0.cbInOutPoint.value = inOutPoint;
    // ********************************************************************************************
    winObj.gr.g0.btnRun.onClick = execAE_Reamp;
    winObj.gr.g0.btnOpen.onClick = openAE_Reamp;
    winObj.gr.g0.btnSaveAs.onClick = saveAsAE_Reamp;
    winObj.gr.g0.btnClose.onClick = closeAE_Reamp;
    /*
    btnSaveArdj.onClick = export_ardj;
    btnCenter.onClick = cenertAE_Reamp;
    */
    winObj.gr.g0.cmbOP.add("item","ブロックディゾルブ");
    winObj.gr.g0.cmbOP.add("item","リマップ最大値");
    winObj.gr.g0.cmbOP.add("item","不透明");
    winObj.gr.g0.cmbOP.items[emptyCell].selected =true;
    winObj.gr.g0.cmbOP.onChange = function()
    {
        emptyCell =  winObj.gr.g0.cmbOP.selection.index;
        savePref();

    }
    winObj.gr.g0.cbInOutPoint.onClick=function()
    {
        inOutPoint = winObj.gr.g0.cbInOutPoint.value;
        savePref();
    }

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
        winObj.gr.g1.edSheetName.text = "";
        clearRbtns();
        cellData = null;
        cellDataV = null;
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
            for (var i=0; i<obj.cell.length;i++)
            {
                var cap =obj.caption[i];
                var b =[10,i*30+10,10+w,i*30+10+25];
                var btn = pnlCells.add(
                    "button",
                    b,
                    cap +" :Apply",
                    {alignment:["fill","top"],justify:"left"}
                    );
                btn.alignment= ["fill","top"];
                btn.justify = "left";
                btn.frameCount = obj.frameCount;
                btn.frameRate = obj.frameRate;
                btn.duration = obj.duration;
                btn.cell = obj.cell[i];
                btn.times = obj.cell[i][0];
                btn.values = obj.cell[i][1];
                btn.onClick=function(){
                    /*
                    var s = this.frameCount+":"+this.duration
                    +"\r\n"+this.times.toSource()
                    +"\r\n"+this.values.toSource()
                    ;
                    alert(s);*/
                    applyCells(this.frameRate,this.duration,this.values,this.times);
                    //applyCells = function(fr,du,values,times)
                }
                rbtns.push(btn);
                winObj.layout.layout();
                winObj.layout.resize();
            }
        }
    }
    //-------------------------------------------------------------------------

    //-------------------------------------------------------------------------
	var execAE_Export = function()
	{
        var ret = false;
		var aeremapCall = new File(aeremapCallPath);
        var cmd =  "\"" + aeremapCall.fsName +"\"";
        clearAll();
		if (aeremapCall.exists==true){
			try{
                var r = system.callSystem(cmd + " /export");
                if(r.indexOf("err")>=0) {
                    alert("01" +r)
                    return;
                }
                if(r=="")
                {
                    alert("接続が切れました。\r\nAE_Remap Exceedを再起動させてください");
                    return ret;
                }
                //alert(r);
                var obj = FsJSON.parse(r);
                if((obj.header =="ardjV2")) {
                    winObj.gr.g1.edSheetName.text = obj.sheetName;
                    cellData =  obj;
                    try{
                        cellDataV =anlysisCellData(obj);
                    }catch(e){
                        alert("execAE_Reamp 01\r\n" + e.toString());
                    }
                    makeRbtn(cellDataV);
                }else{
                    alert("不正なardj.jsonファイルです");
                    return ret;
                }
                ret = true;

			}catch(e){
				alert("execAE_Reamp\r\n" + e.toString());
                ret = false;
			}
		}
        return ret;
    }
    winObj.gr.g0.btnGetData.onClick = execAE_Export;

    //-------------------------------------------------------------------------
    var applyCells = function(fr,du,values,times)
    {
        if ((fr==null)||(du==null)||(values==null)||(times==null))
        {
            alert("セル情報が読み込まれていません");
            return;
        }

        // -----------------------------
        var findProp = function(pb,mn,na)
        {
            var ret = null;
            if (pb.numProperties>0)
            {
                for (var i=1; i<=pb.numProperties;i++)
                {
                    if ( (pb.property(i).matchName == mn)&&(pb.property(i).name == na))
                    {
                        ret = pb.property(i);
                        break;
                    }
                }
            }
            return ret;
        }
        // -----------------------------
        var applySub = function(lyr,times,values,emptys,emptyTimes)
        {
            if ( lyr.canSetTimeRemapEnabled == false) {
                return;
            }
            try{
                var rp = lyr.property(2);
                if (rp.numKeys>0) for ( var i=rp.numKeys; i>=1;i--) rp.removeKey(i);
                lyr.timeRemapEnabled = true;
 		        if (rp.numKeys>0) for ( var i=rp.numKeys; i>1;i--) rp.removeKey(i);
                lyr.startTime = 0;
                lyr.inPoint = 0;
                lyr.outPoint = lyr.containingComp.duration;
                var fr = lyr.containingComp.frameRate;
                rp.setValuesAtTimes(times,values);
		        for (var i=1 ; i<=rp.numKeys ; i++) {
                    rp.setInterpolationTypeAtKey(i,KeyframeInterpolationType.HOLD,KeyframeInterpolationType.HOLD);
                }
                switch(emptyCell)
                {
                    case 0:
                        var eg = lyr.property("ADBE Effect Parade");
                        var mn = "ADBE Block Dissolve";
                        var na = "EmptyCell";
                        if (eg.canAddProperty(mn)==true)
                        {
                            var bp = findProp(eg,mn,na);
                            if (bp==null){
                                bp = eg.addProperty(mn);
                                bp.name = na;
                            }
                            var bpv = bp.property(1);
                            if (bpv.numKeys>0) for ( var i=bpv.numKeys; i>=1;i--) bpv.removeKey(i);
                            bpv.setValuesAtTimes(emptyTimes,emptys);
                            for (var i=1 ; i<=bpv.numKeys ; i++) {
                                bpv.setInterpolationTypeAtKey(i,KeyframeInterpolationType.HOLD,KeyframeInterpolationType.HOLD);
                            }
                        }
                        break;
                    case 2:
                        var opa = lyr.transform.opacity;
                            if (opa.numKeys>0) for ( var i=opa.numKeys; i>=1;i--) opa.removeKey(i);
                            for (var i=0; i<emptys.length;i++)
                            {
                                if(emptys[i]==100)
                                {
                                    emptys[i] =0;
                                }else{
                                    emptys[i] =100;
                                }

                            }
                            opa.setValuesAtTimes(emptyTimes,emptys);
                            for (var i=1 ; i<=opa.numKeys ; i++) {
                                opa.setInterpolationTypeAtKey(i,KeyframeInterpolationType.HOLD,KeyframeInterpolationType.HOLD);
                            }
                        break;
                        case 1:
                    default:
                        break;
                }
                if(inOutPoint==true)
                {
                    if(rp.numKeys>2)
                    {
                        var maxV = Math.round(lyr.source.duration*fr);
                        var ff =  Math.round(rp.keyValue(1)*fr);
                        if(ff>=maxV)
                        {
                            lyr.inPoint = rp.keyTime(2);
                        }
                        ff =  Math.round(rp.keyValue(rp.numKeys)*fr);
                        if(ff>=maxV)
                        {
                            lyr.outPoint = rp.keyTime(rp.numKeys);
                        }

                    }
                }else{
                lyr.outPoint = lyr.containingComp.duration;
                }



            }catch(e){
                alert(e.toString());
            }
        }

        var lyrs = getLayer();
       if ((lyrs==null)||(lyrs.length<=0)){
            return;
        }
        app.beginUndoGroup("AE_Remap");

        //コンポの長さを設定
        var cmp = lyrs[0].containingComp;
        var duration = du;
        if (cmp.duration != duration) cmp.duration = duration;



       for (var i=0; i<lyrs.length;i++)
        {
            var times2 =[];
            var values2 =[];
            var emptys = [];
            var emptyTimes = [];
            var lyr = lyrs[i];
            for (var j=0; j<times.length;j++)
            {
                var rp = lyrs[i].property(2);
                var maxV = rp.maxValue;
                var tim = times[j];
                var num = values[j];
                if (num<0) num = maxV;
                else if (num>maxV) num = maxV;
                times2.push(tim);
                values2.push(num);
                if(num>=maxV)
                {
                    emptys.push(100);
                }else{
                    emptys.push(0);
                }
                emptyTimes.push(tim);
            }
            var cnt = emptys.length;
            for (var j = cnt-1; j>=1;j--)
            {
                if (emptys[j-1] == emptys[j]){
                    emptys.splice(j,1);
                    emptyTimes.splice(j,1);
                }
            }
            applySub(lyrs[i],times2,values2,emptys,emptyTimes);
            //lyrs[i].inPoint = 0;
            //lyrs[i].outPoint = duration;
        }
        app.endUndoGroup();
    }
    //btnApply.onClick = applyCells;

	//-------------------------------------------------------------------------
    var getCellLayer = function()
    {
        // ********************************
        function arrayFromMinus(ary)
        {
            var ret = [];
            for (var i=0 ; i<ary.length;i++) ret.push(ary[i]);
            for (var i=1 ; i<ret.length;i++)
            {
                if (ret[i]<0)
                {
                    ret[i] = ret[i-1];
                }
            }
            return ret;

        }
        // ********************************
        function arrayToMinus(ary)
        {
            var ret = [];
            for (var i=0 ; i<ary.length;i++) ret.push(ary[i]);
            for (var i=ret.length-1 ; i>=1;i--)
            {
                if (ret[i]==ret[i-1])
                {
                    ret[i] = -1;
                }
            }
            return ret;

        }
        // ********************************
        function findBD(lyr)
        {
            var ret = null;
            var eg = lyr.property("ADBE Effect Parade");
            if (eg.numProperties<=0) return ret;

            for ( var i=1; i<=eg.numProperties;i++)
            {
                if (eg.property(i).matchName=="ADBE Block Dissolve")
                {
                    if (eg.property(i).active==true)
                    {
                        ret = eg.property(i)(1);
                        break;
                    }
                }
            }
            return ret;
        }
        // ********************************
		var aeremapCall = new File(aeremapCallPath);
		var cmd =  "\"" + aeremapCall.fsName +"\"";
		if (aeremapCall.exists==false){
            alert("ae_reampcallがありません");
            return;
        }
        var r = system.callSystem(cmd + " /exenow");
        r = r.trim().toLowerCase();
        if (r=="false") {
            alert("no process");
            return;
        }

        var lyr = getLayerOne();
        if (lyr==null) {
            return;
        }
        var remap = lyr(2);
        if (remap.numKeys<=0){
            alert("リマップが未入力です");
            return;
        }
        //コマデータを獲得
        var obj = {};
        obj.header = "ardjV2";
        var du = lyr.containingComp.duration;
        var fr = lyr.containingComp.frameRate;
        var maxV = remap.maxValue;
        var maxVF = Math.round(maxV * fr)+1
        obj.frameCount = Math.round(du*fr);
        obj.frameRate = fr;
        var fc = obj.frameCount;

        var cellData = [];
        for (var i =0; i<fc; i++) cellData.push(-1);
        cellData[0] = 0;

        var isFootage = (lyr.source instanceof FootageItem);

        //リマップデータの獲得
        for (var i=1; i<=remap.numKeys;i++)
        {
            var frm = Math.round(remap.keyTime(i)*fr);
            var v = remap.keyValue(i);
            v = Math.round(v * fr)+1;
            if(v>=maxVF) v = 0;
            cellData[frm]= v;
        }
        cellData = arrayFromMinus(cellData);
        //不透明度の獲得
        var opa = lyr(6)(11);
        if (opa.numKeys>=2)
        {
            var opaData = [];
            for (var i =0; i<fc; i++) opaData.push(-1);
            opaData[0] = 100;
            for (var i=1; i<=opa.numKeys;i++)
            {
                var frm = Math.round(opa.keyTime(i)*fr);
                opaData[frm] = opa.keyValue(i);
            }
            opaData = arrayFromMinus(opaData);
            for (var i=0; i<fc;i++)
            {
                if (opaData[i]<=0){
                    cellData[i]=0;
                }
            }
        }
        var bd = findBD(lyr);
        if (bd!=null)
        {
            if (bd.numKeys>=2)
            {
                var bdData = [];
                for (var i =0; i<fc; i++) bdData.push(-1);
                bdData[0] = 0;
                for (var i=1; i<=bd.numKeys;i++)
                {
                    var frm = Math.round(bd.keyTime(i)*fr);
                    var v = bd.keyValue(i);
                    bdData[frm] = v;
                }
                bdData = arrayFromMinus(bdData);
                for (var i=0; i<fc;i++)
                {
                    if (bdData[i]==100){
                        cellData[i]=0;
                    }
                }
            }
        }
        //ardjに変換
        cellData = arrayToMinus(cellData);
        var data=[];
        for (var i =0; i<fc; i++)
        {
            if (cellData[i]>=0)
            {
                var d = [];
                d.push(i);
                d.push(cellData[i]);
                data.push(d);
            }
        }
        obj.cell = data;
        //データを保存
        var gd = "$";
        gd += ","+obj.frameCount.toString();
        gd += ","+obj.frameRate.toString();
        gd += ","+obj.cell.length.toString();
        for(var i=0; i<obj.cell.length;i++)
        {
            gd+=","+obj.cell[i][0].toString();
            gd+="-"+obj.cell[i][1].toString();
        }

        try{
            var cmdline = cmd + " /import_layer " + gd;
            var r = system.callSystem(cmdline);

        }catch(e){
            alert("import_layer call err\r\n" + e.toString());
        }

    }
    winObj.gr.g0.btnGetCellLayer.onClick = getCellLayer;

    //-------------------------------------------------------------------------
	if ( ( me instanceof Panel) == false){
		winObj.center();
		winObj.show();
	}
	//-------------------------------------------------------------------------
})(this);
