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
// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace 酷安_UWP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {


        // 获取系统当前颜色
        Color CoolColor = Color.FromArgb(255, 72, 174, 76);

        public MainPage()
        {
            this.InitializeComponent();
            // 判断是否存在 StatusBar
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                StatusBar statusBar = StatusBar.GetForCurrentView();
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
                
            }

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e) {
            // 绑定到匿名类型上
            //PART_ListViewStateName.ItemsSource = new[] { new { Text = "应用" }, new { Text = "游戏" }};
            
            Windows.UI.Popups.MessageDialog messageDialog = new Windows.UI.Popups.MessageDialog("66666");
            await messageDialog.ShowAsync();
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e) {

        }
    }
}
