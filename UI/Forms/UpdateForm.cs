using System;
using System.Windows.Forms;
using TerrariaInvEdit.Tools;

namespace TerrariaInvEdit.UI.Forms
{
    public partial class UpdateForm : Form
    {
        Update update;
        public UpdateForm(Update _update)
        {
            InitializeComponent();
            this.update = _update;
        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {
            rtbUpdate.SelectionBullet = true;
            foreach (string line in update.Description)
            {
                if (!string.IsNullOrWhiteSpace(line))
                    rtbUpdate.SelectedText = line + Environment.NewLine;

            }
            rtbUpdate.SelectionBullet = false;

            rtbUpdate.AppendText(Environment.NewLine);
            rtbUpdate.AppendText("-Shoot <3");

            Text += " " + update.ShortVersion;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(update.UpdateURL);
            System.Windows.Forms.Application.Exit();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
