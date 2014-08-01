::根据文件名自动执行

@echo off

set equipFile=装备表.xls
set clothFile=球服升级表.xls
set clothLevelFile=球服升级属性表.xls

if exist %equipFile% python ../../tools/cloth.py %clothFile% %equipFile% %clothLevelFile%


pause
