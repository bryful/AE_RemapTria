#ifndef TSSELECTION_H
#define TSSELECTION_H


#include <QApplication>
#include <QClipboard>
#include <QMimeData>

#include "TSData.h"
class TSData;

class TSSelection
{
private:
    TSData *data;
    int m_start;
    int m_last;
    int m_length;
    int m_targetCell;
    void chk();
public:
    TSSelection(TSData *d = 0);
    void setTSData(TSData *d);
    int start(){return m_start;}
    int last(){return m_last;}
    int length(){return m_length;}
    int targetCell(){return m_targetCell;}

    bool setStart(int v);
    bool setLast(int v);
    bool setLength(int v);
    bool setTargetCell(int v);
    bool setStartLast(int s,int l);
    bool setPoint(int v);

    bool moveUp();
    bool moveDown();
    bool moveLeft();
    bool moveRight();
    bool down(int v);
    bool up(int v);

    bool toEnd();
    bool aLL();

    bool tailPluss();
    bool tailMinus();
    bool inSelFrame(int f){return ((f>=m_start)&&(f<=m_last));}
    bool inSelCel(int c){return (m_targetCell ==c);}
    bool inSel(int c,int f){return ((f>=m_start)&&(f<=m_last)&&(m_targetCell ==c));}

    bool setNum(int v);
    int getNumPrev();

    bool copy();
    bool cut();
    bool paste();

    QString getCaption();
    bool    setCaption(QString s);
    bool cellRemove();
    void autoInput(int s,int last, int len);
    bool moveCellLeft();
    bool moveCellRight();
    void frameInsert(bool IsAll = true,bool isLength = true);
    void frameDelete(bool IsAll = true,bool isLength = true);
    void calc(int value,int mode);

};

#endif // TSSELECTION_H
