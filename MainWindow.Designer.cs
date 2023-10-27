﻿using IO_list_automation_new.Properties;
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
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FileLanguageToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileLanguageENMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileLanguageLTMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileHelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileAboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.FileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.ProjectGetDataFromDesignMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectCompareDesignMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectTransferDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ProjectCPUMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectCPUAddMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectSCADAMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectSCADAAddMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectLanguageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectLanguageAddMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.DataFindFunctionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataKKSCombineMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ObjectsDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.ObjectsFindMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ObjectsFindTypeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ObjectTransferToDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.ObjectEditTypesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IODropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.IOFindModulesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.IOEditMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeclareDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.DeclareGenerateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.DeclareEditMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InstanceDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.InstancesGenerateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.InstancesEditMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SCADADropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.SCADAEditMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.DesignTab = new System.Windows.Forms.TabPage();
            this.DesignGridView = new System.Windows.Forms.DataGridView();
            this.DataTab = new System.Windows.Forms.TabPage();
            this.DataGridView = new System.Windows.Forms.DataGridView();
            this.ObjectTab = new System.Windows.Forms.TabPage();
            this.ObjectsGridView = new System.Windows.Forms.DataGridView();
            this.ModuleTab = new System.Windows.Forms.TabPage();
            this.ModulesGridView = new System.Windows.Forms.DataGridView();
            this.FindTextBox = new System.Windows.Forms.TextBox();
            this.FindButton = new System.Windows.Forms.Button();
            this.comboBoxColumn = new System.Windows.Forms.ComboBox();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.DesignTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DesignGridView)).BeginInit();
            this.DataTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            this.ObjectTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectsGridView)).BeginInit();
            this.ModuleTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ModulesGridView)).BeginInit();
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
            this.ObjectsDropDownButton,
            this.IODropDownButton,
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
            this.toolStripSeparator1,
            this.FileLanguageToolMenuItem,
            this.FileHelpMenuItem,
            this.FileAboutMenuItem,
            this.toolStripSeparator2,
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
            this.FileSaveMenuItem.Size = new System.Drawing.Size(126, 22);
            this.FileSaveMenuItem.Text = "Save";
            this.FileSaveMenuItem.Click += new System.EventHandler(this.FileSaveMenuItem_Click);
            // 
            // FileSaveAllMenuItem
            // 
            this.FileSaveAllMenuItem.Name = "FileSaveAllMenuItem";
            this.FileSaveAllMenuItem.Size = new System.Drawing.Size(126, 22);
            this.FileSaveAllMenuItem.Text = "Save all";
            this.FileSaveAllMenuItem.Click += new System.EventHandler(this.FileSaveAllMenuItem_Click);
            // 
            // FileLoadMenuItem
            // 
            this.FileLoadMenuItem.Name = "FileLoadMenuItem";
            this.FileLoadMenuItem.Size = new System.Drawing.Size(126, 22);
            this.FileLoadMenuItem.Text = "Load";
            this.FileLoadMenuItem.Click += new System.EventHandler(this.FileLoadMenuItem_Click);
            // 
            // FileLoadAllMenuItem
            // 
            this.FileLoadAllMenuItem.Name = "FileLoadAllMenuItem";
            this.FileLoadAllMenuItem.Size = new System.Drawing.Size(126, 22);
            this.FileLoadAllMenuItem.Text = "Load all";
            this.FileLoadAllMenuItem.Click += new System.EventHandler(this.FileLoadAllMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(123, 6);
            // 
            // FileLanguageToolMenuItem
            // 
            this.FileLanguageToolMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileLanguageENMenuItem,
            this.FileLanguageLTMenuItem});
            this.FileLanguageToolMenuItem.Name = "FileLanguageToolMenuItem";
            this.FileLanguageToolMenuItem.Size = new System.Drawing.Size(126, 22);
            this.FileLanguageToolMenuItem.Text = "Language";
            // 
            // FileLanguageENMenuItem
            // 
            this.FileLanguageENMenuItem.Name = "FileLanguageENMenuItem";
            this.FileLanguageENMenuItem.Size = new System.Drawing.Size(89, 22);
            this.FileLanguageENMenuItem.Text = "EN";
            this.FileLanguageENMenuItem.Click += new System.EventHandler(this.FileLanguage_Click);
            // 
            // FileLanguageLTMenuItem
            // 
            this.FileLanguageLTMenuItem.Name = "FileLanguageLTMenuItem";
            this.FileLanguageLTMenuItem.Size = new System.Drawing.Size(89, 22);
            this.FileLanguageLTMenuItem.Text = "LT";
            this.FileLanguageLTMenuItem.Click += new System.EventHandler(this.FileLanguage_Click);
            // 
            // FileHelpMenuItem
            // 
            this.FileHelpMenuItem.Name = "FileHelpMenuItem";
            this.FileHelpMenuItem.Size = new System.Drawing.Size(126, 22);
            this.FileHelpMenuItem.Text = "Help";
            this.FileHelpMenuItem.Click += new System.EventHandler(this.FileHelpMenuItem_Click);
            // 
            // FileAboutMenuItem
            // 
            this.FileAboutMenuItem.Name = "FileAboutMenuItem";
            this.FileAboutMenuItem.Size = new System.Drawing.Size(126, 22);
            this.FileAboutMenuItem.Text = "About";
            this.FileAboutMenuItem.Click += new System.EventHandler(this.FileAboutMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(123, 6);
            // 
            // FileExitMenuItem
            // 
            this.FileExitMenuItem.Name = "FileExitMenuItem";
            this.FileExitMenuItem.Size = new System.Drawing.Size(126, 22);
            this.FileExitMenuItem.Text = "Exit";
            this.FileExitMenuItem.Click += new System.EventHandler(this.FileExitMenuItem_Click);
            // 
            // ProjectDropDownButton
            // 
            this.ProjectDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ProjectDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProjectGetDataFromDesignMenuItem,
            this.ProjectCompareDesignMenuItem,
            this.ProjectTransferDataMenuItem,
            this.toolStripSeparator3,
            this.ProjectCPUMenuItem,
            this.ProjectSCADAMenuItem,
            this.ProjectLanguageMenuItem});
            this.ProjectDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("ProjectDropDownButton.Image")));
            this.ProjectDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ProjectDropDownButton.Name = "ProjectDropDownButton";
            this.ProjectDropDownButton.Size = new System.Drawing.Size(57, 22);
            this.ProjectDropDownButton.Text = "Project";
            this.ProjectDropDownButton.DropDownOpened += new System.EventHandler(this.ProjectDropDownButton_DropDownOpened);
            // 
            // ProjectGetDataFromDesignMenuItem
            // 
            this.ProjectGetDataFromDesignMenuItem.Name = "ProjectGetDataFromDesignMenuItem";
            this.ProjectGetDataFromDesignMenuItem.Size = new System.Drawing.Size(212, 22);
            this.ProjectGetDataFromDesignMenuItem.Text = "Get Data From Design";
            this.ProjectGetDataFromDesignMenuItem.Click += new System.EventHandler(this.ProjectGetDataFromDesignMenuItem_Click);
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(209, 6);
            // 
            // ProjectCPUMenuItem
            // 
            this.ProjectCPUMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProjectCPUAddMenuItem});
            this.ProjectCPUMenuItem.Name = "ProjectCPUMenuItem";
            this.ProjectCPUMenuItem.Size = new System.Drawing.Size(212, 22);
            this.ProjectCPUMenuItem.Text = "CPU";
            this.ProjectCPUMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ProjectCPUMenuItem_DropDownItemClicked);
            this.ProjectCPUMenuItem.MouseEnter += new System.EventHandler(this.ProjectCPUMenuItem_MouseEnter);
            // 
            // ProjectCPUAddMenuItem
            // 
            this.ProjectCPUAddMenuItem.Name = "ProjectCPUAddMenuItem";
            this.ProjectCPUAddMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ProjectCPUAddMenuItem.Tag = "Add";
            this.ProjectCPUAddMenuItem.Text = "Add";
            // 
            // ProjectSCADAMenuItem
            // 
            this.ProjectSCADAMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProjectSCADAAddMenuItem});
            this.ProjectSCADAMenuItem.Name = "ProjectSCADAMenuItem";
            this.ProjectSCADAMenuItem.Size = new System.Drawing.Size(212, 22);
            this.ProjectSCADAMenuItem.Text = "SCADA";
            this.ProjectSCADAMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ProjectSCADAMenuItem_DropDownItemClicked);
            this.ProjectSCADAMenuItem.MouseEnter += new System.EventHandler(this.ProjectSCADAMenuItem_MouseEnter);
            // 
            // ProjectSCADAAddMenuItem
            // 
            this.ProjectSCADAAddMenuItem.Name = "ProjectSCADAAddMenuItem";
            this.ProjectSCADAAddMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ProjectSCADAAddMenuItem.Tag = "Add";
            this.ProjectSCADAAddMenuItem.Text = "Add";
            // 
            // ProjectLanguageMenuItem
            // 
            this.ProjectLanguageMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProjectLanguageAddMenuItem});
            this.ProjectLanguageMenuItem.Name = "ProjectLanguageMenuItem";
            this.ProjectLanguageMenuItem.Size = new System.Drawing.Size(212, 22);
            this.ProjectLanguageMenuItem.Text = "Language";
            this.ProjectLanguageMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ProjectLanguageMenuItem_DropDownItemClicked);
            this.ProjectLanguageMenuItem.MouseEnter += new System.EventHandler(this.ProjectLanguageMenuItem_MouseEnter);
            // 
            // ProjectLanguageAddMenuItem
            // 
            this.ProjectLanguageAddMenuItem.Name = "ProjectLanguageAddMenuItem";
            this.ProjectLanguageAddMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ProjectLanguageAddMenuItem.Tag = "Add";
            this.ProjectLanguageAddMenuItem.Text = "Add";
            // 
            // DataDropDownButton
            // 
            this.DataDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DataDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DataFindFunctionMenuItem,
            this.DataKKSCombineMenuItem});
            this.DataDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("DataDropDownButton.Image")));
            this.DataDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DataDropDownButton.Name = "DataDropDownButton";
            this.DataDropDownButton.Size = new System.Drawing.Size(44, 22);
            this.DataDropDownButton.Text = "Data";
            // 
            // DataFindFunctionMenuItem
            // 
            this.DataFindFunctionMenuItem.Name = "DataFindFunctionMenuItem";
            this.DataFindFunctionMenuItem.Size = new System.Drawing.Size(146, 22);
            this.DataFindFunctionMenuItem.Text = "Find function";
            this.DataFindFunctionMenuItem.Click += new System.EventHandler(this.DataFindFunctionMenuItem_Click);
            // 
            // DataKKSCombineMenuItem
            // 
            this.DataKKSCombineMenuItem.Name = "DataKKSCombineMenuItem";
            this.DataKKSCombineMenuItem.Size = new System.Drawing.Size(146, 22);
            this.DataKKSCombineMenuItem.Text = "KKS Combine";
            this.DataKKSCombineMenuItem.Click += new System.EventHandler(this.DataKKSCombineMenuItem_Click);
            // 
            // ObjectsDropDownButton
            // 
            this.ObjectsDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ObjectsDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ObjectsFindMenuItem,
            this.ObjectsFindTypeMenuItem,
            this.ObjectTransferToDataMenuItem,
            this.toolStripSeparator7,
            this.ObjectEditTypesMenuItem});
            this.ObjectsDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("ObjectsDropDownButton.Image")));
            this.ObjectsDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ObjectsDropDownButton.Name = "ObjectsDropDownButton";
            this.ObjectsDropDownButton.Size = new System.Drawing.Size(60, 22);
            this.ObjectsDropDownButton.Text = "Objects";
            // 
            // ObjectsFindMenuItem
            // 
            this.ObjectsFindMenuItem.Name = "ObjectsFindMenuItem";
            this.ObjectsFindMenuItem.Size = new System.Drawing.Size(164, 22);
            this.ObjectsFindMenuItem.Text = "Find Uniques";
            this.ObjectsFindMenuItem.Click += new System.EventHandler(this.ObjectsFindMenuItem_Click);
            // 
            // ObjectsFindTypeMenuItem
            // 
            this.ObjectsFindTypeMenuItem.Name = "ObjectsFindTypeMenuItem";
            this.ObjectsFindTypeMenuItem.Size = new System.Drawing.Size(164, 22);
            this.ObjectsFindTypeMenuItem.Text = "Find object types";
            this.ObjectsFindTypeMenuItem.Click += new System.EventHandler(this.ObjectsFindTypeMenuItem_Click);
            // 
            // ObjectTransferToDataMenuItem
            // 
            this.ObjectTransferToDataMenuItem.Name = "ObjectTransferToDataMenuItem";
            this.ObjectTransferToDataMenuItem.Size = new System.Drawing.Size(164, 22);
            this.ObjectTransferToDataMenuItem.Text = "Transfer to data";
            this.ObjectTransferToDataMenuItem.Click += new System.EventHandler(this.ObjectTransferToDataMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(161, 6);
            // 
            // ObjectEditTypesMenuItem
            // 
            this.ObjectEditTypesMenuItem.Name = "ObjectEditTypesMenuItem";
            this.ObjectEditTypesMenuItem.Size = new System.Drawing.Size(164, 22);
            this.ObjectEditTypesMenuItem.Text = "Edit object types";
            this.ObjectEditTypesMenuItem.Click += new System.EventHandler(this.ObjectEditTypesMenuItem_Click);
            // 
            // IODropDownButton
            // 
            this.IODropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.IODropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IOFindModulesMenuItem,
            this.toolStripSeparator8,
            this.IOEditMenuItem});
            this.IODropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("IODropDownButton.Image")));
            this.IODropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.IODropDownButton.Name = "IODropDownButton";
            this.IODropDownButton.Size = new System.Drawing.Size(32, 22);
            this.IODropDownButton.Text = "IO";
            // 
            // IOFindModulesMenuItem
            // 
            this.IOFindModulesMenuItem.Name = "IOFindModulesMenuItem";
            this.IOFindModulesMenuItem.Size = new System.Drawing.Size(146, 22);
            this.IOFindModulesMenuItem.Text = "Find Modules";
            this.IOFindModulesMenuItem.Click += new System.EventHandler(this.IOFindModulesMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(143, 6);
            // 
            // IOEditMenuItem
            // 
            this.IOEditMenuItem.Name = "IOEditMenuItem";
            this.IOEditMenuItem.Size = new System.Drawing.Size(146, 22);
            this.IOEditMenuItem.Text = "Edit IO";
            this.IOEditMenuItem.Click += new System.EventHandler(this.IOEditMenuItem_Click);
            // 
            // DeclareDropDownButton
            // 
            this.DeclareDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DeclareDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeclareGenerateMenuItem,
            this.toolStripSeparator6,
            this.DeclareEditMenuItem});
            this.DeclareDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("DeclareDropDownButton.Image")));
            this.DeclareDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeclareDropDownButton.Name = "DeclareDropDownButton";
            this.DeclareDropDownButton.Size = new System.Drawing.Size(59, 22);
            this.DeclareDropDownButton.Text = "Declare";
            // 
            // DeclareGenerateMenuItem
            // 
            this.DeclareGenerateMenuItem.Name = "DeclareGenerateMenuItem";
            this.DeclareGenerateMenuItem.Size = new System.Drawing.Size(135, 22);
            this.DeclareGenerateMenuItem.Text = "Generate";
            this.DeclareGenerateMenuItem.Click += new System.EventHandler(this.DeclareGenerateMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(132, 6);
            // 
            // DeclareEditMenuItem
            // 
            this.DeclareEditMenuItem.Name = "DeclareEditMenuItem";
            this.DeclareEditMenuItem.Size = new System.Drawing.Size(135, 22);
            this.DeclareEditMenuItem.Text = "Edit declare";
            this.DeclareEditMenuItem.Click += new System.EventHandler(this.DeclareEditMenuItem_Click);
            // 
            // InstanceDropDownButton
            // 
            this.InstanceDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.InstanceDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InstancesGenerateMenuItem,
            this.toolStripSeparator4,
            this.InstancesEditMenuItem});
            this.InstanceDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("InstanceDropDownButton.Image")));
            this.InstanceDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.InstanceDropDownButton.Name = "InstanceDropDownButton";
            this.InstanceDropDownButton.Size = new System.Drawing.Size(64, 22);
            this.InstanceDropDownButton.Text = "Instance";
            // 
            // InstancesGenerateMenuItem
            // 
            this.InstancesGenerateMenuItem.Name = "InstancesGenerateMenuItem";
            this.InstancesGenerateMenuItem.Size = new System.Drawing.Size(146, 22);
            this.InstancesGenerateMenuItem.Text = "Generate";
            this.InstancesGenerateMenuItem.Click += new System.EventHandler(this.InstancesGenerateMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(143, 6);
            // 
            // InstancesEditMenuItem
            // 
            this.InstancesEditMenuItem.Name = "InstancesEditMenuItem";
            this.InstancesEditMenuItem.Size = new System.Drawing.Size(146, 22);
            this.InstancesEditMenuItem.Text = "Edit instances";
            this.InstancesEditMenuItem.Click += new System.EventHandler(this.InstancesEditMenuItem_Click);
            // 
            // SCADADropDownButton
            // 
            this.SCADADropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SCADADropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator5,
            this.SCADAEditMenuItem});
            this.SCADADropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("SCADADropDownButton.Image")));
            this.SCADADropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SCADADropDownButton.Name = "SCADADropDownButton";
            this.SCADADropDownButton.Size = new System.Drawing.Size(58, 22);
            this.SCADADropDownButton.Text = "SCADA";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(132, 6);
            // 
            // SCADAEditMenuItem
            // 
            this.SCADAEditMenuItem.Name = "SCADAEditMenuItem";
            this.SCADAEditMenuItem.Size = new System.Drawing.Size(135, 22);
            this.SCADAEditMenuItem.Text = "Edit SCADA";
            this.SCADAEditMenuItem.Click += new System.EventHandler(this.SCADAEditMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.DesignTab);
            this.tabControl1.Controls.Add(this.DataTab);
            this.tabControl1.Controls.Add(this.ObjectTab);
            this.tabControl1.Controls.Add(this.ModuleTab);
            this.tabControl1.Location = new System.Drawing.Point(0, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1284, 648);
            this.tabControl1.TabIndex = 8;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1_SelectedIndexChanged);
            this.tabControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_Event);
            // 
            // DesignTab
            // 
            this.DesignTab.Controls.Add(this.DesignGridView);
            this.DesignTab.Location = new System.Drawing.Point(4, 22);
            this.DesignTab.Name = "DesignTab";
            this.DesignTab.Size = new System.Drawing.Size(1276, 622);
            this.DesignTab.TabIndex = 0;
            this.DesignTab.Text = "Design";
            this.DesignTab.UseVisualStyleBackColor = true;
            // 
            // DesignGridView
            // 
            this.DesignGridView.AllowUserToOrderColumns = true;
            this.DesignGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DesignGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DesignGridView.Location = new System.Drawing.Point(0, 0);
            this.DesignGridView.Name = "DesignGridView";
            this.DesignGridView.Size = new System.Drawing.Size(1276, 622);
            this.DesignGridView.TabIndex = 0;
            this.DesignGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DesignGridView_ColumnHeaderMouseClick);
            // 
            // DataTab
            // 
            this.DataTab.Controls.Add(this.DataGridView);
            this.DataTab.Location = new System.Drawing.Point(4, 22);
            this.DataTab.Name = "DataTab";
            this.DataTab.Size = new System.Drawing.Size(1276, 622);
            this.DataTab.TabIndex = 1;
            this.DataTab.Text = "Data";
            this.DataTab.UseVisualStyleBackColor = true;
            // 
            // DataGridView
            // 
            this.DataGridView.AllowUserToOrderColumns = true;
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridView.Location = new System.Drawing.Point(0, 0);
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.Size = new System.Drawing.Size(1276, 622);
            this.DataGridView.TabIndex = 1;
            this.DataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_ColumnHeaderMouseClick);
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
            this.ObjectsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ObjectsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ObjectsGridView.Location = new System.Drawing.Point(0, 0);
            this.ObjectsGridView.Name = "ObjectsGridView";
            this.ObjectsGridView.Size = new System.Drawing.Size(1276, 622);
            this.ObjectsGridView.TabIndex = 1;
            this.ObjectsGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ObjectsGridView_ColumnHeaderMouseClick);
            // 
            // ModuleTab
            // 
            this.ModuleTab.Controls.Add(this.ModulesGridView);
            this.ModuleTab.Location = new System.Drawing.Point(4, 22);
            this.ModuleTab.Name = "ModuleTab";
            this.ModuleTab.Size = new System.Drawing.Size(1276, 622);
            this.ModuleTab.TabIndex = 3;
            this.ModuleTab.Text = "Module";
            this.ModuleTab.UseVisualStyleBackColor = true;
            // 
            // ModulesGridView
            // 
            this.ModulesGridView.AllowUserToOrderColumns = true;
            this.ModulesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ModulesGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ModulesGridView.Location = new System.Drawing.Point(0, 0);
            this.ModulesGridView.Name = "ModulesGridView";
            this.ModulesGridView.Size = new System.Drawing.Size(1276, 622);
            this.ModulesGridView.TabIndex = 2;
            this.ModulesGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ModulesGridView_ColumnHeaderMouseClick);
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
            // comboBoxColumn
            // 
            this.comboBoxColumn.DisplayMember = "jj";
            this.comboBoxColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxColumn.FormattingEnabled = true;
            this.comboBoxColumn.Location = new System.Drawing.Point(881, 2);
            this.comboBoxColumn.Name = "comboBoxColumn";
            this.comboBoxColumn.Size = new System.Drawing.Size(101, 21);
            this.comboBoxColumn.TabIndex = 11;
            this.comboBoxColumn.Visible = false;
            this.comboBoxColumn.SelectedValueChanged += new System.EventHandler(this.ComboBoxColumn_SelectedValueChanged);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 691);
            this.Controls.Add(this.comboBoxColumn);
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
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_Event);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.DesignTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DesignGridView)).EndInit();
            this.DataTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            this.ObjectTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ObjectsGridView)).EndInit();
            this.ModuleTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ModulesGridView)).EndInit();
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
        private System.Windows.Forms.ToolStripDropDownButton ObjectsDropDownButton;
        private System.Windows.Forms.ToolStripDropDownButton DeclareDropDownButton;
        private System.Windows.Forms.ToolStripDropDownButton InstanceDropDownButton;
        private System.Windows.Forms.ToolStripDropDownButton SCADADropDownButton;
        private System.Windows.Forms.ToolStripMenuItem ProjectGetDataFromDesignMenuItem;
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
        private ToolStripMenuItem DataKKSCombineMenuItem;
        private ComboBox comboBoxColumn;
        private ToolStripMenuItem ObjectsFindMenuItem;
        private ToolStripMenuItem ObjectTransferToDataMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem ProjectCPUMenuItem;
        private ToolStripMenuItem ProjectSCADAMenuItem;
        private ToolStripMenuItem ProjectLanguageMenuItem;
        private ToolStripMenuItem FileLanguageToolMenuItem;
        private ToolStripMenuItem FileLanguageENMenuItem;
        private ToolStripMenuItem FileLanguageLTMenuItem;
        private ToolStripMenuItem ProjectCPUAddMenuItem;
        private ToolStripMenuItem ProjectSCADAAddMenuItem;
        private ToolStripMenuItem ProjectLanguageAddMenuItem;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem ObjectEditTypesMenuItem;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem DeclareEditMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem InstancesEditMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem SCADAEditMenuItem;
        private ToolStripDropDownButton IODropDownButton;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripMenuItem IOEditMenuItem;
        private ToolStripMenuItem ObjectsFindTypeMenuItem;
        private ToolStripMenuItem InstancesGenerateMenuItem;
        private ToolStripMenuItem DeclareGenerateMenuItem;
        private TabPage ModuleTab;
        private DataGridView ModulesGridView;
        private ToolStripMenuItem IOFindModulesMenuItem;
    }
}

