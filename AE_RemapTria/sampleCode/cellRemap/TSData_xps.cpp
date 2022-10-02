#include "TSData.h"
//------------------------------------------------------
QString FindXPS_tag(QStringList lines,QString tag)
{
    QString ret ="";
    tag = tag.toUpper();
    for ( int i=1;i<lines.count();i++){
        QString line = lines[i].trimmed().toUpper();
        QStringList sa = line.split('=');
        if ( sa.length()>=2){
            if (sa[0]==tag){
                ret = sa[1];
                if (ret.isEmpty()==false){
                    int idx = ret.indexOf(".");
                    if (idx>=0){
                        ret = ret.left(idx);
                    }
                    break;
                }
            }
        }
    }
    return ret;
}
//------------------------------------------------------
double FindXPS_FrameRete(QStringList lines)
{
    double ret = 24;
    QString fpsS = FindXPS_tag(lines,"##FRAME_RATE");
    if ( fpsS.isNull()==false)
    {
        bool ok;
        double d = fpsS.toDouble(&ok);
        if (ok) ret = d;
    }

    return ret;
}

//------------------------------------------------------
int XPS_SEC(QString s,int fps)
{
    int ret = 0;

    QStringList sa = s.split('+');
    if (sa.length()>=2){

        bool ok;
        int i0 = sa[0].trimmed().toInt(&ok,10);
        if (ok) ret = i0 * fps;
        int i1 = sa[1].trimmed().toInt(&ok,10);
        if (ok) ret += i1;
//        QTextStream out(stdout);
//        out << "s:"+s+" s[0]:"+sa[0]+" s[1]:"+sa[1]+"\n";
//        out << QString("i0:%1  i1:%2  ret:%3\n").arg(i0).arg(i1).arg(ret);
    }
    return ret;
}

//------------------------------------------------------
int FindXPS_TIME(QStringList lines,int fps)
{
    int ret = 0;
    QString v = FindXPS_tag(lines,"##TIME");
    if ( v.isNull()==false)
    {
        ret = XPS_SEC(v,fps);
    }
    return ret;
}
//------------------------------------------------------
int FindXPS_TRIN(QStringList lines,int fps)
{
    int ret = 0;
    QString v = FindXPS_tag(lines,"##TRIN");
    if ( v.isNull()==false)
    {
        ret = XPS_SEC(v,fps);
    }
    return ret;
}
//------------------------------------------------------
int FindXPS_TROUT(QStringList lines,int fps)
{
    int ret = 0;
    QString v = FindXPS_tag(lines,"##TROUT");
    if ( v.isNull()==false)
    {
        ret = XPS_SEC(v,fps);
    }
    return ret;
}

//------------------------------------------------------
QVector<bool> FindXPS_option(QStringList lines)
{
    QVector<bool> ret;
    for ( int i=1;i<lines.count();i++){
        QString line = lines[i].trimmed().toLower();
        if ( line.indexOf("[option")==0){
            QStringList sa = line.split('\t');
            for ( int i=0; i<sa.length();i++){
                ret.append(sa[i]=="timing");
            }
            break;
        }
    }
    return ret;
}
//------------------------------------------------------
QStringList FindXPS_caption(QStringList lines,QVector<bool> el)
{
    QStringList ret;
    for ( int i=1;i<lines.count();i++){
        QString line = lines[i].trimmed();
        if ( line.indexOf("[CELL")==0){
            QStringList sa = line.split('\t');
            for ( int i=0; i<sa.length();i++){
                if ( i<el.count()) {
                    if (el[i]==true){
                        ret.append(sa[i]);
                    }
                }
            }
            break;
        }
    }
    return ret;
}
//------------------------------------------------------
QStringList FindXPS_cellLine(QStringList lines)
{
    QStringList ret;
    for ( int i=1;i<lines.count();i++){
        QString line = lines[i];
        if (line.isEmpty()==true)continue;
        if ( line[0]=='.'){
            ret.append(line);
        }
    }
    return ret;
}

//------------------------------------------------------
QVector< QVector<int> > FindXPS_cell(QStringList lines,QVector<bool> el)
{
    QVector< QVector<int> > ret;

    QStringList lst = FindXPS_cellLine(lines);

    if ( lst.count()<=0) return ret;

    for ( int i=0;i<lst.count();i++){
        QVector<int> a;
        QStringList sa = lst[i].split('\t');
        for ( int i=0; i<sa.length();i++){
            if ( i<el.count()) {
                if (el[i]==true){
                    QString sv = sa[i].toLower();
                    int v=-1;
                    if ((sv.isEmpty()==true)||(sv=="|")){
                        v =-1;
                    }else if ((sv=="x")||(sv=="繧ｫ繝ｩ")){
                        v=0;
                    }else{
                        if (sv[0]=='(') sv = sv.right(sv.length()-1);
                        if (sv[sv.length()-1]==')') sv = sv.left(sv.length()-1);
                        bool ok;
                        v = sv.toInt(&ok,10);
                        if (ok==false) v=-1;
                    }
                    a.append(v);
                }
            }
        }
        ret.append(a);
        a.clear();
    }
    return ret;
}
//------------------------------------------------------
bool TSData::loadXPS(QString p)
{
    bool ret = false;
    QFile xps(p);

    QString buf;
    if (xps.open(QIODevice::ReadOnly))
    {
        QTextStream in(&xps);
        in.setCodec("shift-jis");
        buf = in.readAll();
        xps.close();
    }
    buf = buf.replace("\r\n","\n");
    QStringList lines = buf.split('\n');
    if (lines[0].indexOf("nasTIME-SHEET")<0) return ret;
    m_sheetName = FsU::getFileNameWithoutExt(p);
    double fps = 24;
    int c = 0;
    int f = 0;

    fps = FindXPS_FrameRete(lines);
    int fpsI = (int)(fps+0.5);

    int t = FindXPS_TIME(lines,fpsI);
    int ti = FindXPS_TRIN(lines,fpsI);
    int to = FindXPS_TROUT(lines,fpsI);

    f = t + (ti/2)+ (to/2);

    QVector<bool> el = FindXPS_option(lines);
    if (el.count()>0){
        c = 0;
        for ( int i=0;i<el.count();i++){
            if ( el[i]==true) c++;
        }
    }
    if ((c<=0)||(f<=0)||(fpsI<=0)) return ret;
    setSize(c,f);
    setFrameRate(fpsI);
    m_sheetName = FsU::getFileNameWithoutExt(p);

    QStringList cn = FindXPS_caption(lines,el);
    for (int i=0;i<cn.count();i++) m_caption[i],cn[i];

    QVector< QVector<int> >cel = FindXPS_cell(lines,el);


    cellsAllMinusTo();
    for ( int j=0;j<f;j++){
        for ( int i=0;i<c;i++){
            int v = cel[j][i];
            if (v>=0)  m_cells[i][j] = v;
        }
    }
    cellsAllMinusFrom();

    ret =true;
    return ret;
}

