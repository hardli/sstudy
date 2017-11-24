using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace SeperateLEDTest
{
    class IniFileBase
    {
        
         /// <summary>
         /// 写入INI文件
         /// </summary>
         /// <param name="section">节点名称[如[TypeName]]</param>
         /// <param name="key">键</param>
         /// <param name="val">值</param>
         /// <param name="filepath">文件路径</param>
         /// <returns></returns>
         [DllImport("kernel32")]
         private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);

         /// <summary>
         /// 读取INI文件
         /// </summary>
         /// <param name="section">节点名称</param>
         /// <param name="key">键</param>
         /// <param name="def">值</param>
         /// <param name="retval">stringbulider对象</param>
         /// <param name="size">字节大小</param>
         /// <param name="filePath">文件路径</param>
         /// <returns></returns>
         [DllImport("kernel32")]
         private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

         /// <summary>
         /// 写入
         /// </summary>
         /// <param name="section"></param>
         /// <param name="key"></param>
         /// <param name="iValue"></param>
         public static void SetKeyValue(string sPath, string section, string key, string iValue)
         {
             //如果文件不存在，则创建
             if (!File.Exists(sPath))
             {
                 using (FileStream myFs = new FileStream(sPath, FileMode.Create)) { }
             }
             WritePrivateProfileString(section, key, iValue, sPath);
         }

         /// <summary>
         /// 读取配置文件
         /// </summary>
         /// <typeparam name="T"></typeparam>
         /// <param name="section"></param>
         /// <param name="key"></param>
         /// <returns></returns>
         public static T GetKeyValue<T>(string sPath, string section, string key, string ivalue)
         {
             string value = string.Empty;
             try
             {
                 if (File.Exists(sPath))
                 {
                     StringBuilder temp = new StringBuilder(1024);
                     GetPrivateProfileString(section, key, ivalue, temp, 1024, sPath);
                     value = temp.ToString();

                     if (String.IsNullOrWhiteSpace(value))
                         return (T)Convert.ChangeType(ivalue, typeof(T));

                     if (typeof(T).IsEnum)
                         return (T)Enum.Parse(typeof(T), value, true);

                     return (T)Convert.ChangeType(value, typeof(T));
                 }
                 else
                 {
                     return (T)Convert.ChangeType(ivalue, typeof(T));
                 }
             }
             catch (Exception e)
             {
                 return (T)Convert.ChangeType(ivalue, typeof(T));
             }
             
         }


         public static void SetKeyValue1<T>(string sPath, string section, string key, T iValue)
         {
             //如果文件不存在，则创建
             if (!File.Exists(sPath))
             {
                 using (FileStream myFs = new FileStream(sPath, FileMode.Create)) { }
             }
             if (iValue == null)
             {
                 WritePrivateProfileString(section, key, string.Empty, sPath);
             }
             else
             {
                 WritePrivateProfileString(section, key, iValue.ToString(), sPath);

             }
         }
    }
}
