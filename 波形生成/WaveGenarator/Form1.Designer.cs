namespace WaveGenarator
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.skinEngine = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.Connbtn = new System.Windows.Forms.Button();
            this.NodeLinker = new System.ComponentModel.BackgroundWorker();
            this.IpAddrBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.testbtn = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.filepathbox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.ampBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lenBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.sendbtn = new System.Windows.Forms.Button();
            this.Sendbutton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.AmpValidate = new System.Windows.Forms.ErrorProvider(this.components);
            this.NetSender = new System.ComponentModel.BackgroundWorker();
            this.DloadFPGABtn = new System.Windows.Forms.Button();
            this.FPGA_filename = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AmpValidate)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // skinEngine
            // 
            this.skinEngine.SerialNumber = "";
            this.skinEngine.SkinFile = "F:\\学习\\计算机语言\\C#.net\\C#\\C#界面皮肤源码（带大量皮肤素材）\\皮肤\\Vista1\\vista1_green.ssk";
            this.skinEngine.SkinStreamMain = ((System.IO.Stream)(resources.GetObject("skinEngine.SkinStreamMain")));
            // 
            // Connbtn
            // 
            this.Connbtn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Connbtn.Location = new System.Drawing.Point(12, 9);
            this.Connbtn.Name = "Connbtn";
            this.Connbtn.Size = new System.Drawing.Size(75, 22);
            this.Connbtn.TabIndex = 1;
            this.Connbtn.Text = "连接";
            this.Connbtn.UseVisualStyleBackColor = true;
            this.Connbtn.Click += new System.EventHandler(this.ConnectNode);
            // 
            // NodeLinker
            // 
            this.NodeLinker.WorkerSupportsCancellation = true;
            this.NodeLinker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.NodeLinker_DoWork);
            this.NodeLinker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.NodeLinker_RunWorkerCompleted);
            // 
            // IpAddrBox
            // 
            this.IpAddrBox.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IpAddrBox.Location = new System.Drawing.Point(97, 8);
            this.IpAddrBox.Name = "IpAddrBox";
            this.IpAddrBox.Size = new System.Drawing.Size(118, 23);
            this.IpAddrBox.TabIndex = 3;
            this.IpAddrBox.Text = "192.168.2.250";
            this.IpAddrBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.AmpValidate.SetIconAlignment(this.groupBox1, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.groupBox1.Location = new System.Drawing.Point(11, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(507, 222);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "波形设置";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.radioButton1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBox1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.radioButton2, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboBox2, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 4, 5);
            this.tableLayoutPanel1.Controls.Add(this.testbtn, 6, 11);
            this.tableLayoutPanel1.Controls.Add(this.textBox2, 5, 5);
            this.tableLayoutPanel1.Controls.Add(this.label4, 4, 6);
            this.tableLayoutPanel1.Controls.Add(this.textBox4, 5, 6);
            this.tableLayoutPanel1.Controls.Add(this.label11, 6, 5);
            this.tableLayoutPanel1.Controls.Add(this.label12, 6, 6);
            this.tableLayoutPanel1.Controls.Add(this.radioButton3, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.filepathbox, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.label8, 4, 8);
            this.tableLayoutPanel1.Controls.Add(this.textBox6, 5, 8);
            this.tableLayoutPanel1.Controls.Add(this.label9, 6, 8);
            this.tableLayoutPanel1.Controls.Add(this.button1, 6, 10);
            this.tableLayoutPanel1.Controls.Add(this.label13, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.label14, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.ampBox, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.textBox3, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label10, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.label15, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.lenBox, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.label16, 2, 11);
            this.tableLayoutPanel1.Controls.Add(this.sendbtn, 4, 11);
            this.tableLayoutPanel1.Controls.Add(this.Sendbutton, 5, 11);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 20);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 12;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(487, 197);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(3, 3);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(71, 16);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.Text = "单频信号";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "100000",
            "80000"});
            this.comboBox1.Location = new System.Drawing.Point(80, 25);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(63, 20);
            this.comboBox1.Sorted = true;
            this.comboBox1.TabIndex = 3;
            this.comboBox1.Text = "120000";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "采样率";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(241, 3);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(71, 16);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "扫频信号";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(275, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "采样率";
            // 
            // comboBox2
            // 
            this.comboBox2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "100000",
            "80000"});
            this.comboBox2.Location = new System.Drawing.Point(322, 25);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(63, 20);
            this.comboBox2.Sorted = true;
            this.comboBox2.TabIndex = 4;
            this.comboBox2.Text = "120000";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(263, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "起始频率";
            // 
            // testbtn
            // 
            this.testbtn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.testbtn.Location = new System.Drawing.Point(403, 165);
            this.testbtn.Name = "testbtn";
            this.testbtn.Size = new System.Drawing.Size(75, 23);
            this.testbtn.TabIndex = 7;
            this.testbtn.Text = "保存文件";
            this.testbtn.UseMnemonic = false;
            this.testbtn.UseVisualStyleBackColor = true;
            this.testbtn.Click += new System.EventHandler(this.testbtn_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(322, 51);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(63, 21);
            this.textBox2.TabIndex = 13;
            this.textBox2.Text = "8500";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(263, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "截止频率";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(322, 78);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(63, 21);
            this.textBox4.TabIndex = 15;
            this.textBox4.Text = "12500";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(403, 55);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 22;
            this.label11.Text = "赫兹";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(403, 82);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 12);
            this.label12.TabIndex = 23;
            this.label12.Text = "赫兹";
            // 
            // radioButton3
            // 
            this.radioButton3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(3, 135);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(71, 16);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.Text = "预存波形";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // filepathbox
            // 
            this.filepathbox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel1.SetColumnSpan(this.filepathbox, 4);
            this.filepathbox.Location = new System.Drawing.Point(80, 132);
            this.filepathbox.Name = "filepathbox";
            this.filepathbox.Size = new System.Drawing.Size(236, 21);
            this.filepathbox.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(287, 109);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "长度";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(322, 105);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(63, 21);
            this.textBox6.TabIndex = 18;
            this.textBox6.Text = "300";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(403, 109);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 20;
            this.label9.Text = "毫秒";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(403, 132);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(37, 22);
            this.button1.TabIndex = 19;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(403, 29);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 12);
            this.label13.TabIndex = 26;
            this.label13.Text = "赫兹";
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(21, 171);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 27;
            this.label14.Text = "发射幅度";
            // 
            // ampBox
            // 
            this.ampBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ampBox.Location = new System.Drawing.Point(80, 166);
            this.ampBox.Name = "ampBox";
            this.ampBox.Size = new System.Drawing.Size(75, 21);
            this.ampBox.TabIndex = 28;
            this.ampBox.Text = "1";
            this.ampBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(45, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "频率";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(80, 51);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(63, 21);
            this.textBox3.TabIndex = 14;
            this.textBox3.Text = "10000";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(161, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 21;
            this.label10.Text = "赫兹";
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(45, 82);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 12);
            this.label15.TabIndex = 29;
            this.label15.Text = "长度";
            // 
            // lenBox
            // 
            this.lenBox.Location = new System.Drawing.Point(80, 78);
            this.lenBox.Name = "lenBox";
            this.lenBox.Size = new System.Drawing.Size(63, 21);
            this.lenBox.TabIndex = 30;
            this.lenBox.Text = "1000";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(161, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 31;
            this.label5.Text = "毫秒";
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(161, 171);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 12);
            this.label16.TabIndex = 32;
            this.label16.Text = "(0-1]";
            // 
            // sendbtn
            // 
            this.sendbtn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.sendbtn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sendbtn.Location = new System.Drawing.Point(241, 165);
            this.sendbtn.Name = "sendbtn";
            this.sendbtn.Size = new System.Drawing.Size(75, 23);
            this.sendbtn.TabIndex = 6;
            this.sendbtn.Text = "加载数据";
            this.sendbtn.UseVisualStyleBackColor = true;
            this.sendbtn.Click += new System.EventHandler(this.sendbtn_Click);
            // 
            // Sendbutton
            // 
            this.Sendbutton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Sendbutton.BackColor = System.Drawing.Color.Transparent;
            this.Sendbutton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Sendbutton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Sendbutton.Location = new System.Drawing.Point(322, 165);
            this.Sendbutton.Name = "Sendbutton";
            this.Sendbutton.Size = new System.Drawing.Size(75, 23);
            this.Sendbutton.TabIndex = 6;
            this.Sendbutton.Text = "发射";
            this.Sendbutton.UseVisualStyleBackColor = false;
            this.Sendbutton.Click += new System.EventHandler(this.Sendbutton_Click);
            // 
            // AmpValidate
            // 
            this.AmpValidate.ContainerControl = this;
            // 
            // NetSender
            // 
            this.NetSender.WorkerReportsProgress = true;
            this.NetSender.WorkerSupportsCancellation = true;
            this.NetSender.DoWork += new System.ComponentModel.DoWorkEventHandler(this.NetSender_DoWork);
            this.NetSender.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.NetSender_RunWorkerCompleted);
            this.NetSender.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.NetSender_ProgressChanged);
            // 
            // DloadFPGABtn
            // 
            this.DloadFPGABtn.Location = new System.Drawing.Point(12, 37);
            this.DloadFPGABtn.Name = "DloadFPGABtn";
            this.DloadFPGABtn.Size = new System.Drawing.Size(75, 23);
            this.DloadFPGABtn.TabIndex = 7;
            this.DloadFPGABtn.Text = "加载FPGA";
            this.DloadFPGABtn.UseVisualStyleBackColor = true;
            this.DloadFPGABtn.Click += new System.EventHandler(this.DloadFPGABtn_Click);
            // 
            // FPGA_filename
            // 
            this.FPGA_filename.AutoSize = true;
            this.FPGA_filename.Location = new System.Drawing.Point(95, 42);
            this.FPGA_filename.Name = "FPGA_filename";
            this.FPGA_filename.Size = new System.Drawing.Size(65, 12);
            this.FPGA_filename.TabIndex = 8;
            this.FPGA_filename.Text = "未加载FPGA";
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = false;
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(350, 17);
            this.StatusLabel.Text = "未连接节点";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolProgressBar
            // 
            this.toolProgressBar.Name = "toolProgressBar";
            this.toolProgressBar.Size = new System.Drawing.Size(160, 16);
            // 
            // statusStrip
            // 
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.toolProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 297);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(529, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 319);
            this.Controls.Add(this.FPGA_filename);
            this.Controls.Add(this.DloadFPGABtn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.IpAddrBox);
            this.Controls.Add(this.Connbtn);
            this.Controls.Add(this.statusStrip);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "波形发射v1.4";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AmpValidate)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Sunisoft.IrisSkin.SkinEngine skinEngine;
        private System.Windows.Forms.Button Connbtn;
        private System.ComponentModel.BackgroundWorker NodeLinker;
        private System.Windows.Forms.TextBox IpAddrBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox filepathbox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button sendbtn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button testbtn;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox ampBox;
        private System.Windows.Forms.ErrorProvider AmpValidate;
        private System.ComponentModel.BackgroundWorker NetSender;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox lenBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Sendbutton;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button DloadFPGABtn;
        private System.Windows.Forms.Label FPGA_filename;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolProgressBar;
    }
}

