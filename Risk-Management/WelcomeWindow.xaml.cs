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

namespace Risk_Management
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        public string ExcelLocation = "";
        public string Trade = "";
        public WelcomeWindow()
        {
            InitializeComponent();
            SetPropertires();
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

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            No.IsChecked = false;
            Next.IsEnabled = true;
        }

        private void No_Checked(object sender, RoutedEventArgs e)
        {
            Yes.IsChecked = false;
            Next.IsEnabled = false;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            Close();
            #region Questionnare window
            Trades x = new Trades();
            x.Height = 310;
            x.Width = 650;
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = x.Width;
            double windowHeight = x.Height;
            x.Left = (screenWidth / 2) - (windowWidth / 2);
            x.Top = (screenHeight / 2) - (windowHeight / 2);
            x.ShowDialog();
            Trade = x.Trade;
            ExcelLocation = x.ExcelLocation;
            #endregion
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Trade = "";
            Close();
        }
    }
}
