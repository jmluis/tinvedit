using System;
using System.Threading;
using System.Windows.Forms;
using AutoUpdaterDotNET;
using TerrariaInvEdit.Terraria;
using TerrariaInvEdit.Tools;
using TerrariaInvEdit.UI.Controls;

namespace TerrariaInvEdit.UI.Forms
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnOpen = new System.Windows.Forms.ToolStripSplitButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnSaveAs = new System.Windows.Forms.ToolStripButton();
            this.btnReload = new System.Windows.Forms.ToolStripButton();
            this.btnCheckUpdates = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbout = new System.Windows.Forms.ToolStripButton();
            this.btnDonate = new System.Windows.Forms.ToolStripButton();
            this.btnChbShoot = new System.Windows.Forms.ToolStripButton();
            this.btnTwitter = new System.Windows.Forms.ToolStripButton();
            this.opnFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.savFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tpStats = new System.Windows.Forms.TabPage();
            this.pContainer = new System.Windows.Forms.Panel();
            this.gbGeneral = new System.Windows.Forms.GroupBox();
            this.numSkinVariant = new System.Windows.Forms.NumericUpDown();
            this.numTaxMoney = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.chkExtraAccessory = new System.Windows.Forms.CheckBox();
            this.numAQuests = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.chkHBLocked = new System.Windows.Forms.CheckBox();
            this.lblDif = new System.Windows.Forms.Label();
            this.comboDifficulty = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pbMana = new TerrariaInvEdit.UI.Controls.ColoredProgressBar();
            this.label6 = new System.Windows.Forms.Label();
            this.numHP = new System.Windows.Forms.NumericUpDown();
            this.numMaxMP = new System.Windows.Forms.NumericUpDown();
            this.pbHealth = new TerrariaInvEdit.UI.Controls.ColoredProgressBar();
            this.numMP = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numMaxHP = new System.Windows.Forms.NumericUpDown();
            this.txtTerrariaVersion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tpLooks = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.pbCharacter = new System.Windows.Forms.PictureBox();
            this.cbEquips = new System.Windows.Forms.CheckBox();
            this.btnHair = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnSkin = new System.Windows.Forms.PictureBox();
            this.btnUShirt = new System.Windows.Forms.PictureBox();
            this.btnPants = new System.Windows.Forms.PictureBox();
            this.btnShirt = new System.Windows.Forms.PictureBox();
            this.btnShoes = new System.Windows.Forms.PictureBox();
            this.btnEye = new System.Windows.Forms.PictureBox();
            this.tpInv = new System.Windows.Forms.TabPage();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.treeInv = new TerrariaInvEdit.UI.Controls.TreeViewMS();
            this.cmsInventory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnMaxAll = new System.Windows.Forms.ToolStripMenuItem();
            this.chkFavoritedItem = new System.Windows.Forms.CheckBox();
            this.btnSaveItem = new System.Windows.Forms.Button();
            this.pbItem = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lbItems = new System.Windows.Forms.ListBox();
            this.comboPrefix = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnMaxStack = new System.Windows.Forms.Button();
            this.numStackSize = new System.Windows.Forms.NumericUpDown();
            this.lbStackSize = new System.Windows.Forms.Label();
            this.tbBuffs = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeBuff = new TerrariaInvEdit.UI.Controls.TreeViewMS();
            this.BuffList = new System.Windows.Forms.ImageList(this.components);
            this.pbSelectedBuff = new System.Windows.Forms.PictureBox();
            this.rtbDesc = new System.Windows.Forms.RichTextBox();
            this.btnMaxTime = new System.Windows.Forms.Button();
            this.btnSaveBuff = new System.Windows.Forms.Button();
            this.numTime = new System.Windows.Forms.NumericUpDown();
            this.comboBuff = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.genderImageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tpStats.SuspendLayout();
            this.pContainer.SuspendLayout();
            this.gbGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSkinVariant)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTaxMoney)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAQuests)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxMP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxHP)).BeginInit();
            this.tpLooks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCharacter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHair)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSkin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUShirt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPants)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnShirt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnShoes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEye)).BeginInit();
            this.tpInv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.cmsInventory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStackSize)).BeginInit();
            this.tbBuffs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSelectedBuff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTime)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpen,
            this.btnSave,
            this.btnSaveAs,
            this.btnReload,
            this.btnCheckUpdates,
            this.toolStripSeparator1,
            this.btnAbout,
            this.btnDonate,
            this.btnChbShoot,
            this.btnTwitter});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(519, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip";
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(32, 22);
            this.btnOpen.Text = "Open Player";
            this.btnOpen.ButtonClick += new System.EventHandler(this.btnOpen_Click);
            this.btnOpen.DropDownOpening += new System.EventHandler(this.btnOpen_DropDownOpening);
            this.btnOpen.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.btnOpen_DropDownItemClicked);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Text = "Save player";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveAs.Image")));
            this.btnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(23, 22);
            this.btnSaveAs.Text = "Save As";
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // btnReload
            // 
            this.btnReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnReload.Image = global::TerrariaInvEdit.Properties.Resources.arrow_refresh;
            this.btnReload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(23, 22);
            this.btnReload.Text = "Reload";
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnCheckUpdates
            // 
            this.btnCheckUpdates.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCheckUpdates.Image = ((System.Drawing.Image)(resources.GetObject("btnCheckUpdates.Image")));
            this.btnCheckUpdates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCheckUpdates.Name = "btnCheckUpdates";
            this.btnCheckUpdates.Size = new System.Drawing.Size(23, 22);
            this.btnCheckUpdates.Text = "Check for updates";
            this.btnCheckUpdates.Click += new System.EventHandler(this.btnCheckUpdates_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAbout
            // 
            this.btnAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAbout.Image = global::TerrariaInvEdit.Properties.Resources.information;
            this.btnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(23, 22);
            this.btnAbout.Text = "About...";
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnDonate
            // 
            this.btnDonate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDonate.Image = global::TerrariaInvEdit.Properties.Resources.money_arrow;
            this.btnDonate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDonate.Name = "btnDonate";
            this.btnDonate.Size = new System.Drawing.Size(23, 22);
            this.btnDonate.Text = "Donate and support!";
            this.btnDonate.Click += new System.EventHandler(this.btnDonate_Click);
            // 
            // btnChbShoot
            // 
            this.btnChbShoot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnChbShoot.Image = global::TerrariaInvEdit.Properties.Resources.shootavy;
            this.btnChbShoot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnChbShoot.Name = "btnChbShoot";
            this.btnChbShoot.Size = new System.Drawing.Size(23, 22);
            this.btnChbShoot.Text = "ChbShoot.ME";
            this.btnChbShoot.Click += new System.EventHandler(this.btnChbShoot_Click);
            // 
            // btnTwitter
            // 
            this.btnTwitter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTwitter.Image = global::TerrariaInvEdit.Properties.Resources.balloon_twitter;
            this.btnTwitter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTwitter.Name = "btnTwitter";
            this.btnTwitter.Size = new System.Drawing.Size(23, 22);
            this.btnTwitter.Text = "Follow us on Twitter";
            this.btnTwitter.Click += new System.EventHandler(this.btnTwitter_Click);
            // 
            // opnFileDialog
            // 
            this.opnFileDialog.DefaultExt = "plr";
            this.opnFileDialog.Filter = "Plr files|*.plr|All Files|*.*";
            this.opnFileDialog.Title = "Open Terraria Player file";
            // 
            // savFileDialog
            // 
            this.savFileDialog.DefaultExt = "plr";
            this.savFileDialog.Filter = "Plr files|*.plr|All Files|*.*";
            this.savFileDialog.Title = "Save player file";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tpStats);
            this.tabControl.Controls.Add(this.tpLooks);
            this.tabControl.Controls.Add(this.tpInv);
            this.tabControl.Controls.Add(this.tbBuffs);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 25);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(519, 318);
            this.tabControl.TabIndex = 1;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            this.tabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl_Selecting);
            // 
            // tpStats
            // 
            this.tpStats.BackColor = System.Drawing.SystemColors.Control;
            this.tpStats.Controls.Add(this.pContainer);
            this.tpStats.Location = new System.Drawing.Point(4, 22);
            this.tpStats.Name = "tpStats";
            this.tpStats.Padding = new System.Windows.Forms.Padding(3);
            this.tpStats.Size = new System.Drawing.Size(511, 292);
            this.tpStats.TabIndex = 0;
            this.tpStats.Text = "Stats";
            // 
            // pContainer
            // 
            this.pContainer.BackColor = System.Drawing.Color.White;
            this.pContainer.Controls.Add(this.gbGeneral);
            this.pContainer.Controls.Add(this.groupBox1);
            this.pContainer.Controls.Add(this.txtTerrariaVersion);
            this.pContainer.Controls.Add(this.label1);
            this.pContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pContainer.Enabled = false;
            this.pContainer.Location = new System.Drawing.Point(3, 3);
            this.pContainer.Name = "pContainer";
            this.pContainer.Size = new System.Drawing.Size(505, 286);
            this.pContainer.TabIndex = 12;
            // 
            // gbGeneral
            // 
            this.gbGeneral.Controls.Add(this.numSkinVariant);
            this.gbGeneral.Controls.Add(this.numTaxMoney);
            this.gbGeneral.Controls.Add(this.label14);
            this.gbGeneral.Controls.Add(this.chkExtraAccessory);
            this.gbGeneral.Controls.Add(this.numAQuests);
            this.gbGeneral.Controls.Add(this.label13);
            this.gbGeneral.Controls.Add(this.label2);
            this.gbGeneral.Controls.Add(this.txtName);
            this.gbGeneral.Controls.Add(this.chkHBLocked);
            this.gbGeneral.Controls.Add(this.lblDif);
            this.gbGeneral.Controls.Add(this.comboDifficulty);
            this.gbGeneral.Controls.Add(this.label9);
            this.gbGeneral.Location = new System.Drawing.Point(3, 3);
            this.gbGeneral.Name = "gbGeneral";
            this.gbGeneral.Size = new System.Drawing.Size(244, 163);
            this.gbGeneral.TabIndex = 35;
            this.gbGeneral.TabStop = false;
            this.gbGeneral.Text = "Player Settings";
            // 
            // numSkinVariant
            // 
            this.numSkinVariant.Location = new System.Drawing.Point(83, 58);
            this.numSkinVariant.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numSkinVariant.Name = "numSkinVariant";
            this.numSkinVariant.Size = new System.Drawing.Size(118, 20);
            this.numSkinVariant.TabIndex = 39;
            this.numSkinVariant.ValueChanged += new System.EventHandler(this.numSkinVariant_ValueChanged);
            // 
            // numTaxMoney
            // 
            this.numTaxMoney.Location = new System.Drawing.Point(83, 106);
            this.numTaxMoney.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numTaxMoney.Name = "numTaxMoney";
            this.numTaxMoney.Size = new System.Drawing.Size(118, 20);
            this.numTaxMoney.TabIndex = 38;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 110);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 13);
            this.label14.TabIndex = 37;
            this.label14.Text = "Tax Money:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // chkExtraAccessory
            // 
            this.chkExtraAccessory.AutoSize = true;
            this.chkExtraAccessory.Location = new System.Drawing.Point(130, 134);
            this.chkExtraAccessory.Name = "chkExtraAccessory";
            this.chkExtraAccessory.Size = new System.Drawing.Size(108, 17);
            this.chkExtraAccessory.TabIndex = 36;
            this.chkExtraAccessory.Text = "Extra Accessory?";
            this.chkExtraAccessory.UseVisualStyleBackColor = true;
            // 
            // numAQuests
            // 
            this.numAQuests.Location = new System.Drawing.Point(83, 82);
            this.numAQuests.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numAQuests.Name = "numAQuests";
            this.numAQuests.Size = new System.Drawing.Size(118, 20);
            this.numAQuests.TabIndex = 35;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 87);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(79, 13);
            this.label13.TabIndex = 34;
            this.label13.Text = "Angler Quests: ";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(83, 12);
            this.txtName.MaxLength = 20;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(118, 20);
            this.txtName.TabIndex = 8;
            // 
            // chkHBLocked
            // 
            this.chkHBLocked.AutoSize = true;
            this.chkHBLocked.Location = new System.Drawing.Point(10, 134);
            this.chkHBLocked.Name = "chkHBLocked";
            this.chkHBLocked.Size = new System.Drawing.Size(99, 17);
            this.chkHBLocked.TabIndex = 33;
            this.chkHBLocked.Text = "Hotbar locked?";
            this.chkHBLocked.UseVisualStyleBackColor = true;
            // 
            // lblDif
            // 
            this.lblDif.AutoSize = true;
            this.lblDif.Location = new System.Drawing.Point(7, 41);
            this.lblDif.Name = "lblDif";
            this.lblDif.Size = new System.Drawing.Size(50, 13);
            this.lblDif.TabIndex = 29;
            this.lblDif.Text = "Difficulty:";
            // 
            // comboDifficulty
            // 
            this.comboDifficulty.FormattingEnabled = true;
            this.comboDifficulty.Items.AddRange(new object[] {
            "Normal",
            "Mediumcore",
            "Hardcore"});
            this.comboDifficulty.Location = new System.Drawing.Point(83, 33);
            this.comboDifficulty.Name = "comboDifficulty";
            this.comboDifficulty.Size = new System.Drawing.Size(118, 21);
            this.comboDifficulty.TabIndex = 27;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 63);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "Skin Variant:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pbMana);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.numHP);
            this.groupBox1.Controls.Add(this.numMaxMP);
            this.groupBox1.Controls.Add(this.pbHealth);
            this.groupBox1.Controls.Add(this.numMP);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numMaxHP);
            this.groupBox1.Location = new System.Drawing.Point(253, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(247, 137);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Health/Mana";
            // 
            // pbMana
            // 
            this.pbMana.BackColor = System.Drawing.SystemColors.Highlight;
            this.pbMana.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.pbMana.Location = new System.Drawing.Point(8, 108);
            this.pbMana.Name = "pbMana";
            this.pbMana.Size = new System.Drawing.Size(231, 23);
            this.pbMana.TabIndex = 24;
            this.pbMana.Click += new System.EventHandler(this.btnRegen_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(111, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "MaxMP:";
            // 
            // numHP
            // 
            this.numHP.Location = new System.Drawing.Point(31, 18);
            this.numHP.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numHP.Name = "numHP";
            this.numHP.Size = new System.Drawing.Size(78, 20);
            this.numHP.TabIndex = 19;
            this.numHP.ValueChanged += new System.EventHandler(this.numHP_ValueChanged);
            // 
            // numMaxMP
            // 
            this.numMaxMP.Location = new System.Drawing.Point(163, 82);
            this.numMaxMP.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numMaxMP.Name = "numMaxMP";
            this.numMaxMP.Size = new System.Drawing.Size(78, 20);
            this.numMaxMP.TabIndex = 22;
            this.numMaxMP.ValueChanged += new System.EventHandler(this.numMaxMP_ValueChanged);
            // 
            // pbHealth
            // 
            this.pbHealth.BackColor = System.Drawing.Color.Red;
            this.pbHealth.ForeColor = System.Drawing.Color.DarkRed;
            this.pbHealth.Location = new System.Drawing.Point(8, 42);
            this.pbHealth.Name = "pbHealth";
            this.pbHealth.Size = new System.Drawing.Size(231, 23);
            this.pbHealth.TabIndex = 23;
            this.pbHealth.Click += new System.EventHandler(this.btnHeal_Click);
            // 
            // numMP
            // 
            this.numMP.Location = new System.Drawing.Point(31, 82);
            this.numMP.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numMP.Name = "numMP";
            this.numMP.Size = new System.Drawing.Size(78, 20);
            this.numMP.TabIndex = 21;
            this.numMP.ValueChanged += new System.EventHandler(this.numMP_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "MP:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "HP:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(111, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "MaxHP:";
            // 
            // numMaxHP
            // 
            this.numMaxHP.Location = new System.Drawing.Point(163, 18);
            this.numMaxHP.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numMaxHP.Name = "numMaxHP";
            this.numMaxHP.Size = new System.Drawing.Size(78, 20);
            this.numMaxHP.TabIndex = 20;
            this.numMaxHP.ValueChanged += new System.EventHandler(this.numMaxHP_ValueChanged);
            // 
            // txtTerrariaVersion
            // 
            this.txtTerrariaVersion.Location = new System.Drawing.Point(400, 3);
            this.txtTerrariaVersion.Name = "txtTerrariaVersion";
            this.txtTerrariaVersion.ReadOnly = true;
            this.txtTerrariaVersion.Size = new System.Drawing.Size(100, 20);
            this.txtTerrariaVersion.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(310, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Terraria Version:";
            // 
            // tpLooks
            // 
            this.tpLooks.Controls.Add(this.splitContainer2);
            this.tpLooks.Location = new System.Drawing.Point(4, 22);
            this.tpLooks.Name = "tpLooks";
            this.tpLooks.Size = new System.Drawing.Size(511, 292);
            this.tpLooks.TabIndex = 3;
            this.tpLooks.Text = "Looks";
            this.tpLooks.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BackColor = System.Drawing.Color.White;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btnRight);
            this.splitContainer2.Panel1.Controls.Add(this.btnLeft);
            this.splitContainer2.Panel1.Controls.Add(this.pbCharacter);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.cbEquips);
            this.splitContainer2.Panel2.Controls.Add(this.btnHair);
            this.splitContainer2.Panel2.Controls.Add(this.label11);
            this.splitContainer2.Panel2.Controls.Add(this.label12);
            this.splitContainer2.Panel2.Controls.Add(this.btnSkin);
            this.splitContainer2.Panel2.Controls.Add(this.btnUShirt);
            this.splitContainer2.Panel2.Controls.Add(this.btnPants);
            this.splitContainer2.Panel2.Controls.Add(this.btnShirt);
            this.splitContainer2.Panel2.Controls.Add(this.btnShoes);
            this.splitContainer2.Panel2.Controls.Add(this.btnEye);
            this.splitContainer2.Size = new System.Drawing.Size(511, 292);
            this.splitContainer2.SplitterDistance = 170;
            this.splitContainer2.TabIndex = 49;
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(142, 0);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(25, 87);
            this.btnRight.TabIndex = 49;
            this.btnRight.Text = "->";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(0, 0);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(25, 87);
            this.btnLeft.TabIndex = 50;
            this.btnLeft.Text = "<-";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // pbCharacter
            // 
            this.pbCharacter.BackColor = System.Drawing.Color.Black;
            this.pbCharacter.Location = new System.Drawing.Point(24, 0);
            this.pbCharacter.Name = "pbCharacter";
            this.pbCharacter.Size = new System.Drawing.Size(119, 87);
            this.pbCharacter.TabIndex = 0;
            this.pbCharacter.TabStop = false;
            // 
            // cbEquips
            // 
            this.cbEquips.AutoSize = true;
            this.cbEquips.Location = new System.Drawing.Point(246, 16);
            this.cbEquips.Name = "cbEquips";
            this.cbEquips.Size = new System.Drawing.Size(88, 17);
            this.cbEquips.TabIndex = 50;
            this.cbEquips.Text = "Show Equips";
            this.cbEquips.ThreeState = true;
            this.cbEquips.UseVisualStyleBackColor = true;
            this.cbEquips.CheckStateChanged += new System.EventHandler(this.cbEquips_CheckStateChanged);
            // 
            // btnHair
            // 
            this.btnHair.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnHair.Location = new System.Drawing.Point(3, 16);
            this.btnHair.Name = "btnHair";
            this.btnHair.Size = new System.Drawing.Size(75, 23);
            this.btnHair.TabIndex = 47;
            this.btnHair.TabStop = false;
            this.btnHair.Text = "Hair Color";
            this.btnHair.Click += new System.EventHandler(this.btnHair_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(218, 13);
            this.label11.TabIndex = 48;
            this.label11.Text = "Hair Color:         Eye Color:            Skin Color:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 42);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(309, 13);
            this.label12.TabIndex = 49;
            this.label12.Text = "Shirt Color:         UnderShirt Color: Pants Color:        Shoes Color:";
            // 
            // btnSkin
            // 
            this.btnSkin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnSkin.Location = new System.Drawing.Point(165, 16);
            this.btnSkin.Name = "btnSkin";
            this.btnSkin.Size = new System.Drawing.Size(75, 23);
            this.btnSkin.TabIndex = 46;
            this.btnSkin.TabStop = false;
            this.btnSkin.Text = "Skin Color";
            this.btnSkin.Click += new System.EventHandler(this.btnSkin_Click);
            // 
            // btnUShirt
            // 
            this.btnUShirt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnUShirt.Location = new System.Drawing.Point(84, 56);
            this.btnUShirt.Name = "btnUShirt";
            this.btnUShirt.Size = new System.Drawing.Size(75, 23);
            this.btnUShirt.TabIndex = 43;
            this.btnUShirt.TabStop = false;
            this.btnUShirt.Text = "Undershirt Color";
            this.btnUShirt.Click += new System.EventHandler(this.btnUShirt_Click);
            // 
            // btnPants
            // 
            this.btnPants.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnPants.Location = new System.Drawing.Point(165, 56);
            this.btnPants.Name = "btnPants";
            this.btnPants.Size = new System.Drawing.Size(75, 23);
            this.btnPants.TabIndex = 42;
            this.btnPants.TabStop = false;
            this.btnPants.Text = "Pants Color";
            this.btnPants.Click += new System.EventHandler(this.btnPants_Click);
            // 
            // btnShirt
            // 
            this.btnShirt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnShirt.Location = new System.Drawing.Point(3, 56);
            this.btnShirt.Name = "btnShirt";
            this.btnShirt.Size = new System.Drawing.Size(75, 23);
            this.btnShirt.TabIndex = 44;
            this.btnShirt.TabStop = false;
            this.btnShirt.Text = "Shirt Color";
            this.btnShirt.Click += new System.EventHandler(this.btnShirt_Click);
            // 
            // btnShoes
            // 
            this.btnShoes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnShoes.Location = new System.Drawing.Point(246, 56);
            this.btnShoes.Name = "btnShoes";
            this.btnShoes.Size = new System.Drawing.Size(75, 23);
            this.btnShoes.TabIndex = 41;
            this.btnShoes.TabStop = false;
            this.btnShoes.Text = "Shoe Color";
            this.btnShoes.Click += new System.EventHandler(this.btnShoes_Click);
            // 
            // btnEye
            // 
            this.btnEye.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnEye.Location = new System.Drawing.Point(84, 16);
            this.btnEye.Name = "btnEye";
            this.btnEye.Size = new System.Drawing.Size(75, 23);
            this.btnEye.TabIndex = 45;
            this.btnEye.TabStop = false;
            this.btnEye.Text = "Eye Color";
            this.btnEye.Click += new System.EventHandler(this.btnEye_Click);
            // 
            // tpInv
            // 
            this.tpInv.BackColor = System.Drawing.SystemColors.Control;
            this.tpInv.Controls.Add(this.splitContainer);
            this.tpInv.Location = new System.Drawing.Point(4, 22);
            this.tpInv.Name = "tpInv";
            this.tpInv.Padding = new System.Windows.Forms.Padding(3);
            this.tpInv.Size = new System.Drawing.Size(511, 292);
            this.tpInv.TabIndex = 1;
            this.tpInv.Text = "Inventory";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(3, 3);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeInv);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.chkFavoritedItem);
            this.splitContainer.Panel2.Controls.Add(this.btnSaveItem);
            this.splitContainer.Panel2.Controls.Add(this.pbItem);
            this.splitContainer.Panel2.Controls.Add(this.lblName);
            this.splitContainer.Panel2.Controls.Add(this.txtSearch);
            this.splitContainer.Panel2.Controls.Add(this.lbItems);
            this.splitContainer.Panel2.Controls.Add(this.comboPrefix);
            this.splitContainer.Panel2.Controls.Add(this.label10);
            this.splitContainer.Panel2.Controls.Add(this.btnMaxStack);
            this.splitContainer.Panel2.Controls.Add(this.numStackSize);
            this.splitContainer.Panel2.Controls.Add(this.lbStackSize);
            this.splitContainer.Size = new System.Drawing.Size(505, 286);
            this.splitContainer.SplitterDistance = 260;
            this.splitContainer.TabIndex = 1;
            // 
            // treeInv
            // 
            this.treeInv.BackColor = System.Drawing.Color.White;
            this.treeInv.ContextMenuStrip = this.cmsInventory;
            this.treeInv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeInv.HideSelection = false;
            this.treeInv.Location = new System.Drawing.Point(0, 0);
            this.treeInv.Name = "treeInv";
            this.treeInv.SelectedNodes = ((System.Collections.Generic.List<System.Windows.Forms.TreeNode>)(resources.GetObject("treeInv.SelectedNodes")));
            this.treeInv.Size = new System.Drawing.Size(260, 286);
            this.treeInv.TabIndex = 0;
            this.treeInv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeInv_AfterSelect);
            // 
            // cmsInventory
            // 
            this.cmsInventory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMaxAll});
            this.cmsInventory.Name = "cmsInventory";
            this.cmsInventory.Size = new System.Drawing.Size(150, 26);
            // 
            // btnMaxAll
            // 
            this.btnMaxAll.Name = "btnMaxAll";
            this.btnMaxAll.Size = new System.Drawing.Size(149, 22);
            this.btnMaxAll.Text = "Max All Stacks";
            this.btnMaxAll.Click += new System.EventHandler(this.btnMaxAll_Click_1);
            // 
            // chkFavoritedItem
            // 
            this.chkFavoritedItem.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkFavoritedItem.AutoSize = true;
            this.chkFavoritedItem.Location = new System.Drawing.Point(135, 14);
            this.chkFavoritedItem.Name = "chkFavoritedItem";
            this.chkFavoritedItem.Size = new System.Drawing.Size(61, 23);
            this.chkFavoritedItem.TabIndex = 15;
            this.chkFavoritedItem.Text = "Favorite?";
            this.chkFavoritedItem.UseVisualStyleBackColor = true;
            // 
            // btnSaveItem
            // 
            this.btnSaveItem.Location = new System.Drawing.Point(3, 237);
            this.btnSaveItem.Name = "btnSaveItem";
            this.btnSaveItem.Size = new System.Drawing.Size(229, 31);
            this.btnSaveItem.TabIndex = 14;
            this.btnSaveItem.Text = "Save item";
            this.btnSaveItem.UseVisualStyleBackColor = true;
            this.btnSaveItem.Click += new System.EventHandler(this.btnSaveItem_Click);
            // 
            // pbItem
            // 
            this.pbItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbItem.Location = new System.Drawing.Point(202, 21);
            this.pbItem.Name = "pbItem";
            this.pbItem.Size = new System.Drawing.Size(32, 32);
            this.pbItem.TabIndex = 13;
            this.pbItem.TabStop = false;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(2, 40);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(30, 13);
            this.lblName.TabIndex = 12;
            this.lblName.Text = "Item:";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(11, 56);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(223, 20);
            this.txtSearch.TabIndex = 11;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lbItems
            // 
            this.lbItems.FormattingEnabled = true;
            this.lbItems.Location = new System.Drawing.Point(11, 81);
            this.lbItems.Name = "lbItems";
            this.lbItems.Size = new System.Drawing.Size(223, 95);
            this.lbItems.Sorted = true;
            this.lbItems.TabIndex = 10;
            this.lbItems.SelectedValueChanged += new System.EventHandler(this.lbItems_SelectedValueChanged);
            // 
            // comboPrefix
            // 
            this.comboPrefix.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPrefix.FormattingEnabled = true;
            this.comboPrefix.Location = new System.Drawing.Point(11, 16);
            this.comboPrefix.Name = "comboPrefix";
            this.comboPrefix.Size = new System.Drawing.Size(112, 21);
            this.comboPrefix.TabIndex = 9;
            this.comboPrefix.SelectedIndexChanged += new System.EventHandler(this.comboPrefix_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "Prefix:";
            // 
            // btnMaxStack
            // 
            this.btnMaxStack.Location = new System.Drawing.Point(5, 208);
            this.btnMaxStack.Name = "btnMaxStack";
            this.btnMaxStack.Size = new System.Drawing.Size(229, 23);
            this.btnMaxStack.TabIndex = 6;
            this.btnMaxStack.Text = "Max Stack";
            this.btnMaxStack.UseVisualStyleBackColor = true;
            this.btnMaxStack.Click += new System.EventHandler(this.BtnMaxStackClick);
            // 
            // numStackSize
            // 
            this.numStackSize.Location = new System.Drawing.Point(69, 182);
            this.numStackSize.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numStackSize.Name = "numStackSize";
            this.numStackSize.Size = new System.Drawing.Size(165, 20);
            this.numStackSize.TabIndex = 4;
            this.numStackSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lbStackSize
            // 
            this.lbStackSize.AutoSize = true;
            this.lbStackSize.Location = new System.Drawing.Point(2, 184);
            this.lbStackSize.Name = "lbStackSize";
            this.lbStackSize.Size = new System.Drawing.Size(61, 13);
            this.lbStackSize.TabIndex = 3;
            this.lbStackSize.Text = "Stack Size:";
            // 
            // tbBuffs
            // 
            this.tbBuffs.BackColor = System.Drawing.SystemColors.Control;
            this.tbBuffs.Controls.Add(this.splitContainer1);
            this.tbBuffs.Location = new System.Drawing.Point(4, 22);
            this.tbBuffs.Name = "tbBuffs";
            this.tbBuffs.Padding = new System.Windows.Forms.Padding(3);
            this.tbBuffs.Size = new System.Drawing.Size(511, 292);
            this.tbBuffs.TabIndex = 2;
            this.tbBuffs.Text = "Buffs";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeBuff);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pbSelectedBuff);
            this.splitContainer1.Panel2.Controls.Add(this.rtbDesc);
            this.splitContainer1.Panel2.Controls.Add(this.btnMaxTime);
            this.splitContainer1.Panel2.Controls.Add(this.btnSaveBuff);
            this.splitContainer1.Panel2.Controls.Add(this.numTime);
            this.splitContainer1.Panel2.Controls.Add(this.comboBuff);
            this.splitContainer1.Panel2.Controls.Add(this.label7);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Size = new System.Drawing.Size(505, 286);
            this.splitContainer1.SplitterDistance = 331;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeBuff
            // 
            this.treeBuff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeBuff.ImageIndex = 0;
            this.treeBuff.ImageList = this.BuffList;
            this.treeBuff.Location = new System.Drawing.Point(0, 0);
            this.treeBuff.Name = "treeBuff";
            this.treeBuff.SelectedImageIndex = 0;
            this.treeBuff.SelectedNodes = ((System.Collections.Generic.List<System.Windows.Forms.TreeNode>)(resources.GetObject("treeBuff.SelectedNodes")));
            this.treeBuff.Size = new System.Drawing.Size(331, 286);
            this.treeBuff.TabIndex = 0;
            this.treeBuff.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeBuff_AfterSelect);
            // 
            // BuffList
            // 
            this.BuffList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.BuffList.ImageSize = new System.Drawing.Size(32, 32);
            this.BuffList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pbSelectedBuff
            // 
            this.pbSelectedBuff.Location = new System.Drawing.Point(10, 108);
            this.pbSelectedBuff.Name = "pbSelectedBuff";
            this.pbSelectedBuff.Size = new System.Drawing.Size(32, 32);
            this.pbSelectedBuff.TabIndex = 15;
            this.pbSelectedBuff.TabStop = false;
            // 
            // rtbDesc
            // 
            this.rtbDesc.Location = new System.Drawing.Point(10, 146);
            this.rtbDesc.Name = "rtbDesc";
            this.rtbDesc.ReadOnly = true;
            this.rtbDesc.Size = new System.Drawing.Size(130, 96);
            this.rtbDesc.TabIndex = 14;
            this.rtbDesc.Text = "";
            // 
            // btnMaxTime
            // 
            this.btnMaxTime.Location = new System.Drawing.Point(48, 108);
            this.btnMaxTime.Name = "btnMaxTime";
            this.btnMaxTime.Size = new System.Drawing.Size(75, 23);
            this.btnMaxTime.TabIndex = 13;
            this.btnMaxTime.Text = "Max Time";
            this.btnMaxTime.UseVisualStyleBackColor = true;
            this.btnMaxTime.Click += new System.EventHandler(this.btnMaxTime_Click);
            // 
            // btnSaveBuff
            // 
            this.btnSaveBuff.Location = new System.Drawing.Point(10, 248);
            this.btnSaveBuff.Name = "btnSaveBuff";
            this.btnSaveBuff.Size = new System.Drawing.Size(130, 23);
            this.btnSaveBuff.TabIndex = 12;
            this.btnSaveBuff.Text = "Save";
            this.btnSaveBuff.UseVisualStyleBackColor = true;
            this.btnSaveBuff.Click += new System.EventHandler(this.btnSaveBuff_Click);
            // 
            // numTime
            // 
            this.numTime.Location = new System.Drawing.Point(10, 82);
            this.numTime.Maximum = new decimal(new int[] {
            35791394,
            0,
            0,
            0});
            this.numTime.Name = "numTime";
            this.numTime.Size = new System.Drawing.Size(130, 20);
            this.numTime.TabIndex = 11;
            this.numTime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // comboBuff
            // 
            this.comboBuff.FormattingEnabled = true;
            this.comboBuff.Location = new System.Drawing.Point(10, 32);
            this.comboBuff.Name = "comboBuff";
            this.comboBuff.Size = new System.Drawing.Size(130, 21);
            this.comboBuff.TabIndex = 8;
            this.comboBuff.SelectedIndexChanged += new System.EventHandler(this.comboBuff_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Buff time (seconds):";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(58, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Buff:";
            // 
            // genderImageList
            // 
            this.genderImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("genderImageList.ImageStream")));
            this.genderImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.genderImageList.Images.SetKeyName(0, "female.png");
            this.genderImageList.Images.SetKeyName(1, "male2.png");
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 343);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Terraria Inventory Edit";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tpStats.ResumeLayout(false);
            this.pContainer.ResumeLayout(false);
            this.pContainer.PerformLayout();
            this.gbGeneral.ResumeLayout(false);
            this.gbGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSkinVariant)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTaxMoney)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAQuests)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxMP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxHP)).EndInit();
            this.tpLooks.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCharacter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHair)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSkin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUShirt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPants)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnShirt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnShoes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEye)).EndInit();
            this.tpInv.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.cmsInventory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStackSize)).EndInit();
            this.tbBuffs.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSelectedBuff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region Windows Form Designer event code
        private void MainForm_Shown(object sender, EventArgs e)
        {
            opnFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\My Games\Terraria\Players";
            savFileDialog.InitialDirectory = opnFileDialog.InitialDirectory;

            AutoUpdater.Start("https://u.chbshoot.me/chk/0");
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }

        private void btnCheckUpdates_Click(object sender, EventArgs e)
        {
            AutoUpdater.Start("https://u.chbshoot.me/chk/0");
        }

        private void btnDonate_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=MLBDGNJV6MWSS&lc=US&item_name=Shoot%27s%20Terraria%20Fund&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donateCC_LG%2egif%3aNonHosted");
        }

        private void btnTwitter_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://twitter.com/#!/chbshoot");
        }

        private void btnChbShoot_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://chbshoot.me/");
        }

        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (PPlayer != null)
            {
                if (PPlayer.TerrariaVersion < 11)
                {
                    e.Cancel = true;
                    MessageBox.Show("Sorry! This version of Terraria doesn't support buffs. How did you even get here?", "Oops!");
                    return;
                }
                if (e.TabPage == tpInv)
                {
                    txtSearch.Text = string.Empty;
                    treeInv.SelectedNode = null;
                    if (lbItems.Items.Count > 0)
                    {
                        lbItems.SelectedIndex = 0;
                        comboPrefix.SelectedIndex = 0;
                    }
                    else
                    {
                        Extensions.AddListBoxItems(lbItems);
                        Extensions.AddComboBoxPrefixes(comboPrefix);
                    }
                }
                if (e.TabPage == tbBuffs)
                    Extensions.AddComboBoxBuffs(comboBuff);
            }
            else if (PPlayer == null)
            {
                e.Cancel = true;
            }
        }

        private void comboPrefix_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboPrefix.SelectedIndex == 0xFF)
                comboPrefix.SelectedIndex = 0;
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case 0: this.Height = (int)(280 * (dpiY / 100)); this.Width = (int)(569 * (dpiX / 100)); break;
                case 1: this.Height = (int)(185 * (dpiY / 100)); this.Width = (int)(569 * (dpiX / 100)); break;
                default: this.Height = (int)(390 * (dpiY / 100)); this.Width = (int)(569 * (dpiX / 100)); break;

            }
        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.SaveFileDialog savFileDialog;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tpStats;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tpInv;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pContainer;
        private System.Windows.Forms.NumericUpDown numMaxMP;
        private System.Windows.Forms.NumericUpDown numMP;
        private System.Windows.Forms.NumericUpDown numHP;
        private System.Windows.Forms.NumericUpDown numMaxHP;
        private System.Windows.Forms.ToolStripButton btnReload;
        private System.Windows.Forms.ToolStripButton btnSaveAs;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.NumericUpDown numStackSize;
        private System.Windows.Forms.Label lbStackSize;
        private System.Windows.Forms.Button btnMaxStack;
        private System.Windows.Forms.ToolStripButton btnAbout;
        private System.Windows.Forms.ToolStripButton btnCheckUpdates;
        private System.Windows.Forms.ToolStripButton btnDonate;
        private System.Windows.Forms.TextBox txtTerrariaVersion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnTwitter;
        private System.Windows.Forms.ToolStripSplitButton btnOpen;
        private System.Windows.Forms.ToolStripButton btnChbShoot;
        private System.Windows.Forms.TabPage tbBuffs;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnMaxTime;
        private System.Windows.Forms.Button btnSaveBuff;
        private System.Windows.Forms.NumericUpDown numTime;
        public System.Windows.Forms.ComboBox comboBuff;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox rtbDesc;
        public System.Windows.Forms.OpenFileDialog opnFileDialog;
        private System.Windows.Forms.ComboBox comboDifficulty;
        private System.Windows.Forms.Label lblDif;
        private UI.Controls.TreeViewMS treeInv;
        private UI.Controls.TreeViewMS treeBuff;
        private System.Windows.Forms.ImageList genderImageList;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkHBLocked;
        private System.Windows.Forms.ComboBox comboPrefix;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ListBox lbItems;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.PictureBox pbItem;
        private System.Windows.Forms.ImageList BuffList;
        private System.Windows.Forms.PictureBox pbSelectedBuff;
        private System.Windows.Forms.GroupBox gbGeneral;
        private System.Windows.Forms.GroupBox groupBox1;
        private ColoredProgressBar pbMana;
        private ColoredProgressBar pbHealth;
        private System.Windows.Forms.PictureBox btnHair;
        private System.Windows.Forms.PictureBox btnSkin;
        private System.Windows.Forms.PictureBox btnEye;
        private System.Windows.Forms.PictureBox btnShirt;
        private System.Windows.Forms.PictureBox btnUShirt;
        private System.Windows.Forms.PictureBox btnPants;
        private System.Windows.Forms.PictureBox btnShoes;
        private System.Windows.Forms.TabPage tpLooks;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.PictureBox pbCharacter;
        private CheckBox cbEquips;
        private Button btnSaveItem;
        private ContextMenuStrip cmsInventory;
        private ToolStripMenuItem btnMaxAll;
        private Label label13;
        private NumericUpDown numAQuests;
        private CheckBox chkExtraAccessory;
        private NumericUpDown numTaxMoney;
        private Label label14;
        private CheckBox chkFavoritedItem;
        private NumericUpDown numSkinVariant;
    }
}

