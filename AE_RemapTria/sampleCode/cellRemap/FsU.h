#ifndef FSUTILS_H
#define FSUTILS_H


#include <QFile>
#include <QDir>

#include <QString>
#include <QStringList>
#include <QColor>

#include <QJsonDocument>
#include <QJsonObject>
#include <QJsonValue>
#include <QJsonArray>
#include <QByteArray>

class FsU
{
public:
    FsU();
    //文字末尾から探す
    //static int lastIndexOf(QString p,QChar c);
    //拡張子を獲得
    static QString getExt(QString p);
    //拡張子なしファイル名を獲得
    static QString getFileNameWithoutExt(QString p);
    //ファイル名を獲得
    static QString getFileName(QString p);
    //親ディレクトリ名を獲得
    static QString getDirectoryName(QString p);
    //拡張子を獲得
    static QString changeExt(QString p, QString e);
    //パスとファイル名を結合
    static QString combine(QString p, QString n);

    static bool isBOM(QByteArray p);
    static QByteArray cutBOM(QByteArray p);
    static QByteArray BOM();

    //JSONをAEのtoSource()形式に直す
    static QString jsonToAE(QString s);

    static int colToInt(QColor c);
    static QColor intToCol(int v);
    static QJsonArray colToJa(QColor c);
    static QColor jaToCol(QJsonValue v,QColor c);
    static QByteArray aeToJson(QByteArray s);

    static QJsonArray listToJa(QStringList lst);
    static QJsonArray aeListToJa(QStringList lst);
    static QStringList jaTolist(QJsonArray ja);
    static QStringList jaToAeList(QJsonValue v);

    static QString pathWinToJS(QString p);
    static QString pathJSToWin(QString p);
    static QString pathString(QString p);

    static int indexOfFrameStr(QString p);
    static QStringList getFile(QString p);
    static QStringList getDir(QString p);
    //static QStringList getVolumes(QString p);
    static bool makeFolder(QString p);

};

#endif // FSUTILS_H
