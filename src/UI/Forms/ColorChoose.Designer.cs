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

using System.ComponentModel;
using System.Windows.Forms;

namespace TerrariaInvEdit.UI.Forms
{
    partial class ColorChoose
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorChoose));
            this.label1 = new System.Windows.Forms.Label();
            this.rTrack = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bTrack = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.gTrack = new System.Windows.Forms.TrackBar();
            this.rText = new System.Windows.Forms.TextBox();
            this.gText = new System.Windows.Forms.TextBox();
            this.bText = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.resultBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.rTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Result:";
            // 
            // rTrack
            // 
            this.rTrack.AutoSize = false;
            this.rTrack.Cursor = System.Windows.Forms.Cursors.Default;
            this.rTrack.Location = new System.Drawing.Point(48, 25);
            this.rTrack.Maximum = 255;
            this.rTrack.Name = "rTrack";
            this.rTrack.Size = new System.Drawing.Size(104, 26);
            this.rTrack.TabIndex = 0;
            this.rTrack.TickStyle = System.Windows.Forms.TickStyle.None;
            this.rTrack.ValueChanged += new System.EventHandler(this.rTrack_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Red:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Green:";
            // 
            // bTrack
            // 
            this.bTrack.AutoSize = false;
            this.bTrack.Cursor = System.Windows.Forms.Cursors.Default;
            this.bTrack.Location = new System.Drawing.Point(48, 91);
            this.bTrack.Maximum = 255;
            this.bTrack.Name = "bTrack";
            this.bTrack.Size = new System.Drawing.Size(104, 26);
            this.bTrack.TabIndex = 0;
            this.bTrack.TickStyle = System.Windows.Forms.TickStyle.None;
            this.bTrack.ValueChanged += new System.EventHandler(this.bTrack_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Blue:";
            // 
            // gTrack
            // 
            this.gTrack.AutoSize = false;
            this.gTrack.Cursor = System.Windows.Forms.Cursors.Default;
            this.gTrack.Location = new System.Drawing.Point(47, 57);
            this.gTrack.Maximum = 255;
            this.gTrack.Name = "gTrack";
            this.gTrack.Size = new System.Drawing.Size(104, 26);
            this.gTrack.TabIndex = 0;
            this.gTrack.TickStyle = System.Windows.Forms.TickStyle.None;
            this.gTrack.ValueChanged += new System.EventHandler(this.gTrack_ValueChanged);
            // 
            // rText
            // 
            this.rText.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.rText.Location = new System.Drawing.Point(157, 22);
            this.rText.MaxLength = 3;
            this.rText.Name = "rText";
            this.rText.Size = new System.Drawing.Size(35, 20);
            this.rText.TabIndex = 0;
            this.rText.TextChanged += new System.EventHandler(this.rText_TextChanged);
            // 
            // gText
            // 
            this.gText.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gText.Location = new System.Drawing.Point(157, 54);
            this.gText.MaxLength = 3;
            this.gText.Name = "gText";
            this.gText.Size = new System.Drawing.Size(35, 20);
            this.gText.TabIndex = 1;
            this.gText.TextChanged += new System.EventHandler(this.gText_TextChanged);
            // 
            // bText
            // 
            this.bText.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bText.Location = new System.Drawing.Point(158, 88);
            this.bText.MaxLength = 3;
            this.bText.Name = "bText";
            this.bText.Size = new System.Drawing.Size(34, 20);
            this.bText.TabIndex = 2;
            this.bText.TextChanged += new System.EventHandler(this.bText_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSave.Location = new System.Drawing.Point(6, 213);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(197, 22);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // resultBox
            // 
            this.resultBox.Location = new System.Drawing.Point(69, 136);
            this.resultBox.Name = "resultBox";
            this.resultBox.Size = new System.Drawing.Size(75, 68);
            this.resultBox.TabIndex = 0;
            this.resultBox.TabStop = false;
            // 
            // ColorChoose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(206, 239);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.bText);
            this.Controls.Add(this.gText);
            this.Controls.Add(this.rText);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.gTrack);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.bTrack);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rTrack);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.resultBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ColorChoose";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ColorChoose";
            ((System.ComponentModel.ISupportInitialize)(this.rTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox resultBox;
        private Label label1;
        private TrackBar rTrack;
        private Label label2;
        private Label label3;
        private TrackBar bTrack;
        private Label label4;
        private TrackBar gTrack;
        private TextBox rText;
        private TextBox gText;
        private TextBox bText;
        private Button btnSave;
    }
}