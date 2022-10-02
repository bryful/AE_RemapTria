#ifndef FRAMEDIALOG_H
#define FRAMEDIALOG_H

#include <QDialog>
#include <QGroupBox>
#include <QRadioButton>
#include <QCheckBox>

#include <QGridLayout>
#include <QHBoxLayout>
#include <QVBoxLayout>
#include <QSpinBox>
#include <QLabel>
#include <QLineEdit>
#include <QPushButton>


class FrameDialog : public QDialog
{
    Q_OBJECT

public:
    explicit FrameDialog(QWidget *parent = 0);
    ~FrameDialog();

    QRadioButton    *rbFrameInsert;
    QRadioButton    *rbFrameDelete;

    QCheckBox       *cbCellAll;
    QCheckBox       *cbFrameCountChange;

    QPushButton     *btnOK;
    QPushButton     *btnCancel;

    bool isInsert();
    void setIsInsert(bool b);
    bool isDelete();
    void setIsDelete(bool b);

    bool isCellAll();
    void setCellAll(bool b);
    bool isFrameCountChange();
    void setIsFrameCountChange(bool b);
private:
};

#endif // framedialog_H
