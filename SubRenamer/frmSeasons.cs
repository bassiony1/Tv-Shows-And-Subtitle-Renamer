using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubRenamer
{
    public partial class frmSeasons : Form
    {
        public frmSeasons()
        {
            InitializeComponent();
        }
        public static int ChosenSeason;
        private void frmSeasons_Load(object sender, EventArgs e)
        {
            lblSearch.Text = frmMain.TVShow;
            for (int i = 1; i <= frmSearch.NumberOfSeasons; i++)
            {
                cmbSeason.Items.Add(i.ToString("D2"));
            }
            cmbSeason.SelectedIndex = 0;
        }
        Form1 f = new Form1(); 
        private void btnNext_Click(object sender, EventArgs e)
        {
            ChosenSeason = cmbSeason.SelectedIndex;
            this.Hide();
            f.ShowDialog();
            this.Visible = true;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
    }
}
