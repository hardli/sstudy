using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ONNONLed5KSDKD
{

    public class Led5kSDK
    {
        enum bx5k_err
        {
            ERR_NO = 0, //No Error 
            ERR_OUTOFGROUP = 1, //Command Group Error 
            ERR_NOCMD = 2, //Command Not Found 
            ERR_BUSY = 3, //The Controller is busy now 
            ERR_MEMORYVOLUME = 4, //Out of the Memory Volume 
            ERR_CHECKSUM = 5, //CRC16 Checksum Error 
            ERR_FILENOTEXIST = 6, //File Not Exist 
            ERR_FLASH = 7, //Flash Access Error 
            ERR_FILE_DOWNLOAD = 8, //File Download Error 
            ERR_FILE_NAME = 9, //Filename Error 
            ERR_FILE_TYPE = 10,//File type Error 
            ERR_FILE_CRC16 = 11,//File CRC16 Error 
            ERR_FONT_NOT_EXIST = 12,//Font Library Not Exist 
            ERR_FIRMWARE_TYPE = 13,//Firmware Type Error (Check the controller type) 
            ERR_DATE_TIME_FORMAT = 14,//Date Time format error 
            ERR_FILE_EXIST = 15,//File Exist for File overwrite 
            ERR_FILE_BLOCK_NUM = 16,//File block number error 
            ERR_COMMUNICATE = 100,//通信失败
            ERR_PROTOCOL = 101,//协议数据不正确
            ERR_TIMEOUT = 102,//通信超时
            ERR_NETCLOSE = 103,//通信断开
            ERR_INVALID_HAND = 104,//无效句柄
            ERR_PARAMETER = 105,//参数错误
            ERR_SHOULDREPEAT = 106,//需要重复上次数据包
            ERR_FILE = 107,//无效文件
        }
        //串口停止位
        public enum serial_stopbits : byte
        {
            COM_ONESTOPBIT = 0,
            COM_ONE5STOPBITS = 1,
            COM_TWOSTOPBITS = 2,
        }
        //串口校验模式
        public enum serial_parity : byte
        {
            COM_NOPARITY = 0,
            COM_ODDPARITY = 1,
            COM_EVENPARITY = 2,
            COM_MARKPARITY = 3,
            COM_SPACEPARITY = 4,
        }
        //串口数据位
        public enum serial_databits : byte
        {
            COM_4BITS = 4,
            COM_5BITS = 5,
            COM_6BITS = 6,
            COM_7BITS = 7,
            COM_8BITS = 8,
        }
        //控制器类型
        public enum bx_5k_card_type : byte
        {
            BX_Any = 0xFE,
            BX_5K1 = 0x51,
            BX_5K2 = 0x58,
            BX_5MK2 = 0x53,
            BX_5MK1 = 0x54,
            BX_5K1Q_YY = 0x5c,
            BX_6K1 = 0x61,
            BX_6K2 = 0x62,
            BX_6K3 = 0x63,
        }
        //-------区域格式------
        // area header | data |
        //---------------------
        //节目内区域定义
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct bx_5k_area_header
        {
            public byte AreaType;
            public short AreaX;
            public short AreaY;
            public short AreaWidth;
            public short AreaHeight;
            public byte DynamicAreaLoc;
            public byte Lines_sizes;
            public byte RunMode;
            public short Timeout;
            public byte Reserved1;
            public byte Reserved2;
            public byte Reserved3;
            public byte SingleLine;
            public byte NewLine;
            public byte DisplayMode;
            public byte ExitMode;
            public byte Speed;
            public byte StayTime;
            public int DataLen;
        }

        [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.StdCall)]
        public delegate void CallBackClientClose(uint hand, int err);

        //初始化动态库
        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern void InitSdk(byte minorVer , byte majorVer);
        //释放动态库
        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern void ReleaseSdk();
        //创建广播通讯模式
        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern uint CreateBroadCast(byte[] broad_ip, uint broad_port, bx_5k_card_type card_type);
        //创建固定IP通讯模式
        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern uint CreateClient(byte[] led_ip, uint led_port, bx_5k_card_type card_type,int tmout_sec, CallBackClientClose pCloseFunc);
        //创建TCP Modbus通讯
        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern uint CreateTcpModbus(byte[] led_ip, bx_5k_card_type card_type, CallBackClientClose pCloseFunc);
        //创建串口通讯
        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern uint CreateComClient(byte com, uint baudrate, bx_5k_card_type card_type, ushort ScreenID);
        //创建串口Modbus通讯
        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern uint CreateComModbus(byte com, uint baudrate, serial_parity Parity, serial_databits DataBits,
            serial_stopbits StopBits, bx_5k_card_type card_type, ushort ScreenID);
        //销毁通讯
        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern void Destroy(uint dwHand);
        //设置通讯超时
        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern void SetTimeout(uint dwHand, uint nSec);
        //ping
        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int CON_PING(uint dwHand);
        //复位
        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int CON_Reset(uint dwHand);
        //
        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int CON_ControllerStatus(uint dwHand, byte[] pStatus, ref ushort len);
        //查询字库信息
        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int CON_CheckCurrentFont(uint dwHand, byte[] fontStatus, ref ushort len);
        //回读客户信息
        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int CON_CheckCurrentCustomer(uint dwHand, byte[] CustomerStatus, ref ushort len);
        //参数回读
        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int CON_ReadScreen(uint dwHand, byte[] ScreenStatus, ref ushort len);

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int CON_SytemClockCorrect(uint dwHand);
        //查询固件状态
        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int CON_CheckCurrentFirmware(uint dwHand, byte[] FirmwareName, byte[] FirmwareVersion, byte[] FirmwareDateTime);

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int CON_FirmwareActivate(uint dwHand, byte[] FirmwareName);

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int CON_SetScreenID(uint dwHand, ushort newScreenID);

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int CON_ReadScreenID(uint dwHand, ref ushort pScreenID);

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int SCREEN_ForceOnOff(uint dwHand, byte OnOffFlag);

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int SCREEN_TimeTurnOnOff(uint dwHand, byte[] pTimer, int nGroup);

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int SCREEN_SetBrightness(uint dwHand, byte BrightnessType, byte CurrentBrightness, byte[] BrightnessValue);

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int SCREEN_SetWaitTime(uint dwHand, byte WaitTime);

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int SCREEN_LockProgram(uint dwHand, byte LockFlag, byte StoreMode, byte[] ProgramFileName);

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int SCREEN_DelDynamicArea(uint dwHand, byte DeleteAreaId);

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int SCREEN_SendDynamicArea(uint dwHand, bx_5k_area_header header, ushort TextLen, byte[] AreaText);

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int SCREEN_SendSoundDynamicArea(uint dwHand, bx_5k_area_header header, ushort TextLen, byte[] AreaText, byte SoundMode, byte SoundPerson, byte SoundVolume, byte SoundSpeed, int sound_len, byte[] sounddata);

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int SCREEN_Test(uint dwHand, byte TestTime);

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int SCREEN_CancelTimeOnOff(uint dwHand);

        #region 设置特殊动态区动态
        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int BX5MK_SetSpecialAppDynamic(uint dwHand, ushort AreaX, ushort AreaY, ushort AreaW, ushort AreaH,
            byte DataType, byte Pagetotal, byte RunState, ushort Timeout, byte SingleLine, byte Lines_sizes, byte NewLine, ushort StayTime);

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int BX5MK_SendPageData(uint dwHand, byte PageNum, ushort PageDataLen, byte[] PageData);

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int BX5MK_SendLatticeMessage(uint dwHand, byte BlockFlag, ushort BlockAddr, byte[] BlockData, ushort BlockDataLen);

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int BX5MK_DelSpecialAppDynamic(uint dwHand);

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int BX5MK_SetIPAddress(uint dwHand, byte ConnnectMode, byte[] ip, byte[] SubnetMask, byte[] Gateway, ushort port,
            byte ServerMode, byte[] ServerIPAddress, ushort ServerPort, byte[] ServerAccessPassword, ushort HeartBeatInterval, byte[] NetID);

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int BX5MK_SetMACAddress(uint dwHand, byte[] MAC);

        //设置特殊动态区动态
        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int BX5MK_SetSpecialAppDynamic(int dwHand, ushort AreaX, ushort AreaY, ushort AreaW, ushort AreaH, byte DataType, byte Pagetotal,
            byte RunState, ushort Timeout, byte SingleLine, byte Lines_sizes, byte NewLine, ushort StayTime);

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int BX5MK_WebSearch(uint dwHand, ref ushort Status, ref ushort Error, byte[] IP,
            byte[] SubNetMask, byte[] Gate, ref ushort Port, byte[] Mac, byte[] NetID);

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int BX5MK_DelPageData(uint dwHand, byte PageLog);
        #endregion

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int OFS_Formatting(uint dwHand);

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int OFS_DeleteFile(uint dwHand, ushort FileNumber, byte[] pFileNameList);

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int OFS_BeginSendMultiFiles(uint dwHand);

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int OFS_SendFile(uint dwHand, byte overwrite, byte[] pFilePath);
        //发送节目
        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int OFS_SendFileData(uint dwHand, byte overwrite, byte[] pFileName, ushort DisplayType, byte PlayTimes,
            byte[] ProgramLife, byte ProgramWeek, byte ProgramTime, byte[] Period, byte AreaNum, byte[] AreaDataList, int AreaDataListLen);

        //添加字库
        public delegate void CloseFunc(int total, int sendlen);
        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int OFS_SendFontData(uint dwHand, byte overwrite, byte[] pFileName, byte FontWidth, byte FontHeight,
            byte[] LibData, int LibData_len, byte FontEncode, CloseFunc pCloseFunc);

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int OFS_SendFirmWareData(uint dwHand, byte overwrite, byte[] pFileName, byte[] FirmWareData, int FirmWareDataLen, CloseFunc pCloseFunc);
        //设置屏参
        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int OFS_SendScreenData(uint dwHand, byte overwrite, byte[] pFileName, ushort Address, byte Baudrate,
            ushort ScreenWith, ushort ScreenHeight, byte Color, byte MirrorMode, byte OE, byte DA, byte RowOrder, byte FreqPar, 
            byte OEAngle, byte CommTimeout);

        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int OFS_EndSendMultiFiles(uint dwHand);

        //设置客户信息
        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern int OFS_SetFontInformation(uint dwHand, byte OverWrite, byte[] ClientMsg);
        
        public delegate void CallBackCon(uint dwHand, string pid);
        public delegate void CallBackLedClose(uint dwHand, string pid, int err_code);
        //启动gprs服务器
        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern bool StartGprsServer(uint port,CallBackCon pCallBackCon,CallBackLedClose pCallBackLedClose);
        //关闭gprs服务器                 
        [DllImport("Led5kSDK.dll", CharSet = CharSet.Unicode)]
        public static extern void CloseGprsServer();

    }
    public class Led5kstaticArea
    {
        public Led5kSDK.bx_5k_area_header header;
        public string text;
        public byte[] AreaToByteArray()
        {
            //计算header的大小：结构体bx_5k_area_header的大小
            ONNONLed5KSDKD.Led5kSDK.bx_5k_area_header tu = new Led5kSDK.bx_5k_area_header();
            int hsz = Marshal.SizeOf(tu);

            //计算len的大小
            byte[] tmp = System.Text.Encoding.Default.GetBytes(text);
            int len = tmp.Length + hsz + 4;
            header.DataLen = tmp.Length;
            //先copy len
            byte[] bt = new byte[len];
            //byte[] lenToByte = System.BitConverter.GetBytes(len);
            byte[] lenToByte = System.BitConverter.GetBytes(len);
            lenToByte.CopyTo(bt, 0);
            int index = lenToByte.Length;

            //再copy header
            //分配结构体大小的内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(hsz);
            //将结构体拷到分配好的内存空间
            Marshal.StructureToPtr(header, structPtr, false);

            //从内存空间拷到AreaDataList数组
            Marshal.Copy(structPtr, bt, index, hsz);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            //copy text
            tmp.CopyTo(bt, index + hsz);
            return bt;
        }
        public int getAreaLen()
        {
            Led5kSDK.bx_5k_area_header tu = new Led5kSDK.bx_5k_area_header();
            int hsz = Marshal.SizeOf(tu);
            //再考header

            byte[] tmp = System.Text.Encoding.Default.GetBytes(text);
            int len = tmp.Length + hsz + 4;
            return len;
        }
    }
    public class Led5kProgram
    {
        public string name;
        public bool overwrite;

        public ushort DisplayType;
        public byte PlayTimes;

        public bool IsValidAlways;
        public ushort StartYear;
        public byte StartMonth;
        public byte StartDay;
        public ushort EndYear;
        public byte EndMonth;
        public byte EndDay;

        public byte ProgramWeek;

        public bool IsPlayOnTime;
        public byte StartHour;
        public byte StartMinute;
        public byte StartSecond;
        public byte EndHour;
        public byte EndMinute;
        public byte EndSecond;
        public byte AreaNum;
        public List<Led5kstaticArea> m_arealist=new List<Led5kstaticArea>();

        #region//转DCB码
        public static byte byte2bcd(byte num)
        {
            int i = num;
            return (byte)(i / 10 * 16 + i % 10);
        }
        public static byte bcd2byte(byte num)
        {
            int i = num;
            return (byte)(i / 16 * 10 + i % 16);
        }
        public static byte[] short2bcd(ushort num)
        {
            int i = num;
            byte high = (byte)(i / 100);
            byte low = (byte)(i % 100);
            byte[] tmp = new byte[2];
            tmp[0] = byte2bcd(low);
            tmp[1] = byte2bcd(high);
            return tmp;
        }
        #endregion

        public int SendProgram(uint hand)
        {

            byte[] ppFileName;
            byte[] ProgramLife;
            byte PlayPeriodGrpNum;
            byte[] Period;
            byte[] AreaDataList;
            int AreaDataListLen;

            int sum = 0;
            foreach (ONNONLed5KSDKD.Led5kstaticArea s in m_arealist)
            {
                sum += s.getAreaLen();
            }
            AreaDataList = new byte[sum];
            int index = 0;
            foreach (ONNONLed5KSDKD.Led5kstaticArea s in m_arealist)
            {
                byte[] bt = s.AreaToByteArray();
                bt.CopyTo(AreaDataList, index);
                index += bt.Length;
            }
            AreaDataListLen = sum;
            if (IsValidAlways == true)
            {
                ProgramLife = new byte[8];
                ProgramLife[0] = 0xff;
                ProgramLife[1] = 0xff;
                ProgramLife[2] = 0xff;
                ProgramLife[3] = 0xff;
                ProgramLife[4] = 0xff;
                ProgramLife[5] = 0xff;
                ProgramLife[6] = 0xff;
                ProgramLife[7] = 0xff;
            }
            else
            {

                ProgramLife = new byte[8];
                byte[] tmp = Led5kProgram.short2bcd(StartYear);
                ProgramLife[0] = tmp[0];
                ProgramLife[1] = tmp[1];
                ProgramLife[2] = byte2bcd(StartMonth);
                ProgramLife[3] = byte2bcd(StartDay);

                byte[] tmp1 = Led5kProgram.short2bcd(EndYear);

                ProgramLife[4] = tmp1[0];
                ProgramLife[5] = tmp1[1];
                ProgramLife[6] = byte2bcd(EndMonth);
                ProgramLife[7] = byte2bcd(EndDay);
            }


            ppFileName = System.Text.Encoding.Default.GetBytes(name);

            if (IsPlayOnTime == true)
            {
                Period = new byte[7];
                Period[0] = byte2bcd(StartHour);
                Period[1] = byte2bcd(StartMinute);
                Period[2] = byte2bcd(StartSecond);
                Period[3] = byte2bcd(EndHour);
                Period[4] = byte2bcd(EndMinute);
                Period[5] = byte2bcd(EndSecond);
                Period[6] = 0;
                
            }
            else
            {
                Period = null; 
            }

            PlayPeriodGrpNum = Convert.ToByte(IsPlayOnTime ? 1 : 0);

            return ONNONLed5KSDKD.Led5kSDK.OFS_SendFileData(hand, 1, ppFileName, DisplayType, PlayTimes, ProgramLife,
                ProgramWeek, PlayPeriodGrpNum, Period, AreaNum, AreaDataList, AreaDataListLen);
        }
    }
}
