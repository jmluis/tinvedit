using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using TerrariaInvEdit.Tools;

namespace TerrariaInvEdit.UI.Forms
{

    public partial class ExceptionHandler : Form
    {
        public const string MAIL_HOST = "smtp.gmail.com";
        public const int MAIL_PORT = 587;
        public const string MAIL_CLIENT_EMAIL = "error@chbshoot.me";
        public const string MAIL_CLIENT_PASS = "lf/|]K&b#lY))IF(<x76|\"VU=}J62C";

        SmtpClient client;
        MailAddress from;
        MailAddress to;
        MailMessage msg;

        Exception curException;
        static bool sent = false;

        public bool canShow = true;

        public ExceptionHandler(Exception theException, string message = null)
        {
            Version curVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            if (TerrariaInvEdit.Tools.Update.Instance != null)
            {
                TerrariaInvEdit.Tools.Update update = TerrariaInvEdit.Tools.Update.Instance;
                bool found = curVersion.CompareTo(new Version(update.Version)) < 0 ? true : false;
                if (found)
                {
                    MessageBox.Show("Please update to the latest " + Application.ProductName + " before you send an error report.", "Update!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    canShow = false;
                    return;
                }
            }

            InitializeComponent();

            if (Program.MainF.PPlayer != null)
            {
                checkBox1.Enabled = Program.MainF.PPlayer.Loaded;
            }
            checkBox1.Checked = checkBox1.Enabled;

            curException = theException;
            label2.Text = message ?? theException.Message;

            client = new SmtpClient(MAIL_HOST, MAIL_PORT);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(MAIL_CLIENT_EMAIL, MAIL_CLIENT_PASS);
            client.EnableSsl = true;

#if DEBUG
            rtbError.Text = curException.ToString();
#endif
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            to = new MailAddress(MAIL_CLIENT_EMAIL);
            from = new MailAddress(MAIL_CLIENT_EMAIL, Environment.UserName);
            msg = new MailMessage(from, to);
            

            msg.Subject = curException.GetType().ToString();
            msg.Body += Application.ProductName + " " + Application.ProductVersion;
            msg.Body += Environment.NewLine;
            msg.Body += "CommandLine: " + Environment.CommandLine;
            msg.Body += Environment.NewLine;
            try
            {
                msg.Body += "OSVersion: " + OperatingSystemVersion.Current.ToString();
            }
            catch (Exception ex)
            {
                msg.Body += "OSVersion: SOMETHING FISHY: " + ex.Message;
            }
            msg.Body += Environment.NewLine;
            msg.Body += "Current culture: " + System.Globalization.CultureInfo.CurrentCulture.Name;
            msg.Body += Environment.NewLine;
            msg.Body += "Current UI culture: " + System.Globalization.CultureInfo.CurrentUICulture.Name;
            msg.Body += Environment.NewLine;
            msg.Body += "CPU Count: " + Environment.ProcessorCount;
            msg.Body += Environment.NewLine;

            msg.Body += "Player version: " + ((Program.MainF.PPlayer == null) ? "(null)" : Program.MainF.PPlayer.TerrariaVersion.ToString());
            msg.Body += Environment.NewLine;
            msg.Body += "Last Path: " + Program.MainF.LastPath;
            msg.Body += Environment.NewLine;

            msg.Body += "Time: " + DateTime.Now.ToString();
            msg.Body += Environment.NewLine;

            msg.Body += "Start time: " + Process.GetCurrentProcess().StartTime.ToString();
            msg.Body += Environment.NewLine;

            msg.Body += "Run time: " + (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString();
            msg.Body += Environment.NewLine;

            msg.Body += "From: " + (textBox1.Text == "" ? "(null)" : textBox1.Text);
            msg.Body += Environment.NewLine;


            msg.Body += Environment.NewLine;


            msg.Body += "Message:";
            msg.Body += Environment.NewLine;


            msg.Body += rtbError.Text == "" ? "(no message)" : rtbError.Text;
            msg.Body += Environment.NewLine;


            msg.Body += Environment.NewLine;


            msg.Body += "Exception:";

            msg.Body += Environment.NewLine;

            msg.Body += curException.ToString();

            msg.Body += Environment.NewLine;

            msg.Body += Environment.NewLine;


            if (checkBox1.Checked)
                msg.Attachments.Add(new Attachment(Program.MainF.PPlayer.FilePath));

            try
            {
                if (File.Exists(Program.MainF.LastPath) && Program.MainF.PPlayer.FilePath != Program.MainF.LastPath)
                    msg.Attachments.Add(new Attachment(Program.MainF.LastPath));
            }
            catch { }

            msg.SubjectEncoding = Encoding.UTF8;

            client.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);
            client.SendAsync(msg, "");

            btnNo.Visible = false;
            btnYes.Visible = false;
            label1.Text = "Sending... you may cancel at any time";
            progressBar1.Visible = true;
            btnCancel.Visible = true;
        }

        void client_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            string token = (string)e.UserState;

            if (e.Cancelled)
            {
                MessageBox.Show("Thank you for your patience and time", "Cancelled");
                this.Close();
                return;
            }

            if (e.Error != null)
            {
                MessageBox.Show("Messaged failed to send: " + e.Error.Message, "Failed!");
            }
            sent = true;
            msg.Dispose();
            this.Close();
        }

        void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!sent)
                client.SendAsyncCancel();
        }
    }
}
