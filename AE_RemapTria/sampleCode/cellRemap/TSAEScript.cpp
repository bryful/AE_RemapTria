#include "TSAEScript.h"

//********************************************************
TSAEScript::TSAEScript(TSData *d,TSSelection *s,TSPref *p,QObject *parent) :
    QObject(parent)
{
    QString ap = QDir::tempPath();
    m_scriptFilename = "C:/temp.jsx";
    m_resultFilename = "C:/result.jsx";
    m_timerCounter = 0;
    m_timerCounterMax = 10;
    m_loopTime = 250;

    connect(&m_timer,SIGNAL(timeout()),this,SLOT(fromRemapLoop()));

    m_data  = d;
    m_sel   = s;
    m_pref  = p;
}
//********************************************************
void TSAEScript::SetParams(TSData *d,TSSelection *s,TSPref *p)
{
    m_data  = d;
    m_sel   = s;
    m_pref  = p;
}
//********************************************************
TSAEScript::~TSAEScript()
{
}
//********************************************************
void TSAEScript::setIsJsxTemp(bool b)
{
    if (b){
        m_scriptFilename = QDir::tempPath()+"/temp.jsx";
        m_resultFilename = QDir::tempPath()+"/result.jsx";
    }else{
        m_scriptFilename = QDir::homePath()+"/temp.jsx";
        m_resultFilename = QDir::homePath()+"/result.jsx";
    }

}

//********************************************************
bool TSAEScript::sendScriptFile(QString p)
{
    bool ret = false;
#if defined(Q_OS_WIN)
    QFile f(p);
    if (f.exists()) {
        QString prg ="\""+m_pref->afterFXPath()+"\"";
        QStringList args;
        args.append("-r");
        args.append(m_scriptFilename.replace("/","\\"));
        QProcess ps;
        ps.startDetached(prg,args);
        ret = true;
    }
#elif defined(Q_OS_MAC)
    QString aScript =
                "tell application \"$CS\"\n"
                "    activate\n"
                "    DoScript \"$FILE\"\n"
                "end tell\n";
    aScript = aScript.replace("$CS",m_pref->afterFXName());
    aScript = aScript.replace("$FILE",p);

    QString osascript = "/usr/bin/osascript";
    QStringList processArguments;
    processArguments << "-l" << "AppleScript";

    QProcess ps;
    ps.start(osascript, processArguments);
    ps.write(aScript.toUtf8());
    ps.closeWriteChannel();
    ps.waitForReadyRead(-1);
    ret = true;
#endif
    return ret;

}
//********************************************************
bool TSAEScript::sendScript(QString code)
{
    bool ret = false;
#if defined(Q_OS_WIN)

    if (m_pref->isJsxTemp()){
        m_scriptFilename = QDir::tempPath() +"/temp.jsx";

    }else{
        m_scriptFilename = "C:/temp.jsx";
    }

    QFile f(m_scriptFilename);
    if (f.exists()) f.remove();
    if (f.open(QFile::WriteOnly)){
        QTextStream out(&f);
        out.setCodec("UTF-8");
        out << code;
        f.close();
     }
    if (f.exists()) {
        QString prg ="\""+m_pref->afterFXPath()+"\"";
        QStringList args;
        args.append("-r");
        QString ss = m_scriptFilename.replace("/","\\");
        args.append(ss);
        QProcess ps;
        ps.startDetached(prg,args);
        ret = true;
    }
#elif defined(Q_OS_MAC)
    QString aScript =
                "tell application \"$CS\"\n"
                "    activate\n"
                "    DoScript \"$CODE\"\n"
                "end tell\n";
    aScript = aScript.replace("$CS",m_pref->afterFXName());
    code =code.replace("\\","\\\\");
    code =code.replace("\"","\\\"");
    aScript = aScript.replace("$CODE",code);

    QString osascript = "/usr/bin/osascript";
    QStringList processArguments;
    processArguments << "-l" << "AppleScript";

    QProcess ps;
    ps.start(osascript, processArguments);
    ps.write(aScript.toUtf8());
    ps.closeWriteChannel();
    ps.waitForReadyRead(-1);
    ret = true;
#endif
    return ret;

}

//********************************************************
bool TSAEScript::toRemap()
{
    bool ret = false;
    if ((m_data==0)||(m_sel==0)||(m_pref==0)) return ret;


    if ( m_data->isEmptyCell(m_sel->targetCell())){
        return false;
    }

    if (isOpenAfetrFX()==false) {
        return ret;
    }

    QString code = loadScript(TSSetRemap);
    if (code.isEmpty()) return ret;

    QList< QList<double> > keys = m_data->getKeys(m_sel->targetCell());

    code = code.replace("$duration",QString("%1").arg((double)m_data->frameCount() / m_data->frameRate()));
    double inP =0;
    for (int i=0;i<keys.size();i++){
        if (keys[i][1]>=0){
            inP = keys[i][0];
            break;
        }
    }
    code = code.replace("$inPoint",QString("%1").arg(inP));
    double outP = (double)m_data->frameCount() / m_data->frameRate();
    if (keys[keys.size()-1][1]<0) {
        outP = keys[keys.size()-1][0];
    }
    code = code.replace("$outPoint",QString("%1").arg(outP));
    QString remap = "";
    for ( int i=0; i<keys.size();i++){
        QString d = QString("[%1,%2]").arg(keys[i][0]).arg(keys[i][1]);
        if (! remap.isEmpty()) remap += ",";
        remap += d;
    }
    remap = "[" + remap +"]";
    code = code.replace("$remap",remap);

    return sendScript(code);

}
//********************************************************
bool TSAEScript::toExpression()
{
    bool ret = false;
    if ((m_data==0)||(m_sel==0)||(m_pref==0)) return ret;


    if ( m_data->isEmptyCell(m_sel->targetCell())){
        return false;
    }

    if (isOpenAfetrFX()==false) {
        return ret;
    }

    QString code = loadScript(TSEXPRESSION);
    if (code.isEmpty()) return ret;



    code.replace("$FPS",QString("%1").arg(m_data->frameRate()));
    bool isCS4 = (m_pref->afterFXPath().indexOf("CS4")>=0);
    if(isCS4){
        code.replace("$SLIDER",tr("SLIDERCS4"));
    }else{
        code.replace("$SLIDER",tr("SLIDERCS5"));
    }
    QString cells;
    QList< QList<int> > keys = m_data->cellKeys(m_sel->targetCell());
    if(keys.size()>0){
        for (int i=0; i<keys.size();i++){
            QString line;
            line = QString("\t%1\t%2\t\r\n").arg(keys[i][0]).arg(keys[i][1]);
            cells += line;
        }
    }
    code.replace("$CELLS",cells);

    QClipboard *q =  QApplication::clipboard();
    q->setText(code);

    return true;
}

//********************************************************
bool TSAEScript::toXMP()
{
    bool ret = false;
    if ((m_data==0)||(m_sel==0)||(m_pref==0)) return ret;

    if (isOpenAfetrFX()==false) return ret;

    QString p = QStandardPaths::writableLocation(QStandardPaths::TempLocation)+"/temp.ardj";

    QString js = m_data->toJson();
    if (js.isEmpty()) return ret;

    QFile f(p);
    if (f.exists()) f.remove();

    if (f.open(QFile::WriteOnly)){
        QTextStream out(&f);
        out.setCodec("UTF-8");
        out << js;
        f.close();
     }
    if (f.exists()==false) return ret;

    QString code = "try{XMPSHEET.importFile(\"$PATH\",true);}catch(e){alert(e.toString());}";
    code = code.replace("$PATH",p);
    return sendScript(code);
}
//*********************************************************************************************
bool TSAEScript::fromRemap()
{
    bool ret = false;
    m_timerCounter = 0;
    if (isOpenAfetrFX()==false) return ret;

    QString code = loadScript(TSGetRemap);
    if (code.isEmpty()){
        return ret;
    }
    code = code.replace("$SAVEPATH",m_resultFilename);
    code = code.replace("$INDEX",QString("%1").arg(m_sel->targetCell()));

    QFile f(m_resultFilename);
    if (f.exists()) f.remove();
    sendScript(code);
    m_timer.start(m_loopTime);
    return true;
}


//*********************************************************************************************
void TSAEScript::fromRemapLoop()
{
    bool stp = false;
    bool ok = false;
    if (m_timerCounter>=m_timerCounterMax){
        stp=true;
    }
    QFile f(m_resultFilename);
    if (f.exists()){
        if (f.open(QFile::ReadOnly)){
            QTextStream in(&f);
            in.setCodec("UTF-8");
            QString js = in.readAll();
            f.close();
            ok = m_data->fromRemap(js);
         }
    }
    if ((stp==true)||(ok==true)){
        m_timer.stop();
        m_timerCounter = 0;
        FromRemapFinished(ok);
    }
    m_timerCounter++;
}
//*********************************************************************************************
void TSAEScript::execAfterFX()
{
    bool b = isOpenAfetrFX();
    if (b) return;
#if defined(Q_OS_WIN)
    QString prg ="\""+m_pref->afterFXPath()+"\"";
    QProcess ps;
    ps.startDetached(prg);
#elif defined(Q_OS_MAC)
    QString prg ="open \""+m_pref->afterFXPath()+"\"";
    QProcess ps;
    ps.startDetached(prg);
#endif
}
//*********************************************************************************************
bool TSAEScript::isOpenAfetrFX()
{
    QProcess ps;
    QString cmd;
    QStringList arg;
    QString psName;
#if defined(Q_OS_WIN)
    cmd = "tasklist";
    psName ="AfterFX.exe";
#elif defined(Q_OS_MAC)
    cmd = "ps";
    arg.append("-e");
    psName ="After Effects";
#endif
    ps.start(cmd,arg);
    if (!ps.waitForStarted()) return true;
   ps.closeWriteChannel();
   if (!ps.waitForFinished()) return false;
   QByteArray result = ps.readAll();
   return (result.indexOf(psName)>=0);


}
//*********************************************************************************************
bool TSAEScript::makeFolder(QString p)
{
    bool ret = false;
    if (p.isEmpty()) return ret;
    QStringList sa = p.split("/");
    if (sa.size()<=1) return ret;
    QString pp = sa[0];

    ret = true;
    for (int i=1; i<sa.size();i++){
        QDir f(pp +"/" +sa[i]);
        if (f.exists()==false){
            QDir ff;
            if (pp.isEmpty()){
                ff =QDir("/");
            }else{
                ff =QDir(pp);
            }
            if ( ff.mkdir(sa[i])==false){
                ret = false;
                break;
            }
        }
        pp += "/" +sa[i];
    }
    return ret;
}
//*********************************************************************************************
QString TSAEScript::scriptsFolder()
{
    QString ret ="";
    QString  p = QStandardPaths::writableLocation(QStandardPaths::DataLocation)+"/"+TSScriptsFoler;
    QDir f(p);
    if (f.exists()==false){
        if (makeFolder(p))
            ret =p;
    }else{
        ret = p;
    }
//    qDebug() <<"DataLocation:"<<QStandardPaths::writableLocation(QStandardPaths::DataLocation);
//    qDebug() << "GenericDataLocation:" <<QStandardPaths::writableLocation(QStandardPaths::GenericDataLocation);
//    qDebug() <<"ApplicationsLocation:"<<QStandardPaths::writableLocation(QStandardPaths::ApplicationsLocation);
//    qDebug() <<"ConfigLocation:"<<QStandardPaths::writableLocation(QStandardPaths::ConfigLocation);
//    qDebug() <<"RuntimeLocation:"<<QStandardPaths::writableLocation(QStandardPaths::RuntimeLocation);
//    qDebug() <<"CacheLocation:"<<QStandardPaths::writableLocation(QStandardPaths::CacheLocation);
//    qDebug() <<"TempLocation:"<<QStandardPaths::writableLocation(QStandardPaths::TempLocation);

    return ret;
}
//*********************************************************************************************
void TSAEScript::exportJsxScript(QString s, bool isWrite)
{
    QString p = scriptsFolder();
    if (!p.isEmpty()){

        QFile ff(p + "/" + s);
        if ((ff.exists())&&(isWrite == false)) return;

        QFile f(":/jsx/"+s);
        f.open(QIODevice::ReadOnly);
        QTextStream in(&f);
        QString ret = in.readAll();
        f.close();
        if (!ret.isEmpty()) {
            if (ff.open(QFile::WriteOnly)){
                QTextStream out(&ff);
                out.setCodec("UTF-8");
                out << ret;
                ff.close();
             }
        }
    }
}
//*********************************************************************************************
QString TSAEScript::loadScript(QString s)
{
    QString ret = "";
    QString p = scriptsFolder();
    if (!p.isEmpty()){

        QFile ff(p + "/" + s);
        if (ff.exists()==true){
            if (ff.open(QFile::ReadOnly)){
                QTextStream in(&ff);
                in.setCodec("UTF-8");
                ret = in.readAll();
                ff.close();
             }
        }
        if (ret.isEmpty()){
            QFile f(":/jsx/"+s);
            f.open(QIODevice::ReadOnly);
            QTextStream in(&f);
            ret = in.readAll();
            f.close();
            if (ff.open(QFile::WriteOnly)){
                QTextStream out(&ff);
                out.setCodec("UTF-8");
                out << ret;
                ff.close();
             }
        }

    }
    return ret;
}
//*********************************************************************************************
void TSAEScript::exportJsxScriptALL(bool isWrite)
{
    exportJsxScript(TSSetRemap,isWrite);
    exportJsxScript(TSGetRemap,isWrite);
    exportJsxScript(TSEXPRESSION,isWrite);
}
//*********************************************************************************************
bool TSAEScript::instScriptSub(QString p, QString s)
{
    bool ret = false;

    QString code ="";
    QFile f(":/jsx/"+s);
    //qDebug() << p +"/" + s;
    f.open(QIODevice::ReadOnly);
    QTextStream in(&f);
    code = in.readAll();

    f.close();
    if (code.isEmpty()) return ret;

    QString ap = QApplication::applicationFilePath();
    if (ap.size()>2){
        if (ap[1]==':'){
            QString ap2 = "/";
                    ap2 +=  ap[0].toLower();
            ap =  ap2 + ap.mid(2);
        }
    }
        code = code.replace("$cellRemapPath",ap);

    QFile ff(p+"/" +s);
    if (ff.open(QFile::WriteOnly)){
        QTextStream out(&ff);
        out.setCodec("UTF-8");
        out << code;
        ff.close();
        ret = ff.exists();
     }
    return ret;
}
//*********************************************************************************************
bool TSAEScript::instScript(QString p)
{
    bool ret = true;
    if (instScriptSub(p +"/" + TSSTARTUP,TSXMPJSX) ==false) ret = false;
    if (instScriptSub(p +"/" + TSSTARTUP,TSJSONJSX) ==false) ret = false;
    if (instScriptSub(p +"/" + TSSCRIPTUI,TSCELLREMAPJSX) ==false) ret = false;
    return ret;
}
//*********************************************************************************************
bool TSAEScript::uninstScript(QString p)
{
    bool ret = true;
    QFile f(p +"/" + TSSTARTUP + "/" + TSXMPJSX);
    if (f.exists()) if ( f.remove()==false) ret = false;
    QFile f2(p +"/" + TSSTARTUP + "/" + TSJSONJSX);
    if (f2.exists()) if ( f2.remove()==false) ret = false;
    QFile f3(p +"/" + TSSCRIPTUI + "/" + TSCELLREMAPJSX);
    if (f3.exists()) if ( f3.remove()==false) ret = false;
    return ret;
}
//*********************************************************************************************
//*********************************************************************************************
void TSAEScript::openScriptFolder()
{
    QString p = scriptsFolder();
    QProcess ps;
#if defined(Q_OS_WIN)
    p.replace("/","\\");
    QString d ="EXPLORER ";
    d += " \"" + p + "\"";
    ps.execute( d );
#elif defined(Q_OS_MAC)
    QString cmd = "open ";
    cmd += "\"" + p +"\"";
    ps.execute(cmd);
#endif
}
