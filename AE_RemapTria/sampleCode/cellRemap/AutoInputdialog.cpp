#include "AutoInputdialog.h"

//------------------------------------------------
AutoInputDialog::AutoInputDialog(int st,int lst,int len,QWidget *parent) :
    QDialog(parent)
{
    this->setWindowFlags(Qt::Dialog|Qt::WindowStaysOnTopHint);


    QGridLayout *gl = new QGridLayout;
    QLabel *lbStart = new QLabel(tr("start"));
    lbStart->setSizePolicy(QSizePolicy::Fixed,QSizePolicy::Fixed);
    gl->addWidget(lbStart,0,0);
    edStart = new QSpinBox();
    edStart->setMinimum(0);
    edStart->setMaximum(99999);
    gl->addWidget(edStart,0,1);

    gl->addWidget(new QLabel(tr("last")),1,0);
    edLast = new QSpinBox();
    edLast->setMinimum(0);
    edLast->setMaximum(99999);
    gl->addWidget(edLast,1,1);

    gl->addWidget(new QLabel(tr("length")),2,0);
    edLength = new QSpinBox();
    edLength->setMinimum(0);
    edLength->setMaximum(99999);
    gl->addWidget(edLength,2,1);

    QVBoxLayout *vl = new QVBoxLayout;
    vl->addLayout(gl);
    vl->addStretch();

    QHBoxLayout *hl = new QHBoxLayout;
    btnCancel   = new QPushButton(tr("cancel"));
    btnOK       = new QPushButton(tr("OK"));
    btnOK->setDefault(true);
    hl->addStretch();
    hl->addWidget(btnCancel);
    hl->addWidget(btnOK);
    vl->addLayout(hl);

    this->setLayout(vl);

    edStart->setValue(st);
    edLast->setValue(lst);
    edLength->setValue(len);

    connect(btnOK,SIGNAL(clicked()),this,SLOT(accept()));
    connect(btnCancel,SIGNAL(clicked()),this,SLOT(reject()));
}

//------------------------------------------------
AutoInputDialog::~AutoInputDialog()
{
//    delete ui;
}
//------------------------------------------------
int AutoInputDialog::startCellNum()
{
    return edStart->value();
}

//------------------------------------------------
void AutoInputDialog::setStartCellNum(int v)
{
    if (v<1) v=1;
    edStart->setValue(v);

}
//------------------------------------------------
int AutoInputDialog::lastCellNum()
{
    return edLast->value();
}

//------------------------------------------------
void AutoInputDialog::setLastCellNum(int v)
{
    if (v<1) v=1;
    edLast->setValue(v);

}
//------------------------------------------------
int AutoInputDialog::lengthCellNum()
{
    return edLength->value();
}
//------------------------------------------------
void AutoInputDialog::setLengthCellNum(int v)
{
    if (v<1) v=1;
    edLength->setValue(v);
}
//------------------------------------------------
