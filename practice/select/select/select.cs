using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace select
{
    public partial class select : Form
    {
        public List<String> li1;
        public List<String> li2;
        public List<String> li3;
        public string TimeStamp = string.Empty;
        public StreamWriter sw1,sw2,sw3;  
        public string strr1, strr2, strr3;  //输出0KB、1KB、2KB文件的个数和帧时间差为0的文件个数以及帧时间差大于40ms的文件个数
        public string twoOne, twoTwo, twoThree; //输出0KB、1KB、2KB的文件名
        public string oneResult, twoResult, threeResult;  //输出帧时间差为0和大于40ms的波形文件名
        public int zerotimeCount1, fortytimeCount1, zeroCount1, oneCount1, twoCount1;  //统计帧时间差为0和大于40ms的波形文件个数以及0KB、1KB、2KB文件个数
        public int zerotimeCount2, fortytimeCount2, zeroCount2, oneCount2, twoCount2;  //统计帧时间差为0和大于40ms的波形文件个数以及0KB、1KB、2KB文件个数
        public int zerotimeCount3, fortytimeCount3, zeroCount3, oneCount3, twoCount3; //统计帧时间差为0和大于40ms的波形文件个数以及0KB、1KB、2KB文件个数
        public int twoOneCount, twoTwoCount, twoThreeCount;   //统计非波形文件个数
        public select()
        {
            InitializeComponent();
            //初始化委托
            InitDelegateState();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            li1 = new List<string>();
            li2 = new List<string>();
            li3 = new List<string>();
            zerotimeCount1 = 0;
            fortytimeCount1 = 0;
            zeroCount1 = 0;
            oneCount1 = 0;
            twoCount1 = 0;
            zerotimeCount2 = 0;
            fortytimeCount2 = 0;
            zeroCount2 = 0;
            oneCount2 = 0;
            twoCount2 = 0;
            zerotimeCount3 = 0;
            fortytimeCount3 = 0; 
            zeroCount3 = 0;
            oneCount3 = 0;
            twoCount3 = 0;
            strr1 = "";
            strr2 = "";
            strr3 = "";
            oneResult = "";
            twoResult = "";
            threeResult = "";
            twoOne = "";
            twoTwo = "";
            twoThree = "";
            twoOneCount = 0;
            twoTwoCount = 0;
            twoThreeCount = 0;
            sw1 = null;
            sw2 = null;
            sw3 = null ;
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            zerotimeCount1 = 0;
            fortytimeCount1 = 0;
            zeroCount1 = 0;
            oneCount1 = 0;
            twoCount1 = 0;
            richTextBox1.Text = " ";
            richTextBox4.Text = " ";
            richTextBox7.Text = " ";
            li1 = new List<string>();
            oneResult = "";
            twoOne = "";
            twoOneCount = 0;
            sw1 = null;
            
            Thread tt = new Thread(delegate() { OneKeyRunProc(0); });
            tt.SetApartmentState(ApartmentState.STA);
            tt.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            zerotimeCount2 = 0;
            fortytimeCount2 = 0;
            zeroCount2 = 0;
            oneCount2 = 0;
            twoCount2 = 0;
            richTextBox2.Text = " ";
            richTextBox5.Text = " ";
            richTextBox8.Text = " ";
            li2 = new List<string>();
            twoResult = "";
            twoTwo = "";
            twoTwoCount = 0;
            sw2 = null;
            
            Thread tt = new Thread(delegate() { OneKeyRunProc(1); });
            tt.SetApartmentState(ApartmentState.STA);
            tt.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            zerotimeCount3 = 0;
            fortytimeCount3 = 0;
            zeroCount3 = 0;
            oneCount3 = 0;
            twoCount3 = 0;
            richTextBox3.Text = " ";
            richTextBox6.Text = " ";
            richTextBox9.Text = " ";
            li3 = new List<string>();
            threeResult = "";
            twoThree = "";
            twoThreeCount = 0;
            sw3 = null;

            Thread tt = new Thread(delegate() { OneKeyRunProc(2); });
            tt.SetApartmentState(ApartmentState.STA);
            tt.Start();
        }


        //UI委托类初始化函数
        private void InitDelegateState()
        {
            DelegateState.DelegateTextBoxOne = DelegateTextBoxOne;
            DelegateState.DelegateTextBoxTwo = DelegateTextBoxTwo;
            DelegateState.DelegateTextBoxThree = DelegateTextBoxThree;
            DelegateState.DelegateTextBoxTen = DelegateTextBoxTen;
        }

        //通过委托向第1层TextBox中更新数据
        private void DelegateTextBoxTwo(string msg, int N)
        {
            try
            {
                this.Invoke(new ThreadStart(delegate
                {
                    if (0 == N)
                    {
                        this.richTextBox4.Text = msg;
                        sw1.Write(msg.Replace("\n", "\r\n"));
                        sw1.WriteLine(richTextBox7.Text.Replace("\n", "\r\n"));
                        sw1.WriteLine(richTextBox1.Text.Replace("\n", "\r\n"));
                    }
                    else if (1 == N)
                    {
                        this.richTextBox5.Text = msg;
                        sw2.Write(msg.Replace("\n","\r\n"));
                        sw2.WriteLine(richTextBox8.Text.Replace("\n", "\r\n"));
                        sw2.WriteLine(richTextBox2.Text.Replace("\n", "\r\n"));
                    }
                    else if (2 == N)
                    {
                        this.richTextBox6.Text = msg;
                        sw3.Write(msg.Replace("\n", "\r\n"));
                        sw3.WriteLine(richTextBox9.Text.Replace("\n", "\r\n"));
                        sw3.WriteLine(richTextBox3.Text.Replace("\n", "\r\n"));
                    }
                }
                    ));
            }
            catch (System.Exception ex)
            {

            }
        }

        //通过委托向第2层TextBox中更新数据
        private void DelegateTextBoxOne(string msg, int N)
        {
            try
            {
                this.Invoke(new ThreadStart(delegate
                {
                    if (0 == N)
                    {
                         this.richTextBox7.Text = msg;

                    }
                    else if (1 == N)
                    {
                         this.richTextBox8.Text = msg;
                    }
                    else if (2 == N)
                    {
                         this.richTextBox9.Text = msg;
                    }
                }
                    ));
            }
            catch (System.Exception ex)
            {

            }
        }


        //通过委托向第3层TextBox中更新数据
        private void DelegateTextBoxThree(string msg, int N)
        {
            try
            {
                this.Invoke(new ThreadStart(delegate
                {
                    if (0 == N)
                    {
                        this.richTextBox1.Text = msg;

                    }
                    else if (1 == N)
                    {
                        this.richTextBox2.Text = msg;
                    }
                    else if (2 == N)
                    {
                        this.richTextBox3.Text = msg;
                    }
                }
                    ));
            }
            catch (System.Exception ex)
            {

            }
        }


        //选择文件夹并打开，遍历选定文件夹中所有的波形文件，统计并输出波形情况
        private void OneKeyRunProc(int N)
        {
            string path = "";
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = false;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                path = folderBrowserDialog.SelectedPath;
            }
            DelegateState.DelegateTextBoxOne("", N);
            DelegateState.DelegateTextBoxTwo("", N);
            DelegateState.DelegateTextBoxThree("", N);

            //遍历文件夹内的文件  
            if (path != "")
            {
                foreach (string fileName in Directory.GetFiles(path))
                {
                    if (fileName.Substring(fileName.Length - 4) == ".lmd")
                    {
                        //对各个文件进行操作  
                        FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);   //读取选择的十六制文件的内容
                        BinaryReader br = new BinaryReader(fs);    //以二进制读取

                        int count = (int)fs.Length;
                        string[] bt = new string[count];
                        while (count > 0)
                        {
                            byte by = br.ReadByte();

                            string s = Convert.ToString(by, 16);  //将二进制字节流转换为16进制，还原成原文件内容
                            if (s.Length == 1)
                                s = "0" + s;
                            bt[bt.Length - count] = s;    //存入数组，方便取目标数据 
                            count--;
                        }

                        String strr = "";
                        string ss = System.Windows.Forms.Application.StartupPath + "\\波形测试结果";  //获取启动了应用程序的可执行文件的路径
                        if (!Directory.Exists(ss))
                        {
                            Directory.CreateDirectory(ss);
                        }

                        int l = bt.Length;
                        switch (N)
                        {
                            case 0:
                                if (null == sw1)
                                    sw1 = new StreamWriter(ss + "\\" + Path.GetFileNameWithoutExtension(path) + "激光器" + 1 + "不合格文件.txt");
                                if (l == 0)
                                {
                                    twoOne += "输入的文件中有文件内容为空： \n" + fileName + "是空的！！！" + "\n\n";
                                    zeroCount1++;
                                }
                                else
                                {
                                    int result1 = checkLaser1(bt);
                                    string s1 = bt[0] + bt[1] + bt[2] + bt[3];
                                    if (Convert.ToInt32(bt[830], 16) == result1 && "02020202" == s1)
                                    {
                                        computeTime(strr, bt, N, fileName);
                                    }
                                    else
                                    {
                                        twoOne += "输入的文件" + fileName + "不是激光器波形文件！！请检查数据是否有误！！！" + "\n\n";
                                        twoOneCount++;
                                    }

                                }
                                break;
                            case 1:
                                if (null == sw2)
                                    sw2 = new StreamWriter(ss + "\\" + Path.GetFileNameWithoutExtension(path) + "激光器" + 2 + "不合格文件.txt");
                                if (l >= 831 * (N + 1))
                                {
                                    int result2 = checkLaser2(bt);
                                    string s2 = bt[0 + 831 * N] + bt[1 + 831 * N] + bt[2 + 831 * N] + bt[3 + 831 * N];
                                    if ((Convert.ToInt32(bt[830 + 831 * N], 16) == result2) && ("02020202" == s2))
                                    {
                                        computeTime(strr, bt, N, fileName);
                                    }
                                    else
                                    {
                                        twoTwo += "输入的文件" + fileName + "不是激光器波形文件！！请检查数据是否有误！！！" + "\n\n";
                                        twoTwoCount++;
                                    }
                                }
                                else if (l == 0)
                                {
                                    twoTwo += "输入的文件中有文件内容为空： \n" + fileName + "是空的！！！" + "\n\n";
                                    zeroCount2++;
                                }
                                else if (l == 831 * N)
                                {
                                    twoTwo += "输入的文件中有文件内容不完整： \n" + fileName + "只有1KB！！！" + "\n\n";
                                    oneCount2++;
                                }
                                break;
                            case 2:
                                if (null == sw3)
                                    sw3 = new StreamWriter(ss + "\\" + Path.GetFileNameWithoutExtension(path) + "激光器" + 3 + "不合格文件.txt");
                                if (l == 831 * (N + 1))
                                {
                                    int result1 = checkLaser3(bt);
                                    string s3 = bt[0 + 831 * N] + bt[1 + 831 * N] + bt[2 + 831 * N] + bt[3 + 831 * N];
                                    if ((Convert.ToInt32(bt[830 + 831 * N], 16) == result1) && ("02020202" == s3))
                                    {
                                        computeTime(strr, bt, N, fileName);
                                    }
                                    else
                                    {
                                        twoThree += "输入的文件" + fileName + "不是激光器波形文件！！请检查数据是否有误！！！" + "\n\n";
                                        twoThreeCount++;
                                    }
                                }
                                else if (l == 0)
                                {
                                    twoThree += "输入的文件中有文件内容为空： \n" + fileName + "是空的！！！" + "\n\n";
                                    zeroCount3++;
                                }
                                else if (l == 831)
                                {
                                    twoThree += "输入的文件中有文件内容不完整： \n" + fileName + "只有1KB！！！" + "\n\n";
                                    oneCount3++;
                                }
                                else if (l == 831 * N)
                                {
                                    twoThree += "输入的文件中有文件内容不完整： \n" + fileName + "只有2KB！！！" + "\n\n";
                                    twoCount3++;
                                }
                                break;
                        }

                        fs.Close();
                        br.Close();
                    }
                    
                }
            }

            if (N == 0)
            {
                strr1 = "输入的有内容为空的文件个数为：" + zeroCount1 + "个" + "\n\n" + "输入的不是激光器波形的文件个数为：" + twoOneCount + "个" + "\n\n"
                       + "帧时间差为0的文件个数为：" + zerotimeCount1 + "个" + "\n\n" + "帧时间差大于40ms的文件个数为：" + fortytimeCount1 + "个" + "\n\n";
                DelegateState.DelegateTextBoxThree(oneResult, 0); 
                DelegateState.DelegateTextBoxOne(twoOne, 0);
                DelegateState.DelegateTextBoxTwo(strr1, 0);
                if(null != sw1)
                sw1.Close();
            } 
            if (N == 1)
            {
                strr2 = "输入的有内容为空的文件个数为：" + zeroCount2 + "个" + "\n\n" + "输入的不是激光器波形的文件个数为：" + twoTwoCount + "个" + "\n\n" + "输入的有内容为1KB的文件个数为：" + 
                        oneCount2 + "个" + "\n\n"+ "帧时间差为0的文件个数为：" + zerotimeCount2 + "个" + "\n\n" + "帧时间差大于40ms的文件个数为：" + fortytimeCount2 + "个" + "\n\n";
                DelegateState.DelegateTextBoxThree(twoResult, 1); 
                DelegateState.DelegateTextBoxOne(twoTwo, 1);
                DelegateState.DelegateTextBoxTwo(strr2, 1);
                if (null != sw2)
                sw2.Close();
            }
            if (N == 2)
            {
                strr3 = "输入的有内容为空的文件个数为：" + zeroCount3 + "个" + "\n\n" + "输入的不是激光器波形的文件个数为：" + twoThreeCount + "个" + "\n\n" + "输入的有内容为1KB的文件个数为：" + oneCount3 + "个" 
                        + "\n\n" + "输入的有内容为2KB的文件个数为：" + twoCount3 + "个" + "\n\n" + "帧时间差为0的文件个数为：" + zerotimeCount3 + "个" + "\n\n" + "帧时间差大于40ms的文件个数为：" + fortytimeCount3 + "个" + "\n\n";
                DelegateState.DelegateTextBoxThree(threeResult, 2); 
                DelegateState.DelegateTextBoxOne(twoThree, 2);
                DelegateState.DelegateTextBoxTwo(strr3, 2);
                if (null != sw3)
                sw3.Close();
            }  

        }

        private void computeTime(string strr, string[] bt, int N, string fileName)
        {
            strr = bt[831 * N + 42] + bt[831 * N + 43] + bt[831 * N + 44] + bt[831 * N + 45];
            switch (N)
            {
                case 0:
                     li1.Add(strr);
                     compute(li1, fileName, N);
                    break;
                case 1:
                     li2.Add(strr);
                     compute(li2, fileName, N);
                    break;
                case 2:
                     li3.Add(strr);
                     compute(li3, fileName, N);
                    break;
            }
        }

        //计算帧时间差为0和大于40ms的文件个数以及该文件对应的帧时间和上一个文件对应的帧时间
        private void compute(List<string> li, string fileName,int N)
        {
            if (li.Count > 1)      //计算两两相邻文件的帧时间差，找出不合格指标文件，将文件名记录到文档中
            {

               int ttime = Convert.ToInt32(li[li.Count - 1], 16) - Convert.ToInt32(li[li.Count - 2], 16);

                if (ttime == 0)
                {
                    switch (N)
                    {
                        case 0:
                            zerotimeCount1++;
                            oneResult += "帧时间差为0的有： \n" + fileName + "，其帧时间为： " + li[li.Count - 1] + "，上一帧的帧时间为：" + li[li.Count - 2] + "\n\n";
                            break;
                        case 1:
                            zerotimeCount2++;
                            twoResult += "帧时间差为0的有： \n" + fileName + "，其帧时间为： " + li[li.Count - 1] + "，上一帧的帧时间为：" + li[li.Count - 2] + "\n\n";
                            break;
                        case 2:
                            zerotimeCount3++;
                            threeResult += "帧时间差为0的有： \n" + fileName + "，其帧时间为： " + li[li.Count - 1] + "，上一帧的帧时间为：" + li[li.Count - 2] + "\n\n";
                            break;
                    }
                }
                if (ttime > 40000)
                {
                    switch (N)
                    {
                        case 0:
                            fortytimeCount1++;
                            oneResult += "帧时间差大于40ms的有： \n" + fileName + "  帧时间差为：" + ttime / 1000 + "ms" + "，其帧时间为： "
                                         +li[li.Count - 1] + "，上一帧的帧时间为：" + li[li.Count - 2] + "\n\n";
                            break;
                        case 1:
                            fortytimeCount2++;
                            twoResult += "帧时间差大于40ms的有： \n" + fileName + "  帧时间差为：" + ttime / 1000 + "ms" + "，其帧时间为： "
                                         + li[li.Count - 1] + "，上一帧的帧时间为：" + li[li.Count - 2] + "\n\n";
                            break;
                        case 2:
                            fortytimeCount3++;
                            threeResult += "帧时间差大于40ms的有： \n" + fileName + "  帧时间差为：" + ttime / 1000 + "ms" + "，其帧时间为： "
                                         + li[li.Count - 1] + "，上一帧的帧时间为：" + li[li.Count - 2] + "\n\n";
                            break;
                    }

                }

            }
        }

        //激光器1帧校验
        private int checkLaser1(string[] bt)
        {
            int i;
            int result = 0;
            for (i = 8; i < 830; i++)
            {
               result ^= Convert.ToInt32(bt[i], 16);
            }
            return result;
        }

        //激光器2帧校验
        private int checkLaser2(string[] bt)
        {
            int i;
            int result = 0;
            for (i = 8 + 831; i < 830 + 831; i++)
            {
                result ^= Convert.ToInt32(bt[i], 16);
            }
            return result;
        }

        //激光器3帧校验
        private int checkLaser3(string[] bt)
        {
            int i;
            int result = 0;
            for (i = 8 + 1662; i < 830 + 1662; i++)
            {
                result ^= Convert.ToInt32(bt[i], 16);
            }
            return result;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Thread tt = new Thread(delegate() { JieYaFile(); });
            tt.SetApartmentState(ApartmentState.STA);
            tt.Start();
        }

        ///将文件夹下的所有合帧文件解压成单帧文件
        public void JieYaFile()
        {
            string path = "";
            FolderBrowserDialog folderBrowerDialog = new FolderBrowserDialog();
            folderBrowerDialog.ShowNewFolderButton = false;
            if(folderBrowerDialog.ShowDialog() == DialogResult.OK)
            {
                path = folderBrowerDialog.SelectedPath;
                DelegateState.DelegateTextBoxTen("正在解压，请耐心等待......", 1);
            }
            if(path != "")
            {
                string ss = "";
                if (Directory.GetDirectories(path).Length != 0)//若文件夹下有文件夹，则遍历子文件夹中的lmds文件
                {
                    foreach (string folder in Directory.GetDirectories(path))
                    {
                        if (Directory.GetFiles(folder).Length != 0)
                        {
                            createLmd(folder, ss);
                            
                        }
                        else
                        {
                            DelegateState.DelegateTextBoxTen("请选择包含合帧文件的文件夹", 1);
                        }
                       
                    }
                }
                else//若文件夹下无子文件夹，则遍历该文件夹中的lmds文件
                {
                    if (Directory.GetFiles(path).Length != 0)
                    {
                        createLmd(path, ss);
                    }
                    else
                    {
                        DelegateState.DelegateTextBoxTen("请选择包含合帧文件的文件夹", 1);
                    }
                    
                }  
            }
        }

        /// <summary>
        /// 遍历文件夹中的合帧文件，解压生成单帧文件
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <param name="ss">输出信息</param>
        private void createLmd(string path,string ss)
        {
            if (path != "")
            {
                foreach (string fileName in Directory.GetFiles(path))
                {
                    if (fileName.Substring(fileName.Length - 5) == ".lmds")
                    {
                        FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        BinaryReader br = new BinaryReader(fs);
                        int wavecount = (int)fs.Length; //字节总数
                        int lmdCount = (int)fs.Length / 2493;  //波形个数，3个激光器对应一个波形（831*3）
                        byte[] bt = new byte[(int)fs.Length];
                        if ((int)fs.Length > 2493)
                        {
                            while (wavecount > 0)
                            {
                                byte by = br.ReadByte();
                                bt[bt.Length - wavecount] = by; //将lmds中的所有字节存入数组中
                                wavecount--;
                            }
                            string lmdName = "";
                            string filePath = Path.GetDirectoryName(fileName) + "\\" + Path.GetFileNameWithoutExtension(fileName);
                            if (Directory.Exists(filePath))
                            {
                                Directory.Delete(filePath, true);  //若存在该文件夹，则删除该文件夹及其下所有子文件

                            }
                            for (int len = 0; len < lmdCount; len++)
                            {
                                if (!Directory.Exists(filePath))
                                {
                                    Directory.CreateDirectory(filePath); //若不存在该文件夹，则创建
                                }
                                string tempLen = len.ToString();
                                string lenStr = "";
                                //对波形文件补零命名，保证遍历文件时按保存时间先后进行遍历
                                if (tempLen.ToString().Length == 1) //一位数
                                {
                                    lenStr = "000000000" + tempLen;
                                }
                                if (tempLen.ToString().Length == 2) //两位数
                                {
                                    lenStr = "00000000" + tempLen;
                                }
                                if (tempLen.ToString().Length == 3) //三位数
                                {
                                    lenStr = "0000000" + tempLen;
                                }
                                if (tempLen.ToString().Length == 4) //四位数
                                {
                                    lenStr = "000000" + tempLen;
                                }
                                if (tempLen.ToString().Length == 5) //五位数
                                {
                                    lenStr = "00000" + tempLen;
                                }
                                if (tempLen.ToString().Length == 6) //六位数
                                {
                                    lenStr = "0000" + tempLen;
                                }
                                if (tempLen.ToString().Length == 7) //七位数
                                {
                                    lenStr = "000" + tempLen;
                                }
                                if (tempLen.ToString().Length == 8) //八位数
                                {
                                    lenStr = "00" + tempLen;
                                }
                                if (tempLen.ToString().Length == 9) //九位数
                                {
                                    lenStr = "0" + tempLen;
                                }
                                if (tempLen.ToString().Length == 10) //十位数
                                {
                                    lenStr = tempLen;
                                }
                                lmdName = "A_" + DateTime.Now.ToString("HH-mm-ss-fff") + "_" + lenStr + ".lmd";
                                FileStream myStream = new FileStream(filePath + "\\" + lmdName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                                BinaryWriter myWriter = new BinaryWriter(myStream);
                                byte[] oneByte = new byte[831];
                                byte[] twoByte = new byte[831];
                                byte[] threeByte = new byte[831];
                                Array.Copy(bt, 0 + 2493 * len, oneByte, 0, 831);
                                Array.Copy(bt, 831 + 2493 * len, twoByte, 0, 831);
                                Array.Copy(bt, 831 * 2 + 2493 * len, threeByte, 0, 831);
                                byte[] lmd = new byte[831 * 3];
                                Array.Clear(lmd, 0, lmd.Length);//数组清零
                                Array.Copy(oneByte, 0, lmd, 0, 831); //将激光器1波形数据存入数组lmd中
                                Array.Copy(twoByte, 0, lmd, 831, 831);//将激光器2波形数据存入数组lmd中
                                Array.Copy(threeByte, 0, lmd, 831 * 2, 831);//将激光器3波形数据存入数组lmd中
                                myWriter.Write(lmd);
                                myWriter.Close();
                                fs.Close();
                            }

                            ss += "\n合帧文件解压结果为：\n" + Path.GetFileNameWithoutExtension(fileName) + ".lmds成功解压为"
                                  + lmdCount + "个单帧lmd文件\n" + "合帧文件" + Path.GetFileNameWithoutExtension(fileName) + ".lmds解压完成！！！";
                            DelegateState.DelegateTextBoxTen(ss, lmdCount);
                        }
                    }
                }
            }
            
        }

        //向解压文件对应的文本框输出内容
        private void DelegateTextBoxTen(string msg, int count)
        {
            try
            {
                this.Invoke(new ThreadStart(delegate
                {
                    
                        this.richTextBox10.Text = msg;
                }
                    ));
            }
            catch (System.Exception ex)
            {

            }
        }
    }

}
