::�����ļ����Զ�ִ��

@echo off

set equipFile=װ����.xls
set clothFile=���������.xls
set clothLevelFile=����������Ա�.xls

if exist %equipFile% python ../../tools/cloth.py %clothFile% %equipFile% %clothLevelFile%


pause
