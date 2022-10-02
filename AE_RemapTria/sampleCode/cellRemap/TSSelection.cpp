#include "TSSelection.h"

//-------------------------------------------------
TSSelection::TSSelection(TSData *d)
{
    data = d;
    m_start = 0;
    m_last = 0;
    m_length = m_last - m_start +1;
    m_targetCell = 0;

}
//-------------------------------------------------
void TSSelection::setTSData(TSData *d)
{
    data = d;
    chk();
}

//-------------------------------------------------
void TSSelection::chk()
{
    if ( m_start>m_last){
        int temp = m_start;
        m_start = m_last;
        m_last = temp;
    }
    m_length = m_last - m_start +1;
}

//-------------------------------------------------
bool TSSelection::setStart(int v)
{
    bool ret = false;
    if (m_start !=v ){
        m_start = v;
        ret = true;
        chk();
    }
    return ret;
}
//-------------------------------------------------
bool TSSelection::setLast(int v)
{
    bool ret = false;
    if (m_last !=v ){
        m_last = v;
        ret = true;
        chk();
    }
    return ret;
}
//-------------------------------------------------
bool TSSelection::down(int v)
{
    bool ret = false;
    if (m_start+v <= data->frameCount()-1){
        m_start += v;
        m_last += v;
        ret = true;
    }
    return ret;
}
//-------------------------------------------------
bool TSSelection::up(int v)
{
    bool ret = false;
    if (m_last-v >= 0){
        m_start -= v;
        m_last -= v;
        ret = true;
    }
return ret;
}

//-------------------------------------------------
bool TSSelection::setLength(int v)
{
    bool ret = false;
    chk();
    if (m_length !=v ){
        m_last = m_start + v -1;
        ret = true;
    }
    return ret;
}
//-------------------------------------------------
bool TSSelection::setPoint(int v)
{
    bool ret = false;
    if ( v< m_start){
        ret = setStart(v);
    }else if( (v>=m_start)&&(v<=m_last)){
        m_last = v;
        chk();
        ret = true;

    }else if ( v>m_last){
        ret = setLast(v);
    }
    return ret;
}
//-------------------------------------------------
bool TSSelection::setStartLast(int s,int l){
    m_start =s;
    m_last = l;
    chk();
    return true;
}

//-------------------------------------------------
bool TSSelection::setTargetCell(int v)
{
    bool ret = false;
    if ((v>=0)&&(v<data->cellCount())){
        if ( m_targetCell != v){
            m_targetCell = v;
            ret = true;
        }
    }
    return ret;
}
//-------------------------------------------------
bool TSSelection::moveUp()
{
    bool ret = false;
    if (m_last - m_length >=0){
        m_start -= m_length;
        m_last -= m_length;
        ret = true;
    }
    return ret;
}
//-------------------------------------------------
bool TSSelection::moveDown()
{
    bool ret = false;
    if (m_start + m_length < data->frameCount()){
        m_start += m_length;
        m_last += m_length;
        ret = true;
    }
    return ret;
}
//-------------------------------------------------
bool TSSelection::moveLeft()
{
    bool ret = false;
    if (m_targetCell>0){
        m_targetCell -= 1;
        ret = true;
    }
    return ret;
}
//-------------------------------------------------
bool TSSelection::moveRight()
{
    bool ret = false;
    if (m_targetCell+1 < data->cellCount()){
        m_targetCell += 1;
        ret = true;
    }
    return ret;
}
//-------------------------------------------------
bool TSSelection::setNum(int v)
{
    bool ret = false;
    if (v<0) v=0;
    for ( int i= m_start; i<=m_last;i++){
        if (data->setNum(m_targetCell,i,v)) ret = true;
    }
    return ret;
}
//-------------------------------------------------
int TSSelection::getNumPrev(){
   if (m_start<=0) return 0;
    return data->getNum(m_targetCell,m_start-1);

}
//-------------------------------------------------
bool TSSelection::tailPluss(){
    bool ret = false;
    if ( m_last+1<data->frameCount()){
        m_last += 1;
        m_length += 1;
        ret = true;
    }
    return ret;
}
//-------------------------------------------------
bool TSSelection::tailMinus(){
    bool ret = false;
    if ( m_last>m_start){
        m_last -= 1;
        m_length -= 1;
        ret = true;
    }
    return ret;
}
//-------------------------------------------------
bool TSSelection::toEnd()
{
    bool ret = false;
    if ( m_last+1 < data->frameCount()){
        m_last = data->frameCount() -1;
        m_length = m_last - m_start +1;
        ret = true;
    }
    return ret;
}
//-------------------------------------------------
bool TSSelection::aLL()
{
    m_start =0;
    m_last = data->frameCount() -1;
    m_length = m_last - m_start +1;
    return true;
}
//-------------------------------------------------
bool TSSelection::copy()
{
    bool b = false;
    QString ret = "";

    if ( (m_targetCell>=0)&&(m_targetCell<data->cellCount())){
        for (int i=m_start;i<=m_last;i++){
            if ((i>=0)&&(i<data->frameCount())){
                if (! ret.isEmpty()) ret +="\n";
                ret += QString("%1").arg(data->getNum(m_targetCell,i));
                b = true;
            }
        }
    }
    QClipboard *q =  QApplication::clipboard();
    q->setText(data->clipHeader()+"\n"+ ret);
    return b;
}

//-------------------------------------------------
bool TSSelection::cut()
{
    bool b = copy();
    if (b){
        setNum(0);
    }
    return b;
}

//-------------------------------------------------
bool TSSelection::paste()
{
    QClipboard *q =  QApplication::clipboard();
    const QMimeData *mimeData = q->mimeData();
    QString s = "";
    if ( mimeData->hasText()) {
         s = q->text();
    }
    QStringList sa;
    sa = s.split('\n');
    if (sa.size()<=1) return false;
    if (sa[0].trimmed() != data->clipHeader()) return false;
    QList <int> lst;
    for (int i=1; i<sa.size();i++) {
        bool ok;
        int v = sa[i].toInt(&ok);
        if ( ok) lst.append(v);
    }
    int cnt = lst.size();
    if (cnt<=0) return false;
    if(cnt>m_length) cnt = m_length;
    for(int i=0; i<cnt;i++){
        data->setNum(m_targetCell,i+ m_start,lst[i]);
    }
    return true;
}
//-------------------------------------------------
QString TSSelection::getCaption()
{
    return data->getCaption(m_targetCell);
}

//-------------------------------------------------
bool TSSelection::setCaption(QString s)
{
    data->setCaption(m_targetCell,s);
    return true;
}
//-------------------------------------------------
bool TSSelection::cellRemove()
{
    data->cellRemove(m_targetCell);
    return true;

}
//-------------------------------------------------
void TSSelection::autoInput(int s,int last, int len)
{
    int idx = m_targetCell;
    int y0 = m_start;
    if (y0 < 0) { y0 = 0; }
    int y1 = m_last;
    if (y1 >= data->frameCount()) { y1 = data->frameCount() -1; }
    int s0 = s;
    int s1 = last;
    int k = len;
    if (k <= 0) k = 1;
    int ll = (s1 - s0 + 1) * k;
    QList<int> loop;
    for (int j = 0; j < ll; j++) loop.append(0);


    int v =0;
    if (s0 == s1)
    {
        for (int j = 0; j < k; j++)
        {
            loop[j] = s0;
        }
    }
    else if (s0 < s1)
    {
        for (int i = s0; i <= s1; i++)
        {
            for (int j = 0; j < k; j++)
            {
                loop[v] = i;
                v++;
            }
        }
    }
    else if (s0 > s1)
    {
        for (int i = s0; i <= s1; i--)
        {
            for (int j = 0; j < k; j++)
            {
                loop[v] = i;
                v++;
            }
        }
    }
    v =0;
    for (int i = y0; i <= y1; i++)
    {
        data->setNum(idx,i, loop[v]);
        v = (v + 1) % ll;
    }

}
//-------------------------------------------------
bool TSSelection::moveCellLeft()
{
    bool b = data->swapCell(m_targetCell,m_targetCell-1);
    if (b) m_targetCell-=1;
    return b;
}
//-------------------------------------------------
bool TSSelection::moveCellRight()
{
    bool b = data->swapCell(m_targetCell,m_targetCell+1);
    if (b) m_targetCell+=1;
    return b;
}

//**********************************************************
void TSSelection::frameInsert(bool IsAll,bool isLength)
{
    data->frameInsert(m_targetCell,m_start,m_last,IsAll,isLength);
}
//**********************************************************
void TSSelection::frameDelete(bool IsAll,bool isLength)
{
    data->frameDelete(m_targetCell,m_start,m_last,IsAll,isLength);

}
//**********************************************************
void TSSelection::calc(int value,int mode)
{
    if ( (m_targetCell<0)||(m_targetCell>=data->cellCount())) return;
    int st = m_start;
    if (st<0) st =0;
    int lt = m_last;
    if (lt>=data->frameCount()) lt = data->frameCount() - 1;


    int v = 0;
    for (int i=st;i<=lt;i++){
        switch (mode) {
        case 0:
            if (value<0) value = 0;
            data->setNum(m_targetCell,i,value);
            break;
        case 1:
            v =  data->getNum(m_targetCell,i);
            v += value;
            if (v<0) v = 0;
            data->setNum(m_targetCell,i,v);
            break;
        case 2:
            v =  data->getNum(m_targetCell,i);
            v -= value;
            if (v<0) v = 0;
            data->setNum(m_targetCell,i,v);
            break;
        }
    }

}

//**********************************************************
