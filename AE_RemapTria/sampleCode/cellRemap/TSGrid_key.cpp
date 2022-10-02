#include "TSGrid.h"

//*******************************************************************************
void	TSGrid::keyPressEvent(QKeyEvent * event){
    keyPressExec(event);
}
//*******************************************************************************
void	TSGrid::keyReleaseEvent(QKeyEvent * )
{
    keyReleaseExec();
}
//*******************************************************************************
bool TSGrid::keyReleaseExec()
{
    mdMode = mdModeNormal;
    cursorArrow();
    releaseKeyboard();
   return true;
}
//*******************************************************************************
bool TSGrid::keyPressExec(QKeyEvent * event)
{
   bool ret = false;
   if (mdMode == mdModeScroll) return ret;
   int k = event->key();

   if (k==Qt::Key_Space ){
       mdMode = mdModeScroll;
       cursorOpenHand();
       grabKeyboard();
       return ret;
   }

   Qt::KeyboardModifiers mod = QApplication::keyboardModifiers();
   if((mod & Qt::ShiftModifier)==Qt::ShiftModifier){
       k |= KEYSHIFT;
   }else if ((mod & Qt::ControlModifier)==Qt::ControlModifier){
       k |= KEYCONTROL;
   }else if ((mod & Qt::MetaModifier)==Qt::MetaModifier){
       k |= KEYMETA;
   }
  // qDebug() << QString("key:0x%1(%2)").arg(k,0,16).arg(event->text());
    if ( (k>=Qt::Key_0)&&(k<=Qt::Key_9)){
        ret = inputNum(event->text());
    }else if ( (k>=(Qt::Key_0|KEYCONTROL))&&(k<=(Qt::Key_9|KEYCONTROL))){

        ret = selLength(event->text());

    }else if ( (k==Qt::Key_Enter)||(k==Qt::Key_Return)){
        ret = inputEnter();
    }else if (k==Qt::Key_Period){
        ret = inputPeriod();
    }else if ( k==Qt::Key_Backspace){
        ret = inputBS();
    }else if ( k==Qt::Key_Delete){
        ret = inputClear();
    }else if ( k==Qt::Key_Plus){
        ret = inputPluss();
    }else if ( k==Qt::Key_Minus){
        ret = inputMinus();
    }else if ( k==(Qt::Key_C |KEYCONTROL)){
        copy();
        ret = true;
    }else if ( k==(Qt::Key_X |KEYCONTROL)){
        cut();
        ret = true;
    }else if ( k==(Qt::Key_V |KEYCONTROL)){
        paste();
        ret = true;
    }else if ( k==Qt::Key_PageUp){
        ret = pageUp();
    }else if ( k==Qt::Key_PageDown){
        ret = pageDown();
    }else if ( k==Qt::Key_Home){
        ret = gotoTop();
    }else if ( k==Qt::Key_End){
        ret = gotoEnd();
    }else if (( k==(Qt::Key_End | KEYSHIFT))||( k==(Qt::Key_End | KEYCONTROL))){
        ret = selToEnd();
    }else if ( k==Qt::Key_Up){
        ret = selMovUp();
    }else if ( k==(Qt::Key_Up|KEYSHIFT)){
        ret = selTailMinus();
    }else if ( k==Qt::Key_Down){
        ret = selMovDown();
    }else if ( k==(Qt::Key_Down|KEYSHIFT)){
        ret = selTailPluss();
    }else if ( k==(Qt::Key_Down|KEYCONTROL)){
        ret = scrolDown();
    }else if ( k==(Qt::Key_Up|KEYCONTROL)){
        ret = scrolUp();
    }else if ( k==Qt::Key_Left){
        ret = selMovLeft();
    }else if ( k==Qt::Key_Right){
        ret = selMovRight();
    }else if ((k==Qt::Key_Asterisk)||(k==Qt::Key_X)){
        ret = selTailPluss();
    }else if ((k==Qt::Key_Slash)||(k==Qt::Key_Z)){
        ret = selTailMinus();
    }else if (k==(Qt::Key_S | KEYCONTROL)){
        toXMP();
    }else if (k==(Qt::Key_Q | KEYCONTROL)){
        m_sheet->close();
    }else if (k==(Qt::Key_K | KEYCONTROL)){
        showSheetDialog();
    }else if (k==(Qt::Key_L | KEYCONTROL)){
        showPrefDialog();
    }else if (k==(Qt::Key_E | KEYSHIFT)){
        toExpression();
    }else if (k==(Qt::Key_E | KEYCONTROL)){
        toRemap();
    }else if (k==(Qt::Key_J | KEYCONTROL)){
       autoInputDialog();
    }else if (k==(Qt::Key_Y | KEYCONTROL)){
       showCalcDialog();
    }else if (k==Qt::Key_B){
        ret = m_sel.moveCellLeft();
    }else if (k==Qt::Key_N){
        ret = m_sel.moveCellRight();
    }

    if (ret){
        grabKeyboard();
        this->update();
    }
    return ret;

}
