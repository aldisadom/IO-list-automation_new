namespace IO_list_automation_new.DB
{
    partial class DBResultForm
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
            this.CellEditCombobox = new System.Windows.Forms.ComboBox();
            this.PageEditCombobox = new System.Windows.Forms.ComboBox();
            this.DBTabControl = new System.Windows.Forms.TabControl();
            this.ResultTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ResultDataGridView = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ResultTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultDataGridView)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CellEditCombobox
            // 
            this.CellEditCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CellEditCombobox.FormattingEnabled = true;
            this.CellEditCombobox.Location = new System.Drawing.Point(3, 3);
            this.CellEditCombobox.Name = "CellEditCombobox";
            this.CellEditCombobox.Size = new System.Drawing.Size(121, 21);
            this.CellEditCombobox.TabIndex = 3;
            this.CellEditCombobox.Visible = false;
            this.CellEditCombobox.SelectedIndexChanged += new System.EventHandler(this.CellEditCombobox_SelectedIndexChanged);
            // 
            // PageEditCombobox
            // 
            this.PageEditCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PageEditCombobox.FormattingEnabled = true;
            this.PageEditCombobox.Location = new System.Drawing.Point(3, 3);
            this.PageEditCombobox.Name = "PageEditCombobox";
            this.PageEditCombobox.Size = new System.Drawing.Size(121, 21);
            this.PageEditCombobox.TabIndex = 3;
            this.PageEditCombobox.Visible = false;
            this.PageEditCombobox.SelectedIndexChanged += new System.EventHandler(this.PageEditCombobox_SelectedIndexChanged);
            // 
            // DBTabControl
            // 
            this.DBTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DBTabControl.Location = new System.Drawing.Point(3, 3);
            this.DBTabControl.Name = "DBTabControl";
            this.DBTabControl.SelectedIndex = 0;
            this.DBTabControl.Size = new System.Drawing.Size(734, 693);
            this.DBTabControl.TabIndex = 0;
            this.DBTabControl.SelectedIndexChanged += new System.EventHandler(this.DBTabControl_SelectedIndexChanged);
            this.DBTabControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DBTabControl_MouseClick);
            // 
            // ResultTabControl
            // 
            this.ResultTabControl.Controls.Add(this.tabPage1);
            this.ResultTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultTabControl.Location = new System.Drawing.Point(743, 3);
            this.ResultTabControl.Name = "ResultTabControl";
            this.ResultTabControl.SelectedIndex = 0;
            this.ResultTabControl.Size = new System.Drawing.Size(438, 693);
            this.ResultTabControl.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ResultDataGridView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(430, 667);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "If (true)";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ResultDataGridView
            // 
            this.ResultDataGridView.AllowUserToAddRows = false;
            this.ResultDataGridView.AllowUserToDeleteRows = false;
            this.ResultDataGridView.AllowUserToResizeColumns = false;
            this.ResultDataGridView.AllowUserToResizeRows = false;
            this.ResultDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResultDataGridView.ColumnHeadersVisible = false;
            this.ResultDataGridView.Location = new System.Drawing.Point(0, 0);
            this.ResultDataGridView.Name = "ResultDataGridView";
            this.ResultDataGridView.ReadOnly = true;
            this.ResultDataGridView.RowHeadersVisible = false;
            this.ResultDataGridView.Size = new System.Drawing.Size(430, 1319);
            this.ResultDataGridView.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.53264F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.46736F));
            this.tableLayoutPanel1.Controls.Add(this.ResultTabControl, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.DBTabControl, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1184, 699);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // DBResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 699);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.CellEditCombobox);
            this.Controls.Add(this.PageEditCombobox);
            this.Name = "DBResultForm";
            this.Text = "ResultForm";
            this.Shown += new System.EventHandler(this.ResultForm_Shown);
            this.ResultTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ResultDataGridView)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox CellEditCombobox;
        private System.Windows.Forms.ComboBox PageEditCombobox;
        private System.Windows.Forms.TabControl ResultTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView ResultDataGridView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.TabControl DBTabControl;
    }
}