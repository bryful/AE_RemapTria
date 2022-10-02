#include "CellInsertDialog.h"

CellInsertDialog::CellInsertDialog(QWidget *parent) :
    QDialog(parent)
{
    this->setWindowFlags(Qt::Dialog|Qt::WindowStaysOnTopHint);


    QVBoxLayout *vl = new QVBoxLayout;

    QHBoxLayout *hl1 = new QHBoxLayout;
    QLabel *lb = new QLabel(tr("Insert Cell Name"));
    lb->setSizePolicy(QSizePolicy::Fixed,QSizePolicy::Fixed);
    hl1->addWidget(lb);

    edNew = new QLineEdit;
    hl1->addWidget(edNew);

    vl->addLayout(hl1);
    vl->addStretch();

    QHBoxLayout *hl2 = new QHBoxLayout;
    btnCancel = new QPushButton(tr("Cancel"));
    btnOK = new QPushButton(tr("OK"));
    btnOK->setDefault(true);
    hl2->addStretch();
    hl2->addWidget(btnCancel);
    hl2->addWidget(btnOK);

    vl->addLayout(hl2);

    setLayout(vl);

    connect(edNew,SIGNAL(textChanged(QString)),this,SLOT(checkCellName(QString)));
    connect(btnOK,SIGNAL(clicked()),this,SLOT(accept()));
    connect(btnCancel,SIGNAL(clicked()),this,SLOT(reject()));
}

CellInsertDialog::~CellInsertDialog()
{
}
//*****************************************************
void CellInsertDialog::setCellName(QString s)
{
    edNew->setText(s);
}
//*****************************************************
QString CellInsertDialog::cellName()
{
    return edNew->text().trimmed();
}
//*****************************************************
void CellInsertDialog::checkCellName(QString s)
{
    bool b =true;
    if (s.trimmed().isEmpty()){
        b = false;
    }
    btnOK->setEnabled(b);
}
