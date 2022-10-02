#ifndef CELLRENAMEDIALOG_H
#define CELLRENAMEDIALOG_H

#include <QDialog>
#include <QGridLayout>
#include <QHBoxLayout>
#include <QVBoxLayout>
#include <QSpinBox>
#include <QLabel>
#include <QLineEdit>
#include <QPushButton>


class CellRenameDialog : public QDialog
{
    Q_OBJECT

public:
    explicit CellRenameDialog(QWidget *parent = 0);
    ~CellRenameDialog();

    QLineEdit   *edOrg;
    QLineEdit   *edNew;
    QPushButton *btnOK;
    QPushButton *btnCancel;

    void setCellName(QString s);
    QString cellName();
private:
public slots:
    void checkCellName(QString s);
};

#endif // CELLRENAMEDIALOG_H
