namespace 通信网数据分析
{
    partial class DataViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataViewForm));
            this.DatatreeGrid = new AdvancedDataGridView.TreeGridView();
            this.DataType = new AdvancedDataGridView.TreeGridColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DatatreeGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // DatatreeGrid
            // 
            this.DatatreeGrid.AllowUserToAddRows = false;
            this.DatatreeGrid.AllowUserToDeleteRows = false;
            this.DatatreeGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DatatreeGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.DatatreeGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DataType,
            this.Data,
            this.DataDescription});
            this.DatatreeGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DatatreeGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DatatreeGrid.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.DatatreeGrid.ImageList = null;
            this.DatatreeGrid.Location = new System.Drawing.Point(0, 0);
            this.DatatreeGrid.MultiSelect = false;
            this.DatatreeGrid.Name = "DatatreeGrid";
            this.DatatreeGrid.RowHeadersVisible = false;
            this.DatatreeGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DatatreeGrid.Size = new System.Drawing.Size(623, 484);
            this.DatatreeGrid.TabIndex = 0;
            // 
            // DataType
            // 
            this.DataType.DefaultNodeImage = null;
            this.DataType.FillWeight = 130F;
            this.DataType.HeaderText = "数据类型";
            this.DataType.Name = "DataType";
            this.DataType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DataType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Data
            // 
            this.Data.HeaderText = "数据原值";
            this.Data.Name = "Data";
            this.Data.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Data.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DataDescription
            // 
            this.DataDescription.HeaderText = "数据描述";
            this.DataDescription.Name = "DataDescription";
            this.DataDescription.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DataDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DataViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 484);
            this.Controls.Add(this.DatatreeGrid);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DataViewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据展开";
            ((System.ComponentModel.ISupportInitialize)(this.DatatreeGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AdvancedDataGridView.TreeGridView DatatreeGrid;
        private AdvancedDataGridView.TreeGridColumn DataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataDescription;
    }
}