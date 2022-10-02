#include "TSGrid.h"
//*******************************************************************************
void TSGrid::showSheetDialog(bool IsShow)
{
   SheetDialog *dlg = new SheetDialog(m_data);
   m_sheet->dialogShowBefore(IsShow);
   if ( dlg->exec()==  QDialog::Accepted){
       setSize(dlg->cellCount(),dlg->frameCount());
       setFrameRate(dlg->frameRate());
       setSheetName(dlg->sheetName());
       setStatus();
       dataChanged(infoStr());
   }
   delete dlg;
   m_sheet->dialogShowAfter(IsShow);
}
//*******************************************************************************
void TSGrid::showPrefDialog(bool IsShow)
{
    m_sheet->dialogShowBefore(IsShow);
    PrefDialog *dlg = new PrefDialog(m_pref,this);
    if (dlg->exec() == QDialog::Accepted){
        m_pref.assign(dlg->getPref());
        setStatus();
        dataChanged(infoStr());
        this->update();
    }
    delete dlg;
    m_sheet->dialogShowAfter(IsShow);

}
//*******************************************************************************
void TSGrid::showCellRenameDialog()
{
    m_sheet->dialogShowBefore();
    CellRenameDialog *dlg = new CellRenameDialog(this);
    dlg->setCellName(m_sel.getCaption());
    if (dlg->exec()==QDialog::Accepted)
    {
        m_sel.setCaption(dlg->cellName());
        dataChanged(infoStr());
        setStatus();
        this->update();
    }
    delete dlg;
    m_sheet->dialogShowAfter();
}

//*******************************************************************************
void TSGrid::showCellInsertDialog()
{
    m_sheet->dialogShowBefore();
    CellInsertDialog *dlg = new CellInsertDialog(this);
    dlg->setCellName(m_sel.getCaption()+"_st");
    if (dlg->exec()==QDialog::Accepted)
    {
        dataChanged(infoStr());
        m_data.cellInsert(m_sel.targetCell(),dlg->cellName());
        m_area.getStatus();
        setStatus();
        dataChanged(infoStr());
        this->update();
    }
    delete dlg;
    m_sheet->dialogShowAfter();
}
//*******************************************************************************
void TSGrid::showCellRemoveDialog()
{
    m_sheet->dialogShowBefore();
    QString cap = m_sel.getCaption();

    QMessageBox::StandardButton btn = QMessageBox::question(
                this,
                tr("Cell Remove"),
                tr("[") + cap + tr("] ") + tr("Remove cell?"),
                QMessageBox::Yes | QMessageBox::No
                );
    if (btn == QMessageBox::Yes){
        dataChanged(infoStr());
        m_sel.cellRemove();
        m_area.getStatus();
        setStatus();
       dataChanged(infoStr());
    }
    m_sheet->dialogShowAfter();
}
//*******************************************************************************
void TSGrid::autoInputDialog()
{
    m_sheet->dialogShowBefore();
    AutoInputDialog dlg(m_AutoInputStart,m_AutoInputLast,m_AutoInputLength, m_sheet);
    if (dlg.exec() ==QDialog::Accepted){
        dataChanged(infoStr());
        m_sel.autoInput(dlg.startCellNum(),dlg.lastCellNum(),dlg.lengthCellNum());
        m_AutoInputStart = dlg.startCellNum();
        m_AutoInputLast = dlg.lastCellNum();
        m_AutoInputLength = dlg.lengthCellNum();
        this->update();
    }
    m_sheet->dialogShowAfter();
}
//*******************************************************************************
void TSGrid::frameInset()
{
    m_sheet->dialogShowBefore();
    frameInsDel(true);
    m_sheet->dialogShowAfter();
}

//*******************************************************************************
void TSGrid::frameDelete()
{
    m_sheet->dialogShowBefore();
    frameInsDel(false);
    m_sheet->dialogShowAfter();
}
//*******************************************************************************
void TSGrid::frameInsDel(bool ins)
{
    FrameDialog *dlg = new FrameDialog(this);

    dlg->setIsInsert(ins);
    dlg->setCellAll(m_FrameCellAll);
    dlg->setIsFrameCountChange(m_FrameCountChange);
    if (dlg->exec() == QDialog::Accepted){
        dataChanged(infoStr());
        m_FrameCellAll = dlg->isCellAll();
        m_FrameCountChange = dlg->isFrameCountChange();
        if (dlg->isInsert()){
            m_sel.frameInsert(m_FrameCellAll,m_FrameCountChange);

        }else{
            m_sel.frameDelete(m_FrameCellAll,m_FrameCountChange);
        }
        m_area.getStatus();
        setStatus();
        dataChanged(infoStr());
    }
    delete dlg;
}
//*******************************************************************************
void TSGrid::showCalcDialog()
{
    m_sheet->dialogShowBefore();
    CalcDialog dlg(this);
    dlg.setValue(m_calc_value);
    dlg.setMode(m_calc_mode);
    if (dlg.exec() ==QDialog::Accepted){
        calc(dlg.value(),dlg.mode());
        dataChanged(infoStr());
        m_calc_value = dlg.value();
        m_calc_mode = dlg.mode();

        this->update();
    }
    m_sheet->dialogShowAfter();
}
