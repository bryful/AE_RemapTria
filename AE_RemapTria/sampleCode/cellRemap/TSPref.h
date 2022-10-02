#ifndef TSPREF_H
#define TSPREF_H

#include <QApplication>
#include <QStringList>
#include <QColor>
#include <QPoint>
#include <QSize>
#include <QFont>
#include <QSettings>

#include <QStandardPaths>

#include <QDir>
#include <QFile>
#include <QFileInfo>

#include <QJsonDocument>
#include <QJsonObject>
#include <QJsonValue>
#include <QJsonArray>
#include <QByteArray>
#include <QTextStream>

#include <QProcess>


#include "FsU.h"
namespace FrameDisp{
    enum{
        Frame = 0,
        SecKoma,
        PageFrame,
        PageSecKoma
    };
}
namespace PageMode{
    enum{
        Sec6 = 6,
        Sec3 = 3
    };
}
class TSPref
{
private:

    QString     m_prefPath;
    QString     m_appData;



    int     m_FrameRateDef;
    int     m_HorGuide;

    bool    m_Is30fps_6;

    int     m_PageMode;
    int     m_PageCount;

    QString m_AfterFXPath;

    void mkDir(QString p);
    bool m_isJsxTemp;

public:

    int cellCountDef;
    int frameCountDef;

    int cellWidth;
    int cellHeight;
    int captionlHeight;
    int frameWidth;

    int frameInter;
    int captionInter;

    QColor baseColor;
    QColor emptyColor;
    QColor emptyColor2nd;
    QColor cellColor;
    QColor selectionColor;
    QColor captionColor;
    QColor captionColorSel;
    QColor frameColor;
    QColor frameColor2nd;
    QColor frameColorSel;
    QColor lineColor;
    QColor serialColor;
    QColor textColor;
    QColor inputColor;

    int     startFrame;
    bool    isNoneDeleteInputValue;
    int     frameDispMode;

    QFont   font;

    QPoint  winPos;
    QSize   winSize;

    TSPref();
    void clear();
    void init();

    void assign(TSPref pf);

    void writePref();
    void readPref();

    void prefSave();
    void prefLoad();

    void setFrameRateDef(int fps);
    int frameRateDef() { return m_FrameRateDef;}
    void setPageMode(int mode);
    int  pageMode(){ return m_PageMode;}
    int  pageCount() { return m_PageCount;}
    void setSizeDef(int c,int f);
    bool is30fps_6(){return m_Is30fps_6;}
    bool setIs30fps_6(bool b);
    int horGuide() { return m_HorGuide;}
    void setAfterFXPath(QString p);
    QString afterFXPath( ) { return m_AfterFXPath;}
    QString afterFXName();

    QStringList afterFXList;

    QString yenToRoot(QString s);
    void chkAfterFXList();
    void addAfterFXList(QString p);
    void findAfterFX();
    bool existsAfterFX(QString p);
    bool isStayOn;
    bool isNavBar;
    bool isWheelRev;
    void openPrefFolder();

    void setIsJsxTemp(bool b);
    bool isJsxTemp() { return m_isJsxTemp;}
};

#endif // TSPREF_H
