using System.Diagnostics;

namespace InstaHack_Rami.Model
{
    public class UserInfo
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; } = "";
        public int FollowedBy { get; set; }
        public int Follows { get; set; }
        public string Biography { get; set; } = "";

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:InstaHack.iOS.Person" /> class.
        /// </summary>
        /// <param name="user">User.</param>
        public static UserInfo GetUserInfoFromInstagramUser(InstagramUser user)
        {
            if (user == null) return null;
            return new UserInfo
            {
                Id = user.Id,
                Username = user.Username,
                Fullname = user.FullName,
                FollowedBy = user.FollowedBy.Count,
                Follows = user.Follows.Count,
                Biography = user.Biography
            };
        }

        public override string ToString()
        {
            Debug.WriteLine($"Id = {Id}");
            Debug.WriteLine($"Username = {Username}");
            Debug.WriteLine($"Fullname = {Fullname}");
            Debug.WriteLine($"Follows = {Follows}");
            Debug.WriteLine($"FollowedBy = {FollowedBy}");
            return
                $"Username: {Username.PadRight(30)} Nom complet:  {Fullname.PadRight(30)} Nb Followers:  {FollowedBy} ";
        }
    }
}