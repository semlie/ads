using System;
namespace Wordpress
{
    public interface IPostBuilder
    {
        int addPage(WordPressSharp.Models.Post post);
        int addPost(WordPressSharp.Models.Post post);
        WordPressSharp.Models.Post buildPostData(string title, string content);
    }
}
