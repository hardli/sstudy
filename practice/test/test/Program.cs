using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int count = 0; count < 20; count++)
            {
                string countstr = count.ToString();
                string filename = countstr + ".txt";
                int folderNum = count / 10;
                char[] attr = { 'A', 'B', 'C', 'D', 'E' };
                string savepath = "E:\\" + folderNum.ToString();//每保存10条数据则新建一个文件夹
                if (Directory.Exists(savepath))
                {
                    ;
                }
                else
                {
                    Directory.CreateDirectory(savepath);//
                }
                FileStream myStream = new FileStream(savepath + "\\" + filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                BinaryWriter myWriter = new BinaryWriter(myStream);
                myWriter.Write(attr, 0, attr.Length);
                myWriter.Close();
                myStream.Close();
            }
        }
    }
}
