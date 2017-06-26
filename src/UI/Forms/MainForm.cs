/*
       This file is part of Terraria Inventory Editor
                            Copyright © 2017 Jose Luis, Anthony Wolfe

    Terraria Inventory Editor is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Terraria Inventory Editor is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Terraria Inventory Editor.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Ionic.Zip;
using TerraLimb;
using TerrariaInvEdit.Properties;

namespace TerrariaInvEdit.UI.Forms
{
    public partial class MainForm : Form
    {
        public ZipFile Data;

        private readonly float dpiX;
        private readonly float dpiY;
        public string LastPath = "";
        public bool Loading;
        public Player PPlayer;
        public bool Stack;
        public Dictionary<string, Bitmap> TextureCache;

        #region ctor

        public MainForm()
        {
            TextureCache = new Dictionary<string, Bitmap>();
            InitializeComponent();
            var g = CreateGraphics();
            dpiX = g.DpiX;
            dpiY = g.DpiY;
            g.Dispose();
#if DEBUG
            Text += " - DEBUG VERSION: " + ProductVersion;
#else
            Text += " - v" + Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
#endif
            splitContainer.Panel2Collapsed = true;
            splitContainer1.Panel2Collapsed = true;
            Height = (int)(270 * (dpiY / 100)) + 10;
            pbItem.SizeMode = PictureBoxSizeMode.Zoom;
            Data = ZipFile.Read(new MemoryStream(Resources.Data));
            Constants.Initialize(GetJson("package.json"), GetJson("items.json"), GetJson("prefixes.json"),
                GetJson("buffs.json"));
        }

        #endregion

        public Bitmap GetBitmap(string Format, params object[] Args)
        {
            var Path = string.Format(Format, Args);
            Bitmap Result = null;
            if (!TextureCache.TryGetValue(Path, out Result))
            {
                var ZipData = Data[Path];
                if (ZipData != null)
                {
                    var Stream = ZipData.OpenReader();
                    var ImageData = new byte[Stream.Length];
                    Stream.Read(ImageData, 0, (int)Stream.Length);
                    TextureCache.Add(Path, Result = new Bitmap(new MemoryStream(ImageData)));
                    Stream.Dispose();
                }
            }
            if (Result == null)
            {
                Result = Resources.ResourceManager.GetObject(Path.Replace('/', '_').Replace(".png", "")) as Bitmap;
                if (Result == null)
                    throw new FileNotFoundException($"Unable to find Texture at Path \"{Path}\"");
            }
            return (Bitmap)Result.Clone();
        }

        public string GetJson(string format, params object[] args)
        {
            var path = string.Format(format, args);
            var data = Data[path];
            using (var s = new MemoryStream())
            {
                data.Extract(s);
                s.Position = 0;
                using (var sr = new StreamReader(s))
                {
                    var json = sr.ReadToEnd();
                    sr.Dispose();
                    s.Dispose();
                    return json;
                }
            }
        }

        private void btnSaveItem_Click(object sender, EventArgs e)
        {
            UpdateNodeItem();
        }

        private void DisposePlayer()
        {
            PPlayer = null;
            txtTerrariaVersion.Text = string.Empty;
            txtName.Text = string.Empty;
            numHP.Value = 0;
            numMaxHP.Value = 0;
            numMP.Value = 0;
            numMaxMP.Value = 0;
            comboDifficulty.SelectedIndex = -1;
            numSkinVariant.Value = 0;
            chkHBLocked.Checked = false;
            chkExtraAccessory.Checked = false;
            numAQuests.Value = 0;
            numTaxMoney.Value = 0;

            #region Colors

            btnHair.BackColor = Color.White;
            btnEye.BackColor = Color.White;
            btnSkin.BackColor = Color.White;

            btnShirt.BackColor = Color.White;
            btnUShirt.BackColor = Color.White;
            btnPants.BackColor = Color.White;
            btnShoes.BackColor = Color.White;

            #endregion

            pbHealth.Value = 0;
            pbMana.Value = 0;
            treeInv.Nodes.Clear();
            treeBuff.Nodes.Clear();
            pContainer.Enabled = false;
        }

        private void numSkinVariant_ValueChanged(object sender, EventArgs e)
        {
            if (PPlayer != null)
            {
                PPlayer.SkinVariant = (byte)numSkinVariant.Value;
                DrawCharacter();
            }
        }

        #region Player

        #region Open

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (opnFileDialog.ShowDialog() != DialogResult.OK)
                return;
            LastPath = opnFileDialog.FileName;
            savFileDialog.InitialDirectory = Path.GetFullPath(opnFileDialog.FileName);
            PPlayer = new Player(opnFileDialog.FileName);
            PPlayer.PlayerLoaded += PPlayer_PlayerLoaded;
            PPlayer.PlayerSaved += PPlayer_PlayerSaved;
            var code = PPlayer.LoadPlayer();
            if (code != Player.ErrorCode.Success)
                DisposePlayer();
        }

        public void LoadPlayerDropDown()
        {
            btnOpen.DropDownItems.Clear();
            if (Directory.Exists(opnFileDialog.InitialDirectory))
            {
                var players = Directory.GetFiles(opnFileDialog.InitialDirectory);
                var item = new ToolStripMenuItem();

                foreach (var file in players)
                {
                    var ext = Path.GetExtension(file);
                    item = new ToolStripMenuItem();
                    if (Path.GetExtension(file) == ".plr")
                    {
                        var temp = Player.getMiniPlayer(file);

                        if (temp != null)
                            if (string.IsNullOrEmpty(temp.Name))
                            {
                            }
                            else
                            {
                                item.Text = temp.Name;
                                item.Tag = temp;
                                if (temp.Difficulty > 0)
                                    item.ForeColor = Color.White;

                                if (temp.Difficulty == 2)
                                    item.BackColor = Color.MediumPurple;
                                else if (temp.Difficulty == 1)
                                    item.BackColor = Color.DodgerBlue;
                                item.Image = genderImageList.Images[temp.IsMale ? 1 : 0];
                                btnOpen.DropDownItems.Add(item);
                            }
                    }
                }
            }
        }

        private void btnOpen_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var plr = (Player)e.ClickedItem.Tag;
            LastPath = plr.FilePath;
            plr.PlayerLoaded += PPlayer_PlayerLoaded;
            plr.PlayerSaved += PPlayer_PlayerSaved;
            var code = plr.LoadPlayer();
            if (code != Player.ErrorCode.Success)
            {
                DisposePlayer();
                LastPath = string.Empty;
            }
        }

        private void btnOpen_DropDownOpening(object sender, EventArgs e)
        {
            LoadPlayerDropDown();
        }

        #endregion

        #region Load

        private void PPlayer_PlayerLoaded(object sender, EventArgs e)
        {
            Loading = true;
            var p = (Player)sender;
            PPlayer = p;
            txtTerrariaVersion.Text = PPlayer.TerrariaVersion.ToString();
            txtName.Text = PPlayer.Name;
            numHP.Value = PPlayer.HP < 0 ? 0 : PPlayer.HP;
            numMaxHP.Value = PPlayer.MaxHP < 0 ? 0 : PPlayer.MaxHP;
            numMP.Value = PPlayer.MP < 0 ? 0 : PPlayer.MP;
            numMaxMP.Value = PPlayer.MaxMP < 0 ? 0 : PPlayer.MaxMP;
            comboDifficulty.SelectedIndex = p.Difficulty;
            numSkinVariant.Value = p.SkinVariant;
            chkHBLocked.Checked = p.HotBarLocked;
            chkExtraAccessory.Checked = p.ExtraAccessory;
            numAQuests.Value = p.AnglerQuestsFinished;
            numTaxMoney.Value = p.TaxMoney;

            #region Colors

            btnHair.BackColor = p.HairColor;
            btnEye.BackColor = p.EyeColor;
            btnSkin.BackColor = p.SkinColor;

            btnShirt.BackColor = p.ShirtColor;
            btnUShirt.BackColor = p.UnderShirtColor;
            btnPants.BackColor = p.PantsColor;
            btnShoes.BackColor = p.ShoeColor;

            #endregion

            DoTreeView(true);
            pContainer.Enabled = true;
            Loading = false;
            DrawCharacter();
        }

        private void DoTreeView(bool clear)
        {
            if (PPlayer == null)
                return;

            if (clear)
            {
                treeInv.Nodes.Clear();
                treeBuff.Nodes.Clear();
            }
            splitContainer.Panel2Collapsed = true;

            AddArmor(PPlayer);
            AddDye(PPlayer);
            AddInventory(PPlayer);
            AddPurse(PPlayer);
            AddAmmo(PPlayer);
            AddMiscEquips(PPlayer);
            AddMiscDye(PPlayer);
            AddPiggyBank(PPlayer);
            AddPiggyBank2(PPlayer);
            AddPiggyBank3(PPlayer);
            AddBuffs(PPlayer);
        }

        #endregion

        #region Add

        private void AddMiscDye(Player player)
        {
            var tempNode = new TreeNode();
            var tempNode2 = new TreeNode();

            tempNode = new TreeNode("Equipment Dyes");
            tempNode.Tag = player.MiscDye;
            tempNode.Name = "dyeNode";
            treeInv.Nodes.Add(tempNode);

            foreach (var item in player.MiscDye)
            {
                string prefix;
                if (item.Prefix == 0)
                    prefix = "";
                else
                    prefix = "[" + Constants.Prefixes[item.Prefix] + "]";
                tempNode2 = new TreeNode(prefix + Constants.Items[item.ItemID].ItemName + " " + item.Nick);
                tempNode2.Tag = item;
                tempNode2.Name = "dye";
                tempNode.Nodes.Add(tempNode2);
            }
        }

        private void AddMiscEquips(Player player)
        {
            var tempNode = new TreeNode();
            var tempNode2 = new TreeNode();

            tempNode = new TreeNode("Equipment");
            tempNode.Tag = player.MiscEquip;
            tempNode.Name = "miscEquipNode";
            treeInv.Nodes.Add(tempNode);

            foreach (var item in player.MiscEquip)
            {
                string prefix;
                if (item.Prefix == 0)
                    prefix = "";
                else
                    prefix = "[" + Constants.Prefixes[item.Prefix] + "]";
                tempNode2 = new TreeNode(prefix + Constants.Items[item.ItemID].ItemName + " " + item.Nick);
                tempNode2.Tag = item;
                tempNode2.Name = "armor";
                tempNode.Nodes.Add(tempNode2);
            }
        }

        private void AddInventory(Player player)
        {
            var tempNode = new TreeNode();
            var tempNode2 = new TreeNode();

            tempNode.Text = "Inventory";
            tempNode.Tag = player.Inventory;
            tempNode.Name = "invNode";
            treeInv.Nodes.Add(tempNode);

            foreach (var item in player.Inventory)
            {
                string prefix;
                if (item.Prefix == 0)
                    prefix = "";
                else
                    prefix = "[" + Constants.Prefixes[item.Prefix] + "]";

                if (item.ItemName == null)
                    tempNode2 = new TreeNode("");
                if (item.Nick == null)
                    tempNode2 = new TreeNode(prefix + Constants.Items[item.ItemID].ItemName);

                tempNode2 = new TreeNode(prefix + Constants.Items[item.ItemID].ItemName + " " + item.Nick);
                tempNode2.Tag = item;
                tempNode2.Name = "item";
                tempNode.Nodes.Add(tempNode2);
            }
        }

        private void AddAmmo(Player player)
        {
            var tempNode = new TreeNode();
            var tempNode2 = new TreeNode();

            tempNode.Text = "Ammo";
            tempNode.Tag = player.Ammo;
            tempNode.Name = "ammoNode";
            treeInv.Nodes.Add(tempNode);

            foreach (var item in player.Ammo)
            {
                string prefix;
                if (item.Prefix == 0)
                    prefix = "";
                else
                    prefix = "[" + Constants.Prefixes[item.Prefix] + "]";
                tempNode2 = new TreeNode(prefix + Constants.Items[item.ItemID].ItemName + " " + item.Nick);
                tempNode2.Tag = item;
                tempNode2.Name = "item";
                tempNode.Nodes.Add(tempNode2);
            }
        }

        private void AddArmor(Player player)
        {
            var tempNode = new TreeNode();
            var tempNode2 = new TreeNode();

            tempNode = new TreeNode("Armor");
            tempNode.Tag = player.Armor;
            tempNode.Name = "armorNode";
            treeInv.Nodes.Add(tempNode);

            foreach (var item in player.Armor)
            {
                string prefix;
                if (item.Prefix == 0)
                    prefix = "";
                else
                    prefix = "[" + Constants.Prefixes[item.Prefix] + "]";
                tempNode2 = new TreeNode(prefix + Constants.Items[item.ItemID].ItemName + " " + item.Nick);
                tempNode2.Tag = item;
                tempNode2.Name = "armor";
                tempNode.Nodes.Add(tempNode2);
            }
        }

        private void AddPiggyBank(Player player)
        {
            var tempNode = new TreeNode();
            var tempNode2 = new TreeNode();

            tempNode = new TreeNode("Piggy Bank");
            tempNode.Tag = player.Bank;
            tempNode.Name = "piggyNode";
            treeInv.Nodes.Add(tempNode);

            foreach (var item in player.Bank)
            {
                string prefix;
                if (item.Prefix == 0)
                    prefix = "";
                else
                    prefix = "[" + Constants.Prefixes[item.Prefix] + "]";
                tempNode2 = new TreeNode(prefix + Constants.Items[item.ItemID].ItemName + " " + item.Nick);
                tempNode2.Tag = item;
                tempNode2.Name = "item";
                tempNode.Nodes.Add(tempNode2);
            }
        }

        private void AddPiggyBank2(Player player)
        {
            var tempNode = new TreeNode();
            var tempNode2 = new TreeNode();

            tempNode = new TreeNode("Safe");
            tempNode.Tag = player.Bank2;
            tempNode.Name = "piggyNode2";
            treeInv.Nodes.Add(tempNode);

            foreach (var item in player.Bank2)
            {
                string prefix;
                if (item.Prefix == 0)
                    prefix = "";
                else
                    prefix = "[" + Constants.Prefixes[item.Prefix] + "]";
                tempNode2 = new TreeNode(prefix + Constants.Items[item.ItemID].ItemName + " " + item.Nick);
                tempNode2.Tag = item;
                tempNode2.Name = "item";
                tempNode.Nodes.Add(tempNode2);
            }
        }

        private void AddPiggyBank3(Player player)
        {
            var tempNode = new TreeNode();
            var tempNode2 = new TreeNode();

            tempNode = new TreeNode("???");
            tempNode.Tag = player.Bank3;
            tempNode.Name = "piggyNode3";
            treeInv.Nodes.Add(tempNode);

            foreach (var item in player.Bank3)
            {
                string prefix;
                if (item.Prefix == 0)
                    prefix = "";
                else
                    prefix = "[" + Constants.Prefixes[item.Prefix] + "]";
                tempNode2 = new TreeNode(prefix + Constants.Items[item.ItemID].ItemName + " " + item.Nick);
                tempNode2.Tag = item;
                tempNode2.Name = "item";
                tempNode.Nodes.Add(tempNode2);
            }
        }

        private void AddDye(Player player)
        {
            var tempNode = new TreeNode();
            var tempNode2 = new TreeNode();

            tempNode = new TreeNode("Armor Dyes");
            tempNode.Tag = player.Dye;
            tempNode.Name = "dyeNode";
            treeInv.Nodes.Add(tempNode);

            foreach (var item in player.Dye)
            {
                string prefix;
                if (item.Prefix == 0)
                    prefix = "";
                else
                    prefix = "[" + Constants.Prefixes[item.Prefix] + "]";
                tempNode2 = new TreeNode(prefix + Constants.Items[item.ItemID].ItemName + " " + item.Nick);
                tempNode2.Tag = item;
                tempNode2.Name = "dye";
                tempNode.Nodes.Add(tempNode2);
            }
        }

        private void AddPurse(Player player)
        {
            var tempNode = new TreeNode("Coin Purse");
            tempNode.Tag = player.Purse;
            tempNode.Name = "purseNode";
            treeInv.Nodes.Add(tempNode);

            foreach (var item in player.Purse)
            {
                string prefix;
                if (item.Prefix == 0)
                    prefix = "";
                else
                    prefix = "[" + Constants.Prefixes[item.Prefix] + "]";
                var tempNode2 = new TreeNode(prefix + Constants.Items[item.ItemID].ItemName + " " + item.Nick);
                tempNode2.Tag = item;
                tempNode2.Name = "item";
                tempNode.Nodes.Add(tempNode2);
            }
        }

        private void AddBuffs(Player player)
        {
            var tempNode = new TreeNode("Buffs");
            tempNode.Tag = player.Buffs;
            tempNode.Name = "buffNode";
            treeBuff.Nodes.Add(tempNode);

            foreach (var buff in player.Buffs)
            {
                if (!BuffList.Images.ContainsKey(buff.BuffID.ToString()))
                    BuffList.Images.Add(buff.BuffID.ToString(), GetBitmap("Buff/{0}.png", buff.BuffID));

                if (string.IsNullOrEmpty(buff.BuffName))
                    buff.BuffName = "(Empty)";
                var tempNode2 = new TreeNode(buff.BuffName);
                tempNode2.Tag = buff;
                tempNode2.ImageKey = buff.BuffID.ToString();
                tempNode2.SelectedImageKey = buff.BuffID.ToString();
                tempNode2.Name = "buff";
                tempNode.Nodes.Add(tempNode2);
            }
        }

        #endregion

        #region Save

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (PPlayer != null)
                SavePlayer(PPlayer.FilePath);
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            if (PPlayer != null)
            {
                savFileDialog.FileName = PPlayer.Name;
                if (savFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                SavePlayer(savFileDialog.FileName);
            }
        }

        public void SavePlayer(string destPath)
        {
            var Errors = new List<string>();
            var Warnings = new List<string>();

            var NonSpace = false;
            foreach (var Char in txtName.Text)
                if (Char != ' ')
                {
                    NonSpace = true;
                    break;
                }
            if (txtName.Text.Equals(string.Empty) || !NonSpace || txtName.Text[0] == ' ')
                Errors.Add("Please input a valid name!");

            if (numHP.Value > numMaxHP.Value)
                Warnings.Add("Your HP: (" + numHP.Value + ") is higher than your MaxHP: (" + numMaxHP.Value + ")");
            if (numHP.Value > 500 || numMaxHP.Value > 500)
                Warnings.Add("Your HP going over the game's limit of (" + 500 +
                             ") MAY prevent you from joining certain servers!");


            if (Warnings.Count > 0)
            {
                var result = MessageBox.Show("Warnings: \r\n" + string.Join(Environment.NewLine, Warnings), "Warning!",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (Errors.Count == 0)
            {
                PPlayer.Name = txtName.Text;
                PPlayer.HP = (int)numHP.Value;
                PPlayer.MaxHP = (int)numMaxHP.Value;
                PPlayer.MP = (int)numMP.Value;
                PPlayer.MaxMP = (int)numMaxMP.Value;
                PPlayer.Difficulty = (byte)comboDifficulty.SelectedIndex;
                PPlayer.SkinVariant = (byte)numSkinVariant.Value;
                PPlayer.HotBarLocked = chkHBLocked.Checked;
                PPlayer.ExtraAccessory = chkExtraAccessory.Checked;
                PPlayer.TaxMoney = Convert.ToInt32(numTaxMoney.Value);
                PPlayer.AnglerQuestsFinished = Convert.ToInt32(numAQuests.Value);
                PPlayer.SavePlayer(destPath);
            }
            else
            {
                MessageBox.Show("Player not Saved!\r\n" + string.Join(Environment.NewLine, Errors), "",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PPlayer_PlayerSaved(object sender, EventArgs e)
        {
            PPlayer.LoadPlayer();
        }

        #endregion

        private void btnReload_Click(object sender, EventArgs e)
        {
            if (PPlayer != null)
                PPlayer.LoadPlayer();
        }

        #endregion

        #region Stats

        #region ValueChanged

        #region HP

        private void numHP_ValueChanged(object sender, EventArgs e)
        {
            if (PPlayer != null)
                if (numHP.Value <= pbHealth.Maximum)
                {
                    pbHealth.Value = (int)numHP.Value;
                    pbHealth.ForeColor = Color.DarkRed;
                }
                else
                {
                    pbHealth.Value = pbHealth.Maximum;
                    pbHealth.ForeColor = Color.DeepPink;
                }
        }

        private void numMaxHP_ValueChanged(object sender, EventArgs e)
        {
            if (PPlayer != null)
            {
                pbHealth.Maximum = (int)numMaxHP.Value;

                if (numHP.Value <= pbHealth.Maximum)
                {
                    pbHealth.Value = (int)numHP.Value;
                    pbHealth.ForeColor = Color.DarkRed;
                }
                else
                {
                    pbHealth.Value = pbHealth.Maximum;
                    pbHealth.ForeColor = Color.HotPink;
                }
            }
        }

        #endregion

        #region MP

        private void numMP_ValueChanged(object sender, EventArgs e)
        {
            if (PPlayer != null)
                if (numMP.Value <= pbMana.Maximum)
                {
                    pbMana.Value = (int)numMP.Value;
                    pbMana.ForeColor = SystemColors.HotTrack;
                }
                else
                {
                    pbMana.Value = pbMana.Maximum;
                    pbMana.ForeColor = Color.SlateBlue;
                }
        }

        private void numMaxMP_ValueChanged(object sender, EventArgs e)
        {
            if (PPlayer != null)
            {
                pbMana.Maximum = (int)numMaxMP.Value;

                if (numMP.Value <= pbMana.Maximum)
                {
                    pbMana.Value = (int)numMP.Value;
                    pbMana.ForeColor = SystemColors.HotTrack;
                }
                else
                {
                    pbMana.Value = pbMana.Maximum;
                    pbMana.ForeColor = Color.SlateBlue;
                }
            }
        }

        #endregion

        #endregion

        private void btnHeal_Click(object sender, EventArgs e)
        {
            numHP.Value = numMaxHP.Value;
        }

        private void btnRegen_Click(object sender, EventArgs e)
        {
            numMP.Value = numMaxMP.Value;
        }

        #endregion

        #region Looks

        #region Color Clicked

        private void btnHair_Click(object sender, EventArgs e)
        {
            if (PPlayer != null)
            {
                var frm = new ColorChoose(PPlayer.HairColor);
                frm.ShowDialog();
                btnHair.BackColor = PPlayer.HairColor = frm.curColor;
                DrawCharacter();
            }
        }

        private void btnSkin_Click(object sender, EventArgs e)
        {
            if (PPlayer != null)
            {
                var frm = new ColorChoose(PPlayer.SkinColor);
                frm.ShowDialog();
                btnSkin.BackColor = PPlayer.SkinColor = frm.curColor;
                DrawCharacter();
            }
        }

        private void btnEye_Click(object sender, EventArgs e)
        {
            if (PPlayer != null)
            {
                var frm = new ColorChoose(PPlayer.EyeColor);
                frm.ShowDialog();
                btnEye.BackColor = PPlayer.EyeColor = frm.curColor;
                DrawCharacter();
            }
        }

        private void btnShirt_Click(object sender, EventArgs e)
        {
            if (PPlayer != null)
            {
                var frm = new ColorChoose(PPlayer.ShirtColor);
                frm.ShowDialog();
                btnShirt.BackColor = PPlayer.ShirtColor = frm.curColor;
                DrawCharacter();
            }
        }

        private void btnUShirt_Click(object sender, EventArgs e)
        {
            if (PPlayer != null)
            {
                var frm = new ColorChoose(PPlayer.UnderShirtColor);
                frm.ShowDialog();
                btnUShirt.BackColor = PPlayer.UnderShirtColor = frm.curColor;
                DrawCharacter();
            }
        }

        private void btnPants_Click(object sender, EventArgs e)
        {
            if (PPlayer != null)
            {
                var frm = new ColorChoose(PPlayer.PantsColor);
                frm.ShowDialog();
                btnPants.BackColor = PPlayer.PantsColor = frm.curColor;
                DrawCharacter();
            }
        }

        private void btnShoes_Click(object sender, EventArgs e)
        {
            if (PPlayer != null)
            {
                var frm = new ColorChoose(PPlayer.ShoeColor);
                frm.ShowDialog();
                btnShoes.BackColor = PPlayer.ShoeColor = frm.curColor;
                DrawCharacter();
            }
        }

        #endregion

        #region Drawing

        public void DrawCharacter(bool EquipUpdate = false)
        {
            if (!Loading && (EquipUpdate && cbEquips.Checked || !EquipUpdate))
            {
                var Character = new Bitmap(pbCharacter.Width, pbCharacter.Height);

                using (var Sprite = Graphics.FromImage(Character))
                {
                    Sprite.InterpolationMode = InterpolationMode.NearestNeighbor;
                    var neo = new Matrix();
                    neo.Scale(dpiX / 100, dpiY / 100, MatrixOrder.Append);
                    Sprite.Transform = neo;
                    var Clip = new Rectangle(0, 0, 40, 55);

                    DrawPants(Sprite, Clip);
                    DrawShirt(Sprite, Clip);
                    DrawHead(Sprite, Clip);
                }
                pbCharacter.Image = Character;
                pbCharacter.BackgroundImage = GetBitmap("Background/74.png");
            }
        }

        public void DrawHead(Graphics Sprite, Rectangle Clip)
        {
            var Helmet = PPlayer.Armor[8];
            Sprite.DrawImage(GetHead(), 40, 15, Clip, GraphicsUnit.Pixel);
            Sprite.DrawImage(GetEyes(), 40, 15, Clip, GraphicsUnit.Pixel);
            Sprite.DrawImage(GetBitmap("Player/Eye/Whites.png"), 40, 15, Clip, GraphicsUnit.Pixel);

            if (cbEquips.CheckState == CheckState.Indeterminate && Helmet.ItemID > 0 &&
                (Helmet = Constants.Items[Helmet.ItemID]).HeadSlot != 0 ||
                cbEquips.CheckState != CheckState.Unchecked && (Helmet = PPlayer.Armor[0]).ItemID > 0 &&
                (Helmet = Constants.Items[Helmet.ItemID]).HeadSlot != 0)
            {
                Sprite.DrawImage(GetHair(true), 40, 15, Clip, GraphicsUnit.Pixel);
                Sprite.DrawImage(GetHelmet(Helmet.HeadSlot), 40, 15, Clip, GraphicsUnit.Pixel);
            }
            else
            {
                Sprite.DrawImage(GetHair(false), 40, 15, Clip, GraphicsUnit.Pixel);
            }
        }

        public void DrawShirt(Graphics Sprite, Rectangle Clip)
        {
            var Shirt = PPlayer.Armor[9];
            if (cbEquips.CheckState == CheckState.Indeterminate && Shirt.ItemID > 0 &&
                (Shirt = Constants.Items[Shirt.ItemID]).BodySlot != 0 ||
                cbEquips.CheckState != CheckState.Unchecked && (Shirt = PPlayer.Armor[1]).ItemID > 0 &&
                (Shirt = Constants.Items[Shirt.ItemID]).BodySlot != 0)
            {
                Sprite.DrawImage(GetBodyArmor(Shirt.BodySlot), 40, 15, Clip, GraphicsUnit.Pixel);
            }
            else
            {
                Sprite.DrawImage(GetShirt(), 40, 15, Clip, GraphicsUnit.Pixel);
                Sprite.DrawImage(GetUndershirt(), 40, 15, Clip, GraphicsUnit.Pixel);
                Sprite.DrawImage(GetHands(), 40, 15, Clip, GraphicsUnit.Pixel);
            }
        }

        public void DrawPants(Graphics Sprite, Rectangle Clip)
        {
            var Pants = PPlayer.Armor[10];
            if (cbEquips.CheckState == CheckState.Indeterminate && Pants.ItemID > 0 &&
                (Pants = Constants.Items[Pants.ItemID]).LegSlot != 0 ||
                cbEquips.CheckState != CheckState.Unchecked && (Pants = PPlayer.Armor[2]).ItemID > 0 &&
                (Pants = Constants.Items[Pants.ItemID]).LegSlot != 0)
            {
                Sprite.DrawImage(GetLegArmor(Pants.LegSlot), 40, 15, Clip, GraphicsUnit.Pixel);
            }
            else
            {
                if (PPlayer.SkinVariant >= 4)
                    Sprite.DrawImage(GetLegs(), 40, 15, Clip, GraphicsUnit.Pixel);
                Sprite.DrawImage(GetPants(), 40, 15, Clip, GraphicsUnit.Pixel);
                Sprite.DrawImage(GetShoes(), 40, 15, Clip, GraphicsUnit.Pixel);
            }
        }

        #region GetTextures

        private Image GetHead()
        {
            var img = GetBitmap("Player/Head.png");
            ColorImage(ref img, PPlayer.SkinColor);

            return img;
        }

        private Image GetEyes()
        {
            var img = GetBitmap("Player/Eyes.png");
            ColorImage(ref img, PPlayer.EyeColor);

            return img;
        }

        private Image GetHair(bool Helmet)
        {
            var img = GetBitmap("Player/Hair{0}/{1}.png", Helmet ? "Alt" : string.Empty, PPlayer.Hair + 1);
            ColorImage(ref img, PPlayer.HairColor);

            return img;
        }

        private Image GetHelmet(int Slot)
        {
            var img = GetBitmap("Armor/Head/{0}.png", Slot);
            //ColorImage(ref img, PPlayer.SkinColor);

            return img;
        }

        private Image GetShirt()
        {
            var img = GetBitmap("{0}/Shirt.png", !PPlayer.IsMale ? "Female" : "Player");
            ColorImage(ref img, PPlayer.ShirtColor);

            return img;
        }

        private Image GetUndershirt()
        {
            var img = GetBitmap("{0}/Undershirt.png", !PPlayer.IsMale ? "Female" : "Player");
            ColorImage(ref img, PPlayer.UnderShirtColor);

            return img;
        }

        private Image GetHands()
        {
            var img = GetBitmap("Player/Hands.png");
            ColorImage(ref img, PPlayer.SkinColor);

            return img;
        }

        private Image GetBodyArmor(int Slot)
        {
            var img = GetBitmap("{0}/Body/{1}.png", !PPlayer.IsMale ? "Female" : "Armor", Slot);
            //ColorImage(ref img, PPlayer.SkinColor);

            return img;
        }

        private Image GetLegs()
        {
            var img = GetBitmap("Skin/Legs.png");
            ColorImage(ref img, PPlayer.SkinColor);

            return img;
        }

        private Image GetPants()
        {
            var img = GetBitmap("{0}/Pants.png", !PPlayer.IsMale ? "Female" : "Player");
            ColorImage(ref img, PPlayer.PantsColor);

            return img;
        }

        private Image GetShoes()
        {
            var img = GetBitmap("{0}/Shoes.png", !PPlayer.IsMale ? "Female" : "Player");
            ColorImage(ref img, PPlayer.ShoeColor);

            return img;
        }

        private Image GetLegArmor(int Slot)
        {
            var img = GetBitmap("Armor/Legs/{0}.png", Slot);
            //ColorImage(ref img, PPlayer.SkinColor);

            return img;
        }

        public Image GetItem(int ID, Color? Color = null)
        {
            var img = GetBitmap("Item/{0}.png", ID);
            if (Color != null)
                ColorImage(ref img, Color.Value);

            return img;
        }

        #endregion

        public void ColorImage(ref Bitmap img, Color Color)
        {
            for (var x = 0; x < img.Width; x++)
                for (var y = 0; y < img.Height && y < 55; y++)
                {
                    var pixel = img.GetPixel(x, y);

                    if (pixel != Color.FromArgb(0, 0, 0, 0))
                        if (pixel == Color.FromArgb(255, 249, 249, 249))
                        {
                            img.SetPixel(x, y, Color.White);
                        }
                        else
                        {
                            var R = pixel.R * Color.R / 255;
                            var G = pixel.G * Color.G / 255;
                            var B = pixel.B * Color.B / 255;

                            img.SetPixel(x, y, Color.FromArgb(R, G, B));
                        }
                }
        }

        #endregion

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (PPlayer.Hair < 133)
            {
                PPlayer.Hair++;
                DrawCharacter();
            }
            else
            {
                PPlayer.Hair = 0;
                DrawCharacter();
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (PPlayer.Hair > 0)
            {
                PPlayer.Hair--;
                DrawCharacter();
            }
            else
            {
                PPlayer.Hair = 133;
                DrawCharacter();
            }
        }

        private void cbEquips_CheckStateChanged(object sender, EventArgs e)
        {
            DrawCharacter();
        }

        #endregion

        #region Inventory

        #region AfterSelect

        private void treeInv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (PPlayer != null)
            {
                var clickedTreeNodes = new List<TreeNode>();
                clickedTreeNodes = treeInv.SelectedNodes;
                TreeNode clickedNode;
                if (clickedTreeNodes.Count == 1)
                    clickedNode = treeInv.SelectedNode;
                else
                    clickedNode = clickedTreeNodes[0];

                if (clickedNode.Parent == null) return;
                if (!txtSearch.Text.Contains("coin") && !txtSearch.Text.Contains("dye"))
                    txtSearch.Tag = txtSearch.Text;
                switch (clickedNode.Parent.Name.ToLower())
                {
                    case "pursenode":
                        if (!txtSearch.Text.Contains("coin"))
                        {
                            txtSearch.Text = "coin";
                            txtSearch.AcceptsReturn = true; //Poor Mans hack
                        }
                        break;
                    case "dyenode":
                        if (!txtSearch.Text.Contains("dye"))
                        {
                            txtSearch.Text = "dye";
                            txtSearch.AcceptsReturn = true;
                        }
                        break;
                    case "miscdyenode":
                        if (!txtSearch.Text.Contains("dye"))
                        {
                            txtSearch.Text = "dye";
                            txtSearch.AcceptsReturn = true;
                        }
                        break;
                    default:
                        if (txtSearch.Tag is string && txtSearch.AcceptsReturn)
                        {
                            txtSearch.Text = txtSearch.Tag as string;
                            txtSearch.AcceptsReturn = false;
                        }
                        break;
                }

                Loading = true;
                Item item;
                int index;
                switch (clickedNode.Name.ToLower())
                {
                    case "dye":
                        item = (Item)clickedNode.Tag;
                        index = item.Prefix != 0 ? item.Prefix : 0;
                        comboPrefix.SelectedIndex = index;
                        lbItems.SelectedItem = Constants.Items[item.ItemID];
                        lbStackSize.Visible = false;
                        splitContainer.Panel2Collapsed = false;
                        numStackSize.Visible = false;
                        btnMaxStack.Visible = false;
                        btnMaxAll.Visible = false;
                        chkFavoritedItem.Visible = false;
                        break;
                    case "item":
                        item = (Item)clickedNode.Tag;
                        index = item.Prefix != 0 ? item.Prefix : 0;
                        comboPrefix.SelectedIndex = index;
                        numStackSize.Value = item.Stack;
                        lbItems.SelectedItem = Constants.Items[item.ItemID];
                        chkFavoritedItem.Visible = true;
                        chkFavoritedItem.Checked = item.IsFavorite;
                        lbStackSize.Visible = true;
                        splitContainer.Panel2Collapsed = false;
                        btnMaxStack.Visible = true;
                        btnMaxAll.Visible = true;
                        numStackSize.Visible = true;
                        break;
                    case "armor":
                        item = (Item)clickedNode.Tag;
                        index = item.Prefix != 0 ? item.Prefix : 0;
                        comboPrefix.SelectedIndex = index;
                        lbItems.SelectedItem = Constants.Items[item.ItemID];
                        lbStackSize.Visible = false;
                        splitContainer.Panel2Collapsed = false;
                        numStackSize.Visible = false;
                        btnMaxStack.Visible = false;
                        btnMaxAll.Visible = false;
                        chkFavoritedItem.Visible = false;
                        break;
                    default:
                        splitContainer.Panel2Collapsed = true;
                        break;
                }
                Loading = false;
            }
        }

        #endregion

        #region MaxStack

        private void BtnMaxStackClick(object sender, EventArgs e)
        {
            if (treeInv.SelectedNode.Parent != null && lbItems.SelectedIndex != -1 && !Loading)
            {
                var item = (Item)lbItems.SelectedItem;
                item.Stack = Constants.Items[item.ItemID].MaxStack;
                numStackSize.Value = item.Stack;
            }
        }

        private void btnMaxAll_Click_1(object sender, EventArgs e)
        {
            if (PPlayer != null && !Loading)
            {
                foreach (TreeNode node2 in treeInv.Nodes)
                    foreach (TreeNode node in node2.Nodes)
                    {
                        var item = (Item)node.Tag;
                        var items = (Item[])node2.Tag;
                        if (item.ItemID == 0)
                        {
                            item.Stack = 0;
                            continue;
                        }
                        var maxstack = Constants.Items[item.ItemID].MaxStack;
                        item.Stack = maxstack;
                        node.Tag = item;
                        items[item.Index] = item;
                    }

                DoTreeView(true);
            }
        }

        #endregion

        #region ItemSelect

        private void comboItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (treeInv.SelectedNode != null && !Loading)
                if (treeInv.SelectedNode.Parent != null)
                    if (treeInv.SelectedNode.Tag is Item)
                    {
                        var kpItm = (Item)lbItems.SelectedItem;
                        var item = kpItm.ItemName;
                        var temp = (Item)treeInv.SelectedNode.Tag;
                        try
                        {
                            temp.ItemID = kpItm.ItemID;
                        }
                        catch (NullReferenceException)
                        {
                            MessageBox.Show("Item does not exist!", "Failed!");
                            return;
                        }
                        temp.Nick = "";
                        treeInv.SelectedNode.Tag = temp;
                    }
        }

        #endregion

        #region Update Node

        private void UpdateNodeItem()
        {
            if (treeInv.SelectedNode?.Parent != null && !Loading)
            {
                foreach (var node in treeInv.SelectedNodes)
                    switch (node.Name.ToLower())
                    {
                        case "armornode":
                            return;
                        case "invnode":
                            return;
                        case "ammonode":
                            return;
                        case "piggynode":
                            return;
                        case "piggynode2":
                            return;
                        case "piggynode3":
                            return;
                        case "dyenode":
                            return;
                        case "pursenode":
                            return;
                        case "miscdyenode":
                            return;
                        case "miscequipnode":
                            return;
                        default:
                            continue;
                    }
                foreach (var node in treeInv.SelectedNodes)
                    if (node.Name == "armor" || node.Name == "item" || node.Name == "dye")
                    {
                        var items = (Item[])node.Parent.Tag;
                        var item = (Item)node.Tag;
                        item.ItemName = node.Text;
                        var kpItm = (Item)lbItems.SelectedItem;
                        var stack = (int)numStackSize.Value;
                        var max = Constants.Items[kpItm.ItemID].MaxStack;
                        var prefix = (byte)comboPrefix.SelectedIndex;


                        if (kpItm.ItemID == 0)
                        {
                            comboPrefix.SelectedIndex = 0;
                            numStackSize.Value = 0;
                            stack = 0;
                            prefix = 0;
                        }

                        if (stack > max && !Stack)
                        {
                            var result =
                                MessageBox.Show(
                                    "Going over your the stack size limit of (" + max + ") for this item (" + kpItm +
                                    ") MAY prevent you from joining certain servers!", "Warning!", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                            Stack = true;
                        }
                        else if (stack > 999 && !Stack)
                        {
                            var result =
                                MessageBox.Show(
                                    "Going over your the stack size limit of (" + 999 +
                                    ") for Terraria on this item (" + kpItm +
                                    ")  MAY prevent you from joining certain servers!", "Warning!",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Stack = true;
                        }

                        items[item.Index].Stack = stack;
                        items[item.Index].ItemID = kpItm.ItemID;
                        items[item.Index].Prefix = prefix;
                        items[item.Index].IsFavorite = chkFavoritedItem.Checked;
                        string strPrefix;
                        if (items[item.Index].Prefix == 0)
                            strPrefix = "";
                        else
                            strPrefix = "[" + Constants.Prefixes[items[item.Index].Prefix] + "]";


                        node.Tag = items[item.Index];
                        node.Text = strPrefix + Constants.Items[items[item.Index].ItemID].ItemName + " " +
                                    items[item.Index].Nick;
                        node.BackColor = items[item.Index].IsFavorite ? Color.LightYellow : Color.White;
                        node.TreeView.Refresh();
                        if (node.Name == "armor")
                            DrawCharacter(true);
                    }
            }
            treeInv.Refresh();
        }

        #endregion

        #region Item Selection Changed

        private void lbItems_SelectedValueChanged(object sender, EventArgs e)
        {
            // IMG PICTUREBOX
            if (lbItems.SelectedIndex != -1)
            {
                var itm = (Item)lbItems.SelectedItem;

                if (itm.ItemID == 0)
                    numStackSize.Value = 0;

                if (numStackSize.Value == 0 && itm.ItemID != 0)
                    numStackSize.Value = 1;


                ///SPECIAL CASES:

                //Pickaxes
                if (itm.ItemID == -1 || itm.ItemID == -7 || itm.ItemID == -13 || itm.ItemID == -25 ||
                    itm.ItemID == -31 || itm.ItemID == -37 || itm.ItemID == -43)
                {
                    pbItem.Image = GetItem(1, itm.GetColor());
                }
                //Broadswords
                else if (itm.ItemID == -2 || itm.ItemID == -8 || itm.ItemID == -14 || itm.ItemID == -26 ||
                         itm.ItemID == -32 || itm.ItemID == -38 || itm.ItemID == -44)
                {
                    pbItem.Image = GetItem(4, itm.GetColor());
                }
                //Shortswords
                else if (itm.ItemID == -3 || itm.ItemID == -9 || itm.ItemID == -15 || itm.ItemID == -27 ||
                         itm.ItemID == -33 || itm.ItemID == -39 || itm.ItemID == -45)
                {
                    pbItem.Image = GetItem(6, itm.GetColor());
                }
                //Axes
                else if (itm.ItemID == -4 || itm.ItemID == -10 || itm.ItemID == -16 || itm.ItemID == -28 ||
                         itm.ItemID == -34 || itm.ItemID == -40 || itm.ItemID == -46)
                {
                    pbItem.Image = GetItem(10, itm.GetColor());
                }
                //Hammers
                else if (itm.ItemID == -5 || itm.ItemID == -11 || itm.ItemID == -17 || itm.ItemID == -29 ||
                         itm.ItemID == -35 || itm.ItemID == -41 || itm.ItemID == -47)
                {
                    pbItem.Image = GetItem(7, itm.GetColor());
                }
                //Bows
                else if (itm.ItemID == -6 || itm.ItemID == -12 || itm.ItemID == -18 || itm.ItemID == -30 ||
                         itm.ItemID == -36 || itm.ItemID == -42 || itm.ItemID == -48)
                {
                    pbItem.Image = GetItem(99, itm.GetColor());
                }
                //Phasesabers
                else if (itm.ItemID <= -19 && itm.ItemID >= -24)
                {
                    var magicByte = 179;
                    magicByte += itm.ItemID * -1;
                    pbItem.Image = GetItem(magicByte, itm.GetColor());
                }
                else
                {
                    pbItem.Image = GetItem(itm.ItemID, itm.GetColor());
                }
            }
        }

        #endregion

        #region Search Filter

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ((TextBox)sender).Text = ((TextBox)sender).Text.Replace("^", string.Empty).Replace("=", string.Empty);
            lbItems.BeginUpdate();
            var dataSource = (BindingSource)lbItems.DataSource;
            if (((TextBox)sender).Text != string.Empty)
            {
                var matches = ((TextBox)sender).Text.Replace("^", string.Empty).Replace("=", string.Empty).Split(',');
                var filter = string.Empty;

                for (var i = 0; i < matches.Length; i++)
                    filter += $"{(i == 0 ? string.Empty : " AND ")}ItemName ^ {matches[i]}";
                dataSource.Filter = filter;
            }
            else
            {
                dataSource.Filter = null;
            }
            lbItems.EndUpdate();
        }

        #endregion

        #endregion

        #region Buffs

        #region AfterSelect

        private void treeBuff_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (PPlayer != null)
            {
                var clickedTreeNodes = new List<TreeNode>();
                clickedTreeNodes = treeBuff.SelectedNodes;
                TreeNode clickedNode;
                if (clickedTreeNodes.Count == 1)
                    clickedNode = treeBuff.SelectedNode;
                else
                    clickedNode = clickedTreeNodes[0];

                switch (clickedNode.Name.ToLower())
                {
                    case "buff":
                        splitContainer1.Panel2Collapsed = false;
                        var buff = (Buff)clickedNode.Tag;
                        numTime.Value = buff.BuffTime / 60;
                        rtbDesc.Text = buff.BuffDescription;
                        var index = buff.BuffID;
                        comboBuff.SelectedIndex = index;
                        break;
                    default:
                        splitContainer1.Panel2Collapsed = true;
                        break;
                }
            }
        }

        #endregion

        #region Save

        private void btnSaveBuff_Click(object sender, EventArgs e)
        {
            if (treeBuff.SelectedNode.Parent != null)
            {
                foreach (var node in treeBuff.SelectedNodes)
                    switch (node.Name.ToLower())
                    {
                        case "buffnode":
                            return;
                        default:
                            continue;
                    }
                foreach (var node in treeBuff.SelectedNodes)
                    if (node.Name == "buff")
                    {
                        var buffs = (Buff[])node.Parent.Tag;
                        var buff = (Buff)node.Tag;
                        Buff newBuff;

                        var buffname = comboBuff.SelectedItem.ToString();
                        var buffId = comboBuff.SelectedIndex;
                        var time = (int)numTime.Value * 60;
                        if (buffname == "(Empty)")
                        {
                            buffname = "(Empty)";
                            numTime.Value = 0;
                            buffId = 0;
                            time = 0;
                        }
                        newBuff = new Buff(buffId, time, buff.Slot);
                        buffs[buff.Slot] = newBuff;
                        node.Tag = newBuff;
                        node.ImageKey = buffId.ToString();
                        node.SelectedImageKey = buffId.ToString();

                        if (string.IsNullOrEmpty(newBuff.BuffName))
                            newBuff.BuffName = "(Empty)";

                        node.Text = newBuff.BuffName;
                        rtbDesc.Text = newBuff.BuffDescription;
                    }
            }
        }

        #endregion

        private void comboBuff_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!BuffList.Images.ContainsKey(comboBuff.SelectedIndex.ToString()))
                BuffList.Images.Add(comboBuff.SelectedIndex.ToString(),
                    GetBitmap("Buff/{0}.png", comboBuff.SelectedIndex));
            pbSelectedBuff.Image = BuffList.Images[comboBuff.SelectedIndex.ToString()];
            rtbDesc.Text = Constants.Buffs[comboBuff.SelectedIndex].BuffDescription;
        }

        private void btnMaxTime_Click(object sender, EventArgs e)
        {
            if (treeBuff.SelectedNode.Parent != null)
            {
                var buff = (Buff)treeBuff.SelectedNode.Tag;
                buff.BuffTime = int.MaxValue / 60;
                numTime.Value = buff.BuffTime;
                treeBuff.SelectedNode.Tag = buff;
            }
        }

        #endregion
    }
}