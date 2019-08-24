using System;
using System.Text.RegularExpressions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace 酷安_UWP
{
    public sealed partial class LoginPage : Page
    {
        public static String source1 = "", source2 = "";
        public static String UserName = "", UserFace = "";

        public LoginPage()
        {
            this.InitializeComponent();

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.Tag = MainPage.applink;
        }

        public String ReplaceHtml(String str)
        {
            //换行和段落
            String s = str.Replace("<br>", "\n").Replace("<br>", "\n").Replace("<br/>", "\n").Replace("<br />", "\n").Replace("<p>", "").Replace("</p>", "\n");
            //链接彻底删除！
            try
            {
                while (s.IndexOf("<a") > 0)
                {
                    s = s.Replace(@"<a href=""" + Regex.Split(Regex.Split(s, @"<a href=""")[1], @""">")[0] + @""">", "");
                    s = s.Replace("</a>", "");
                }
            }
            catch (Exception)
            {

            }
            return s;
        }

        private async void WebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            try
            {
                string html = "";
                html = await sender.InvokeScriptAsync("eval", new String[] { "document.getElementsByTagName('html')[0].innerHTML" });
                if (html.Contains("ex-drawer__header-username"))
                {
                    UserName = Regex.Split(Regex.Split(html, @"<span class=""ex-drawer__header-username"">")[1], @"<")[0];
                    UserFace = Regex.Split(Regex.Split(html, @"<img class=""ex-drawer__header-avatar"" src=""")[1], @""">")[0];
                }
                if (!sender.Source.ToString().Contains("account.coolapk"))
                {
                    MainPage.App_Back();
                    MainPage.User_Load();
                }
                if (html.Contains("欢迎来到酷安开发者"))
                {
                    sender.Navigate(new Uri("https://developer.coolapk.com/do?c=apk&m=myList&listType=publish"));

                    //<span class="ex-drawer__header-username">一块小板子</span>
                    //<img class="ex-drawer__header-avatar" src="http://avatar.coolapk.com/data/000/69/59/42_avatar_middle.jpg?1477565948">
                }
                else if (sender.Source.ToString().Contains("listType=publish"))
                {
                    source1 = html;
                    sender.Navigate(new Uri("https://developer.coolapk.com/do?c=apk&m=myList&listType=trash"));
                }
                else if (sender.Source.ToString().Contains("listType=trash"))
                {
                    source2 = html;
                }
            }
            catch (Exception)
            {

            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage.App_Back();
        }

        private void WebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            sender.Settings.IsJavaScriptEnabled = true;
        }
    }
}