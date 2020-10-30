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
    /// Interaction logic for Questionnare.xaml
    /// </summary>
    public partial class Questionnare : Window
    {
        public Questionnare()
        {
            InitializeComponent();
            SetPropertires();

        }

        void SetPropertires()
        {
            System.Drawing.Image img = Properties.Resources.UNI_LOGO;
            _01_jpg.Source = GetImageSource(img);
            this.Icon= GetImageSource(img);
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

        private void Grid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Next(object sender, RoutedEventArgs e)
        {
            foreach(var x in Table.RowGroups)
            {

            }

            List<string> resultsList = new List<string>();
            TableRow x = One as TableRow;
            foreach (var tableCell in x.Cells)
            {
                // May want to start another list here in case there are multiple blocks.
                List<string> blockContent = new List<string>();
                foreach (var block in tableCell.Blocks.OfType<Paragraph>())
                {
                    // Probably want to start another list here to which to add in the next loop.
                    List<string> inlineContent = new List<string>();
                    foreach (var inline in block.Inlines)
                    {
                        // Implement whatever in here depending the type of inline,
                        // such as Span, Run, InlineUIContainer, etc.
                        // I just assumed it was text.
                        inlineContent.Add(new TextRange(inline.ContentStart, inline.ContentEnd).Text);
                    }
                    blockContent.Add(string.Join("", inlineContent.ToArray()));
                }
            }
                /*
                #region Open the pop up          
                Window02 x = new Window02();
                x.Height = 555;
                x.Width = 1200;
                double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
                double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
                double windowWidth = x.Width;
                double windowHeight = x.Height;
                x.Left = (screenWidth / 2) - (windowWidth / 2);
                x.Top = (screenHeight / 2) - (windowHeight / 2);
                x.ShowDialog();
                #endregion
                */

            }
    }
}
