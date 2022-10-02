#ifndef AUTIINPUTDIALOG_H
#define AUTIINPUTDIALOG_H

#include <QDialog>
#include <QGridLayout>
#include <QHBoxLayout>
#include <QVBoxLayout>
#include <QSpinBox>
#include <QLabel>
#include <QPushButton>

class AutoInputDialog : public QDialog
{
    Q_OBJECT

public:
    explicit AutoInputDialog(int st = 1,int lst = 3,int len = 3,QWidget *parent = 0);
    ~AutoInputDialog();

    QSpinBox    *edStart;
    QSpinBox    *edLast;
    QSpinBox    *edLength;
    QPushButton *btnOK;
    QPushButton *btnCancel;

    int startCellNum();
    void setStartCellNum(int v);
    int lastCellNum();
    void setLastCellNum(int v);
    int lengthCellNum();
    void setLengthCellNum(int v);

private:
    //Ui::AutoInputDialog *ui;
};

#endif // AUTIINPUTDIALOG_H
