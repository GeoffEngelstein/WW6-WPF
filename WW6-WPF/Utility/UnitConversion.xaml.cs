using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WinWam6.Utility
{
    /// <summary>
    /// Interaction logic for UnitConversion.xaml
    /// </summary>
    public partial class UnitConversion : UserControl, IMainTab
    {
        public event EventHandler<MainTabEventArgs> CreateNewTab;
        public UnitConversionViewModel ucvm { get; set; }
        private UnitConversionViewModel lucvm = new UnitConversionViewModel();

        public UnitConversion()
        {
            ucvm = lucvm;
            InitializeComponent();
            DataContext = this;
        }

    
       
        //IMainTab Interface
        public string TabIcon
        {
            get { return "pack://application:,,,/WW6-WPF;component/Images/16/view-refresh-6.png"; }
        }
        public string TabCaption
        {
            get { return "Unit Conversion"; }
        }
        public System.Windows.UIElement TreePaneContent
        {
            get
            {
                return new StackPanel();
            }
        }
        public System.Windows.UIElement ActionPaneContent { get { return new StackPanel(); } }

        public void TabRequested(object sender, MainTabEventArgs e)
        {
            CreateNewTab(this, e);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LstSourceUnits.ItemsSource = this.ucvm.SourceUnits(((ListBox)sender).SelectedItem.ToString());
        }
    }
}
