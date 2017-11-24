namespace SeperateLEDTest
{
    partial class Form1
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
            this.UseLED = new System.Windows.Forms.Button();
            this.configLED = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UseLED
            // 
            this.UseLED.Location = new System.Drawing.Point(54, 137);
            this.UseLED.Name = "UseLED";
            this.UseLED.Size = new System.Drawing.Size(130, 43);
            this.UseLED.TabIndex = 0;
            this.UseLED.Text = "启用LED屏";
            this.UseLED.UseVisualStyleBackColor = true;
            this.UseLED.Click += new System.EventHandler(this.OpenLED_Click);
            // 
            // configLED
            // 
            this.configLED.Location = new System.Drawing.Point(54, 59);
            this.configLED.Name = "configLED";
            this.configLED.Size = new System.Drawing.Size(130, 43);
            this.configLED.TabIndex = 1;
            this.configLED.Text = "设置LED屏参数";
            this.configLED.UseVisualStyleBackColor = true;
            this.configLED.Click += new System.EventHandler(this.configLED_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.configLED);
            this.Controls.Add(this.UseLED);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button UseLED;
        private System.Windows.Forms.Button configLED;
    }
}

