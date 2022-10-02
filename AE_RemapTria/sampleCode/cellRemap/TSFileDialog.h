#ifndef TSFILEDIALOG_H
#define TSFILEDIALOG_H

#include <QDialog>
#include <QRect>
#include <QStringList>
#include <QFile>
#include <QFileInfo>
#include <QFileInfoList>
#include <QDir>
#include <QModelIndex>
#include <QStandardPaths>
#include <QList>
#include <QLabel>
#include <QListWidget>
#include <QComboBox>
#include <QPushButton>
#include <QLineEdit>
#include <QHBoxLayout>
#include <QVBoxLayout>


#include "FsU.h"



class TSFileDialog : public QDialog
{
    Q_OBJECT

public:
    explicit TSFileDialog(QWidget *parent = 0);
    ~TSFileDialog();

    QLabel      *lbCaption;
    QListWidget *lstDir;
    QComboBox   *cmbParent;
    QPushButton *btnParent;
    QLineEdit   *edFileName;
    QListWidget *listFiles;
    QComboBox   *cmbFilter;

    QPushButton *btnCancel;
    QPushButton *btnOK;




    QString fileName();
    QString caption();
    void setCaption(QString s);
    QString dir();
    void setDir(QString s);
    bool openDialog();
    bool saveDialog();
    void createDrive();
    void setDirItem(QString p,QString cap);
    void setFilter(QString s);
    QString Filter();


    int filerIndex();
    bool matchFile(QFileInfo fi);
    bool saveMode();
    bool loadMode();
signals:
    void setTargetFileName(QString s);

public slots:
    void setFileIndex(QModelIndex mi);
    void setParentIndex(int idx);
    void setDirIndex(QModelIndex mi);

    void setFileName(QString s);
    void chEdFileName(QString s);
    void goParent();
    void setFilterIndex(int idx);
    void setLoadMode(bool b = true);
    void setSaveMode(bool b = true);
private:

    QString m_CurrentPath;
    QList<QDir> m_ParentList;
    QFileInfoList m_files;
    QStringList m_Drives;
    QString m_ext;
    void setTargetFileName(int idx);
    void getParent(QString s);

    QList< QList<QString> > m_filter;
    int m_FilerIndex;

    void listup();
protected:
};

#endif // TSFILEDIALOG_H
