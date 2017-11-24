using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SeperateLEDTest
{
    class SockectClient
    {

        #region 全局变量
        Form1 MF;

        public bool IsConnected = false;
        public Socket LED_client_scoket = null;
        public IPAddress SerIp = null;
        public int SerPort = 0;
        public int LostDataCnt = 0;
        public bool ManulDisCon = false;
        public bool queryFlag = false;       
        #endregion

        public SockectClient(Form1 mf, string Serip, int nPort)
        {
            SerIp = IPAddress.Parse(Serip);
            SerPort = int.Parse(nPort.ToString());
            MF = mf;
            
        }

        private void InitParam()
        {
            IsConnected = false;
            
        }

        /// <summary>
        /// 启用LED屏
        /// </summary>
        /// <returns></returns>
        public bool StartLEDConnect()
        {
            //定义主机的IP及端口
            IPEndPoint ipEnd = new IPEndPoint(SerIp, SerPort);
            LED_client_scoket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //尝试连接
            try
            {
                InitParam();
                LED_client_scoket.Connect(ipEnd);

                string iptemp = ((IPEndPoint)LED_client_scoket.RemoteEndPoint).Address.ToString();
                int portint = ((IPEndPoint)LED_client_scoket.RemoteEndPoint).Port;
                string porttemp = portint.ToString();

                IsConnected = true;

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 禁用LED屏
        /// </summary>
        public void CloseLEDConnect()
        {

            if (LED_client_scoket != null)
            {
                try
                {
                    LED_client_scoket.Shutdown(SocketShutdown.Both);
                    LED_client_scoket.Disconnect(false);
                }
                catch (Exception ex)
                {
                    //DelegateState.DelegateTipsText(ex.Message.ToString());
                }
                finally
                {
                    LED_client_scoket.Close();
                    LED_client_scoket.Dispose();
                    LED_client_scoket = null;
                }
            }

        }

        //给LED屏发送已封装好的数据
        public int SendLEDMsg(byte[] Data,int datasize)
        {
            int len = -1;
            if (LED_client_scoket != null && IsConnected)
            {
                try
                {
                    len = LED_client_scoket.Send(Data, datasize, SocketFlags.None);
                    byte[] RecvDataTmp = new byte[10240];
                    LED_client_scoket.ReceiveTimeout = 2000;  //防止socket阻塞
                    int lent = LED_client_scoket.Receive(RecvDataTmp);
                    LED_client_scoket.ReceiveTimeout = 0; //进入下次接收数据
                    if (lent <= 0)
                    {
                        IsConnected = false;
                    }
                }
                catch (Exception ex)
                {
                    len = -1;
                }
            }
            return len;
        }
    }
}
