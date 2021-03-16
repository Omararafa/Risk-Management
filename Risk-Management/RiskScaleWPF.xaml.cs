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
    public partial class RiskScaleWPF: Window
    {
        public double min = 0.16;
        public double max= 0.39;
        public string ExcelLocation = "";
        public RiskScaleWPF()
        {
            InitializeComponent();
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Max = Max.Text;
            Properties.Settings.Default.Min = Min.Text;
            Properties.Settings.Default.Meduim = Meduim.Text;
            Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Max = "0.39";
            Properties.Settings.Default.Min = "0.16";
            Properties.Settings.Default.Meduim = "0.16";
            Close();
        }
    }
}
