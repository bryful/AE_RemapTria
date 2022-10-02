#include "CalcDialog.h"

//****************************************************************************
CalcDialog::CalcDialog(QWidget *parent) :
    QDialog(parent)
{
    QVBoxLayout *vl = new QVBoxLayout;

    vl->addWidget(new QLabel(tr("Calc Input")));

    edValue = new QSpinBox;
    edValue->setMinimum(-9999);
    edValue->setMaximum(99999);
    edValue->setValue(0);
    vl->addWidget(edValue);
    rbDirect    = new QRadioButton(tr("Direct"));
    rbDirect->setChecked(true);
    rbAdd       = new QRadioButton(tr("Addition"));
    rbSub       = new QRadioButton(tr("Subtraction"));
    QHBoxLayout *hl = new QHBoxLayout;
    hl->addWidget(rbDirect);
    hl->addWidget(rbAdd);
    hl->addWidget(rbSub);
    hl->addStretch();
    vl->addLayout(hl);

    QHBoxLayout *hl2 = new QHBoxLayout;
    btnCancel   = new QPushButton(tr("Cancel"));
    btnOK       = new QPushButton(tr("OK"));
    btnOK->setDefault(true);
    hl2->addStretch();
    hl2->addWidget(btnCancel);
    hl2->addWidget(btnOK);
    vl->addStretch();
    vl->addLayout(hl2);

    setLayout(vl);

    connect(btnCancel,SIGNAL(clicked()),this,SLOT(reject()));
    connect(btnOK,SIGNAL(clicked()),this,SLOT(accept()));
}
//****************************************************************************
void CalcDialog::setValue(int v)
{
    edValue->setValue(v);
}
//****************************************************************************
void CalcDialog::setMode(int md)
{
    switch (md) {
    case 1:
        rbAdd->setChecked(true);
        rbDirect->setChecked(false);
        rbSub->setChecked(false);
        break;
    case 2:
        rbSub->setChecked(true);
        rbDirect->setChecked(false);
        rbAdd->setChecked(false);
        break;
    case 0:
    default:
        rbDirect->setChecked(true);
        rbAdd->setChecked(false);
        rbSub->setChecked(false);
        break;
        break;
    }
}
//****************************************************************************
int CalcDialog::mode()
{
    if (rbDirect->isChecked()){
        return 0;
    } else if(rbAdd->isChecked()){
        return 1;
    } else if(rbSub->isChecked()){
        return 2;
    }else{
        return 1;
    }
}
//****************************************************************************
