#ifndef TSAESCRIPT_H
#define TSAESCRIPT_H

#include <QObject>
#include <QTimer>
#include <QDir>
#include <QFile>
#include <QPointer>
#include <QProcess>

#include "TSData.h"
#include "TSSelection.h"
#include "TSGrid.h"
#include "TSPref.h"

class TSData;
class TSSelection;
class TSGrid;
class TSPref;

#define TSEXPRESSION    "expression.txt"
#define TSGetRemap      "getRemap.jsx"
#define TSSetRemap      "setRemap.jsx"
#define TSScriptsFoler  "Scripts"

#define TSXMPJSX        "XMPSHEET.jsx"
#define TSJSONJSX       "FsJSON.jsx"
#define TSCELLREMAPJSX  "cellRemap.jsx"
#define TSSCRIPTUI      "Scripts/ScriptUI Panels"
#define TSSTARTUP       "Scripts/Startup"


class TSAEScript : public QObject
{

    Q_OBJECT
private:
    TSData      *m_data;
    TSSelection *m_sel;
    TSPref      *m_pref;

    QString     m_resultFilename;
    QString     m_scriptFilename;
    QTimer      m_timer;
    int         m_timerCounter;
    int         m_timerCounterMax;
    int         m_loopTime;
    bool instScriptSub(QString p, QString s);


public:
    explicit TSAEScript(TSData *d = 0,TSSelection *s = 0,TSPref *p = 0, QObject *parent = 0);
    ~TSAEScript();
    void SetParams(TSData *d,TSSelection *s,TSPref *p);

    int counterMax(){return m_timerCounterMax;}
    void setCounterMax(int v){m_timerCounterMax = v; if(m_timerCounterMax<2)m_timerCounterMax=2; }
    int loopTime(){return m_loopTime;}
    void setLoopTime(int v){m_loopTime = v; if(m_loopTime<100)m_loopTime=100; }
    bool sendScript(QString code);
    bool sendScriptFile(QString p);
    bool isOpenAfetrFX();
    static QString scriptsFolder();
    static bool makeFolder(QString p);
    static void exportJsxScript(QString s, bool isWrite = false);
    static void exportJsxScriptALL(bool isWrite = false);
    QString loadScript(QString s);
    bool instScript(QString p);
    bool uninstScript(QString p);
    void openScriptFolder();
    void setIsJsxTemp(bool b);

signals:
    void FromRemapFinished(bool);

public slots:
    bool toExpression();
    bool toRemap();
    bool fromRemap();
    void execAfterFX();
    bool toXMP();
    //void chkScripts();

private slots:
    void fromRemapLoop();
};

#endif // TSAESCRIPT_H
