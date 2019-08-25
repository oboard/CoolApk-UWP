using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using 酷安_UWP.CoolApk;

namespace 酷安_UWP
{

    public sealed partial class FeedPage : Page
    {
        public Int32 uid;
        public FeedPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //将传过来的数据 类型转换一下
            uid = (Int32)e.Parameter;
            LoadFeeds(uid);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }

        public async Task LoadFeeds(dynamic uid)
        {

            //绑定一个列表
            ObservableCollection<DataItem> FeedsCollection = new ObservableCollection<DataItem>();
            listView.ItemsSource = FeedsCollection;

            FeedRoot feedRoot = await CoolApkSDK.GetFeedListByID(uid, 1, "");
            foreach (DataItem i in feedRoot.data)
            {
                FeedsCollection.Add(i);
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage.App_Back();
        }

    }
}
