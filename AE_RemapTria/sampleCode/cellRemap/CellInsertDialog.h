/*
 *
 */
#ifndef CELLINSERTDIALOG_H
#define CELLINSERTDIALOG_H

#include <QDialog>
#include <QGridLayout>
#include <QHBoxLayout>
#include <QVBoxLayout>
#include <QSpinBox>
#include <QLabel>
#include <QLineEdit>
#include <QPushButton>


class CellInsertDialog : public QDialog
{
    Q_OBJECT

public:
    explicit CellInsertDialog(QWidget *parent = 0);
    ~CellInsertDialog();

    QLineEdit   *edNew;
    QPushButton *btnOK;
    QPushButton *btnCancel;

    void setCellName(QString s);
    QString cellName();
private:
public slots:
    void checkCellName(QString s);

};

#endif // CELLINSERTDIALOG_H
