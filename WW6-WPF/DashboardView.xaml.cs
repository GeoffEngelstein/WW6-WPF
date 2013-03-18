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
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl, IMainTab
    {
        public event EventHandler<MainTabEventArgs> CreateNewTab;

        public DashboardView()
        {
            InitializeComponent();
        }
        
                //IMainTab Interface
        public string TabIcon
        {
            get { return "pack://application:,,,/WW6-WPF;component/Images/16/building-icon-16.png"; }
        }
        public string TabCaption
        {
            get { return "Dashboard"; }
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
    }
}
