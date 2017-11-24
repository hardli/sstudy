using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace select
{
    class DelegateState
    {
        //提示框信息显示
        public delegate void TipsTextdelegate(string msg, bool bInLog = true, int SpeechOut = 0);

        //TextBox 提示信息
        public delegate void TextBoxdelegate(string msg, int N);
        /// <summary>
        /// 提示框信息显示
        ///</summary>
        public static TipsTextdelegate DelegateTipsText;

        //TextBox消息提示框
        public static TextBoxdelegate DelegateTextBoxOne;
            //TextBox消息提示框
        public static TextBoxdelegate DelegateTextBoxTwo;
        //TextBox消息提示框
        public static TextBoxdelegate DelegateTextBoxThree;
        //TextBox消息提示框
        public static TextBoxdelegate DelegateTextBoxTen;
    }
}
