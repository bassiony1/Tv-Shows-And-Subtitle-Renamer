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
using HtmlAgilityPack;
using System.Net.Http.Headers;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace SubRenamer
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        public static string TVShow;
        public static string SeriesSeason;
        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        frmSearch f = new frmSearch();
        HttpClient client = new HttpClient();


        public static List<string> SearchResult = new List<string>();
        public static List<string> SearchLinks = new List<string>();
        private void btnNext_Click(object sender, EventArgs e)
        {
            var sName = Regex.Replace(txtName.Text.Trim(), @"\s+", " ");
            var SeriesName = string.Join("+", sName.ToLower().Split(" ").Select((string s) => {
                s = ((s.Length > 1) ? char.ToUpper(s[0]) + s.Substring(1) : char.ToUpper(s[0]).ToString());
                return s;
            }));
            TVShow = SeriesName.Replace("+", " ");
            var url = $"https://www.imdb.com/find?q={SeriesName}&ref_=nv_sr_sm";
            var res = client.GetStringAsync(url);

            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(res.Result);
            var shows_search = htmlDoc.DocumentNode.SelectNodes(".//*[contains(concat(' ',normalize-space(@class),' '),' result_text ')]/a");
            var shows_names_search = htmlDoc.DocumentNode.SelectNodes(".//*[contains(concat(' ',normalize-space(@class),' '),' result_text ')]");
            foreach (var show in shows_names_search)
            {
                SearchResult.Add(show.InnerText);
            }
            foreach (var show in shows_search)
            {
                SearchLinks.Add(show.GetAttributeValue("href", null));
            }
            this.Hide();
            f.ShowDialog();
            this.Visible = true;

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (txtName.Text != "")
                btnNext.Enabled=true;
            else
                btnNext.Enabled = false;
        }

    }
}
