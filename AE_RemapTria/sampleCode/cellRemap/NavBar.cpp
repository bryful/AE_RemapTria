#include "NavBar.h"

int randV()
{
    int r = 0;
    r = 128 + (128 *(rand() & 0xFFFF)/0xFFFF);
    if (r>255) r = 255;
    return r;
}

NavBar::NavBar(QWidget *parent) : QWidget(parent)
{
    m_mp = false;

    this->setWindowFlags(Qt::FramelessWindowHint|Qt::WindowStaysOnTopHint | Qt::NoDropShadowWindowHint);
    this->setMinimumSize(0,0);

    QTime t = QTime::currentTime();
    srand(t.msec());

    backColor = QColor(randV(),randV(),randV());

    textColor = QColor(0,0,0);
    m_WinHeight = 20;
    m_infoStr = "cellRemap";

}

NavBar::~NavBar()
{
}

void NavBar::setWinPos(QPoint pos)
{
    pos.setY(pos.y() - this->frameSize().height());
    this->move(pos);
}
//*************************************************************
void NavBar::setWinWidth(int w)
{
    QRect rct = this->geometry();
    w /=3;
    if (w<200) w= 200;
    rct.setWidth(w);
    rct.setHeight(m_WinHeight);
    this->setGeometry(rct);
}
//*************************************************************
void NavBar::infoStr(QString s)
{
    m_infoStr = s;
    this->update();
}
//*************************************************************
void NavBar::focusInEvent(QFocusEvent *)
{
    actived();

}
//*************************************************************
void NavBar::SentPosChanged()
{
    QPoint pos = this->pos();
    pos.setY(pos.y() + this->frameSize().height());
    posChanged(pos);
}

//*************************************************************
void NavBar::mousePressEvent(QMouseEvent *)
{
    m_winPos = pos();
    m_mousePos = QCursor::pos();
    m_mp = true;
    actived();
    grabMouse();
}
//*************************************************************
void NavBar::mouseMoveEvent(QMouseEvent *)
{
    if (m_mp){
        move(m_winPos + QCursor::pos() - m_mousePos);
        SentPosChanged();
    }
}
//*************************************************************
void NavBar::mouseReleaseEvent(QMouseEvent *)
{
    m_mp = false;
    releaseMouse();
}
void NavBar::paintEvent(QPaintEvent *)
{
    QPainter pnt(this);
    QRect r = rect();
    QLinearGradient g(0,0,0,m_WinHeight);
    pnt.fillRect(r,backColor);
    if (! m_infoStr.isEmpty()){
        pnt.setPen(textColor);
        pnt.drawText(r.left()+16,r.top(),r.width()-32,r.height(),Qt::AlignLeft | Qt::AlignVCenter,m_infoStr);

    }
    pnt.drawRect(r.left(),r.top(),r.width()-1,r.height()-1);
}
void NavBar::setStayOn(bool b,bool IsShow)
{
    if (b){
        this->setWindowFlags(Qt::FramelessWindowHint | Qt::WindowStaysOnTopHint);
    }else{
        if((this->windowFlags() & Qt::WindowStaysOnTopHint) == Qt::WindowStaysOnTopHint){
            this->setWindowFlags(Qt::FramelessWindowHint | Qt::WindowStaysOnBottomHint);
        }else{
            this->setWindowFlags(Qt::FramelessWindowHint);
        }
    }
    if (IsShow) this->show();

}
