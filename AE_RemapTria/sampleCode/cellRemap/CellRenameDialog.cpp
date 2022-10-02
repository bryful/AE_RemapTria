#include "CellRenameDialog.h"

//*****************************************************
CellRenameDialog::CellRenameDialog(QWidget *parent) :
    QDialog(parent)
{
    this->setWindowFlags(Qt::Dialog|Qt::WindowStaysOnTopHint);


    QVBoxLayout *vl = new QVBoxLayout;

    QGridLayout *gl = new QGridLayout;

    QLabel *lbOrg = new QLabel(tr("Original Name"));
    QLabel *lbNew = new QLabel(tr("New Name"));

    edNew = new QLineEdit;
    edOrg = new QLineEdit;
    edOrg->setReadOnly(true);
    gl->addWidget(lbOrg,0,0);
    gl->addWidget(edOrg,0,1);
    gl->addWidget(lbNew,1,0);
    gl->addWidget(edNew,1,1);

    vl->addLayout(gl);
    vl->addStretch();

    QHBoxLayout *hl = new QHBoxLayout;
    hl->addStretch();
    btnCancel   = new QPushButton(tr("Cancel"));
    btnOK       = new QPushButton(tr("OK"));
    btnOK->setDefault(true);
    hl->addWidget(btnCancel);
    hl->addWidget(btnOK);
    vl->addLayout(hl);

    setLayout(vl);
    btnOK->setEnabled(false);
    connect(edNew,SIGNAL(textChanged(QString)),this,SLOT(checkCellName(QString)));
    connect(btnOK,SIGNAL(clicked()),this,SLOT(accept()));
    connect(btnCancel,SIGNAL(clicked()),this,SLOT(reject()));
}
//*****************************************************
CellRenameDialog::~CellRenameDialog()
{
}
//*****************************************************
void CellRenameDialog::setCellName(QString s)
{
    edOrg->setText(s);
    edNew->setText(s);
}
//*****************************************************
QString CellRenameDialog::cellName()
{
    return edNew->text().trimmed();
}
//*****************************************************
void CellRenameDialog::checkCellName(QString s)
{
    bool b =true;
    if ((s == edOrg->text())||(s.trimmed().isEmpty())){
        b = false;
    }
    btnOK->setEnabled(b);
}
//*****************************************************
