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
            this.PinIsNumber = new System.Windows.Forms.CheckBox();
            this.PinHasNumber = new System.Windows.Forms.CheckBox();
            this.ChannelIsNumber = new System.Windows.Forms.CheckBox();
            this.ChannelHasNumber = new System.Windows.Forms.CheckBox();
            this.RowOffsetInput = new System.Windows.Forms.TextBox();
            this.RowOffsetLabel = new System.Windows.Forms.Label();
            this.comboBoxColumn = new System.Windows.Forms.ComboBox();
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
            this.InputDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.InputDataGridView.Location = new System.Drawing.Point(2, 41);
            this.InputDataGridView.Name = "InputDataGridView";
            this.InputDataGridView.ReadOnly = true;
            this.InputDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.InputDataGridView.Size = new System.Drawing.Size(998, 589);
            this.InputDataGridView.TabIndex = 0;
            this.InputDataGridView.Visible = false;
            this.InputDataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.InputDataGridView_ColumnHeaderMouseClick);
            // 
            // PinIsNumber
            // 
            this.PinIsNumber.AutoSize = true;
            this.PinIsNumber.Checked = true;
            this.PinIsNumber.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PinIsNumber.Location = new System.Drawing.Point(325, 15);
            this.PinIsNumber.Name = "PinIsNumber";
            this.PinIsNumber.Size = new System.Drawing.Size(86, 17);
            this.PinIsNumber.TabIndex = 40;
            this.PinIsNumber.Text = "PinIsNumber";
            this.PinIsNumber.UseVisualStyleBackColor = true;
            // 
            // PinHasNumber
            // 
            this.PinHasNumber.AutoSize = true;
            this.PinHasNumber.Location = new System.Drawing.Point(325, 0);
            this.PinHasNumber.Name = "PinHasNumber";
            this.PinHasNumber.Size = new System.Drawing.Size(97, 17);
            this.PinHasNumber.TabIndex = 39;
            this.PinHasNumber.Text = "PinHasNumber";
            this.PinHasNumber.UseVisualStyleBackColor = true;
            // 
            // ChannelIsNumber
            // 
            this.ChannelIsNumber.AutoSize = true;
            this.ChannelIsNumber.Location = new System.Drawing.Point(2, 15);
            this.ChannelIsNumber.Name = "ChannelIsNumber";
            this.ChannelIsNumber.Size = new System.Drawing.Size(110, 17);
            this.ChannelIsNumber.TabIndex = 38;
            this.ChannelIsNumber.Text = "ChannelIsNumber";
            this.ChannelIsNumber.UseVisualStyleBackColor = true;
            // 
            // ChannelHasNumber
            // 
            this.ChannelHasNumber.AutoSize = true;
            this.ChannelHasNumber.Checked = true;
            this.ChannelHasNumber.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChannelHasNumber.Location = new System.Drawing.Point(2, 0);
            this.ChannelHasNumber.Name = "ChannelHasNumber";
            this.ChannelHasNumber.Size = new System.Drawing.Size(121, 17);
            this.ChannelHasNumber.TabIndex = 37;
            this.ChannelHasNumber.Text = "ChannelHasNumber";
            this.ChannelHasNumber.UseVisualStyleBackColor = true;
            // 
            // RowOffsetInput
            // 
            this.RowOffsetInput.Location = new System.Drawing.Point(533, 17);
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
            this.RowOffsetLabel.Location = new System.Drawing.Point(530, 0);
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
            this.comboBoxColumn.SelectedValueChanged += new System.EventHandler(this.comboBoxColumn_SelectedValueChanged);
            // 
            // DesignInputData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 631);
            this.Controls.Add(this.comboBoxColumn);
            this.Controls.Add(this.RowOffsetLabel);
            this.Controls.Add(this.RowOffsetInput);
            this.Controls.Add(this.PinIsNumber);
            this.Controls.Add(this.PinHasNumber);
            this.Controls.Add(this.ChannelIsNumber);
            this.Controls.Add(this.ChannelHasNumber);
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
        private System.Windows.Forms.CheckBox PinIsNumber;
        private System.Windows.Forms.CheckBox PinHasNumber;
        private System.Windows.Forms.CheckBox ChannelIsNumber;
        private System.Windows.Forms.CheckBox ChannelHasNumber;
        private System.Windows.Forms.TextBox RowOffsetInput;
        private System.Windows.Forms.Label RowOffsetLabel;
        private System.Windows.Forms.ComboBox comboBoxColumn;
    }
}