#include "TSGrid.h"
//*******************************************************************************
void TSGrid::newSheet()
{
    QString exePath = QApplication::arguments()[0];
    m_sheet->dialogShowBefore(true);
#if defined(Q_OS_MAC)
    // .app/Contents/MacOS/cellRemap
    int idx = exePath.indexOf(".app");
    if (idx>=0){
        exePath = exePath.left(idx+4);
    }
    QString ap = "open -n";
    ap += " \"" + exePath +"\"";
    QProcess::execute(ap);
#elif defined(Q_OS_WIN32)
    QProcess::startDetached("\"" + exePath +"\"");
#endif
    m_sheet->dialogShowAfter();
 }
//*******************************************************************************
bool TSGrid::save(QString p){
    bool ret = m_data.save(p);
    if (ret){
        m_data.modiFlag = false;
        setStatus();
        dataChanged(infoStr());
    }
    return ret;
}
//*******************************************************************************
bool TSGrid::load(QString p)
{
    bool ret = m_data.load(p);
    if (ret){
        if (! m_data.afterFXPath().isEmpty()){
            if (m_data.afterFXPath() != m_pref.afterFXPath()){
                m_pref.setAfterFXPath(m_data.afterFXPath());
            }
        }
        setStatus();
        m_data.modiFlag = false;
        dataChanged(infoStr());
    }
    return ret;
}
//*******************************************************************************
void TSGrid::toRemap()
{
    bool b = m_ae->toRemap();
    if(m_mes==0)m_mes = new ExecMessage(m_sheet);

    m_sheet->activeSheet();
    if(b){
        m_mes->showMessage(tr("To Time Remap"),350);
    }else{
        m_mes->showMessage(tr("Error To Remap!"),750);
    }
}
//*******************************************************************************
void TSGrid::toExpression()
{
    bool b = m_ae->toExpression();
    if(m_mes==0)m_mes = new ExecMessage(m_sheet);

    m_sheet->activeSheet();
    if(b){
        m_mes->showMessage(tr("Expression To Clipbord"),350);
    }else{
        m_mes->showMessage(tr("Error Expression!"),750);
    }
}

//*******************************************************************************
void TSGrid::toXMP()
{
    bool b = m_ae->toXMP();
    if(m_mes==0)m_mes = new ExecMessage(m_sheet);

    m_sheet->activeSheet();
    if(b){
        m_mes->showMessage(tr("To XMP"),350);
        m_data.modiFlag = false;
        dataChanged(infoStr());
    }else{
        m_mes->showMessage(tr("Error ToXMP!"),750);
    }
}
//*******************************************************************************
void TSGrid::fromRemap()
{
    bool b = m_ae->fromRemap();
    if(!b){
        m_sheet->activeSheet();
        if(m_mes==0)m_mes = new ExecMessage(m_sheet);
        m_mes->showMessage(tr("error From remap!"),750);
    }
}
//*******************************************************************************
void TSGrid::fromRemapFinshed(bool b)
{
    if(m_mes==0)m_mes = new ExecMessage(m_sheet);
    m_sheet->activeSheet();
    if(b){
        m_mes->showMessage("From Time Remap",350);
        setModiFlag();
        this->update();
    }else{
        m_mes->showMessage("error !",500);

    }
}
//*******************************************************************************
void TSGrid::openAfterFX()
{
    m_ae->execAfterFX();
}
//*******************************************************************************
void TSGrid::prefWrite()
{
    if (m_sheet != NULL){
        m_pref.winPos   = m_sheet->pos();
        m_pref.winSize  = m_sheet->size();
    }else{
        m_pref.winPos   = QPoint(0,0);
        m_pref.winSize  = QSize(0,0);
    }
    m_pref.prefSave();
}
//*******************************************************************************
void TSGrid::prefRead()
{
    Qt::KeyboardModifiers mod = QApplication::keyboardModifiers();
    bool isControl = ( (mod & Qt::ControlModifier) ==Qt::ControlModifier);
    bool isShift = ( (mod & Qt::ShiftModifier)==Qt::ShiftModifier);
    m_pref.winPos   = QPoint(0,0);
    m_pref.winSize  = QSize(0,0);

    if ((isControl==false)&&(isShift==false)) {
        m_pref.prefLoad();
        prefToData();
    }
    navBarChanged(m_pref.isNavBar);
    stayOnChanged(m_pref.isStayOn);

    if (m_sheet != NULL){
        if ((m_pref.winSize.width()>0)&&(m_pref.winSize.height()>0)&&(m_pref.winPos.x()>0)&&(m_pref.winPos.y()>0)){
            qsrand( QDateTime::currentMSecsSinceEpoch() & 0xFFFF);
            int ax = (int)(50 - 100  * (double)qrand()/RAND_MAX);
            int ay = (int)(50 - 100  * (double)qrand()/RAND_MAX);
            m_pref.winPos.setX(m_pref.winPos.x() + ax);
            m_pref.winPos.setY(m_pref.winPos.y() + ay);
            QRect rct(m_pref.winPos,m_pref.winSize);
            m_sheet->setGeometry(rct);
        }else{
            QList<QScreen *> scrs = QApplication::screens();
            if (scrs.size()>0){
                QRect r = scrs[0]->geometry();
                QPoint pos;
                pos.setX( (r.width() - this->width()) /2);
                pos.setY( (r.height() - this->height()) /2);
                m_sheet->move(pos);
            }
        }
    }
}
