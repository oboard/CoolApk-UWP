using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.ViewManagement;
using Windows.UI;
using Windows.Foundation.Metadata;
using Windows.Foundation;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media.Animation;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq;
using Windows.UI.Xaml.Media.Imaging;
using System.Text.RegularExpressions;
using Windows.UI.Core;
using Windows.Storage;
// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace 酷安_UWP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //最近更新
        Grid[] newUpdate;
        Image[] newUpdateImage;
        TextBlock[] newUpdateText;
        TextBlock[] newUpdateTime;

        //开发者应用
        Grid[] developerApp;
        Image[] developerAppImage;
        TextBlock[] developerAppText;
        TextBlock[] developerAppName;

        //热门应用
        Grid[] hotApp;
        Image[] hotAppImage;
        TextBlock[] hotAppText;
        TextBlock[] hotAppName;

        int itemw = 100, itemh = 90;
        
        public static Frame _ViewFrame, _ViewFrameS;
        public static TextBlock _User_Name;
        public static Image _User_Face;
        public static ColumnDefinition _dcd, _lcd;

        //App链接
        public static String applink = "", searchlink = "";
        
        Color CoolColor = Color.FromArgb(255, 72, 174, 76);

        public MainPage()
        {
            this.InitializeComponent();
            // 判断是否存在 StatusBar
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                StatusBar statusBar = StatusBar.GetForCurrentView();
                statusBar.BackgroundOpacity = 1; // 透明度
                statusBar.BackgroundColor = CoolColor;
                statusBar.ForegroundColor = Colors.White;
            }
            else
            {
                var view = ApplicationView.GetForCurrentView().TitleBar;
                // active
                view.BackgroundColor = CoolColor;
                view.ForegroundColor = Colors.White;

                // inactive
                view.InactiveBackgroundColor = CoolColor;
                view.InactiveForegroundColor = Colors.White;

                // button
                view.ButtonBackgroundColor = CoolColor;
                view.ButtonForegroundColor = Colors.White;

                view.ButtonHoverBackgroundColor = CoolColor;
                view.ButtonHoverForegroundColor = Colors.White;

                view.ButtonPressedBackgroundColor = CoolColor;
                view.ButtonPressedForegroundColor = Colors.White;

                view.ButtonInactiveBackgroundColor = CoolColor;
                view.ButtonInactiveForegroundColor = Colors.White;

                //标题栏返回按钮事件注册
                Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
            }
            _ViewFrame = ViewFrame;
            _ViewFrameS = ViewFrameS;
            _User_Name = User_Name;
            _User_Face = User_Face;
            _dcd = dcd;

            LeftButton1.Foreground = new SolidColorBrush(CoolColor);
            BottomBar1.Foreground = new SolidColorBrush(CoolColor);
        }

        public static void User_Load()
        {
            _User_Name.Text = LoginPage.UserName;
            _User_Face.Source = new BitmapImage(new Uri(LoginPage.UserFace, UriKind.RelativeOrAbsolute));

            //Save
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["name"] = LoginPage.UserName;
            localSettings.Values["face"] = LoginPage.UserFace;
            localSettings.Values["login"] = "1";
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // 绑定到匿名类型上
            //PART_ListViewStateName.ItemsSource = new[] { new { Text = "应用" }, new { Text = "游戏" }};

            //本地信息
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            try
            {
                if (localSettings.Values["login"].ToString().Contains("1"))
                {
                    _User_Name.Text = localSettings.Values["name"].ToString();
                    _User_Face.Source = new BitmapImage(new Uri(localSettings.Values["face"].ToString(), UriKind.RelativeOrAbsolute));
                }
            }
            catch (Exception ex)
            {
                localSettings.Values["login"] = "0";
            }
            //网络信息
            await LoadNewUpdate(await WebMessage.getMessage("https://www.coolapk.com/"));
            await LoadCatchLove(await WebMessage.getMessage("https://www.coolapk.com/apk?p=2"));
            await LoadHotApp(await WebMessage.getMessage("https://www.coolapk.com/apk/recommend"));
            await LoadDeveloperApp(await WebMessage.getMessage("https://www.coolapk.com/apk/developer"));
            await LoadHotGame(await WebMessage.getMessage("https://www.coolapk.com/game/"));
            await LoadHotGame(await WebMessage.getMessage("https://www.coolapk.com/game?p=2"));
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
            var m = SearchText.Margin;
            m.Left = 0;
            SearchText.Margin = m;
            SearchBack.Visibility = Visibility.Collapsed;

            App_Back();
        }

        private async Task LoadHotGame(String str)
        {
            String bod = Regex.Split(str, @"<div class=""applications"">")[1];
            for (int i = 0; i < 4; i++)
            {
                Grid newG = new Grid();
                {
                    newG.Height = itemh;
                    newG.Width = itemw;

                    newG.Tag = "/game/" + Regex.Split(bod, "game/")[i + 1].Split('"')[0];
                }
                Image newI = new Image();
                {
                    var margin = hotAppImage[i].Margin;
                    margin.Left = 10;
                    margin.Top = 10;
                    margin.Right = 10;
                    margin.Bottom = 40;
                    newI.Margin = margin;
                    try
                    {
                        newI.Source = new BitmapImage(new Uri(Regex.Split(bod, "src=")[i + 1].Split('"')[1], UriKind.RelativeOrAbsolute));
                    }
                    catch (Exception e)
                    {
                    }
                    //                    await WebMessage.DownloadImage(bodys[(i + 1) * 2]);
                }
                TextBlock newT = new TextBlock();
                {
                    var margin = newT.Margin;
                    margin.Bottom = 20;
                    newT.Margin = margin;
                    newT.Height = 20;
                    newT.FontSize = 13;
                    newT.TextAlignment = TextAlignment.Center;
                    newT.VerticalAlignment = VerticalAlignment.Bottom;
                    newT.Foreground = new SolidColorBrush(Colors.Black);
                    newT.Text = Regex.Split(bod, @"sp-name""")[i + 1].Split('>')[1].Split('<')[0];
                }
                TextBlock newTT = new TextBlock();
                {
                    newTT.Height = 20;
                    newTT.FontSize = 8;
                    newTT.TextAlignment = TextAlignment.Center;
                    newTT.VerticalAlignment = VerticalAlignment.Bottom;
                    newTT.Foreground = new SolidColorBrush(Colors.Black);
                    newTT.Text = Regex.Split(bod, @"sp-time""")[i + 1].Split('>')[1].Split('<')[0];
                }
                newG.Children.Add(newI);
                newG.Children.Add(newT);
                newG.Children.Add(newTT);
                hotgview.Items.Add(newG);
            }

            String body = Regex.Split(str, @"<div class=""game_left_three"">")[1];
            String[] bodys = Regex.Split(body, @"\n");
            for (int i = 0; i < 9; i++)
            {
                Grid newGrid = new Grid();
                {
                    newGrid.Height = 60;
                    newGrid.Tag = bodys[i * 15 + 6].Split('"')[1].Split('/')[2];
                }
                Image newImage = new Image();
                {
                    var margin = newImage.Margin;
                    margin.Left = 20;
                    margin.Top = 10;
                    margin.Right = 0;
                    margin.Bottom = 10;
                    newImage.Margin = margin;
                    newImage.Width = 40;
                    newImage.HorizontalAlignment = HorizontalAlignment.Left;
                    try
                    {
                        newImage.Source = new BitmapImage(new Uri(bodys[i * 15 + 6 + 3].Split('"')[3], UriKind.RelativeOrAbsolute));
                    }
                    catch (Exception e)
                    {
                    }
                }
                TextBlock newText1 = new TextBlock();
                {
                    var margin = newText1.Margin;
                    margin.Left = 80;
                    margin.Top = 10;
                    newText1.Margin = margin;
                    newText1.HorizontalAlignment = HorizontalAlignment.Left;
                    newText1.VerticalAlignment = VerticalAlignment.Top;
                    newText1.Foreground = new SolidColorBrush(Colors.Black);
                    newText1.TextWrapping = TextWrapping.Wrap;
                    newText1.Text = WebMessage.ReplaceHtml(bodys[i * 15 + 6 + 5].Split('>')[1].Split('<')[0]);
                }
                TextBlock newText2 = new TextBlock();
                {
                    var margin = newText2.Margin;
                    margin.Left = 80;
                    margin.Top = 30;
                    newText2.Margin = margin;
                    newText2.HorizontalAlignment = HorizontalAlignment.Left;
                    newText2.VerticalAlignment = VerticalAlignment.Top;
                    newText2.Foreground = new SolidColorBrush(Color.FromArgb(60, 0, 0, 0));
                    newText2.TextWrapping = TextWrapping.Wrap;
                    newText2.Text = WebMessage.ReplaceHtml(bodys[i * 15 + 6 + 7].Split('>')[1].Split('<')[0]);
                }
                newGrid.Children.Add(newImage);
                newGrid.Children.Add(newText1);
                newGrid.Children.Add(newText2);
                gamelist.Items.Add(newGrid);
            }
        }

        private async Task LoadHotApp(String str)
        {
            String body = Regex.Split(str, @"<div class=""app_list_left"">")[1];
            String[] bodys = Regex.Split(body, @"\n");
            hotApp = new Grid[20];
            hotAppImage = new Image[20];
            hotAppText = new TextBlock[20];
            hotAppName = new TextBlock[20];
            for (int i = 0; i < 20; i++)
            {
                try
                {
                    hotApp[i].Children.Remove(hotAppImage[i]);
                    hotApp[i].Children.Remove(hotAppText[i]);
                    hotApp[i].Children.Remove(hotAppName[i]);
                    hotview.Items.Remove(hotApp[i]);
                }
                catch (Exception e)
                {
                }
                try {
                    hotApp[i] = new Grid();
                    {
                        hotApp[i].Height = itemh;
                        hotApp[i].Width = itemw;
                        hotApp[i].Tag = bodys[i * 15 + 5].Split('"')[1];
                    }
                    hotAppImage[i] = new Image();
                    {
                        var margin = hotAppImage[i].Margin;
                        margin.Left = 10;
                        margin.Top = 10;
                        margin.Right = 10;
                        margin.Bottom = 40;
                        hotAppImage[i].Margin = margin;
                        try
                        {
                            hotAppImage[i].Source = new BitmapImage(new Uri(bodys[i * 15 + 5 + 3].Split('"')[3], UriKind.RelativeOrAbsolute));
                        }
                        catch (Exception e)
                        {
                        }
                        //                    await WebMessage.DownloadImage(bodys[(i + 1) * 2]);
                    }
                    hotAppText[i] = new TextBlock();
                    {
                        var margin = hotAppText[i].Margin;
                        margin.Bottom = 20;
                        hotAppText[i].Margin = margin;
                        hotAppText[i].Height = 20;
                        hotAppText[i].FontSize = 13;
                        hotAppText[i].TextAlignment = TextAlignment.Center;
                        hotAppText[i].VerticalAlignment = VerticalAlignment.Bottom;
                        hotAppText[i].Foreground = new SolidColorBrush(Colors.Black);
                        hotAppText[i].Text = bodys[i * 15 + 5 + 5].Split('>')[1].Split('<')[0];
                    }
                    hotAppName[i] = new TextBlock();
                    {
                        hotAppName[i].Height = 20;
                        hotAppName[i].FontSize = 8;
                        hotAppName[i].TextAlignment = TextAlignment.Center;
                        hotAppName[i].VerticalAlignment = VerticalAlignment.Bottom;
                        hotAppName[i].Foreground = new SolidColorBrush(Colors.Black);
                        hotAppName[i].Text = bodys[i * 15 + 5 + 9].Split('>')[1].Split('<')[0];
                    }
                    hotApp[i].Children.Add(hotAppImage[i]);
                    hotApp[i].Children.Add(hotAppText[i]);
                    hotApp[i].Children.Add(hotAppName[i]);
                    hotview.Items.Add(hotApp[i]);
                }
                catch (Exception e)
                {

                }
            }

        }

        private async Task LoadDeveloperApp(String str)
        {
            String body = Regex.Split(str, @"<div class=""left_nav"">")[1];
            String[] bodys = Regex.Split(body, @"\n");
            developerApp = new Grid[20];
            developerAppImage = new Image[20];
            developerAppText = new TextBlock[20];
            developerAppName = new TextBlock[20];
            for (int i = 0; i < 20; i++)
            {
                try
                {
                    developerApp[i].Children.Remove(developerAppImage[i]);
                    developerApp[i].Children.Remove(developerAppText[i]);
                    developerApp[i].Children.Remove(developerAppName[i]);
                    developerview.Items.Remove(developerApp[i]);
                }
                catch (Exception e)
                {
                }
                developerApp[i] = new Grid();
                {
                    developerApp[i].Height = itemh;
                    developerApp[i].Width = itemw;
                    developerApp[i].Tag = bodys[i * 15 + 4].Split('"')[1];
                }
                developerAppImage[i] = new Image();
                {
                    var margin = developerAppImage[i].Margin;
                    margin.Left = 10;
                    margin.Top = 10;
                    margin.Right = 10;
                    margin.Bottom = 40;
                    developerAppImage[i].Margin = margin;
                    try
                    {
                        developerAppImage[i].Source = new BitmapImage(new Uri(bodys[i * 15 + 4 + 3].Split('"')[3], UriKind.RelativeOrAbsolute));
                    }
                    catch (Exception e)
                    {
                    }
                    //                    await WebMessage.DownloadImage(bodys[(i + 1) * 2]);
                }
                developerAppText[i] = new TextBlock();
                {
                    var margin = developerAppText[i].Margin;
                    margin.Bottom = 20;
                    developerAppText[i].Margin = margin;
                    developerAppText[i].Height = 20;
                    developerAppText[i].FontSize = 13;
                    developerAppText[i].TextAlignment = TextAlignment.Center;
                    developerAppText[i].VerticalAlignment = VerticalAlignment.Bottom;
                    developerAppText[i].Foreground = new SolidColorBrush(Colors.Black);
                    developerAppText[i].Text = bodys[i * 15 + 4 + 5].Split('>')[1].Split('<')[0];
                }
                developerAppName[i] = new TextBlock();
                {
                    developerAppName[i].Height = 20;
                    developerAppName[i].FontSize = 8;
                    developerAppName[i].TextAlignment = TextAlignment.Center;
                    developerAppName[i].VerticalAlignment = VerticalAlignment.Bottom;
                    developerAppName[i].Foreground = new SolidColorBrush(Colors.Black);
                    developerAppName[i].Text = bodys[i * 15 + 4 + 9].Split('>')[1].Split('<')[0].Replace("开发者:","");
                }
                developerApp[i].Children.Add(developerAppImage[i]);
                developerApp[i].Children.Add(developerAppText[i]);
                developerApp[i].Children.Add(developerAppName[i]);
                developerview.Items.Add(developerApp[i]);
            }
        }

        private async Task LoadCatchLove(String str)
        {
            String body = Regex.Split(str , @"<div class=""applications"">")[1];
            CatchLoveButton.Tag = Regex.Split(Regex.Split(body, @"<a href=""")[1], @""">")[0];
            CatchLoveButton2.Tag = Regex.Split(Regex.Split(body, @"<a href=""")[2], @""">")[0];
            CatchLoveImage.Source =  new BitmapImage(new Uri(Regex.Split(body, "<img")[1].Split('"')[1], UriKind.RelativeOrAbsolute));
            CatchLoveImage2.Source = new BitmapImage(new Uri(Regex.Split(body, "<img")[2].Split('"')[1], UriKind.RelativeOrAbsolute));
        }

        private async Task LoadDeveloper(String str, ListView list)
        {
            String body = Regex.Split(str, "<tbody>")[1];
            body = Regex.Split(body, "</tbody>")[0];
            String[] bodys = Regex.Split(body, @"\n");
            String[] bodylist = Regex.Split(body, @"<tr id=""data-row--");
            for (int i = 0; i < bodylist.Length - 1; i++)
            {
                Grid newGrid = new Grid();
                {
                    newGrid.Height = 80;
                    newGrid.Tag = bodys[i * 33 + 3 + 2].Split('"')[1];
                }
                Image newImage = new Image();
                {
                    var margin = newImage.Margin;
                    margin.Left = 10;
                    margin.Top = 10;
                    margin.Right = 10;
                    margin.Bottom = 10;
                    newImage.Margin = margin;
                    newImage.Width = 40;
                    newImage.HorizontalAlignment = HorizontalAlignment.Left;
                    try
                    {
                        newImage.Source = new BitmapImage(new Uri(bodys[i * 33 + 3 + 2].Split('"')[7], UriKind.RelativeOrAbsolute));
                    }
                    catch (Exception e)
                    {
                    }
                }
                TextBlock newText1 = new TextBlock();
                {
                    var margin = newText1.Margin;
                    margin.Left = 80;
                    margin.Top = 20;
                    newText1.Margin = margin;
                    newText1.VerticalAlignment = VerticalAlignment.Top;
                    newText1.Foreground = new SolidColorBrush(Colors.Black);
                    newText1.Text = bodys[i * 33 + 3 + 5].Split('>')[1].Split('<')[0];
                }
                TextBlock newText2 = new TextBlock();
                {
                    var margin = newText2.Margin;
                    margin.Left = 80;
                    margin.Top = 30;
                    newText2.Margin = margin;
                    newText2.VerticalAlignment = VerticalAlignment.Bottom;
                    newText2.Foreground = new SolidColorBrush(Color.FromArgb(60, 0, 0, 0));
                    newText2.Text = bodys[i * 33 + 3 + 6].Split('>')[1].Split('<')[0];
                }
                Button newButton = new Button();
                {
                    var margin = newButton.Margin;
                    margin.Left = 0;
                    margin.Top = 20;
                    margin.Right = 10;
                    margin.Bottom = 20;
                    newButton.Margin = margin;
                    newButton.VerticalAlignment = VerticalAlignment.Center;
                    newButton.HorizontalAlignment = HorizontalAlignment.Right;
                    newButton.Foreground = new SolidColorBrush(Color.FromArgb(60, 0, 0, 0));
                }
                newGrid.Children.Add(newImage);
                newGrid.Children.Add(newText1);
                newGrid.Children.Add(newText2);
                newGrid.Children.Add(newButton);
                list.Items.Add(newGrid);
            }

            /*
             <Grid Height="80" Width="350">
                                        <Image Margin="10" Source="http://image.coolapk.com/apk_logo/2017/0723/HeyI-for-146936-o_1blnbn9l610mf1php1r77uo51ge0q-uid-695942.png" HorizontalAlignment="Left" Width="90"/>
                                        <TextBlock Margin="80,20,0,0" Text="名字" VerticalAlignment="Top" Foreground="Black"/>
                                        <tr id="data-row--
                                        <TextBlock Margin="80,0,0,10" VerticalAlignment="Bottom" Text="信息  大小  下载量" Foreground="#AA000000" />

                                        <Button Content="查看" Margin="0,20,10,20" HorizontalAlignment="Right" VerticalAlignment="Center" />
                                    </Grid>
             */
        }
        private async Task LoadNewUpdate(String str)
        {
            String body = str.Substring(str.IndexOf("<!--最近更新应用-->"), str.IndexOf("<!--应用推荐-->") - str.IndexOf("<!--最近更新应用-->"));
            body = Regex.Split(body, @"<div class=""applications"">")[1];
            String[] bodys = Regex.Split(body,@"\n");
            newUpdate = new Grid[12];
            newUpdateImage = new Image[12];
            newUpdateText = new TextBlock[12];
            newUpdateTime = new TextBlock[12];
            for (int i = 0; i < 12; i++)
            {
                try
                {
                    newUpdate[i].Children.Remove(newUpdateImage[i]);
                    newUpdate[i].Children.Remove(newUpdateText[i]);
                    newUpdate[i].Children.Remove(newUpdateTime[i]);
                    updateview.Items.Remove(newUpdate[i]);
                } catch(Exception e) {
                }
                newUpdate[i] = new Grid();
                {
                    newUpdate[i].Height = itemh;
                    newUpdate[i].Width = itemw;
                    newUpdate[i].Tag = bodys[i * 12 + 1].Split('"')[1];
                }
                newUpdateImage[i] = new Image();
                {
                    var margin = newUpdateImage[i].Margin;
                    margin.Left = 10;
                    margin.Top = 10;
                    margin.Right = 10;
                    margin.Bottom = 40;
                    newUpdateImage[i].Margin = margin;
                    try
                    {
                        newUpdateImage[i].Source = new BitmapImage(new Uri(bodys[i * 12 + 4].Split('"')[1], UriKind.RelativeOrAbsolute));
                    }
                    catch (Exception e)
                    {
                    }
                    //                    await WebMessage.DownloadImage(bodys[(i + 1) * 2]);
                }
                newUpdateText[i] = new TextBlock();
                {
                    var margin = newUpdateText[i].Margin;
                    margin.Bottom = 20;
                    newUpdateText[i].Margin = margin;
                    newUpdateText[i].Height = 20;
                    newUpdateText[i].FontSize = 13;
                    newUpdateText[i].TextAlignment = TextAlignment.Center;
                    newUpdateText[i].VerticalAlignment = VerticalAlignment.Bottom;
                    newUpdateText[i].Foreground = new SolidColorBrush(Colors.Black);
                    newUpdateText[i].Text = bodys[i * 12 + 7].Split('>')[1].Split('<' )[0];
                }
                newUpdateTime[i] = new TextBlock();
                {
                    newUpdateTime[i].Height = 20;
                    newUpdateTime[i].FontSize = 10;
                    newUpdateTime[i].TextAlignment = TextAlignment.Center;
                    newUpdateTime[i].VerticalAlignment = VerticalAlignment.Bottom;
                    newUpdateTime[i].Foreground = new SolidColorBrush(Colors.Black);
                    newUpdateTime[i].Text = bodys[i * 12 + 8].Split('>')[1].Split('<')[0];
                }
                newUpdate[i].Children.Add(newUpdateImage[i]);
                newUpdate[i].Children.Add(newUpdateText[i]);
                newUpdate[i].Children.Add(newUpdateTime[i]);
                updateview.Items.Add(newUpdate[i]);
            }
        }
        


        private void updateview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int si = -1;
            si = ((GridView)sender).SelectedIndex;
            if (si != -1) OpenAppPage("https://www.coolapk.com" + ((Grid)((GridView)sender).Items[si]).Tag.ToString());
            ((GridView)sender).SelectedIndex = -1;
        }

        private void gamelist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int si = -1;
            si = ((ListBox)sender).SelectedIndex;
            if (si != -1) OpenAppPage("https://www.coolapk.com/apk/" + ((Grid)gamelist.Items[si]).Tag.ToString());
            ((ListBox)sender).SelectedIndex = -1;
            
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            // OpenAppPage("https://www.coolapk.com/apk/" + ttt.Text);
            _ViewFrame.Navigate(typeof(LoginPage), new LoginPage());
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            _ViewFrame.Visibility = Visibility.Visible;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchPage newPage = new SearchPage();
            searchlink = "https://www.coolapk.com/search?q=" + SearchText.Text;
            ViewFrameS.Navigate(typeof(SearchPage), newPage);
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            ViewFrameS.Visibility = Visibility.Visible;

            var m = SearchText.Margin;
            m.Left = 45;
            SearchText.Margin = m;
            SearchBack.Visibility = Visibility.Visible;
        }

        private void SearchText_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                SearchButton_Click(null, null);
            }
        }

        public static void OpenAppPage(String link)
        {
            AppPage newPage = new AppPage();
            applink = link;
            _ViewFrame.Navigate(typeof(AppPage), newPage);
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            _ViewFrame.Visibility = Visibility.Visible;
        }


        private async void Bar_Click(object sender, RoutedEventArgs e)
        {
            //Hide All
            P1.Visibility = Visibility.Collapsed;
            P2.Visibility = Visibility.Collapsed;
            P3.Visibility = Visibility.Collapsed;
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
            if (s.Equals("1"))
            {
                LeftButton1.Foreground = new SolidColorBrush(CoolColor);
                BottomBar1.Foreground = new SolidColorBrush(CoolColor);
                P1.Visibility = Visibility.Visible;
            }
            else if (s.Equals("2"))
            {
                LeftButton2.Foreground = new SolidColorBrush(CoolColor);
                BottomBar2.Foreground = new SolidColorBrush(CoolColor);
                LoadDeveloper(LoginPage.source1, xl1);
                LoadDeveloper(LoginPage.source2, xl2);
                P2.Visibility = Visibility.Visible;
            }
            else if (s.Equals("3"))
            {
                LeftButton3.Foreground = new SolidColorBrush(CoolColor);
                BottomBar3.Foreground = new SolidColorBrush(CoolColor);
                P3.Visibility = Visibility.Visible;
            }
        }

        private void CatchLoveButton_Click(object sender, RoutedEventArgs e)
        {
            OpenAppPage("https://www.coolapk.com" + ((AppBarButton)sender).Tag.ToString());
        }

        private void Classify_Click(object sender, RoutedEventArgs e)
        {
            SearchPage newPage = new SearchPage();
            SearchText.Text = ((Button)sender).Content.ToString();
            searchlink = "https://www.coolapk.com/search?q=" + SearchText.Text;
            ViewFrameS.Navigate(typeof(SearchPage), newPage);
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            ViewFrameS.Visibility = Visibility.Visible;

            var m = SearchText.Margin;
            m.Left = 45;
            SearchText.Margin = m;
            SearchBack.Visibility = Visibility.Visible;
        }
    }
}
