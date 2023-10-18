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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ResultDataGridView = new System.Windows.Forms.DataGridView();
            this.RecalculateButton = new System.Windows.Forms.Button();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(711, 694);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Location = new System.Drawing.Point(708, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(476, 694);
            this.tabControl2.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ResultDataGridView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(468, 668);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "If (true)";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ResultDataGridView
            // 
            this.ResultDataGridView.AllowUserToAddRows = false;
            this.ResultDataGridView.AllowUserToDeleteRows = false;
            this.ResultDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResultDataGridView.ColumnHeadersVisible = false;
            this.ResultDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultDataGridView.Location = new System.Drawing.Point(0, 0);
            this.ResultDataGridView.Name = "ResultDataGridView";
            this.ResultDataGridView.ReadOnly = true;
            this.ResultDataGridView.RowHeadersVisible = false;
            this.ResultDataGridView.Size = new System.Drawing.Size(468, 668);
            this.ResultDataGridView.TabIndex = 0;
            // 
            // RecalculateButton
            // 
            this.RecalculateButton.Location = new System.Drawing.Point(832, -1);
            this.RecalculateButton.Name = "RecalculateButton";
            this.RecalculateButton.Size = new System.Drawing.Size(75, 23);
            this.RecalculateButton.TabIndex = 3;
            this.RecalculateButton.Text = "Recalculate";
            this.RecalculateButton.UseVisualStyleBackColor = true;
            this.RecalculateButton.Click += new System.EventHandler(this.RecalculateButton_Click);
            // 
            // DBResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 699);
            this.Controls.Add(this.RecalculateButton);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.tabControl1);
            this.Name = "DBResultForm";
            this.Text = "ResultForm";
            this.Shown += new System.EventHandler(this.ResultForm_Shown);
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ResultDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView ResultDataGridView;
        private System.Windows.Forms.Button RecalculateButton;
    }
}