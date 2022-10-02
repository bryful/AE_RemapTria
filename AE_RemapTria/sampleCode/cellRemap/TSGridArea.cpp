#include "TSGridArea.h"

TSGridArea::TSGridArea(TSData *d, TSPref *pf, QWidget *p)
{
    setParams(d,pf,p);
    DispX = 0;
    DispY = 0;
}
void TSGridArea::setParams(TSData *d, TSPref *pf, QWidget *p)
{
    BarWigth = 16;
    data = d;
    pref = pf;
    wdt = p;
    getStatus();
}

void TSGridArea::getDispStatus(){
    m_DispCellStart = DispX / CellWidth;
    m_DispCellLast = (CellAreaWidth + DispX) /CellWidth;
    m_DispFrameStart = DispY / CellHeight;
    m_DispFrameLast = (CellAreaHeight + DispY) /CellHeight;
}

void TSGridArea::getStatus()
{
    if ( (data==NULL)||(pref == NULL)||(wdt == NULL)) return;
    m_cell = -1;
    m_frame = -1;
    m_area = -1;

    CellCount           = data->cellCount();
    FrameCount          = data->frameCount();
    CellWidth           = pref->cellWidth;
    CellHeight          = pref->cellHeight;
    CaptionlHeight      = pref->captionlHeight;
    FrameWidth          = pref->frameWidth;
    FrameInter          = pref->frameInter;
    CaptionInter        = pref->captionInter;
    int w = 300;
    int h = 300;
    w = wdt->width();
    h = wdt->height();

    MinWidth    = FrameWidth + FrameInter + CellWidth + BarWigth;
    MinHeight   = CaptionlHeight + CaptionInter + CellHeight + BarWigth;
    MaxWidth    = FrameWidth + FrameInter + CellWidth*CellWidth + BarWigth;
    MaxHeight   = CaptionlHeight + CaptionInter + CellHeight*FrameCount + BarWigth;


    DispXMax = CellCount * CellWidth - (w -(FrameWidth+ FrameInter+BarWigth));
    if (DispXMax<0) DispXMax = 0;

    DispYMax = FrameCount * CellHeight - (h -(CaptionlHeight+ CaptionInter+BarWigth));
    if (DispYMax<0) DispYMax = 0;

    if ( DispX<0) DispX = 0;
    else if (DispX>DispXMax) DispX = DispXMax;

    if ( DispY<0) DispY = 0;
    else if (DispY>DispYMax) DispY = DispYMax;


    int ww = FrameWidth + FrameInter + CellCount * CellWidth - DispX;
    int hh = CaptionlHeight + CaptionInter + FrameCount * CellHeight - DispY;


    InputTop            = 0;
    InputBottom         = CaptionlHeight;
    InputLeft           = 0;
    InputRight          = FrameWidth;
    InputWidth          = FrameWidth;
    InputHeight         = CaptionlHeight;

    CaptionTop          = InputTop;
    CaptionBottom       = CaptionlHeight;
    CaptionLeft         = FrameWidth + FrameInter;
    CaptionRight        = w - BarWigth;
    if (CaptionRight>ww) CaptionRight = ww;
    CaptionAreaWidth    = CaptionRight - CaptionLeft;
    CaptionAreaHeight   = CaptionlHeight;


    CellTop             = CaptionlHeight + CaptionInter;
    CellBottom          = h - BarWigth;
    if (CellBottom>hh) CellBottom = hh;

    CellLeft            = CaptionLeft;
    CellRight           = CaptionRight;
    CellAreaWidth       = CaptionAreaWidth;
    CellAreaHeight      = CellBottom - CellTop;


    FrameTop            = CellTop;
    FrameBottom         = CellBottom;
    FrameLeft           = InputLeft;
    FrameRight          = InputRight;
    FrameAreaWidth      = FrameWidth;
    FrameAreaHeight     = CellAreaHeight;

    getDispStatus();

}
//*****************************************************************************
int TSGridArea::getArea(int x,int y)
{
    m_cell = -1;
    m_frame = -1;
    m_area = AreaNone;
    m_IsShift = ((QApplication::keyboardModifiers() & Qt::ShiftModifier)==Qt::ShiftModifier);
    if (x<CellLeft){
        if ( y<InputBottom){
            m_area = AreaInput;
        }else if (y<CellBottom){
            m_frame = (y- FrameTop + DispY) / CellHeight;
            m_area = AreaFrame;
        }
    }else if (x<CellRight){
        if ( y<InputBottom){
            m_area = AreaCaption;
            m_cell = (x- CellLeft + DispX) / CellWidth;
        }else if (y<CellBottom){
            m_area = AreaCell;
            m_cell = (x- CellLeft + DispX) / CellWidth;
            m_frame = (y- FrameTop + DispY) / CellHeight;
        }
    }
    return m_area;
}
//*****************************************************************************
QRect TSGridArea::hBarRect()
{
    return QRect(FrameLeft,wdt->height() - BarWigth ,wdt->width() - BarWigth,BarWigth);
}
QRect TSGridArea::vBarRect()
{
    return QRect(wdt->width() - BarWigth,CaptionTop,BarWigth,wdt->height()-BarWigth);
}
//*****************************************************************************
int TSGridArea::getDispYTop(int v){
    int ret = v * CellHeight;
    if (ret<0)ret =0;
    return ret;
}

int TSGridArea::getDispYBottom(int v){
     int ret =(v - (m_DispFrameLast - m_DispFrameStart))* CellHeight;
     if (ret<0)ret =0;
     return ret;
}
int TSGridArea::getDispXLeft(int v){
     int ret =v * CellWidth;
     if (ret<0)ret =0;
     return ret;
}
int TSGridArea::getDispXRight(int v){
    int ret =(v - (m_DispCellLast -m_DispCellStart)) * CellWidth;
     if (ret<0)ret =0;
     return ret;
}

