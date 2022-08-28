using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace SubRenamer
{
   
    public partial class frmChanger : Form
    {
        public frmChanger()
        {
            InitializeComponent();
        }

        HttpClient client = new HttpClient();

         List<string> episodes_names = new List<string>();
        string seriesName;
        Regex regex = new Regex(@"E(?<episode>\d{1,2})");
        Regex regex01 = new Regex(@"(?<episode>\d{1,2})");

        Match match;
        Match match01;
        private void frmChanger_Load(object sender, EventArgs e)
        {
            seriesName = $"{frmMain.TVShow} - S{(frmSeasons.ChosenSeason+1).ToString("D2")}E";
            var response = client.GetStringAsync($"https://www.imdb.com/title/{frmSearch.ShowID}/episodes?season={frmSeasons.ChosenSeason+1}");
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(response.Result);
            var episode_search = htmlDoc.DocumentNode.SelectNodes(".//*[contains(concat(' ',normalize-space(@class),' '),' list_item ')]//strong/a");
            foreach (var ep in episode_search)
            {
                episodes_names.Add(ep.InnerText.Replace(":", " ").Replace("/", " ").Replace("\\", " ").Replace("<", " ").Replace(">",  " ").Replace("?", " ").Replace("*", " ").Replace("|", " ").Replace("\"", " "));
            }

            int k;
            foreach (var path  in Form1.Paths )
            {
                if (Path.GetExtension(path) == ".srt" || Path.GetExtension(path) == ".ass")
                {
                    var filename = Path.GetFileName(path);
                    match = regex.Match(filename);
                    match01 = regex01.Match(filename);
                    if (match.Success)
                    {
                        k = int.Parse(match.Groups["episode"].Value);
                        txtNames.AppendText(Path.GetFileName(path) + "    ----->  ");
                        if (Path.GetExtension(path) == ".srt")
                            txtNames.AppendText($"{seriesName}{(k).ToString("D2")} - {episodes_names[k - 1 % (episodes_names.Count)]}.srt\n");
                        else if (Path.GetExtension(path) == ".ass")
                            txtNames.AppendText($"{seriesName}{(k).ToString("D2")} - {episodes_names[k - 1 % (episodes_names.Count)]}.ass\n");

                    }
                    else if (match01.Success)
                    {
                        k = int.Parse(match01.Groups["episode"].Value);
                    txtNames.AppendText(Path.GetFileName(path)+"    ----->  ");
                    if(Path.GetExtension(path) == ".srt")
                        txtNames.AppendText($"{seriesName}{(k).ToString("D2")} - {episodes_names[k-1 % (episodes_names.Count)]}.srt\n");
                    else if (Path.GetExtension(path) == ".ass")
                        txtNames.AppendText($"{seriesName}{(k ).ToString("D2")} - {episodes_names[k-1%(episodes_names.Count)]}.ass\n");
                    }

                }
            }
            
            txtNames.AppendText("\n\n\n");
            foreach (var path in Form1.Paths)
            {
                if (Path.GetExtension(path) == ".mkv" || Path.GetExtension(path) == ".mp4")
                {
                    var filename = Path.GetFileName(path);
                    match = regex.Match(filename);
                    match01 = regex01.Match(filename);
                    if (match.Success)
                    {
                        k = int.Parse(match.Groups["episode"].Value);
                        txtNames.AppendText(Path.GetFileName(path) + "    ----->  ");
                        if (Path.GetExtension(path) == ".mkv")
                            txtNames.AppendText($"{seriesName}{(k).ToString("D2")} - {episodes_names[k - 1 % (episodes_names.Count)]}.mkv\n");
                        else if (Path.GetExtension(path) == ".mp4")
                            txtNames.AppendText($"{seriesName}{(k).ToString("D2")} - {episodes_names[k - 1 % (episodes_names.Count)]}.mp4\n");
					}
					else if (match01.Success)
					{
                        k = int.Parse(match01.Groups["episode"].Value);
                        txtNames.AppendText(Path.GetFileName(path) + "    ----->  ");
                        if (Path.GetExtension(path) == ".mkv")
                            txtNames.AppendText($"{seriesName}{(k).ToString("D2")} - {episodes_names[k - 1 % (episodes_names.Count)]}.mkv\n");
                        else if (Path.GetExtension(path) == ".mp4")
                            txtNames.AppendText($"{seriesName}{(k).ToString("D2")} - {episodes_names[k - 1 % (episodes_names.Count)]}.mp4\n");
                    }
                    

                }
            }
        }

        private void frmChanger_FormClosing(object sender, FormClosingEventArgs e)
        {
            txtNames.Clear();
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are You Sure You Want To Rename ?",
                "Warning", MessageBoxButtons.YesNo , MessageBoxIcon.Warning , MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                int i = 0;
                foreach (var path in Form1.Paths)
                {
                        if (Path.GetExtension(path) == ".srt" || Path.GetExtension(path) == ".ass")
                        {
                        var filename = Path.GetFileName(path);
                        match = regex.Match(filename);
                        match01 = regex01.Match(filename);
                        if (match.Success)
                        {
                            i = int.Parse(match.Groups["episode"].Value);
                            if (Path.GetExtension(path) == ".srt")
                            {
                                if (File.Exists($@"{Form1.dir}\{seriesName}{(i).ToString("D2")} - {episodes_names[i-1 % (episodes_names.Count)]}.srt"))
                                    continue;
                                    File.Move(path, $@"{Form1.dir}\{seriesName}{(i).ToString("D2")} - {episodes_names[i-1 % (episodes_names.Count)]}.srt");

                            }
                            else if (Path.GetExtension(path) == ".ass")
                            {
                                if (File.Exists($@"{Form1.dir}\{seriesName}{(i).ToString("D2")} - {episodes_names[i-1 % (episodes_names.Count)]}.ass"))
                                    continue;
                                File.Move(path, $@"{Form1.dir}\{seriesName}{(i).ToString("D2")} - {episodes_names[i-1 % (episodes_names.Count)]}.ass");
                            }
						}
						else if (match01.Success)
						{
                            i = int.Parse(match01.Groups["episode"].Value);
                            if (Path.GetExtension(path) == ".srt")
                            {
                                if (File.Exists($@"{Form1.dir}\{seriesName}{(i).ToString("D2")} - {episodes_names[i - 1 % (episodes_names.Count)]}.srt"))
                                    continue;
                                File.Move(path, $@"{Form1.dir}\{seriesName}{(i).ToString("D2")} - {episodes_names[i - 1 % (episodes_names.Count)]}.srt");

                            }
                            else if (Path.GetExtension(path) == ".ass")
                            {
                                if (File.Exists($@"{Form1.dir}\{seriesName}{(i).ToString("D2")} - {episodes_names[i - 1 % (episodes_names.Count)]}.ass"))
                                    continue;
                                File.Move(path, $@"{Form1.dir}\{seriesName}{(i).ToString("D2")} - {episodes_names[i - 1 % (episodes_names.Count)]}.ass");
                            }
                        }
						
                    }
                }
                foreach (var path in Form1.Paths)
                {
                    if (Path.GetExtension(path) == ".mp4" || Path.GetExtension(path) == ".mkv")
                    {
                        var filename = Path.GetFileName(path);
                        match = regex.Match(filename);
                        match01 = regex01.Match(filename);
                        if (match.Success)
                        {
                            i = int.Parse(match.Groups["episode"].Value);
                            if (Path.GetExtension(path) == ".mp4")
                            {
                                if (File.Exists($@"{Form1.dir}\{seriesName}{(i).ToString("D2")} - {episodes_names[i-1 % (episodes_names.Count)]}.mp4"))
                                    continue;
                                File.Move(path, $@"{Form1.dir}\{seriesName}{(i).ToString("D2")} - {episodes_names[i-1 % (episodes_names.Count)]}.mp4");
                            }
                            else if (Path.GetExtension(path) == ".mkv")
                                if (File.Exists($@"{Form1.dir}\{seriesName}{(i).ToString("D2")} - {episodes_names[i-1 % (episodes_names.Count)]}.mkv"))
                                    continue;
                            File.Move(path, $@"{Form1.dir}\{seriesName}{(i).ToString("D2")} - {episodes_names[i-1 % (episodes_names.Count)]}.mkv");
						}
						else if (match01.Success)
						{
                            i = int.Parse(match01.Groups["episode"].Value);
                            if (Path.GetExtension(path) == ".mp4")
                            {
                                if (File.Exists($@"{Form1.dir}\{seriesName}{(i).ToString("D2")} - {episodes_names[i - 1 % (episodes_names.Count)]}.mp4"))
                                    continue;
                                File.Move(path, $@"{Form1.dir}\{seriesName}{(i).ToString("D2")} - {episodes_names[i - 1 % (episodes_names.Count)]}.mp4");
                            }
                            else if (Path.GetExtension(path) == ".mkv")
                                if (File.Exists($@"{Form1.dir}\{seriesName}{(i).ToString("D2")} - {episodes_names[i - 1 % (episodes_names.Count)]}.mkv"))
                                    continue;
                            File.Move(path, $@"{Form1.dir}\{seriesName}{(i).ToString("D2")} - {episodes_names[i - 1 % (episodes_names.Count)]}.mkv");
                        }
                    }
                }

            }
            MessageBox.Show("Renaming is Complete." , "Success" ,MessageBoxButtons.OK ,MessageBoxIcon.Information);
        }

       
    }
}
