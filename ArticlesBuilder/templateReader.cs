using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticlesBuilder
{
    public class TemplateReader : ArticlesBuilder.ITemplateReader
    {
        public string FileName { get; set; }
        public string[][] RandArtical { get; set; }
        public int MyProperty { get; set; }
        public TemplateReader(string filePath)
        {
            FileName = filePath;
            RandArtical = buildArticleArray(ReadFile());
        }

        private string[] ReadFile()
        {
            var lines = System.IO.File.ReadAllLines(FileName);
            return lines;
        }

        private string[][] buildArticleArray(string[] lines)
        {
            var ret = new List<string[]>();
            foreach (var item in lines)
            {
                ret.Add(item.Split('|').ToArray());
            }
            //var line = lines.Select(x => x.Split('|'));
            return ret.ToArray();
        }

        private string[] buildRandomArticalArray( )
        {            
            var rand = new Random();
            var article = RandArtical.Select(x => x[rand.Next(x.Length - 1)]);
            return article.ToArray();

        }
        public string GetArtical(string term)
        {

            var artical = buildRandomArticalArray();
            var result = new StringBuilder();
            var ret = artical.Select(x => result.Append(String.Format(x, term))).ToArray()[0];
            return ret.ToString();
        }
    }
}
