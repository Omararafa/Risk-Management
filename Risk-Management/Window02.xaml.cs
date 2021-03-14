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
using Smart_Design_Plug_in_Updates;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.ComponentModel;
using System.Data;

namespace Risk_Management
{
    /// <summary>
    /// Interaction logic for Window02.xaml
    /// </summary>
    public partial class Window02 : Window
    {
        public string ExcelLocation = "";
        //Image / Grid Sorter
        #region
        void SetPropertires()
        {
            System.Drawing.Image img = Properties.Resources.UNI_LOGO;
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
        #endregion

        //Variables
        #region
        public List<string> SePublic=new List<string>();
        public List<string> TexPublic = new List<string>();
        public List<Emp> list = new List<Emp>();
        #endregion

        //Main Execution
        public Window02(List<string> SerialList, List<string> TextList)
        {

            InitializeComponent();
            SetPropertires();
            var Data = ClustersData.GetData(SerialList, TextList);
            Grid1.ItemsSource = Data;

            /// <summary>
            /// Inputing Data Of Cells With Serial In App
            /// </summary>
            #region
            Emp[] e = new Emp[30];
            for (int i = 0; i < TextList.Count; i++)
            {
                e[i] = new Emp();
                if (SerialList[i] == "1")
                {
                    e[i].Serial = "1";
                }
                #region

                else if (SerialList[i] == "2")
                {
                    e[i].Serial = "4";
                }
                else if (SerialList[i] == "3")
                {
                    e[i].Serial = "5";
                }
                else if (SerialList[i] == "4")
                {
                    e[i].Serial = "6";
                }
                else if (SerialList[i] == "5")
                {
                    e[i].Serial = "7";
                }
                else if (SerialList[i] == "6")
                {
                    e[i].Serial = "9";
                }
                else if (SerialList[i] == "7")
                {
                    e[i].Serial = "10";
                }
                else if (SerialList[i] == "8")
                {
                    e[i].Serial = "11";
                }
                else if (SerialList[i] == "9")
                {
                    e[i].Serial = "12";
                }
                else if (SerialList[i] == "10")
                {
                    e[i].Serial = "14";
                }
                else if (SerialList[i] == "11")
                {
                    e[i].Serial = "15";
                }
                else if (SerialList[i] == "12")
                {
                    e[i].Serial = "17";
                }
                else if (SerialList[i] == "13")
                {
                    e[i].Serial = "18";
                }
                else if (SerialList[i] == "14")
                {
                    e[i].Serial = "19";
                }
                else if (SerialList[i] == "15")
                {
                    e[i].Serial = "20";
                }
                else if (SerialList[i] == "16")
                {
                    e[i].Serial = "20";
                }
                else if (SerialList[i] == "17")
                {
                    e[i].Serial = "20";
                }
                else if (SerialList[i] == "18")
                {
                    e[i].Serial = "21";
                }
                else if (SerialList[i] == "19")
                {
                    e[i].Serial = "23";
                }
                else if (SerialList[i] == "20")
                {
                    e[i].Serial = "24";
                }
                else if (SerialList[i] == "21")
                {
                    e[i].Serial = "27";
                }
                else if (SerialList[i] == "22")
                {
                    e[i].Serial = "40";
                }
                #endregion
                e[i].Definition = TextList[i];
                list.Add(e[i]);
            }
            #endregion

        }

        //Exporting Methods
        #region
        /// <summary>
        /// Creating The Class Of Objects 
        /// </summary>
        public class Emp
        {
            public string Serial { get; set; }
            public string Definition { get; set; }
        }
        /// <summary>
        /// Method To Convert List Of Objects To DataTable
        /// </summary>
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        #endregion

        //Buttons
        #region
        private void Close(object sender, RoutedEventArgs e)
        {
            Close();

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
            #region Open the pop up          
            XamlLocation x = new XamlLocation();
            x.Height = 170;
            x.Width = 350;
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = x.Width;
            double windowHeight = x.Height;
            x.Left = (screenWidth / 2) - (windowWidth / 2);
            x.Top = (screenHeight / 2) - (windowHeight / 2);
            x.ShowDialog();
            ExcelLocation=x.ExcelLocation;
            #endregion
        }
        private void btnBook_Click(object sender, RoutedEventArgs e)
        {
            Axcel.ExcelUtlity obj = new Axcel.ExcelUtlity();
            DataTable dt = ConvertToDataTable(list);
            obj.WriteDataTableToExcel(dt, "Risk Management","Selected Risks");
        }
        #endregion
    }
}
