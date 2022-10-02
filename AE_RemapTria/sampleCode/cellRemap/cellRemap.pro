#-------------------------------------------------
#
# Project created by QtCreator 2013-04-13T11:18:51
#
#-------------------------------------------------

QT       += core gui

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

TARGET = cellRemap
TEMPLATE = app

TRANSLATIONS = cellRemap_jp.ts

mac{
	QMAKE_CXXFLAGS += -mmacosx-version-min=10.6
	QMAKE_INFO_PLIST = cellRemapInfo.plist
	ICON = res/cellRemap.icns
}
win32{
	RC_FILE = "res/icon.rc"
}

SOURCES += main.cpp\
    TSData.cpp \
    TSData_io.cpp \
    TSData_sts.cpp \
    TSData_ard.cpp \
    TSGridArea.cpp \
    TSGrid.cpp \
    TSGrid_paint.cpp \
    TSGrid_mouse.cpp \
    TSGrid_key.cpp \
    TSGrid_io.cpp \
    TSGrid_dialog.cpp \
    TSGrid_action.cpp \
    AutoInputdialog.cpp \
    FsU.cpp \
    TSData_tsh.cpp \
    TSData_xps.cpp \
    TSData_retas.cpp \
    SheetDialog.cpp \
    CellInsertDialog.cpp \
    CellRenameDialog.cpp \
    FrameDialog.cpp \
    PrefDialog.cpp \
    ColorBox.cpp \
    TSFileDialog.cpp \
    TSSheet.cpp \
    ExecMessage.cpp \
    TSSelection.cpp \
    TSPref.cpp \
    KeyHelpForm.cpp \
    TSAEScript.cpp \
    NavBar.cpp \
    CalcDialog.cpp

HEADERS  += \
    AutoInputdialog.h \
    FsU.h \
    SheetDialog.h \
    CellInsertDialog.h \
    CellRenameDialog.h \
    FrameDialog.h \
    PrefDialog.h \
    ColorBox.h \
    TSFileDialog.h \
    TSSheet.h \
    ExecMessage.h \
    TSSelection.h \
    TSPref.h \
    KeyHelpForm.h \
    TSAEScript.h \
    NavBar.h \
    TSData.h \
    TSDataDef.h \
    TSGrid.h \
    TSGridArea.h \
    CalcDialog.h


RESOURCES += \
	res.qrc

