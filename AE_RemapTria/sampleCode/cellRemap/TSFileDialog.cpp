#include "TSFileDialog.h"
//-------------------------------------------------------------------
TSFileDialog::TSFileDialog(QWidget *parent) :
    QDialog(parent)
{
    this->setWindowFlags(Qt::Dialog|Qt::WindowStaysOnTopHint);

    QVBoxLayout *mainVl = new QVBoxLayout;
    //----
    lbCaption = new QLabel;
    mainVl->addWidget(lbCaption);
    //----
    QHBoxLayout *lrHl = new QHBoxLayout;

    lstDir = new QListWidget;
    lstDir->setSizePolicy(QSizePolicy::Fixed,QSizePolicy::Preferred);
    lstDir->resize(100,300);
    lrHl->addWidget(lstDir);

    QVBoxLayout *rVl = new QVBoxLayout;

    QHBoxLayout *parentHl = new QHBoxLayout;
    cmbParent = new QComboBox;
    parentHl->addWidget(cmbParent);
    btnParent = new QPushButton(tr("Parent"));
    btnParent->setSizePolicy(QSizePolicy::Fixed,QSizePolicy::Fixed);
    parentHl->addWidget(btnParent);
    rVl->addLayout(parentHl);

    edFileName = new QLineEdit;
    edFileName->setSizePolicy(QSizePolicy::Preferred,QSizePolicy::Fixed);
    rVl->addWidget(edFileName);
    listFiles = new QListWidget;
    rVl->addWidget(listFiles);
    cmbFilter = new QComboBox;
    cmbFilter->setSizePolicy(QSizePolicy::Preferred,QSizePolicy::Fixed);
    rVl->addWidget(cmbFilter);

    lrHl->addLayout(rVl);
    mainVl->addLayout(lrHl);

    QHBoxLayout *hl = new QHBoxLayout;
    hl->addStretch();
    btnCancel = new QPushButton(tr("Cancel"));
    btnOK = new QPushButton(tr("OK"));
    hl->addStretch();
    hl->addWidget(btnCancel);
    hl->addWidget(btnOK);
    hl->addSpacing(20);

    mainVl->addLayout(hl);

    setLayout(mainVl);


    m_CurrentPath = QDir::homePath();
    listup();
    createDrive();
    connect(listFiles,SIGNAL(doubleClicked(QModelIndex)),this,SLOT(setFileIndex(QModelIndex)));
    connect(lstDir,SIGNAL(doubleClicked(QModelIndex)),this,SLOT(setDirIndex(QModelIndex)));
    connect(cmbParent,SIGNAL(currentIndexChanged(int)),this,SLOT(setParentIndex(int)));
    connect(edFileName,SIGNAL(textChanged(QString)),this,SLOT(chEdFileName(QString)));
    connect(btnParent,SIGNAL(clicked()),this,SLOT(goParent()));

    connect(cmbFilter,SIGNAL(currentIndexChanged(int)),this,SLOT(setFilterIndex(int)));
    connect(btnCancel,SIGNAL(clicked()),this,SLOT(reject()));
    connect(btnOK,SIGNAL(clicked()),this,SLOT(accept()));
}
//-------------------------------------------------------------------
TSFileDialog::~TSFileDialog()
{
}
//-------------------------------------------------------------------
void TSFileDialog::getParent(QString s)
{


    QDir f(s);
    if (f.exists()==false) return;
    m_ParentList.clear();
    QStringList sa = f.absolutePath().split("/");
#if defined(Q_OS_MAC)
#elif defined(Q_OS_WIN)
#endif
    QString p = sa.join("/");
    f = QDir(p);
    while (f.exists())
    {
        m_ParentList.append(p);
        sa.removeLast();
        if (sa.size()<=0) break;
        if ((sa.size()==1)&&(sa[0]=="")){
            p ="/";
        }else{
            p = sa.join("/");
        }
        f =QDir(p);
    }

    cmbParent->clear();
    if (m_ParentList.size()>0){
        for (int i=0; i<m_ParentList.size();i++){
            cmbParent->addItem(m_ParentList[i].absolutePath());
        }
    }
}
//-------------------------------------------------------------------
void TSFileDialog::setParentIndex(int idx)
{

    if ((idx>=0)&&(idx<m_ParentList.size())){
        QString p = m_ParentList[idx].absolutePath();
        if (m_CurrentPath != p){
            m_CurrentPath = p;
            listup();
        }
    }
}
//-------------------------------------------------------------------
bool TSFileDialog::matchFile(QFileInfo fi)
{
    bool ret = false;
    if (m_filter.size()<=0) return true;
    if (m_FilerIndex<0) return true;
    QString e = fi.suffix().toLower();
    if (m_filter[m_FilerIndex].size()>1){
        for (int i=1; i<m_filter[m_FilerIndex].size();i++){
            QString ee =m_filter[m_FilerIndex][i];
            if ((e == ee)||(ee == "*")){
                ret = true;
                break;
            }
        }
    }
    return ret;
}

//-------------------------------------------------------------------
void TSFileDialog::listup()
{
    QDir d(m_CurrentPath);
    listFiles->clear();
    edFileName->setText("");
    btnOK->setEnabled(false);
    if(d.exists()==false) return;
    QFileInfoList fis = d.entryInfoList(QDir::AllEntries);
    m_files.clear();
    QFileInfoList dl;
    QFileInfoList fl;
    if(fis.size()>0){
        for(int i=0;i<fis.size();i++){
            if(fis[i].isDir()){
                dl.append(fis[i]);
            }else if(fis[i].isFile()){
                if (matchFile(fis[i]))
                    fl.append(fis[i]);
            }
        }
    }
    if (dl.size()>0){
        for(int i=0;i<dl.size();i++){
            if ((dl[i].fileName()!=".")&&(dl[i].fileName()!="..")){
                m_files.append(dl[i]);
            }
        }
    }
    if (fl.size()>0){
        for(int i=0;i<fl.size();i++){
            m_files.append(fl[i]);
        }
    }
    if (m_files.size()){
        for (int i=0;i<m_files.size();i++){
            QString fn = m_files[i].fileName();
            if (m_files[i].isDir()){
                fn = "<" + fn + ">";
            }
            listFiles->addItem(fn);
        }
    }
    getParent(m_CurrentPath);

}

//-------------------------------------------------------------------
void TSFileDialog::setFileIndex(QModelIndex mi)
{
    int idx = mi.row();
    if ((idx>=0)&&(idx<m_files.size())){
        if (m_files[idx].isDir()){

            if (m_CurrentPath != m_files[idx].absoluteFilePath()){
                m_CurrentPath = m_files[idx].absoluteFilePath();
                listup();
            }
        }else{
            setTargetFileName(idx);
        }
    }
}
//-------------------------------------------------------------------
void TSFileDialog::setDirIndex(QModelIndex mi)
{
    int idx = mi.row();
    if ((idx>=0)&&(idx<m_Drives.size())){
        QDir f(m_Drives[idx]);
        if (m_CurrentPath != f.absolutePath()){
            m_CurrentPath = f.absolutePath();
            listup();
        }
    }
}
//-------------------------------------------------------------------
void TSFileDialog::setTargetFileName(int idx)
{
    if ((idx>=0)&&(idx<m_files.size())){
        if (m_files[idx].isFile()){
            edFileName->setText(m_files[idx].fileName());
            btnOK->setEnabled(true);
        }
    }
}
//-------------------------------------------------------------------
void TSFileDialog::setFileName(QString s)
{
    QFile f(s);
    QFileInfo fi(f);
    QDir d = fi.dir();

    if (m_CurrentPath != d.absolutePath()){
        m_CurrentPath = d.absolutePath();
        listup();
    }
    edFileName->setText(f.fileName());
    btnOK->setEnabled(true);

}
//-------------------------------------------------------------------
QString TSFileDialog::fileName()
{
    QString m = m_CurrentPath;
    if (!m.isEmpty()) m += "/";
    QString fn = edFileName->text();
    QString e = FsU::getExt(fn);
    if (e.isEmpty()){
        if (m_ext[0]!= '.')m_ext = "." +m_ext;
        fn = fn + m_ext;
    }
    m += fn;
    return m;
}
//-------------------------------------------------------------------
QString TSFileDialog::caption()
{
    return lbCaption->text();
}

//-------------------------------------------------------------------
void TSFileDialog::setCaption(QString s)
{
    lbCaption->setText(s);
}
//-------------------------------------------------------------------
bool TSFileDialog::openDialog()
{
    this->setWindowTitle("Open File Dialog");
    edFileName->setText("");
    btnOK->setEnabled(false);
    return (this->exec() == QDialog::Accepted);
}
//-------------------------------------------------------------------
bool TSFileDialog::saveDialog()
{
    this->setWindowTitle("Save File Dialog");
    return (this->exec() == QDialog::Accepted);
}
//-------------------------------------------------------------------
QString TSFileDialog::dir()
{
    return m_CurrentPath;
}

//-------------------------------------------------------------------
void TSFileDialog::setDir(QString s)
{
    if (m_CurrentPath != s){
        m_CurrentPath = s;
        listup();
    }
}

//-------------------------------------------------------------------
void TSFileDialog::chEdFileName(QString s)
{
    btnOK->setEnabled(!s.isEmpty() );
}
//-------------------------------------------------------------------
void TSFileDialog::setDirItem(QString p,QString cap)
{
    QDir f(p);
    if(f.exists()){
        m_Drives.append(p);
        lstDir->addItem(cap);
    }
}
//-------------------------------------------------------------------
void TSFileDialog::createDrive()
{
    m_Drives.clear();
    setDirItem(QStandardPaths::writableLocation(QStandardPaths::DocumentsLocation),tr("Document"));
    setDirItem(QStandardPaths::writableLocation(QStandardPaths::HomeLocation),tr("Home"));
    setDirItem(QStandardPaths::writableLocation(QStandardPaths::DesktopLocation),tr("Desktop"));
    setDirItem("/Volumes",tr("Volumes"));

#if defined(Q_OS_WIN)
    QFileInfoList drv =	QDir::drives();
    if (drv.size()>0){
        for (int i=0;i<drv.size();i++){
            setDirItem(drv[i].absoluteFilePath(),drv[i].absoluteFilePath());
        }
    }
#elif defined(Q_OS_MAC)
    QDir f("/Volumes");
    QFileInfoList drv = f.entryInfoList(QDir::AllEntries);
    if (drv.size()>0){
        for (int i=0;i<drv.size();i++){
            if ((drv[i].fileName() !=".")&&(drv[i].fileName() !=".."))
                setDirItem(drv[i].absoluteFilePath(),drv[i].fileName());
        }
    }

#endif
}

//-------------------------------------------------------------------
void TSFileDialog::goParent()
{
    QStringList sa = m_CurrentPath.split("/");
    sa.removeLast();
    QString p ="";
    if ( sa.size()<=0){
        return;
    }else if ((sa.size()==1)&&(sa[0]=="")){
        p="/";
    }else{
        p = sa.join("/");
    }
    m_CurrentPath = p;
    listup();
}

//-------------------------------------------------------------------
void TSFileDialog::setFilter(QString s)
{
    m_filter.clear();
    m_FilerIndex = -1;
    cmbFilter->clear();
    //All C++ files (*.cpp *.cc *.C *.cxx *.c++)
    QStringList sa = s.split(";;");
    if (sa.size()<=0) return;

    for (int i=0;i<sa.size();i++){
        int idx = sa[i].indexOf("(");
        if (idx<0) continue;
        QString cap = sa[i].left(idx).trimmed();
        QString exts = sa[i].mid(idx+1);
        if(exts.size()<=0) continue;
        if (exts[exts.size()-1]==')') exts = exts.left(exts.size()-1);
        exts = exts.trimmed();
        if(exts.size()<=0) continue;
        QStringList ex = exts.split(" ");
        QList <QString> dd;
        dd.append(sa[i]);
        for (int j=0;j<ex.size();j++){
            dd.append(ex[j].replace("*.","").toLower());
        }
        m_filter.append(dd);
    }
    if (m_filter.size()>0){
        for (int i=0;i<m_filter.size();i++){
            cmbFilter->addItem(m_filter[i][0]);
        }
        cmbFilter->setCurrentIndex(0);
        m_FilerIndex = 0;
        m_ext = m_filter[0][1];
    }

}
//-------------------------------------------------------------------
QString TSFileDialog::Filter()
{
    QString ret = "";
    if (m_filter.size()>0){
        for (int i=0;i<m_filter.size();i++){
            if (!ret.isEmpty()) ret += ";;";
            ret += m_filter[i][0];
        }
    }
    return ret;
}
//-------------------------------------------------------------------
int TSFileDialog::filerIndex()
{
    return m_FilerIndex;
}
//-------------------------------------------------------------------
void TSFileDialog::setFilterIndex(int idx)
{
    if((idx<0)||(idx>=m_filter.size())) return;
    if (m_FilerIndex != idx){
        m_FilerIndex = idx;
        if (cmbFilter->currentIndex() != m_FilerIndex){
            cmbFilter->setCurrentIndex(m_FilerIndex);
        }
        m_ext = m_filter[m_FilerIndex][1];
        listup();
    }
}
//-------------------------------------------------------------------
void TSFileDialog::setLoadMode(bool b)
{
    bool bb = !b;
    if (bb != edFileName->isEnabled()){
        edFileName->setEnabled(bb);
    }
}
//-------------------------------------------------------------------
void TSFileDialog::setSaveMode(bool b)
{
    if (b != edFileName->isEnabled()){
        edFileName->setEnabled(b);
    }
}
//-------------------------------------------------------------------
bool TSFileDialog::saveMode()
{
    return edFileName->isEnabled();
}
//-------------------------------------------------------------------
bool TSFileDialog::loadMode()
{
    return ! edFileName->isEnabled();

}
