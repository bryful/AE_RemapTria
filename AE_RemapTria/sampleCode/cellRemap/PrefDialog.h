#ifndef PREFDIALOG_H
#define PREFDIALOG_H

#include <QDialog>
#include <QFontDialog>
#include <QProcess>
#include <QGridLayout>
#include <QHBoxLayout>
#include <QVBoxLayout>
#include <QSpinBox>
#include <QLabel>
#include <QPushButton>
#include <QComboBox>
#include <QCheckBox>
#include <QSpinBox>

#include "ColorBox.h"
#include "TSPref.h"
#include "TSAEScript.h"


class PrefDialog : public QDialog
{
    Q_OBJECT

public:
    explicit PrefDialog(TSPref pf,QWidget *parent = 0);
    ~PrefDialog();


    QComboBox   *cmbFrameDisp;
    QComboBox   *cmbPageMode;
    QCheckBox   *cbIs30fps_6;
    QCheckBox   *cbIsNoneDeleteInputValue;
    QCheckBox   *cbIsStartZero;
    QCheckBox   *cbWheelRev;
    QCheckBox   *cbIsJsxTemp;


    QSpinBox    *sbCellWidth;
    QSpinBox    *sbCellHeight;
    QSpinBox    *sbCaptionHeight;
    QSpinBox    *sbFrameWidth;

    QPushButton *btnFont;
    QLabel      *lbFont;
    ColorBox    *cbxBase;
    ColorBox    *cbxCaption;
    ColorBox    *cbxCaptionSel;
    ColorBox    *cbxCell;
    ColorBox    *cbxEmpty;
    ColorBox    *cbxEmpty2nd;
    ColorBox    *cbxFrame;
    ColorBox    *cbxFrame2nd;
    ColorBox    *cbxFrameSel;
    ColorBox    *cbxInput;
    ColorBox    *cbxLine;
    ColorBox    *cbxSelection;
    ColorBox    *cbxSerial;
    ColorBox    *cbxText;

    //QPushButton *btnAddAfterFX;
    QComboBox   *cmbAfterFX;
    QPushButton *btnScriptInst;
    QPushButton *btnScriptUninst;
    QLabel      *lbScript;
    QPushButton *btnInstFolder;
    QPushButton *btnOpenScrptFld;

    QPushButton *btnInit;
    QPushButton *btnOpenPref;
    QPushButton *btnCancel;
    QPushButton *btnOK;

    TSPref getPref() { return m_pref;}
private slots:
    void btnFontClicked();
    void btnOKClicked();
    void btnInitClicked();

    void instScript();
    void uninstScript();
    void openScriptFolder();
    void openInstFolder();
    void openPrefFolder();

private:
    TSPref m_pref;
    void fromPref();
    void toPref();
    QString afterFXFolder();
};

#endif // PREFDIALOG_H
