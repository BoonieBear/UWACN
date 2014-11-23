namespace webnode.Forms
{
    partial class TrackForm
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
            this.Trace = new System.Windows.Forms.DataGridView();
            this.DestNode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TraceLog = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CancelBtn = new DevComponents.DotNetBar.ButtonX();
            this.Confbtn = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.Trace)).BeginInit();
            this.SuspendLayout();
            // 
            // Trace
            // 
            this.Trace.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Trace.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DestNode,
            this.TraceLog});
            this.Trace.Dock = System.Windows.Forms.DockStyle.Top;
            this.Trace.Location = new System.Drawing.Point(0, 0);
            this.Trace.MultiSelect = false;
            this.Trace.Name = "Trace";
            this.Trace.RowHeadersVisible = false;
            this.Trace.RowTemplate.Height = 23;
            this.Trace.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Trace.ShowCellErrors = false;
            this.Trace.ShowEditingIcon = false;
            this.Trace.ShowRowErrors = false;
            this.Trace.Size = new System.Drawing.Size(309, 164);
            this.Trace.TabIndex = 0;
            // 
            // DestNode
            // 
            this.DestNode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DestNode.FillWeight = 50F;
            this.DestNode.HeaderText = "目标节点";
            this.DestNode.Name = "DestNode";
            // 
            // TraceLog
            // 
            this.TraceLog.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TraceLog.HeaderText = "路径安排";
            this.TraceLog.Name = "TraceLog";
            this.TraceLog.ToolTipText = "节点号间用逗号分隔";
            // 
            // CancelBtn
            // 
            this.CancelBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.CancelBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(153, 170);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.CancelBtn.TabIndex = 1;
            this.CancelBtn.Text = "取消";
            // 
            // Confbtn
            // 
            this.Confbtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Confbtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.Confbtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Confbtn.Location = new System.Drawing.Point(234, 170);
            this.Confbtn.Name = "Confbtn";
            this.Confbtn.Size = new System.Drawing.Size(75, 23);
            this.Confbtn.TabIndex = 2;
            this.Confbtn.Text = "确定";
            // 
            // TrackForm
            // 
            this.AcceptButton = this.Confbtn;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(309, 199);
            this.ControlBox = false;
            this.Controls.Add(this.Confbtn);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.Trace);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TrackForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "路径选择";
            ((System.ComponentModel.ISupportInitialize)(this.Trace)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX CancelBtn;
        private DevComponents.DotNetBar.ButtonX Confbtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DestNode;
        private System.Windows.Forms.DataGridViewTextBoxColumn TraceLog;
        public System.Windows.Forms.DataGridView Trace;

    }
}