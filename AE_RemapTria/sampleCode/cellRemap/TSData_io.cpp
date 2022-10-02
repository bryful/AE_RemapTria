#include "TSData.h"
//**********************************************************
QList< QList<double> > TSData::getKeys(int c)
{
    QList< QList<double> > ret;
    if ((c<0)||(c>=m_CellCount)) return ret;
    QList<double> key;
    key.append(0);
    if (m_cells[c][0]<=0){
        key.append(-1);
    }else{
        key.append((double)(m_cells[c][0]-1)/m_FrameRate);
    }
    ret.append(key);

    for (int f=1;f<m_cells[c].size();f++)
    {
        if (m_cells[c][f-1] != m_cells[c][f]){
            key.clear();
            key.append((double)f / m_FrameRate);
            if (m_cells[c][f]<=0){
                key.append(-1);
            }else{
                key.append((double)(m_cells[c][f]-1)/m_FrameRate);
            }
            ret.append(key);
        }
    }
    return ret;
}

//**********************************************************
QList < QList<int> > TSData::cellKeys(int index)
{
    QList < QList<int> > ret;
    if ( (index<0)||(index>=m_CellCount)) return ret;
    QList<int> key;
    key.append(0);
    key.append(m_cells[index][0]);
    ret.append(key);
    for (int i=1;i<m_cells[index].size();i++){
        if (m_cells[index][i-1] != m_cells[index][i]){
            QList<int> key;
            key.append(i);
            key.append(m_cells[index][i]);
            ret.append(key);
        }
    }
    return ret;
}

//**********************************************************
QJsonArray TSData::cellsToJ()
{
    QJsonArray jcells;
    int cSize = m_cells.size();

    for (int c = 0; c<cSize;c++){
         QList < QList<int> > keys;
         keys = cellKeys(c);

         QJsonArray jc;
         for (int k=0;k<keys.size();k++){
             QJsonArray jk;
             jk.append(keys[k][0]);
             jk.append(keys[k][1]);
             jc.append(jk);

         }
         jcells.append(jc);

    }
    return jcells;
}
//**********************************************************
QByteArray TSData::aeToJson(QByteArray s)
{
    if (s.size()<=0) return s;
    s = s.trimmed();
    //BOM
    if (s.size()>3){
        if ((s[0]==(char)0xEF)&&(s[1]==(char)0xBB)&&(s[2]==(char)0xBF)){
            s = s.right(s.size()-3);
        }
    }
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

//**********************************************************
QString TSData::jsonToAE(QString s)
{
    QString ret ="";
    if (s.isEmpty()) return s;
    QStringList sa = s.split('\n');
    if (sa.size()>0){
        for(int i=0; i<sa.size();i++){
            ret += sa[i].trimmed();
        }
    }else{
        ret = s;
    }

    return ret;
}

//**********************************************************
QString  TSData::toJson()
{
    QJsonObject jo;


    jo["sheetName"]     = m_sheetName;
    jo["cellCount"]     = m_CellCount;
    jo["frameCount"]    = m_FrameCount;
    jo["frameRate"]     = m_FrameRate;

    jo["caption"]       = captionToJ();
    jo["cells"]         = cellsToJ();

//    if (! m_AfterFXPath.isEmpty())
//    {
//        jo["AfterFXPath"] = m_AfterFXPath;
//    }

    QJsonDocument jd(jo);
    return jsonToAE(jd.toJson());

}
//**********************************************************
QString  TSData::toJsonAE()
{
    QString org =  toJson();
    QString ret = "";
    QString node = "";

    int idx = 0;
    while(idx<org.size()){
        QChar c = org[idx];
        if ((c=='{')||(c=='}')||(c==',')||(c=='[')||(c==']')){
            ret += node;
            ret += c;
            node = "";
        }else if ( c==':'){
            node = node.trimmed();
            if (node.size()>0) if(node[0]=='\"') node = node.mid(1);
            if (node.size()>0) if(node[node.size()-1]=='\"') node = node.mid(0,node.size()-1);
            ret += node;
            ret += c;
            node = "";

        }else{
            node += c;
        }
        idx++;
    }
    if (!node.isEmpty()) ret += node;
    if(ret.size()>0) if (ret[0]!='(') ret = "("+ret;
    if(ret.size()>0) if (ret[ret.size()-1]!=')') ret = ret + ")";

    return ret;

}

//**********************************************************
bool TSData::saveJson(QString p)
{
    QString js = toJson();

    QFile f(p);

    if (f.open(QFile::WriteOnly)){
        QTextStream out(&f);
        out.setCodec("UTF-8");
        out << js;
        f.close();
       return true;
    }else{
        return false;
    }
}

//**********************************************************
bool  TSData::fromJson(QByteArray js)
{
    js = aeToJson(js);
    if (js.isEmpty() ) return false;
    QJsonDocument jd = QJsonDocument::fromJson(js);
    if ( (js.isEmpty())||(js.isNull())) return false;


    QJsonObject jo = jd.object();

    TSData d;
    if (! jo["sheetName"].isUndefined()){
        d.m_sheetName = jo["sheetName"].toString();
    }
    int c =6;
    int frm = 72;
    int fps = 24;
    if (! jo["frameRate"].isUndefined()){
        fps = (int)jo["frameRate"].toDouble();
    }
    if (! jo["cellCount"].isUndefined()){
        c = (int)jo["cellCount"].toDouble();
    }
    if (! jo["frameCount"].isUndefined()){
        frm = (int)jo["frameCount"].toDouble();
    }
    if ((fps<=0)||(c<=0)||(frm<=0)) return false;
    d.setSize(c,frm);
    d.setFrameRate(fps);


    if (! jo["caption"].isUndefined()){
        QJsonArray caps = jo["caption"].toArray();
        if (caps.size()>0){
            for (int i=0;i<caps.size();i++) d.setCaption(i,caps[i].toString());
        }
    }

    QJsonArray jcells;
    if (! jo["cells"].isUndefined()){
        jcells = jo["cells"].toArray();

        d.cellsAllMinusTo();
        for (int i=0;i<c;i++){
            QJsonArray cell = jcells[i].toArray();
            int fcnt = cell.size();
            if (fcnt>0){
                for (int k=0;k<fcnt;k++){
                    QJsonArray key = cell[k].toArray();
                    if (key.size()>=2){
                        int fr  = (int)key[0].toDouble();
                        int v    = (int)key[1].toDouble();
                        d.setNumRow(i,fr,v);

                    }
                }
            }
        }
        d.cellsAllMinusFrom();
    }

    if (! jo["AfterFXPath"].isUndefined()){
        m_AfterFXPath = jo["AfterFXPath"].toString();
    }
    this->assign(d);
    return true;

}

//**********************************************************
bool TSData::loadJson(QString p)
{
    bool ret = false;
    QFile f(p);
    if ( f.exists()==false) return ret;
    QFileInfo fi(f);
    QByteArray js = "";
    if (! f.open(QIODevice::ReadOnly | QIODevice::Text)) return ret;
    js = f.readAll();
    f.close();

    return fromJson(js);
}

//**********************************************************
bool TSData::save(QString p)
{
    bool ret = false;
    QFile f(p);
    QFileInfo fi(f);
    if ( fi.baseName() != m_sheetName){
        m_sheetName = fi.baseName();
    }
    QString e = fi.suffix().toLower();
    if (e.isEmpty()){
        e = "ardj";
        p += ".ardj";
    }
    if (e=="ardj"){
        ret = saveJson(p);
    }else if (e=="sts"){
        ret = saveSTS(p);
    }
    if(ret){
        splitName(p);
    }

    return ret;
}
//**********************************************************
bool TSData::load(QString p)
{
    p = p.replace("\\","/");
    bool ret = false;
    m_AfterFXPath = "";
    QFile f(p);

    //QTextStream cout(stdout);

    if ( f.exists()==false) return ret;
    QFileInfo fi(f);
    QString e = fi.suffix().toLower();
    //cout << e;
    if (e=="ardj"){
        ret = loadJson(p);
    }else if (e=="ard"){
            ret = loadARD(p);
    }else if (e=="tsh"){
           ret = loadTSH(p);
    }else if (e=="sts"){
            ret = loadSTS(p);
    }else if (e=="xps"){
            ret = loadXPS(p);
    }else if (e=="csv"){
            ret = loadStylos(p);
    }
    if(ret){
        splitName(p);
    }
    return ret;
}
