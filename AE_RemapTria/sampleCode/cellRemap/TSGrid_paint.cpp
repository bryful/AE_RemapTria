#include "TSGrid.h"

//*******************************************************************************
//描画系
//*******************************************************************************
//*******************************************************************************
void TSGrid::paintEvent(QPaintEvent *)
{
    QPainter pnt(this);
    m_area.getStatus();

   pnt.setFont(m_pref.font);
   pnt.fillRect(rect(),m_pref.baseColor);
   drawCell(&pnt);
   drawCaption(&pnt);
   drawFrame(&pnt);
   drawInput(&pnt);
}
//*******************************************************************************
void TSGrid::drawCaption(QPainter *pnt)
{
    QRect rct = m_area.CaptionRect();
    pnt->fillRect(rct,m_pref.captionColor);

    pnt->setPen(m_pref.lineColor);
    int y = m_area.CaptionTop;
    for (int i=0; i<m_data.cellCount();i++){
        if ( m_area.inCell(i)==true)
        {
           int x = i * m_area.CellWidth - m_area.DispX + m_area.CellLeft;
           int x1 = x + m_area.CellWidth -1;

           if (m_sel.inSelCel(i)){
               QRect b(x,m_area.CaptionTop,m_area.CellWidth,m_area.CaptionlHeight);
               pnt->fillRect(b,m_pref.captionColorSel);
           }

           pnt->setPen(m_pref.lineColor);
           pnt->drawLine(x1,rct.top(),x1,rct.bottom());
           pnt->setPen(m_pref.textColor);
           pnt->drawText(x,y,m_area.CellWidth,m_area.CaptionlHeight,Qt::AlignCenter,m_data.getCaption(i));
           if( (i +1)%5 ==0 ){
               pnt->drawLine(x1-1,rct.top(),x1-1,rct.bottom());

           }
        }
    }
    pnt->fillRect(m_area.CaptionRectUnde(),m_pref.baseColor);

    pnt->setPen(m_pref.lineColor);
    pnt->drawRect(rct);

}
//*******************************************************************************
QString TSGrid::frameDispStr(int v)
{
    QString ret = "";
    int sec=0;
    int koma=0;
    int page = 0;
    switch(m_pref.frameDispMode)
    {
    case FrameDisp::Frame:
        ret += QString("%1").arg(v+m_pref.startFrame);
        break;
    case FrameDisp::SecKoma:
        if ( v % m_data.frameRate() ==0 ){
           sec = v / m_data.frameRate();
           ret = QString("%1 + ").arg(sec);
        }
        koma = v % m_data.frameRate();
        ret += QString("%1").arg(koma + m_pref.startFrame);
        break;
    case FrameDisp::PageSecKoma:
        page = v / m_pref.pageCount();
        v %= m_pref.pageCount();
        sec = v / m_data.frameRate();
        koma = v % m_data.frameRate();
        if ( v % m_data.frameRate() ==0 ){
           ret += QString("%1p %2 + ").arg(page+1).arg(sec);
        }
        ret += QString("%1").arg(koma + m_pref.startFrame);
        break;
    case FrameDisp::PageFrame:
    default:
        page = v / m_pref.pageCount();
        v %= m_pref.pageCount();
        if ( v % m_data.frameRate() ==0 ){
           ret += QString("%1p ").arg(page+1);
        }
        ret += QString("%1").arg(v + m_pref.startFrame);
        break;
    }
    return ret;
}

//*******************************************************************************
void TSGrid::drawFrame(QPainter *pnt)
{
    QRect rct = m_area.FrameRect();
    pnt->fillRect(rct,m_pref.frameColor);
    int x = m_area.FrameLeft;
    for (int i=0; i<m_data.frameCount();i++){
        if ( m_area.inFrame(i)==true)
        {
           int y = i * m_area.CellHeight - m_area.DispY + m_area.CellTop;
           int y1 = y + m_area.CellHeight -1;
           if (m_sel.inSelFrame(i)){
               QRect b(m_area.FrameLeft,y,m_area.FrameWidth,m_area.CellHeight);
               pnt->fillRect(b,m_pref.frameColorSel);
           }else if ( (i / m_pref.pageCount()) % 2 == 1){
               QRect b(m_area.FrameLeft,y,m_area.FrameWidth,m_area.CellHeight);
               pnt->fillRect(b,m_pref.frameColor2nd);
           }
           pnt->setPen(m_pref.lineColor);
           if ( (i+1)% m_pref.pageCount() ==0){
               pnt->drawLine(m_area.FrameLeft,y1-3,m_area.FrameRight,y1-3);
           }
           if ( (i+1)% m_pref.horGuide() ==0){
               pnt->drawLine(m_area.FrameLeft,y1-1,m_area.FrameRight,y1-1);
           }
           if ( (i+1)% m_data.frameRate() == 0){
               pnt->drawLine(m_area.FrameLeft,y1-2,m_area.FrameRight,y1-2);
           }
           pnt->drawLine(m_area.FrameLeft,y1,m_area.FrameRight,y1);
           pnt->setPen(m_pref.textColor);
           pnt->drawText(x,y,m_area.FrameWidth-5,m_area.CellHeight,Qt::AlignRight | Qt::AlignVCenter,frameDispStr(i));
        }
    }
    pnt->fillRect(m_area.FrameRectRight(),m_pref.baseColor);
    pnt->setPen(m_pref.lineColor);
    pnt->drawRect(rct);
}
//*******************************************************************************
void TSGrid::drawCell(QPainter *pnt)
{
    QRect rct = m_area.CellRect();
    pnt->fillRect(rct,m_pref.emptyColor);

     for (int c=0; c<m_data.cellCount();c++){
         if ( m_area.inCell(c)==true){
             int x = m_area.CellLeft + c * m_area.CellWidth - m_area.DispX;
             int x1 = x + m_area.CellWidth-1;
             for (int f=0;f<m_data.frameCount();f++){
                if(m_area.inFrame(f)==true){
                    int y = m_area.CellTop + f * m_area.CellHeight - m_area.DispY;
                    int y1 = y + m_area.CellHeight-1;
                    int v = m_data.getNumType(c,f);
                    QRect cr(x,y,m_area.CellWidth,m_area.CellHeight);

                    if (m_sel.inSel(c,f)){
                        pnt->fillRect(cr,m_pref.selectionColor);
                    }else if((v>0)||(v == CellNumSerial)){
                        pnt->fillRect(cr,m_pref.cellColor);
                    }else if ( (f / m_pref.pageCount())%2 == 1){
                        pnt->fillRect(cr,m_pref.emptyColor2nd);

                    }

                    if ( v ==CellNumZeroStart){
                        pnt->setPen(m_pref.lineColor);
                        pnt->drawLine(x,y,x1,y1);
                        pnt->drawLine(x,y1,x1,y);
                    }else if (v==CellNumSerial){
                        int cx = (x +x1)/2;
                        pnt->setPen(m_pref.serialColor);
                        pnt->drawLine(cx,y1,cx,y);
                    }else if (v>0){
                        pnt->setPen(m_pref.textColor);
                        pnt->drawText(cr,Qt::AlignCenter,QString("%1").arg(v));
                    }
                    pnt->setPen(m_pref.lineColor);
                    pnt->drawLine(x1,y,x1,y+m_area.CellHeight-1);
                    if( (c +1)%5 ==0 ){
                        pnt->drawLine(x1-1,y,x1-1,y+m_area.CellHeight-1);

                    }

                }
            }
         }
     }
      pnt->setPen(m_pref.lineColor);
     for (int f=0;f<m_data.frameCount();f++){
         if(m_area.inFrame(f)==true){
             int y = m_area.CellTop + (f+1) * m_area.CellHeight - m_area.DispY -1;
             if ( (f+1)% m_pref.pageCount() ==0){
                 pnt->drawLine(m_area.CellLeft,y-3,m_area.CellRight,y-3);
             }
             if ( (f+1)% m_pref.horGuide() ==0){
                 pnt->drawLine(m_area.CellLeft,y-1,m_area.CellRight,y-1);
             }
             if ( (f+1)% m_data.frameRate() == 0){
                 pnt->drawLine(m_area.CellLeft,y-2,m_area.CellRight,y-2);
             }
             pnt->drawLine(m_area.CellLeft,y,m_area.CellRight,y);
         }
     }

    pnt->fillRect(m_area.CellRectUnder(),m_pref.cellColor);
    pnt->setPen(m_pref.lineColor);
    pnt->drawRect(rct);
}
//*******************************************************************************
void TSGrid::drawInput(QPainter *pnt)
{
    QRect rct = m_area.InputRect();
    pnt->fillRect(rct,m_pref.inputColor);

    if(m_inputValue.isEmpty()==false){
        pnt->setPen(m_pref.textColor);
       pnt->drawText(m_area.InputLeft,m_area.InputTop,m_area.InputWidth-4,m_area.InputHeight,Qt::AlignRight | Qt::AlignVCenter,m_inputValue);
    }
    pnt->fillRect(m_area.InputRectRight(),m_pref.baseColor);
    pnt->fillRect(m_area.InputRectUnder(),m_pref.baseColor);
    pnt->setPen(m_pref.lineColor);
    pnt->drawRect(rct);
}

//*******************************************************************************
void TSGrid::dispFrame()
{
    if(m_pref.frameDispMode != FrameDisp::Frame){
       m_pref.frameDispMode = FrameDisp::Frame;
       this->update();
    }
}

//*******************************************************************************
void TSGrid::dispSecKoma()
{
    if(m_pref.frameDispMode != FrameDisp::SecKoma){
       m_pref.frameDispMode = FrameDisp::SecKoma;
       this->update();
    }
}
//*******************************************************************************
void TSGrid::dispPageFrame()
{
    if(m_pref.frameDispMode != FrameDisp::PageFrame){
       m_pref.frameDispMode = FrameDisp::PageFrame;
       this->update();
    }
}

//*******************************************************************************
void TSGrid::dispPageSecKoma()
{
    if(m_pref.frameDispMode != FrameDisp::PageSecKoma){
       m_pref.frameDispMode = FrameDisp::PageSecKoma;
       this->update();
    }
}


