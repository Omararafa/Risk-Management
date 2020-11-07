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



            /*var col = Grid1.Columns[0] as DataGridTextColumn;
            var colone = Grid1.Columns[1] as DataGridTextColumn;
            var style = new Style(typeof(TextBlock));
            style.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));
            style.Setters.Add(new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center));
            col.ElementStyle = style;
            colone.ElementStyle = style;*/





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
        private void Grid1_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Definition")
            {
                var col = e.Column as DataGridTextColumn;
                col.Width = 500;
                var style = new Style(typeof(TextBlock));
                style.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));
                style.Setters.Add(new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center));

                col.ElementStyle = style;
            }
            else
            {
                var col = e.Column as DataGridTextColumn;
                col.Width = 50;
                var style = new Style(typeof(TextBlock));
                style.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));
                style.Setters.Add(new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center));
                style.Setters.Add(new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center));

                col.ElementStyle = style;
            }
        }



        private void Close(object sender, RoutedEventArgs e)
        {
            Close();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
            #region Open the pop up          
            XamlLocation x = new XamlLocation();
            x.Height = 160;
            x.Width = 350;
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = x.Width;
            double windowHeight = x.Height;
            x.Left = (screenWidth / 2) - (windowWidth / 2);
            x.Top = (screenHeight / 2) - (windowHeight / 2);
            x.ShowDialog();
            #endregion
        }
    }
}
