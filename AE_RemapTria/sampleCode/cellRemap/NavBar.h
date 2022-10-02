#ifndef NAVBAR_H
#define NAVBAR_H

#include <QWidget>
#include <QLabel>
#include <QVBoxLayout>
#include <QPainter>
#include <QTime>

class NavBar : public QWidget
{
    Q_OBJECT

public:
    explicit NavBar(QWidget *parent = 0);
    ~NavBar();

    void SentPosChanged();
signals:
    void actived();
    void posChanged(QPoint);
public slots:
    void setWinPos(QPoint pos);
    void setWinWidth(int w);
    void infoStr(QString s);
    void setStayOn(bool b,bool IsShow = true);
private:

    bool m_mp;
    QPoint m_mousePos;
    QPoint m_winPos;
    QString m_infoStr;
    QColor backColor;
    QColor textColor;
    int m_WinHeight;

protected:
    void paintEvent(QPaintEvent *);
    void mousePressEvent(QMouseEvent *);
    void mouseMoveEvent(QMouseEvent *);
    void mouseReleaseEvent(QMouseEvent *);

    void focusInEvent(QFocusEvent *);
};

#endif // NAVBAR_H
