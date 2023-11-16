namespace IO_list_automation_new
{
    partial class IOTextEdit
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
            this.ObjectSpecificsSeparator = new System.Windows.Forms.TextBox();
            this.FunctionTextSeparator = new System.Windows.Forms.TextBox();
            this.IOEditOkNextButton = new System.Windows.Forms.Button();
            this.IOEditCancelButton = new System.Windows.Forms.Button();
            this.IOEditOKButton = new System.Windows.Forms.Button();
            this.IOTextIn = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.ObjectName = new System.Windows.Forms.Label();
            this.ObjectSpecifics = new System.Windows.Forms.Label();
            this.FunctionText = new System.Windows.Forms.Label();
            this.SeparatorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ObjectSpecificsSeparator
            // 
            this.ObjectSpecificsSeparator.Location = new System.Drawing.Point(40, 132);
            this.ObjectSpecificsSeparator.Name = "ObjectSpecificsSeparator";
            this.ObjectSpecificsSeparator.Size = new System.Drawing.Size(50, 20);
            this.ObjectSpecificsSeparator.TabIndex = 45;
            this.ObjectSpecificsSeparator.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ObjectSpecificsSeparator.TextChanged += new System.EventHandler(this.Parse_Event);
            // 
            // FunctionTextSeparator
            // 
            this.FunctionTextSeparator.Location = new System.Drawing.Point(40, 162);
            this.FunctionTextSeparator.Name = "FunctionTextSeparator";
            this.FunctionTextSeparator.Size = new System.Drawing.Size(50, 20);
            this.FunctionTextSeparator.TabIndex = 44;
            this.FunctionTextSeparator.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.FunctionTextSeparator.TextChanged += new System.EventHandler(this.Parse_Event);
            // 
            // IOEditOkNextButton
            // 
            this.IOEditOkNextButton.Location = new System.Drawing.Point(271, 244);
            this.IOEditOkNextButton.Name = "IOEditOkNextButton";
            this.IOEditOkNextButton.Size = new System.Drawing.Size(83, 23);
            this.IOEditOkNextButton.TabIndex = 43;
            this.IOEditOkNextButton.Text = "OK, Next";
            this.IOEditOkNextButton.UseVisualStyleBackColor = true;
            this.IOEditOkNextButton.Click += new System.EventHandler(this.IOEditOkNextButton_Click);
            // 
            // IOEditCancelButton
            // 
            this.IOEditCancelButton.Location = new System.Drawing.Point(342, 202);
            this.IOEditCancelButton.Name = "IOEditCancelButton";
            this.IOEditCancelButton.Size = new System.Drawing.Size(55, 23);
            this.IOEditCancelButton.TabIndex = 41;
            this.IOEditCancelButton.Text = "Cancel";
            this.IOEditCancelButton.UseVisualStyleBackColor = true;
            this.IOEditCancelButton.Click += new System.EventHandler(this.IOEditCancelButton_Click);
            // 
            // IOEditOKButton
            // 
            this.IOEditOKButton.Location = new System.Drawing.Point(235, 202);
            this.IOEditOKButton.Name = "IOEditOKButton";
            this.IOEditOKButton.Size = new System.Drawing.Size(55, 23);
            this.IOEditOKButton.TabIndex = 40;
            this.IOEditOKButton.Text = "OK";
            this.IOEditOKButton.UseVisualStyleBackColor = true;
            this.IOEditOKButton.Click += new System.EventHandler(this.IOEditOKButton_Click);
            // 
            // IOTextIn
            // 
            this.IOTextIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IOTextIn.Location = new System.Drawing.Point(98, 27);
            this.IOTextIn.Name = "IOTextIn";
            this.IOTextIn.Size = new System.Drawing.Size(492, 21);
            this.IOTextIn.TabIndex = 37;
            this.IOTextIn.Tag = "";
            this.IOTextIn.Text = "IO Text in";
            this.IOTextIn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.Location = new System.Drawing.Point(278, 9);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(98, 18);
            this.label.TabIndex = 36;
            this.label.Text = "IO text Input";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ObjectName
            // 
            this.ObjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ObjectName.Location = new System.Drawing.Point(98, 101);
            this.ObjectName.Name = "ObjectName";
            this.ObjectName.Size = new System.Drawing.Size(492, 21);
            this.ObjectName.TabIndex = 50;
            this.ObjectName.Tag = "";
            this.ObjectName.Text = "ObjectName";
            this.ObjectName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ObjectSpecifics
            // 
            this.ObjectSpecifics.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ObjectSpecifics.Location = new System.Drawing.Point(98, 131);
            this.ObjectSpecifics.Name = "ObjectSpecifics";
            this.ObjectSpecifics.Size = new System.Drawing.Size(492, 21);
            this.ObjectSpecifics.TabIndex = 51;
            this.ObjectSpecifics.Tag = "";
            this.ObjectSpecifics.Text = "ObjectSpecifics";
            this.ObjectSpecifics.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FunctionText
            // 
            this.FunctionText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FunctionText.Location = new System.Drawing.Point(98, 161);
            this.FunctionText.Name = "FunctionText";
            this.FunctionText.Size = new System.Drawing.Size(492, 21);
            this.FunctionText.TabIndex = 52;
            this.FunctionText.Tag = "";
            this.FunctionText.Text = "FunctionText";
            this.FunctionText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SeparatorLabel
            // 
            this.SeparatorLabel.AutoSize = true;
            this.SeparatorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.SeparatorLabel.Location = new System.Drawing.Point(29, 81);
            this.SeparatorLabel.Name = "SeparatorLabel";
            this.SeparatorLabel.Size = new System.Drawing.Size(71, 17);
            this.SeparatorLabel.TabIndex = 53;
            this.SeparatorLabel.Text = "Separator";
            this.SeparatorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IOTextEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 279);
            this.Controls.Add(this.SeparatorLabel);
            this.Controls.Add(this.FunctionText);
            this.Controls.Add(this.ObjectSpecifics);
            this.Controls.Add(this.ObjectName);
            this.Controls.Add(this.ObjectSpecificsSeparator);
            this.Controls.Add(this.FunctionTextSeparator);
            this.Controls.Add(this.IOEditOkNextButton);
            this.Controls.Add(this.IOEditCancelButton);
            this.Controls.Add(this.IOEditOKButton);
            this.Controls.Add(this.IOTextIn);
            this.Controls.Add(this.label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IOTextEdit";
            this.Text = "IOTextEdit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox ObjectSpecificsSeparator;
        private System.Windows.Forms.TextBox FunctionTextSeparator;
        private System.Windows.Forms.Button IOEditOkNextButton;
        private System.Windows.Forms.Button IOEditCancelButton;
        private System.Windows.Forms.Button IOEditOKButton;
        private System.Windows.Forms.Label IOTextIn;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label ObjectName;
        private System.Windows.Forms.Label ObjectSpecifics;
        private System.Windows.Forms.Label FunctionText;
        private System.Windows.Forms.Label SeparatorLabel;
    }
}