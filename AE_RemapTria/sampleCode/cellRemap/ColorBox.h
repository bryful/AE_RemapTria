#ifndef COLORBOX_H
#define COLORBOX_H

#include <QWidget>
#include <QPainter>
#include <QColor>
#include <QColorDialog>

class ColorBox : public QWidget
{
    Q_OBJECT
private:
    QColor m_Color;
public:
    explicit ColorBox(QWidget *parent = 0);
    void setColor(QColor c);
    QColor getColor();
signals:

public slots:

protected:
    void mouseDoubleClickEvent(QMouseEvent *);
    void paintEvent(QPaintEvent *);
};

#endif // COLORBOX_H
