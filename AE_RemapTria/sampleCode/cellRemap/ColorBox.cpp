#include "ColorBox.h"

ColorBox::ColorBox(QWidget *parent) :
    QWidget(parent)
{
    m_Color = QColor(255,255,255);
    this->setFixedSize(30,20);
}

void ColorBox::paintEvent(QPaintEvent *)
{
    QPainter pnt(this);

    QRect r = rect();
    pnt.fillRect(r,m_Color);
    r.setWidth(r.width()-1);
    r.setHeight(r.height()-1);
    pnt.drawRect(r);

}

void ColorBox::mouseDoubleClickEvent(QMouseEvent *)
{
    QColor c = QColorDialog::getColor(m_Color,this);
    if (c.isValid()) {
        m_Color = c;
        this->update();
    }

}
void ColorBox::setColor(QColor c)
{
    m_Color =c;
}

QColor ColorBox::getColor()
{
    return m_Color;
}
