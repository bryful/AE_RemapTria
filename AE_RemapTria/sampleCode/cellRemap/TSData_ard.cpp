#include "TSData.h"

//------------------------------------------------------
int FindARD_Tag(QString tag,QStringList lst)
{
    int ret = -1;
    if (tag.isEmpty()) return ret;
    tag = tag.toLower();
    for ( int i=0; i<lst.count();i++)
    {
        if (lst[i].toLower()==tag)
        {
            ret = i;
            break;
        }
    }
    return ret;
}
//------------------------------------------------------
int FindARD_TagNext(int idx,QStringList lst)
{
    int ret = -1;
    for ( int i=idx; i<lst.count();i++)
    {
        if (lst[i][0]=='*')
        {
            ret = i;
            break;
        }
    }
    return ret;
}
//------------------------------------------------------
QString FindARD_TagValue(QString tag,QStringList lst)
{
    QString ret = "";
    tag = tag.toLower();
    for ( int i=1; i<lst.count();i++)
    {
        QString line = lst[i].toLower();
        if(line.indexOf(tag)==0){
            QStringList s = line.split('\t');
            if (s.length()>=2){
                ret = s[1];
                break;
            }
        }
    }
    return ret;
}
//--------------------------------------------------------------------------------------
bool TSData::loadARD(QString p)
{
    bool ret = false;
    QFile f(p);
    if (! f.exists()) return ret;

    //ラインごとに読み込み
    QStringList lines;
    if (f.open(QIODevice::ReadOnly | QIODevice::Text))
    {
       QTextStream in(&f);
       in.setCodec("Shift-JIS");
       while (!in.atEnd()) {
           QString line =in.readLine();
           if (! line.isEmpty()){
                lines.append(line);
           }
       }
       f.close();
    }
    if (lines.size()<=1) return ret;
    if (lines[0] != "#TimeSheetGrid SheetData") return ret;
    m_sheetName = FsU::getFileNameWithoutExt(p);
    int c=0;
    int fr=0;
    int fps = 0;
    bool ok;
    QString v = FindARD_TagValue("LayerCoun",lines);
    if ( v!=""){
        c = v.toInt(&ok,10);
    }
    v = FindARD_TagValue("FrameCount",lines);
    if ( v!=""){
        fr = v.toInt(&ok,10);
    }
    v = FindARD_TagValue("CmpFps",lines);
     if ( v!=""){
         double d;
         d= v.toDouble(&ok);
         if (ok==true) {
             fps = (int)(d+0.5);
         }
     }
     if ((c<=0)||(fr<=0)||(fps<=0)) return ret;
     setFrameRate(fps);
     setSize(c,fr);
     int idx0 = FindARD_Tag("*CellName",lines)+1;
     int idx1 = FindARD_TagNext(idx0,lines);
     for (int i = idx0; i <  idx1; i++)
     {
         QString line = lines[i].trimmed();
         QStringList sa = line.split('\t');
         if (sa.length()<2) continue;
         int v2 = sa[0].toInt(&ok,10);
         if ( ok==true){
             m_caption[v2] = sa[1].trimmed();
         }
     }
     idx0 = FindARD_Tag("*CellDataStart",lines)+1;
     idx1 = FindARD_Tag("*End",lines);
     int tc = 0;

     cellsAllMinusTo();
     for (int i = idx0; i < idx1; i++)
     {
        QString line = lines[i].trimmed();
        QStringList sa = line.split('\t');
        if (sa.length()<2) continue;
        QString tag = sa[0].trimmed();
        if ( tag == "*Cell")
        {
            tc = sa[1].toInt(&ok);
            if ( ok==false) tc =-1;
        }
        else if (tag == "*CellEnd")
        {
        }
        else
        {
            if (tc>=0){
                int frm = sa[0].toInt(&ok,10);
                if ( ok==false) frm=-1;
                int num = sa[1].toInt(&ok,10);
                if ( ok==false) num=-1;
                if (frm>=0){
                    m_cells[tc][frm] = num;
                }
            }
        }
     }
     ret = true;
     cellsAllMinusFrom();
    return ret;
}
//--------------------------------------------------------------------------------------
