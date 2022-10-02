#ifndef TSGRIDAREA_H
#define TSGRIDAREA_H

#include <QtWidgets>


#include "TSPref.h"
#include "TSData.h"
class TSData;

enum{
    AreaNone = -1,
    AreaInput =0,
    AreaCaption,
    AreaFrame,
    AreaCell

};

class TSGridArea
{
private:
    TSData *data;
    TSPref *pref;
    QWidget *wdt;

    int m_area;
    int m_cell;
    int m_frame;
    bool m_IsShift;


    int m_DispCellStart;
    int m_DispCellLast;
    int m_DispFrameStart;
    int m_DispFrameLast;

public:
    TSGridArea(TSData *d = 0, TSPref *pf = 0, QWidget *p = 0);

    bool isEnabled(){ return ( (data!=NULL)&&(pref != NULL)&&(wdt != NULL)) ;}

    int BarWigth;

    int CellCount;
    int FrameCount;

    int DispX;
    int DispY;

    int DispXMax;
    int DispYMax;

    int CellWidth;
    int CellHeight;
    int CaptionlHeight;
    int FrameWidth;

    int FrameInter;
    int CaptionInter;


    int InputTop;
    int InputBottom;
    int InputLeft;
    int InputRight;
    int InputWidth;
    int InputHeight;

    int CellTop;
    int CellBottom;
    int CellLeft;
    int CellRight;
    int CellAreaWidth;
    int CellAreaHeight;

    int CaptionTop;
    int CaptionBottom;
    int CaptionLeft;
    int CaptionRight;
    int CaptionAreaWidth;
    int CaptionAreaHeight;


    int FrameTop;
    int FrameBottom;
    int FrameLeft;
    int FrameRight;
    int FrameAreaWidth;
    int FrameAreaHeight;


    int MinWidth;
    int MinHeight;
    int MaxWidth;
    int MaxHeight;

    void setParams(TSData *d = NULL, TSPref *pf = NULL, QWidget *p = NULL);
    void getDispStatus();
    void getStatus();

    int getDispYTop(int v);
    int getDispYBottom(int v);
    int getDispXLeft(int v);
    int getDispXRight(int v);

    int getArea(int x,int y);

    int area(){return m_area;}
    int cell(){return m_cell;}
    int frame(){return m_frame;}
    bool isShift(){return m_IsShift;}


    QRect CaptionRect(){return QRect(CaptionLeft,CaptionTop,CaptionAreaWidth,CaptionAreaHeight);}
    QRect CaptionRectUnde()
    {
        return QRect(CaptionLeft,CaptionBottom,CaptionAreaWidth,CaptionInter);
    }
    QRect CellRect(){
        return QRect(CellLeft,CellTop,CellAreaWidth,CellAreaHeight);
    }
    QRect CellRectUnder(){
        return QRect(wdt->width()-BarWigth ,wdt->height() -BarWigth,BarWigth,BarWigth);
    }
    QRect FrameRect(){
        return QRect(FrameLeft,FrameTop,FrameAreaWidth,FrameAreaHeight);
    }
    QRect FrameRectRight(){
        return QRect(FrameRight,FrameTop,FrameInter,FrameAreaHeight);
    }
    QRect InputRect(){
        return QRect(InputLeft,InputTop,InputWidth,InputHeight);
    }
    QRect InputRectUnder(){
        return QRect(InputLeft,InputBottom,InputWidth + FrameInter ,CaptionInter);
    }
    QRect InputRectRight(){
        return QRect(InputRight,InputTop,FrameInter,CaptionlHeight);
    }
    bool inCell(int c){return ( (c>=m_DispCellStart)&&(c<=m_DispCellLast));}
    bool inFrame(int f){return ( (f>=m_DispFrameStart)&&(f<=m_DispFrameLast));}
    QRect hBarRect();
    QRect vBarRect();
    int horGuide();

    int dispCellStart(){return m_DispCellStart;}
    int dispCellLast(){return m_DispCellLast;}
    int dispFrameStart(){return m_DispFrameStart;}
    int dispFrameLast(){return m_DispFrameLast;}
    int dispFrameLength(){return m_DispFrameLast -m_DispFrameStart +1;}

};

#endif // TSGRIDAREA_H
