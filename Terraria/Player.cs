using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using TerrariaInvEdit.UI.Forms;

namespace TerrariaInvEdit.Terraria
{
    public class Player
    {
        public string FilePath { get; set; }
        public bool Loaded { get; set; }
        public int TerrariaVersion { get; set; }
        public string Name { get; set; }
        public byte Difficulty { get; set; }

        public int Hair { get; set; }
        public byte HairDye { get; set; }
        public TimeSpan PlayTime { get; set; }

        /// <summary>
        /// What exactly are these guys doing
        /// </summary>
        public BitArray HideVisual { get; set; }
        public BitArray HideVisual2 { get; set; }
        public BitArray HideMisc { get; set; }

        public byte[] HideBytes = new byte[3];
        public bool[] HideInfo = new bool[13];

        public bool ExtraAccessory;
        public int TaxMoney;
        public bool DownedDD2EventAnyDifficulty;
        public byte SkinVariant;

        public int[] DPadRadialBindings = new int[4];
        public int[] BuilderAccStatus = new int[10];
        public int BartenderQuestLog;
        // endconfusion

        public bool IsMale { get { return SkinVariant < 4; } }

        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int MP { get; set; }
        public int MaxMP { get; set; }

        public Color HairColor;
        public Color SkinColor;
        public Color EyeColor;
        public Color ShirtColor;
        public Color UnderShirtColor;
        public Color PantsColor;
        public Color ShoeColor;

        public Item[] Armor = new Item[20];
        public Item[] Dye = new Item[10];
        public Item[] Inventory = new Item[50];
        public Item[] MiscEquip = new Item[5];
        public Item[] MiscDye = new Item[5];
        public Item[] Bank = new Item[40];
        public Item[] Bank2 = new Item[40];
        public Item[] Bank3 = new Item[40]; // ???
        public Item[] Purse = new Item[4];
        public Item[] Ammo = new Item[4];
        public Buff[] Buffs = new Buff[22];

        public int[] SpawnX = new int[200];
        public int[] SpawnY = new int[200];
        public int[] WorldIDs = new int[200];
        public string[] WorldNames = new string[200];
        public bool HBLocked { get; set; }
        public int AnglerQuestsFinished { get; set; }

        public delegate void PlayerLoadedEventHandler(object sender, EventArgs e);
        public event PlayerLoadedEventHandler PlayerLoaded;
        public delegate void PlayerSavedEventHandler(object sender, EventArgs e);
        public event PlayerSavedEventHandler PlayerSaved;
        private ulong MagicNumber;
        private byte FileType;
        private uint Revision;
        private bool IsFavorite; // Doesn't work

        public Player(string path)
        {
            Loaded = false;
            FilePath = path;
        }

        protected virtual void OnLoaded(EventArgs e)
        {
            PlayerLoaded?.Invoke(this, e);
            Loaded = true;
        }

        protected virtual void OnSaved(EventArgs e)
        {
            PlayerSaved?.Invoke(this, e);
        }

        private static bool DecryptFile(string inputFile, string outputFile)
        {
            string s = "h3y_gUyZ";
            byte[] bytes = new UnicodeEncoding().GetBytes(s);

            FileStream stream = new FileStream(inputFile, FileMode.Open);
            RijndaelManaged managed = new RijndaelManaged();
            CryptoStream stream2 = new CryptoStream(stream, managed.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
            FileStream stream3 = new FileStream(outputFile, FileMode.Create);
            try
            {
                int num;
                while ((num = stream2.ReadByte()) != -1)
                {
                    stream3.WriteByte((byte)num);
                }
                stream3.Close();
                stream2.Close();
                stream.Close();
            }
            catch
            {
                stream3.Close();
                stream.Close();
                File.Delete(outputFile);
                return true;
            }
            return false;
        }

        private static void EncryptFile(string inputFile, string outputFile)
        {
            int num;
            string s = "h3y_gUyZ";
            byte[] bytes = new UnicodeEncoding().GetBytes(s);
            string path = outputFile;
            FileStream stream = new FileStream(path, FileMode.Create);


            RijndaelManaged managed = new RijndaelManaged();
            CryptoStream stream2 = new CryptoStream(stream, managed.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
            FileStream stream3 = new FileStream(inputFile, FileMode.Open);
            while ((num = stream3.ReadByte()) != -1)
            {
                stream2.WriteByte((byte)num);
            }
            stream3.Close();
            stream2.Close();
            stream.Close();
        }

        public static Player getMiniPlayer(string plrFile)
        {
            Player player = new Player(plrFile);
            bool flag = true;
            string datFile = plrFile + ".dat";
            try
            {
                try
                {
                    flag = DecryptFile(plrFile, datFile);
                }
                catch (UnauthorizedAccessException) { return null; }
                catch (IOException) { return null; }
                if (!flag)
                {
                    using (FileStream stream = new FileStream(datFile, FileMode.Open))
                    {
                        using (BinaryReader reader = new BinaryReader(stream))
                        {
                            player.TerrariaVersion = reader.ReadInt32();
                            if (player.TerrariaVersion < Constants.TERRARIA_RELEASE)
                                return null;
                            // Some terraria magic for relogic files
                            player.MagicNumber = reader.ReadUInt64();
                            if (((long)player.MagicNumber & 72057594037927935L) != 27981915666277746L)
                                return null;
                            player.FileType = (byte)(player.MagicNumber >> 56 & (ulong)byte.MaxValue);
                            if (player.FileType != 3)
                                return null;
                            player.Revision = reader.ReadUInt32(); // Useless metadata stuff in player files, for now? Only used in maps
                            player.IsFavorite = (((long)reader.ReadUInt64() & 1L) == 1L); // Does not actually matter? favorites.json seems to handle favorites
                            player.Name = reader.ReadString();
                            player.Difficulty = reader.ReadByte();
                            player.PlayTime = new TimeSpan(reader.ReadInt64()); // TimeSpan
                            reader.ReadBytes(8); // We don't need this
                            player.SkinVariant = reader.ReadByte();
                            reader.Close();
                        }
                    }
                    File.Delete(datFile);
                }
            }
            catch
            {
                flag = true;
            }
            if (flag)
            {
                return null;
            }

            return player;
        }

        public bool LoadPlayer(string _plrFile = "")
        {
            #region Error stuff
            if (!File.Exists(FilePath))
            {
                MessageBox.Show("Failed to load " + Path.GetFileName(_plrFile) + " because the file or path does not exist.", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            #endregion

            string plrFile = _plrFile == "" ? FilePath : _plrFile;
            bool flag = true;
            try
            {
                string datFile = FilePath + ".dat";
                try
                {
                    flag = DecryptFile(plrFile, datFile);
                }
                #region Error Handling
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Failed to load " + Path.GetFileName(_plrFile) + " because the access was denied. Try to run " + Application.ProductName + " " + Application.ProductVersion + " as administrator", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                catch (IOException)
                {
                    MessageBox.Show("Failed to load " + Path.GetFileName(_plrFile) + " because the file was in use by another application. Close it and try again.", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                #endregion

                if (!flag)
                {
                    using (FileStream stream = new FileStream(datFile, FileMode.Open))
                    {
                        using (BinaryReader reader = new BinaryReader(stream))
                        {
                            #region Loading character data

                            TerrariaVersion = reader.ReadInt32();
                            if (TerrariaVersion != Constants.TERRARIA_RELEASE)
                            {
                                MessageBox.Show("This version of the editor was optimized for " + Constants.TERRARIA_STRING + ". It may not function properly with other versions of Terraria. Careful!", "WARNING");
                                return false;
                            }
                            // Some terraria magic for relogic files
                            MagicNumber = reader.ReadUInt64();
                            if (((long)MagicNumber & 72057594037927935L) != 27981915666277746L)
                            {
                                MessageBox.Show("This is an invalid or corrupted Terraria File!", "Failed to load");
                                return false;
                            }
                            FileType = (byte)(MagicNumber >> 56 & (ulong)byte.MaxValue);
                            if (FileType != 3)
                            {
                                MessageBox.Show("This file is not a Terrarian player file!", "Failed to load");
                                return false;
                            }
                            Revision = reader.ReadUInt32(); // Useless metadata stuff in player files, for now? Only used in maps
                            IsFavorite = (((long)reader.ReadUInt64() & 1L) == 1L); // Does not actually matter? favorites.json seems to handle favorites
                            Name = reader.ReadString();
                            Difficulty = reader.ReadByte();
                            PlayTime = new TimeSpan(reader.ReadInt64()); // TimeSpan
                            Hair = reader.ReadInt32();
                            HairDye = reader.ReadByte(); // Hair Dye
                            /// TODO:
                            ///     Figure out which slots are actually being hidden, what's actually going on behind the hide bits
                            /*
                            HideVisual = new BitArray((byte)reader.ReadByte()); // Hide Visual 8 bits representing 8 what nows?
                            HideVisual2 = new BitArray((byte)reader.ReadByte());
                            HideMisc = new BitArray((byte)reader.ReadByte());
                             */
                            HideBytes = reader.ReadBytes(3); // Eat these three bytes and save them later

                            /// TODO:
                            ///     Figure out how Gender works in 1.3
                            SkinVariant = reader.ReadByte();
                            HP = reader.ReadInt32();
                            MaxHP = reader.ReadInt32();
                            MP = reader.ReadInt32();
                            MaxMP = reader.ReadInt32();
                            if (HP <= 0)
                                HP = MaxHP;
                            if (MP <= 0)
                                MP = MaxMP;
                            ExtraAccessory = reader.ReadBoolean();
                            DownedDD2EventAnyDifficulty = reader.ReadBoolean();
                            TaxMoney = reader.ReadInt32();
                            HairColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
                            SkinColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
                            EyeColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
                            ShirtColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
                            UnderShirtColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
                            PantsColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
                            ShoeColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());

                            #endregion
                            #region Loading Inventory
                            for (int i = 0; i < (Armor.Length / 2); ++i)
                            {
                                string nick = string.Empty;
                                switch (i)
                                {
                                    case 0:
                                        nick = "(Helmet)";
                                        break;
                                    case 1:
                                        nick = "(Shirt)";
                                        break;
                                    case 2:
                                        nick = "(Pants)";
                                        break;
                                    case 8:
                                        nick = "(Expert Accessory)";
                                        break;
                                    case 9:
                                        nick = "(Unknown Accessory)";
                                        break;
                                    default:
                                        nick = "(Accessory)";
                                        break;
                                }
                                Armor[i] = new Item
                                {
                                    ItemID = reader.ReadInt32(),
                                    Prefix = reader.ReadByte(),
                                    Index = i,
                                    ItemNick = nick
                                };
                            }
                            for (int i = 10; i < Armor.Length; ++i)
                            {
                                string nick = "(Social ";
                                switch (i)
                                {
                                    case 10:
                                        nick += "Helmet)";
                                        break;
                                    case 11:
                                        nick += "Shirt)";
                                        break;
                                    case 12:
                                        nick += "Pants)";
                                        break;
                                    case 18:
                                        nick += "Expert Accessory)";
                                        break;
                                    case 19:
                                        nick += "Unknown Accessory)";
                                        break;
                                    default:
                                        nick += "Accessory)";
                                        break;
                                }
                                Armor[i] = new Item
                                {
                                    ItemID = reader.ReadInt32(),
                                    Prefix = reader.ReadByte(),
                                    Index = i,
                                    ItemNick = nick
                                };
                            }
                            for (int i = 0; i < Dye.Length; ++i)
                            {
                                string nick = string.Empty;
                                switch (i)
                                {
                                    case 0:
                                        nick = "(Helmet)";
                                        break;
                                    case 1:
                                        nick = "(Shirt";
                                        break;
                                    case 2:
                                        nick = "(Pants";
                                        break;
                                    case 8:
                                        nick = "(Expert Accessory";
                                        break;
                                    case 9:
                                        nick = "(Unknown Accessory";
                                        break;
                                    default:
                                        nick = "(Accessory";
                                        break;
                                }
                                nick += " Dye)";
                                Dye[i] = new Item
                                    {
                                        ItemID = reader.ReadInt32(),
                                        Prefix = reader.ReadByte(),
                                        Index = i,
                                        ItemNick = nick
                                    };
                            }
                            for (int i = 0; i < Inventory.Length; ++i)
                            {
                                Inventory[i] = new Item
                                {
                                    ItemID = reader.ReadInt32(),
                                    Stack = reader.ReadInt32(),
                                    Prefix = reader.ReadByte(),
                                    IsFavorite = reader.ReadBoolean(),
                                    Index = i
                                };
                            }
                            for (int i = 0; i < Purse.Length; ++i)
                            {
                                Purse[i] = new Item
                                {
                                    ItemID = reader.ReadInt32(),
                                    Stack = reader.ReadInt32(),
                                    Prefix = reader.ReadByte(),
                                    IsFavorite = reader.ReadBoolean(),
                                    Index = i
                                };
                            }
                            for (int i = 0; i < Ammo.Length; ++i)
                            {
                                Ammo[i] = new Item
                                {
                                    ItemID = reader.ReadInt32(),
                                    Stack = reader.ReadInt32(),
                                    Prefix = reader.ReadByte(),
                                    IsFavorite = reader.ReadBoolean(),
                                    Index = i
                                };
                            }
                            for (int i = 0; i < MiscEquip.Length; ++i)
                            {
                                string nick = string.Empty;
                                switch (i)
                                {
                                    case 0:
                                        nick = "(Pet";
                                        break;
                                    case 1:
                                        nick = "(Light Pet";
                                        break;
                                    case 2:
                                        nick = "(Minecart";
                                        break;
                                    case 3:
                                        nick = "(Mount";
                                        break;
                                    case 4:
                                        nick = "(Grappling Hook";
                                        break;
                                }
                                MiscEquip[i] = new Item
                                {
                                    ItemID = reader.ReadInt32(),
                                    Prefix = reader.ReadByte(),
                                    Index = i,
                                    ItemNick = nick + ")"
                                };
                                MiscDye[i] = new Item
                                {
                                    ItemID = reader.ReadInt32(),
                                    Prefix = reader.ReadByte(),
                                    Index = i,
                                    ItemNick = nick + " Dye)"
                                };
                            }
                            for (int i = 0; i < Bank.Length; ++i)
                            {
                                Bank[i] = new Item
                                {
                                    ItemID = reader.ReadInt32(),
                                    Stack = reader.ReadInt32(),
                                    Prefix = reader.ReadByte(),
                                    Index = i
                                };
                            }
                            for (int i = 0; i < Bank2.Length; ++i)
                            {
                                Bank2[i] = new Item
                                {
                                    ItemID = reader.ReadInt32(),
                                    Stack = reader.ReadInt32(),
                                    Prefix = reader.ReadByte(),
                                    Index = i
                                };
                            }
                            for (int i = 0; i < Bank3.Length; ++i)
                            {
                                Bank3[i] = new Item
                                {
                                    ItemID = reader.ReadInt32(),
                                    Stack = reader.ReadInt32(),
                                    Prefix = reader.ReadByte(),
                                    Index = i
                                };
                            }

                            #endregion
                            #region Buffregion
                            for (int i = 0; i < Buffs.Length; i++)
                            {
                                int id = reader.ReadInt32();
                                int time = reader.ReadInt32();
                                time = time < 0 ? 0 : time;
                                Buffs[i] = new Buff(id, time, i);
                            }

                            #endregion
                            #region Misc
                            for (int m = 0; m < SpawnX.Length; m++)
                            {
                                try
                                {
                                    int spawnX = reader.ReadInt32();
                                    if (spawnX != -1)
                                    {
                                        SpawnX[m] = spawnX;
                                        SpawnY[m] = reader.ReadInt32();
                                        WorldIDs[m] = reader.ReadInt32();
                                        WorldNames[m] = reader.ReadString();
                                    }
                                    else
                                        break;
                                }
                                catch { }
                            }
                            HBLocked = reader.ReadBoolean();
                            for (int i = 0; i < HideInfo.Length; i++)
                            {
                                HideInfo[i] = reader.ReadBoolean();
                            }
                            AnglerQuestsFinished = reader.ReadInt32();

                            for (int i = 0; i < DPadRadialBindings.Length; i++)
                            {
                                DPadRadialBindings[i] = reader.ReadInt32();
                            }

                            for (int i = 0; i < BuilderAccStatus.Length; i++)
                            {
                                BuilderAccStatus[i] = reader.ReadInt32();
                            }
                            BartenderQuestLog = reader.ReadInt32();
                            #endregion
                            reader.Close();
                        }
                    }
                    File.Delete(datFile);
                }
            }
            catch (Exception e)
            {
                ExceptionHandler handler = new ExceptionHandler(e);
                if (handler.canShow)
                    handler.Show();
                flag = true;
            }

            if (flag)
            {
                #region Use backup
                string path2 = plrFile + ".bak";
                if (File.Exists(path2))
                {
#if DEBUG
                    Console.WriteLine("Using backup for some reason");
#endif
                    File.Delete(plrFile);
                    File.Move(path2, plrFile);
                    LoadPlayer(plrFile);
                }
                #endregion
            }

            if (Name == null)
            {
                DialogResult resu = System.Windows.Forms.MessageBox.Show("Something weird happened... name == null!", "Error!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
            else
            {
                OnLoaded(EventArgs.Empty);
                return true;
            }
        }


        public void SavePlayer(string destPath = "")
        {
            string destFileName = destPath == "" ? FilePath + ".bak" : destPath + ".bak";
            if (File.Exists(FilePath))
            {
                try
                {
                    File.Copy(FilePath, destFileName, true);
                }
                catch (IOException)
                {
                    MessageBox.Show("Failed to backup " + Path.GetFileName(FilePath) + " because the file was in use by another application. Try again later.", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string tempPath = destPath == "" ? FilePath + ".dat" : destPath + ".dat";
                using (FileStream stream = new FileStream(tempPath, FileMode.Create))
                {
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        #region Saving char info
                        writer.Write(Constants.TERRARIA_RELEASE);

                        // Metadata stuff
                        writer.Write(MagicNumber);
                        writer.Write(Revision);
                        writer.Write((ulong)Convert.ToUInt64(IsFavorite));

                        writer.Write(Name);
                        writer.Write(Difficulty);
                        writer.Write(PlayTime.Ticks);

                        writer.Write(Hair);
                        writer.Write(HairDye);

                        /// TODO:
                        ///     Actually write appropriate bytes
                        writer.Write(HideBytes);

                        writer.Write(SkinVariant);
                        writer.Write(HP);
                        writer.Write(MaxHP);
                        writer.Write(MP);
                        writer.Write(MaxMP);

                        writer.Write(ExtraAccessory);
                        writer.Write(DownedDD2EventAnyDifficulty);
                        writer.Write(TaxMoney);

                        writer.Write(HairColor.R);
                        writer.Write(HairColor.G);
                        writer.Write(HairColor.B);
                        writer.Write(SkinColor.R);
                        writer.Write(SkinColor.G);
                        writer.Write(SkinColor.B);
                        writer.Write(EyeColor.R);
                        writer.Write(EyeColor.G);
                        writer.Write(EyeColor.B);
                        writer.Write(ShirtColor.R);
                        writer.Write(ShirtColor.G);
                        writer.Write(ShirtColor.B);
                        writer.Write(UnderShirtColor.R);
                        writer.Write(UnderShirtColor.G);
                        writer.Write(UnderShirtColor.B);
                        writer.Write(PantsColor.R);
                        writer.Write(PantsColor.G);
                        writer.Write(PantsColor.B);
                        writer.Write(ShoeColor.R);
                        writer.Write(ShoeColor.G);
                        writer.Write(ShoeColor.B);
                        #endregion
                        #region Save inventory
                        for (int i = 0; i < 20; i++)
                        {
                            if (Armor[i].ItemID == 0)
                                Armor[i].Prefix = 0;
                            writer.Write(Armor[i].ItemID);
                            writer.Write(Armor[i].Prefix);
                        }

                        for (int i = 0; i < 10; i++)
                        {
                            if (Dye[i].ItemID == 0)
                                Dye[i].Prefix = 0;
                            writer.Write(Dye[i].ItemID);
                            writer.Write(Dye[i].Prefix);
                        }
                        for (int j = 0; j < 50; j++)
                        {
                            if (Inventory[j].ItemID == 0)
                            {
                                Inventory[j].Stack = 0;
                                Inventory[j].Prefix = 0;
                                Inventory[j].IsFavorite = false;
                            }
                            writer.Write(Inventory[j].ItemID);
                            writer.Write(Inventory[j].Stack);
                            writer.Write(Inventory[j].Prefix);
                            writer.Write(Inventory[j].IsFavorite);
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            if (Purse[i].ItemID == 0)
                            {
                                Purse[i].Stack = 0;
                                Purse[i].Prefix = 0;
                                Purse[i].IsFavorite = false;
                            }
                            writer.Write(Purse[i].ItemID);
                            writer.Write(Purse[i].Stack);
                            writer.Write(Purse[i].Prefix);
                            writer.Write(Purse[i].IsFavorite);
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            if (Ammo[i].ItemID == 0)
                            {
                                Ammo[i].Stack = 0;
                                Ammo[i].Prefix = 0;
                                Ammo[i].IsFavorite = false;
                            }
                            writer.Write(Ammo[i].ItemID);
                            writer.Write(Ammo[i].Stack);
                            writer.Write(Ammo[i].Prefix);
                            writer.Write(Ammo[i].IsFavorite);
                        }
                        for (int i = 0; i < 5; i++)
                        {
                            if (MiscEquip[i].ItemID == 0)
                                MiscEquip[i].Prefix = 0;
                            if (MiscDye[i].ItemID == 0)
                                MiscDye[i].Prefix = 0;
                            writer.Write(MiscEquip[i].ItemID);
                            writer.Write(MiscEquip[i].Prefix);
                            writer.Write(MiscDye[i].ItemID);
                            writer.Write(MiscDye[i].Prefix);
                        }
                        for (int k = 0; k < 40; k++)
                        {
                            if (Bank[k].ItemID == 0)
                            {
                                Bank[k].Stack = 0;
                                Bank[k].Prefix = 0;
                            }
                            writer.Write(Bank[k].ItemID);
                            writer.Write(Bank[k].Stack);
                            writer.Write(Bank[k].Prefix);
                        }
                        for (int k = 0; k < 40; k++)
                        {
                            if (Bank2[k].ItemID == 0)
                            {
                                Bank2[k].Stack = 0;
                                Bank2[k].Prefix = 0;
                            }
                            writer.Write(Bank2[k].ItemID);
                            writer.Write(Bank2[k].Stack);
                            writer.Write(Bank2[k].Prefix);
                        }
                        for (int k = 0; k < 40; k++)
                        {
                            if (Bank3[k].ItemID == 0)
                            {
                                Bank3[k].Stack = 0;
                                Bank3[k].Prefix = 0;
                            }
                            writer.Write(Bank3[k].ItemID);
                            writer.Write(Bank3[k].Stack);
                            writer.Write(Bank3[k].Prefix);
                        }
                        for (int i = 0; i < 22; i++)
                        {
                            if (Buffs[i] == null)
                            {
                                Buffs[i] = new Buff(0, 0, i);
                            }
                            writer.Write(Buffs[i].BuffID);
                            writer.Write(Buffs[i].BuffTime);
                        }
                        for (int m = 0; m < 200; m++)
                        {
                            if (WorldNames[m] == null)
                            {
                                writer.Write(-1);
                                break;
                            }
                            writer.Write(SpawnX[m]);
                            writer.Write(SpawnY[m]);
                            writer.Write(WorldIDs[m]);
                            writer.Write(WorldNames[m]);
                        }
                        writer.Write(HBLocked);
                        for (int i = 0; i < 13; i++)
                        {
                            writer.Write(HideInfo[i]);
                        }
                        writer.Write(AnglerQuestsFinished);

                        for (int i = 0; i < DPadRadialBindings.Length; i++)
                        {
                            writer.Write(DPadRadialBindings[i]);
                        }
                        for (int i = 0; i < BuilderAccStatus.Length; i++)
                        {
                            writer.Write(BuilderAccStatus[i]);
                        }
                        writer.Write(BartenderQuestLog);
                        #endregion
                        writer.Close();
                    }
                }
                try
                {
                    EncryptFile(tempPath, destPath == "" ? FilePath : destPath);
                }
                catch (IOException)
                {
                    File.Delete(tempPath);
                    File.Delete(FilePath);
                    if (File.Exists(destFileName))
                        File.Copy(destFileName, FilePath);
                    MessageBox.Show("Failed to save " + Path.GetFileName(FilePath) + " because the file was in use by another application. Try again later.", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                File.Delete(tempPath);
                OnSaved(EventArgs.Empty);
            }
        }
    }
}