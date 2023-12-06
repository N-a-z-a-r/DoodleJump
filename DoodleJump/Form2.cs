using DoodleJump.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoodleJump
{
    public partial class Form2 : Form
    {
        private PictureBox pic;

        public Form2()
        {
            InitializeComponent();

            this.BackgroundImage = Properties.Resources.bak;
            this.Height = 880;
            this.Width = 409;

            pic = new PictureBox();
            pic.Image = Properties.Resources.play;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;

            pic.Width = 150;
            pic.Height = 67;

            pic.Left = (this.ClientSize.Width - pic.Width) / 2;
            pic.Top = (this.ClientSize.Height - pic.Height) / 2;

            pic.Click += Pic_Click;
 
            this.Controls.Add(pic);
        }

        private void Pic_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }
    }
}
