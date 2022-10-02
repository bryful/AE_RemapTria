#include "SheetDialog.h"

//******************************************************************
SheetDialog::SheetDialog(TSData d, QWidget *parent) :
    QDialog(parent)//,ui(new Ui::SheetDialog)
{
    this->setWindowFlags(Qt::Dialog|Qt::WindowStaysOnTopHint);

    QVBoxLayout *vl = new QVBoxLayout;

    QGridLayout *gl1 = new QGridLayout;

    gl1->addWidget(new QLabel(tr("SheetName")),1,0);
    edSheetName = new QLineEdit;
    gl1->addWidget(edSheetName,1,1);

    gl1->addWidget(new QLabel(tr("CellCount")),2,0);
    sbCellCount = new QSpinBox;
    gl1->addWidget(sbCellCount,2,1);

    gl1->addWidget(new QLabel(tr("FrameRate")),3,0);
    cmbFrameRate = new QComboBox;
    cmbFrameRate->addItem(tr("12fps"));
    cmbFrameRate->addItem(tr("15fps"));
    cmbFrameRate->addItem(tr("24fps"));
    cmbFrameRate->addItem(tr("30fps"));
    gl1->addWidget(cmbFrameRate,3,1);

    QGroupBox *gb1 = new QGroupBox(tr("Sheet"));
    gb1->setLayout(gl1);

    vl->addWidget(gb1);

    QGridLayout *gl2 = new QGridLayout;

    QLabel *fc = new QLabel(tr("FrameCount"));
    fc->setSizePolicy(QSizePolicy::Fixed,QSizePolicy::Fixed);
    gl2->addWidget(fc ,1,0);
    sbFrameCount = new QSpinBox;
    sbFrameCount->setMinimum(1);
    sbFrameCount->setMaximum(99999);
    gl2->addWidget(sbFrameCount,1,1);

    gl2->addWidget(new QLabel(tr("SecKoma")),2,0);

    QHBoxLayout *hb1 = new QHBoxLayout;
    sbSec = new QSpinBox;
    sbSec->setMinimum(0);
    sbSec->setMaximum(99999);
    hb1->addWidget(sbSec);
    QLabel *pls = new QLabel(tr("+"));
    pls->setSizePolicy(QSizePolicy::Fixed,QSizePolicy::Preferred);
    hb1->addWidget(pls);
    sbKoma = new QSpinBox;
    sbKoma->setMinimum(0);
    sbKoma->setMaximum(99999);
    hb1->addWidget(sbKoma);
    gl2->addLayout(hb1,2,1);

    QGroupBox *gb2 = new QGroupBox(tr("Duration"));
    gb2->setLayout(gl2);

    vl->addWidget(gb2);
    vl->addStretch();

    QHBoxLayout *hb2 = new QHBoxLayout;
    hb2->addStretch();
    btnCancel = new QPushButton(tr("Cancel"));
    btnOK = new QPushButton(tr("OK"));
    btnOK->setDefault(true);
    hb2->addWidget(btnCancel);
    hb2->addWidget(btnOK);
    vl->addLayout(hb2);

    this->setLayout(vl);

    chgFlg = false;

    m_FrameRate = d.frameRate();
    m_FrameCount = d.frameCount();
    m_CellCount = d.cellCount();
    m_SheetName = d.sheetName();


    connect(sbFrameCount,SIGNAL(valueChanged(int)),this,SLOT(sbFrameCountChanged(int)));

    connect(sbSec,SIGNAL(valueChanged(int)),this,SLOT(sbSecChanged(int)));
    connect(sbKoma,SIGNAL(valueChanged(int)),this,SLOT(sbKomaChanged(int)));

    connect(cmbFrameRate,SIGNAL(currentIndexChanged(int)),this,SLOT(cmbFrameRatecurrentIndexChanged(int)));

    connect(sbCellCount,SIGNAL(valueChanged(int)),this,SLOT(sbCellCountChanged(int)));
    connect(edSheetName,SIGNAL(textChanged(QString)),this,SLOT(edSheetNameTextChanged(QString)));

    connect(btnOK,SIGNAL(clicked()),this,SLOT(accept()));
    connect(btnCancel,SIGNAL(clicked()),this,SLOT(reject()));


    edSheetName->setText(m_SheetName);
    dataTocmbFrameRate();
    sbFrameCount->setValue(m_FrameCount);
    frameToSecKoma();
    sbCellCount->setValue(m_CellCount);   enabledChk();
}
//******************************************************************
SheetDialog::~SheetDialog()
{
}
//******************************************************************
void SheetDialog::enabledChk()
{
     btnOK->setEnabled( (m_FrameCount>0)&&(m_CellCount>0)&&(m_SheetName.isEmpty()==false));
}
//******************************************************************
void SheetDialog::cmbFrameRateToData(int v)
{
    int fps = 24;
    switch(v){
        case 0:fps =12;break;
        case 1:fps =15;break;
        case 2:fps =24;break;
        case 3:fps =30;break;
    }
    if (m_FrameRate != fps){
        m_FrameRate = fps;
        frameToSecKoma();
    }
}

//******************************************************************

void SheetDialog::dataTocmbFrameRate()
{
    int index = 2;
    switch(m_FrameRate){
        case 12:index =0;break;
        case 15:index =1;break;
        case 24:index =2;break;
        case 30:index =3;break;
    }
    cmbFrameRate->setCurrentIndex(index);
}

//******************************************************************

void SheetDialog::secKomaToFrame()
{
    if(chgFlg) return;
    chgFlg = true;
    int sec = sbSec->value();
    if (sec<0) sec =0;

    int koma = sbKoma->value();
    if (koma<0) koma =0;

    int f = sec * m_FrameRate + koma;
    if (f != sbFrameCount->value()) {
        sbFrameCount->setValue(f);
        m_FrameCount = f;
    }
    enabledChk();
    chgFlg = false;
}
//******************************************************************
void SheetDialog::frameToSecKoma()
{
    if(chgFlg) return;
    chgFlg = true;
    int sec = m_FrameCount / m_FrameRate;
    if (sec<0) sec = 0;
    if (sec != sbSec->value()) sbSec->setValue(sec);
    int k = m_FrameCount % m_FrameRate;
    if (k != sbKoma->value()) sbKoma->setValue(k);

    enabledChk();
    chgFlg = false;
}
//******************************************************************

void SheetDialog::sbFrameCountChanged(int arg1)
{
    if (arg1<0)arg1 = 0;
    if (m_FrameCount != arg1){
        m_FrameCount = arg1;
        frameToSecKoma();
    }

}
//******************************************************************

void SheetDialog::sbSecChanged(int)
{
    secKomaToFrame();
}
//******************************************************************
void SheetDialog::sbKomaChanged(int)
{
    secKomaToFrame();
}
//******************************************************************
void SheetDialog::cmbFrameRatecurrentIndexChanged(int index)
{
    cmbFrameRateToData(index);
}
//******************************************************************
void SheetDialog::sbCellCountChanged(int arg1)
{
    if(chgFlg) return;
    chgFlg = true;
    if (arg1<=0) arg1=0;
    if ( m_CellCount != arg1){
        m_CellCount = arg1;
    }
    enabledChk();
    chgFlg = false;
}
//******************************************************************
void SheetDialog::edSheetNameTextChanged(const QString &arg1)
{
    m_SheetName = arg1.trimmed();
    enabledChk();

}
//******************************************************************
