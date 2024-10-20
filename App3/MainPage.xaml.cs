using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private int _countTabs = 1;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void btnadd_Click(object sender, RoutedEventArgs e)
        {
            // mainframe.Navigate(typeof(home));
            _countTabs++;
            PivotItem newtab = new PivotItem
            {
                Header = "Tab" + _countTabs
            };
            WebView newweb = new WebView
            {
                Source = new Uri("http://www.bing.com")
            };
            newtab.Content = newweb;
            TabPivot.Items.Add(newtab);
            TabPivot.SelectedItem = newtab;
        }

        private void btnrev_Click(object sender, RoutedEventArgs e)
        {

            UpdatePreview();
        }

        private async void UpdatePreview()
        {
            thumbnail.Children.Clear();
            foreach(PivotItem item in TabPivot.Items)
            {
                if(item.Content is WebView webpage)
                {
                    var stream = new InMemoryRandomAccessStream();
                    await webpage.CapturePreviewToStreamAsync(stream);

                    BitmapImage bitmapImage = new BitmapImage();
                    stream.Seek(0);
                    await bitmapImage.SetSourceAsync(stream);

                    Image capturedImage = new Image
                    {
                        Source = bitmapImage,
                        Width = 100,
                        Height = 60,
                        Margin = new Thickness(7)
                    };
                    thumbnail.Children.Add(capturedImage);

                }
            }
        }
    }
}
