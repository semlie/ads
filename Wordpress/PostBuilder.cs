using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordPressSharp;
using WordPressSharp.Models;
using System.Configuration;


namespace Wordpress
{
    public class PostBuilder : Wordpress.IPostBuilder
    {
        public WordPressSiteConfig Config { get; set; }
        public PostBuilder()
        {
            Config = WordPressSiteConfigFactory();
        }

        public PostBuilder(string url,string userName,string password)
        {
            Config = new WordPressSiteConfig
            {
                BlogId = 1,
                BaseUrl = url,
                Username = userName,
                Password = password
            };
        }
        public WordPressSiteConfig WordPressSiteConfigFactory() 
        {
            var a = ConfigurationManager.AppSettings["WordPressBaseUrl"];
            return new WordPressSiteConfig
            {
                BaseUrl = ConfigurationManager.AppSettings["WordPressBaseUrl"],
                BlogId = int.Parse(ConfigurationManager.AppSettings["WordPressBlogId"]),
                Username = ConfigurationManager.AppSettings["WordPressUsername"],
                Password = ConfigurationManager.AppSettings["WordPressPassword"],
            };
        
        }
        public int addPost(Post post)
        {
            post.PostType = "post";
            return sendToWP(post);

        }
        public int addPage(Post post)
        {
            post.PostType = "page";
            return sendToWP(post);

        }

        private int sendToWP(Post post)
        {
            using (var client = new WordPressClient(Config))
            {

                var id = Convert.ToInt32(client.NewPost(post));
                return id;
            }
        }

        public Post buildPostData(string title,string content)
        {
            var post = new Post
            {
                Title = title,
                Content = content,
                PublishDateTime = DateTime.Now,
                Status = "publish"

            };
            return post;
        }
    }
}
