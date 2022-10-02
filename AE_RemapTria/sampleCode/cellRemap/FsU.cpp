#include "FsU.h"

//--------------------------------------------------------------------------
FsU::FsU()
{
}
//--------------------------------------------------------------------------
QString FsU::pathString(QString p)
{
    QString ret = p.replace("\\","/");
    return ret;
}

//--------------------------------------------------------------------------
QString FsU::getExt(QString p)
{
    QString ret = "";
    int idx = p.lastIndexOf('.');
    if (idx>=0){
        ret = p.mid(idx);
    }
    return ret;
}
//--------------------------------------------------------------------------
QString FsU::getFileNameWithoutExt(QString p)
{
    QString ret = p;
    int idx = p.lastIndexOf('.');
    if (idx>=0){
        ret = p.mid(0,idx);
    }
    idx = p.lastIndexOf('/');

    if (idx<0){
        //
    }else if (idx==ret.size()-1){
        ret = "";
    }else{
        ret = ret.mid(idx+1);

    }
    return ret;

}
//--------------------------------------------------------------------------
QString FsU::getFileName(QString p)
{
    QString ret = "";
    int idx = p.lastIndexOf('/');

    if ( idx == p.size()-1){
        //
    } else if (idx>=0){
        ret = p.mid(idx+1);
    }else{
        ret = p;
    }
    return ret;
}
//--------------------------------------------------------------------------
QString FsU::getDirectoryName(QString p)
{
    QString ret = "";
    int idx = p.lastIndexOf('/');
    if ( idx == p.size()-1){
        ret = p.left(p.size()-1);
    }else if(idx==0){
        ret = "";
    }else if (idx>0){
        ret = p.mid(0,idx);
    }
    return ret;
}

//--------------------------------------------------------------------------
QString  FsU::changeExt(QString p, QString e)
{
    QString ret = "";
    int idx = p.lastIndexOf('.');
    QString node;

    if (idx>=0){
        node = p.mid(0,idx);
    }else{
        node = p;
    }
    if (! e.isEmpty()){
        if (e[0] != '.') e = "."  + e;
    }
    return node + e;
}
//--------------------------------------------------------------------------
QString  FsU::combine(QString p, QString n)
{
    QString ret = "";
    if (p.isEmpty()){
        ret = n;
    }else{
        ret = p;
        if (p[p.size()-1]!='/') ret += "/";
        ret += n;
    }
    return ret;
}
//--------------------------------------------------------------------------
bool FsU::isBOM(QByteArray p)
{
    bool ret = false;
    if ( p.size()>=3){
        ret =( ((unsigned char)p[0]==0xEF)&&((unsigned char)p[1]==0xBB)&&((unsigned char)p[2]==0xBF));
    }
    return ret;
}
//-------------------------------------------------------
QByteArray FsU::cutBOM(QByteArray p)
{
    if (isBOM(p)){
        return p.mid(3);
    }else{
        return p;
    }
}
//-------------------------------------------------------
QByteArray FsU::BOM()
{
    QByteArray ret = "";
    ret.append((unsigned char)0xEF);
    ret.append((unsigned char)0xBB);
    ret.append((unsigned char)0xBF);
    return ret;
}
//-------------------------------------------------------
int FsU::colToInt(QColor c)
{
    return (c.red() << 16) | (c.green() <<8) | c.blue();
}

//---------------------------------------------------------------------
QColor FsU::intToCol(int v)
{
    return QColor((v>>16) & 0xFF,(v>>8)&0xFF,v & 0xFF);
}
//---------------------------------------------------------------------
QJsonArray FsU::colToJa(QColor c)
{
    QJsonArray ja;
    ja.append(c.red());
    ja.append(c.green());
    ja.append(c.blue());
    return  ja;
}
//---------------------------------------------------------------------
QColor FsU::jaToCol(QJsonValue v,QColor c)
{
    QColor ret = c;
    if (v.isArray()){
        QJsonArray ja = v.toArray();
        if (ja.size()>=3){
            ret = QColor::fromRgb((int) ja[0].toDouble(),ja[1].toDouble(),ja[2].toDouble());
        }
    }
    return ret;
}
//---------------------------------------------------------------------
QByteArray FsU::aeToJson(QByteArray s)
{
    if (s.size()<=0) return s;
    s = s.trimmed();
    //BOM
    s = cutBOM(s);
    //()
    if (s.size()>2){
        if( (s[0]=='(')&&(s[s.size()-1]==')')){
            s =s.mid(1,s.size()-2);
        }
    }

    QByteArray ret = "";
    int cnt = s.size();
    if (cnt<=0) return ret;
    QByteArray node = "";
    int idx = 0;
    while(idx<cnt)
    {
        if ( (s[idx]=='{')||(s[idx]=='}')||(s[idx]==',')){
            ret += node + s[idx];
            node = "";
        }else if (s[idx]==':'){
            QByteRef c = ret[ret.size()-1];
            if ((c=='{')||(c==',')){
                node = node.trimmed();
                if(node.size()>0) if (node[0]!='\"') node = "\""+node;
                if(node.size()>0) if (node[node.size()-1]!='\"') node = node + "\"";
                ret += node +":";
                node = "";
            }else{
                node +=s[idx];
            }

        }else{
            node +=s[idx];
        }
        idx++;
    }
    if(! node.isEmpty()) ret += node;
    return ret;
}
//---------------------------------------------------------------------
QJsonArray FsU::listToJa(QStringList lst)
{
    QJsonArray ja;
    if (lst.size()>0){
        for ( int i=0;i<lst.size();i++)
            ja.append(lst[i]);
    }
    return  ja;
}
//---------------------------------------------------------------------
QJsonArray FsU::aeListToJa(QStringList lst)
{
    QJsonArray ja;
    if (lst.size()>0){
        for ( int i=0;i<lst.size();i++)
#if defined(Q_OS_WIN)
            ja.append( pathWinToJS( lst[i]));
#elif defined(Q_OS_MAC)
            ja.append(lst[i]);
#endif
    }
    return  ja;
}
//---------------------------------------------------------------------
QStringList FsU::jaTolist(QJsonArray ja)
{
    QStringList ret;
    if (ja.size()>0){
        for(int i=0; i<ja.size();i++){
            ret.append(ja[i].toString());
        }
    }
    return ret;
}
//---------------------------------------------------------------------
QStringList FsU::jaToAeList(QJsonValue v)
{
    QStringList ret;
    if (v.isArray()){
        QJsonArray ja = v.toArray();
        if (ja.size()>0){
            for(int i=0; i<ja.size();i++){
#if defined(Q_OS_WIN)
                ret.append(pathJSToWin(ja[i].toString()));
#elif defined(Q_OS_MAC)
                ret.append(ja[i].toString());
#endif
            }
        }
    }
    return ret;
}

//---------------------------------------------------------------------
QString FsU::pathWinToJS(QString p)
{
    QString ret = p;
    if (!ret.isEmpty()){
        if(ret.size()>=2){
            if (ret[1]==':'){
                QString ret2 = "/";
                ret2 += ret[0].toLower();
                ret2 += ret.mid(2);
                ret = ret2;
            }
        }
    }
    return ret;
}
//---------------------------------------------------------------------
QString FsU::pathJSToWin(QString p)
{
    QString ret = p;
    if (!ret.isEmpty()){
        if(p.size()>=3){
            if ((ret[0]=='/')&&(ret[2]=='/')){
                if ((ret[1]>='a')&&(ret[1]<='z')){
                    QString ret2;
                    ret2 =ret[1].toUpper();
                    ret2 +=  ":" +ret.mid(2);
                    ret = ret2;
                }
            }
        }
    }
    return ret;

}
//---------------------------------------------------------------------
int FsU::indexOfFrameStr(QString p)
{
    int ret = -1;
    if (p.isEmpty()) return ret;
    int e = p.size()-1;
    ret = 0;
    for (int i= e; i>=0;i--){
        QChar c = p[i];
        if ((c<'0')||(c>'9')){
            if (i==e){
                ret = -1;
            }else{
                ret = i+1;
            }
            break;
        }
    }
    return ret;
}
//---------------------------------------------------------------------
QStringList FsU::getFile(QString p)
{
    QStringList ret;
    if (p.isEmpty()) return ret;
    p = p.replace("\\","/");
    QDir d(p);
    if (d.exists()){
        QStringList r = d.entryList(QDir::Files,QDir::Name);
        if(r.size()>0){
            for (int i=0; i<r.size();i++){
                if ((r[i]==".")||(r[i]=="..")) continue;
                ret.append(p +"/" + r[i]);
            }
        }

    }
    return ret;
}

//---------------------------------------------------------------------
QStringList FsU::getDir(QString p)
{
    QStringList ret;
    p = p.replace("\\","/");
    QDir d(p);
    if (d.exists()){
        QStringList r = d.entryList(QDir::Dirs,QDir::Name);
        if(r.size()>0){
            for (int i=0; i<r.size();i++){
                if ((r[i]==".")||(r[i]=="..")) continue;
                ret.append(p +"/" + r[i]);
            }
        }
    }
    return ret;

}
//---------------------------------------------------------------------
bool FsU::makeFolder(QString p)
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

//---------------------------------------------------------------------
//QStringList FsU::getVolumes(QString p)
//{

//}
