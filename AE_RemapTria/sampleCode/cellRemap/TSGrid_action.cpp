#include "TSGrid.h"
//*******************************************************************************
void TSGrid::createActions()
{
    actionCellRename = new QAction(tr("CellRename"), this);
    actionCellRename->setStatusTip(tr("CellRename"));
    actionCellRename->setFont(this->font());
    connect(actionCellRename, SIGNAL(triggered()), this, SLOT(showCellRenameDialog()));

    actionCellInsert = new QAction(tr("CellInsert"), this);
    actionCellInsert->setStatusTip(tr("actionCellInsert"));
    actionCellInsert->setFont(this->font());
    connect(actionCellInsert, SIGNAL(triggered()), this, SLOT(showCellInsertDialog()));

    actionCellRemove = new QAction(tr("CellRemove"), this);
    actionCellRemove->setStatusTip(tr("CellRemove"));
    actionCellRemove->setFont(this->font());
    connect(actionCellRemove, SIGNAL(triggered()), this, SLOT(showCellRemoveDialog()));

    actionFrameDispFrame = new QAction(tr("Frame"), this);
    actionFrameDispFrame->setStatusTip(tr("FrameDisp-Frame"));
    actionFrameDispFrame->setCheckable(true);
    actionFrameDispFrame->setFont(this->font());
    connect(actionFrameDispFrame, SIGNAL(triggered()), this, SLOT(dispFrame()));

    actionFrameDispSecKoma = new QAction(tr("Sec_Koma"), this);
    actionFrameDispSecKoma->setStatusTip(tr("FrameDisp-Sec+Koma"));
    actionFrameDispSecKoma->setCheckable(true);
    actionFrameDispSecKoma->setFont(this->font());
    connect(actionFrameDispSecKoma, SIGNAL(triggered()), this, SLOT(dispSecKoma()));

    actionFrameDispPageFrame = new QAction(tr("Page_Frame"), this);
    actionFrameDispPageFrame->setStatusTip(tr("FrameDisp-Page+Frame"));
    actionFrameDispPageFrame->setCheckable(true);
    actionFrameDispPageFrame->setFont(this->font());
    connect(actionFrameDispPageFrame, SIGNAL(triggered()), this, SLOT(dispPageFrame()));

    actionFrameDispPageSecKoma= new QAction(tr("Page_Sec_Koma"), this);
    actionFrameDispPageSecKoma->setStatusTip(tr("FrameDisp-Page+Sec+Koma"));
    actionFrameDispPageSecKoma->setCheckable(true);
    actionFrameDispPageSecKoma->setFont(this->font());
    connect(actionFrameDispPageSecKoma, SIGNAL(triggered()), this, SLOT(dispPageSecKoma()));

    actionStarrFrameZero= new QAction(tr("StartFraameZero"), this);
    actionStarrFrameZero->setStatusTip(tr("StartFraame Zero"));
    actionStarrFrameZero->setCheckable(true);
    actionStarrFrameZero->setFont(this->font());
    connect(actionStarrFrameZero, SIGNAL(triggered(bool)), this, SLOT(setStartZero(bool)));

    actionOpenAfterFX= new QAction(tr("OpenAfterFX"), this);
    actionOpenAfterFX->setStatusTip(tr("OpenAfterFX"));
    actionOpenAfterFX->setFont(this->font());
    connect(actionOpenAfterFX, SIGNAL(triggered(bool)), this, SLOT(setStartZero(bool)));

    actionAutoInput= new QAction(tr("AutoInput"), this);
    actionAutoInput->setStatusTip(tr("AutoInput"));
    actionAutoInput->setFont(this->font());
    connect(actionAutoInput, SIGNAL(triggered()), this, SLOT(autoInputDialog()));

    actionCalc= new QAction(tr("Calc"), this);
    actionCalc->setStatusTip(tr("Calc"));
    actionCalc->setFont(this->font());
    connect(actionCalc, SIGNAL(triggered()), this, SLOT(showCalcDialog()));


    actionFrameAdd= new QAction(tr("FrameAdd"), this);
    actionFrameAdd->setStatusTip(tr("FrameAdd"));
    actionFrameAdd->setFont(this->font());
    connect(actionFrameAdd, SIGNAL(triggered()), this, SLOT(frameInset()));

    actionFrameDel= new QAction(tr("FrameDel"), this);
    actionFrameDel->setStatusTip(tr("FrameDel"));
    actionFrameDel->setFont(this->font());
    connect(actionFrameDel, SIGNAL(triggered()), this, SLOT(frameDelete()));

    actionCopy= new QAction(tr("Copy"), this);
    actionCopy->setStatusTip(tr("Copy"));
    actionCopy->setFont(this->font());
    connect(actionCopy, SIGNAL(triggered()), this, SLOT(copy()));

    actionCut= new QAction(tr("Cut"), this);
    actionCut->setStatusTip(tr("Cut"));
    actionCut->setFont(this->font());
    connect(actionCut, SIGNAL(triggered()), this, SLOT(cut()));

    actionPaste= new QAction(tr("Paste"), this);
    actionPaste->setStatusTip(tr("Paste"));
    actionPaste->setFont(this->font());
    connect(actionPaste, SIGNAL(triggered()), this, SLOT(paste()));

    actionAutoInput= new QAction(tr("AutoInput"), this);
    actionAutoInput->setStatusTip(tr("AutoInput"));
    actionAutoInput->setFont(this->font());
    connect(actionAutoInput, SIGNAL(triggered()), this, SLOT(autoInputDialog()));
}
//*******************************************************************************
void TSGrid::contextMenuEvent(QContextMenuEvent *e)
{
    int area = m_area.getArea(e->x(),e->y());
    if (area ==AreaCaption){
        QMenu menu(this);
        menu.addAction(actionCellRename);
        menu.addAction(actionCellInsert);
        menu.addAction(actionCellRemove);
        menu.exec(e->globalPos());

    }else if (area ==AreaFrame){
        QMenu menuf(this);
        actionFrameDispFrame->setChecked(false);
        actionFrameDispSecKoma->setChecked(false);
        actionFrameDispPageFrame->setChecked(false);
        actionFrameDispPageSecKoma->setChecked(false);
        switch(m_pref.frameDispMode)
        {
        case FrameDisp::Frame:
            actionFrameDispFrame->setChecked(true);
            break;
        case FrameDisp::SecKoma:
            actionFrameDispSecKoma->setChecked(true);
            break;
        case FrameDisp::PageFrame:
            actionFrameDispPageFrame->setChecked(true);
            break;
        case FrameDisp::PageSecKoma:
            actionFrameDispPageSecKoma->setChecked(true);
            break;
        }
        menuf.addAction(actionFrameDispFrame);
        menuf.addAction(actionFrameDispSecKoma);
        menuf.addAction(actionFrameDispPageFrame);
        menuf.addAction(actionFrameDispPageSecKoma);
        menuf.addSeparator();
        menuf.addAction(actionStarrFrameZero);
        menuf.exec(e->globalPos());

    }else if (area ==AreaCell){
        QMenu menuC(this);
        menuC.addAction(actionCopy);
        menuC.addAction(actionCut);
        menuC.addAction(actionPaste);
        menuC.addSeparator();
        menuC.addAction(actionAutoInput);
        menuC.addAction(actionCalc);
        menuC.addSeparator();
        menuC.addAction(actionFrameAdd);
        menuC.addAction(actionFrameDel);
        menuC.exec(e->globalPos());
    }

}
