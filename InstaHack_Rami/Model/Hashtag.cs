namespace InstaHack_Rami.Model
{
    public class Hashtag
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long CountOnInstagram { get; set; }

        public static Hashtag GetHashtagFromTag(Tag tag)
        {
            if (tag == null) return null;
            return new Hashtag
            {
                Name = tag.Name,
                CountOnInstagram = tag.Media?.Count ?? 0
            };
        }
    }
}