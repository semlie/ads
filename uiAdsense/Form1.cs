using ArticlesBuilder;
using Google.Apis.YouTube.Samples;
using googleSherchApi;
using Suggestions;
using Suggestions.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wordpress;

namespace uiAdsense
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var googleSerch = new GoogleSearchParser();
            var youTube = new SearchYoutube();
            var searchTermBuilder = new SearchTermBuilder();
            var googleSuggest = new GoogleSuggest();


            var TemplateReader = new TemplateReader(textBoxContentFile.Text);
            var SnipetReader = new TemplateReader(textBoxSnipetFile.Text);
            var TitlesReader = new TemplateReader(textBoxTitleFile.Text);
            var CraditReader = new TemplateReader(textBoxCredit.Text);


            var postBuilder = new PostBuilder(textBoxURL.Text, textBoxUserName.Text, textBoxPassword.Text);


            var program = new ProgramFlow(textBoxSubject.Text, textBoxURL.Text, CraditReader,
                googleSerch, searchTermBuilder, SnipetReader, TitlesReader, googleSuggest,
                TemplateReader, postBuilder, youTube);
            program.run();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxTitleFile.Text = openFileDialog1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxContentFile.Text = openFileDialog1.FileName;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxSnipetFile.Text = openFileDialog1.FileName;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBoxSnipetFile_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxCredit.Text = openFileDialog1.FileName;
            }
        }
    }
}
