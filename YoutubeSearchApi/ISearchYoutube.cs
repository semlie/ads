using System;
namespace YoutubeSearchApi
{
    public interface ISearchYoutube
    {
        global::BE.YouTubeEntity GetFromYouTube(string qu);
    }
}
