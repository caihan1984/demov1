#!/usr/bin/python
# -*- coding: utf-8 -*-
# 这段代码主要的功能是把excel表格转换成utf-8格式的json文件
# lastdate:2011-8-15 14:21 version 1.1 

import os
import sys
import codecs
import xlrd #http://pypi.python.org/pypi/xlrd

def FloatToString (aFloat):
    if type(aFloat) != float:
        return ""
    strTemp = str(aFloat)
    strList = strTemp.split(".")
    if len(strList) == 1 :
        return strTemp
    else:
        if strList[1] == "0" :
            return strList[0]
        else:
            return strTemp

#查找第1个非要求的字符串的下标
def findFirstNot(str, begin, substr):
    for i in range(begin, len(str)):
        if substr.find(str[i]) == -1:
            return i
    return -1

#解析filter字符串，返回变量数组
def parseFilterKey(filter):
    ret = []
    begin = 0
    while True:
        index = filter.find("$", begin)
        if index >= 0:
            index += 1
            end = findFirstNot(filter, index, "1234567890abcdefghijklmnopqrstuvwxyz_ABCDEFGHIJKLMNOPQRSTUVWXYZ")
            key = filter[index:end]
            ret.append(key)
            begin = end
        else:
            return ret

#读入字段映射表
def readMap(table):
    mapTable = {}
    nrow = table.nrows
    if table.ncols == 0:
        return mapTable

    for r in range(nrow):
        k = table.cell_value(r, 0)
        if table.ncols < 2:
            v = k
        else:
            v = table.cell_value(r, 1)
            if (len(v) == 0):
                v = k
        mapTable[k] = v
    return mapTable

def table2jsn(table, jsonfilename, mapTable, mapParam, destTableName):
# {"name":"数组名", "var":"变量名", "index1":True, "filter":"$Online>0 and $Name=='加速锦囊(1小时)'"}
    nrows = table.nrows
    ncols = table.ncols
    hasMap = (len(mapTable) > 0)
    dir = os.path.dirname(jsonfilename)
    if dir and not os.path.exists(dir):
        os.makedirs(dir)        
    #写入json文件夹内
    nowpath=os.getcwd()
    filedir = os.path.join(nowpath,"json")
    filejson = os.path.join(filedir,jsonfilename)
    f = codecs.open(filejson,"w","utf-8")
    strTmp = u""
    
    #解析filter字符串
    filterKey = []
    filterString = ""
    if "filter" in mapParam and len(mapParam["filter"]) > 0:
        filterString = mapParam["filter"].decode("utf8")
        filterKey = parseFilterKey(filterString)

    #var xxx = 
    if ("var" in mapParam) and (len(mapParam["var"]) > 0):
        strTmp += u"var " + mapParam["var"] + u" = "

    #name:[
    if "name" in mapParam:
        if (len(mapParam["name"]) > 0):
            strTmp += u"{\n\t\"" + mapParam["name"] + "\":"
    else:
        strTmp += u"{\n\t\"list\":"

    if len(strTmp) == 0:   #此时加个\t使前后对齐
        strTmp += u"\t[\n"
    else:
        strTmp += u"[\n"

    if ("index1" in mapParam) and (mapParam["index1"]):
        strTmp += u"\t\t{},\n"
    f.write(strTmp)
    rs = 0
    for r in range(1, nrows):
        strTmp = u"\t\t{ "
        i = 0
        strFilter = filterString
        skip_row = False
        get_this = not (len(filterKey) > 0)
        for c in range(ncols):
            title = table.cell_value(0,c)
            isString = (title.rfind(u"\"") >= 0)
            title = title.replace(u"\n", u"").replace(u"\"", u"")

            if hasMap:
                if not title in mapTable:
                    continue
                else:
                    title = mapTable[title]

            strCellValue = u""
            CellObj = table.cell_value(r,c)
            if type(CellObj) == unicode:
                strCellValue = CellObj
            elif type(CellObj) == float:
                strCellValue = FloatToString(CellObj)
            else:
                strCellValue = str(CellObj)

            if isString:
                strCellValue = strCellValue.replace(u"\n", u"").replace(u"\"", u"")

            if not get_this and title in filterKey:
                if isString:
                    strFilter = strFilter.replace(u"$" + title, u"\"" + strCellValue + u"\"")
                else:
                    strFilter = strFilter.replace(u"$" + title, strCellValue)

                if strFilter.find("$") == -1:
                    if not eval(strFilter):  #被过滤了
                        skip_row = True
                        break
                    else:
                        get_this = True     #确定了这行要

            if i > 0:
                delm = u", "
            else:
                delm = u""

            if isString:
                strTmp += delm + u"\""  + title + u"\":\""+ strCellValue + u"\""
            else:
                strTmp += delm + u"\""  + title + u"\":"+ strCellValue
            i += 1
        
        if skip_row:    #被过滤了
            continue
        
        strTmp += u" }"
        if rs > 0:  #不是第1行
            f.write(u",\n")
        f.write(strTmp)
        rs += 1

    strTmp = u"\n\t]"
    if "name" in mapParam:
        if (len(mapParam["name"]) > 0):
            strTmp += u"\n}"
    else:
        strTmp += u"\n}"

    strTmp += u"\n"
    f.write(strTmp)
    f.close()
    print "Create ",jsonfilename," OK"
    return

def table2sql(table, jsonfilename, mapTable, mapParam, destTableName):
# {"name":"数组名", "delete":False, "commit":True, "filter":"$Online>0 and $Name=='加速锦囊(1小时)'"}
    nrows = table.nrows
    ncols = table.ncols
    hasMap = (len(mapTable) > 0)
    dir = os.path.dirname(jsonfilename)
    if dir and not os.path.exists(dir):
        os.makedirs(dir)

    #解析filter字符串
    filterKey = []
    filterString = ""
    if "filter" in mapParam and len(mapParam["filter"]) > 0:
        filterString = mapParam["filter"].decode("utf8")
        filterKey = parseFilterKey(filterString)

    tablename = destFileName[:destFileName.rfind(".")]    #用文件名做表名
    tablename = tablename[tablename.rfind("\\")+1:]
    if ("name" in mapParam) and len(mapParam["name"]) > 0:
        tablename = mapParam["name"]

    f = codecs.open(jsonfilename,"w","utf-8")
    if not ("delete" in mapParam and not mapParam["delete"]):
        f.write(u"truncate table " + tablename + u";\n")
    f.write(u"set names 'utf8';\n")

    if not(("commit" in mapParam) and not mapParam["commit"]):
        f.write(u"set autocommit=0;\n")

    for r in range(1, nrows):
        strTmp = u"insert into " + tablename + " set "
        i = 0
        strFilter = filterString
        skip_row = False
        get_this = not (len(filterKey) > 0)
        for c in range(ncols):
            title = table.cell_value(0,c)
            isString = (title.rfind(u"\"") >= 0)
            title = title.replace(u"\n", u"").replace(u"\"", u"")

            if hasMap:
                if not title in mapTable:
                    continue
                else:
                    title = mapTable[title]

            strCellValue = u""
            CellObj = table.cell_value(r,c)
            if type(CellObj) == unicode:
                strCellValue = CellObj
            elif type(CellObj) == float:
                strCellValue = FloatToString(CellObj)
            else:
                strCellValue = str(CellObj)
            
            if isString:
                strCellValue = strCellValue.replace(u"\n", u"").replace(u"'", u"\"")

            if not get_this and title in filterKey:
                if isString:
                    strFilter = strFilter.replace(u"$" + title, u"\"" + strCellValue + u"\"")
                else:
                    strFilter = strFilter.replace(u"$" + title, strCellValue)

                if strFilter.find("$") == -1:
                    if not eval(strFilter):  #被过滤了
                        skip_row = True
                        break
                    else:
                        get_this = True     #确定了这行要

            if i > 0:
                delm = u", "
            else:
                delm = u""

            if isString:
                strTmp += delm + title + u" = '" + strCellValue + u"'"
            else:
                strTmp += delm + title + u" = " + strCellValue
            i += 1

        if skip_row:    #被过滤了
            continue

        strTmp += u";\n"
        f.write(strTmp)

    if not(("commit" in mapParam) and not mapParam["commit"]):
        f.write(u"commit;\n")
    f.write(u"\n")
    f.close()
    print "Create ",jsonfilename," OK"
    return

def table2ini(table, inifilename, mapTable, mapParam, destTableName):
# {"name":"section名", "filter":"$Online>0 and $Name=='加速锦囊(1小时)'"}
    nrows = table.nrows
    ncols = table.ncols
    hasMap = (len(mapTable) > 0)
    dir = os.path.dirname(inifilename)
    if dir and not os.path.exists(dir):
        os.makedirs(dir)
        
    #解析filter字符串
    filterKey = []
    filterString = ""
    if "filter" in mapParam and len(mapParam["filter"]) > 0:
        filterString = mapParam["filter"].decode("utf8")
        filterKey = parseFilterKey(filterString)

    section = destFileName[:destFileName.rfind(".")]    #用文件名做节名
    section = section[section.rfind("\\")+1:]
    if ("name" in mapParam) and len(mapParam["name"]) > 0:
        section = mapParam["name"]

    f = codecs.open(inifilename,"w","utf-8")
    rs = 1
    for r in range(1, nrows):
        strTmp = u"[" + section + str(rs) + u"]\n"
        strFilter = filterString
        skip_row = False
        get_this = not (len(filterKey) > 0)
        
        for c in range(ncols):
            title = table.cell_value(0,c)
            isString = (title.rfind(u"\"") >= 0)
            title = title.replace(u"\n", u"").replace(u"\"", u"")

            if hasMap:
                if not title in mapTable:
                    continue
                else:
                    title = mapTable[title]

            strCellValue = u""
            CellObj = table.cell_value(r,c)
            if type(CellObj) == unicode:
                strCellValue = CellObj
            elif type(CellObj) == float:
                strCellValue = FloatToString(CellObj)
            else:
                strCellValue = str(CellObj)

            strCellValue = strCellValue.replace(u"\n", u"")  #去掉换行
            if not get_this and title in filterKey:
                if isString:
                    strFilter = strFilter.replace(u"$" + title, u"\"" + strCellValue.replace(u"\"", u"") + u"\"")
                else:
                    strFilter = strFilter.replace(u"$" + title, strCellValue)

                if strFilter.find("$") == -1:
                    if not eval(strFilter):  #被过滤了
                        skip_row = True
                        break
                    else:
                        get_this = True     #确定了这行要

            strTmp += title + u" = "+ strCellValue + "\n"

        if skip_row:    #被过滤了
            continue

        rs += 1
        strTmp += u"\n"
        f.write(strTmp)

    strTmp = u"[" + section + u"]\n"
    strTmp += u"Count = " + str(rs - 1) + u"\n"
    f.write(strTmp)

    f.close()
    print "Create ",inifilename," OK"
    return

def table2csvSaveAs(table, csvfilename, mapTable, mapParam, destTableName):
    import win32com.client  #pywin32 http://sourceforge.net/projects/pywin32/files/
    import tempfile
    import shutil

    dir = os.path.dirname(csvfilename)
    if dir and not os.path.exists(dir):
        os.makedirs(dir)

    #创建Excel对象
    xlApp = win32com.client.Dispatch('Excel.Application')
    xlApp.DisplayAlerts = False
    mso = xlApp.AutomationSecurity
    xlApp.AutomationSecurity = 3 #msoAutomationSecurityForceDisable=3

    fsrc = os.path.abspath(sys.argv[1])
    fdst = os.path.abspath(csvfilename)
    try:
        xlWbs = xlApp.Workbooks.Open(fsrc)
    
        #依次Active每个工作表
        for i in range(1, xlWbs.Worksheets.Count + 1):
            if destTableName != xlWbs.Worksheets(i).Name:
                continue

            xlWbs.Worksheets(i).Activate()
            xlWbs.SaveAs(fdst, FileFormat=6) #xlUnicodeText=42   xlCSV=6
            xlWbs.Close()
            
            file = codecs.open(fdst, "r", "gbk")
            data = file.read()
            file.close()
            file = codecs.open(fdst, "w", "utf-8")
            file.write(data)
            file.close()
            break
    finally:
        xlApp.AutomationSecurity = mso
        xlApp.Quit()
    
def table2csv(table, csvfilename, mapTable, mapParam, destTableName):
# {"title":False, "filter":"$Online>0 and $Name=='加速锦囊(1小时)'"}
# {"flag": "saveas"} 表示直接用excel的saveas功能，保存为csv格式，不用于上面的
    if "flag" in mapParam and mapParam["flag"] == "saveas":
        table2csvSaveAs(table, csvfilename, mapTable, mapParam, destTableName)
        return

    nrows = table.nrows
    ncols = table.ncols
    hasMap = (len(mapTable) > 0)
    dir = os.path.dirname(csvfilename)
    if dir and not os.path.exists(dir):
        os.makedirs(dir)
    f = codecs.open(csvfilename,"w","utf-8")
        
    #解析filter字符串
    filterKey = []
    filterString = ""
    if "filter" in mapParam and len(mapParam["filter"]) > 0:
        filterString = mapParam["filter"].decode("utf8")
        filterKey = parseFilterKey(filterString)

    for r in range(nrows):
        i = 0
        strFilter = filterString
        skip_row = False
        get_this = not (len(filterKey) > 0)
        strTmp = u""

        if r == 0 and ("title" in mapParam) and not (mapParam["title"]):
            continue

        for c in range(ncols):
            title = table.cell_value(0,c)
            isString = (title.rfind(u"\"") >= 0)
            title = title.replace(u"\n", u"").replace(u"\"", u"").replace(u",", u"")

            if hasMap:
                if not title in mapTable:
                    continue
                else:
                    title = mapTable[title]

            if i > 0:
                delm = u", "
            else:
                delm = u""

            if r == 0:  #第一行不同
                strTmp += delm + title
            else:
                strCellValue = u""
                CellObj = table.cell_value(r,c)
                if type(CellObj) == unicode:
                    strCellValue = CellObj.replace(u"\n", u"").replace(u",", u"")
                elif type(CellObj) == float:
                    strCellValue = FloatToString(CellObj)
                else:
                    strCellValue = str(CellObj).replace(u"\n", u"").replace(u",", u"")

                if not get_this and title in filterKey:
                    if isString:
                        strFilter = strFilter.replace(u"$" + title, u"\"" + strCellValue.replace(u"\"", u"") + u"\"")
                    else:
                        strFilter = strFilter.replace(u"$" + title, strCellValue)

                    if strFilter.find("$") == -1:
                        if not eval(strFilter):  #被过滤了
                            skip_row = True
                            break
                        else:
                            get_this = True     #确定了这行要

                strTmp += delm + strCellValue
            i += 1

        if skip_row:    #被过滤了
            continue

        strTmp += u"\n"
        f.write(strTmp)
    f.close()
    print "Create ",csvfilename," OK"
    return

def table2key(table, jsonfilename, mapTable, mapParam, destTableName):
# {"var":"变量名", "filter":"$Online>0 and $Name=='加速锦囊(1小时)'"}
    nrows = table.nrows
    ncols = table.ncols
    hasMap = (len(mapTable) > 0)
    dir = os.path.dirname(jsonfilename)
    if dir and not os.path.exists(dir):
        os.makedirs(dir)    
    f = codecs.open(jsonfilename,"w","utf-8")
    strTmp = u""

    #解析filter字符串
    filterKey = []
    filterString = ""
    if "filter" in mapParam and len(mapParam["filter"]) > 0:
        filterString = mapParam["filter"].decode("utf8")
        filterKey = parseFilterKey(filterString)

    #var xxx = 
    if ("var" in mapParam) and (len(mapParam["var"]) > 0):
        strTmp += u"var " + mapParam["var"] + u" = "

    strTmp += u"{\n"
    f.write(strTmp)
    rs = 0
    for r in range(1, nrows): #跳过第1行标题
        strTmp = u"\t"
        i = 0
        strFilter = filterString
        skip_row = False
        get_this = not (len(filterKey) > 0)
        for c in range(2):  #只处理最前面2列
            title = table.cell_value(0,c)
            isString = (title.rfind(u"\"") >= 0)
            title = title.replace(u"\n", u"").replace(u"\"", u"")

            if hasMap:
                if not title in mapTable:
                    continue
                else:
                    title = mapTable[title]

            strCellValue = u""
            CellObj = table.cell_value(r,c)
            if type(CellObj) == unicode:
                strCellValue = CellObj
            elif type(CellObj) == float:
                strCellValue = FloatToString(CellObj)
            else:
                strCellValue = str(CellObj)

            if isString:
                strCellValue = strCellValue.replace(u"\n", u"").replace(u"\"", u"")

            if not get_this and title in filterKey:
                if isString:
                    strFilter = strFilter.replace(u"$" + title, u"\"" + strCellValue + u"\"")
                else:
                    strFilter = strFilter.replace(u"$" + title, strCellValue)

                if strFilter.find("$") == -1:
                    if not eval(strFilter):  #被过滤了
                        skip_row = True
                        break
                    else:
                        get_this = True     #确定了这行要

            if i > 0:
                delm = u":"
            else:
                delm = u""

            if isString:
                strTmp += delm + u"\""+ strCellValue + u"\""
            else:
                strTmp += delm + strCellValue
            i += 1

        if skip_row:    #被过滤了
            continue
        
        if rs > 0:  #不是第1行
            f.write(u",\n")
        f.write(strTmp)
        rs += 1

    strTmp = u"\n}\n"
    f.write(strTmp)
    f.close()
    print "Create ",jsonfilename," OK"
    return

if __name__ == '__main__':
    if len(sys.argv) < 2:
        print 'Usage: %s <excel_file>' % sys.argv[0]
        sys.exit(1)

    print "handle file: %s" % sys.argv[1]

    excelFileName = sys.argv[1]
    data = xlrd.open_workbook(excelFileName)
    table = data.sheet_by_name(u"tablelist")
    rs = table.nrows
    for r in range(rs-1):
        destTableName = table.cell_value(r+1,0)
        destFileName = table.cell_value(r+1,2)

        mapTableName = u""
        if (table.ncols >= 4):
            mapTableName = table.cell_value(r+1,3)

        mapParam = {}
        if (table.ncols >= 5):
            s = table.cell_value(r+1,4)
            if len(s) > 0:
                mapParam = eval(s)

        print destTableName, "==>", destFileName

        destTable = data.sheet_by_name(destTableName)
        mapTable = {}
        if (len(mapTableName) > 0):
            t = data.sheet_by_name(mapTableName)
            mapTable = readMap(t)

        suffix = destFileName[destFileName.rfind("."):].lower()
        if suffix == ".csv":
            table2csv(destTable, destFileName, mapTable, mapParam, destTableName)
        elif suffix == ".jsn" or suffix == ".js":
            table2jsn(destTable, destFileName, mapTable, mapParam, destTableName)
        elif suffix == ".conf":
            table2ini(destTable, destFileName, mapTable, mapParam, destTableName)
        elif suffix == ".sql":
            table2sql(destTable, destFileName, mapTable, mapParam, destTableName)
        elif suffix == ".key":
            table2key(destTable, destFileName, mapTable, mapParam, destTableName)
        else:
            print "only support jsn, ini, csv format"
            exit(1)

    print "All OK"
