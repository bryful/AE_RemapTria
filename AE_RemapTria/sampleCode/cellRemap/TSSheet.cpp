#include "TSSheet.h"

//*********************************************************************************************
TSSheet::TSSheet(QWidget *parent) :
    QMainWindow(parent)
{
    m_IsShowDialogs = true;

    this->resize(270,490);
    m_grid = new TSGrid;
    m_grid->setTSSheet(this);
    m_grid->setFocusPolicy(Qt::WheelFocus);
    m_grid->prefRead();

    setCentralWidget(m_grid);
    createNavBar();
    createMenuBar();


    connect(m_grid,SIGNAL(dataChanged(QString)),this,SLOT(infoStr(QString)));

    m_grid->setFocus();
    m_IsShowDialogs = false;
}

//*********************************************************************************************
TSSheet::~TSSheet()
{
}
//*********************************************************************************************
void TSSheet::createMenuBar()
{
    this->menuBar()->setNativeMenuBar(false);
    //File
    QMenu *menuFile = new QMenu(tr("File"));

    actionToXMP = new QAction(tr("ToXMP"),this);
    menuFile->addAction(actionToXMP);
    connect(actionToXMP,SIGNAL(triggered()),m_grid,SLOT(toXMP()));

    menuFile->addSeparator();

    actionNew = new QAction(tr("New"),this);
    menuFile->addAction(actionNew);
    connect(actionNew,SIGNAL(triggered()),this,SLOT(newSheet()));

    actionImport = new QAction(tr("Import"),this);
    menuFile->addAction(actionImport);
    connect(actionImport,SIGNAL(triggered()),this,SLOT(loadAsDialog()));

    actionExport = new QAction(tr("Export"),this);
    menuFile->addAction(actionExport);
    connect(actionExport,SIGNAL(triggered()),this,SLOT(saveAsDialog()));

    menuFile->addSeparator();

    actionFromClip = new QAction(tr("FromClip"),this);
    menuFile->addAction(actionFromClip);
    connect(actionFromClip,SIGNAL(triggered()),m_grid,SLOT(fromClip()));

    menuFile->addSeparator();

    actionToClip = new QAction(tr("ToClip"),this);
    menuFile->addAction(actionToClip);
    connect(actionToClip,SIGNAL(triggered()),m_grid,SLOT(toClip()));

    menuFile->addSeparator();

    actionAfterFX = new QAction(tr("Execute After Effects"),this);
    menuFile->addAction(actionAfterFX);
    connect(actionAfterFX,SIGNAL(triggered()),m_grid,SLOT(openAfterFX()));

    menuFile->addSeparator();

    actionQuit = new QAction(tr("Quit"),this);
    menuFile->addAction(actionQuit);
    connect(actionQuit,SIGNAL(triggered()),this,SLOT(close()));

    this->menuBar()->addMenu(menuFile);

    //Edit
    QMenu *menuEdit = new QMenu(tr("Edit"));

    actionCopy = new QAction(tr("Copy"),this);
    menuEdit->addAction(actionCopy);
    connect(actionCopy,SIGNAL(triggered()),m_grid,SLOT(copy()));

    actionCut = new QAction(tr("Cut"),this);
    menuEdit->addAction(actionCut);
    connect(actionCut,SIGNAL(triggered()),m_grid,SLOT(cut()));

    actionPaste = new QAction(tr("Paste"),this);
    menuEdit->addAction(actionPaste);
    connect(actionPaste,SIGNAL(triggered()),m_grid,SLOT(paste()));

    menuEdit->addSeparator();

    actionSheet = new QAction(tr("Sheet Settings"),this);
    menuEdit->addAction(actionSheet);
    connect(actionSheet,SIGNAL(triggered()),this,SLOT(showSheetDialog()));

    actionPref = new QAction(tr("Preferences"),this);
    menuEdit->addAction(actionPref);
    connect(actionPref,SIGNAL(triggered()),this,SLOT(showPrefDialog()));

    this->menuBar()->addMenu(menuEdit);

    //Func
    QMenu *menuFunc = new QMenu(tr("Func"));

    actionToRemap = new QAction(tr("ToRemap"),this);
    menuFunc->addAction(actionToRemap);
    connect(actionToRemap,SIGNAL(triggered()),m_grid,SLOT(toRemap()));


    actionToExpression = new QAction(tr("ToExpression"),this);
    menuFunc->addAction(actionToExpression);
    connect(actionToExpression,SIGNAL(triggered()),m_grid,SLOT(toExpression()));

    menuFunc->addSeparator();

    actionCellRename = new QAction(tr("CellRename"),this);
    menuFunc->addAction(actionCellRename);
    connect(actionCellRename,SIGNAL(triggered()),m_grid,SLOT(showCellRenameDialog()));

    actionCellInsert = new QAction(tr("CellInsert"),this);
    menuFunc->addAction(actionCellInsert);
    connect(actionCellInsert,SIGNAL(triggered()),m_grid,SLOT(showCellInsertDialog()));

    actionCellRemove = new QAction(tr("CellRemove"),this);
    menuFunc->addAction(actionCellRemove);
    connect(actionCellRemove,SIGNAL(triggered()),m_grid,SLOT(showCellRemoveDialog()));

    menuFunc->addSeparator();

    actionFromRemap = new QAction(tr("FromRemap"),this);
    menuFunc->addAction(actionFromRemap);
    connect(actionFromRemap,SIGNAL(triggered()),m_grid,SLOT(fromRemap()));

    menuFunc->addSeparator();

    actionAutoInput = new QAction(tr("AutoInput"),this);
    menuFunc->addAction(actionAutoInput);
    connect(actionAutoInput,SIGNAL(triggered()),m_grid,SLOT(autoInputDialog()));

    actionCalc = new QAction(tr("Calc"),this);
    menuFunc->addAction(actionCalc);
    connect(actionCalc,SIGNAL(triggered()),m_grid,SLOT(showCalcDialog()));

    menuFunc->addSeparator();

    actionFrameAdd = new QAction(tr("FrameInsert"),this);
    menuFunc->addAction(actionFrameAdd);
    connect(actionFrameAdd,SIGNAL(triggered()),m_grid,SLOT(frameInset()));
    actionFrameDel = new QAction(tr("FrameDelete"),this);
    menuFunc->addAction(actionFrameDel);
    connect(actionFrameDel,SIGNAL(triggered()),m_grid,SLOT(frameDelete()));

    this->menuBar()->addMenu(menuFunc);

    //Window
    QMenu *menuWindow = new QMenu(tr("Window"));

    actionNavBar = new QAction(tr("NavBar"),this);
    actionNavBar->setCheckable(true);
    menuWindow->addAction(actionNavBar);
    actionNavBar->setChecked(m_grid->isNavBar());

    connect(actionNavBar,SIGNAL(triggered(bool)),this,SLOT(setNavBar(bool)));
    connect(this,SIGNAL(navBarChanged(bool)),m_grid,SLOT(setNavBar(bool)));
    connect(m_grid,SIGNAL(navBarChanged(bool)),actionNavBar,SLOT(setChecked(bool)));

    actionStayOn = new QAction(tr("Stay on top"),this);
    actionStayOn->setCheckable(true);
    menuWindow->addAction(actionStayOn);

    actionStayOn->setChecked(m_grid->isStayOn());
    setStayOn(m_grid->isStayOn(),false);
    qDebug() <<m_grid->isStayOn();
    connect(actionStayOn,SIGNAL(toggled(bool)),this,SLOT(setStayOn(bool)));
    connect(this,SIGNAL(stayOnChanged(bool)),m_grid,SLOT(setStayOn(bool)));
    connect(m_grid,SIGNAL(stayOnChanged(bool)),actionStayOn,SLOT(setChecked(bool)));


    this->menuBar()->addMenu(menuWindow);

    //Help
    QMenu *menuHelp = new QMenu(tr("Help"));

    actionAbout = new QAction(tr("About"),this);
    menuHelp->addAction(actionAbout);
    connect(actionAbout,SIGNAL(triggered()),this,SLOT(showAbout()));

    actionKeyHelp = new QAction(tr("KeyHelp"),this);
    menuHelp->addAction(actionKeyHelp);
    connect(actionKeyHelp,SIGNAL(triggered()),this,SLOT(keyHelpShow()));

    actionHelp = new QAction(tr("Help"),this);
    menuHelp->addAction(actionHelp);
    connect(actionHelp,SIGNAL(triggered()),this,SLOT(openHelp()));

    this->menuBar()->addMenu(menuHelp);

#if defined(Q_OS_MAC)
    menuFile->setTitle(menuFile->title()+"   ");
    menuEdit->setTitle(menuEdit->title()+"   ");
    menuFunc->setTitle(menuFunc->title()+"   ");
    menuWindow->setTitle(menuWindow->title()+"   ");
    menuHelp->setTitle(menuHelp->title()+"  ");
    QMenuBar *m = menuBar();
    m->setFont( QFont("Osaka",12));
#endif
}

//*********************************************************************************************
void TSSheet::setConsoleMode(bool b)
{
    consoleMode = b;
}
//*********************************************************************************************
void TSSheet::setNavBar(bool b)
{
    if (!nav) createNavBar();

    if (b){
        nav->setWinPos(this->pos());
        nav->show();
    }else{
        nav->hide();
    }
    if (actionNavBar->isChecked() != b){
        actionNavBar->setChecked(b);
    }
    navBarChanged(b);
}
//*********************************************************************************************
void TSSheet::createNavBar()
{
    nav = new NavBar;
    nav->setAttribute(Qt::WA_DeleteOnClose);

    connect(m_grid,SIGNAL(dataChanged(QString)),nav,SLOT(infoStr(QString)));
    connect(this,SIGNAL(widthChanged(int)),nav,SLOT(setWinWidth(int)));
    connect(this,SIGNAL(posChanged(QPoint)),nav,SLOT(setWinPos(QPoint)));
    connect(nav,SIGNAL(actived()),this,SLOT(activeSheet()));
    connect(nav,SIGNAL(posChanged(QPoint)),this,SLOT(setWinPos(QPoint)));

}

//*********************************************************************************************
void TSSheet::closeEvent(QCloseEvent *e)
{
    if(m_grid->modiFlag()){
        QMessageBox msgBox;
        msgBox.setText(tr("The document has been modified."));
        msgBox.setInformativeText(tr("Do you want to save your changes?"));
        msgBox.setStandardButtons(/*QMessageBox::Save |*/ QMessageBox::Discard | QMessageBox::Cancel);
        msgBox.setDefaultButton(QMessageBox::Cancel);
        dialogShowBefore();
        int ret = msgBox.exec();
        if(ret !=QMessageBox::Discard){
            e->ignore();
            dialogShowAfter();
            return;
        }
    }

    if (nav) nav->close();
    if (m_keyhelp) m_keyhelp->close();
    m_grid->prefWrite();
    m_grid->setSheetName("");
    m_grid->toClip();
    e->accept();


}
//*********************************************************************************************
void TSSheet::resizeEvent(QResizeEvent *)
{
    widthChanged(this->frameSize().width());
    m_grid->setFocus();
}
//*********************************************************************************************
void TSSheet::moveEvent(QMoveEvent *)
{
    posChanged(this->pos());
}
//*********************************************************************************************
void TSSheet::showEvent(QShowEvent *)
{
    if(m_IsShowDialogs) return;

    if(m_grid->isNavBar()){
        setNavBar(m_grid->isNavBar());
    }
    m_grid->setFocus();


}
//*********************************************************************************************
void TSSheet::mousePressEvent(QMouseEvent *e)
{
    m_grid->mousePressExec(e);
}
//*********************************************************************************************
void TSSheet::mouseMoveEvent(QMouseEvent *e){
    m_grid->mouseMoveExec(e);
}
//*********************************************************************************************
void TSSheet::mouseReleaseEvent(QMouseEvent *)
{
    m_grid->mouseReleaseExec();
}

//*********************************************************************************************
void TSSheet::setWinPos(QPoint pos)
{
    this->move(pos);
}

//*********************************************************************************************
void TSSheet::activeSheet()
{
    this->raise();
    this->activateWindow();
}
//*********************************************************************************************
QString TSSheet::sheetName()
{
    return m_grid->getSheetName();
}
//*********************************************************************************************

void TSSheet::keyPressEvent(QKeyEvent *event)
{
    QApplication::sendEvent(m_grid,event);
    //m_grid->keyPressExec(event);
    m_grid->setFocus();
}
//*********************************************************************************************
void TSSheet::keyReleaseEvent(QKeyEvent *e)
{
    QApplication::sendEvent(m_grid,e);
    m_grid->setFocus();
   // m_grid->keyReleaseExec();

}
//*********************************************************************************************
void TSSheet::showSheetDialog(bool IsShow)
{
    m_grid->showSheetDialog(IsShow);
}
//*********************************************************************************************
void TSSheet::showPrefDialog(bool IsShow)
{
    m_grid->showPrefDialog(IsShow);
}

//*********************************************************************************************
void TSSheet::infoStr(QString s)
{
    this->setWindowTitle(s);
}

//*********************************************************************************************
void TSSheet::newSheet()
{
    m_grid->newSheet();
 }
//*********************************************************************************************
void TSSheet::Save(QString p)
{
    m_grid->save(p);
}
//*********************************************************************************************
void TSSheet::saveAsDialog()
{
    dialogShowBefore(true);

    TSFileDialog *dlg = new TSFileDialog(this);
    dlg->setSaveMode();
    dlg->setCaption(tr("Save Timesheet File"));
    dlg->setDir(m_grid->fileFullName());
    dlg->setFilter(tr(TSSaveFileFiler));
    if (dlg->openDialog()){
        Save(dlg->fileName());
    }
    delete dlg;
    dialogShowAfter(true);
}
//********************************************************************
void TSSheet::load(QString p)
{
    m_grid->load(p);
}
//*********************************************************************************************
void TSSheet::fromClip()
{
    m_grid->fromClip();
}

//*********************************************************************************************
void TSSheet::loadAsDialog()
{
   dialogShowBefore(true);

    TSFileDialog *dlg = new TSFileDialog(this);
    dlg->setLoadMode();
    dlg->setCaption(tr("Open Timesheet File"));
    dlg->setDir(m_grid->FilePath());
    dlg->setFilter(tr(TSLosdFileFiler));
    if (dlg->openDialog()){
        load(dlg->fileName());
    }
    delete dlg;
    dialogShowAfter(true);
}
//*********************************************************************************************
QString TSSheet::afterFXPath()
{
    return m_grid->afterFXPath();
}
//*********************************************************************************************
void TSSheet::setStayOn(bool b,bool IsShow)
{
    if (b){
        this->setWindowFlags(Qt::Window | Qt::WindowStaysOnTopHint);
    }else{
        if((this->windowFlags() & Qt::WindowStaysOnTopHint) == Qt::WindowStaysOnTopHint){
            this->setWindowFlags(Qt::Window | Qt::WindowStaysOnBottomHint);
        }else{
            this->setWindowFlags(Qt::Window);
        }
    }
    if (IsShow){
        this->show();
        this->raise();
        this->activateWindow();
    }
    stayOnChanged(b);
}
//*********************************************************************************************
void TSSheet::dialogShowBefore(bool IsShow)
{
    m_IsShowDialogs = true;
    if (m_grid->isStayOn()){
        if((this->windowFlags() & Qt::WindowStaysOnTopHint) == Qt::WindowStaysOnTopHint){
            this->setWindowFlags(Qt::Window | Qt::WindowStaysOnBottomHint);
        }else{
            this->setWindowFlags(Qt::Window);
        }
    }
    if(nav!=0){
        nav->setVisible(false);
    }
    if (IsShow){
        this->show();
    }
}
//*********************************************************************************************
void TSSheet::dialogShowAfter(bool IsShow)
{
    m_IsShowDialogs = false;
    if (m_grid->isStayOn()){
        this->setWindowFlags(Qt::Window | Qt::WindowStaysOnTopHint);
    }
    if(nav!=0){
        nav->show();
    }
    if (IsShow){
        this->show();
        this->raise();
        this->activateWindow();
    }

}
//*********************************************************************************************
void TSSheet::keyHelpShow()
{
    if (!m_keyhelp){
        m_keyhelp = new KeyHelpForm();
        m_keyhelp->setWindowFlags(Qt::WindowStaysOnTopHint);
        m_keyhelp->setReadOnly(true);
        m_keyhelp->setGeometry(m_keyhelp->pos().x(),m_keyhelp->pos().y(),350,400);
        QFile f(":/res/keyHelp.html");
        f.open(QIODevice::ReadOnly);
        QTextStream in(&f);
        in.setCodec("utf-8");
        m_keyhelp->setHtml(in.readAll());
        f.close();
        QPoint p =m_keyhelp->pos();
        if ((p.x()<=5)||(p.y()<=5)){
            p.setX(50);
            p.setY(50);
            m_keyhelp->move(p);
        }
    }
    m_keyhelp->show();
}

//*********************************************************************************************
void TSSheet::showAbout()
{
    QMessageBox::about(
                this,
                "cellRemap",
                "cellRemap v1.03 by bry-ful\n"
                "  for After Effects Timesheet"
                );
}
//*********************************************************************************************
void TSSheet::openHelp()
{
    QString p = QApplication::applicationDirPath();
#if defined(Q_OS_MAC)

    QString appExt = ".app";
    int idx=p.indexOf(appExt);
    if ( idx>=0) {
        p = p.mid(0,idx + appExt.size());
        idx = p.lastIndexOf("/");
        if (idx>0){
            p = p.mid(0,idx);
        }
    }
#endif
    p += "/cellRemapHelp/index.html";
    QFile f(p);
    if (!f.exists()) {
        QMessageBox::about(this,tr("help"),tr("not found help!"));
        return;
    }

    QProcess ps;
#if defined(Q_OS_WIN)
    p.replace("/","\\");
    QString d ="EXPLORER ";
    d += " \"" + p + "\"";
    ps.execute( d );
#elif defined(Q_OS_MAC)

    QString cmd = "open ";
    cmd += "\"" + p +"\"";
    ps.execute(cmd);
#endif
}

//*********************************************************************************************
