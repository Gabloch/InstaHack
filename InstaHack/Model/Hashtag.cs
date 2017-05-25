using InstaHack.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InstaHack
{
    public class Hashtag
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long CountOnInstagram { get; set; } = 0;

        public static Hashtag GetHashtagFromTag(Tag tag)
        {
            if (tag == null) return null;
            return new Hashtag()
            {
                Name = tag.Name,
                CountOnInstagram = tag.Media?.Count ?? 0
            };
        }
    }
}
