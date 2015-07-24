using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using googleSherchApi;
using Suggestions.Engine;
using Suggestions;
using ArticlesBuilder;
using Wordpress;
using BE;
using YoutubeSearchApi;


namespace uiAdsense
{
    public class ProgramFlow
    {

        private IGoogleSearchParser GoogleSearchParser;
        private ISearchTermBuilder SearchTermBuilder;
        private IGoogleSuggest GoogleSuggest;
        private ITemplateReader TemplateReader;
        private ITemplateReader SnipetReader;
        private ITemplateReader TitlesReader;
        private ITemplateReader CraditReader;
        private IPostBuilder PostBuilder;
        private ISearchYoutube SearchYoutube;
        private string BaseUrl;
        private string BaseTerm;




        public ProgramFlow(string baseTerm,
                            string baseUrl,
                            ITemplateReader craditReader,
                            IGoogleSearchParser googleSearchParser,
                            ISearchTermBuilder searchTermBuilder,
                            ITemplateReader snipetReader,
                            ITemplateReader titlesReader,
                            IGoogleSuggest googleSuggest,
                            ITemplateReader templateReader,
                            IPostBuilder postBuilder,
                            ISearchYoutube searchYoutube)
        {
            CraditReader = craditReader;
            BaseTerm = baseTerm;
            BaseUrl = baseUrl;
            SearchYoutube = searchYoutube;
            TitlesReader = titlesReader;
            SnipetReader = snipetReader;
            PostBuilder = postBuilder;
            TemplateReader = templateReader;
            SearchTermBuilder = searchTermBuilder;
            GoogleSearchParser = googleSearchParser;
            GoogleSuggest = googleSuggest;

        }
        public void run()
        {

            var candidate = SearchTermBuilder.buildSearchTermsFromSeedHebrew(BaseTerm);
            foreach (var item in candidate)
            {
                runOnTerm(item);
            }

            //var searchResults = GetFromGoogleSherch();
            //var videoResults = GetFromYouTubeSearch();
            //var buildPage = GetPageFromTerm();
            //var buildPostsForTerms = GetPostesForEachTerm();


        }
        private void runOnTerm(string term)
        {
            var suggest = GoogleSuggest.GetSearchSuggestions(term).Answers;
            var pages = suggest.Select(x => BuildPagePerSuggestTerm(x.answer));
            foreach (var page in pages)
            {

                runOnResult(page);


            }
            //  
        }

        private void runOnResult(TermPage term)
        {
            var googleSearchResults = getSearchResultsForTerm(term);
            var googleYouTubeResults = getYouTubeResultsForTerm(term);

            var postForGoogleSearchResults = BuildAllPostForGoogleSearchTerm(googleSearchResults);
            var postForgoogleYouTubeResults = BuildAllPostForYouTubeSearchTerm(googleYouTubeResults);

            var FinelsPost = postForgoogleYouTubeResults.Zip(postForGoogleSearchResults, (y, g) => addStringToPage(y, g));

            foreach (var post in FinelsPost)
            {
                sendToWordpress(post);
            }

        }
        private Page addStringToPage(Page page, string s)
        {
            page.Content += "<br>" + s;
            return page;
        }
        private string buildInLinkUrl(TermPage term)
        {

            return String.Format("<a href =\"{0}\"> {1} </a>", BaseUrl + "" + term.PageId, term.Term);


        }
        private string buildYouTubeIframe(YouTubeReasults term)
        {

            if (term.Type == "playlist")
            {
                return string.Format("<iframe width=\"560\" height=\"315\" src=\"https://www.youtube.com/embed/?list={0}\" frameborder=\"0\" allowfullscreen></iframe>", term.Id);
            }
            if (term.Type == "video")
            {
                return string.Format("<iframe width=\"560\" height=\"315\" src=\"https://www.youtube.com/embed/{0}\" frameborder=\"0\" allowfullscreen></iframe>", term.Id);

            }
            else
            {
                return default(string);
            }

        }

        private string buildOutLinkUrl(string text, string url)
        {
            return String.Format("<a href =\"{0}\"> {1} </a>", url, text);

        }


        private string[] BuildAllPostForGoogleSearchTerm(TermResultsGoogle termResult)
        {
            return termResult.GoogleResult.Select(
                 x => BuildPostForGoogleSearchTerm(x)).ToArray();

        }

        private string BuildPostForGoogleSearchTerm(ResultsGoogle termResult)
        {


            var cradit = buildOutLinkUrl(CraditReader.GetArtical(termResult.Query), termResult.Link);

            var Title = String.Format("<h3> {0} </h3><br>", termResult.Title);
            var Content = termResult.Description + "<br>" + SnipetReader.GetArtical(termResult.Query);



            return Title + cradit + "<br>" + Content;


        }

        private Page[] BuildAllPostForYouTubeSearchTerm(TermYouTubeEntity termResult)
        {
            var pages = new List<Page>();
            foreach (var item in termResult.YouTubeResult.Videos)
            {
                pages.Add(BuildPostForYouTubeSearchTerm(item, termResult.Term));
            }

            foreach (var item in termResult.YouTubeResult.Playlists)
            {
                pages.Add(BuildPostForYouTubeSearchTerm(item, termResult.Term));
            }

          //  var video = termResult.YouTubeResult.Videos.Select(x => BuildPostForYouTubeSearchTerm(x, termResult.Term));
            
           // return video.Concat(termResult.YouTubeResult.Playlists.Select(x => BuildPostForYouTubeSearchTerm(x, termResult.Term))).ToArray();
            return pages.ToArray();
        }
        private Page BuildPostForYouTubeSearchTerm(YouTubeReasults termResult, TermPage term)
        {

            return new Page
    {
        Title = termResult.Title + " " + term.Term,
        Content = buildYouTubeIframe(termResult) + "<br>" + termResult.Description + "<br>" + SnipetReader.GetArtical(term.Term),
    };

        }

        private TermResultsGoogle getSearchResultsForTerm(TermPage term)
        {
            return new TermResultsGoogle
            {
                GoogleResult = GoogleSearchParser.SearchQuery(term.Term).ToArray<ResultsGoogle>(),
                Term = term
            };
        }

        private TermYouTubeEntity getYouTubeResultsForTerm(TermPage term)
        {
            return new TermYouTubeEntity
            {
                YouTubeResult = SearchYoutube.GetFromYouTube(term.Term),
                Term = term
            };
        }

        private TermPage BuildPagePerSuggestTerm(string suggest)
        {
            var page = new Page
            {
                Content = TemplateReader.GetArtical(suggest),
                Title = TitlesReader.GetArtical(suggest)
            };

            var post = PostBuilder.buildPostData(page.Title, page.Content);
            var wordpressPageId = PostBuilder.addPage(post);

            return new TermPage
            {
                PageId = wordpressPageId,
                Term = suggest,
            };

        }

        private void sendToWordpress(Page page)
        {
            var post = PostBuilder.buildPostData(page.Title, page.Content);
            PostBuilder.addPost(post);
        }
    }
}
