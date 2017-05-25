using Newtonsoft.Json;
using System.Collections.Generic;

namespace InstaHack.Model
{
  
        #region Instagram Result

        public class FollowedBy
        {

            [JsonProperty("count")]
            public int Count { get; set; }
        }

        public class Follows
        {

            [JsonProperty("count")]
            public int Count { get; set; }
        }

        public class Dimensions
        {

            [JsonProperty("height")]
            public int Height { get; set; }

            [JsonProperty("width")]
            public int Width { get; set; }
        }

        public class Owner
        {

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("username")]
            public string Username { get; set; }

            [JsonProperty("full_name")]
            public string FullName { get; set; }

        }

        public class Comments
        {

            [JsonProperty("count")]
            public int Count { get; set; }
        }

        public class Likes
        {

            [JsonProperty("count")]
            public int Count { get; set; }
        }

        public class Node
        {


            [JsonProperty("id")]
            public string Id { get; set; }


            [JsonProperty("owner")]
            public Owner Owner { get; set; }

            [JsonProperty("code")]
            public string Code { get; set; }


        }


        public class Media
        {

            [JsonProperty("count")]
            public int Count { get; set; }

            [JsonProperty("nodes")]
            public IList<Node> Nodes { get; set; }


            [JsonProperty("owner")]
            public Owner Owner { get; set; }

            [JsonProperty("code")]
            public string Code { get; set; }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("date")]
            public int Date { get; set; }

            [JsonProperty("usertags")]
            public Usertags Usertags { get; set; }

        }

        public class InstagramUser
        {

            [JsonProperty("followed_by")]
            public FollowedBy FollowedBy { get; set; }

            [JsonProperty("Follows")]
            public Follows Follows { get; set; }

            [JsonProperty("username")]
            public string Username { get; set; }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("media")]
            public Media Media { get; set; }

            [JsonProperty("full_name")]
            public string FullName { get; set; }

            [JsonProperty("biography")]
            public string Biography { get; set; }

            public override string ToString()
            {
                return Username;
            }
        }

        public class ProfilePage
        {
            [JsonProperty("user")]
            public InstagramUser User { get; set; }
        }

        public class EntryData
        {

            [JsonProperty("ProfilePage")]
            public IList<ProfilePage> ProfilePage { get; set; }

            [JsonProperty("TagPage")]
            public IList<TagPage> TagPage { get; set; }

            [JsonProperty("PostPage")]
            public IList<PostPage> PostPage { get; set; }
        }


        public class InstagramResult
        {

            [JsonProperty("entry_data")]
            public EntryData EntryData { get; set; }

            [JsonProperty("platform")]
            public string Platform { get; set; }


        }
        public class TagPage
        {
            [JsonProperty("tag")]
            public Tag Tag { get; set; }
        }
        public class Tag
        {
            [JsonProperty("top_posts")]
            public TopPosts TopPosts { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("media")]
            public Media Media { get; set; }
        }
        public class TopPosts
        {
            [JsonProperty("nodes")]
            public IList<Node> Nodes { get; set; }
        }
        public class PostPage
        {

            [JsonProperty("media")]
            public Media Media { get; set; }

            [JsonProperty("graphql")]
            public Graphql Graphql { get; set; }
        }

        public class Usertags
        {

            [JsonProperty("nodes")]
            public IList<Node> Nodes { get; set; }
        }


        public class Graphql
        {
            [JsonProperty("shortcode_media")]
            public ShortcodeMedia ShortcodeMedia { get; set; }
        }


        public class ShortcodeMedia
        {

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("shortcode")]
            public string Shortcode { get; set; }

            [JsonProperty("owner")]
            public Owner Owner { get; set; }

        }

        #endregion
    }

