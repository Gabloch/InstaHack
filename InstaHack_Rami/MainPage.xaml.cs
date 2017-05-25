using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using InstaHack_Rami.Model;
using Newtonsoft.Json;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace InstaHack_Rami
{
    /// <summary>
    ///     Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static readonly HttpClient Httpclient = new HttpClient();
        public string currentUrl;
        public int nbTotalPhotos;

        public int nbUploadedPhotos;

        public MainPage()
        {
            InitializeComponent();
        }

        private Hashtag CurrentHashtag { get; set; }

        private async void Webview_OnContentLoading(WebView sender, WebViewContentLoadingEventArgs args)
        {
            Debug.WriteLine("Content Loading");
            currentUrl = args.Uri.ToString();
            Debug.WriteLine("current url : " + currentUrl);
            var instagramContent = ExtractInstagramContentInUrl(currentUrl);
            Debug.WriteLine("InstagramContent : " + instagramContent);
            await HandleContent(await instagramContent);
        }

        private void Webview_OnFrameNavigationCompletedew_FrameNavigationCompleted(WebView sender,
            WebViewNavigationCompletedEventArgs args)
        {
            // TODO : Implement
        }

        private async Task HandleContent(EntryData entityData, Hashtag hashtagContext = null)
        {
            // UpdateView();
            try
            {
                if (entityData.PostPage != null)
                    foreach (var postPage in entityData.PostPage)
                    {
                        await HandlePostPage(postPage, hashtagContext);
                        Debug.WriteLine("Handle post page start");
                    }
                if (entityData.ProfilePage != null)
                    foreach (var profilePage in entityData.ProfilePage)
                    {
                        HandleProfilePage(profilePage, hashtagContext);
                        Debug.WriteLine("Handle profile page start");
                    }
                if (entityData.TagPage != null)
                    foreach (var tagPage in entityData.TagPage)
                    {
                        HandleTagPage(tagPage);
                        Debug.WriteLine("Handle Tag page start");
                    }
                // UpdateView();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("No data found");
                Debug.WriteLine($"{ex}");
            }
        }

        public async Task HandlePostPage(PostPage postPage, Hashtag hashtagContext = null)
        {
            // Declare new thread
            //   Semaphore.Wait();
            // UpdateView();
            var instagramContent =
                await ExtractInstagramContentInUrl(
                    $"https://www.instagram.com/{postPage.Graphql.ShortcodeMedia.Owner.Username}/");
            Debug.WriteLine("Extracting : " +
                            $"https://www.instagram.com/{postPage.Graphql.ShortcodeMedia.Owner.Username}/");
            if (instagramContent != null)
            {
                Debug.WriteLine("Handle Post page, instagram content not null, starting Handle content");
                await HandleContent(instagramContent, hashtagContext);
            }
            // Release this thread
            //  Semaphore.Release();
            //  UpdateView();
        }

        public async Task HandleProfilePage(ProfilePage profilePage, Hashtag hashtagContext = null)
        {
            // Declare new thread
            // Semaphore.Wait();
            //  UpdateView();
            var user = await AzureHelper.GetOrCreateUserInfo(profilePage.User);
            if (hashtagContext != null)
                await AzureHelper.GetOrCreateLinkHashtagUser(hashtagContext, user);
            // Release this thread
            //  Semaphore.Release();
            //  UpdateView();
        }


        public async Task HandleTagPage(TagPage tagPage)
        {
            // Declare new thread
            // Semaphore.Wait();
            //  UpdateView();
            var Hashtag = await AzureHelper.GetOrCreateHashtag(tagPage.Tag);
            CurrentHashtag = Hashtag;
            Debug.WriteLine("current hashtag : " + Hashtag);
            var Codes = new HashSet<string>(); // No duplicates in those lists
            foreach (var _node in tagPage.Tag.TopPosts.Nodes)
                if (!Codes.Contains(_node.Code))
                    Codes.Add(_node.Code);
            // nbOperationMax++;
            foreach (var _node in tagPage.Tag.Media.Nodes)
                if (!Codes.Contains(_node.Code))
                    Codes.Add(_node.Code);
            // nbOperationMax++;

            Debug.WriteLine($"Handeling the new {Codes.Count} codes");
            foreach (var Code in Codes)
            {
                var instagramContent = await ExtractInstagramContentInUrl($"https://www.instagram.com/p/{Code}/");
                if (instagramContent != null)
                    await HandleContent(instagramContent, Hashtag);
            }
            // Release this thread
            // Semaphore.Release();
            // UpdateView();
        }


        /// <summary>
        ///     Retrieve the JSON of the rawdata
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        private static InstagramResult GetInstagramObjects(string rawData)
        {
            InstagramResult output;
            const string pattern =
                @"[\s\S\r]+<script type=\""text\/javascript\"">window\._sharedData = (.*);<\/script>[\s\S\r]+";
            const string substitution = @"$1";
            var regex = new Regex(pattern);
            try
            {
                var onlyJson = regex.Replace(rawData, substitution, 1);
                Debug.WriteLine("JSON : " + onlyJson);
                output = JsonConvert.DeserializeObject<InstagramResult>(onlyJson);
            }
            catch (Exception)
            {
                Debug.WriteLine("Could not replace rawData by JSon or deserialize the Json Object.");
                output = null;
            }
            return output;
        }

        private static async Task<EntryData> ExtractInstagramContentFromContent(string content)
        {
            try
            {
                var instagramResult = GetInstagramObjects(content);
                return instagramResult?.EntryData;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex}");
                return null;
            }
        }

        private static async Task<EntryData> ExtractInstagramContentInUrl(string url)
        {
            Debug.WriteLine($"Retrieving Json : {url}");
            try
            {
                var rawcontent = await Httpclient.GetStringAsync(url);
                return await ExtractInstagramContentFromContent(rawcontent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex}");
                return null;
            }
        }


        /// <summary>
        ///     Scroll Automatically toward the bottom.
        /// </summary>
        public async void AutoScrollBottom()
        {
            var ScrollToTopString = @"window.scrollTo(0,document.body.scrollHeight);";
            await Webview.InvokeScriptAsync("eval", new[] {ScrollToTopString});
        }
    }
}