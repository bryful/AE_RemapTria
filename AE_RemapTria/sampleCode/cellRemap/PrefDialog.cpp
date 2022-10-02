#include "PrefDialog.h"

PrefDialog::PrefDialog(TSPref pf,QWidget *parent) :
    QDialog(parent)
{
    this->setWindowFlags(Qt::Dialog|Qt::WindowStaysOnTopHint);

    QVBoxLayout *mainVl = new QVBoxLayout;//メインレイアウト
    QVBoxLayout *leftVl = new QVBoxLayout;//メインレイアウト
    QVBoxLayout *rightVl = new QVBoxLayout;//メインレイアウト
    QHBoxLayout *lrhl = new QHBoxLayout;

    //--------------
    QGroupBox *gpDisplay = new QGroupBox(tr("Display"));
    QVBoxLayout *dispVl = new QVBoxLayout;
    QGridLayout *dispGl1 = new QGridLayout;
    QLabel *lb1 = new QLabel(tr("Frame Disp Mode"));
    lb1->setSizePolicy(QSizePolicy::Fixed,QSizePolicy::Fixed);
    dispGl1->addWidget(lb1,0,0);
    cmbFrameDisp = new QComboBox;
    cmbFrameDisp->addItem(tr("Frame"));
    cmbFrameDisp->addItem(tr("SecKoma"));
    cmbFrameDisp->addItem(tr("PageFrame"));
    cmbFrameDisp->addItem(tr("PageSecKoma"));
    dispGl1->addWidget(cmbFrameDisp,0,1);

    QLabel *lb2 = new QLabel(tr("Pae Sec"));
    dispGl1->addWidget(lb2,1,0);
    cmbPageMode = new QComboBox;
    cmbPageMode->addItem(tr("6Sec"));
    cmbPageMode->addItem(tr("3Sec"));
    dispGl1->addWidget(cmbPageMode,1,1);

    dispVl->addLayout(dispGl1);
    cbIs30fps_6 = new QCheckBox(tr("Guide line 6 frame at 30fps"));
    cbIsNoneDeleteInputValue = new QCheckBox(tr("Not initialize numerical on input"));
    cbIsStartZero = new QCheckBox(tr("StartFrame is Zero"));
    cbWheelRev = new QCheckBox(tr("WheelRev"));
    cbIsJsxTemp = new QCheckBox(tr("JsxTemp"));

    dispVl->addWidget(cbIs30fps_6);
    dispVl->addWidget(cbIsNoneDeleteInputValue);
    dispVl->addWidget(cbIsStartZero);
    dispVl->addWidget(cbWheelRev);
    dispVl->addWidget(cbIsJsxTemp);

    gpDisplay->setLayout(dispVl);
    leftVl->addWidget(gpDisplay);
    //--------------
    QGroupBox *gpSize = new QGroupBox(tr("Size"));
    QGridLayout *sizeGl1 = new QGridLayout;
    QLabel *lb3 = new QLabel(tr("CellWidth"));
    lb3->setSizePolicy(QSizePolicy::Fixed,QSizePolicy::Fixed);
    sizeGl1->addWidget(lb3,0,0);
    sizeGl1->addWidget(new QLabel(tr("CellHeight")),1,0);
    sizeGl1->addWidget(new QLabel(tr("CaptionHeight")),2,0);
    sizeGl1->addWidget(new QLabel(tr("FrameWidth")),3,0);
    sbCellWidth     = new QSpinBox;
    sbCellHeight    = new QSpinBox;
    sbCaptionHeight = new QSpinBox;
    sbFrameWidth    = new QSpinBox;
    sizeGl1->addWidget(sbCellWidth,0,1);
    sizeGl1->addWidget(sbCellHeight,1,1);
    sizeGl1->addWidget(sbCaptionHeight,2,1);
    sizeGl1->addWidget(sbFrameWidth,3,1);

    gpSize->setLayout(sizeGl1);
    leftVl->addWidget(gpSize);
    //--------------
    QGroupBox *gpFont = new QGroupBox(tr("Font"));
    QHBoxLayout *hb = new QHBoxLayout;
    btnFont = new QPushButton(tr("Font"));
    btnFont->setDefault(false);
    hb->addWidget(btnFont);
    lbFont = new QLabel(tr("0 1 2 A B C"));
    hb->addWidget(lbFont);
    gpFont->setLayout(hb);
    leftVl->addWidget(gpFont);
    //leftVl->addStretch();
    //--------------
    QGroupBox *gpColor = new QGroupBox(tr("Color"));
    QGridLayout *colorGl = new QGridLayout;
    cbxBase         = new ColorBox;
    cbxEmpty        = new ColorBox;
    cbxEmpty2nd     = new ColorBox;
    cbxCell         = new ColorBox;
    cbxSelection    = new ColorBox;
    cbxCaption      = new ColorBox;
    cbxCaptionSel   = new ColorBox;
    cbxFrame        = new ColorBox;
    cbxFrame2nd     = new ColorBox;
    cbxFrameSel     = new ColorBox;
    cbxLine         = new ColorBox;
    cbxSerial       = new ColorBox;
    cbxText         = new ColorBox;
    cbxInput        = new ColorBox;
    colorGl->addWidget(cbxBase,         0,0);
    colorGl->addWidget(new QLabel(tr("BaseColor")),     0,1);

    colorGl->addWidget(cbxEmpty,        1,0);
    colorGl->addWidget(new QLabel(tr("EmptyCell")),     1,1);

    colorGl->addWidget(cbxEmpty2nd,     2,0);
    colorGl->addWidget(new QLabel(tr("EmptyCell2nd")),  2,1);

    colorGl->addWidget(cbxCell,         3,0);
    colorGl->addWidget(new QLabel(tr("NumberCell")),    3,1);

    colorGl->addWidget(cbxSelection,    4,0);
    colorGl->addWidget(new QLabel(tr("Selection")),     4,1);

    colorGl->addWidget(cbxCaption,      5,0);
    colorGl->addWidget(new QLabel(tr("Caption")),       5,1);

    colorGl->addWidget(cbxCaptionSel,   6,0);
    colorGl->addWidget(new QLabel(tr("CaptionAdtives")),6,1);

    colorGl->addWidget(cbxFrame,        7,0);
    colorGl->addWidget(new QLabel(tr("Frame")),         7,1);

    colorGl->addWidget(cbxFrame2nd,     8,0);
    colorGl->addWidget(new QLabel(tr("Frame2nd")),      8,1);

    colorGl->addWidget(cbxFrameSel,     9,0);
    colorGl->addWidget(new QLabel(tr("FrameActived")),  9,1);

    colorGl->addWidget(cbxLine,         10,0);
    colorGl->addWidget(new QLabel(tr("Line")),          10,1);

    colorGl->addWidget(cbxSerial,       11,0);
    colorGl->addWidget(new QLabel(tr("SerialLine")),    11,1);

    colorGl->addWidget(cbxText,         12,0);
    colorGl->addWidget(new QLabel(tr("NumberText")),    12,1);

    colorGl->addWidget(cbxInput,        13,0);
    colorGl->addWidget(new QLabel(tr("InputArea")),     13,1);

    gpColor->setLayout(colorGl);
    rightVl->addWidget(gpColor);
    //rightVl->addStretch();
    //--------------
    lrhl->addLayout(leftVl);
    lrhl->addLayout(rightVl);

    mainVl->addLayout(lrhl);
    //--------------
    QGroupBox *gpAE = new QGroupBox(tr("AfterFX"));
    QVBoxLayout *aeVl = new QVBoxLayout;
    //QHBoxLayout *hl4 = new QHBoxLayout;
    //btnAddAfterFX = new QPushButton(tr("add AfterFX"));
    //hl4->addWidget(btnAddAfterFX);
    //hl4->addStretch();
    //aeVl->addLayout(hl4);

    cmbAfterFX = new QComboBox;
    aeVl->addWidget(cmbAfterFX);
    QHBoxLayout *aeHl1 = new QHBoxLayout;
    aeHl1->addStretch();
    lbScript = new QLabel(tr("Install Scripts"));
    aeHl1->addWidget(lbScript);
    btnScriptInst = new QPushButton(tr("Inst"));
    aeHl1->addWidget(btnScriptInst);
    btnScriptUninst = new QPushButton(tr("Uninst"));
    aeHl1->addWidget(btnScriptUninst);
    btnInstFolder = new QPushButton(tr("Open InstFolder"));
    aeHl1->addWidget(btnInstFolder);
    aeVl->addLayout(aeHl1);
    QHBoxLayout *aeHl2 = new QHBoxLayout;
    aeHl2->addStretch();
    aeHl2->addWidget(new QLabel(tr("Base Script")));
    btnOpenScrptFld = new QPushButton(tr("Open BaseScripts Folder"));
    aeHl2->addWidget(btnOpenScrptFld);
    aeVl->addLayout(aeHl2);
    gpAE->setLayout(aeVl);
    mainVl->addWidget(gpAE);
    //--------------
    QHBoxLayout *hl2 = new QHBoxLayout;
    btnInit = new QPushButton(tr("Init"));
    hl2->addWidget(btnInit);

    btnOpenPref = new QPushButton(tr("Open Pref"));
    hl2->addWidget(btnOpenPref);


    hl2->addStretch();
    btnCancel = new QPushButton(tr("Cancel"));
    hl2->addWidget(btnCancel);
    btnOK = new QPushButton(tr("OK"));
    btnOK->setDefault(true);
    hl2->addWidget(btnOK);
    mainVl->addStretch();
    mainVl->addLayout(hl2);

    setLayout(mainVl);
    m_pref.assign(pf);
    fromPref();

    connect(btnScriptInst,SIGNAL(clicked()),this,SLOT(instScript()));
    connect(btnScriptUninst,SIGNAL(clicked()),this,SLOT(uninstScript()));
    connect(btnOpenScrptFld,SIGNAL(clicked()),this,SLOT(openScriptFolder()));
    connect(btnInstFolder,SIGNAL(clicked()),this,SLOT(openInstFolder()));
    connect(btnCancel,SIGNAL(clicked()),this,SLOT(reject()));
    connect(btnOK,SIGNAL(clicked()),this,SLOT(btnOKClicked()));
    connect(btnInit,SIGNAL(clicked()),this,SLOT(btnInitClicked()));
    connect(btnOpenPref,SIGNAL(clicked()),this,SLOT(openPrefFolder()));
    connect(btnFont,SIGNAL(clicked()),this,SLOT(btnFontClicked()));
}

PrefDialog::~PrefDialog()
{
}
void PrefDialog::fromPref()
{
    cmbFrameDisp->setCurrentIndex(m_pref.frameDispMode);
    int idx = 0;
    if (m_pref.pageMode() != PageMode::Sec6) idx =1;

    cmbPageMode->setCurrentIndex(idx);

    cbIs30fps_6->setChecked(m_pref.is30fps_6());
    cbIsStartZero->setChecked(m_pref.startFrame == 0);
    cbIsNoneDeleteInputValue->setChecked(m_pref.isNoneDeleteInputValue);
    cbWheelRev->setChecked(m_pref.isWheelRev);
    cbIsJsxTemp->setChecked(m_pref.isJsxTemp());


    sbCellWidth->setValue(m_pref.cellWidth);
    sbCellHeight->setValue(m_pref.cellHeight);
    sbCaptionHeight->setValue(m_pref.captionlHeight);
    sbFrameWidth->setValue(m_pref.frameWidth);


    cbxBase->setColor(m_pref.baseColor);
    cbxEmpty->setColor(m_pref.emptyColor);
    cbxEmpty2nd->setColor(m_pref.emptyColor2nd);
    cbxCell->setColor(m_pref.cellColor);
    cbxSelection->setColor(m_pref.selectionColor);
    cbxCaption->setColor(m_pref.captionColor);
    cbxCaptionSel->setColor(m_pref.captionColorSel);
    cbxFrame->setColor(m_pref.frameColor);
    cbxFrame2nd->setColor(m_pref.frameColor2nd);

    cbxFrameSel->setColor(m_pref.frameColorSel);
    cbxLine->setColor(m_pref.lineColor);
    cbxSerial->setColor(m_pref.serialColor);
    cbxText->setColor(m_pref.textColor);
    cbxInput->setColor(m_pref.inputColor);

    lbFont->setFont(m_pref.font);

    if (m_pref.afterFXList.size()>0)
    {
        int index = 0;
        cmbAfterFX->clear();
        for(int i=0; i<m_pref.afterFXList.size();i++) {
            cmbAfterFX->addItem(m_pref.afterFXList[i]);
            if (m_pref.afterFXList[i] == m_pref.afterFXPath()) index= i;
        }
        cmbAfterFX->setCurrentIndex(index);
    }
    this->update();
}
void PrefDialog::toPref()
{
    m_pref.frameDispMode = cmbFrameDisp->currentIndex();
    int idx = PageMode::Sec6;
    if (cmbPageMode->currentIndex() != 0) idx =PageMode::Sec3;
    m_pref.setPageMode(idx);
    m_pref.setIs30fps_6(cbIs30fps_6->checkState()==Qt::Checked);
    m_pref.isNoneDeleteInputValue = ( cbIsNoneDeleteInputValue->checkState()==Qt::Checked);
    if ( cbIsStartZero->checkState()==Qt::Checked){
        m_pref.startFrame = 0;
    }else{
        m_pref.startFrame = 1;
    }
    m_pref.isWheelRev = (cbWheelRev->checkState() == Qt::Checked);
    m_pref.setIsJsxTemp( (cbIsJsxTemp->checkState() == Qt::Checked));

    m_pref.cellWidth =sbCellWidth->value();
    if (m_pref.cellWidth<8) m_pref.cellWidth = 8;
    m_pref.cellHeight =sbCellHeight->value();
    if (m_pref.cellHeight<8) m_pref.cellHeight = 8;
    m_pref.captionlHeight =sbCaptionHeight->value();
    if (m_pref.captionlHeight<8) m_pref.captionlHeight = 8;
    m_pref.frameWidth =sbFrameWidth->value();
    if (m_pref.frameWidth<8) m_pref.frameWidth = 8;


    m_pref.baseColor        = cbxBase->getColor();
    m_pref.emptyColor       = cbxEmpty->getColor();
    m_pref.emptyColor2nd    = cbxEmpty2nd->getColor();
    m_pref.cellColor        = cbxCell->getColor();
    m_pref.selectionColor   = cbxSelection->getColor();
    m_pref.captionColor     = cbxCaption->getColor();
    m_pref.captionColorSel  = cbxCaptionSel->getColor();
    m_pref.frameColor       = cbxFrame->getColor();
    m_pref.frameColor2nd    = cbxFrame2nd->getColor();
    m_pref.frameColorSel    = cbxFrameSel->getColor();
    m_pref.lineColor        = cbxLine->getColor();
    m_pref.serialColor      = cbxSerial->getColor();
    m_pref.textColor        = cbxText->getColor();
    m_pref.inputColor       = cbxInput->getColor();

    m_pref.font = lbFont->font();

    idx = cmbAfterFX->currentIndex();
    if (idx>=0){
        m_pref.setAfterFXPath(cmbAfterFX->itemText(idx));
    }

}


void PrefDialog::btnFontClicked()
{
    bool ok;
    QFont fnt = QFontDialog::getFont(
                &ok,
                lbFont->font(),
                this);
    if(ok){
        lbFont->setFont(fnt);
        m_pref.font = fnt;
    }
}

void PrefDialog::btnOKClicked()
{
    toPref();
    accept();
}

void PrefDialog::btnInitClicked()
{
    m_pref.init();
    fromPref();
}
//***********************************************************
QString PrefDialog::afterFXFolder()
{
    QString p = "";
    int c =cmbAfterFX->currentIndex();
    if (c<0) return p;
    p = cmbAfterFX->itemText(c);
    int idx = -1;
    for (int i= p.size()-1;i>=0;i--){
        if (p[i]=='/'){
            idx = i;
            break;
        }
    }
    if (idx>=0){
        p = p.left(idx);
    }
    return p;
}
//***********************************************************
void PrefDialog::instScript()
{
    QString p = afterFXFolder();
    TSAEScript ae(0,0,&m_pref);
    QString mes = "";
    if ( ae.instScript(p)){
        mes =(tr("Inst OK!"));
    }else{
        mes =(tr("Inst Error!"));
    }
    lbScript->setText(mes);

}
//***********************************************************
void PrefDialog::uninstScript()
{
    QString p = afterFXFolder();
    TSAEScript ae(0,0,&m_pref);
    QString mes = "";
    if ( ae.uninstScript(p)){
        mes =(tr("UnInst OK!"));
    }else{
        mes =(tr("UnInst Error!"));
    }
    lbScript->setText(mes);

}
//***********************************************************
void PrefDialog::openScriptFolder()
{
    TSAEScript ae;
    ae.openScriptFolder();
}
//***********************************************************
void PrefDialog::openInstFolder()
{
    QString p = afterFXFolder() +"/Scripts";
    QProcess ps;
#if defined(Q_OS_WIN)
    p.replace("/","\\");
    QString d ="EXPLORER ";
    d += " \"" + p + "\"";
    ps.execute( d );
#elif defined(Q_OS_MAC)
    QString cmd = "open ";
    cmd += "\"" + p +"\"";
    ps.execute(cmd);
#endif
}
//***********************************************************
void PrefDialog::openPrefFolder()
{
    m_pref.openPrefFolder();
}

