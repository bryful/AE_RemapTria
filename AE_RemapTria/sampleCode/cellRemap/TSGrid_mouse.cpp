#include "TSGrid.h"

//*******************************************************************************
void TSGrid::mouseDoubleClickEvent(QMouseEvent *event)
{
    bool b = false;
    Qt::KeyboardModifiers mod = QApplication::keyboardModifiers();
    bool IsS = ((mod & Qt::ShiftModifier)==Qt::ShiftModifier);
    //bool IsC = ((mod & Qt::ControlModifier)==Qt::ControlModifier);

    if ( event->button() ==Qt::LeftButton){
        int area = m_area.getArea(event->x(),event->y());
        if (area == AreaCaption){
            if (IsS){
                showCellRenameDialog();
            }else{
               b = selALL();
            }
        } else if (area == AreaCell){
            b = selToEnd();
        }
    }
    if(b) {
        this->update();
    }
}
//*******************************************************************************
void TSGrid::mousePressEvent(QMouseEvent *e )
{
        mousePressExec(e);
}
//*******************************************************************************
void TSGrid::mousePressExec(QMouseEvent *e)
{
    bool b = false;
    if (mdMode == mdModeScroll){
        cursorCloseHand();
        mdPos.setX(e->x());
        mdPos.setY(e->y());
        mdDispXY.setX(m_area.DispX);
        mdDispXY.setY(m_area.DispY);
        b= true;
    }else{
        if ( e->button() ==Qt::LeftButton){
            int area = m_area.getArea(e->x(),e->y());
            if (area == AreaCaption){
                if (m_sel.setTargetCell(m_area.cell())) b = true;
            } else if (area == AreaFrame){
            } else if (area == AreaCell){
                if(m_area.isShift()){
                    b = m_sel.setPoint(m_area.frame());
                }else{
                    b = m_sel.setTargetCell(m_area.cell());
                    b =  m_sel.setStartLast(m_area.frame(),m_area.frame());
                    mdFrame = m_area.frame();
                    mdMode = mdModeSelection;
                }
            }
        }
    }
    if(b) {
        grabMouse();
        this->update();
    }
}

//*******************************************************************************
void TSGrid::mouseReleaseEvent(QMouseEvent * )
{
    mouseReleaseExec();
}
//*******************************************************************************
void TSGrid::mouseReleaseExec()
{
    if (mdMode == mdModeScroll){
        cursorOpenHand();
    }
     releaseMouse();
     mdMode = mdModeNormal;
}
//*******************************************************************************
void TSGrid::mouseMoveEvent(QMouseEvent *e)
{
    mouseMoveExec(e);
}
//*******************************************************************************
void TSGrid::mouseMoveExec(QMouseEvent *e)
{
    if (mdMode==mdModeScroll){
        int nx = mdDispXY.x() - e->x() + mdPos.x();
        if (nx<0)nx=0;
        else if(nx>m_area.DispXMax) nx = m_area.DispXMax;
        int ny = mdDispXY.y() - e->y() + mdPos.y();
        if (ny<0)ny=0;
        else if(ny>m_area.DispYMax) ny = m_area.DispYMax;

        if ( (m_area.DispX!= nx)||(m_area.DispY!= ny)){
           setDispX(nx);
           setDispY(ny);
           this->update();
        }
    }else if (mdMode == mdModeSelection){
        if ( e->y()<m_area.CellTop){
            setDispY(m_area.DispY - m_area.CellHeight);
        }else if( e->y()>= m_area.CellBottom){
            setDispY(m_area.DispY + m_area.CellHeight);
        }
        switch( m_area.getArea(e->x(),e->y())){
        case AreaCell:
            m_sel.setStartLast(mdFrame,m_area.frame());
            break;
        }
        this->update();
    }
}
//*******************************************************************************
void TSGrid::cursorOpenHand()
{
    if (this->cursor().shape() != Qt::OpenHandCursor)
        this->setCursor(Qt::OpenHandCursor);
}

//*******************************************************************************
void TSGrid::cursorCloseHand()
{
    if (this->cursor().shape() != Qt::ClosedHandCursor)
        this->setCursor(Qt::ClosedHandCursor);

}

//*******************************************************************************
void TSGrid::cursorArrow()
{
    if (this->cursor().shape() != Qt::ArrowCursor)
        this->setCursor(Qt::ArrowCursor);

}
