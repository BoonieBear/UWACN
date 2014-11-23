namespace webnode.Forms
{
    partial class SetRouteForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetRouteForm));
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.AddRoute = new DevComponents.DotNetBar.ButtonX();
            this.NetGrid = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.AddNeiborToList = new DevComponents.DotNetBar.ButtonX();
            this.CancelBtn = new DevComponents.DotNetBar.ButtonX();
            this.AddRouteToList = new DevComponents.DotNetBar.ButtonX();
            this.ShowRouteAni = new System.Windows.Forms.CheckBox();
            this.NeiborNodeLst = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.SourceNode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NeiborNode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Distance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.RouteGrid = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.DestColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NextHopColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HopsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Serial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.AddAllToCmd = new DevComponents.DotNetBar.ButtonX();
            this.NodeBox = new System.Windows.Forms.ComboBox();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.ClearNetBtn = new DevComponents.DotNetBar.ButtonX();
            this.BuildRouteBtn = new DevComponents.DotNetBar.ButtonX();
            this.Reload = new DevComponents.DotNetBar.ButtonX();
            this.SaveBtn = new DevComponents.DotNetBar.ButtonX();
            this.routemap = new webnode.MapCustom.Map();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NetGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NeiborNodeLst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RouteGrid)).BeginInit();
            this.groupPanel1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.groupPanel2.Controls.Add(this.AddRoute);
            this.groupPanel2.Controls.Add(this.NetGrid);
            this.groupPanel2.Controls.Add(this.AddNeiborToList);
            this.groupPanel2.Controls.Add(this.CancelBtn);
            this.groupPanel2.Controls.Add(this.AddRouteToList);
            this.groupPanel2.Controls.Add(this.ShowRouteAni);
            this.groupPanel2.Controls.Add(this.NeiborNodeLst);
            this.groupPanel2.Controls.Add(this.RouteGrid);
            this.groupPanel2.Controls.Add(this.label1);
            this.groupPanel2.Controls.Add(this.AddAllToCmd);
            this.groupPanel2.Controls.Add(this.NodeBox);
            this.groupPanel2.DrawTitleBox = false;
            this.groupPanel2.IsShadowEnabled = true;
            this.groupPanel2.Location = new System.Drawing.Point(872, 2);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(312, 577);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.groupPanel2.TabIndex = 6;
            this.groupPanel2.Text = "路由/邻节点/网络";
            // 
            // AddRoute
            // 
            this.AddRoute.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.AddRoute.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.AddRoute.Location = new System.Drawing.Point(105, 528);
            this.AddRoute.Name = "AddRoute";
            this.AddRoute.Size = new System.Drawing.Size(96, 25);
            this.AddRoute.TabIndex = 17;
            this.AddRoute.Text = "添加全路由";
            this.AddRoute.Tooltip = "将路由表加入命令列表";
            this.AddRoute.Click += new System.EventHandler(this.AddRoute_Click);
            // 
            // NetGrid
            // 
            this.NetGrid.AllowUserToAddRows = false;
            this.NetGrid.AllowUserToDeleteRows = false;
            this.NetGrid.AllowUserToResizeColumns = false;
            this.NetGrid.AllowUserToResizeRows = false;
            this.NetGrid.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.NetGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.NetGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.NetGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewComboBoxColumn1});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.NetGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.NetGrid.EnableHeadersVisualStyles = false;
            this.NetGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.NetGrid.Location = new System.Drawing.Point(3, 357);
            this.NetGrid.Name = "NetGrid";
            this.NetGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.NetGrid.RowHeadersVisible = false;
            this.NetGrid.RowTemplate.Height = 23;
            this.NetGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NetGrid.SelectAllSignVisible = false;
            this.NetGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.NetGrid.ShowCellErrors = false;
            this.NetGrid.ShowCellToolTips = false;
            this.NetGrid.ShowEditingIcon = false;
            this.NetGrid.ShowRowErrors = false;
            this.NetGrid.Size = new System.Drawing.Size(300, 134);
            this.NetGrid.TabIndex = 16;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.FillWeight = 80F;
            this.dataGridViewTextBoxColumn1.HeaderText = "源节点";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.FillWeight = 80F;
            this.dataGridViewTextBoxColumn2.HeaderText = "邻节点";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.HeaderText = "距离(米)";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewComboBoxColumn1
            // 
            this.dataGridViewComboBoxColumn1.HeaderText = "评价";
            this.dataGridViewComboBoxColumn1.Items.AddRange(new object[] {
            "良好",
            "较好",
            "较差",
            "恶劣"});
            this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
            // 
            // AddNeiborToList
            // 
            this.AddNeiborToList.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.AddNeiborToList.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.AddNeiborToList.Location = new System.Drawing.Point(207, 326);
            this.AddNeiborToList.Name = "AddNeiborToList";
            this.AddNeiborToList.Size = new System.Drawing.Size(96, 25);
            this.AddNeiborToList.TabIndex = 15;
            this.AddNeiborToList.Text = "添加全邻节点表";
            this.AddNeiborToList.Tooltip = "将全节点邻接表加入命令列表";
            this.AddNeiborToList.Click += new System.EventHandler(this.AddNeiborToList_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.CancelBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(207, 528);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(96, 25);
            this.CancelBtn.TabIndex = 14;
            this.CancelBtn.Text = "完成";
            this.CancelBtn.Tooltip = "离开路由设置页面";
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // AddRouteToList
            // 
            this.AddRouteToList.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.AddRouteToList.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.AddRouteToList.Location = new System.Drawing.Point(207, 176);
            this.AddRouteToList.Name = "AddRouteToList";
            this.AddRouteToList.Size = new System.Drawing.Size(96, 25);
            this.AddRouteToList.TabIndex = 14;
            this.AddRouteToList.Text = "添加单点路由";
            this.AddRouteToList.Tooltip = "将单节点路由表加入命令列表";
            this.AddRouteToList.Click += new System.EventHandler(this.AddRouteToList_Click);
            // 
            // ShowRouteAni
            // 
            this.ShowRouteAni.AutoSize = true;
            this.ShowRouteAni.Location = new System.Drawing.Point(5, 176);
            this.ShowRouteAni.Name = "ShowRouteAni";
            this.ShowRouteAni.Size = new System.Drawing.Size(96, 16);
            this.ShowRouteAni.TabIndex = 4;
            this.ShowRouteAni.Text = "显示路由动画";
            this.ShowRouteAni.UseVisualStyleBackColor = true;
            this.ShowRouteAni.CheckedChanged += new System.EventHandler(this.ShowRouteAni_CheckedChanged);
            // 
            // NeiborNodeLst
            // 
            this.NeiborNodeLst.AllowUserToAddRows = false;
            this.NeiborNodeLst.AllowUserToDeleteRows = false;
            this.NeiborNodeLst.AllowUserToResizeColumns = false;
            this.NeiborNodeLst.AllowUserToResizeRows = false;
            this.NeiborNodeLst.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.NeiborNodeLst.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.NeiborNodeLst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.NeiborNodeLst.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SourceNode,
            this.NeiborNode,
            this.Distance,
            this.Value});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.NeiborNodeLst.DefaultCellStyle = dataGridViewCellStyle2;
            this.NeiborNodeLst.EnableHeadersVisualStyles = false;
            this.NeiborNodeLst.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.NeiborNodeLst.Location = new System.Drawing.Point(3, 205);
            this.NeiborNodeLst.Name = "NeiborNodeLst";
            this.NeiborNodeLst.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.NeiborNodeLst.RowHeadersVisible = false;
            this.NeiborNodeLst.RowTemplate.Height = 23;
            this.NeiborNodeLst.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NeiborNodeLst.SelectAllSignVisible = false;
            this.NeiborNodeLst.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.NeiborNodeLst.ShowCellErrors = false;
            this.NeiborNodeLst.ShowCellToolTips = false;
            this.NeiborNodeLst.ShowEditingIcon = false;
            this.NeiborNodeLst.ShowRowErrors = false;
            this.NeiborNodeLst.Size = new System.Drawing.Size(300, 115);
            this.NeiborNodeLst.TabIndex = 3;
            // 
            // SourceNode
            // 
            this.SourceNode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SourceNode.FillWeight = 80F;
            this.SourceNode.HeaderText = "源节点";
            this.SourceNode.Name = "SourceNode";
            this.SourceNode.ReadOnly = true;
            // 
            // NeiborNode
            // 
            this.NeiborNode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NeiborNode.FillWeight = 80F;
            this.NeiborNode.HeaderText = "邻节点";
            this.NeiborNode.Name = "NeiborNode";
            this.NeiborNode.ReadOnly = true;
            this.NeiborNode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Distance
            // 
            this.Distance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Distance.HeaderText = "距离(米)";
            this.Distance.Name = "Distance";
            this.Distance.ReadOnly = true;
            this.Distance.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Value
            // 
            this.Value.HeaderText = "评价";
            this.Value.Items.AddRange(new object[] {
            "良好",
            "较好",
            "较差",
            "恶劣"});
            this.Value.Name = "Value";
            // 
            // RouteGrid
            // 
            this.RouteGrid.AllowUserToAddRows = false;
            this.RouteGrid.AllowUserToDeleteRows = false;
            this.RouteGrid.AllowUserToResizeColumns = false;
            this.RouteGrid.AllowUserToResizeRows = false;
            this.RouteGrid.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.RouteGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.RouteGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DestColumn,
            this.NextHopColumn,
            this.HopsColumn,
            this.Serial,
            this.Status});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.RouteGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.RouteGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.RouteGrid.Location = new System.Drawing.Point(3, 32);
            this.RouteGrid.Name = "RouteGrid";
            this.RouteGrid.RowHeadersVisible = false;
            this.RouteGrid.RowTemplate.Height = 23;
            this.RouteGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.RouteGrid.SelectAllSignVisible = false;
            this.RouteGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RouteGrid.ShowCellErrors = false;
            this.RouteGrid.ShowCellToolTips = false;
            this.RouteGrid.ShowEditingIcon = false;
            this.RouteGrid.ShowRowErrors = false;
            this.RouteGrid.Size = new System.Drawing.Size(300, 138);
            this.RouteGrid.TabIndex = 0;
            this.RouteGrid.SelectionChanged += new System.EventHandler(this.RouteGrid_SelectionChanged);
            this.RouteGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RouteGrid_KeyDown);
            // 
            // DestColumn
            // 
            this.DestColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DestColumn.FillWeight = 70F;
            this.DestColumn.HeaderText = "目标";
            this.DestColumn.Name = "DestColumn";
            this.DestColumn.ReadOnly = true;
            this.DestColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // NextHopColumn
            // 
            this.NextHopColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NextHopColumn.FillWeight = 90F;
            this.NextHopColumn.HeaderText = "下一跳";
            this.NextHopColumn.Name = "NextHopColumn";
            this.NextHopColumn.ReadOnly = true;
            this.NextHopColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // HopsColumn
            // 
            this.HopsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.HopsColumn.FillWeight = 70F;
            this.HopsColumn.HeaderText = "跳数";
            this.HopsColumn.Name = "HopsColumn";
            this.HopsColumn.ReadOnly = true;
            this.HopsColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Serial
            // 
            this.Serial.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Serial.FillWeight = 95F;
            this.Serial.HeaderText = "序列号";
            this.Serial.Name = "Serial";
            // 
            // Status
            // 
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Status.DropDownWidth = 100;
            this.Status.FillWeight = 130F;
            this.Status.HeaderText = "状态";
            this.Status.Items.AddRange(new object[] {
            "路由有效",
            "路由无效",
            "路由正在修复",
            "非法状态标志"});
            this.Status.Name = "Status";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "目标节点";
            // 
            // AddAllToCmd
            // 
            this.AddAllToCmd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.AddAllToCmd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.AddAllToCmd.Location = new System.Drawing.Point(207, 497);
            this.AddAllToCmd.Name = "AddAllToCmd";
            this.AddAllToCmd.Size = new System.Drawing.Size(96, 25);
            this.AddAllToCmd.TabIndex = 13;
            this.AddAllToCmd.Text = "添加网络表";
            this.AddAllToCmd.Tooltip = "将网络表加入命令列表";
            this.AddAllToCmd.Click += new System.EventHandler(this.AddAllToCmd_Click);
            // 
            // NodeBox
            // 
            this.NodeBox.FormattingEnabled = true;
            this.NodeBox.Location = new System.Drawing.Point(70, 3);
            this.NodeBox.Name = "NodeBox";
            this.NodeBox.Size = new System.Drawing.Size(104, 20);
            this.NodeBox.Sorted = true;
            this.NodeBox.TabIndex = 1;
            this.NodeBox.SelectionChangeCommitted += new System.EventHandler(this.NodeBox_SelectionChangeCommitted);
            // 
            // groupPanel1
            // 
            this.groupPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupPanel1.AutoSize = true;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.groupPanel1.Controls.Add(this.ClearNetBtn);
            this.groupPanel1.Controls.Add(this.BuildRouteBtn);
            this.groupPanel1.Controls.Add(this.Reload);
            this.groupPanel1.Controls.Add(this.SaveBtn);
            this.groupPanel1.Controls.Add(this.routemap);
            this.groupPanel1.DrawTitleBox = false;
            this.groupPanel1.Location = new System.Drawing.Point(12, 2);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(857, 577);
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
            this.groupPanel1.TabIndex = 7;
            this.groupPanel1.Text = "网络拓扑设置";
            // 
            // ClearNetBtn
            // 
            this.ClearNetBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ClearNetBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ClearNetBtn.Location = new System.Drawing.Point(775, 446);
            this.ClearNetBtn.Name = "ClearNetBtn";
            this.ClearNetBtn.Size = new System.Drawing.Size(75, 25);
            this.ClearNetBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.ClearNetBtn.TabIndex = 17;
            this.ClearNetBtn.Text = "清空配置";
            this.ClearNetBtn.Tooltip = "重新生成路由表";
            this.ClearNetBtn.Click += new System.EventHandler(this.ClearNetBtn_Click);
            // 
            // BuildRouteBtn
            // 
            this.BuildRouteBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BuildRouteBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BuildRouteBtn.Location = new System.Drawing.Point(775, 474);
            this.BuildRouteBtn.Name = "BuildRouteBtn";
            this.BuildRouteBtn.Size = new System.Drawing.Size(75, 25);
            this.BuildRouteBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.BuildRouteBtn.TabIndex = 16;
            this.BuildRouteBtn.Text = "更新路由";
            this.BuildRouteBtn.Tooltip = "重新生成路由表";
            this.BuildRouteBtn.Click += new System.EventHandler(this.BuildRouteBtn_Click);
            // 
            // Reload
            // 
            this.Reload.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Reload.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.Reload.Enabled = false;
            this.Reload.Location = new System.Drawing.Point(775, 501);
            this.Reload.Name = "Reload";
            this.Reload.Size = new System.Drawing.Size(75, 25);
            this.Reload.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.Reload.TabIndex = 15;
            this.Reload.Text = "撤销更改";
            this.Reload.Tooltip = "重载初始拓扑";
            this.Reload.Click += new System.EventHandler(this.Reload_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.SaveBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.SaveBtn.Location = new System.Drawing.Point(775, 528);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(75, 25);
            this.SaveBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.SaveBtn.TabIndex = 10;
            this.SaveBtn.Text = "保存设置";
            this.SaveBtn.Tooltip = "保存当前拓扑为默认配置";
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // routemap
            // 
            this.routemap.AutoSize = true;
            this.routemap.Bearing = 0F;
            this.routemap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.routemap.CanDragMap = true;
            this.routemap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.routemap.GrayScaleMode = false;
            this.routemap.LevelsKeepInMemmory = 5;
            this.routemap.Location = new System.Drawing.Point(0, 0);
            this.routemap.MarkersEnabled = true;
            this.routemap.MaxZoom = 17;
            this.routemap.MinZoom = 2;
            this.routemap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.routemap.Name = "routemap";
            this.routemap.NegativeMode = false;
            this.routemap.PolygonsEnabled = false;
            this.routemap.RetryLoadTile = 0;
            this.routemap.RoutesEnabled = true;
            this.routemap.ShowTileGridLines = false;
            this.routemap.Size = new System.Drawing.Size(851, 553);
            this.routemap.TabIndex = 2;
            this.routemap.Zoom = 0D;
            this.routemap.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.routemap_OnMarkerClick);
            this.routemap.OnMarkerEnter += new GMap.NET.WindowsForms.MarkerEnter(this.routemap_OnMarkerEnter);
            this.routemap.OnMarkerLeave += new GMap.NET.WindowsForms.MarkerLeave(this.routemap_OnMarkerLeave);
            this.routemap.KeyDown += new System.Windows.Forms.KeyEventHandler(this.routemap_KeyDown);
            this.routemap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.routemap_MouseClick);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "grid";
            this.openFileDialog.Filter = "GRID文件|*.grid";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "grid";
            this.saveFileDialog.Filter = "GRID文件|*.grid";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 582);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1184, 22);
            this.statusStrip.TabIndex = 16;
            this.statusStrip.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // SetRouteForm
            // 
            this.AcceptButton = this.AddAllToCmd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 604);
            this.ControlBox = false;
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.groupPanel2);
            this.Controls.Add(this.groupPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetRouteForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置路由表";
            this.Load += new System.EventHandler(this.SetRouteForm_Load);
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NetGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NeiborNodeLst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RouteGrid)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private webnode.MapCustom.Map routemap;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.Controls.DataGridViewX RouteGrid;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox NodeBox;
        private DevComponents.DotNetBar.ButtonX SaveBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private DevComponents.DotNetBar.ButtonX AddAllToCmd;
        private DevComponents.DotNetBar.ButtonX CancelBtn;
        private DevComponents.DotNetBar.Controls.DataGridViewX NeiborNodeLst;
        private DevComponents.DotNetBar.ButtonX Reload;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.CheckBox ShowRouteAni;
        private DevComponents.DotNetBar.ButtonX AddNeiborToList;
        private DevComponents.DotNetBar.ButtonX AddRouteToList;
        private System.Windows.Forms.DataGridViewTextBoxColumn DestColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NextHopColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn HopsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Serial;
        private System.Windows.Forms.DataGridViewComboBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceNode;
        private System.Windows.Forms.DataGridViewTextBoxColumn NeiborNode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Distance;
        private System.Windows.Forms.DataGridViewComboBoxColumn Value;
        private DevComponents.DotNetBar.Controls.DataGridViewX NetGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
        private DevComponents.DotNetBar.ButtonX BuildRouteBtn;
        private DevComponents.DotNetBar.ButtonX AddRoute;
        private DevComponents.DotNetBar.ButtonX ClearNetBtn;
    }
}