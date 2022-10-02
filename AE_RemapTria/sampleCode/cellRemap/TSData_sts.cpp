#include "TSData.h"
//****************************************************
bool TSData::loadSTS(QString p)
{
    bool ret = false;
    QFile f(p);
    if (f.exists()==false) return ret;
    if (f.open(QIODevice::ReadOnly))
    {
        QFileInfo info(f);
        int s =info.size();
        char *buf = new char[s];
        QDataStream in(&f);
        in.readRawData(buf,s);
        f.close();

        QByteArray ba(buf,s);
        if(ba.indexOf("ShiraheiTimeSheet")!=1){
            return ret;
        }
        QFileInfo fi(f);
        m_sheetName = fi.baseName();
        int c= (unsigned char)ba[0x12];
        int f=(unsigned char)ba[0x13] + (unsigned char)ba[0x14]*0x100;
        if ((c<=0)||(f<=0)) return ret;
        setFrameRate(24);
        setSize(c,f);
        m_sheetName = FsU::getFileNameWithoutExt(p);
        int idx =0x17;
        for (int j=0;j<c;j++){
            for ( int i=0;i<f;i++){
                int v = (int)ba[idx];
                idx++;
                v += ba[idx]*0x100;
                idx++;
                if (v<0) v=0;
                m_cells[j][i] = v;
            }
        }
        int cc =0;
        while(idx<s)
        {
            int cnt =ba[idx];
            idx++;
            QByteArray cap;
            for ( int i=0;i<cnt;i++)
            {
                cap.append(ba[idx]);
                idx++;
            }
            QTextCodec *codec =QTextCodec::codecForName("shift-jis");
            QString caps = codec->toUnicode(cap);
            m_caption[cc] =caps;
            cc++;
        }
        delete buf;
    }
    ret = true;
    return ret;
}
//****************************************************
bool TSData::saveSTS(QString p)
{
    bool ret = false;
    QByteArray ba;
    ba.append((unsigned char)0x11);
    ba.append("ShiraheiTimeSheet");
    ba.append((unsigned char)m_CellCount);
    ba.append((unsigned char)( (m_FrameCount % 0x100)&0xFF));
    ba.append((unsigned char)( (m_FrameCount / 0x100) &0xFF));
    ba.append((char)0x00);
    ba.append((char)0x00);

    for ( int j=0; j<m_CellCount;j++){
        for ( int i=0; i<m_FrameCount; i++){
            int v = m_cells[j][i];
            unsigned char v1 = (v % 0x100) & 0xFF;
            unsigned char v2 = (v / 0x100) & 0xFF;
            ba.append(v1);
            ba.append(v2);

        }
    }
    QTextCodec* sjisCodec = QTextCodec::codecForName( "SJIS" );
    for (int i=0; i<m_CellCount; i++){
        QByteArray c = sjisCodec->fromUnicode( m_caption[i] );
        ba.append((unsigned char)c.size());
        ba.append(c);

    }


    char *data = new char[ba.size()];
    for (int i=0;i<ba.size();i++) data[i] = ba[i];
    QFile f(p);
    if (f.open(QIODevice::WriteOnly)){
        f.write(ba);
        f.close();
        ret = true;
    }
    return ret;
}
//****************************************************

