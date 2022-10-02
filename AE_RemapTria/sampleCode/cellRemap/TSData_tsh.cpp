#include "TSData.h"


bool TSData::loadTSH(QString p)
{
    bool ret = false;
    QFile f(p);
    if (! f.exists()) return ret;

    QString buf;
    if (f.open(QIODevice::ReadOnly))
    {
       QTextStream in(&f);
       in.setCodec("shift-jis");
       buf = in.readAll();
       f.close();
    }
    buf = buf.replace("\r","\t");
    buf = buf.trimmed();
    if (buf[0]=='\"') buf = buf.mid(1);
    if (buf[buf.size()-1]=='\"') buf = buf.left(buf.size()-1);

    QStringList data = buf.split('\t');
    if (data.size()<52) return ret;
    setFrameRate(24);

    int c = 26;

    int fm = data.size()/c -2;
    setSize(c,fm);
    m_sheetName = FsU::getFileNameWithoutExt(p);

    int idx = 0;
    for (int i=0;i<c;i++)
    {
        m_caption[i] = data[idx].trimmed();
        idx++;
    }
    idx = 26*2;

     cellsAllMinusTo();
    for (int j=0;j<fm;j++){
        for (int i=0;i<c;i++){
            QString num = data[idx].trimmed();
            idx++;
            if ( num.isEmpty()==false){
                if ( num=="X"){
                    m_cells[i][j] = 0;
                }else{
                    bool b = false;
                    int v = num.toInt(&b,10);
                    if (b){
                        if (v<=0) v = 0;
                        m_cells[i][j] = v;
                    }
                }
            }
        }
    }
    cellsAllMinusFrom();
    ret = true;
    return ret;

}
