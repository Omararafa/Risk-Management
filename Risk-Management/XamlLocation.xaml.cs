using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for XamlLocation.xaml
    /// </summary>
    public partial class XamlLocation : Window
    {
        public string ExcelLocation = "";
        public XamlLocation()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog.ShowDialog();
            ExcelLocation= openFileDialog.FileName;
            Location.Text=openFileDialog.FileName;
            
            
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            ExcelLocation = Location.Text;
            Close();
        }
    }
}
