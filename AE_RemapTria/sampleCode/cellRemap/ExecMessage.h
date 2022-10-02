#ifndef EXECMESSAGE_H
#define EXECMESSAGE_H

#include <QWidget>
#include <QLabel>
#include <QRect>
#include <QTimer>
#include <QPainter>

class ExecMessage : public QWidget
{
    Q_OBJECT

public:
    explicit ExecMessage(QWidget *sheet,QWidget *parent = 0);
    QTimer timer;
    QString m_mes;
    QWidget *m_sheet;
    void showMessage(QString mes, int tm= 500);
signals:

public slots:
protected:
    void paintEvent(QPaintEvent *);

};

#endif // EXECMESSAGE_H
