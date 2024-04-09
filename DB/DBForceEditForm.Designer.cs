namespace IO_list_automation_new.DB
{
    partial class DBForceEditForm
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
            this.Data_Grid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.Data_Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // Data_Grid
            // 
            this.Data_Grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Data_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Data_Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Data_Grid.Location = new System.Drawing.Point(0, 0);
            this.Data_Grid.Name = "DataGrid";
            this.Data_Grid.Size = new System.Drawing.Size(800, 450);
            this.Data_Grid.TabIndex = 0;
            this.Data_Grid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_Event);
            // 
            // DBForceEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Data_Grid);
            this.Name = "DBForceEditForm";
            this.Text = "Force Edit";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_Event);
            ((System.ComponentModel.ISupportInitialize)(this.Data_Grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView Data_Grid;
    }
}