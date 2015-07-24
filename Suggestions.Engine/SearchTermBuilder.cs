using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suggestions.Engine
{
    public class SearchTermBuilder : Suggestions.Engine.ISearchTermBuilder
    {
        public SearchTermBuilder ()
    {
        alfaBetaHebrew = new string[] { "א", "ב", "ג", "ד", "ה", "ו", "ז", "ח", "ט", "י", "כ", "ל", "מ", "נ", "ס", "ע", "פ", "צ", "ק", "ר", "ש", "ת" };
        alfaBetaEnglish = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "g", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
    }
        public string[] alfaBetaHebrew;
        public string[] alfaBetaEnglish;
        private ICollection<string> buildSearchTermsFromSeed(string seed,string[] alfaBeta)
        {
            var ret = new List<string>();
            foreach (var item in alfaBeta)
            {
                foreach (var item2 in alfaBeta)
                {
                    ret.Add(seed + " " + item + item2);
                    
                }
            }
            return ret;
        }
        public ICollection<string> buildSearchTermsFromSeedHebrew(string seed)
        {
            return buildSearchTermsFromSeed(seed, alfaBetaHebrew);
        }

        public ICollection<string> buildSearchTermsFromSeedEnglish(string seed)
        {
            return buildSearchTermsFromSeed(seed, alfaBetaEnglish);
        }
        
    }
}
