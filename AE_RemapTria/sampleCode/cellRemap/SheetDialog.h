#ifndef SHEETDIALOG_H
#define SHEETDIALOG_H

#include <QDialog>
#include <QGridLayout>
#include <QHBoxLayout>
#include <QVBoxLayout>
#include <QSpinBox>
#include <QLineEdit>
#include <QLabel>
#include <QPushButton>
#include <QGroupBox>
#include <QComboBox>


#include "TSData.h"

class SheetDialog : public QDialog
{
    Q_OBJECT

public:
    explicit SheetDialog(TSData d, QWidget *parent = 0);
    ~SheetDialog();


    QPushButton     *btnCancel;
    QPushButton     *btnOK;
    QLineEdit       *edSheetName;
    QSpinBox        *sbCellCount;
    QSpinBox        *sbFrameCount;
    QSpinBox        *sbSec;
    QSpinBox        *sbKoma;
    QComboBox       *cmbFrameRate;

    int frameRate(){ return m_FrameRate;}
    int frameCount(){ return m_FrameCount;}
    int cellCount(){ return m_CellCount;}
    QString sheetName(){ return m_SheetName;}

private slots:

    void sbFrameCountChanged(int arg1);
    void sbSecChanged(int);
    void sbKomaChanged(int);
    void cmbFrameRatecurrentIndexChanged(int index);
    void sbCellCountChanged(int arg1);

    void edSheetNameTextChanged(const QString &arg1);

private:

    int m_FrameRate;
    int m_FrameCount;
    int m_CellCount;
    QString m_SheetName;

    void cmbFrameRateToData(int v);
    void dataTocmbFrameRate();
    void secKomaToFrame();
    void frameToSecKoma();
    void enabledChk();

    bool chgFlg;

};

#endif // SHEETDIALOG_H
