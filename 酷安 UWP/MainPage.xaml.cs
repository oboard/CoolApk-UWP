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
        int itemw = 110, itemh = 90;
        
        public static Frame _ViewFrame, _ViewFrameS;
        public static TextBlock _User_Name;
        public static Image _User_Face;
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
                Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
            }
            _ViewFrame = ViewFrame;
            _ViewFrameS = ViewFrameS;
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
            //网络信息
            await LoadNewUpdate(await Web.GetHttp("https://www.coolapk.com/"));
            await LoadHotApp(await Web.GetHttp("https://www.coolapk.com/apk/recommend"));
            await LoadDeveloperApp(await Web.GetHttp("https://www.coolapk.com/apk/developer"));
            await LoadHotGame(await Web.GetHttp("https://www.coolapk.com/game/"));
            await LoadHotGame(await Web.GetHttp("https://www.coolapk.com/game?p=2"));
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
                    var margin = newI.Margin;
                    margin.Left = 10;
                    margin.Top = 10;
                    margin.Right = 10;
                    margin.Bottom = 40;
                    newI.Margin = margin;
                    try
                    {
                        newI.Source = new BitmapImage(new Uri(Regex.Split(bod, "src=")[i + 1].Split('"')[1], UriKind.RelativeOrAbsolute));
                    }
                    catch (Exception)
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
                Grid newGrid = new Grid
                {
                    Height = 60,
                    Tag = bodys[i * 15 + 6].Split('"')[1].Split('/')[2]
                };
                Image newImage = new Image();
                {
                    var margin = new Thickness(20, 10, 0, 10);
                    newImage.Margin = margin;
                    newImage.Width = 40;
                    newImage.HorizontalAlignment = HorizontalAlignment.Left;
                    try
                    {
                        newImage.Source = new BitmapImage(new Uri(bodys[i * 15 + 6 + 3].Split('"')[3], UriKind.RelativeOrAbsolute));
                    }
                    catch (Exception)
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
                    newText1.Text = Web.ReplaceHtml(bodys[i * 15 + 6 + 5].Split('>')[1].Split('<')[0]);
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
                    newText2.Text = Web.ReplaceHtml(bodys[i * 15 + 6 + 7].Split('>')[1].Split('<')[0]);
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

            hotview.Items.Clear();

            for (int i = 0; i < 20; i++)
            {
                try {
                    Grid hotApp = new Grid
                    {
                        Height = itemh,
                        Width = itemw,
                        Tag = bodys[i * 15 + 5].Split('"')[1]
                    };
                    Image hotAppImage = new Image();
                    {
                        var margin = new Thickness(10, 10, 10, 40);
                        hotAppImage.Margin = margin;
                        try
                        {
                            hotAppImage.Source = new BitmapImage(new Uri(bodys[i * 15 + 5 + 3].Split('"')[3], UriKind.RelativeOrAbsolute));
                        }
                        catch (Exception)
                        {
                        }
                        //                    await WebMessage.DownloadImage(bodys[(i + 1) * 2]);
                    }
                    TextBlock hotAppText = new TextBlock
                    {
                        Margin = new Thickness(0, 0, 0, 20),
                        Height = 20,
                        FontSize = 13,
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        Foreground = new SolidColorBrush(Colors.Black),
                        Text = bodys[i * 15 + 5 + 5].Split('>')[1].Split('<')[0]
                    };
                    TextBlock hotAppName = new TextBlock
                    {
                        Height = 20,
                        FontSize = 10,
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        Foreground = new SolidColorBrush(Colors.Black),
                        Text = bodys[i * 15 + 5 + 7].Split('>')[1].Split('<')[0].Split(' ')[0]
                    };
                    hotApp.Children.Add(hotAppImage);
                    hotApp.Children.Add(hotAppText);
                    hotApp.Children.Add(hotAppName);
                    hotview.Items.Add(hotApp);
                }
                catch (Exception)
                {

                }
            }

        }

        private async Task LoadDeveloperApp(String str)
        {
            String body = Regex.Split(str, @"<div class=""left_nav"">")[1];
            String[] bodys = Regex.Split(body, @"\n");

            developerview.Items.Clear();

            for (int i = 0; i < 20; i++)
            {
                Grid developerApp = new Grid
                {
                    Height = itemh,
                    Width = itemw,
                    Tag = bodys[i * 15 + 4].Split('"')[1]
                };
                Image developerAppImage = new Image();
                {
                    developerAppImage.Margin = new Thickness(10, 10, 10, 40);
                    try
                    {
                        developerAppImage.Source = new BitmapImage(new Uri(bodys[i * 15 + 4 + 3].Split('"')[3], UriKind.RelativeOrAbsolute));
                    }
                    catch (Exception)
                    {
                    }
                    //                    await WebMessage.DownloadImage(bodys[(i + 1) * 2]);
                }
            TextBlock developerAppText = new TextBlock
                {
                    Margin = new Thickness(0, 0, 0, 20),
                    Height = 20,
                    FontSize = 13,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    Foreground = new SolidColorBrush(Colors.Black),
                    Text = bodys[i * 15 + 4 + 5].Split('>')[1].Split('<')[0]
            };
            TextBlock developerAppName = new TextBlock
            {
                Height = 20,
                FontSize = 8,
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Foreground = new SolidColorBrush(Colors.Black),
                Text = bodys[i * 15 + 4 + 9].Split('>')[1].Split('<')[0].Replace("开发者:", "")
            };
                developerApp.Children.Add(developerAppImage);
                developerApp.Children.Add(developerAppText);
                developerApp.Children.Add(developerAppName);
                developerview.Items.Add(developerApp);
            }
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
                    catch (Exception)
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

            updateview.Items.Clear();

            for (int i = 0; i < 12; i++)
            {
                Grid newUpdate = new Grid();
                {
                    newUpdate.Height = itemh;
                    newUpdate.Width = itemw;
                    newUpdate.Tag = bodys[i * 12 + 1].Split('"')[1];
                }
                Image newUpdateImage = new Image();
                {
                    var margin = newUpdateImage.Margin;
                    margin.Left = 10;
                    margin.Top = 10;
                    margin.Right = 10;
                    margin.Bottom = 40;
                    newUpdateImage.Margin = margin;
                    try
                    {
                        newUpdateImage.Source = new BitmapImage(new Uri(bodys[i * 12 + 4].Split('"')[1], UriKind.RelativeOrAbsolute));
                    }
                    catch (Exception)
                    {
                    }
                    //                    await WebMessage.DownloadImage(bodys[(i + 1) * 2]);
                }
                TextBlock newUpdateText = new TextBlock();
                {
                    var margin = newUpdateText.Margin;
                    margin.Bottom = 20;
                    newUpdateText.Margin = margin;
                    newUpdateText.Height = 20;
                    newUpdateText.FontSize = 13;
                    newUpdateText.TextAlignment = TextAlignment.Center;
                    newUpdateText.VerticalAlignment = VerticalAlignment.Bottom;
                    newUpdateText.Foreground = new SolidColorBrush(Colors.Black);
                    newUpdateText.Text = bodys[i * 12 + 7].Split('>')[1].Split('<' )[0];
                }
                TextBlock newUpdateTime = new TextBlock();
                {
                    newUpdateTime.Height = 20;
                    newUpdateTime.FontSize = 10;
                    newUpdateTime.TextAlignment = TextAlignment.Center;
                    newUpdateTime.VerticalAlignment = VerticalAlignment.Bottom;
                    newUpdateTime.Foreground = new SolidColorBrush(Colors.Black);
                    newUpdateTime.Text = bodys[i * 12 + 8].Split('>')[1].Split('<')[0];
                }
                newUpdate.Children.Add(newUpdateImage);
                newUpdate.Children.Add(newUpdateText);
                newUpdate.Children.Add(newUpdateTime);
                updateview.Items.Add(newUpdate);
            }
        }
        


        private void Updateview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int si = -1;
            si = ((GridView)sender).SelectedIndex;
            if (si != -1) OpenAppPage("https://www.coolapk.com" + ((Grid)((GridView)sender).Items[si]).Tag.ToString());
            ((GridView)sender).SelectedIndex = -1;
        }

        private void Gamelist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int si = -1;
            si = ((ListBox)sender).SelectedIndex;
            if (si != -1) OpenAppPage("https://www.coolapk.com/apk/" + ((Grid)gamelist.Items[si]).Tag.ToString());
            ((ListBox)sender).SelectedIndex = -1;
            
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchPage newPage = new SearchPage();
            ViewFrameS.Navigate(typeof(SearchPage), newPage);
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            ViewFrameS.Visibility = Visibility.Visible;
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
    }
}
