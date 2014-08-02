import os
import sys

def readfile(filepath):
    if not os.path.isdir(filepath):
        print '%s is not directory' % filepath
        return
    filearray = []
    for parent, dirnames, filenames in os.walk(filepath):
        #print dirnames
        for file in filenames:
            filearray.append(file)
    return filearray

if __name__ == '__main__':
    #get file list
    nowpath=os.getcwd()
    excelpath = os.path.join(nowpath,"excel")
    if not os.path.isdir(excelpath):
        print '%s is not directory' % excelpath
        sys.exit(1)
    pypath = os.path.join(nowpath,"py\\excel2conf.py")
    filelist = readfile(excelpath)
    for path in filelist:
        allpath = os.path.join(excelpath,path)
        syspython=str(pypath) + ' ' + str(allpath)
        os.system( "python %s" % syspython )
    print "All is ok!"