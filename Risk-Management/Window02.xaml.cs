using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;

namespace Risk_Management
{
    /// <summary>
    /// Interaction logic for Window02.xaml
    /// </summary>
    public partial class Window02 : Window
    {
        public Window02()
        {
            InitializeComponent();
            SetPropertires();

            ListBoxItem itm = new ListBoxItem();
            itm.Content = "some text";

            listBox.Items.Add(itm);

        }
        void SetPropertires()
        {
            System.Drawing.Image img = Properties.Resources.UNI_LOGO;
            _01_jpg.Source = GetImageSource(img);
            this.Icon = GetImageSource(img);
        }

        private BitmapSource GetImageSource(System.Drawing.Image img)
        {
            BitmapImage bmp = new BitmapImage();

            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Png);
                ms.Position = 0;
                bmp.BeginInit();
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.UriSource = null;
                bmp.StreamSource = ms;
                bmp.EndInit();
            }
            return bmp;
        }
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void listBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {


        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
