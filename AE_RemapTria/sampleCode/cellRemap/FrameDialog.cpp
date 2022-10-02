#include "FrameDialog.h"

FrameDialog::FrameDialog(QWidget *parent) :
    QDialog(parent)
{
    QVBoxLayout *vl = new QVBoxLayout;

    QGroupBox *gb = new QGroupBox(tr("Frame"));

    QVBoxLayout *vl1 = new QVBoxLayout;
    rbFrameInsert = new QRadioButton(tr("FrameInsert"));
    rbFrameDelete = new QRadioButton(tr("FrameDelete"));
    vl1->addWidget(rbFrameInsert);
    vl1->addWidget(rbFrameDelete);

    QVBoxLayout *vl2 = new QVBoxLayout;
    cbCellAll = new QCheckBox(tr("CellAll"));
    cbFrameCountChange = new QCheckBox(tr("FrameCountChange"));
    vl2->addWidget(cbCellAll);
    vl2->addWidget(cbFrameCountChange);
    QHBoxLayout *hl = new QHBoxLayout;
    hl->addLayout(vl2);
    vl1->addLayout(hl);
    hl->addStretch();

    gb->setLayout(vl1);

    vl->addWidget(gb);

    QHBoxLayout *hl2 = new QHBoxLayout;
    btnCancel = new QPushButton(tr("Cancel"));
    btnOK = new QPushButton(tr("OK"));
    btnOK->setDefault(true);
    hl2->addStretch();
    hl2->addWidget(btnCancel);
    hl2->addWidget(btnOK);
    vl->addStretch();
    vl->addLayout(hl2);

    setLayout(vl);

    connect(btnOK,SIGNAL(clicked()),this,SLOT(accept()));
    connect(btnCancel,SIGNAL(clicked()),this,SLOT(reject()));

}

//********************************************************************
FrameDialog::~FrameDialog()
{
}
//********************************************************************
bool FrameDialog::isInsert()
{
    return rbFrameInsert->isChecked();
}
//********************************************************************
void FrameDialog::setIsInsert(bool b)
{
    rbFrameDelete->setChecked(!b);
    rbFrameInsert->setChecked(b);
}
//********************************************************************
bool FrameDialog::isDelete()
{
    return rbFrameDelete->isChecked();
}
//********************************************************************
void FrameDialog::setIsDelete(bool b)
{
    rbFrameInsert->setChecked(!b);
    rbFrameDelete->setChecked(b);

}
//********************************************************************
bool FrameDialog::isCellAll()
{
    return cbCellAll->isChecked();
}

//********************************************************************
void FrameDialog::setCellAll(bool b)
{
    cbCellAll->setChecked(b);
}
//********************************************************************
bool FrameDialog::isFrameCountChange()
{
    return cbFrameCountChange->isChecked();

}
//********************************************************************
void FrameDialog::setIsFrameCountChange(bool b)
{
    cbFrameCountChange->setChecked(b);
}
//********************************************************************
