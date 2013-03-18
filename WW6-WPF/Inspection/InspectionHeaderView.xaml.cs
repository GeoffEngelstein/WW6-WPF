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

namespace WinWam6
{
    /// <summary>
    /// Interaction logic for InspectionHeaderView.xaml
    /// </summary>
    public partial class InspectionHeaderView : UserControl
    {
        public event EventHandler<MainTabEventArgs> CreateNewTab;

        private List<Inspector> inspectors;

        public InspectionHeaderView()
        {
            InitializeComponent();
        }

        private void cmdViewBusiness_Click(object sender, RoutedEventArgs e)
        {
            CreateNewTab(this, new MainTabEventArgs(MainTabEventArgs.TabType.Business, txtBusiness.Text.Trim()));
        }

        private void cmdSearchBusiness_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
        }
    }
}
