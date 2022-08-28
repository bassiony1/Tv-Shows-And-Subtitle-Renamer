using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace SubRenamer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblName.Text = frmMain.TVShow;


        }

        public static List<string> Paths = new List<string>();
        public static string dir;
        string []allowedExtensions = new[] { ".srt", ".ass" , ".mkv" , ".mp4" };
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (dlgBrowser.ShowDialog() == DialogResult.OK)
            {
                btnOk.Enabled = true;
                Paths = Directory.GetFiles(dlgBrowser.SelectedPath).Where(file => allowedExtensions.Any(file.ToLower().EndsWith)).ToList();
                dir = dlgBrowser.SelectedPath;
                lblPlace.Text = $"({dir})";
            }
        }

        frmChanger frmChange = new frmChanger();
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmChange.ShowDialog();
            this.Visible = true; 
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
