
#include "TSData.h"

//**********************************************************
TSData::TSData()
{
    m_ClipHeader = "#AF_Remap.exe clip";
    setSize(6,72);
    m_FrameRate = 24;
    m_sheetName = "";
    m_AfterFXPath = "";
    m_filePath = QDir::homePath();
    m_fileExt = "ardj";

}
//**********************************************************
void TSData::assign(TSData d)
{
    m_CellCount     = d.m_CellCount;
    m_FrameCount    = d.m_FrameCount;
    m_FrameRate     = d.m_FrameRate;
    m_sheetName       = d.m_sheetName;
    m_filePath          = d.m_filePath;
    m_fileExt          = d.m_fileExt;

    m_caption.clear();
    if (d.m_caption.length()>0){
        for (int i=0;i<d.m_caption.length();i++){
            m_caption.push_back(d.m_caption[i]);
        }
    }
    m_cells.clear();
    if (d.m_cells.length()>0){
        for (int c=0;c<d.m_cells.length();c++){
            QList<int> lyr;
            if (d.m_cells[0].length()>0){
                for (int f=0;f<d.m_cells[c].length();f++){
                    lyr.push_back(d.m_cells[c][f]);
                }
            }
            m_cells.push_back(lyr);
        }
    }
}

//**********************************************************
void TSData::setSize(int c, int f)
{
    int nc = m_cells.count();
    int nf = 0;
    if (nc>0) nf = m_cells[0].count();
    if (c==nc){
    }else if (c>nc){
        for (int i= nc; i<c;i++){
            QList <int> cc;
            if (nf>0)
                for ( int j =0; j<nf;j++)
                    cc.append(0);
            m_cells.append(cc);
            m_caption.append(QString('A'+i) );
        }
    } else{
        for (int i=c; i<nc;i++ )m_cells.removeLast();
    }
    if ( nf ==f){
    }else if ( f>nf){
        for ( int i=0; i<m_cells.count();i++){
            for ( int j = nf;j<f;j++){
                m_cells[i].append(0);
            }
        }
    }else{
        for ( int i=0; i<m_cells.count();i++){
            for ( int j = f;j<nf;j++){
                m_cells[i].removeLast();
            }
        }
    }
    m_CellCount = m_cells.count();
    if ( m_CellCount>0){
        m_FrameCount = m_cells[0].count();
    }else{
         m_FrameCount = 0;
    }
}
//**********************************************************
void TSData::setSheetName(QString s)
{
    m_sheetName = s.trimmed();
}

//**********************************************************
QString TSData::sheetName()
{
    return m_sheetName;
}

//**********************************************************
void TSData::cellInsert(int c,QString cap)
{
    CellArray n;
    for ( int i=0; i<m_FrameCount; i++) n.append(0);
    m_cells.insert(c,n);
    m_caption.insert(c,cap);
    m_CellCount += 1;
}
//**********************************************************
void TSData::cellRemove(int c)
{
    if((m_CellCount>1)&&(c>=0)&&(c<m_CellCount)){
        m_cells.removeAt(c);
        m_caption.removeAt(c);
        m_CellCount -=1;
    }
}

//**********************************************************
void TSData::setFrameRate(int fps)
{
    m_FrameRate = fps;
}
//**********************************************************
QString TSData::getCaption(int idx)
{
    if ( (idx>=0)&&(idx<m_CellCount)){
        return m_caption[idx];
    }else{
        return "?";
    }

}
//**********************************************************
 bool TSData::setCaption(int idx, QString cap)
{
    bool ret = false;
    if ( (idx>=0)&&(idx<m_CellCount)){
        m_caption[idx] = cap.trimmed();
        ret = true;
    }
    return ret;
}
 //**********************************************************
 void TSData::setNumRow(int c, int f, int v)
 {
     m_cells[c][f] = v;
 }
 //**********************************************************
 void TSData::cellsAllMinusTo()
 {
     if (m_cells.size()<=0) return;
     for (int c=0 ;c<m_cells.size();c++){
         int fcnt = m_cells[c].size();
         if (fcnt<=0) continue;
         m_cells[c][0] = 0;
         if (fcnt>1){
             for (int f=1 ;f<fcnt;f++){
                 m_cells[c][f] = -1;
             }
         }
     }
 }
 //**********************************************************
 void TSData::cellsAllMinusFrom()
 {
     if (m_cells.size()<=0) return;
     for (int c=0 ;c<m_cells.size();c++){
         int fcnt = m_cells[c].size();
         if (fcnt<=0) continue;
         if (m_cells[c][0]<0) m_cells[c][0] = 0;
         if (fcnt>1){
             for (int f=1 ;f<fcnt;f++){
                 if (m_cells[c][f]<0) {
                     m_cells[c][f] = m_cells[c][f-1];
                 }
             }
         }
     }
 }

//**********************************************************
int TSData::getNum(int c,int f){
    int ret = 0;
    if ( (c>=0)&&(c<m_CellCount)&&(f>=0)&&(f<m_FrameCount)){
        ret = m_cells[c][f];
    }
    return ret;
}
//**********************************************************
int TSData::getNumType(int c,int f)
{
    int ret = CellNumNone;
    if ( (c>=0)&&(c<m_CellCount)&&(f>=0)&&(f<m_FrameCount)){
        int v = m_cells[c][f];
        if (f==0){
            if (v==0){
               ret = CellNumZeroStart;
            }else{
                ret = v;
            }
        }else{
            int bv = m_cells[c][f-1];
            if (v==bv){
                if (v==0){
                    ret = CellNumZeroSerial;
                }else{
                    ret = CellNumSerial;
                }
            }else{
                if (v==0){
                    ret = CellNumZeroStart;
                }else{
                    ret = v;
                }
            }
        }
    }
    return ret;
}
//**********************************************************
bool TSData::setNum(int c,int f,int num)
{
    bool ret = false;
    if ( (c>=0)&&(c<m_CellCount))
        if ( (f>=0)&&(f<m_FrameCount)){
            m_cells[c][f] = num;
            ret = true;
        }
    return ret;
}
//**********************************************************
QString TSData::secStr()
{
    int s = m_FrameCount / m_FrameRate;
    int k = m_FrameCount % m_FrameRate;
    return QString("%1+%2").arg(s).arg(k);
}

//**********************************************************
QString TSData::infoStr()
{
    QString r = QString("[%1] %2(%3), fps:%4").arg(m_sheetName).arg(secStr()).arg(m_FrameCount).arg(m_FrameRate);
    if (modiFlag) r += "*";
    return r;
}
//**********************************************************
QJsonArray TSData:: captionToJ()
{
    QJsonArray ja;
    if (m_caption.size()>0){
        for (int i=0;i<m_caption.size();i++) ja.append(m_caption[i]);
    }
    return ja;
}
//**********************************************************
void TSData::toStdout()
{
    m_AfterFXPath = "";
    QString js = toJson();
    if (!js.isEmpty()){
        QTextStream outputStream(stdout);
        outputStream << js << endl;
    }

}
//**********************************************************
void TSData::toStdoutAE()
{
    m_AfterFXPath = "";
    QString js = toJsonAE();
    if (!js.isEmpty()){
        QTextStream outputStream(stdout);
        outputStream << js << endl;
    }

}
#ifndef CONSOLE_MODE
//**********************************************************
bool TSData::toClipBoard()
{
    bool ret = false;
    m_AfterFXPath = "";
    QString js = toJson();

    if (!js.isEmpty()){
        QClipboard *clipboard = QApplication::clipboard();
        clipboard->setText(js);
        ret = true;
    }
    return ret;
}
//**********************************************************
bool TSData::fromClipBoard()
{
    bool ret = false;
    m_AfterFXPath = "";
    QClipboard *clipboard = QApplication::clipboard();
    QString Text = clipboard->text().trimmed();
    if (!Text.isEmpty()){
        if ((Text[0] == '{')||(Text[0] == '(')){
#if defined(Q_OS_MAC)
            ret = fromJson(Text.toUtf8());
#elif defined(Q_OS_WIN)
            ret = fromJson(Text.toLocal8Bit());
#endif
        }
    }
    return ret;
}
#endif
//**********************************************************
bool TSData::isEmptyCell(int c)
{
    bool ret = true;
    if ((c<0)||(c>=m_CellCount)) return ret;
    for (int f=0;f<m_cells[c].size();f++)
    {
        if (m_cells[c][f]>0){
            ret = false;
            break;
        }
    }
     return ret;
}

//**********************************************************
bool TSData::fromRemap(QString s)
{
    bool ret = false;
    QByteArray js = s.toUtf8();
    js = aeToJson(js);
    if (js.isEmpty() ) return false;
    QJsonDocument jd = QJsonDocument::fromJson(js);
    if ( (js.isEmpty())||(js.isNull())) return ret;
    QJsonObject jo = jd.object();
    int index = 0;
    if (! jo["index"].isUndefined()){
        index = (int)jo["index"].toDouble();
    }
    if ((index<0)||(index>=m_CellCount)) return ret;

    QJsonArray ja;
    if (! jo["cell"].isUndefined()){
        QList<int> cell;
        cell.append(0);
        for (int i=1;i<m_FrameCount;i++) cell.append(-1);
        ja = jo["cell"].toArray();
        if (ja.size()>0){
            for (int i=0;i<ja.size();i++){
                QJsonArray key = ja[i].toArray();
                if(key.size()>=2){
                    int frm =(int)key[0].toDouble();
                    int num =(int)key[1].toDouble();
                    if ((frm>=0)&&(frm<m_FrameCount)){
                        cell[frm] = num;
                    }
                }
            }
        }
        for (int i=1;i<m_FrameCount;i++) {
            if (cell[i]<0) cell[i] = cell[i-1];
        }
        for (int i=0;i<m_FrameCount;i++) m_cells[index][i] = cell[i];
        ret = true;
    }
    return ret;
}
//**********************************************************
int TSData::lastIndexOf(QString s,QChar c)
{
    int ret = -1;
    if (s.isEmpty()) return ret;
    for (int i=s.size()-1;i>=0;i--){
        if (s[i]==c){
            ret = i;
            break;
        }
    }
    return ret;
}

//**********************************************************
void TSData::splitName(QString s)
{
    if ( s.isEmpty()) return;
    s = s.replace("\\","/");
    m_fileExt = FsU::getExt(s);
    if (m_sheetName.isEmpty()){
        m_sheetName = FsU::getFileNameWithoutExt(s);
    }
    m_filePath = FsU::getDirectoryName(s);
    if (m_filePath.isEmpty()){
        m_filePath = QDir::homePath();
    }

}
//**********************************************************
QString TSData::fileFullName()
{
    QString ret ="";
    ret +=m_filePath;
    if (!ret.isEmpty()) ret +="/";
    if(m_sheetName.isEmpty()){
        ret += "notitle";
    }else{
        ret +=m_sheetName;
    }
    ret +="."+m_fileExt;
    return ret;
}
//**********************************************************
bool TSData::swapCell(int s,int d)
{
    if ( (s==d)||(s<0)||(d<0)||(s>=m_CellCount)||(d>=m_CellCount)) return false;

    for ( int i=0; i<m_FrameCount;i++){
        int t = m_cells[s][i];
        m_cells[s][i] = m_cells[d][i];
        m_cells[d][i] = t;
    }
    QString ts = m_caption[s];
    m_caption[s] = m_caption[d];
    m_caption[d] = ts;
    return true;
}

//**********************************************************
//**********************************************************
void TSData::frameInsert(int c ,int s,int l,bool IsAll,bool isLength)
{
    if (s<0) s= 0;
    if (l>=m_FrameCount) l = m_FrameCount -1;
    int len = l -s +1;
    if (IsAll==false){
        if ((c<0)||(c>=m_CellCount)) return;
    }else{
        if (c<0) c=0;
        else if(c>=m_CellCount) c = m_CellCount-1;

    }
    for (int i= 0;i<len;i++){
        if(IsAll){
            for (int j=0;j<m_CellCount;j++) m_cells[j].insert(s,0);
        }else{
            m_cells[c].insert(s,0);
        }
    }
    if(! isLength){
        if(IsAll){
            for (int j=0;j<m_CellCount;j++) {
                for (int i= 0;i<len;i++){
                    m_cells[j].removeLast();
                }
            }
        }else{
            for (int i= 0;i<len;i++){
                m_cells[c].removeLast();
            }
        }
    }else{
        m_FrameCount = m_cells[c].size();
    }
}
//**********************************************************
void TSData::frameDelete(int c ,int s,int l,bool IsAll,bool isLength)
{
    if (s<0) s= 0;
    if (l>=m_FrameCount) l = m_FrameCount -1;
    int len = l -s +1;

    if (IsAll==false){
        if ((c<0)||(c>=m_CellCount)) return;
    }else{
        if (c<0) c=0;
        else if(c>=m_CellCount) c = m_CellCount-1;

    }
    for (int i= 0;i<len;i++){
        if(IsAll){
            for (int j=0;j<m_CellCount;j++) m_cells[j].removeAt(s);
        }else{
            m_cells[c].removeAt(s);
        }
    }
    if(! isLength){
        if(IsAll){
            for (int j=0;j<m_CellCount;j++) {
                for (int i= 0;i<len;i++){
                    m_cells[j].append(0);
                }
            }
        }else{
            for (int i= 0;i<len;i++){
                m_cells[c].append(0);
            }
        }
    }else{
        m_FrameCount = m_cells[c].size();
    }

}
