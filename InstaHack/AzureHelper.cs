using InstaHack.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InstaHack
{
   public static class AzureHelper
    {
        public static async Task<UserInfo> GetOrCreateUserInfo(InstagramUser instagramUser)
        {
            UserInfo output = null;
            try
            {
                output = (await App.UserInfoTable.Where(item => item.Username == instagramUser.Username).Take(1).ToEnumerableAsync()).FirstOrDefault();
                Debug.WriteLine($"User '{output.Username}' already exists");
            }
            catch (Exception)
            {
                Debug.WriteLine($"User '{instagramUser.Username}' doesn't exist");
                output = UserInfo.GetUserInfoFromInstagramUser(instagramUser);
                await App.UserInfoTable.InsertAsync(output);
                Debug.WriteLine($"ADDED User '{output.Username}'");
            }
            return output;
        }

        public static async Task<Hashtag> GetOrCreateHashtag(Tag tag)
        {
            Hashtag output = null;
            try
            {
                output = (await App.HashtagTable.Where(item => item.Name == tag.Name).Take(1).ToEnumerableAsync()).FirstOrDefault();
                Debug.WriteLine($"Hashtag '{output.Name}' already exists");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Hashtag '{tag.Name}' doesn't exist : {ex}");
                output = Hashtag.GetHashtagFromTag(tag);
                await App.HashtagTable.InsertAsync(output);
                Debug.WriteLine($"ADDED Hashtag '{output.Name}'");
            }
            return output;
        }

        public static async Task<LinkHashtagUser> GetOrCreateLinkHashtagUser(Hashtag tag, UserInfo user)
        {
            LinkHashtagUser output = null;
            try
            {
                output = (await App.LinkHashtagUserTable.Where(item => item.UserId == user.Id && item.HashtagId == tag.Id).Take(1).ToEnumerableAsync()).FirstOrDefault();
                Debug.WriteLine($"LinkHashtagUser '{output.Id}' already exists");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LinkHashtagUser for Hashtag '{tag.Id}' & User '{user.Id}' doesn't exist");
                output = LinkHashtagUser.GetLinkHashtagUser(tag, user);
                await App.LinkHashtagUserTable.InsertAsync(output);
                Debug.WriteLine($"ADDED LinkHashtagUser '{output.Id}'");
            }
            return output;
        }
    }
}
