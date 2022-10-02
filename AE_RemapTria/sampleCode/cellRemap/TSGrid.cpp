#include "TSGrid.h"

//*******************************************************************************
TSGrid::TSGrid(QWidget *parent) :
    QWidget(parent)
{
    TSAEScript::exportJsxScriptALL(true);

    m_AutoInputStart = 1;
    m_AutoInputLast = 10;
    m_AutoInputLength = 3;

    m_calc_value = 0;
    m_calc_mode = 0;

    m_FrameCellAll = true;
    m_FrameCountChange = true;

    m_sel.setTSData(&m_data);
    m_area.setParams(&m_data,&m_pref,this);
    m_ae = new TSAEScript(&m_data,&m_sel,&m_pref);

    isPushSpace = false;
    mdMode = mdModeNormal;

    m_inputValue = "";
    isNoDeleteInputValueSub = false;

    createActions();
    hBar = new QScrollBar(Qt::Horizontal,this);
    hBar->setPageStep(100);
    vBar = new QScrollBar(Qt::Vertical,this);
    vBar->setPageStep(10);


    connect(hBar,SIGNAL(valueChanged(int)),this,SLOT(setDispX(int)));
    connect(vBar,SIGNAL(valueChanged(int)),this,SLOT(setDispY(int)));
    connect(this,SIGNAL(dispXChanged(int)),hBar,SLOT(setValue(int)));
    connect(this,SIGNAL(dispYChanged(int)),vBar,SLOT(setValue(int)));

    connect(m_ae,SIGNAL(FromRemapFinished(bool)),this,SLOT(fromRemapFinshed(bool)));

    setStatus();

    this->setAcceptDrops(true);
    m_data.modiFlag = false;
    dataChanged(infoStr());
}
//*******************************************************************************
TSGrid::~TSGrid()
{
    if (m_mes != 0) delete m_mes;
}

//*******************************************************************************
void TSGrid::setTSSheet(TSSheet *tss)
{
    m_sheet = tss;
}

//*******************************************************************************
void TSGrid::prefToData()
{
    setSize(m_pref.cellCountDef,m_pref.frameCountDef);
    setFrameRate(m_pref.frameRateDef());
}
//*******************************************************************************
void TSGrid::setSize(int c, int f)
{
    if (c<1) c = 1;
    if (f<6) f = 6;
    if ( (m_data.cellCount()!=c)||(m_data.frameCount() !=f)){
        m_data.setSize(c,f);
        m_pref.setSizeDef(c,f);
        setStatus();
        dataChanged(infoStr());
        dataSizeChanged(c ,f);
        this->update();
    }
}
//*******************************************************************************
void TSGrid::setFrameRate(int fps)
{
    if (fps<=0) fps = 24;
    m_data.setFrameRate(fps);
    m_pref.setFrameRateDef(fps);

    dataChanged(infoStr());
    this->update();
}
//*******************************************************************************
void TSGrid::setSheetName(QString nm)
{
    if(m_data.sheetName() != nm){
        m_data.setSheetName( nm);
        dataChanged(infoStr());
    }
}

//*******************************************************************************
QString TSGrid::getSheetName()
{
    return m_data.sheetName();

}
//*******************************************************************************
QString TSGrid::infoStr()
{
    return m_data.infoStr();
}

//*******************************************************************************
void TSGrid::setStatus()
{

    if ((m_data.cellCount()<=0)||(m_data.frameCount()<=0)||(m_area.isEnabled()==false)) return;
    m_area.getStatus();
    if ( hBar != NULL){
        int h = m_area.DispXMax;// +hBar->pageStep();
        hBar->setMinimum(0);
        if ( hBar->maximum() != h) hBar->setMaximum(h);
        if ( hBar->value() > m_area.DispXMax) hBar->setValue(m_area.DispXMax);
        hBar->setEnabled(hBar->maximum()>0);
    }
    if ( vBar != NULL){
        int v = m_area.DispYMax;//+hBar->pageStep();
        vBar->setMinimum(0);
        if ( vBar->maximum() != v) vBar->setMaximum(v);
        if ( vBar->value() > m_area.DispYMax) vBar->setValue(m_area.DispYMax);
        vBar->setEnabled(vBar->maximum()>0);
    }
    this->setMinimumSize(m_area.MinWidth,m_area.MinHeight);
}
//*******************************************************************************
void TSGrid::setDispX(int x)
{
    if (x<0) x = 0;
    else if (x>m_area.DispXMax)x = m_area.DispXMax;
    if (m_area.DispX != x){
        m_area.DispX = x;
        this->update();
        dispXChanged(m_area.DispX);
    }
}
//*******************************************************************************
void TSGrid::setDispY(int y)
{
    if (y<0) y = 0;
    else if (y>m_area.DispYMax) y = m_area.DispYMax;
    if (m_area.DispY != y){
        m_area.DispY = y;
        this->update();
        dispYChanged(m_area.DispY);
    }
}
//*******************************************************************************
void TSGrid::setDispXYDelta(int dx,int dy)
{
    if((dx==0)&&(dy==0)) return;
    int x = m_area.DispX + dx;
    int y = m_area.DispY + dy;
    if (x<0) x = 0;
    else if (x>m_area.DispXMax)x = m_area.DispXMax;
    if (y<0) y = 0;
    else if (y>m_area.DispYMax) y = m_area.DispYMax;
    if ((m_area.DispX != x)||(m_area.DispY != y)){
        m_area.DispY = x;
        m_area.DispY = y;
        this->update();
        dispXChanged(m_area.DispX);
        dispYChanged(m_area.DispY);
    }
}

//*******************************************************************************
 void	TSGrid::resizeEvent(QResizeEvent * )
 {
     if (m_area.isEnabled()){
         setStatus();
         hBar->setGeometry(m_area.hBarRect());
         vBar->setGeometry(m_area.vBarRect());
     }

 }
//*******************************************************************************
 bool TSGrid::inputNum(QString k)
 {
   if (isNoDeleteInputValueSub){
       m_inputValue = k;
       isNoDeleteInputValueSub = false;
        return true;
   }
   bool ok = false;
   int p = m_inputValue.toInt(&ok);
   if (ok==false){
       m_inputValue = "";
   }else if(p<=0){
       m_inputValue = "";
   }else if(p>=9999){
        return false;
   }
   m_inputValue += k;
   return true;
 }
 //*******************************************************************************
  bool TSGrid::selLength(QString k)
  {
      bool b = false;
      if (k.isEmpty()) return b;
        if ((k<"0")||(k>"9")) return b;
        bool ok;
        int v = k.toInt(&ok);
        if (ok){
            if (v==0) v = 10;
            if (m_sel.length() != v){
                m_sel.setLength(v);
                b = true;
            }
        }
        return b;
  }
  //*******************************************************************************
  void TSGrid::setModiFlag()
  {
      if ( m_data.modiFlag == false){
          m_data.modiFlag = true;
          dataChanged(infoStr());
      }
  }

 //*******************************************************************************
 bool TSGrid::inputBS(){

     if (isNoDeleteInputValueSub==true){
         isNoDeleteInputValueSub = false;
         m_inputValue = "";
         return true;
     }else if (m_inputValue.isEmpty()){
         m_sel.setNum(0);
         setModiFlag();
         return true;
     }else{
         m_inputValue = m_inputValue.left(m_inputValue.length()-1);
        return true;
     }
 }

 //*******************************************************************************
 bool TSGrid::inputClear(){
     if (m_inputValue.isEmpty()){
         m_sel.setNum(0);
         setModiFlag();
         return true;
     }else{
         m_inputValue = "";
        return true;
     }
    isNoDeleteInputValueSub = false;
 }

 //*******************************************************************************
 bool TSGrid::inputEnter()
 {
     bool ret = false;
     int v = m_inputValue.toInt(&ret);
     ret = m_sel.setNum(v);

     if (m_pref.isNoneDeleteInputValue){
         isNoDeleteInputValueSub = true;
     }else{
         m_inputValue = "";
     }
     if (ret){
         setModiFlag();
         selMovDown();
     }
    return ret;
 }
 //*******************************************************************************
 bool TSGrid::inputPeriod()
 {
     bool ret = false;
    int v = m_sel.getNumPrev();
    ret = m_sel.setNum(v);
    if (ret){
        setModiFlag();
        selMovDown();
    }
    return ret;
 }
 //*******************************************************************************
 bool TSGrid::inputPluss(){
     bool ret = false;
    int v = m_sel.getNumPrev();
    ret = m_sel.setNum(v+1);
    if (ret){
        setModiFlag();
        selMovDown();
    }
    return ret;
 }

 //*******************************************************************************
 bool TSGrid::inputMinus(){
     bool ret = false;
    int v = m_sel.getNumPrev();
    if (v>1){
        ret = m_sel.setNum(v-1);
    }
    if (ret){
        setModiFlag();
        selMovDown();
    }
    return ret;
 }
 //*******************************************************************************
 bool TSGrid::selMovUp()
 {
     bool ret = m_sel.moveUp();
     if (ret){
         if ( m_sel.last()<=m_area.dispFrameStart()){
            setDispY(m_area.getDispYBottom(m_sel.last()));
            m_area.getDispStatus();
         }
     }
     return ret;
 }
 //*******************************************************************************
 bool TSGrid::selMovDown()
 {
     bool ret = m_sel.moveDown();
     if (ret){
         if ( m_sel.start()>=m_area.dispFrameLast()){
            setDispY(m_area.getDispYTop( m_sel.start()));
            m_area.getDispStatus();
         }
     }
     return ret;
 }
 //*******************************************************************************
 bool TSGrid::scrolDown()
 {
     bool ret = m_sel.down(1);
     if (ret){
         int v = m_area.DispY + m_area.CellHeight;
         if (v>m_area.DispYMax)v =m_area.DispYMax;
         if (v !=m_area.DispY){
            setDispY(v);
            m_area.getDispStatus();
         }
     }
     return ret;
 }
 //*******************************************************************************
 bool TSGrid::scrolUp()
 {
     bool ret = m_sel.up(1);
     if (ret){
         int v = m_area.DispY - m_area.CellHeight;
         if (v<0)v = 0;
         if (v !=m_area.DispY){
            setDispY(v);
            m_area.getDispStatus();
         }
     }
     return ret;
 }
 //*******************************************************************************
 bool TSGrid::selMovLeft()
 {
     bool ret = m_sel.moveLeft();
     if (ret){
         if ( m_sel.targetCell()<=m_area.dispCellStart()){
            setDispX(m_area.getDispXLeft(m_sel.targetCell()));
            m_area.getDispStatus();
         }
     }
     return ret;
 }
 //*******************************************************************************
 bool TSGrid::selMovRight()
 {
     bool ret = m_sel.moveRight();
     if (ret){
         if ( m_sel.targetCell()>=m_area.dispCellLast()){
            setDispX(m_area.getDispXRight(m_sel.targetCell()));
            m_area.getDispStatus();
         }
     }
     return ret;
 }
 //*******************************************************************************
 bool TSGrid::selTailPluss(){
     bool ret = m_sel.tailPluss();
     if (ret){
         if ( m_sel.last()>=m_area.dispFrameLast()){
            setDispX(m_area.getDispYBottom(m_sel.last()));
            m_area.getDispStatus();
         }
     }
     return ret;
 }
 //*******************************************************************************
 bool TSGrid::pageDown()
 {
    int v = m_area.dispFrameLength() * m_area.CellHeight + m_area.DispY;
    if (v>m_area.DispYMax) v=m_area.DispYMax;

    if (m_area.DispY != v){
        setDispY(v);
        return true;
    }else{
        return false;
    }

 }
 //*******************************************************************************
 bool TSGrid::pageUp()
 {
    int v = m_area.DispY - m_area.dispFrameLength() * m_area.CellHeight;
    if (v<0) v=0;

    if (m_area.DispY != v){
        setDispY(v);
        return true;
    }else{
        return false;
    }

 }
 //*******************************************************************************
 bool TSGrid::gotoTop()
 {
    if (m_area.DispY > 0){
        setDispY(0);
        return true;
    }else{
        return false;
    }

 }
 //*******************************************************************************
 bool TSGrid::gotoEnd()
 {
    if (m_area.DispY < m_area.DispYMax){
        setDispY(m_area.DispYMax);
        return true;
    }else{
        return false;
    }

 }

 //*******************************************************************************
 bool TSGrid::selTailMinus(){
     bool ret = m_sel.tailMinus();
     return ret;
 }
 //*******************************************************************************
 bool TSGrid::selToEnd(){
     bool ret = m_sel.toEnd();
     return ret;
 }
 //*******************************************************************************
 bool TSGrid::selALL(){
     bool ret = m_sel.aLL();
     return ret;
 }
 //*******************************************************************************
 void TSGrid::copy()
 {
     m_sel.copy();
 }
 //*******************************************************************************
void TSGrid::cut()
 {
    if ( m_sel.cut() ) {
        dataChanged(infoStr());
        this->update();
    }
 }
 //*******************************************************************************
 void TSGrid::paste()
 {
     if (m_sel.paste()) {
         dataChanged(infoStr());
         this->update();
     }
 }
 //*******************************************************************************
 void TSGrid::wheelEvent(QWheelEvent *e)
 {
     wheelExec(e);
 }
 //*******************************************************************************
void TSGrid:: wheelExec(QWheelEvent *e)
{
    QPoint numPixels = e->pixelDelta();
    QPoint numDegrees = e->angleDelta();

   if (!numPixels.isNull()) {
       if (m_pref.isWheelRev ) numPixels *= -1;
       setDispXYDelta(numPixels.x(),numPixels.y());
   }else if (!numDegrees.isNull()) {
       QPoint numSteps = numDegrees;
       if (m_pref.isWheelRev ) {
           numSteps *= -1;

       }
       setDispXYDelta(numSteps.x(),numSteps.y());
   }
   e->accept();
}
 //*******************************************************************************
 void TSGrid::setStartZero(bool b)
 {
     if ( (m_pref.startFrame==0) != b){
         if (b){
             m_pref.startFrame = 0;
         }else{
             m_pref.startFrame = 1;
         }
     }
     this->update();
 }

//*******************************************************************************
bool TSGrid::toClip()
{
    return m_data.toClipBoard();
}
//*******************************************************************************
bool TSGrid::fromClip()
{
    bool ret = m_data.fromClipBoard();
    if (ret){
        if (! m_data.afterFXPath().isEmpty()){
            if (m_data.afterFXPath() != m_pref.afterFXPath()){
                m_pref.setAfterFXPath(m_data.afterFXPath());
            }
        }
        dataChanged(infoStr());
        setStatus();
        dataChanged(infoStr());
        this->update();
    }
    return ret;
}
//*******************************************************************************
void TSGrid::dragEnterEvent(QDragEnterEvent *e)
{
    if (e->mimeData()->hasFormat("text/uri-list")){
        e->acceptProposedAction();
    }
}
//*******************************************************************************
void TSGrid::dropEvent(QDropEvent *e)
{
    QList <QUrl> urls = e->mimeData()->urls();
    if (urls.isEmpty()) return;
    for (int i=0; i<urls.size();i++){
        QFile f(urls[i].toLocalFile());
        QFileInfo fi(f);
        QString e = fi.suffix().toLower();
        if ((e=="ardj")||(e=="ard")||(e=="xps")||(e=="tsh")||(e=="sts")){
            if (load(urls[i].toLocalFile())){
                this->update();
                break;
            }
        }
    }

}

//*******************************************************************************
//private string m_senScriptCodeXMP = "try{XMPSHEET.importFile(\"$path\",true);}catch(e){alert(e.toString());}";


//*******************************************************************************
void TSGrid::setNavBar(bool b)
{
    if (m_pref.isNavBar != b){
        m_pref.isNavBar = b;
        navBarChanged(b);
    }
}
//*******************************************************************************
void TSGrid::setStayOn(bool b)
{
    if (m_pref.isStayOn != b){
        m_pref.isStayOn = b;
        stayOnChanged(b);
    }
}
//*******************************************************************************
void TSGrid::calc(int value,int mode)
{
    m_sel.calc(value,mode);
}
//*******************************************************************************
