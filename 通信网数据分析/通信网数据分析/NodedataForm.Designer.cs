namespace 通信网数据分析
{
    partial class NodedataForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.StatusList = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecvTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CommNodeTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NodeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.v33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.V48 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.v33left = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.V48left = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NodeTemp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.leakAlarm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmitAuto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmitAmp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecvAuto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecvGain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeviceList = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TraceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeviceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TempColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepthColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeviceTimeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.NewWindow = new DevComponents.DotNetBar.ButtonX();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.NodeBox = new System.Windows.Forms.ComboBox();
            this.SaveDeviceLst = new DevComponents.DotNetBar.ButtonX();
            this.SaveStatusLst = new DevComponents.DotNetBar.ButtonX();
            this.FileList = new System.Windows.Forms.DataGridView();
            this.TimeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StatusList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceList)).BeginInit();
            this.groupPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FileList)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.43783F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.08698F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.47519F));
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.StatusList, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.DeviceList, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.FileList, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(886, 461);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(494, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(389, 25);
            this.label6.TabIndex = 9;
            this.label6.Text = "节点状态表";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(157, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(331, 25);
            this.label5.TabIndex = 8;
            this.label5.Text = "设备数据表";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StatusList
            // 
            this.StatusList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.StatusList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.RecvTime,
            this.CommNodeTime,
            this.trace,
            this.NodeType,
            this.Status,
            this.v33,
            this.V48,
            this.v33left,
            this.V48left,
            this.NodeTemp,
            this.leakAlarm,
            this.EmitAuto,
            this.EmitAmp,
            this.RecvAuto,
            this.RecvGain});
            this.StatusList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StatusList.Location = new System.Drawing.Point(494, 108);
            this.StatusList.Name = "StatusList";
            this.StatusList.ReadOnly = true;
            this.StatusList.RowHeadersVisible = false;
            this.StatusList.RowTemplate.Height = 23;
            this.StatusList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.StatusList.ShowEditingIcon = false;
            this.StatusList.Size = new System.Drawing.Size(389, 350);
            this.StatusList.TabIndex = 6;
            // 
            // ID
            // 
            this.ID.FillWeight = 50F;
            this.ID.HeaderText = "源节点ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 78;
            // 
            // RecvTime
            // 
            this.RecvTime.FillWeight = 150F;
            this.RecvTime.HeaderText = "接收时间";
            this.RecvTime.Name = "RecvTime";
            this.RecvTime.ReadOnly = true;
            this.RecvTime.Width = 78;
            // 
            // CommNodeTime
            // 
            this.CommNodeTime.HeaderText = "节点时间";
            this.CommNodeTime.Name = "CommNodeTime";
            this.CommNodeTime.ReadOnly = true;
            this.CommNodeTime.Width = 78;
            // 
            // trace
            // 
            this.trace.HeaderText = "路径记录";
            this.trace.Name = "trace";
            this.trace.ReadOnly = true;
            this.trace.Width = 78;
            // 
            // NodeType
            // 
            this.NodeType.HeaderText = "节点类型";
            this.NodeType.Name = "NodeType";
            this.NodeType.ReadOnly = true;
            this.NodeType.Width = 78;
            // 
            // Status
            // 
            this.Status.HeaderText = "状态数据(Hex)";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 108;
            // 
            // v33
            // 
            this.v33.HeaderText = "3.3V电压";
            this.v33.Name = "v33";
            this.v33.ReadOnly = true;
            this.v33.Width = 78;
            // 
            // V48
            // 
            this.V48.HeaderText = "48V电压";
            this.V48.Name = "V48";
            this.V48.ReadOnly = true;
            this.V48.Width = 72;
            // 
            // v33left
            // 
            this.v33left.HeaderText = "3.3V剩余电量";
            this.v33left.Name = "v33left";
            this.v33left.ReadOnly = true;
            this.v33left.Width = 102;
            // 
            // V48left
            // 
            this.V48left.HeaderText = "48V剩余电量";
            this.V48left.Name = "V48left";
            this.V48left.ReadOnly = true;
            this.V48left.Width = 96;
            // 
            // NodeTemp
            // 
            this.NodeTemp.HeaderText = "节点温度";
            this.NodeTemp.Name = "NodeTemp";
            this.NodeTemp.ReadOnly = true;
            this.NodeTemp.Width = 78;
            // 
            // leakAlarm
            // 
            this.leakAlarm.HeaderText = "漏水报警";
            this.leakAlarm.Name = "leakAlarm";
            this.leakAlarm.ReadOnly = true;
            this.leakAlarm.Width = 78;
            // 
            // EmitAuto
            // 
            this.EmitAuto.HeaderText = "发射自动调节开关";
            this.EmitAuto.Name = "EmitAuto";
            this.EmitAuto.ReadOnly = true;
            this.EmitAuto.Width = 126;
            // 
            // EmitAmp
            // 
            this.EmitAmp.HeaderText = "发射幅度设置";
            this.EmitAmp.Name = "EmitAmp";
            this.EmitAmp.ReadOnly = true;
            this.EmitAmp.Width = 102;
            // 
            // RecvAuto
            // 
            this.RecvAuto.HeaderText = "接收自动调节开关";
            this.RecvAuto.Name = "RecvAuto";
            this.RecvAuto.ReadOnly = true;
            this.RecvAuto.Width = 126;
            // 
            // RecvGain
            // 
            this.RecvGain.HeaderText = "接收增益设置";
            this.RecvGain.Name = "RecvGain";
            this.RecvGain.ReadOnly = true;
            this.RecvGain.Width = 102;
            // 
            // DeviceList
            // 
            this.DeviceList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DeviceList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.COM,
            this.dataGridViewTextBoxColumn2,
            this.TraceColumn,
            this.DeviceColumn,
            this.TempColumn,
            this.DepthColumn,
            this.DeviceTimeColumn});
            this.DeviceList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeviceList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DeviceList.Location = new System.Drawing.Point(157, 108);
            this.DeviceList.Name = "DeviceList";
            this.DeviceList.ReadOnly = true;
            this.DeviceList.RowHeadersVisible = false;
            this.DeviceList.RowTemplate.Height = 23;
            this.DeviceList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DeviceList.ShowEditingIcon = false;
            this.DeviceList.Size = new System.Drawing.Size(331, 350);
            this.DeviceList.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn1.FillWeight = 50F;
            this.dataGridViewTextBoxColumn1.HeaderText = "源节点";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 66;
            // 
            // COM
            // 
            this.COM.HeaderText = "端口号";
            this.COM.Name = "COM";
            this.COM.ReadOnly = true;
            this.COM.Width = 66;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "接收时间";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 78;
            // 
            // TraceColumn
            // 
            this.TraceColumn.HeaderText = "路径记录";
            this.TraceColumn.Name = "TraceColumn";
            this.TraceColumn.ReadOnly = true;
            this.TraceColumn.Width = 78;
            // 
            // DeviceColumn
            // 
            this.DeviceColumn.HeaderText = "设备类型";
            this.DeviceColumn.Name = "DeviceColumn";
            this.DeviceColumn.ReadOnly = true;
            this.DeviceColumn.Width = 78;
            // 
            // TempColumn
            // 
            this.TempColumn.HeaderText = "温度";
            this.TempColumn.Name = "TempColumn";
            this.TempColumn.ReadOnly = true;
            this.TempColumn.Width = 54;
            // 
            // DepthColumn
            // 
            this.DepthColumn.HeaderText = "深度";
            this.DepthColumn.Name = "DepthColumn";
            this.DepthColumn.ReadOnly = true;
            this.DepthColumn.Width = 54;
            // 
            // DeviceTimeColumn
            // 
            this.DeviceTimeColumn.HeaderText = "采集时间";
            this.DeviceTimeColumn.Name = "DeviceTimeColumn";
            this.DeviceTimeColumn.ReadOnly = true;
            this.DeviceTimeColumn.Width = 78;
            // 
            // groupPanel1
            // 
            this.groupPanel1.AutoSize = true;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.tableLayoutPanel1.SetColumnSpan(this.groupPanel1, 3);
            this.groupPanel1.Controls.Add(this.tableLayoutPanel2);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.Location = new System.Drawing.Point(3, 3);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(880, 74);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.groupPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.NewWindow, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.dateTimePicker2, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.dateTimePicker1, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.NodeBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.SaveDeviceLst, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.SaveStatusLst, 5, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(874, 68);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(257, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 26);
            this.label3.TabIndex = 12;
            this.label3.Text = "数据结束时间";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NewWindow
            // 
            this.NewWindow.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.NewWindow.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NewWindow.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.NewWindow.Location = new System.Drawing.Point(289, 3);
            this.NewWindow.Name = "NewWindow";
            this.NewWindow.Size = new System.Drawing.Size(84, 20);
            this.NewWindow.TabIndex = 13;
            this.NewWindow.Text = "新建窗口";
            this.NewWindow.Tooltip = "新建一个当前数据窗口";
            this.NewWindow.Click += new System.EventHandler(this.NewWindow_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 26);
            this.label2.TabIndex = 11;
            this.label2.Text = "数据开始时间";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(411, 29);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(139, 21);
            this.dateTimePicker2.TabIndex = 15;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(103, 29);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(148, 21);
            this.dateTimePicker1.TabIndex = 14;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 26);
            this.label1.TabIndex = 16;
            this.label1.Text = "选择源节点";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NodeBox
            // 
            this.NodeBox.FormattingEnabled = true;
            this.NodeBox.Location = new System.Drawing.Point(103, 3);
            this.NodeBox.Name = "NodeBox";
            this.NodeBox.Size = new System.Drawing.Size(121, 20);
            this.NodeBox.Sorted = true;
            this.NodeBox.TabIndex = 17;
            this.NodeBox.TextChanged += new System.EventHandler(this.NodeBox_TextChanged);
            // 
            // SaveDeviceLst
            // 
            this.SaveDeviceLst.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.SaveDeviceLst.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SaveDeviceLst.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.SaveDeviceLst.Location = new System.Drawing.Point(597, 29);
            this.SaveDeviceLst.Name = "SaveDeviceLst";
            this.SaveDeviceLst.Size = new System.Drawing.Size(83, 20);
            this.SaveDeviceLst.TabIndex = 19;
            this.SaveDeviceLst.Text = "导出设备数据";
            this.SaveDeviceLst.Tooltip = "新建一个当前数据窗口";
            this.SaveDeviceLst.Click += new System.EventHandler(this.SaveDeviceLst_Click);
            // 
            // SaveStatusLst
            // 
            this.SaveStatusLst.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.SaveStatusLst.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SaveStatusLst.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.SaveStatusLst.Location = new System.Drawing.Point(753, 29);
            this.SaveStatusLst.Name = "SaveStatusLst";
            this.SaveStatusLst.Size = new System.Drawing.Size(83, 20);
            this.SaveStatusLst.TabIndex = 18;
            this.SaveStatusLst.Text = "导出节点状态";
            this.SaveStatusLst.Tooltip = "新建一个当前数据窗口";
            this.SaveStatusLst.Click += new System.EventHandler(this.SaveStatusLst_Click);
            // 
            // FileList
            // 
            this.FileList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.FileList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TimeColumn});
            this.FileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.FileList.Location = new System.Drawing.Point(3, 108);
            this.FileList.Name = "FileList";
            this.FileList.ReadOnly = true;
            this.FileList.RowHeadersVisible = false;
            this.FileList.RowTemplate.Height = 23;
            this.FileList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.FileList.ShowEditingIcon = false;
            this.FileList.Size = new System.Drawing.Size(148, 350);
            this.FileList.TabIndex = 4;
            this.FileList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.FileList_CellDoubleClick);
            // 
            // TimeColumn
            // 
            this.TimeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TimeColumn.HeaderText = "接收时间";
            this.TimeColumn.Name = "TimeColumn";
            this.TimeColumn.ReadOnly = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 25);
            this.label4.TabIndex = 7;
            this.label4.Text = "文件列表";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NodedataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 461);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "NodedataForm";
            this.Text = "数据集合查看";
            this.Load += new System.EventHandler(this.NodedataForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StatusList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceList)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FileList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private System.Windows.Forms.DataGridView StatusList;
        private System.Windows.Forms.DataGridView DeviceList;
        private System.Windows.Forms.DataGridView FileList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private DevComponents.DotNetBar.ButtonX NewWindow;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox NodeBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeColumn;
        private DevComponents.DotNetBar.ButtonX SaveDeviceLst;
        private DevComponents.DotNetBar.ButtonX SaveStatusLst;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn COM;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn TraceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeviceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TempColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepthColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeviceTimeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecvTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn CommNodeTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn trace;
        private System.Windows.Forms.DataGridViewTextBoxColumn NodeType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn v33;
        private System.Windows.Forms.DataGridViewTextBoxColumn V48;
        private System.Windows.Forms.DataGridViewTextBoxColumn v33left;
        private System.Windows.Forms.DataGridViewTextBoxColumn V48left;
        private System.Windows.Forms.DataGridViewTextBoxColumn NodeTemp;
        private System.Windows.Forms.DataGridViewTextBoxColumn leakAlarm;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmitAuto;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmitAmp;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecvAuto;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecvGain;

    }
}