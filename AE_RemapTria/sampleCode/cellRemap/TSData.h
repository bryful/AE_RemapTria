#ifndef TSDATA_H
#define TSDATA_H


#include "TSDataDef.h"


#ifndef CONSOLE_MODE
#include <QApplication>
#endif
#include <string.h>
#include <QtGlobal>
#include <QList>
#include <QVector>
#include <QStringList>

#include <QJsonDocument>
#include <QJsonObject>
#include <QJsonValue>
#include <QJsonArray>
#include <QByteArray>

#include <QDir>
#include <QFile>
#include <QFileInfo>
#include <QTextCodec>
#include <QTextStream>
#include <QClipboard>

#include "FsU.h"


enum{
    CellNumNone         = -4,
    CellNumZeroStart    = -3,
    CellNumZeroSerial   = -2,
    CellNumSerial       = -1,
    CellNumZero         = 0
};

enum{
    fps12 = 12,
    fps15 = 15,
    fps24 = 24,
    fps30 = 30
};
#define CellArray QList<int>
#define CellArrays QList< CellArray >

class TSData
{

public:
    TSData();
    void assign(TSData d);
    void setSize(int c, int f);

    int modiFlag;
    QString clipHeader(){return m_ClipHeader;}
    void setSheetName(QString s);
    QString sheetName();
    int cellCount() { return m_CellCount;}
    int frameCount() { return m_FrameCount;}
    int frameRate() { return m_FrameRate;}

    QString getCaption(int idx);
    bool setCaption(int idx, QString cap);

    int getNumType(int c,int f);
    int getNum(int c,int f);
    bool setNum(int c,int f,int num);
    void setFrameRate(int fps);

    QString infoStr();
    QString secStr();

    QString afterFXPath(){return m_AfterFXPath;}
    QString  toJson();
    QString  toJsonAE();

    bool  fromJson(QByteArray js);
    bool save(QString p);
    bool load(QString p);

#ifndef CONSOLE_MODE
    bool toClipBoard();
    bool fromClipBoard();
#endif
    bool isEmptyCell(int c);
    QList < QList<int> > cellKeys(int index);
    void jsonArrayToCells(QJsonArray ja);
    QList< QList<double> > getKeys(int c);
    bool fromRemap(QString s);

    QString fileName(){return m_sheetName;}
    QString fileFullName();
    QString filePath(){return m_filePath;}
    QString fileExt(){return m_fileExt;}

    void cellInsert(int c,QString cap);
    void cellRemove(int c);
    void toStdout();
    void toStdoutAE();
    void frameInsert(int c ,int s,int l,bool IsAll = true,bool isLength = true);
    void frameDelete(int c ,int s,int l,bool IsAll = true,bool isLength = true);
    bool swapCell(int s,int d);

    bool toClipExpression(int c);

private:
    QString m_filePath;
    QString m_fileExt;
    QString m_sheetName;

    QStringList m_caption;
    CellArrays m_cells;
    int m_CellCount;
    int m_FrameCount;
    int m_FrameRate;


    QString m_AfterFXPath;
    QJsonArray captionToJ();
    QJsonArray cellsToJ();
    QString jsonToAE(QString s);
    QByteArray aeToJson(QByteArray s);

    void cellsAllMinusTo();
    void cellsAllMinusFrom();
    void setNumRow(int c, int f, int v);
    QString m_ClipHeader;

    int lastIndexOf(QString s,QChar c);
    void splitName(QString s);
    bool saveJson(QString p);
    bool loadJson(QString p);

    bool loadARD(QString p);
    bool loadTSH(QString p);
    bool loadSTS(QString p);
    bool loadXPS(QString p);
    bool loadStylos(QString p);
//    bool saveARD(QString p);
//    bool saveTSH(QString p);
    bool saveSTS(QString p);
//    bool saveXPS(QString p);
};

#endif // TSDATA_H
