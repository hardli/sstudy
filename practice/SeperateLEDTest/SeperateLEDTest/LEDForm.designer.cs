namespace SeperateLEDTest
{
    partial class LEDForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.LedSet = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.selectBox = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.firstPart = new System.Windows.Forms.GroupBox();
            this.ledFirstPart = new System.Windows.Forms.RichTextBox();
            this.LastPart = new System.Windows.Forms.GroupBox();
            this.LedLastPart = new System.Windows.Forms.RichTextBox();
            this.GiveUp = new System.Windows.Forms.Button();
            this.Comfirm = new System.Windows.Forms.Button();
            this.LedSet.SuspendLayout();
            this.firstPart.SuspendLayout();
            this.LastPart.SuspendLayout();
            this.SuspendLayout();
            // 
            // LedSet
            // 
            this.LedSet.Controls.Add(this.label1);
            this.LedSet.Controls.Add(this.selectBox);
            this.LedSet.Controls.Add(this.panel1);
            this.LedSet.Location = new System.Drawing.Point(3, 3);
            this.LedSet.Name = "LedSet";
            this.LedSet.Size = new System.Drawing.Size(393, 85);
            this.LedSet.TabIndex = 0;
            this.LedSet.TabStop = false;
            this.LedSet.Text = "LED屏通信方式设置";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "通信方式：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // selectBox
            // 
            this.selectBox.FormattingEnabled = true;
            this.selectBox.Items.AddRange(new object[] {
            "串口",
            "网口"});
            this.selectBox.Location = new System.Drawing.Point(93, 17);
            this.selectBox.Name = "selectBox";
            this.selectBox.Size = new System.Drawing.Size(99, 20);
            this.selectBox.TabIndex = 11;
            this.selectBox.SelectedIndexChanged += new System.EventHandler(this.selectBox_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(7, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(380, 28);
            this.panel1.TabIndex = 2;
            // 
            // firstPart
            // 
            this.firstPart.Controls.Add(this.ledFirstPart);
            this.firstPart.Location = new System.Drawing.Point(3, 94);
            this.firstPart.Name = "firstPart";
            this.firstPart.Size = new System.Drawing.Size(393, 127);
            this.firstPart.TabIndex = 0;
            this.firstPart.TabStop = false;
            this.firstPart.Text = "LED屏协议前部分";
            // 
            // ledFirstPart
            // 
            this.ledFirstPart.Location = new System.Drawing.Point(6, 17);
            this.ledFirstPart.Name = "ledFirstPart";
            this.ledFirstPart.Size = new System.Drawing.Size(381, 104);
            this.ledFirstPart.TabIndex = 0;
            this.ledFirstPart.Text = "";
            // 
            // LastPart
            // 
            this.LastPart.Controls.Add(this.LedLastPart);
            this.LastPart.Location = new System.Drawing.Point(3, 227);
            this.LastPart.Name = "LastPart";
            this.LastPart.Size = new System.Drawing.Size(393, 114);
            this.LastPart.TabIndex = 2;
            this.LastPart.TabStop = false;
            this.LastPart.Text = "LED屏协议后部分";
            // 
            // LedLastPart
            // 
            this.LedLastPart.Location = new System.Drawing.Point(6, 17);
            this.LedLastPart.Name = "LedLastPart";
            this.LedLastPart.Size = new System.Drawing.Size(381, 110);
            this.LedLastPart.TabIndex = 0;
            this.LedLastPart.Text = "";
            // 
            // GiveUp
            // 
            this.GiveUp.Location = new System.Drawing.Point(96, 347);
            this.GiveUp.Name = "GiveUp";
            this.GiveUp.Size = new System.Drawing.Size(75, 23);
            this.GiveUp.TabIndex = 24;
            this.GiveUp.Text = "放弃输入";
            this.GiveUp.UseVisualStyleBackColor = true;
            this.GiveUp.Click += new System.EventHandler(this.GiveUp_Click);
            // 
            // Comfirm
            // 
            this.Comfirm.Location = new System.Drawing.Point(213, 347);
            this.Comfirm.Name = "Comfirm";
            this.Comfirm.Size = new System.Drawing.Size(75, 23);
            this.Comfirm.TabIndex = 25;
            this.Comfirm.Text = "确认输入";
            this.Comfirm.UseVisualStyleBackColor = true;
            this.Comfirm.Click += new System.EventHandler(this.Comfirm_Click);
            // 
            // LEDForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 373);
            this.Controls.Add(this.Comfirm);
            this.Controls.Add(this.GiveUp);
            this.Controls.Add(this.LastPart);
            this.Controls.Add(this.firstPart);
            this.Controls.Add(this.LedSet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LEDForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "LED屏参数设置";
            this.Load += new System.EventHandler(this.LEDForm_Load);
            this.LedSet.ResumeLayout(false);
            this.LedSet.PerformLayout();
            this.firstPart.ResumeLayout(false);
            this.LastPart.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox LedSet;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox firstPart;

        #region 动态控件定义
        private System.Windows.Forms.Label lbl1; //串口
        private System.Windows.Forms.Label lbl2; //波特率
        private System.Windows.Forms.ComboBox cb1; //串口号选项
        private System.Windows.Forms.ComboBox cb2; //波特率值选项

        //通信方式为网口
        private System.Windows.Forms.TextBox NetIp; //IP地址
        private System.Windows.Forms.TextBox NetPort; //端口号

        #endregion
        private System.Windows.Forms.GroupBox LastPart;
        private System.Windows.Forms.RichTextBox ledFirstPart;
        private System.Windows.Forms.RichTextBox LedLastPart;
        private System.Windows.Forms.Button GiveUp;
        private System.Windows.Forms.Button Comfirm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox selectBox;
        private System.Windows.Forms.Button bt1;
        private System.Windows.Forms.Button bt2;

    }
}

