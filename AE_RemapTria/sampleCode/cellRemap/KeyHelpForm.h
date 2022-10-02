#ifndef KEYHELPFORM_H
#define KEYHELPFORM_H

#include <QWidget>
#include <QTextBrowser>

class KeyHelpForm : public QTextBrowser

{
    Q_OBJECT

public:
    explicit KeyHelpForm(QWidget *parent = 0);
    ~KeyHelpForm();

private:
};

#endif // KEYHELPFORM_H
