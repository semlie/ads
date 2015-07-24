using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using BE;


namespace Google.Apis.YouTube.Samples
{
  /// <summary>
  /// YouTube Data API v3 sample: search by keyword.
  /// Relies on the Google APIs Client Library for .NET, v1.7.0 or higher.
  /// See https://code.google.com/p/google-api-dotnet-client/wiki/GettingStarted
  ///
  /// Set ApiKey to the API key value from the APIs & auth > Registered apps tab of
  ///   https://cloud.google.com/console
  /// Please ensure that you have enabled the YouTube Data API for your project.
  /// </summary>
  public class SearchYoutube : YoutubeSearchApi.ISearchYoutube
  {
    [STAThread]
    static void Main(string[] args)
    {
      Console.WriteLine("YouTube Data API: SearchYoutube");
      Console.WriteLine("========================");

      try
      {
       // new Search().Run().Wait();
      }
      catch (AggregateException ex)
      {
        foreach (var e in ex.InnerExceptions)
        {
          Console.WriteLine("Error: " + e.Message);
        }
      }

      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    public BE.YouTubeEntity GetFromYouTube(string qu)
    {
      var youtubeService = new YouTubeService(new BaseClientService.Initializer()
      {
          ApiKey = "AIzaSyDwPz82k6gi4vid9BvvZ_PhI6MXqK3amOs",
        ApplicationName = this.GetType().ToString()
      });
       
      var searchListRequest = youtubeService.Search.List("snippet");
      searchListRequest.Q = qu; // Replace with your search term.
      searchListRequest.MaxResults = 50;

      // Call the search.list method to retrieve results matching the specified query term.
      var searchListResponse = searchListRequest.Execute();

      List<YouTubeReasults> videos = new List<YouTubeReasults>();
      List<YouTubeReasults> channels = new List<YouTubeReasults>();
      List<YouTubeReasults> playlists = new List<YouTubeReasults>();

      // Add each result to the appropriate list, and then display the lists of
      // matching videos, channels, and playlists.
      foreach (var searchResult in searchListResponse.Items)
      {
        switch (searchResult.Id.Kind)
        {
          case "youtube#video":
            videos.Add(new YouTubeReasults{
                Title = searchResult.Snippet.Title,
                Description =searchResult.Snippet.Description,
                Id = searchResult.Id.VideoId,
                Type = "video"
            });
            break;

          case "youtube#channel":
            channels.Add(new YouTubeReasults{
                Title = searchResult.Snippet.Title,
                Description =searchResult.Snippet.Description,
                Id = searchResult.Id.ChannelId,
                Type = "channel"

            }                
                );
            break;

          case "youtube#playlist":
            playlists.Add(new YouTubeReasults{
                Title = searchResult.Snippet.Title,
                Description =searchResult.Snippet.Description,
                Id = searchResult.Id.PlaylistId,
                Type = "playlist"
            });
            break;
        }
      }

      Console.WriteLine(String.Format("Videos:\n{0}\n", string.Join("\n", videos)));
      Console.WriteLine(String.Format("Channels:\n{0}\n", string.Join("\n", channels)));
      Console.WriteLine(String.Format("Playlists:\n{0}\n", string.Join("\n", playlists)));
      var ret = new BE.YouTubeEntity()
      {
          Channels = channels,
          Playlists = playlists,
          Videos = videos,
          Query = qu
      };
      return ret;
    }

  }
}