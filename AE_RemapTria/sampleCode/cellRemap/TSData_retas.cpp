#include "TSData.h"
//**********************************************************************
QString delWQ(QByteArray s)
{
    s = s.trimmed();
    if (s.isEmpty()) return "";
    if (s =="\"\"") return "";
    if (s.size()>0) if(s[0]=='"') s = s.mid(1);
    if (s.size()>0) if(s[s.size()-1]=='"') s = s.mid(0,s.size()-1);
    if (s.size()==4){
        if ( ((unsigned char)s[0] ==0x93)&&((unsigned char)s[1] ==0xAE)&&((unsigned char)s[2] ==0x89)&&((unsigned char)s[3] ==0xE6)){
            return "douga";
        }
    }else if (s.size()==2){
        if ( ((unsigned char)s[0] ==0x81)&&((unsigned char)s[1] ==0x7E)){
            return "0";
        }
    }

    QTextCodec *codec =QTextCodec::codecForName("shift-jis");

    return codec->toUnicode(s);

}
//**********************************************************************
int toNumber(QString s)
{
    int ret = -1;
    bool b = false;
    if (s.isEmpty()) return ret;
    for (int i=0; i<s.size(); i++){
        if ((s[i]<'0')||(s[i]>'9')) {
            return ret;
        }
    }
    ret = s.toInt(&b);
    if (!b)ret = -1;
    return ret;
}

//**********************************************************************
bool TSData::loadStylos(QString p)
{
    bool ret = false;
    QFile f(p);
    QList< QStringList > csv;
    if (f.exists()==false) return ret;
    if (f.open(QIODevice::ReadOnly | QIODevice::Text))
    {
        while(!f.atEnd()){
            QList<QByteArray> line = f.readLine().split(',');
            QStringList lineS;
            if (line.size()>0){
                for (int i=0; i<line.size();i++) {
                    lineS.append(  delWQ(line[i]));
                }
            }
            csv.append(lineS);
        }
        f.close();
    }
    if ((csv.size()<=0)||(csv[0].size()<=0)) return ret;
    if ( csv[0][0]!="Frame") return ret;
    int c = 0;
    int frm = 0;
    int idx =-1;
    for ( int i=0;i<csv[0].size();i++){
        if (csv[0][i].trimmed()=="douga"){
            idx = i;
            break;
        }
    }
    if(idx<0) return ret;
    m_sheetName = FsU::getFileNameWithoutExt(p);
    c = csv[0].size() - idx;
    frm = csv.size() -2;
    setSize(c, frm);

    setFrameRate(24);

    for ( int i=0 ; i<c; i++){
        int cc = i +idx;
        if (cc<csv[1].size()){
            setCaption(i,csv[1][cc]);
        }
    }

    cellsAllMinusTo();
    for ( int i=0;i<c;i++){
        int cc = i+idx;
        for ( int j=0;j< frm;j++){
            int ff = j +2;
            if ((ff<csv.size())&&(cc<csv[ff].size())){
                int v= toNumber(csv[ff][cc]);
                if (v>=0){
                    m_cells[i][j] = v;
                }
            }
        }

    }
    cellsAllMinusFrom();
    ret = true;
    return ret;

}
//**********************************************************************
