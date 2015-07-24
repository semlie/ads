using System;
namespace Suggestions.Engine
{
   public  interface ISearchTermBuilder
    {
        System.Collections.Generic.ICollection<string> buildSearchTermsFromSeedEnglish(string seed);
        System.Collections.Generic.ICollection<string> buildSearchTermsFromSeedHebrew(string seed);
    }
}
