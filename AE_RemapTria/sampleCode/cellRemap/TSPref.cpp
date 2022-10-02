#include "TSPref.h"

//---------------------------------------------------------------------
TSPref::TSPref()
{
    clear();
    m_appData = QStandardPaths::writableLocation(QStandardPaths::DataLocation);
}

//---------------------------------------------------------------------
void TSPref::clear()
{
    cellCountDef = 6;
    frameCountDef = 72;
    m_FrameRateDef = 24;
    m_HorGuide = 6;

    init();

    m_PageCount     = m_PageMode * m_FrameRateDef;

    m_AfterFXPath   =  "";
    findAfterFX();
    if ( (m_AfterFXPath.isEmpty() )&&(afterFXList.size()>0)){
        m_AfterFXPath = afterFXList[0];
    }
    winPos.setX(0);
    winPos.setY(0);

    winSize.setWidth(0);
    winSize.setHeight(0);
    isStayOn    = false;
    isNavBar    = true;
    m_isJsxTemp   = true;
}
//---------------------------------------------------------------------
void TSPref::init()
{

    cellWidth       = 30;
    cellHeight      = 16;
    captionlHeight  = 20;
    frameWidth      = 66;

    frameInter      = 3;
    captionInter    = 3;

    baseColor       = QColor(128,128,128);
    emptyColor      = QColor(200,200,200);
    emptyColor2nd   = QColor(220,220,220);
    cellColor       = QColor(250,250,250);
    selectionColor  = QColor(255,128,128);
    captionColor    = QColor(177,195,210);
    captionColorSel = QColor(255,150,150);
    frameColor      = QColor(190,190,210);
    frameColor2nd   = QColor(220,220,240);
    frameColorSel   = QColor(255,160,160);
    lineColor       = QColor(0,0,0);
    serialColor     = QColor(80,80,80);
    textColor       = QColor(0,0,0);
    inputColor      = QColor(250,250,250);

    startFrame      = 1;
    m_Is30fps_6     = true;
    isNoneDeleteInputValue = false;
    frameDispMode   = FrameDisp::PageFrame;
    m_PageMode      = PageMode::Sec6;
    m_PageCount     = m_PageMode * m_FrameRateDef;
#if defined(Q_OS_WIN)
    font            = QFont("ＭＳ ゴシック",10,QFont::Bold);
#elif defined(Q_OS_MAC)
    font            = QFont("Osaka",10,QFont::Bold);
#endif
    isWheelRev      = false;

}
//---------------------------------------------------------------------
void TSPref::assign(TSPref pf)
{
    cellCountDef    = pf.cellCountDef;
    frameCountDef   = pf.frameCountDef;
    m_FrameRateDef  = pf.m_FrameRateDef;
    m_HorGuide      = pf.m_HorGuide;

    cellWidth       = pf.cellWidth;
    cellHeight      = pf.cellHeight;
    captionlHeight  = pf.captionlHeight;
    frameWidth      = pf.frameWidth;

    frameInter      = pf.frameInter;
    captionInter    = pf.captionInter;

    baseColor       = pf.baseColor;
    emptyColor      = pf.emptyColor;
    emptyColor2nd   = pf.emptyColor2nd;
    cellColor       = pf.cellColor;
    selectionColor  = pf.selectionColor;
    captionColor    = pf.captionColor;
    captionColorSel = pf.captionColorSel;
    frameColor      = pf.frameColor;
    frameColor2nd   = pf.frameColor2nd;
    frameColorSel   = pf.frameColorSel;
    lineColor       = pf.lineColor;
    serialColor     = pf.serialColor;
    textColor       = pf.textColor;
    inputColor      = pf.inputColor;

    startFrame     = pf.startFrame;
    m_Is30fps_6     = pf.m_Is30fps_6;
    isNoneDeleteInputValue = pf.isNoneDeleteInputValue;
    frameDispMode   = pf.frameDispMode;
    m_PageMode        = pf.m_PageMode;
    m_PageCount     = pf.m_PageCount;

    font            = pf.font;

    m_AfterFXPath   =  pf.m_AfterFXPath;
    afterFXList     = pf.afterFXList;

    winPos          = pf.winPos;
    winSize         = pf.winSize;
    isStayOn        = pf.isStayOn;
    isNavBar        = pf.isNavBar;
    isWheelRev      = pf.isWheelRev;
    m_isJsxTemp     = pf.m_isJsxTemp;

}
//---------------------------------------------------------------------
void TSPref::setFrameRateDef(int fps){
    m_FrameRateDef = fps;
    if ( (fps % 6)==0){
        m_HorGuide = 6;
    }else if ( (fps % 5)==0){
        m_HorGuide = 5;
    }
    if (( m_Is30fps_6)&&(fps==30)){
         m_HorGuide = 6;
    }
    m_PageCount = m_PageMode * m_FrameRateDef;
}
//---------------------------------------------------------------------
void TSPref::setPageMode(int mode)
{
    m_PageMode = mode;
    if ((m_PageMode!=PageMode::Sec6)&&(m_PageMode!=PageMode::Sec3)){
        m_PageMode = PageMode::Sec6;
    }
    m_PageCount = m_PageMode * m_FrameRateDef;
}
//---------------------------------------------------------------------
void TSPref::setSizeDef(int c,int f)
{
    cellCountDef = c;
    frameCountDef = f;
}
//---------------------------------------------------------------------
bool TSPref::setIs30fps_6(bool b){
    m_Is30fps_6 = b;
    if ( (m_FrameRateDef % 6)==0){
        m_HorGuide = 6;
    }else if ( (m_FrameRateDef % 5)==0){
        m_HorGuide = 5;
    }
    if (( m_Is30fps_6)&&(m_FrameRateDef==30)){
         m_HorGuide = 6;
    }
    return true;
}


//---------------------------------------------------------------------
bool TSPref::existsAfterFX(QString p)
{
#if defined(Q_OS_MAC)
    QDir f(p);
#elif defined(Q_OS_WIN32)
    QFile f(p);
#endif
    return f.exists();
}

//---------------------------------------------------------------------
void TSPref::setAfterFXPath(QString p)
{
    if (existsAfterFX(p)){
        m_AfterFXPath = p;
        addAfterFXList(p);
    }
}
//---------------------------------------------------------------------
QString TSPref::yenToRoot(QString s)
{
    QString ret = "";
    if ( s.isEmpty()) return ret;
    for (int i=0;i<s.size();i++) {
        if ( s[i]=='\\'){
            ret +="/";
        }else{
            ret += s[i];
        }
    }
    return ret;
}

//---------------------------------------------------------------------
void TSPref::chkAfterFXList()
{
    int cnt = afterFXList.length();
    if (cnt<=0) return;

    afterFXList.sort();

    if (cnt>1){
        for ( int i=cnt-1; i>0; i--){
            if (afterFXList[i-1]==afterFXList[i] ){
                afterFXList.removeAt(i);
            }
        }
    }
    for ( int i=cnt-1; i>=0; i--){
        QFile f(afterFXList[i]);
        if (f.exists()==false){
            afterFXList.removeAt(i);
        }
    }
}
//---------------------------------------------------------------------
void TSPref::addAfterFXList(QString p)
{

    if (existsAfterFX(p)==false) return;
#if defined(Q_OS_WIN32)
    p = yenToRoot(p);
#endif
    int cnt = afterFXList.size();
    if ( cnt<=0){
        afterFXList.push_back(p);
    }else{
        int idx = -1;
        for ( int i=0;i<cnt;i++){
            if (afterFXList[i]==p){
                idx = i;
                break;
            }
        }
        if ( idx<0)  {
            afterFXList.push_back(p);
        }
    }
}

//---------------------------------------------------------------------
void TSPref::findAfterFX()
{
#if  defined(Q_OS_WIN32)
    QStringList ap;
    ap.append("C:/Program Files (x86)/Adobe");
    ap.append("C:/Program Files/Adobe");
    QString spt = "Support Files";
    QString afxName = "AfterFX.exe";
    QStringList fldName;
    fldName.append("Adobe After Effects CS3");
    fldName.append("Adobe After Effects CS4");
    fldName.append("Adobe After Effects CS5");
    fldName.append("Adobe After Effects CS5.5");
    fldName.append("Adobe After Effects CS6");
    fldName.append("Adobe After Effects CS6.5");
    fldName.append("Adobe After Effects CS7");

    chkAfterFXList();
    for ( int j=0; j<fldName.size();j++){
        for ( int i=0; i<ap.length();i++){
            QString p = ap[i] + "/"  + fldName[j] + "/" + spt +"/"+ afxName;
            QFile f(p);
            if (f.exists()){
                addAfterFXList(p);
            }
        }
    }
#elif  defined(Q_OS_MAC)
   QString ap ="/Applications";
    QStringList cs;
    cs.append("Adobe After Effects CS4");
    cs.append("Adobe After Effects CS5");
    cs.append("Adobe After Effects CS5.5");
    cs.append("Adobe After Effects CS6");
    cs.append("Adobe After Effects CS6.5");
    cs.append("Adobe After Effects CS7");
    cs.append("Adobe After Effects CS3");
    QString ext =".app";
    chkAfterFXList();
    for ( int i=0; i<cs.size();i++){
        QString p = ap + "/" +cs[i] + "/" + cs[i] +ext;
        QDir f(p);
        if (f.exists()){
            addAfterFXList(p);
        }
    }
#endif
}
//***********************************************************************
QString TSPref::afterFXName()
{
    QString ret = "";
    if ( m_AfterFXPath.isNull()) return ret;
    if(! m_AfterFXPath.isEmpty()){
        int idx = -1;
        for (int i=m_AfterFXPath.size()-1; i >=0; i--){
            if (m_AfterFXPath[i] == '/') {
                idx = i;
                break;
            }
        }
        if (idx>=0){
            ret = m_AfterFXPath.mid(idx+1);
        }
    }
    return ret;
}
//***********************************************************************
void TSPref::prefSave()
{
    QJsonObject jo;

    jo["cellCountDef"]      = cellCountDef;
    jo["frameCountDef"]     = frameCountDef;
    jo["frameRateDef"]      = m_FrameRateDef;

    jo["cellWidth"]         = cellWidth;
    jo["cellHeight"]        = cellHeight;
    jo["captionlHeight"]    = captionlHeight;
    jo["frameWidth"]        = frameWidth;
    jo["frameInter"]        = frameInter;
    jo["captionInter"]      = captionInter;

    jo["baseColor"]         = FsU::colToJa(baseColor);
    jo["emptyColor"]        = FsU::colToJa(emptyColor);
    jo["emptyColor2nd"]     = FsU::colToJa(emptyColor2nd);
    jo["cellColor"]         = FsU::colToJa(cellColor);
    jo["selectionColor"]    = FsU::colToJa(selectionColor);
    jo["captionColor"]      = FsU::colToJa(captionColor);
    jo["captionColorSel"]   = FsU::colToJa(captionColorSel);
    jo["frameColor"]        = FsU::colToJa(frameColor);
    jo["frameColor2nd"]     = FsU::colToJa(frameColor2nd);
    jo["frameColorSel"]     = FsU::colToJa(frameColorSel);
    jo["lineColor"]         = FsU::colToJa(lineColor);
    jo["serialColor"]       = FsU::colToJa(serialColor);
    jo["textColor"]         = FsU::colToJa(textColor);
    jo["inputColor"]        = FsU::colToJa(inputColor);

    jo["startFrame"]        = startFrame;
    jo["is30fps_6"]         = m_Is30fps_6;
    jo["isNoneDeleteInputValue"]        = isNoneDeleteInputValue;
    jo["frameDispMode"]     = frameDispMode;
    jo["pageMode"]          = m_PageMode;
    jo["font"]              = font.toString();

    if (afterFXList.size()<=0){
        findAfterFX();
    }
    if ( (m_AfterFXPath.isEmpty() )&&(afterFXList.size()>0)){
        m_AfterFXPath = afterFXList[0];
    }
    jo["AfterFXPath"]       = m_AfterFXPath;
    jo["AfterFXList"]      = FsU::aeListToJa(afterFXList);

    jo["WinPosX"]          = winPos.x();
    jo["WinPosY"]          = winPos.y();
    jo["WinSizeX"]         = winSize.width();
    jo["WinSizeY"]         = winSize.height();
    jo["isStayOn"]         = isStayOn;
    jo["isNavBar"]         = isNavBar;
    jo["isWheelRev"]       = isWheelRev;
    jo["isJsxTemp"]        = m_isJsxTemp;

    QJsonDocument jd(jo);
    QByteArray js = jd.toJson();

    QFile f(m_appData+"/" + QApplication::applicationName()+".pref");
    if (f.open(QFile::WriteOnly)){
        QTextStream out(&f);
        out.setCodec("UTF-8");
        out << js;
        f.close();
    }

}
//***********************************************************************
void TSPref::prefLoad()
{
    QFile f(m_appData+"/" + QApplication::applicationName()+".pref");
    //qDebug() << m_appData+"/" + QApplication::applicationName()+".pref";
    if ( f.exists()==false) return;
    QByteArray js = "";
    if (! f.open(QIODevice::ReadOnly | QIODevice::Text)) return;
    js = f.readAll();
    f.close();
    if (js.isEmpty()) return;

    if (js.isEmpty() ) return;
    QJsonDocument jd = QJsonDocument::fromJson(js);
    if ( (js.isEmpty())||(js.isNull())) return;

    QJsonObject jo = jd.object();

    if (! jo["cellCountDef"].isUndefined())     {
        cellCountDef = (int)jo["cellCountDef"].toDouble();
    }
    if (! jo["frameCountDef"].isUndefined()){

        frameCountDef = (int)jo["frameCountDef"].toDouble();
    }
    if (! jo["frameRateDef"].isUndefined()){
        m_FrameRateDef = (int)(jo["frameRateDef"].toDouble());
    }

    if (! jo["cellWidth"].isUndefined())        cellWidth = (int)jo["cellWidth"].toDouble();
    if (! jo["cellHeight"].isUndefined())       cellHeight = (int)jo["cellHeight"].toDouble();
    if (! jo["captionlHeight"].isUndefined())   captionlHeight = (int)jo["captionlHeight"].toDouble();
    if (! jo["frameWidth"].isUndefined())       frameWidth = (int)jo["frameWidth"].toDouble();
    if (! jo["frameInter"].isUndefined())       frameInter = (int)jo["frameInter"].toDouble();
    if (! jo["captionInter"].isUndefined())     captionInter = (int)jo["captionInter"].toDouble();

    if (! jo["baseColor"].isUndefined())       baseColor = FsU::jaToCol(jo["baseColor"],QColor(128,128,128));
    if (! jo["emptyColor"].isUndefined())      emptyColor = FsU::jaToCol(jo["emptyColor"],QColor(200,200,200));
    if (! jo["emptyColor2nd"].isUndefined())   emptyColor2nd = FsU::jaToCol(jo["emptyColor2nd"],QColor(220,220,220));
    if (! jo["cellColor"].isUndefined())       cellColor = FsU::jaToCol(jo["cellColor"],QColor(250,250,250));
    if (! jo["selectionColor"].isUndefined())  selectionColor = FsU::jaToCol(jo["selectionColor"],QColor(255,128,128));
    if (! jo["captionColor"].isUndefined())    captionColor = FsU::jaToCol(jo["captionColor"],QColor(177,195,210));
    if (! jo["captionColorSel"].isUndefined()) captionColorSel = FsU::jaToCol(jo["captionColorSel"],QColor(255,150,150));
    if (! jo["frameColor"].isUndefined())      frameColor = FsU::jaToCol(jo["frameColor"],QColor(190,190,210));
    if (! jo["frameColor2nd"].isUndefined())   frameColor2nd = FsU::jaToCol(jo["frameColor2nd"],QColor(220,220,240));
    if (! jo["frameColorSel"].isUndefined())   frameColorSel = FsU::jaToCol(jo["frameColorSel"],QColor(255,160,160));
    if (! jo["lineColor"].isUndefined())       lineColor = FsU::jaToCol(jo["lineColor"],QColor(0,0,0));
    if (! jo["serialColor"].isUndefined())     serialColor = FsU::jaToCol(jo["serialColor"],QColor(80,80,80));
    if (! jo["textColor"].isUndefined())       textColor = FsU::jaToCol(jo["textColor"],QColor(0,0,0));
    if (! jo["inputColor"].isUndefined())      inputColor = FsU::jaToCol(jo["inputColor"],QColor(240,240,240));

    if (! jo["startFrame"].isUndefined())     startFrame = (int)jo["startFrame"].toDouble();
    if (! jo["is30fps_6"].isUndefined())      m_Is30fps_6 = (int)jo["is30fps_6"].toBool();
    if (! jo["isNoneDeleteInputValue"].isUndefined())      isNoneDeleteInputValue = jo["isNoneDeleteInputValue"].toBool();
    if (! jo["frameDispMode"].isUndefined())      frameDispMode = (int)jo["frameDispMode"].toDouble();
    if (! jo["pageMode"].isUndefined())      m_PageMode = (int)jo["pageMode"].toDouble();
    if (! jo["font"].isUndefined())      font.fromString(jo["font"].toString());

    if (! jo["AfterFXPath"].isUndefined())      m_AfterFXPath = jo["AfterFXPath"].toString();
    if (! jo["AfterFXList"].isUndefined())      afterFXList = FsU::jaToAeList(jo["AfterFXList"]);
    if (afterFXList.size()<=0){
        findAfterFX();
    }
    if ( (m_AfterFXPath.isEmpty() )&&(afterFXList.size()>0)){
        m_AfterFXPath = afterFXList[0];
    }

    int w = 0;
    int h = 0;
    int x = 0;
    int y = 0;
    if (! jo["WinPosX"].isUndefined())    x = (int)jo["WinPosX"].toDouble();
    if (! jo["WinPosY"].isUndefined())    y = (int)jo["WinPosY"].toDouble();
    winPos.setX(x);
    winPos.setY(y);
    if (! jo["WinSizeX"].isUndefined())   w = (int)jo["WinSizeX"].toDouble();
    if (! jo["WinSizeY"].isUndefined())   h = (int)jo["WinSizeY"].toDouble();
    winSize.setWidth(w);
    winSize.setHeight(h);


    if (! jo["isStayOn"].isUndefined())      isStayOn = jo["isStayOn"].toBool();
    if (! jo["isNavBar"].isUndefined())      isNavBar = jo["isNavBar"].toBool();
    if (! jo["isWheelRev"].isUndefined())    isWheelRev = jo["isWheelRev"].toBool();
    if (! jo["isJsxTemp"].isUndefined())     m_isJsxTemp = jo["isJsxTemp"].toBool();
}

//***********************************************************************
void TSPref::openPrefFolder()
{
    QProcess ps;
    QString p= m_appData;
#if defined(Q_OS_WIN)
    p.replace("/","\\");
    QString d ="EXPLORER ";
    d += " \"" +p + "\"";
    ps.execute( d );
#elif defined(Q_OS_MAC)
    QString cmd = "open ";
    cmd += "\"" + p +"\"";
    ps.execute(cmd);
#endif
}
//*********************************************************************************************
void TSPref::setIsJsxTemp(bool b)
{
    m_isJsxTemp = b;
}

//*********************************************************************************************
