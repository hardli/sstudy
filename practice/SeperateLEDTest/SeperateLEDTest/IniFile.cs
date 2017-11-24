using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace SeperateLEDTest
{
    /// <summary>
    /// 存储本地INI文件的类。
    /// </summary>
    public class IniFile
    {
        private static string FileName = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Config.ini";
        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T ReadConfig<T>(string section, string key, string ivalue)
        {
            if (File.Exists(FileName))
            {
                T value=IniFileBase.GetKeyValue<T>(FileName,section,key,ivalue);
                return value;
            }
            else
            {
                return (T)Convert.ChangeType(ivalue, typeof(T));
            }
        }

        /// <summary>
        /// 写配置文件
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void WriteConfig(string section, string key, string value)
        {
            //如果文件不存在，则创建
            if (!File.Exists(FileName))
            {
                using (FileStream myFs = new FileStream(FileName, FileMode.Create)) { }
            }
            IniFileBase.SetKeyValue(FileName, section, key, value);
        }

    }
}
