namespace IO_list_automation_new.DB
{
    partial class NewName
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
            this.InputData = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InputData
            // 
            this.InputData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.InputData.Location = new System.Drawing.Point(51, 40);
            this.InputData.Name = "InputData";
            this.InputData.Size = new System.Drawing.Size(195, 26);
            this.InputData.TabIndex = 30;
            this.InputData.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(113, 99);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 31;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // NewName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 134);
            this.ControlBox = false;
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.InputData);
            this.Name = "NewName";
            this.Text = "NewName";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox InputData;
        private System.Windows.Forms.Button buttonOK;
    }
}