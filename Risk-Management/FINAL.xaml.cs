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
using System.Collections.ObjectModel;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Data;
using System.ComponentModel;

namespace Risk_Management
{
    /// <summary>
    /// Interaction logic for Questionnare.xaml
    /// </summary>
    public partial class FINAL : Window
    {
        public string ExcelLocation = "";
        public string Trade = "";
        public Transaction TX;
        public Document DOC;
        public List<ElementId> test=new List<ElementId>();
        public UIDocument UIDOC;
        public IDictionary<string, ICollection<Element>> ElementsGroupss;
        List<Data> Data = new List<Data>();
        public FINAL(IDictionary<string, ICollection<Element>> ElementsGroups, List<string> PercentagesCost, List<string> PercentagesTime,Transaction tx,Document doc,UIDocument uidoc)
        {

            InitializeComponent();
            var data = Risk_Management.Data.GetData(ElementsGroups, PercentagesCost, PercentagesTime);
            foreach(var t in data)
            {
                Data.Add(t);
            }
            GridData.ItemsSource = Risk_Management.Data.GetData(ElementsGroups, PercentagesCost, PercentagesTime);
            TX = tx;
            DOC = doc;
            UIDOC = uidoc;
            ElementsGroupss = ElementsGroups;
        }

        private void GridData_Loaded(object sender, RoutedEventArgs e)
        {
            GridData.Columns[1].Width =200;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach(Data item in GridData.ItemsSource)
            {
                if (item.Checked == true)
                {
                    foreach(var it in ElementsGroupss[item.ItemName])
                    {
                        test.Add(it.Id);
                    }
                    
                }
            }
            if (test.Count == 0)
            {
                MessageBox.Show("Nothing have been selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                UIDOC.Selection.SetElementIds(test);
            }

        }
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Axcel.ExcelUtlity obj = new Axcel.ExcelUtlity();
            DataTable dt = ConvertToDataTable(Data);
            obj.WriteDataTableToExcelTwo(dt);
        }
    }
}
