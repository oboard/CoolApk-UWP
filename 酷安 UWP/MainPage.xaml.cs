using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using 酷安_UWP.Model;
// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace 酷安_UWP
{
    public sealed partial class MainPage : Page
    {
        int itemw = 110, itemh = 90;

        public static Frame _ViewFrame, _ViewFrameS;
        public static TextBlock _User_Name;
        public static ImageBrush _User_Face;
        public static ColumnDefinition _dcd, _lcd;

        //App链接
        public static String applink = "", search = "";

        Color CoolColor = ((SolidColorBrush)Application.Current.Resources["CoolApk_Theme"]).Color;
        Color CoolForeColor = ((SolidColorBrush)Application.Current.Resources["CoolApk_Theme_Fore"]).Color;
        Color CoolForeInactiveColor = Color.FromArgb(255, 50, 50, 50);
        Color CoolBackPressedColor = Color.FromArgb(255, 200, 200, 200);
        Color CoolBackHoverColor = Color.FromArgb(255, 255, 255, 255);
        Color CoolBackColor = Color.FromArgb(255, 230, 230, 230);
        //Color.FromArgb(255, 72, 174, 76);

        public MainPage()
        {
            this.InitializeComponent();

            // 判断是否存在 StatusBar
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                StatusBar statusBar = StatusBar.GetForCurrentView();
                statusBar.BackgroundOpacity = 1; // 透明度
                statusBar.BackgroundColor = CoolBackColor;
                statusBar.ForegroundColor = CoolForeColor;
            }
            else
            {
                var view = ApplicationView.GetForCurrentView().TitleBar;
                // active
                view.BackgroundColor = CoolBackColor;
                view.ForegroundColor = CoolForeColor;

                // inactive
                view.InactiveBackgroundColor = CoolBackColor;
                view.InactiveForegroundColor = CoolForeInactiveColor;

                // button
                view.ButtonBackgroundColor = CoolBackColor;
                view.ButtonForegroundColor = CoolForeColor;

                view.ButtonHoverBackgroundColor = CoolBackHoverColor;
                view.ButtonHoverForegroundColor = CoolForeColor;

                view.ButtonPressedBackgroundColor = CoolBackPressedColor;
                view.ButtonPressedForegroundColor = CoolForeColor;

                view.ButtonInactiveBackgroundColor = CoolBackColor;
                view.ButtonInactiveForegroundColor = CoolForeInactiveColor;

                //标题栏返回按钮事件注册
                //Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
                //👆微软放弃返回按钮了！！！！！！！！！！！！！！！！！！！！！！！！
            }
            _ViewFrame = ViewFrame;
            _ViewFrameS = ViewFrameS;
            _dcd = dcd;

            LeftButton1.Foreground = new SolidColorBrush(CoolColor);
            BottomBar1.Foreground = new SolidColorBrush(CoolColor);
        }



        #region Main：主要

        public static void User_Load()
        {
            _User_Name.Text = LoginPage.UserName;
            _User_Face.ImageSource = new BitmapImage(new Uri(LoginPage.UserFace, UriKind.RelativeOrAbsolute));

            //Save
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["name"] = LoginPage.UserName;
            localSettings.Values["face"] = LoginPage.UserFace;
            localSettings.Values["login"] = "1";
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //网络信息
            //LoadNewUpdate(await Web.GetHttp("https://www.coolapk.com/"));
            LoadHotApp(await Web.GetHttp("https://www.coolapk.com/apk/recommend"));
            LoadDeveloperApp(await Web.GetHttp("https://www.coolapk.com/apk/developer"));
            LoadHotGame(await Web.GetHttp("https://www.coolapk.com/game/"));
            LoadHotGame(await Web.GetHttp("https://www.coolapk.com/game?p=2"));
            LoadHotGame(await Web.GetHttp("https://www.coolapk.com/game?p=3"));
        }

        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            //Frame rootFrame = Window.Current.Content as Frame;
            if (ViewFrame == null) return;
            App_Back();
        }

        public static void App_Back()
        {
            if (_ViewFrame.Visibility == Visibility.Visible)
            {
                _ViewFrame.Visibility = Visibility.Collapsed;
            }
            else
            {
                _ViewFrameS.Visibility = Visibility.Collapsed;
            }

            if (_ViewFrame.Visibility == Visibility.Collapsed && _ViewFrameS.Visibility == Visibility.Collapsed) SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }
        private void App_Back(object sender, RoutedEventArgs e)
        {
            App_Back();
        }

        #endregion



        #region Task：任务


        private void LoadNewUpdate(String str)
        {
            //绑定一个列表
            ObservableCollection<AppDate> updateCollection = new ObservableCollection<AppDate>();
            updateview.ItemsSource = updateCollection;


            //循环添加AppDate
            String body = str.Substring(str.IndexOf("<!--最近更新应用-->"), str.IndexOf("<!--应用推荐-->") - str.IndexOf("<!--最近更新应用-->"));
            body = Regex.Split(body, @"<div class=""applications"">")[1];
            String[] bodys = Regex.Split(body, @"\n"); for (int i = 0; i < 12; i++)
            {
                AppDate date = new AppDate()
                {
                    Tag = bodys[i * 12 + 1].Split('"')[1],
                    Thumbnail = new Uri(bodys[i * 12 + 4].Split('"')[1], UriKind.RelativeOrAbsolute),
                    Title = bodys[i * 12 + 7].Split('>')[1].Split('<')[0],
                    Describe = bodys[i * 12 + 8].Split('>')[1].Split('<')[0],
                };
                updateCollection.Add(date);
            }
        }



        private void LoadHotGame(String str)
        {
            //绑定一个列表
            ObservableCollection<AppDate> gameCollection = new ObservableCollection<AppDate>();
            gamelist.ItemsSource = gameCollection;


            //循环添加AppDate
            String bod = Regex.Split(str, @"<div class=""applications"">")[1];
            for (int i = 0; i < 4; i++)
            {
                AppDate date = new AppDate()
                {
                    Tag = "/game/" + Regex.Split(bod, "game/")[i + 1].Split('"')[0],
                    Thumbnail = new Uri(Regex.Split(bod, "src=")[i + 1].Split('"')[1], UriKind.RelativeOrAbsolute),
                    Title = Regex.Split(bod, @"sp-name""")[i + 1].Split('>')[1].Split('<')[0],
                    Describe = Regex.Split(bod, @"sp-time""")[i + 1].Split('>')[1].Split('<')[0]
                };
                gameCollection.Add(date);
            }


            //循环添加AppDate
            String body = Regex.Split(str, @"<div class=""game_left_three"">")[1];
            String[] bodys = Regex.Split(body, @"\n");
            for (int i = 0; i < 9; i++)
            {
                AppDate date = new AppDate()
                {
                    Tag = bodys[i * 15 + 6].Split('"')[1].Split('/')[2],
                    Thumbnail = new Uri(bodys[i * 15 + 6 + 3].Split('"')[3], UriKind.RelativeOrAbsolute),
                    Title = Web.ReplaceHtml(bodys[i * 15 + 6 + 5].Split('>')[1].Split('<')[0]),
                    Describe = Web.ReplaceHtml(bodys[i * 15 + 6 + 7].Split('>')[1].Split('<')[0])
                };
                gameCollection.Add(date);
            }
        }

        private void LoadHotApp(String str)
        {
            //绑定一个列表
            ObservableCollection<AppDate> hotCollection = new ObservableCollection<AppDate>();
            hotview.ItemsSource = hotCollection;


            //循环添加AppDate
            String body = Regex.Split(str, @"<div class=""app_list_left"">")[1];
            String[] bodys = Regex.Split(body, @"\n");
            for (int i = 0; i < 20; i++)
            {
                AppDate date = new AppDate()
                {
                    Tag = bodys[i * 15 + 5].Split('"')[1],
                    Thumbnail = new Uri(bodys[i * 15 + 5 + 3].Split('"')[3], UriKind.RelativeOrAbsolute),
                    Title = bodys[i * 15 + 5 + 5].Split('>')[1].Split('<')[0],
                    Describe = bodys[i * 15 + 5 + 7].Split('>')[1].Split('<')[0].Split(' ')[0]
                };
                hotCollection.Add(date);
            }
        }



        private void LoadDeveloperApp(String str)
        {
            //绑定一个列表
            ObservableCollection<AppDate> developerCollection = new ObservableCollection<AppDate>();
            developerview.ItemsSource = developerCollection;


            //循环添加AppDate
            String body = Regex.Split(str, @"<div class=""left_nav"">")[1];
            String[] bodys = Regex.Split(body, @"\n");
            for (int i = 0; i < 20; i++)
            {
                AppDate date = new AppDate()
                {
                    Tag = bodys[i * 15 + 4].Split('"')[1],
                    Thumbnail = new Uri(bodys[i * 15 + 4 + 3].Split('"')[3], UriKind.RelativeOrAbsolute),
                    Title = bodys[i * 15 + 4 + 5].Split('>')[1].Split('<')[0],
                    Describe = bodys[i * 15 + 4 + 9].Split('>')[1].Split('<')[0].Replace("开发者:", "")
                };
                developerCollection.Add(date);
            }
        }

        private void LoadDeveloper(String str, ListView list)
        {
            //绑定一个列表
            ObservableCollection<AppDate> Collection = new ObservableCollection<AppDate>();
            list.ItemsSource = Collection;

            if (str.Equals("")) return;

            //循环添加AppDate
            String body = Regex.Split(str, "<tbody>")[1];
            body = Regex.Split(body, "</tbody>")[0];
            String[] bodys = Regex.Split(body, @"\n");
            String[] bodylist = Regex.Split(body, @"<tr id=""data-row--");
            for (int i = 0; i < bodylist.Length - 1; i++)
            {
                AppDate date = new AppDate()
                {
                    Tag = bodys[i * 33 + 3 + 2].Split('"')[1],
                    Thumbnail = new Uri(bodys[i * 33 + 3 + 2].Split('"')[7], UriKind.RelativeOrAbsolute),
                    Title = bodys[i * 33 + 3 + 5].Split('>')[1].Split('<')[0],
                    Describe = bodys[i * 33 + 3 + 6].Split('>')[1].Split('<')[0]
                };
                Collection.Add(date);
            }
        }







        #endregion




        private void updateview_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppDate date = e.ClickedItem as AppDate;

            if (date != null) OpenAppPage("https://www.coolapk.com" + date.Tag);
        }

        private void Gamelist_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppDate date = e.ClickedItem as AppDate;

            if (date != null) OpenAppPage("https://www.coolapk.com/apk/" + date.Tag);
        }





        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchPage newPage = new SearchPage();
            ViewFrameS.Navigate(typeof(SearchPage), newPage);
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            ViewFrameS.Visibility = Visibility.Visible;
        }


        private void Bar_Click(object sender, RoutedEventArgs e)
        {
            //Hide All
            P1.Visibility = Visibility.Collapsed;
            P2.Visibility = Visibility.Collapsed;
            _ViewFrameS.Visibility = Visibility.Collapsed;
            LeftButton1.Foreground = new SolidColorBrush(Colors.Black);
            LeftButton2.Foreground = new SolidColorBrush(Colors.Black);
            LeftButton3.Foreground = new SolidColorBrush(Colors.Black);
            BottomBar1.Foreground = new SolidColorBrush(Colors.Black);
            BottomBar2.Foreground = new SolidColorBrush(Colors.Black);
            BottomBar3.Foreground = new SolidColorBrush(Colors.Black);

            string s = "1";
            if (sender is AppBarButton)
                s = ((AppBarButton)sender).Tag.ToString();
            else
                s = ((Button)sender).Tag.ToString();
            //Show
            switch (s)
            {
                case "1":
                    LeftButton1.Foreground = new SolidColorBrush(CoolColor);
                    BottomBar1.Foreground = new SolidColorBrush(CoolColor);
                    P1.Visibility = Visibility.Visible;
                    break;
                case "2":
                    LeftButton2.Foreground = new SolidColorBrush(CoolColor);
                    BottomBar2.Foreground = new SolidColorBrush(CoolColor);
                    LoadDeveloper(LoginPage.source1, xl1);
                    LoadDeveloper(LoginPage.source2, xl2);
                    P2.Visibility = Visibility.Visible;
                    break;
                case "3":
                    LeftButton3.Foreground = new SolidColorBrush(CoolColor);
                    BottomBar3.Foreground = new SolidColorBrush(CoolColor);
                    _ViewFrameS.Navigate(typeof(MyPage), new MyPage());
                    _ViewFrameS.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void CatchLoveButton_Click(object sender, RoutedEventArgs e)
        {
            OpenAppPage("https://www.coolapk.com" + ((AppBarButton)sender).Tag.ToString());
        }

        private void Classify_Click(object sender, RoutedEventArgs e)
        {
            search = ((Button)sender).Content.ToString();
            SearchButton_Click(null, null);
        }





        public static void OpenAppPage(String link)
        {
            AppPage newPage = new AppPage();
            applink = link;
            _ViewFrame.Navigate(typeof(AppPage), newPage);
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            _ViewFrame.Visibility = Visibility.Visible;
        }


    }
}
