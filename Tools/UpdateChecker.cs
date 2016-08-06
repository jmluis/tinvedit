using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using TerrariaInvEdit.UI.Forms;

namespace TerrariaInvEdit.Tools
{
    public class UpdateChecker
    {
        public const string CheckSite = @"http://u.chbshoot.me/c/0";
        public static string desc;
        public static string link;

        public static bool DoUpdate()
        {
            Version curVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            bool found = false;

            try
            {
                XmlReader reader = XmlReader.Create(CheckSite);
                XmlSerializer serializer = new XmlSerializer(typeof(Update));
                found = serializer.CanDeserialize(reader);
                if (found)
                {
                    Update.Instance = (Update)serializer.Deserialize(reader);
                    found = curVersion.CompareTo(new Version(Update.Instance.Version)) < 0;
                }
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Couldn't check for updates!", "Oh noes!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                found = false;
            }
            return found;
        }

        public static void CheckForUpdates(bool showNoUpdates)
        {
            if (DoUpdate())
            {
                UpdateForm frm = new UpdateForm(Update.Instance);
                frm.ShowDialog();
            }
            else
            {
                if (showNoUpdates)
                {
                    System.Windows.Forms.MessageBox.Show("No updates found.", "No updates!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
            }
        }
    }

    public class Update
    {
        public static Update Instance;

        public string Version;
        public string ShortVersion;
        public string UpdateURL;
        public string[] Description = new string[0];
    }
}
