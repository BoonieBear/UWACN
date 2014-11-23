namespace CRCBuilder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.sourcebox = new System.Windows.Forms.TextBox();
            this.databox = new System.Windows.Forms.TextBox();
            this.pastebtn = new System.Windows.Forms.Button();
            this.modecheck = new System.Windows.Forms.CheckBox();
            this.clearbutton = new System.Windows.Forms.Button();
            this.loadbtn = new System.Windows.Forms.Button();
            this.clrbtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.crcbtn = new System.Windows.Forms.Button();
            this.crctext = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.copytobtn = new System.Windows.Forms.Button();
            this.savefile = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // sourcebox
            // 
            this.sourcebox.Location = new System.Drawing.Point(6, 22);
            this.sourcebox.Multiline = true;
            this.sourcebox.Name = "sourcebox";
            this.sourcebox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.sourcebox.Size = new System.Drawing.Size(453, 109);
            this.sourcebox.TabIndex = 0;
            // 
            // databox
            // 
            this.databox.Location = new System.Drawing.Point(6, 20);
            this.databox.Multiline = true;
            this.databox.Name = "databox";
            this.databox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.databox.Size = new System.Drawing.Size(453, 101);
            this.databox.TabIndex = 1;
            // 
            // pastebtn
            // 
            this.pastebtn.Location = new System.Drawing.Point(486, 74);
            this.pastebtn.Name = "pastebtn";
            this.pastebtn.Size = new System.Drawing.Size(92, 23);
            this.pastebtn.TabIndex = 2;
            this.pastebtn.Text = "从剪贴板复制";
            this.pastebtn.UseVisualStyleBackColor = true;
            this.pastebtn.Click += new System.EventHandler(this.pastebtn_Click);
            // 
            // modecheck
            // 
            this.modecheck.AutoSize = true;
            this.modecheck.Location = new System.Drawing.Point(487, 12);
            this.modecheck.Name = "modecheck";
            this.modecheck.Size = new System.Drawing.Size(84, 16);
            this.modecheck.TabIndex = 3;
            this.modecheck.Text = "16进制模式";
            this.modecheck.UseVisualStyleBackColor = true;
            this.modecheck.CheckedChanged += new System.EventHandler(this.modecheck_CheckedChanged);
            // 
            // clearbutton
            // 
            this.clearbutton.Location = new System.Drawing.Point(486, 45);
            this.clearbutton.Name = "clearbutton";
            this.clearbutton.Size = new System.Drawing.Size(92, 23);
            this.clearbutton.TabIndex = 4;
            this.clearbutton.Text = "清除";
            this.clearbutton.UseVisualStyleBackColor = true;
            this.clearbutton.Click += new System.EventHandler(this.clearbutton_Click);
            // 
            // loadbtn
            // 
            this.loadbtn.Location = new System.Drawing.Point(486, 103);
            this.loadbtn.Name = "loadbtn";
            this.loadbtn.Size = new System.Drawing.Size(92, 23);
            this.loadbtn.TabIndex = 5;
            this.loadbtn.Text = "从文件加载";
            this.loadbtn.UseVisualStyleBackColor = true;
            this.loadbtn.Click += new System.EventHandler(this.loadbtn_Click);
            // 
            // clrbtn
            // 
            this.clrbtn.Location = new System.Drawing.Point(486, 194);
            this.clrbtn.Name = "clrbtn";
            this.clrbtn.Size = new System.Drawing.Size(92, 23);
            this.clrbtn.TabIndex = 6;
            this.clrbtn.Text = "清除";
            this.clrbtn.UseVisualStyleBackColor = true;
            this.clrbtn.Click += new System.EventHandler(this.clrbtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.sourcebox);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(465, 140);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "源数据";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.databox);
            this.groupBox2.Location = new System.Drawing.Point(13, 176);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(467, 129);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "校验数据";
            // 
            // crcbtn
            // 
            this.crcbtn.Location = new System.Drawing.Point(229, 156);
            this.crcbtn.Name = "crcbtn";
            this.crcbtn.Size = new System.Drawing.Size(75, 23);
            this.crcbtn.TabIndex = 9;
            this.crcbtn.Text = "计算";
            this.crcbtn.UseVisualStyleBackColor = true;
            this.crcbtn.Click += new System.EventHandler(this.crcbtn_Click);
            // 
            // crctext
            // 
            this.crctext.Location = new System.Drawing.Point(372, 158);
            this.crctext.Name = "crctext";
            this.crctext.Size = new System.Drawing.Size(100, 21);
            this.crctext.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(312, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "CRC";
            // 
            // copytobtn
            // 
            this.copytobtn.Location = new System.Drawing.Point(487, 224);
            this.copytobtn.Name = "copytobtn";
            this.copytobtn.Size = new System.Drawing.Size(91, 23);
            this.copytobtn.TabIndex = 12;
            this.copytobtn.Text = "复制到剪贴板";
            this.copytobtn.UseVisualStyleBackColor = true;
            this.copytobtn.Click += new System.EventHandler(this.copytobtn_Click);
            // 
            // savefile
            // 
            this.savefile.Location = new System.Drawing.Point(487, 254);
            this.savefile.Name = "savefile";
            this.savefile.Size = new System.Drawing.Size(91, 23);
            this.savefile.TabIndex = 13;
            this.savefile.Text = "保存成文件";
            this.savefile.UseVisualStyleBackColor = true;
            this.savefile.Click += new System.EventHandler(this.savefile_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.RestoreDirectory = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 311);
            this.Controls.Add(this.savefile);
            this.Controls.Add(this.copytobtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.crctext);
            this.Controls.Add(this.crcbtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.clrbtn);
            this.Controls.Add(this.loadbtn);
            this.Controls.Add(this.clearbutton);
            this.Controls.Add(this.modecheck);
            this.Controls.Add(this.pastebtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "CRC生成器 -v1.0.2";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox sourcebox;
        private System.Windows.Forms.TextBox databox;
        private System.Windows.Forms.Button pastebtn;
        private System.Windows.Forms.CheckBox modecheck;
        private System.Windows.Forms.Button clearbutton;
        private System.Windows.Forms.Button loadbtn;
        private System.Windows.Forms.Button clrbtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button crcbtn;
        private System.Windows.Forms.TextBox crctext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button copytobtn;
        private System.Windows.Forms.Button savefile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

