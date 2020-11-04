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
using Smart_Design_Plug_in_Updates;

namespace Risk_Management
{
    /// <summary>
    /// Interaction logic for Window02.xaml
    /// </summary>
    public partial class Window02 : Window
    {
        public List<string> SePublic=new List<string>();
        public List<string> TexPublic = new List<string>();
        public Window02(List<string> SerialList, List<string> TextList)
        {
            InitializeComponent();
            SetPropertires();
            var Data = ClustersData.GetData(SerialList, TextList);
            Grid1.ItemsSource = Data;
            




            //Grid1.ItemsSource = ExistingData.Existing(Data);
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
