using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace select
{
    class Log
    {
        /************************************************************************
        * Function: Create Directory if not exists
        * IN:  string dirPath
        * OUT: void
        * Author: DQS
        * Date: 2012-08-22
        ************************************************************************/
        public static void CreateDir(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                try
                {
                    Directory.CreateDirectory(dirPath);
                }
                catch { }
            }
        }

        /************************************************************************
        * Function: Write Log to the Module log file, one day a file
        *      the file path like: log\2012-08-22\HSData.2012-08-22.log
        * IN:  string ModuleName, which Module Error Occur
        *      string ErrorType, Error Type: Error, Warning or Message
        *      string Msg, the message
        * OUT: void
        * Author: DQS
        * Date: 2012-08-22
        ************************************************************************/
        public static void WriteLog(string Msg)
        {
            // string sDirPath = ".\\log\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
            string sDirPath = ".\\log\\";
            CreateDir(sDirPath);
            string sFilePath = sDirPath + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            try
            {
                using (StreamWriter sw = File.AppendText(sFilePath))
                {
                    //sw.WriteLine(Msg);
                    sw.Write(DateTime.Now.ToString("HH-mm-ss") + ":" + Msg + "\r\n");
                    sw.Flush();
                    sw.Dispose();
                }
            }
            catch { }
        }


        public static void DeleteHistoryLog()
        {
            string sDirPath = ".\\log\\";
            string filename = "";
            DateTime CurDt = DateTime.Now.Date;
            DirectoryInfo TheFolder = new DirectoryInfo(sDirPath);
            FileInfo[] fileInfo = TheFolder.GetFiles("*.log", SearchOption.AllDirectories); // 获得当前的文件夹内的所有文件数组
            foreach (FileInfo NextFile in fileInfo)
            {
                filename = System.IO.Path.GetFileNameWithoutExtension(NextFile.FullName);
                if (DateDiff(CurDt, filename) > 5)
                {
                    NextFile.Delete();
                }
            }
        }

        private static int DateDiff(DateTime Curdt, string sdt)
        {
            int nDays = 0;
            try
            {

                DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
                dtFormat.ShortDatePattern = "yyyy-MM-dd";
                DateTime dt = Convert.ToDateTime(sdt, dtFormat);

                TimeSpan ts = Curdt - dt;

                nDays = ts.Days;//整数天数，1天12小时或者1天20小时结果都是1 
            }
            catch (Exception e)
            {

            }
            return nDays;
        }
        public void WriteLogUnStatic(string Msg)
        {
            // string sDirPath = ".\\log\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
            string sDirPath = ".\\log\\";
            CreateDir(sDirPath);
            string sFilePath = sDirPath + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            try
            {
                using (StreamWriter sw = File.AppendText(sFilePath))
                {
                    //sw.WriteLine(Msg);
                    sw.Write(Msg);
                    sw.Flush();
                    sw.Dispose();
                }
            }
            catch { }
        }

        public static void WriteLog2(string ModuleName, string ErrorType, string Msg)
        {
            // string sDirPath = ".\\log\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
            string sDirPath = ".\\log\\";
            CreateDir(sDirPath);
            string sFilePath = sDirPath + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            try
            {
                using (StreamWriter sw = File.AppendText(sFilePath))
                {
                    sw.WriteLine(DateTime.Now.ToString() + "->" + ModuleName + "\t" + ErrorType + ":  " + Msg);
                    sw.Flush();
                    sw.Dispose();
                }
            }
            catch { }
        }

        /************************************************************************
        * Function: Delete The Directory and The files in the folder
        * IN:  string dir, the directory to be deleted
        * OUT: void
        * Author: DQS
        * Date: 2012-08-22
        * Commit: Directory.Delete("log\\2012-08-22\\", true); can also delete
        *      the folder
        ************************************************************************/
        public static void DeleteDir(string dir)
        {
            try
            {
                if (Directory.Exists(dir))
                {
                    foreach (string d in Directory.GetFileSystemEntries(dir))
                    {
                        //Recursive Delete Directory
                        if (File.Exists(d))
                            File.Delete(d);
                        else
                            DeleteDir(d);
                    }
                    //Delete the Empty Directory
                    Directory.Delete(dir);
                }
            }
            catch { }
        }
    }
}
