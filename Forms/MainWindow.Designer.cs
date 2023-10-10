using IO_list_automation_new.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace IO_list_automation_new
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.ProgressLabel = new System.Windows.Forms.Label();
            this.ProgressBars = new System.Windows.Forms.ProgressBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.FileDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.FileSaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileSaveAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileLoadMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileLoadAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileHelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileAboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.ProjectDesignMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectCompareDesignMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectTransferDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.DataFindFunctionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataCombineMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UniquesDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.DeclareDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.InstanceDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.SCADADropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.DesignTab = new System.Windows.Forms.TabPage();
            this.DesignGridView = new System.Windows.Forms.DataGridView();
            this.DataTab = new System.Windows.Forms.TabPage();
            this.DataGridView = new System.Windows.Forms.DataGridView();
            this.ObjectTab = new System.Windows.Forms.TabPage();
            this.ObjectsGridView = new System.Windows.Forms.DataGridView();
            this.FindTextBox = new System.Windows.Forms.TextBox();
            this.FindButton = new System.Windows.Forms.Button();
            this.comboboxColumn = new System.Windows.Forms.ComboBox();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.DesignTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DesignGridView)).BeginInit();
            this.DataTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            this.ObjectTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ProgressLabel.AutoSize = true;
            this.ProgressLabel.BackColor = System.Drawing.Color.Transparent;
            this.ProgressLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProgressLabel.Location = new System.Drawing.Point(624, 676);
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(48, 13);
            this.ProgressLabel.TabIndex = 6;
            this.ProgressLabel.Text = "Progress";
            this.ProgressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ProgressLabel.UseMnemonic = false;
            this.ProgressLabel.Visible = false;
            // 
            // ProgressBars
            // 
            this.ProgressBars.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBars.Location = new System.Drawing.Point(0, 674);
            this.ProgressBars.Name = "ProgressBars";
            this.ProgressBars.Size = new System.Drawing.Size(1283, 18);
            this.ProgressBars.Step = 1;
            this.ProgressBars.TabIndex = 5;
            this.ProgressBars.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileDropDownButton,
            this.ProjectDropDownButton,
            this.DataDropDownButton,
            this.UniquesDropDownButton,
            this.DeclareDropDownButton,
            this.InstanceDropDownButton,
            this.SCADADropDownButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1284, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // FileDropDownButton
            // 
            this.FileDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FileDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileSaveMenuItem,
            this.FileSaveAllMenuItem,
            this.FileLoadMenuItem,
            this.FileLoadAllMenuItem,
            this.FileHelpMenuItem,
            this.FileAboutMenuItem,
            this.FileExitMenuItem});
            this.FileDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("FileDropDownButton.Image")));
            this.FileDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FileDropDownButton.Name = "FileDropDownButton";
            this.FileDropDownButton.Size = new System.Drawing.Size(38, 22);
            this.FileDropDownButton.Text = "File";
            // 
            // FileSaveMenuItem
            // 
            this.FileSaveMenuItem.Name = "FileSaveMenuItem";
            this.FileSaveMenuItem.Size = new System.Drawing.Size(115, 22);
            this.FileSaveMenuItem.Text = "Save";
            this.FileSaveMenuItem.Click += new System.EventHandler(this.FileSaveMenuItem_Click);
            // 
            // FileSaveAllMenuItem
            // 
            this.FileSaveAllMenuItem.Name = "FileSaveAllMenuItem";
            this.FileSaveAllMenuItem.Size = new System.Drawing.Size(115, 22);
            this.FileSaveAllMenuItem.Text = "Save all";
            this.FileSaveAllMenuItem.Click += new System.EventHandler(this.FileSaveAllMenuItem_Click);
            // 
            // FileLoadMenuItem
            // 
            this.FileLoadMenuItem.Name = "FileLoadMenuItem";
            this.FileLoadMenuItem.Size = new System.Drawing.Size(115, 22);
            this.FileLoadMenuItem.Text = "Load";
            this.FileLoadMenuItem.Click += new System.EventHandler(this.FileLoadMenuItem_Click);
            // 
            // FileLoadAllMenuItem
            // 
            this.FileLoadAllMenuItem.Name = "FileLoadAllMenuItem";
            this.FileLoadAllMenuItem.Size = new System.Drawing.Size(115, 22);
            this.FileLoadAllMenuItem.Text = "Load all";
            this.FileLoadAllMenuItem.Click += new System.EventHandler(this.FileLoadAllMenuItem_Click);
            // 
            // FileHelpMenuItem
            // 
            this.FileHelpMenuItem.Name = "FileHelpMenuItem";
            this.FileHelpMenuItem.Size = new System.Drawing.Size(115, 22);
            this.FileHelpMenuItem.Text = "Help";
            this.FileHelpMenuItem.Click += new System.EventHandler(this.FileHelpMenuItem_Click);
            // 
            // FileAboutMenuItem
            // 
            this.FileAboutMenuItem.Name = "FileAboutMenuItem";
            this.FileAboutMenuItem.Size = new System.Drawing.Size(115, 22);
            this.FileAboutMenuItem.Text = "About";
            this.FileAboutMenuItem.Click += new System.EventHandler(this.FileAboutMenuItem_Click);
            // 
            // FileExitMenuItem
            // 
            this.FileExitMenuItem.Name = "FileExitMenuItem";
            this.FileExitMenuItem.Size = new System.Drawing.Size(115, 22);
            this.FileExitMenuItem.Text = "Exit";
            this.FileExitMenuItem.Click += new System.EventHandler(this.FileExitMenuItem_Click);
            // 
            // ProjectDropDownButton
            // 
            this.ProjectDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ProjectDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProjectDesignMenuItem,
            this.ProjectCompareDesignMenuItem,
            this.ProjectTransferDataMenuItem});
            this.ProjectDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("ProjectDropDownButton.Image")));
            this.ProjectDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ProjectDropDownButton.Name = "ProjectDropDownButton";
            this.ProjectDropDownButton.Size = new System.Drawing.Size(57, 22);
            this.ProjectDropDownButton.Text = "Project";
            // 
            // ProjectDesignMenuItem
            // 
            this.ProjectDesignMenuItem.Name = "ProjectDesignMenuItem";
            this.ProjectDesignMenuItem.Size = new System.Drawing.Size(212, 22);
            this.ProjectDesignMenuItem.Text = "Design";
            this.ProjectDesignMenuItem.Click += new System.EventHandler(this.ProjectDesignMenuItem_Click);
            // 
            // ProjectCompareDesignMenuItem
            // 
            this.ProjectCompareDesignMenuItem.Name = "ProjectCompareDesignMenuItem";
            this.ProjectCompareDesignMenuItem.Size = new System.Drawing.Size(212, 22);
            this.ProjectCompareDesignMenuItem.Text = "Compare with new design";
            this.ProjectCompareDesignMenuItem.Click += new System.EventHandler(this.ProjectCompareDesignMenuItem_Click);
            // 
            // ProjectTransferDataMenuItem
            // 
            this.ProjectTransferDataMenuItem.Name = "ProjectTransferDataMenuItem";
            this.ProjectTransferDataMenuItem.Size = new System.Drawing.Size(212, 22);
            this.ProjectTransferDataMenuItem.Text = "Transfer design to data";
            this.ProjectTransferDataMenuItem.Click += new System.EventHandler(this.ProjectTransferDataMenuItem_Click);
            // 
            // DataDropDownButton
            // 
            this.DataDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DataDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DataFindFunctionMenuItem,
            this.DataCombineMenuItem});
            this.DataDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("DataDropDownButton.Image")));
            this.DataDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DataDropDownButton.Name = "DataDropDownButton";
            this.DataDropDownButton.Size = new System.Drawing.Size(57, 22);
            this.DataDropDownButton.Text = "Signals";
            // 
            // DataFindFunctionMenuItem
            // 
            this.DataFindFunctionMenuItem.Name = "DataFindFunctionMenuItem";
            this.DataFindFunctionMenuItem.Size = new System.Drawing.Size(146, 22);
            this.DataFindFunctionMenuItem.Text = "Find function";
            // 
            // DataCombineMenuItem
            // 
            this.DataCombineMenuItem.Name = "DataCombineMenuItem";
            this.DataCombineMenuItem.Size = new System.Drawing.Size(146, 22);
            this.DataCombineMenuItem.Text = "KKS Combine";
            this.DataCombineMenuItem.Click += new System.EventHandler(this.KKSCombineMenuItem_Click);
            // 
            // UniquesDropDownButton
            // 
            this.UniquesDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.UniquesDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("UniquesDropDownButton.Image")));
            this.UniquesDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UniquesDropDownButton.Name = "UniquesDropDownButton";
            this.UniquesDropDownButton.Size = new System.Drawing.Size(63, 22);
            this.UniquesDropDownButton.Text = "Uniques";
            // 
            // DeclareDropDownButton
            // 
            this.DeclareDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DeclareDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("DeclareDropDownButton.Image")));
            this.DeclareDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeclareDropDownButton.Name = "DeclareDropDownButton";
            this.DeclareDropDownButton.Size = new System.Drawing.Size(59, 22);
            this.DeclareDropDownButton.Text = "Declare";
            // 
            // InstanceDropDownButton
            // 
            this.InstanceDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.InstanceDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("InstanceDropDownButton.Image")));
            this.InstanceDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.InstanceDropDownButton.Name = "InstanceDropDownButton";
            this.InstanceDropDownButton.Size = new System.Drawing.Size(64, 22);
            this.InstanceDropDownButton.Text = "Instance";
            // 
            // SCADADropDownButton
            // 
            this.SCADADropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SCADADropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("SCADADropDownButton.Image")));
            this.SCADADropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SCADADropDownButton.Name = "SCADADropDownButton";
            this.SCADADropDownButton.Size = new System.Drawing.Size(58, 22);
            this.SCADADropDownButton.Text = "SCADA";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.DesignTab);
            this.tabControl1.Controls.Add(this.DataTab);
            this.tabControl1.Controls.Add(this.ObjectTab);
            this.tabControl1.Location = new System.Drawing.Point(0, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1284, 648);
            this.tabControl1.TabIndex = 8;
            this.tabControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabControl1_KeyDown);
            // 
            // DesignTab
            // 
            this.DesignTab.Controls.Add(this.DesignGridView);
            this.DesignTab.Location = new System.Drawing.Point(4, 22);
            this.DesignTab.Name = "DesignTab";
            this.DesignTab.Padding = new System.Windows.Forms.Padding(3);
            this.DesignTab.Size = new System.Drawing.Size(1276, 622);
            this.DesignTab.TabIndex = 0;
            this.DesignTab.Text = "Design";
            this.DesignTab.UseVisualStyleBackColor = true;
            // 
            // DesignGridView
            // 
            this.DesignGridView.AllowUserToOrderColumns = true;
            this.DesignGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DesignGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DesignGridView.Location = new System.Drawing.Point(0, 0);
            this.DesignGridView.Name = "DesignGridView";
            this.DesignGridView.Size = new System.Drawing.Size(1276, 622);
            this.DesignGridView.TabIndex = 0;
            this.DesignGridView.Click += new System.EventHandler(this.DesignGridView_Click);
            // 
            // DataTab
            // 
            this.DataTab.Controls.Add(this.DataGridView);
            this.DataTab.Location = new System.Drawing.Point(4, 22);
            this.DataTab.Name = "DataTab";
            this.DataTab.Padding = new System.Windows.Forms.Padding(3);
            this.DataTab.Size = new System.Drawing.Size(1276, 622);
            this.DataTab.TabIndex = 1;
            this.DataTab.Text = "Data";
            this.DataTab.UseVisualStyleBackColor = true;
            // 
            // DataGridView
            // 
            this.DataGridView.AllowUserToOrderColumns = true;
            this.DataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView.Location = new System.Drawing.Point(0, 0);
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.Size = new System.Drawing.Size(1276, 622);
            this.DataGridView.TabIndex = 1;
            this.DataGridView.Click += new System.EventHandler(this.DataGridView_Click);
            // 
            // ObjectTab
            // 
            this.ObjectTab.Controls.Add(this.ObjectsGridView);
            this.ObjectTab.Location = new System.Drawing.Point(4, 22);
            this.ObjectTab.Name = "ObjectTab";
            this.ObjectTab.Size = new System.Drawing.Size(1276, 622);
            this.ObjectTab.TabIndex = 2;
            this.ObjectTab.Text = "Object";
            this.ObjectTab.UseVisualStyleBackColor = true;
            // 
            // ObjectsGridView
            // 
            this.ObjectsGridView.AllowUserToOrderColumns = true;
            this.ObjectsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ObjectsGridView.Location = new System.Drawing.Point(0, 0);
            this.ObjectsGridView.Name = "ObjectsGridView";
            this.ObjectsGridView.Size = new System.Drawing.Size(1276, 622);
            this.ObjectsGridView.TabIndex = 1;
            this.ObjectsGridView.Click += new System.EventHandler(this.ObjectsGridView_Click);
            // 
            // FindTextBox
            // 
            this.FindTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FindTextBox.Location = new System.Drawing.Point(993, 3);
            this.FindTextBox.Name = "FindTextBox";
            this.FindTextBox.Size = new System.Drawing.Size(190, 20);
            this.FindTextBox.TabIndex = 9;
            // 
            // FindButton
            // 
            this.FindButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FindButton.Location = new System.Drawing.Point(1187, 3);
            this.FindButton.Name = "FindButton";
            this.FindButton.Size = new System.Drawing.Size(91, 19);
            this.FindButton.TabIndex = 10;
            this.FindButton.Text = "Find";
            this.FindButton.UseVisualStyleBackColor = true;
            // 
            // comboboxColumn
            // 
            this.comboboxColumn.FormattingEnabled = true;
            this.comboboxColumn.Location = new System.Drawing.Point(942, 50);
            this.comboboxColumn.Name = "comboboxColumn";
            this.comboboxColumn.Size = new System.Drawing.Size(101, 21);
            this.comboboxColumn.TabIndex = 11;
            this.comboboxColumn.Visible = false;
            this.comboboxColumn.SelectedValueChanged += new System.EventHandler(this.comboboxColumn_SelectedValueChanged);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 691);
            this.Controls.Add(this.comboboxColumn);
            this.Controls.Add(this.FindButton);
            this.Controls.Add(this.FindTextBox);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.ProgressLabel);
            this.Controls.Add(this.ProgressBars);
            this.DoubleBuffered = true;
            this.Name = "MainWindow";
            this.Text = "IO List Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.DesignTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DesignGridView)).EndInit();
            this.DataTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            this.ObjectTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ObjectsGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label ProgressLabel;
        private System.Windows.Forms.ProgressBar ProgressBars;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton FileDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem FileSaveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileSaveAllMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileLoadMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileLoadAllMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileHelpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileAboutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileExitMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton ProjectDropDownButton;
        private System.Windows.Forms.ToolStripDropDownButton DataDropDownButton;
        private System.Windows.Forms.ToolStripDropDownButton UniquesDropDownButton;
        private System.Windows.Forms.ToolStripDropDownButton DeclareDropDownButton;
        private System.Windows.Forms.ToolStripDropDownButton InstanceDropDownButton;
        private System.Windows.Forms.ToolStripDropDownButton SCADADropDownButton;
        private System.Windows.Forms.ToolStripMenuItem ProjectDesignMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ProjectCompareDesignMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ProjectTransferDataMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DataFindFunctionMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage DesignTab;
        private System.Windows.Forms.TabPage DataTab;
        private System.Windows.Forms.TabPage ObjectTab;
        private System.Windows.Forms.TextBox FindTextBox;
        private System.Windows.Forms.Button FindButton;
        private System.Windows.Forms.DataGridView DesignGridView;
        private DataGridView DataGridView;
        private DataGridView ObjectsGridView;
        private ToolStripMenuItem DataCombineMenuItem;
        private ComboBox comboboxColumn;
    }
}

