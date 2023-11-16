namespace IO_list_automation_new.Forms
{
    partial class DesignInputData 
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
            this.InputDataGridView = new System.Windows.Forms.DataGridView();
            this.RowOffsetInput = new System.Windows.Forms.TextBox();
            this.RowOffsetLabel = new System.Windows.Forms.Label();
            this.comboBoxColumn = new System.Windows.Forms.ComboBox();
            this.ElementHasChannelAndIsNumber = new System.Windows.Forms.Label();
            this.ElementHasModuleName = new System.Windows.Forms.Label();
            this.ElementHasIOText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.InputDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // InputDataGridView
            // 
            this.InputDataGridView.AllowUserToAddRows = false;
            this.InputDataGridView.AllowUserToDeleteRows = false;
            this.InputDataGridView.AllowUserToResizeColumns = false;
            this.InputDataGridView.AllowUserToResizeRows = false;
            this.InputDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InputDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.InputDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.InputDataGridView.Location = new System.Drawing.Point(2, 52);
            this.InputDataGridView.Name = "InputDataGridView";
            this.InputDataGridView.ReadOnly = true;
            this.InputDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.InputDataGridView.Size = new System.Drawing.Size(998, 578);
            this.InputDataGridView.TabIndex = 0;
            this.InputDataGridView.Visible = false;
            this.InputDataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.InputDataGridView_ColumnHeaderMouseClick);
            // 
            // RowOffsetInput
            // 
            this.RowOffsetInput.Location = new System.Drawing.Point(11, 18);
            this.RowOffsetInput.Name = "RowOffsetInput";
            this.RowOffsetInput.Size = new System.Drawing.Size(50, 20);
            this.RowOffsetInput.TabIndex = 41;
            this.RowOffsetInput.Text = "2";
            this.RowOffsetInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.RowOffsetInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RowOffsetInput_KeyPress);
            // 
            // RowOffsetLabel
            // 
            this.RowOffsetLabel.AutoSize = true;
            this.RowOffsetLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RowOffsetLabel.Location = new System.Drawing.Point(8, 1);
            this.RowOffsetLabel.Name = "RowOffsetLabel";
            this.RowOffsetLabel.Size = new System.Drawing.Size(68, 16);
            this.RowOffsetLabel.TabIndex = 42;
            this.RowOffsetLabel.Tag = "";
            this.RowOffsetLabel.Text = "RowOffset";
            this.RowOffsetLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxColumn
            // 
            this.comboBoxColumn.FormattingEnabled = true;
            this.comboBoxColumn.Location = new System.Drawing.Point(563, 43);
            this.comboBoxColumn.Name = "comboBoxColumn";
            this.comboBoxColumn.Size = new System.Drawing.Size(101, 21);
            this.comboBoxColumn.TabIndex = 43;
            this.comboBoxColumn.Visible = false;
            this.comboBoxColumn.SelectedValueChanged += new System.EventHandler(this.ComboBoxColumn_SelectedValueChanged);
            // 
            // ElementHasChannelAndIsNumber
            // 
            this.ElementHasChannelAndIsNumber.AutoSize = true;
            this.ElementHasChannelAndIsNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ElementHasChannelAndIsNumber.Location = new System.Drawing.Point(237, 1);
            this.ElementHasChannelAndIsNumber.Name = "ElementHasChannelAndIsNumber";
            this.ElementHasChannelAndIsNumber.Size = new System.Drawing.Size(212, 16);
            this.ElementHasChannelAndIsNumber.TabIndex = 44;
            this.ElementHasChannelAndIsNumber.Tag = "";
            this.ElementHasChannelAndIsNumber.Text = "ElementHasChannelAndIsNumber";
            this.ElementHasChannelAndIsNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ElementHasModuleName
            // 
            this.ElementHasModuleName.AutoSize = true;
            this.ElementHasModuleName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ElementHasModuleName.Location = new System.Drawing.Point(237, 17);
            this.ElementHasModuleName.Name = "ElementHasModuleName";
            this.ElementHasModuleName.Size = new System.Drawing.Size(163, 16);
            this.ElementHasModuleName.TabIndex = 45;
            this.ElementHasModuleName.Tag = "";
            this.ElementHasModuleName.Text = "ElementHasModuleName";
            this.ElementHasModuleName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ElementHasIOText
            // 
            this.ElementHasIOText.AutoSize = true;
            this.ElementHasIOText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ElementHasIOText.Location = new System.Drawing.Point(237, 33);
            this.ElementHasIOText.Name = "ElementHasIOText";
            this.ElementHasIOText.Size = new System.Drawing.Size(120, 16);
            this.ElementHasIOText.TabIndex = 46;
            this.ElementHasIOText.Tag = "";
            this.ElementHasIOText.Text = "ElementHasIOText";
            this.ElementHasIOText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DesignInputData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 631);
            this.Controls.Add(this.ElementHasIOText);
            this.Controls.Add(this.ElementHasModuleName);
            this.Controls.Add(this.ElementHasChannelAndIsNumber);
            this.Controls.Add(this.comboBoxColumn);
            this.Controls.Add(this.RowOffsetLabel);
            this.Controls.Add(this.RowOffsetInput);
            this.Controls.Add(this.InputDataGridView);
            this.Name = "DesignInputData";
            this.Text = "DesignInputData";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DesignInputData_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.InputDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView InputDataGridView;
        private System.Windows.Forms.TextBox RowOffsetInput;
        private System.Windows.Forms.Label RowOffsetLabel;
        private System.Windows.Forms.ComboBox comboBoxColumn;
        private System.Windows.Forms.Label ElementHasChannelAndIsNumber;
        private System.Windows.Forms.Label ElementHasModuleName;
        private System.Windows.Forms.Label ElementHasIOText;
    }
}