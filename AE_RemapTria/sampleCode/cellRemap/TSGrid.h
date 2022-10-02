#ifndef TSGRID_H
#define TSGRID_H


#include <QMainWindow>
#include <QWidget>
#include <QScrollBar>
#include <QPainter>
#include <QPoint>
#include <QRect>
#include <QPointer>
#include <QDateTime>
#include <QMenu>
#include <QActionGroup>
#include <QAction>
#include <QMessageBox>

#include "TSData.h"
#include "TSPref.h"
#include "TSGridArea.h"
#include "TSSelection.h"

#include "TSSheet.h"
#include "SheetDialog.h"
#include "PrefDialog.h"
#include "TSAEScript.h"
#include "ExecMessage.h"

#include "CellRenameDialog.h"
#include "CellInsertDialog.h"
#include "AutoInputDialog.h"
#include "FrameDialog.h"
#include "CalcDialog.h"


class TSSheet;
class TSAEScript;

enum
{
    mdModeNormal,
    mdModeScroll,
    mdModeSelection
};
#define KEYSHIFT    0x00010000
#define KEYCONTROL  0x00020000
#define KEYMETA     0x00040000

class TSGrid : public QWidget
{
    Q_OBJECT
private:

    TSSheet     *m_sheet;
    TSPref      m_pref;
    TSData      m_data;
    TSGridArea  m_area;
    TSSelection m_sel;
    TSAEScript  *m_ae;

    int isPushSpace;
    int mdMode;
    int mdFrame;
    QPoint mdPos;
    QPoint mdDispXY;

    QScrollBar *hBar;
    QScrollBar *vBar;


    void drawCaption(QPainter *pnt);
    void drawFrame(QPainter *pnt);
    void drawCell(QPainter *pnt);
    void drawInput(QPainter *pnt);

    QString m_inputValue;
    bool isNoDeleteInputValueSub;

    QString frameDispStr(int v);
    QPointer <ExecMessage>  m_mes;

    int m_calc_value;
    int m_calc_mode;


    QAction     *actionCopy;
    QAction     *actionCut;
    QAction     *actionPaste;
    QAction     *actionCellRename;
    QAction     *actionCellRemove;
    QAction     *actionCellInsert;
    QAction     *actionFrameAdd;
    QAction     *actionFrameDel;
    QAction     *actionAutoInput;
    QAction     *actionCalc;
    QAction     *actionFrameDispFrame;
    QAction     *actionFrameDispSecKoma;
    QAction     *actionFrameDispPageFrame;
    QAction     *actionFrameDispPageSecKoma;
    QAction     *actionStarrFrameZero;
    QAction     *actionOpenAfterFX;

    void createActions();

    int m_AutoInputStart;
    int m_AutoInputLast;
    int m_AutoInputLength;
    int m_FrameCellAll;
    int m_FrameCountChange;


public:
    explicit TSGrid(QWidget *parent = 0);
    ~TSGrid();
    void setTSSheet(TSSheet *tss);

    void setStatus();
    bool keyPressExec(QKeyEvent * event);
    bool keyReleaseExec();
    void setSize(int c, int f);
    void setFrameRate(int fps);
    void setSheetName(QString nm);
    QString getSheetName();
    QString infoStr();

    bool inputNum(QString k);
    bool selLength(QString k);
    bool inputBS();
    bool inputClear();
    bool inputEnter();
    bool inputPeriod();
    bool inputPluss();
    bool inputMinus();

    bool selMovUp();
    bool selMovDown();
    bool selMovLeft();
    bool selMovRight();

    bool selTailPluss();
    bool selTailMinus();
    bool selToEnd();
    bool selALL();
    bool pageDown();
    bool pageUp();
    bool gotoTop();
    bool gotoEnd();
    bool scrolDown();
    bool scrolUp();

    bool save(QString p);
    bool load(QString p);
    QString afterFXPath(){return m_data.afterFXPath();}
    void prefWrite();
    void prefRead();


    bool isNavBar(){ return m_pref.isNavBar;}
    bool isStayOn(){ return m_pref.isStayOn;}

    QString fileFullName() { return m_data.fileFullName();}
    QString FilePath() { return m_data.filePath();}
    QString FileName() { return m_data.fileName();}
    QString FileExt() { return m_data.fileExt();}

    void newSheet();

    void setModiFlag();
    bool modiFlag() { return m_data.modiFlag;}

signals:
    void dataChanged(QString);
    void dataSizeChanged(int ,int);

    void dispXChanged(int);
    void dispYChanged(int);

    void navBarChanged(bool);
    void stayOnChanged(bool);

public slots:
    void setNavBar(bool);
    void setStayOn(bool);

    void prefToData();
    void setDispX(int x);
    void setDispY(int y);
    void setDispXYDelta(int dx,int dy);
    void showSheetDialog(bool IsShow = true);
    void showPrefDialog(bool IsShow = true);
    void showCellRenameDialog();
    void showCellInsertDialog();
    void showCellRemoveDialog();
    void showCalcDialog();
    void copy();
    void cut();
    void paste();
    void toRemap();
    void fromRemap();
    void toXMP();
    void fromRemapFinshed(bool b);
    bool toClip();
    void toExpression();
    bool fromClip();
    void openAfterFX();
    void cursorOpenHand();
    void cursorCloseHand();
    void cursorArrow();
    void mousePressExec(QMouseEvent * e);
    void mouseMoveExec(QMouseEvent *e);
    void mouseReleaseExec();

    void dispFrame();
    void dispSecKoma();
    void dispPageFrame();
    void dispPageSecKoma();

    void setStartZero(bool b);

    void autoInputDialog();
    void calc(int value,int mode);
    void frameInset();
    void frameDelete();
    void frameInsDel(bool ins);
    void wheelExec(QWheelEvent *e);

protected:
    void    paintEvent(QPaintEvent *event);
    void	resizeEvent(QResizeEvent * event);
    void	keyPressEvent(QKeyEvent * event);
    void	keyReleaseEvent(QKeyEvent * event);


    void mousePressEvent(QMouseEvent * event);
    void mouseReleaseEvent(QMouseEvent * event);
    void mouseMoveEvent(QMouseEvent * event);
    void mouseDoubleClickEvent(QMouseEvent *event);
    void wheelEvent(QWheelEvent *e);
    void dragEnterEvent(QDragEnterEvent *);
    void dropEvent(QDropEvent *);

    void contextMenuEvent(QContextMenuEvent *);
};

#endif // TSGRID_H
