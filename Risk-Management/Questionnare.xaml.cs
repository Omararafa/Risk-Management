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
        public string ExcelLocation = "";
        public string Trade = "";
        public Questionnare(string trade)
        {
            Trade = trade;
            if (!(string.IsNullOrEmpty(Trade)))
            {
                InitializeComponent();
                SetPropertires();
            }


        }

        void SetPropertires()
        {
            System.Drawing.Image img = Properties.Resources.UNI_LOGO;
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
            //Entire Row List
            //-----------------------------------------------------------------------------------------------------------------------------
            List<string> RowContent = new List<string>();
            List<string> SerialList = new List<string>();
            List<string> TextList = new List<string>();
            //-----------------------------------------------------------------------------------------------------------------------------
            //Getting SerialList TextList
            foreach (var RowGroup in Table.RowGroups)
            {

                int count = 0;
                
                foreach (var Row in RowGroup.Rows)
                {
                    bool? check = false;
                    if (count > 1)
                    {
                        if (RowContent.Count == 2)
                        {
                            SerialList.Add(RowContent[0]);
                            TextList.Add(RowContent[1]);
                            RowContent.Clear();
                        }
                        else if (RowContent.Count != 2)
                        {
                            RowContent.Clear();
                        }
                        //if (CheckBox01.IsChecked == true)
                        //{
                        int CountTwo = 0;
                        foreach (var Cell in Row.Cells)
                        {
                            // May want to start another list here in case there are multiple blocks.
                            //BlockContent List "Paragraph"
                            List<string> blockContent = new List<string>();
                            //---------------------------
                            foreach (var block in Cell.Blocks.OfType<Paragraph>())
                            {
                                // Probably want to start another list here to which to add in the next loop.
                                //InlineContent List "Text"
                                List<string> inlineContent = new List<string>();
                                //---------------------------
                                #region Checkbox
                                if (block.Inlines.ElementAt<Inline>(CountTwo) is Run)
                                {
                                    var Dummy = "run";
                                }
                                else
                                {
                                    var UIList = block.Inlines.OfType<InlineUIContainer>();
                                    var UI = UIList.First();
                                    var Case = UI.Child;
                                    if (Case is CheckBox)
                                    {
                                        var checkbox = Case as CheckBox;
                                        check = checkbox.IsChecked;
                                    }
                                    else
                                    {
                                        check = false;
                                    }
                                    //var Check = testt.IsChecked;
                                }
                                #endregion

                            }


                        }

                        if (check == true)
                        {
                            foreach (var Cell in Row.Cells)
                            {
                                // May want to start another list here in case there are multiple blocks.
                                //BlockContent List "Paragraph"
                                List<string> blockContent = new List<string>();
                                //---------------------------
                                foreach (var block in Cell.Blocks.OfType<Paragraph>())
                                {
                                    // Probably want to start another list here to which to add in the next loop.
                                    //InlineContent List "Text"
                                    List<string> inlineContent = new List<string>();
                                    //---------------------------

                                    foreach (var inline in block.Inlines.OfType<Run>())
                                    {
                                        // Implement whatever in here depending the type of inline,
                                        // such as Span, Run, InlineUIContainer, etc.
                                        // I just assumed it was text.
                                        inlineContent.Add(new TextRange(inline.ContentStart, inline.ContentEnd).Text);
                                    }
                                    blockContent.Add(string.Join("", inlineContent.ToArray()));

                                }
                                if (string.Join("", blockContent.ToArray()) != "")
                                {
                                    RowContent.Add(string.Join("", blockContent.ToArray()));
                                }

                            }
                        }

                        //}
                    }

 
                    count = count+1;
                }
            }
            //Adding Last Row Values
            if (RowContent.Count == 2)
            {
                SerialList.Add(RowContent[0]);
                TextList.Add(RowContent[1]);
                RowContent.Clear();
            }

            Close();
            #region Open the pop up          
            Window02 x = new Window02(SerialList,TextList,Trade);
             x.Height = 515;
             x.Width = 600;
             double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
             double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
             double windowWidth = x.Width;
             double windowHeight = x.Height;
             x.Left = (screenWidth / 2) - (windowWidth / 2);
             x.Top = (screenHeight / 2) - (windowHeight / 2);
             x.ShowDialog();
            ExcelLocation = x.ExcelLocation;
            //Trade = "";
            #endregion

             

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Trade = "";
        }
        private void HighCheck_Checked(object sender, RoutedEventArgs e)
        {
            MeduimCheck.IsEnabled = false;
            LowCheck.IsEnabled = false;
        }
        private void HighCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            MeduimCheck.IsEnabled = true;
            LowCheck.IsEnabled = true;
        }
        private void MeduimCheck_Checked(object sender, RoutedEventArgs e)
        {
            HighCheck.IsEnabled = false;
            LowCheck.IsEnabled = false;
        }
        private void MeduimCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            HighCheck.IsEnabled = true;
            LowCheck.IsEnabled = true;
        }
        private void LowCheck_Checked(object sender, RoutedEventArgs e)
        {
            MeduimCheck.IsEnabled = false;
            HighCheck.IsEnabled = false;
        }
        private void LowCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            MeduimCheck.IsEnabled = true;
            HighCheck.IsEnabled = true;
        }
 
        }
}
