::根据文件名自动执行

@echo off

set file=%~n0%.xls
if exist %file% python ../../tools/excel2conf.py %file%

set file=%~n0%.xlsx
if exist %file% python ../../tools/excel2conf.py %file%

pause
