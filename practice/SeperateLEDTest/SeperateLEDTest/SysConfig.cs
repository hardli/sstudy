using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace SeperateLEDTest
{
    public class SysConfig
    {
        //LED参数设置
        public static string MsgWay = "串口"; //LED通信方式
        public static string btl = ""; //波特率
        public static string comNum = ""; //串口号
        public static string netIp = "";  //IP地址
        public static string netPort = ""; //网络端口号
        public static string ledFirstPart = "";
        public static string ledLastPart = "";

        //运行模式
        /// <summary>
        /// 0-实测；1-根据注册值生成；
        /// </summary>
        public static int nRunMode = 0;


        public SysConfig()
        {

            IniReadConfig();
        }

        /// <summary>
        /// true-读取成功；false-加密狗未授权读取失败；
        /// </summary>
        /// <returns></returns>
        public static bool IniReadConfig()
        {
            //LED参数相关设置
            ReadLEDParam();

            return true;
        }

        public static void ReadLEDParam()
        {
            //[LED]
            MsgWay = IniFile.ReadConfig<string>("LED", "SelectWay", "串口");
            comNum = IniFile.ReadConfig<string>("LED", "comNum", "COM1");
            btl = IniFile.ReadConfig<string>("LED", "btl", "2400");
            netIp = IniFile.ReadConfig<string>("LED", "netIp", "192.168.2.108");
            netPort = IniFile.ReadConfig<string>("LED", "netPort", "8885");
            ledFirstPart = IniFile.ReadConfig<string>("LED", "ledFirstPart", "0A 0A 0A 0A 0A 0A 0A 0A");
            ledLastPart = IniFile.ReadConfig<string>("LED", "ledLastPart", "FF FF FF FF FF FF");
        }


        public static void IniSaveConfig()
        {
            //[LED]
            SaveLEDParam();

        }

        /// <summary>
        /// 保存LED参数设置
        /// </summary>
        public static void SaveLEDParam()
        {
            //[Server]
            IniFile.WriteConfig("LED", "SelectWay", MsgWay);
            if (MsgWay.Equals("串口"))
            {
                IniFile.WriteConfig("LED", "comNum", comNum);
                IniFile.WriteConfig("LED", "btl", btl);
                IniFile.WriteConfig("LED", "netIp", "");
                IniFile.WriteConfig("LED", "netPort", "");
            }
            else if (MsgWay.Equals("网口"))
            {
                IniFile.WriteConfig("LED", "comNum", "");
                IniFile.WriteConfig("LED", "btl", "");
                IniFile.WriteConfig("LED", "netIp", netIp);
                IniFile.WriteConfig("LED", "netPort", netPort);
            }
            IniFile.WriteConfig("LED", "ledFirstPart", ledFirstPart);
            IniFile.WriteConfig("LED", "ledLastPart", ledLastPart);
        }
      
    }

}
