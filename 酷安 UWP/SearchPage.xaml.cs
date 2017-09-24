using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace 酷安_UWP
{
    public sealed partial class SearchPage : Page
    {
        public SearchPage()
        {
            this.InitializeComponent();

        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.Tag = MainPage.searchlink;
            try
            {
                SearchLoad(await WebMessage.GetMessage(Tag.ToString()));
            }
            catch(Exception ex)
            {

            }
        }
        private void SearchLoad(String str)
        {
            String body = Regex.Split(str, @"<div class=""left_nav"">")[1];
            body = Regex.Split(body, @"<div class=""panel-footer ex-card-footer text-center"">")[0];
            //&nbsp;处理
            body = body.Replace("&nbsp;", " ");
            String[] bodylist = Regex.Split(body, @"<a href=""");
            String[] bodys = Regex.Split(body, @"\n");
            for (int i = 0; i < bodylist.Length - 1; i++)
            {
                Grid newGrid = new Grid();
                {
                    newGrid.Height = 80;
                    newGrid.Tag = bodys[i * 15 + 5].Split('"')[1];
                }
                Image newImage = new Image();
                {
                    var margin = newImage.Margin;
                    margin.Left = 20;
                    margin.Top = 20;
                    margin.Right = 0;
                    margin.Bottom = 20;
                    newImage.Margin = margin;
                    newImage.Width = 40;
                    newImage.HorizontalAlignment = HorizontalAlignment.Left;
                    try
                    {
                        newImage.Source = new BitmapImage(new Uri(bodys[i * 15 + 5 + 3].Split('"')[3], UriKind.RelativeOrAbsolute));
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
                    newText1.Text = bodys[i * 15 + 5 + 5].Split('>')[1].Split('<')[0];
                }
                TextBlock newText2 = new TextBlock();
                {
                    var margin = newText2.Margin;
                    margin.Left = 80;
                    margin.Top = 30;
                    newText2.Margin = margin;
                    newText2.HorizontalAlignment = HorizontalAlignment.Left;
                    newText2.VerticalAlignment = VerticalAlignment.Top;
                    newText2.Foreground = new SolidColorBrush(Color.FromArgb(60,0,0,0));
                    newText2.TextWrapping = TextWrapping.Wrap;
                    newText2.Text = bodys[i * 15 + 5 + 6].Split('>')[1].Split('<')[0];
                }
                TextBlock newText3 = new TextBlock();
                {
                    var margin = newText3.Margin;
                    margin.Left = 80;
                    margin.Top = 50;
                    newText3.Margin = margin;
                    newText3.HorizontalAlignment = HorizontalAlignment.Left;
                    newText3.VerticalAlignment = VerticalAlignment.Top;
                    newText3.Foreground = new SolidColorBrush(Color.FromArgb(60, 0, 0, 0));
                    newText3.TextWrapping = TextWrapping.Wrap;
                    newText3.Text = bodys[i * 15 + 5 + 7].Split('>')[1].Split('<')[0];
                }
                newGrid.Children.Add(newImage);
                newGrid.Children.Add(newText1);
                newGrid.Children.Add(newText2);
                newGrid.Children.Add(newText3);
                SearchList.Items.Add(newGrid);
            }

        }

        private void SearchList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SearchList.SelectedIndex == -1) return;
            MainPage.OpenAppPage("https://www.coolapk.com" + ((Grid)SearchList.Items[SearchList.SelectedIndex]).Tag.ToString());
            SearchList.SelectedIndex = -1;
        }
    }

}


/*
            < Grid Height = "80" >


                     < Image HorizontalAlignment = "Left" Margin = "20,20,0,20" Width = "40" />


                          < TextBlock HorizontalAlignment = "Left" Margin = "80,10,0,0" TextWrapping = "Wrap" Text = "应用名" VerticalAlignment = "Top" />


                                   < TextBlock HorizontalAlignment = "Left" Margin = "80,30,0,0" TextWrapping = "Wrap" Text = "分数 大小" VerticalAlignment = "Top" Foreground = "#99000000" />


                                              < TextBlock HorizontalAlignment = "Left" Margin = "80,50,0,0" TextWrapping = "Wrap" Text = "下载量" VerticalAlignment = "Top" Foreground = "#99000000" />


                                                     </ Grid >
                                                     */