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
            this.MainToolStrip = new System.Windows.Forms.ToolStrip();
            this.File_DropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.File_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.File_SaveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.File_Load = new System.Windows.Forms.ToolStripMenuItem();
            this.File_LoadAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.File_Language = new System.Windows.Forms.ToolStripMenuItem();
            this.File_Language_EN = new System.Windows.Forms.ToolStripMenuItem();
            this.File_Language_LT = new System.Windows.Forms.ToolStripMenuItem();
            this.File_DebugLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.File_DebugLevel_None = new System.Windows.Forms.ToolStripMenuItem();
            this.File_DebugLevel_Minimum = new System.Windows.Forms.ToolStripMenuItem();
            this.File_DebugLevel_High = new System.Windows.Forms.ToolStripMenuItem();
            this.File_DebugLevel_Development = new System.Windows.Forms.ToolStripMenuItem();
            this.File_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.File_About = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.File_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.Project_DropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.Project_GetDataFromDesign = new System.Windows.Forms.ToolStripMenuItem();
            this.Project_CompareDesign = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.Project_CPU = new System.Windows.Forms.ToolStripMenuItem();
            this.Project_CPU_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.Project_SCADA = new System.Windows.Forms.ToolStripMenuItem();
            this.Project_SCADA_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.Project_Language = new System.Windows.Forms.ToolStripMenuItem();
            this.Project_Language_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.Data_DropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.Data_GetDataFromProject = new System.Windows.Forms.ToolStripMenuItem();
            this.DataFindFunctionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Data_KKSCombine = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.Data_EditFunctions = new System.Windows.Forms.ToolStripMenuItem();
            this.IO_DropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.IO_FindModules = new System.Windows.Forms.ToolStripMenuItem();
            this.IO_Generate = new System.Windows.Forms.ToolStripMenuItem();
            this.IO_ClearAddresses = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.IO_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.SCADA_DropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.SCADA_GenerateObjects = new System.Windows.Forms.ToolStripMenuItem();
            this.SCADA_GenerateModules = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.SCADA_EditObjects = new System.Windows.Forms.ToolStripMenuItem();
            this.SCADA_EditModules = new System.Windows.Forms.ToolStripMenuItem();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.DesignTab = new System.Windows.Forms.TabPage();
            this.DesignGridView = new System.Windows.Forms.DataGridView();
            this.DataTab = new System.Windows.Forms.TabPage();
            this.DataGridView = new System.Windows.Forms.DataGridView();
            this.ObjectTab = new System.Windows.Forms.TabPage();
            this.ObjectsGridView = new System.Windows.Forms.DataGridView();
            this.ModuleTab = new System.Windows.Forms.TabPage();
            this.ModulesGridView = new System.Windows.Forms.DataGridView();
            this.AddressTab = new System.Windows.Forms.TabPage();
            this.AddressesGridView = new System.Windows.Forms.DataGridView();
            this.FindTextBox = new System.Windows.Forms.TextBox();
            this.FindButton = new System.Windows.Forms.Button();
            this.Objects_Find = new System.Windows.Forms.ToolStripMenuItem();
            this.ObjectsFindTypeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Object_TransferToData = new System.Windows.Forms.ToolStripMenuItem();
            this.Objects_DeclareGenerate = new System.Windows.Forms.ToolStripMenuItem();
            this.Objects_InstancesGenerate = new System.Windows.Forms.ToolStripMenuItem();
            this.Objects_ClearAddresses = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.Object_EditTypes = new System.Windows.Forms.ToolStripMenuItem();
            this.Objects_DeclareEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.Objects_InstancesEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.Objects_DropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.MainToolStrip.SuspendLayout();
            this.MainTabControl.SuspendLayout();
            this.DesignTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DesignGridView)).BeginInit();
            this.DataTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            this.ObjectTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectsGridView)).BeginInit();
            this.ModuleTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ModulesGridView)).BeginInit();
            this.AddressTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AddressesGridView)).BeginInit();
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
            // MainToolStrip
            // 
            this.MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File_DropDown,
            this.Project_DropDown,
            this.Data_DropDown,
            this.Objects_DropDown,
            this.IO_DropDown,
            this.SCADA_DropDown});
            this.MainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.MainToolStrip.Name = "MainToolStrip";
            this.MainToolStrip.Size = new System.Drawing.Size(1284, 25);
            this.MainToolStrip.TabIndex = 7;
            this.MainToolStrip.Text = "toolStrip1";
            this.MainToolStrip.Click += new System.EventHandler(this.MainToolStrip_Click);
            // 
            // File_DropDown
            // 
            this.File_DropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.File_DropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File_Save,
            this.File_SaveAll,
            this.File_Load,
            this.File_LoadAll,
            this.toolStripSeparator1,
            this.File_Language,
            this.File_DebugLevel,
            this.File_Help,
            this.File_About,
            this.toolStripSeparator2,
            this.File_Exit});
            this.File_DropDown.Image = ((System.Drawing.Image)(resources.GetObject("File_DropDown.Image")));
            this.File_DropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.File_DropDown.Name = "File_DropDown";
            this.File_DropDown.Size = new System.Drawing.Size(38, 22);
            this.File_DropDown.Text = "File";
            // 
            // File_Save
            // 
            this.File_Save.Name = "File_Save";
            this.File_Save.Size = new System.Drawing.Size(136, 22);
            this.File_Save.Text = "Save";
            this.File_Save.Click += new System.EventHandler(this.File_Save_Click);
            // 
            // File_SaveAll
            // 
            this.File_SaveAll.Name = "File_SaveAll";
            this.File_SaveAll.Size = new System.Drawing.Size(136, 22);
            this.File_SaveAll.Text = "Save all";
            this.File_SaveAll.Click += new System.EventHandler(this.File_SaveAll_Click);
            // 
            // File_Load
            // 
            this.File_Load.Name = "File_Load";
            this.File_Load.Size = new System.Drawing.Size(136, 22);
            this.File_Load.Text = "Load";
            this.File_Load.Click += new System.EventHandler(this.File_Load_Click);
            // 
            // File_LoadAll
            // 
            this.File_LoadAll.Name = "File_LoadAll";
            this.File_LoadAll.Size = new System.Drawing.Size(136, 22);
            this.File_LoadAll.Text = "Load all";
            this.File_LoadAll.Click += new System.EventHandler(this.File_LoadAll_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(133, 6);
            // 
            // File_Language
            // 
            this.File_Language.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File_Language_EN,
            this.File_Language_LT});
            this.File_Language.Name = "File_Language";
            this.File_Language.Size = new System.Drawing.Size(136, 22);
            this.File_Language.Text = "Language";
            // 
            // File_Language_EN
            // 
            this.File_Language_EN.Name = "File_Language_EN";
            this.File_Language_EN.Size = new System.Drawing.Size(89, 22);
            this.File_Language_EN.Text = "EN";
            this.File_Language_EN.Click += new System.EventHandler(this.FileLanguage_Click);
            // 
            // File_Language_LT
            // 
            this.File_Language_LT.Name = "File_Language_LT";
            this.File_Language_LT.Size = new System.Drawing.Size(89, 22);
            this.File_Language_LT.Text = "LT";
            this.File_Language_LT.Click += new System.EventHandler(this.FileLanguage_Click);
            // 
            // File_DebugLevel
            // 
            this.File_DebugLevel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File_DebugLevel_None,
            this.File_DebugLevel_Minimum,
            this.File_DebugLevel_High,
            this.File_DebugLevel_Development});
            this.File_DebugLevel.Name = "File_DebugLevel";
            this.File_DebugLevel.Size = new System.Drawing.Size(136, 22);
            this.File_DebugLevel.Text = "DebugLevel";
            // 
            // File_DebugLevel_None
            // 
            this.File_DebugLevel_None.Name = "File_DebugLevel_None";
            this.File_DebugLevel_None.Size = new System.Drawing.Size(145, 22);
            this.File_DebugLevel_None.Text = "None";
            this.File_DebugLevel_None.Click += new System.EventHandler(this.File_DebugLevel_None_Click);
            // 
            // File_DebugLevel_Minimum
            // 
            this.File_DebugLevel_Minimum.Name = "File_DebugLevel_Minimum";
            this.File_DebugLevel_Minimum.Size = new System.Drawing.Size(145, 22);
            this.File_DebugLevel_Minimum.Text = "Minimum";
            this.File_DebugLevel_Minimum.Click += new System.EventHandler(this.File_DebugLevel_Minimum_Click);
            // 
            // File_DebugLevel_High
            // 
            this.File_DebugLevel_High.Name = "File_DebugLevel_High";
            this.File_DebugLevel_High.Size = new System.Drawing.Size(145, 22);
            this.File_DebugLevel_High.Text = "High";
            this.File_DebugLevel_High.Click += new System.EventHandler(this.File_DebugLevel_High_Click);
            // 
            // File_DebugLevel_Development
            // 
            this.File_DebugLevel_Development.Name = "File_DebugLevel_Development";
            this.File_DebugLevel_Development.Size = new System.Drawing.Size(145, 22);
            this.File_DebugLevel_Development.Text = "Development";
            this.File_DebugLevel_Development.Click += new System.EventHandler(this.File_DebugLevel_Development_Click);
            // 
            // File_Help
            // 
            this.File_Help.Name = "File_Help";
            this.File_Help.Size = new System.Drawing.Size(136, 22);
            this.File_Help.Text = "Help";
            this.File_Help.Click += new System.EventHandler(this.File_Help_Click);
            // 
            // File_About
            // 
            this.File_About.Name = "File_About";
            this.File_About.Size = new System.Drawing.Size(136, 22);
            this.File_About.Text = "About";
            this.File_About.Click += new System.EventHandler(this.File_About_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(133, 6);
            // 
            // File_Exit
            // 
            this.File_Exit.Name = "File_Exit";
            this.File_Exit.Size = new System.Drawing.Size(136, 22);
            this.File_Exit.Text = "Exit";
            this.File_Exit.Click += new System.EventHandler(this.File_Exit_Click);
            // 
            // Project_DropDown
            // 
            this.Project_DropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Project_DropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Project_GetDataFromDesign,
            this.Project_CompareDesign,
            this.toolStripSeparator3,
            this.Project_CPU,
            this.Project_SCADA,
            this.Project_Language});
            this.Project_DropDown.Image = ((System.Drawing.Image)(resources.GetObject("Project_DropDown.Image")));
            this.Project_DropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Project_DropDown.Name = "Project_DropDown";
            this.Project_DropDown.Size = new System.Drawing.Size(57, 22);
            this.Project_DropDown.Text = "Project";
            this.Project_DropDown.DropDownOpened += new System.EventHandler(this.Project_DropDown_DropDownOpened);
            // 
            // Project_GetDataFromDesign
            // 
            this.Project_GetDataFromDesign.Name = "Project_GetDataFromDesign";
            this.Project_GetDataFromDesign.Size = new System.Drawing.Size(212, 22);
            this.Project_GetDataFromDesign.Text = "Get Data From Design";
            this.Project_GetDataFromDesign.Click += new System.EventHandler(this.Project_GetDataFromDesign_Click);
            // 
            // Project_CompareDesign
            // 
            this.Project_CompareDesign.Name = "Project_CompareDesign";
            this.Project_CompareDesign.Size = new System.Drawing.Size(212, 22);
            this.Project_CompareDesign.Text = "Compare with new design";
            this.Project_CompareDesign.Click += new System.EventHandler(this.Project_CompareDesign_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(209, 6);
            // 
            // Project_CPU
            // 
            this.Project_CPU.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Project_CPU_Add});
            this.Project_CPU.Name = "Project_CPU";
            this.Project_CPU.Size = new System.Drawing.Size(212, 22);
            this.Project_CPU.Text = "CPU";
            this.Project_CPU.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.Project_CPU_DropDownItemClicked);
            // 
            // Project_CPU_Add
            // 
            this.Project_CPU_Add.Name = "Project_CPU_Add";
            this.Project_CPU_Add.Size = new System.Drawing.Size(96, 22);
            this.Project_CPU_Add.Tag = "Add";
            this.Project_CPU_Add.Text = "Add";
            // 
            // Project_SCADA
            // 
            this.Project_SCADA.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Project_SCADA_Add});
            this.Project_SCADA.Name = "Project_SCADA";
            this.Project_SCADA.Size = new System.Drawing.Size(212, 22);
            this.Project_SCADA.Text = "SCADA";
            this.Project_SCADA.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.Project_SCADA_DropDownItemClicked);
            // 
            // Project_SCADA_Add
            // 
            this.Project_SCADA_Add.Name = "Project_SCADA_Add";
            this.Project_SCADA_Add.Size = new System.Drawing.Size(96, 22);
            this.Project_SCADA_Add.Tag = "Add";
            this.Project_SCADA_Add.Text = "Add";
            // 
            // Project_Language
            // 
            this.Project_Language.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Project_Language_Add});
            this.Project_Language.Name = "Project_Language";
            this.Project_Language.Size = new System.Drawing.Size(212, 22);
            this.Project_Language.Text = "Language";
            this.Project_Language.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.Project_Language_DropDownItemClicked);
            // 
            // Project_Language_Add
            // 
            this.Project_Language_Add.Name = "Project_Language_Add";
            this.Project_Language_Add.Size = new System.Drawing.Size(96, 22);
            this.Project_Language_Add.Tag = "Add";
            this.Project_Language_Add.Text = "Add";
            // 
            // Data_DropDown
            // 
            this.Data_DropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Data_DropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Data_GetDataFromProject,
            this.DataFindFunctionMenuItem,
            this.Data_KKSCombine,
            this.toolStripSeparator9,
            this.Data_EditFunctions});
            this.Data_DropDown.Image = ((System.Drawing.Image)(resources.GetObject("Data_DropDown.Image")));
            this.Data_DropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Data_DropDown.Name = "Data_DropDown";
            this.Data_DropDown.Size = new System.Drawing.Size(44, 22);
            this.Data_DropDown.Text = "Data";
            // 
            // Data_GetDataFromProject
            // 
            this.Data_GetDataFromProject.Name = "Data_GetDataFromProject";
            this.Data_GetDataFromProject.Size = new System.Drawing.Size(193, 22);
            this.Data_GetDataFromProject.Text = "Transfer design to data";
            this.Data_GetDataFromProject.Click += new System.EventHandler(this.Data_GetDataFromProject_Click);
            // 
            // DataFindFunctionMenuItem
            // 
            this.DataFindFunctionMenuItem.Name = "DataFindFunctionMenuItem";
            this.DataFindFunctionMenuItem.Size = new System.Drawing.Size(193, 22);
            this.DataFindFunctionMenuItem.Text = "Find function";
            this.DataFindFunctionMenuItem.Click += new System.EventHandler(this.DataFindFunctionMenuItem_Click);
            // 
            // Data_KKSCombine
            // 
            this.Data_KKSCombine.Name = "Data_KKSCombine";
            this.Data_KKSCombine.Size = new System.Drawing.Size(193, 22);
            this.Data_KKSCombine.Text = "KKS Combine";
            this.Data_KKSCombine.Click += new System.EventHandler(this.Data_KKSCombine_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(190, 6);
            // 
            // Data_EditFunctions
            // 
            this.Data_EditFunctions.Name = "Data_EditFunctions";
            this.Data_EditFunctions.Size = new System.Drawing.Size(193, 22);
            this.Data_EditFunctions.Text = "EditFunctions";
            this.Data_EditFunctions.Click += new System.EventHandler(this.Data_EditFunctions_Click);
            // 
            // IO_DropDown
            // 
            this.IO_DropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.IO_DropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IO_FindModules,
            this.IO_Generate,
            this.IO_ClearAddresses,
            this.toolStripSeparator8,
            this.IO_Edit});
            this.IO_DropDown.Image = ((System.Drawing.Image)(resources.GetObject("IO_DropDown.Image")));
            this.IO_DropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.IO_DropDown.Name = "IO_DropDown";
            this.IO_DropDown.Size = new System.Drawing.Size(32, 22);
            this.IO_DropDown.Text = "IO";
            // 
            // IO_FindModules
            // 
            this.IO_FindModules.Name = "IO_FindModules";
            this.IO_FindModules.Size = new System.Drawing.Size(180, 22);
            this.IO_FindModules.Text = "Find Modules";
            this.IO_FindModules.Click += new System.EventHandler(this.IO_FindModules_Click);
            // 
            // IO_Generate
            // 
            this.IO_Generate.Name = "IO_Generate";
            this.IO_Generate.Size = new System.Drawing.Size(180, 22);
            this.IO_Generate.Text = "IO Generate";
            this.IO_Generate.Click += new System.EventHandler(this.IO_Generate_Click);
            // 
            // IO_ClearAddresses
            // 
            this.IO_ClearAddresses.Name = "IO_ClearAddresses";
            this.IO_ClearAddresses.Size = new System.Drawing.Size(180, 22);
            this.IO_ClearAddresses.Text = "ClearAddresses";
            this.IO_ClearAddresses.Click += new System.EventHandler(this.ClearAddresses_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(177, 6);
            // 
            // IO_Edit
            // 
            this.IO_Edit.Name = "IO_Edit";
            this.IO_Edit.Size = new System.Drawing.Size(180, 22);
            this.IO_Edit.Text = "Edit IO";
            this.IO_Edit.Click += new System.EventHandler(this.IO_Edit_Click);
            // 
            // SCADA_DropDown
            // 
            this.SCADA_DropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SCADA_DropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SCADA_GenerateObjects,
            this.SCADA_GenerateModules,
            this.toolStripSeparator5,
            this.SCADA_EditObjects,
            this.SCADA_EditModules});
            this.SCADA_DropDown.Image = ((System.Drawing.Image)(resources.GetObject("SCADA_DropDown.Image")));
            this.SCADA_DropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SCADA_DropDown.Name = "SCADA_DropDown";
            this.SCADA_DropDown.Size = new System.Drawing.Size(58, 22);
            this.SCADA_DropDown.Text = "SCADA";
            // 
            // SCADA_GenerateObjects
            // 
            this.SCADA_GenerateObjects.Name = "SCADA_GenerateObjects";
            this.SCADA_GenerateObjects.Size = new System.Drawing.Size(167, 22);
            this.SCADA_GenerateObjects.Text = "GenerateObjects";
            this.SCADA_GenerateObjects.Click += new System.EventHandler(this.SCADA_GenerateObjects_Click);
            // 
            // SCADA_GenerateModules
            // 
            this.SCADA_GenerateModules.Name = "SCADA_GenerateModules";
            this.SCADA_GenerateModules.Size = new System.Drawing.Size(167, 22);
            this.SCADA_GenerateModules.Text = "GenerateModules";
            this.SCADA_GenerateModules.Click += new System.EventHandler(this.SCADA_GenerateModules_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(164, 6);
            // 
            // SCADA_EditObjects
            // 
            this.SCADA_EditObjects.Name = "SCADA_EditObjects";
            this.SCADA_EditObjects.Size = new System.Drawing.Size(167, 22);
            this.SCADA_EditObjects.Text = "EditObjects";
            this.SCADA_EditObjects.Click += new System.EventHandler(this.SCADA_EditObjects_Click);
            // 
            // SCADA_EditModules
            // 
            this.SCADA_EditModules.Name = "SCADA_EditModules";
            this.SCADA_EditModules.Size = new System.Drawing.Size(167, 22);
            this.SCADA_EditModules.Text = "EditModules";
            this.SCADA_EditModules.Click += new System.EventHandler(this.SCADA_EditModules_Click);
            // 
            // MainTabControl
            // 
            this.MainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTabControl.Controls.Add(this.DesignTab);
            this.MainTabControl.Controls.Add(this.DataTab);
            this.MainTabControl.Controls.Add(this.ObjectTab);
            this.MainTabControl.Controls.Add(this.ModuleTab);
            this.MainTabControl.Controls.Add(this.AddressTab);
            this.MainTabControl.Location = new System.Drawing.Point(0, 28);
            this.MainTabControl.Margin = new System.Windows.Forms.Padding(0);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(1284, 648);
            this.MainTabControl.TabIndex = 8;
            this.MainTabControl.Click += new System.EventHandler(this.MainTabControl_Click);
            this.MainTabControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_Event);
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
            this.DesignGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DesignGridView_CellClick);
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
            this.DataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellClick);
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
            this.ObjectsGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ObjectsGridView_CellClick);
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
            this.ModulesGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ModulesGridView_CellClick);
            this.ModulesGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ModulesGridView_ColumnHeaderMouseClick);
            // 
            // AddressTab
            // 
            this.AddressTab.Controls.Add(this.AddressesGridView);
            this.AddressTab.Location = new System.Drawing.Point(4, 22);
            this.AddressTab.Name = "AddressTab";
            this.AddressTab.Size = new System.Drawing.Size(1276, 622);
            this.AddressTab.TabIndex = 4;
            this.AddressTab.Text = "Address";
            this.AddressTab.UseVisualStyleBackColor = true;
            // 
            // AddressesGridView
            // 
            this.AddressesGridView.AllowUserToAddRows = false;
            this.AddressesGridView.AllowUserToDeleteRows = false;
            this.AddressesGridView.AllowUserToResizeColumns = false;
            this.AddressesGridView.AllowUserToResizeRows = false;
            this.AddressesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AddressesGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AddressesGridView.Location = new System.Drawing.Point(0, 0);
            this.AddressesGridView.Name = "AddressesGridView";
            this.AddressesGridView.ReadOnly = true;
            this.AddressesGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.AddressesGridView.Size = new System.Drawing.Size(1276, 622);
            this.AddressesGridView.TabIndex = 3;
            this.AddressesGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AddressesGridView_CellClick);
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
            this.FindButton.Click += new System.EventHandler(this.FindButton_Click);
            // 
            // Objects_Find
            // 
            this.Objects_Find.Name = "Objects_Find";
            this.Objects_Find.Size = new System.Drawing.Size(215, 22);
            this.Objects_Find.Text = "Find Unique";
            this.Objects_Find.Click += new System.EventHandler(this.Objects_Find_Click);
            // 
            // ObjectsFindTypeMenuItem
            // 
            this.ObjectsFindTypeMenuItem.Name = "ObjectsFindTypeMenuItem";
            this.ObjectsFindTypeMenuItem.Size = new System.Drawing.Size(215, 22);
            this.ObjectsFindTypeMenuItem.Text = "Find object types";
            this.ObjectsFindTypeMenuItem.Click += new System.EventHandler(this.ObjectsFindTypeMenuItem_Click);
            // 
            // Object_TransferToData
            // 
            this.Object_TransferToData.Name = "Object_TransferToData";
            this.Object_TransferToData.Size = new System.Drawing.Size(215, 22);
            this.Object_TransferToData.Text = "Transfer to data";
            this.Object_TransferToData.Click += new System.EventHandler(this.Object_TransferToData_Click);
            // 
            // Objects_DeclareGenerate
            // 
            this.Objects_DeclareGenerate.Name = "Objects_DeclareGenerate";
            this.Objects_DeclareGenerate.Size = new System.Drawing.Size(215, 22);
            this.Objects_DeclareGenerate.Text = "Objects_DeclareGenerate";
            this.Objects_DeclareGenerate.Click += new System.EventHandler(this.Objects_DeclareGenerate_Click);
            // 
            // Objects_InstancesGenerate
            // 
            this.Objects_InstancesGenerate.Name = "Objects_InstancesGenerate";
            this.Objects_InstancesGenerate.Size = new System.Drawing.Size(215, 22);
            this.Objects_InstancesGenerate.Text = "Objects_InstancesGenerate";
            this.Objects_InstancesGenerate.Click += new System.EventHandler(this.Objects_InstancesGenerate_Click);
            // 
            // Objects_ClearAddresses
            // 
            this.Objects_ClearAddresses.Name = "Objects_ClearAddresses";
            this.Objects_ClearAddresses.Size = new System.Drawing.Size(215, 22);
            this.Objects_ClearAddresses.Text = "ClearAddresses";
            this.Objects_ClearAddresses.Click += new System.EventHandler(this.ClearAddresses_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(212, 6);
            // 
            // Object_EditTypes
            // 
            this.Object_EditTypes.Name = "Object_EditTypes";
            this.Object_EditTypes.Size = new System.Drawing.Size(215, 22);
            this.Object_EditTypes.Text = "Edit object types";
            this.Object_EditTypes.Click += new System.EventHandler(this.Object_EditTypes_Click);
            // 
            // Objects_DeclareEdit
            // 
            this.Objects_DeclareEdit.Name = "Objects_DeclareEdit";
            this.Objects_DeclareEdit.Size = new System.Drawing.Size(215, 22);
            this.Objects_DeclareEdit.Text = "Edit declare";
            this.Objects_DeclareEdit.Click += new System.EventHandler(this.Objects_DeclareEdit_Click);
            // 
            // Objects_InstancesEdit
            // 
            this.Objects_InstancesEdit.Name = "Objects_InstancesEdit";
            this.Objects_InstancesEdit.Size = new System.Drawing.Size(215, 22);
            this.Objects_InstancesEdit.Text = "Edit instances";
            this.Objects_InstancesEdit.Click += new System.EventHandler(this.Objects_InstancesEdit_Click);
            // 
            // Objects_DropDown
            // 
            this.Objects_DropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Objects_DropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Objects_Find,
            this.ObjectsFindTypeMenuItem,
            this.Object_TransferToData,
            this.Objects_DeclareGenerate,
            this.Objects_InstancesGenerate,
            this.Objects_ClearAddresses,
            this.toolStripSeparator4,
            this.Object_EditTypes,
            this.Objects_DeclareEdit,
            this.Objects_InstancesEdit});
            this.Objects_DropDown.Image = ((System.Drawing.Image)(resources.GetObject("Objects_DropDown.Image")));
            this.Objects_DropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Objects_DropDown.Name = "Objects_DropDown";
            this.Objects_DropDown.Size = new System.Drawing.Size(60, 22);
            this.Objects_DropDown.Text = "Objects";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 691);
            this.Controls.Add(this.FindButton);
            this.Controls.Add(this.FindTextBox);
            this.Controls.Add(this.MainTabControl);
            this.Controls.Add(this.MainToolStrip);
            this.Controls.Add(this.ProgressLabel);
            this.Controls.Add(this.ProgressBars);
            this.DoubleBuffered = true;
            this.Name = "MainWindow";
            this.Text = "IO List Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Shown += new System.EventHandler(this.MainWindow_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_Event);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseClick);
            this.MainToolStrip.ResumeLayout(false);
            this.MainToolStrip.PerformLayout();
            this.MainTabControl.ResumeLayout(false);
            this.DesignTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DesignGridView)).EndInit();
            this.DataTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            this.ObjectTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ObjectsGridView)).EndInit();
            this.ModuleTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ModulesGridView)).EndInit();
            this.AddressTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AddressesGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label ProgressLabel;
        private System.Windows.Forms.ProgressBar ProgressBars;
        private System.Windows.Forms.ToolStrip MainToolStrip;
        private System.Windows.Forms.ToolStripDropDownButton File_DropDown;
        private System.Windows.Forms.ToolStripMenuItem File_Save;
        private System.Windows.Forms.ToolStripMenuItem File_SaveAll;
        private System.Windows.Forms.ToolStripMenuItem File_Load;
        private System.Windows.Forms.ToolStripMenuItem File_LoadAll;
        private System.Windows.Forms.ToolStripMenuItem File_Help;
        private System.Windows.Forms.ToolStripMenuItem File_About;
        private System.Windows.Forms.ToolStripMenuItem File_Exit;
        private System.Windows.Forms.ToolStripDropDownButton Project_DropDown;
        private System.Windows.Forms.ToolStripDropDownButton Data_DropDown;
        private System.Windows.Forms.ToolStripDropDownButton SCADA_DropDown;
        private System.Windows.Forms.ToolStripMenuItem Project_GetDataFromDesign;
        private System.Windows.Forms.ToolStripMenuItem Project_CompareDesign;
        private System.Windows.Forms.ToolStripMenuItem Data_GetDataFromProject;
        private System.Windows.Forms.ToolStripMenuItem DataFindFunctionMenuItem;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage DesignTab;
        private System.Windows.Forms.TabPage DataTab;
        private System.Windows.Forms.TabPage ObjectTab;
        private System.Windows.Forms.TextBox FindTextBox;
        private System.Windows.Forms.Button FindButton;
        private System.Windows.Forms.DataGridView DesignGridView;
        private DataGridView DataGridView;
        private DataGridView ObjectsGridView;
        private ToolStripMenuItem Data_KKSCombine;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem Project_CPU;
        private ToolStripMenuItem Project_SCADA;
        private ToolStripMenuItem Project_Language;
        private ToolStripMenuItem File_Language;
        private ToolStripMenuItem File_Language_EN;
        private ToolStripMenuItem File_Language_LT;
        private ToolStripMenuItem Project_CPU_Add;
        private ToolStripMenuItem Project_SCADA_Add;
        private ToolStripMenuItem Project_Language_Add;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem SCADA_EditModules;
        private ToolStripDropDownButton IO_DropDown;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripMenuItem IO_Edit;
        private TabPage ModuleTab;
        private DataGridView ModulesGridView;
        private ToolStripMenuItem IO_FindModules;
        private ToolStripMenuItem IO_Generate;
        private ToolStripMenuItem SCADA_GenerateModules;
        private TabPage AddressTab;
        private DataGridView AddressesGridView;
        private ToolStripMenuItem SCADA_GenerateObjects;
        private ToolStripMenuItem SCADA_EditObjects;
        private ToolStripSeparator toolStripSeparator9;
        private ToolStripMenuItem Data_EditFunctions;
        private ToolStripMenuItem File_DebugLevel;
        private ToolStripMenuItem File_DebugLevel_None;
        private ToolStripMenuItem File_DebugLevel_Minimum;
        private ToolStripMenuItem File_DebugLevel_High;
        private ToolStripMenuItem File_DebugLevel_Development;
        private ToolStripMenuItem IO_ClearAddresses;
        private ToolStripDropDownButton Objects_DropDown;
        private ToolStripMenuItem Objects_Find;
        private ToolStripMenuItem ObjectsFindTypeMenuItem;
        private ToolStripMenuItem Object_TransferToData;
        private ToolStripMenuItem Objects_DeclareGenerate;
        private ToolStripMenuItem Objects_InstancesGenerate;
        private ToolStripMenuItem Objects_ClearAddresses;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem Object_EditTypes;
        private ToolStripMenuItem Objects_DeclareEdit;
        private ToolStripMenuItem Objects_InstancesEdit;
    }
}

