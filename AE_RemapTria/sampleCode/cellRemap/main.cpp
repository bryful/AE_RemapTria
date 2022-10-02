#include <QApplication>
#include <QStringList>
#include <QTextCodec>
#include <QFile>
#include <QProcess>
#include <QTranslator>
#include <QClipboard>
#include <QMessageBox>

#if defined(DEBUG)
#include <QDebug>
#endif


#include "TSData.h"
#include "TSSheet.h"
#include "NavBar.h"

//-------------------------------------------------
bool toClip(QString p)
{
    bool ret = false;
    QFile f(p);
    QString s;
    if (f.exists()){
        if (f.open(QFile::ReadOnly)){
            QTextStream ts(&f);
            ts.setCodec("UTF-8");
            s = ts.readAll();
            f.close();
            if (!s .isEmpty()){
                QClipboard *clipboard = QApplication::clipboard();
                clipboard->setText(s);
                ret = true;
            }

        }
    }
    return ret;
}
//-------------------------------------------------
int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    QTranslator appTranslator;
    appTranslator.load(":res/cellRemap_jp.qm");
    a.installTranslator(&appTranslator);

    QCoreApplication::setApplicationName("cellRemap");
    QCoreApplication::setOrganizationName("bry-ful");


    //----------------
    //Option Chk
    bool IsNewProcess = false;
    QString filePath = "";
    QString myPath = "";
    QStringList cmds = a.arguments();
    myPath = QApplication::applicationFilePath();
    if(cmds.size()>1){
        for (int i=1; i<cmds.size();i++){
            QString c = cmds[i];
            if (c[0]=='-') c = c.toLower();
            if (c == "-n") {
               IsNewProcess = true;
            }else if(filePath.isEmpty()){
                QFile f(c);
                if (f.exists()) filePath = c;

            }
        }
    }
    //----------------
    if (IsNewProcess){
        QProcess ps;
        QString prg = "";
        cmds.clear();
#if defined(Q_OS_MAC)
        QString appExt = ".app";
        int idx=myPath.indexOf(appExt);
        if ( idx>=0) myPath = myPath.mid(0,idx + appExt.size());
        prg = "open -n ";
        prg +=  "\"" + myPath + "\"";
        if ( !filePath.isEmpty()){
            toClip(filePath);
        }
        QProcess::execute(prg);
#elif defined(Q_OS_WIN)
        prg = "\"" + myPath.replace("/","\\") + "\"";
        if ( ! filePath.isEmpty()){
            prg += " \"" + filePath.replace("/","\\") + "\"";
        }
        QProcess::startDetached(prg);

#endif
        return 0;
    }
    TSSheet w;
    if ( !filePath.isEmpty()){
        w.load(filePath);
    }else{
        w.fromClip();
    }
    if (w.sheetName().isEmpty()){

        w.showSheetDialog(false);
    }
    if (w.sheetName().isEmpty()){
        w.close();
        return 1;
    }
    w.show();
    return a.exec();

}

