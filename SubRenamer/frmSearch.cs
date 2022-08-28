using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubRenamer
{
    public partial class frmSearch : Form
    {
        public frmSearch()
        {
            InitializeComponent();
        }

        private void frmSearch_Load(object sender, EventArgs e)
        {
            lblSearch.Text = frmMain.TVShow;
            foreach (var (show , i) in frmMain.SearchResult.Select((show , i)=>(show , i)))
            {
                cmbSearchSelection.Items.Add($"{(i+1).ToString("D2")}- {show}");
            }
            cmbSearchSelection.SelectedIndex = 0;
        }
        public static string ShowID ;

        frmSeasons f = new frmSeasons();
        HttpClient client = new HttpClient();
        public static int NumberOfSeasons;
        private void btnNext_Click(object sender, EventArgs e)
        {
            var  SelectedShowLink = frmMain.SearchLinks[cmbSearchSelection.SelectedIndex];
            ShowID = SelectedShowLink.Split("/")[2];
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            var response = client.GetStringAsync($"https://www.imdb.com/title/{ShowID}/episodes?ref_=tt_eps_sm");
            htmlDoc.LoadHtml(response.Result);
            NumberOfSeasons = htmlDoc.DocumentNode.SelectNodes(".//*[@id='bySeason']//option").Count;
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
