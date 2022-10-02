#ifndef TSSHEET_H
#define TSSHEET_H



#include <QMainWindow>
#include <QAction>


#include <QCloseEvent>
#include <QProcess>
#include <QScreen>
#include <QDir>
#include <QFile>
#include <QPointer>
#include <QFileDialog>

#include <QAction>
#include <QToolBar>
#include <QToolButton>
#include <QPointer>
#include <QMessageBox>

#include "TSGrid.h"
#include "TSPref.h"
#include "ExecMessage.h"

#include "NavBar.h"

#include "TSFileDialog.h"
#include "KeyHelpForm.h"

class TSGrid;


#define TSLosdFileFiler  "cellRemap Save Files (*.ardj *.ard);;STS Save Files (*.sts);;TSH Save Files (*.tsh);;Rimaping Save Files (*.xps);;Stylos CSV Files (*.csv);;Timesheet(*.ardj *.ard *.sts *.tsh *.xps);;All Files (*.*)"
#define TSSaveFileFiler  "cellRemap Save Files (*.ardj);;STS Save Files (*.sts);;All Files (*.*)"

class TSSheet : public QMainWindow
{
    Q_OBJECT

public:
    explicit TSSheet(QWidget *parent = 0);
    ~TSSheet();

    void GetStatus();

    void Save(QString p);

    QString afterFXPath();
    QString sheetName();
    void createNavBar();


    void showDialogs(bool IsShow);
    void createMenuBar();


signals:
    void afterFXPathChanged(QString);
    void changedStayoOn(bool);
    void widthChanged(int);
    void posChanged(QPoint);
    void navBarChanged(bool);
    void stayOnChanged(bool);

public slots:
    void showSheetDialog(bool IsShow = true);

    void infoStr(QString s);

    void newSheet();
    void saveAsDialog();
    void load(QString p);
    void fromClip();
    void loadAsDialog();

    void setStayOn(bool b,bool IsShow = true);

    void showPrefDialog(bool IsShow = true);
    void activeSheet();
    void setWinPos(QPoint pos);
    void setNavBar(bool b);

    void dialogShowBefore(bool IsShow = true);
    void dialogShowAfter(bool IsShow = true);
    void setConsoleMode(bool b);
    void keyHelpShow();
    void showAbout();

private:
    //----------------------
    TSGrid      *m_grid;
    //----------------------
    //----------------------
    QAction     *actionToXMP;
    QAction     *actionNew;
    QAction     *actionImport;
    QAction     *actionExport;
    QAction     *actionFromClip;
    QAction     *actionToClip;
    QAction     *actionAfterFX;
    QAction     *actionQuit;
    //----------------------
    QAction     *actionCopy;
    QAction     *actionCut;
    QAction     *actionPaste;
    QAction     *actionSheet;
    QAction     *actionPref;
    //----------------------
    QAction     *actionToRemap;
    QAction     *actionToExpression;
    QAction     *actionCellRename;
    QAction     *actionCellInsert;
    QAction     *actionCellRemove;
    QAction     *actionFromRemap;
    QAction     *actionAutoInput;
    QAction     *actionCalc;
    QAction     *actionFrameAdd;
    QAction     *actionFrameDel;
    //----------------------
    QAction     *actionNavBar;
    QAction     *actionStayOn;
    //----------------------
    QAction     *actionAbout;
    QAction     *actionKeyHelp;
    QAction     *actionHelp;



    QPointer <NavBar>   nav;
    QPointer <KeyHelpForm> m_keyhelp;
    bool m_IsShowDialogs;

    bool consoleMode;

protected:
    void keyPressEvent(QKeyEvent *event);
    void keyReleaseEvent(QKeyEvent *);


    void resizeEvent(QResizeEvent *);
    void moveEvent(QMoveEvent *);
    void closeEvent(QCloseEvent *);
    void showEvent(QShowEvent *);

    void mousePressEvent(QMouseEvent *e);
    void mouseMoveEvent(QMouseEvent *e);
    void mouseReleaseEvent(QMouseEvent *);

private slots:
    void openHelp();
};





#endif // TSSHEET_H

