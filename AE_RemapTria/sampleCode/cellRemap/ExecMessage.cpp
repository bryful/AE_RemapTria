#include "ExecMessage.h"

//*********************************************************************
ExecMessage::ExecMessage(QWidget *sheet,QWidget *parent) : QWidget(parent)
{

    this->setWindowFlags(Qt::Dialog | Qt::FramelessWindowHint | Qt::WindowStaysOnTopHint);
    m_sheet = sheet;
    connect(&timer, SIGNAL(timeout()), this, SLOT(close()));

}
//*********************************************************************
void ExecMessage::showMessage(QString mes, int tm)
{
    QRect r = m_sheet->geometry();
    r.setHeight(32);
    this->setGeometry(r);

    m_mes = mes;
    timer.start(tm);
    this->show();
}
//*********************************************************************
void ExecMessage::paintEvent(QPaintEvent *)
{
    QRect r = rect();
    QPainter pnt(this);
    pnt.fillRect(r,QColor(255,255,255));
    pnt.setPen(QColor(0,0,0));
    QFont f = pnt.font();
    f.setBold(true);
    pnt.setFont(f);
    pnt.drawText(r,Qt::AlignCenter,m_mes);
    r.setWidth(r.width()-1);
    r.setHeight(r.height()-1);
    pnt.drawRect(r);
}
