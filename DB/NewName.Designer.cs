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
            this.InputDBObjectName = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.DBObjectName = new System.Windows.Forms.Label();
            this.DBObjectSignalName = new System.Windows.Forms.Label();
            this.InputDBObjectSignalName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // InputDBObjectName
            // 
            this.InputDBObjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.InputDBObjectName.Location = new System.Drawing.Point(51, 33);
            this.InputDBObjectName.Name = "InputDBObjectName";
            this.InputDBObjectName.Size = new System.Drawing.Size(195, 26);
            this.InputDBObjectName.TabIndex = 30;
            this.InputDBObjectName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(113, 125);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 32;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // DBObjectName
            // 
            this.DBObjectName.AutoSize = true;
            this.DBObjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DBObjectName.Location = new System.Drawing.Point(51, 14);
            this.DBObjectName.Name = "DBObjectName";
            this.DBObjectName.Size = new System.Drawing.Size(114, 16);
            this.DBObjectName.TabIndex = 32;
            this.DBObjectName.Text = "DBObjectName";
            // 
            // DBObjectSignalName
            // 
            this.DBObjectSignalName.AutoSize = true;
            this.DBObjectSignalName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DBObjectSignalName.Location = new System.Drawing.Point(51, 66);
            this.DBObjectSignalName.Name = "DBObjectSignalName";
            this.DBObjectSignalName.Size = new System.Drawing.Size(158, 16);
            this.DBObjectSignalName.TabIndex = 34;
            this.DBObjectSignalName.Text = "DBObjectSignalName";
            // 
            // InputDBObjectSignalName
            // 
            this.InputDBObjectSignalName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.InputDBObjectSignalName.Location = new System.Drawing.Point(51, 85);
            this.InputDBObjectSignalName.Name = "InputDBObjectSignalName";
            this.InputDBObjectSignalName.Size = new System.Drawing.Size(195, 26);
            this.InputDBObjectSignalName.TabIndex = 31;
            this.InputDBObjectSignalName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // NewName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 160);
            this.ControlBox = false;
            this.Controls.Add(this.DBObjectSignalName);
            this.Controls.Add(this.InputDBObjectSignalName);
            this.Controls.Add(this.DBObjectName);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.InputDBObjectName);
            this.Name = "NewName";
            this.Text = "NewName";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox InputDBObjectName;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label DBObjectName;
        private System.Windows.Forms.Label DBObjectSignalName;
        private System.Windows.Forms.TextBox InputDBObjectSignalName;
    }
}