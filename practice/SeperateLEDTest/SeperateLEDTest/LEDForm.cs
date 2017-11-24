using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SeperateLEDTest
{
    public partial class LEDForm : Form
    {
        public LEDForm()
        {
            InitializeComponent();
        }

        private void LEDForm_Load(object sender, EventArgs e)
        {
            FirstRunUI();
        }

        private void FirstRunUI()
        {
            //初始化UI 通信方式设置
            this.selectBox.Text = SysConfig.MsgWay;
            if (SysConfig.MsgWay.Equals("串口") )
            {
                ShowCKExchangeUI();
                this.cb1.Text = SysConfig.comNum; //串口号
                this.cb2.Text = SysConfig.btl; //波特率
            }
            else if (SysConfig.MsgWay.Equals("网口"))
            {
                ShowWKExchangeUI();
                this.NetIp.Text = SysConfig.netIp;
                this.NetPort.Text = SysConfig.netPort;
            }
            this.ledFirstPart.Text = SysConfig.ledFirstPart;
            this.LedLastPart.Text = SysConfig.ledLastPart;
        }
        /// <summary>
        /// 显示通信方式为串口时的界面
        /// </summary>
        private void ShowCKExchangeUI()
        {
            this.panel1.Controls.Clear();
            //选择通信方式为：串口
            lbl1 = new Label();
            this.panel1.Controls.Add(lbl1);
            lbl1.Name = "lbl1";
            lbl1.Text = "串口号：";
            lbl1.Top = 5;
            lbl1.Width = 80;
            lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lbl1.Height = 20;
            //串口选项
            cb1 = new ComboBox();
            this.panel1.Controls.Add(cb1);
            cb1.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10"});
            cb1.Text = SysConfig.comNum;
            cb1.Name = "comNum";
            cb1.Top = lbl1.Top;
            cb1.Left = lbl1.Right + 5;
            cb1.Width = 100;
            cb1.Height = 20;

            //波特率 
            lbl2 = new Label();
            this.panel1.Controls.Add(lbl2);
            lbl2.Name = "lbl2";
            lbl2.Text = "波特率：";
            lbl2.Top = 5;
            lbl2.Width = 80;
            lbl2.Left = cb1.Right + 5;
            lbl2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lbl2.Height = 20;
            //波特率选项
            cb2 = new ComboBox();
            this.panel1.Controls.Add(cb2);
            cb2.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "56000",
            "57600"});
            cb2.Text = SysConfig.btl;
            cb2.Name = "comNum";
            cb2.Top = lbl2.Top;
            cb2.Left = lbl2.Right + 5;
            cb2.Width = 100;
            cb2.Height = 20;

            //确认修改按钮 buttom
            //bt1 = new Button();
            //this.panel1.Controls.Add(bt1);
            //bt1.Text = "确认连接";
            //bt1.Width = 80;
            //bt1.Height = 20;
            //bt1.Top = cb2.Top;
            //bt1.Left = cb2.Right + 15;
        }

        private void ShowWKExchangeUI()
        {
            this.panel1.Controls.Clear();

            //选择通信方式为：网口
            lbl1 = new Label();
            this.panel1.Controls.Add(lbl1);
            lbl1.Name = "lbl1";
            lbl1.Text = "IP地址：";
            lbl1.Top = 5;
            lbl1.Width = 80;
            lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lbl1.Height = 20;
            //IP地址
            NetIp = new TextBox();
            this.panel1.Controls.Add(NetIp);
            NetIp.Name = "NetIp";
            NetIp.Text = SysConfig.netIp;
            NetIp.Top = lbl1.Top;
            NetIp.Left = lbl1.Right + 5;
            NetIp.Width = 100;
            NetIp.Height = 20;

            //端口 
            lbl2 = new Label();
            this.panel1.Controls.Add(lbl2);
            lbl2.Name = "lbl2";
            lbl2.Text = "端口：";
            lbl2.Top =  5;
            lbl2.Left = NetIp.Right + 5;
            lbl2.Width = 80;
            lbl2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lbl2.Height = 20;

            //端口值
            NetPort = new TextBox();
            this.panel1.Controls.Add(NetPort);
            NetPort.Name = "NetPort";
            NetPort.Text = SysConfig.netPort;
            NetPort.Top = lbl2.Top;
            NetPort.Left = lbl2.Right + 5;
            NetPort.Width = 100;
            NetPort.Height = 20;

            //确认修改按钮 buttom
            //bt2 = new Button();
            //this.panel1.Controls.Add(bt2);
            //bt2.Text = "确认连接";
            //bt2.Width = 80;
            //bt2.Height = 20;
            //bt2.Top = NetPort.Top;
            //bt2.Left = NetPort.Right + 15;
        }

        private void GiveUp_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void Comfirm_Click(object sender, EventArgs e)
        {
            SysConfig.MsgWay = this.selectBox.Text.ToString();
            if (this.selectBox.Text.ToString().Equals("串口"))
            {
                SysConfig.comNum = this.cb1.Text.ToString();
                SysConfig.btl = this.cb2.Text.ToString();
            }
            else if (this.selectBox.Text.ToString().Equals("网口"))
            {
                SysConfig.netIp = this.NetIp.Text.ToString();
                SysConfig.netPort = this.NetPort.Text.ToString();
            }
            SysConfig.ledFirstPart = this.ledFirstPart.Text.ToString();
            SysConfig.ledLastPart = this.LedLastPart.Text.ToString();
            SysConfig.SaveLEDParam();
            MessageBox.Show("LED启用成功", "提示", MessageBoxButtons.OK);
        }

        private void selectBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectBox.Text.Equals("串口"))
            {
                ShowCKExchangeUI();

            }
            else if (selectBox.Text.Equals("网口"))
            {
                ShowWKExchangeUI();

            }
        }

    }
}
