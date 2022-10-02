#ifndef CALCDIALOG_H
#define CALCDIALOG_H

#include <QDialog>
#include <QSpinBox>
#include <QRadioButton>
#include <QLabel>
#include <QPushButton>
#include <QHBoxLayout>
#include <QVBoxLayout>

class CalcDialog : public QDialog
{
    Q_OBJECT
public:
    explicit CalcDialog(QWidget *parent = 0);

    QSpinBox        *edValue;
    QRadioButton    *rbDirect;
    QRadioButton    *rbAdd;
    QRadioButton    *rbSub;
    QPushButton     *btnOK;
    QPushButton     *btnCancel;

    int value(){return edValue->value();}
    int mode();
signals:

public slots:
    void setValue(int v);
    void setMode(int md);

};

#endif // CALCDIALOG_H
