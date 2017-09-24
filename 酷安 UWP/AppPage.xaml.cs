﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace 酷安_UWP
{
    public sealed partial class AppPage : Page
    {
        String jstr = "", vmstr = "", dstr = "", vstr, mstr, nstr, iurl, vtstr, rstr, pstr;

        public AppPage()
        {
            this.InitializeComponent();
            
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.Tag = MainPage.applink;
            LaunchAppViewLoad(await WebMessage.GetMessage(Tag.ToString()));
        }
        private async void LaunchAppViewLoad(String str)
        {
            try { jstr = WebMessage.ReplaceHtml(Regex.Split(Regex.Split(Regex.Split(str, "应用简介</p>")[1], @"<div class=""apk_left_title_info"">")[1], "</div>")[0].Trim()); } catch(Exception e) { }
            try { vmstr = WebMessage.ReplaceHtml(Regex.Split(Regex.Split(str, @"<p class=""apk_left_title_info"">")[2], "</p>")[0].Replace("<br />", "").Replace("<br/>", "").Trim()); } catch (Exception e) { }
            try { dstr = WebMessage.ReplaceHtml(Regex.Split(Regex.Split(str, @"<p class=""apk_left_title_info"">")[1], "</p>")[0].Replace("<br />", "").Replace("<br/>", "").Trim()); } catch (Exception e) { }
            vstr = Regex.Split(str, @"<p class=""detail_app_title"">")[1].Split('>')[1].Split('<')[0].Trim();
            mstr = Regex.Split(str, @"<p class=""apk_topba_message"">")[1].Split('<')[0].Trim().Replace("\n", "").Replace(" ", "");
            nstr = Regex.Split(str, @"<p class=""detail_app_title"">")[1].Split('<')[0].Trim();
            iurl = Regex.Split(str, @"<div class=""apk_topbar"">")[1].Split('"')[1].Trim();
            vtstr = Regex.Split(str, "更新时间：")[1].Split('<')[0].Trim();
            rstr = Regex.Split(str, @"<p class=""rank_num"">")[1].Split('<')[0].Trim();
            pstr = Regex.Split(str, @"<p class=""apk_rank_p1"">")[1].Split('<')[0].Trim();

            AppIconImage.Source = new BitmapImage(new Uri(iurl, UriKind.RelativeOrAbsolute));
            AppTitleText.Text = nstr;
            AppVTText.Text = vtstr;
            AppV2Text.Text = vstr;
            AppVText.Text = vstr;
            AppMText.Text = Regex.Split(mstr, "/")[2] + " " + Regex.Split(mstr, "/")[3] + " " + rstr + "分";
            AppXText.Text = Regex.Split(mstr, "/")[1] + " · " + Regex.Split(mstr, "/")[0];

            if (Regex.Split(str, @"<p class=""apk_left_title_info"">").Length > 3)
            {
                //当应用有点评
                AppVMText.Text = vmstr;
                AppDText.Text = dstr;
                DPanel.Visibility = Visibility.Visible;
            }
            else
            {
               //当应用无点评的时候（小编要是一个一个全好好点评我就不用加判断了嘛！）
                AppVMText.Text = dstr;
                AppDText.Text = "";
            }
            if (dstr.Contains("更新时间") && dstr.Contains("ROM") && dstr.Contains("名称")) UPanel.Visibility = Visibility.Collapsed;




            //加载截图！
            String images = Regex.Split(Regex.Split(str, @"<div class=""ex-screenshot-thumb-carousel"">")[1], "</div>")[0];
            String[] imagearray = Regex.Split(images, "<img");
            for (int i = 0; i < imagearray.Length - 1; i++)
            {
                String imageurl = imagearray[i + 1].Split('"')[1];
                Image newImage = new Image();
                newImage.Height = 100;
                //获得图片
                newImage.Source = new BitmapImage(new Uri(imageurl, UriKind.RelativeOrAbsolute));
                //添加到缩略视图
                ScreenShotView.Items.Add(newImage);
            }
            images = Regex.Split(Regex.Split(str, @"<div class=""carousel-inner"">")[1], @"<a class=""left carousel-control""")[0];
            imagearray = Regex.Split(images, "<img");
            for (int i = 0; i < imagearray.Length - 1; i++)
            {
                String imageurl = imagearray[i + 1].Split('"')[1];
                Image newImage = new Image();
                //获得图片
                newImage.Source = new BitmapImage(new Uri(imageurl, UriKind.RelativeOrAbsolute));
                //添加到视图
                ScreenShotFlipView.Items.Add(newImage);
            }

            //还有简介（丧心病狂啊）
            AppJText.Text = jstr;

            //评分。。
            AppRText.Text = rstr;
            AppPText.Text = pstr;
            //星星
            double rdob = Double.Parse(rstr);
            if (rdob > 4.5) {

            }
            else if (rdob > 3.0)
            {
                star5.Symbol = Symbol.OutlineStar;
            }
            else if (rdob > 4.0)
            {
                star4.Symbol = Symbol.OutlineStar;
                star5.Symbol = Symbol.OutlineStar;
            }
            else if (rdob > 3.0)
            {
                star3.Symbol = Symbol.OutlineStar;
                star4.Symbol = Symbol.OutlineStar;
                star5.Symbol = Symbol.OutlineStar;
            }
            else if (rdob < 2.0)
            {
                //没有评分那么差的应用吧233
                star2.Symbol = Symbol.OutlineStar;
                star3.Symbol = Symbol.OutlineStar;
                star4.Symbol = Symbol.OutlineStar;
                star5.Symbol = Symbol.OutlineStar;
            }

            
            //获取开发者
            String knstr = WebMessage.ReplaceHtml(Regex.Split(Regex.Split(str, "开发者名称：")[1] ,"</p>")[0]);
            try { 
                AppKNText.Text = knstr;
                AppKImage.Source = new BitmapImage(new Uri(await WebMessage.GetCoolApkUserFace(knstr), UriKind.RelativeOrAbsolute));
            } catch (Exception e)
            {
                KPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void CopyM_Click(object sender, RoutedEventArgs e)
        {

            if (sender is MenuFlyoutItem selectedItem)
            {
                DataPackage dp = new DataPackage();
                // copy 
                if (selectedItem.Tag.ToString() == "0")
                {
                    dp.SetText(jstr);
                }
                else if (selectedItem.Tag.ToString() == "1")
                {
                    dp.SetText(dstr);
                }
                else if (selectedItem.Tag.ToString() == "2")
                {
                    dp.SetText(Regex.Split(Tag.ToString(), "/")[4]);
                }
                else if (selectedItem.Tag.ToString() == "3")
                {
                    dp.SetText(vstr);
                }
                else if (selectedItem.Tag.ToString() == "24")
                {
                    dp.SetText(vmstr);
                }
                Clipboard.SetContent(dp);
            }
        }
        private void GotoUri_Click(object sender, RoutedEventArgs e)
        {
            string uriToLaunch = MainPage.applink;
            var uri = new Uri(uriToLaunch);
            async void LaunchWithEdge()
            {
                var options = new Windows.System.LauncherOptions();
                options.TargetApplicationPackageFamilyName = "Microsoft.MicrosoftEdge_8wekyb3d8bbwe";
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage.App_Back();
        }

        private void ScreenShotView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ScreenShotFlipView.Visibility = Visibility.Visible;
            CloseFlip.Visibility = Visibility.Visible;
            ScreenShotView.SelectedIndex = -1;
        }

        private void CloseFlip_Click(object sender, RoutedEventArgs e)
        {
            ScreenShotFlipView.Visibility = Visibility.Collapsed;
            CloseFlip.Visibility = Visibility.Collapsed;
        }
    }
}
