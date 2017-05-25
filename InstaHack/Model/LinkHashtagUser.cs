
namespace InstaHack
{
   public class LinkHashtagUser
    {
        public string Id { get; set; }
        public string HashtagId { get; set; }
        public string UserId { get; set; }

        public static LinkHashtagUser GetLinkHashtagUser(Hashtag tag, UserInfo user)
        {
            if (tag == null || user == null) return null;
            return new LinkHashtagUser()
            {
                HashtagId = tag.Id,
                UserId = user.Id
            };
        }
    }
}
