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
using System.Windows.Shapes;
using Microsoft.Windows.Controls.Ribbon;
using WinWam6.Business;

namespace WinWam6
{
    /// <summary>
    /// Interaction logic for WWMain.xaml
    /// </summary>
    public partial class WWMain : RibbonWindow
    {
        public WWMain()
        {
            WWD.OpenDatabase();



            InitializeComponent();


  

            // Insert code required on object creation below this point.
        }

        private void cmdBusiness_Click(object sender, RoutedEventArgs e)
        {
            BusinessDetailView fbd = new BusinessDetailView();
            TabGrid1.Children.Add(fbd);
            fbd.Height = TabGrid1.ActualHeight;
            fbd.Width = TabGrid1.ActualWidth;
        }

        private void cmdInspector_Click(object sender, RoutedEventArgs e)
        {
            CloseableTab myTab = new CloseableTab();
            int newTab = MasterTabs.Items.Count + 1;
            
            myTab.Title = "Tab " + newTab.ToString();

            if (newTab % 2 > 0)
            {
                myTab.Picture = new BitmapImage(new Uri("pack://application:,,,/WW6-WPF;component/Images/16/emblem-package.png"));
            }
            else
            {
                
            }
            MasterTabs.Items.Add(myTab) ;
        }

        private void TabGrid1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (UserControl uc in TabGrid1.Children)
            {
                uc.Height = TabGrid1.ActualHeight;
                uc.Width = TabGrid1.ActualWidth;
            }
        }

        private void StackPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            StackPanel sp = (StackPanel)sender;
            foreach (UserControl uc in sp.Children)
            {
                uc.Height = TabGrid1.ActualHeight;
                uc.Width = TabGrid1.ActualWidth;
            }
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Grid grid = (Grid)sender;
            foreach (UserControl uc in grid.Children)
            {
                uc.Height = grid.ActualHeight;
                uc.Width = grid.ActualWidth;
            }
        }

        private void cmdCalculator_Click(object sender, RoutedEventArgs e)
        {
            TableWrapper ltw = new TableWrapper("UPCD");
            Clipboard.SetText(ltw.GenerateClass());
        }

        private void cmdFindInsp_Click(object sender, RoutedEventArgs e)
        {
            InspectionEdit fie = new InspectionEdit();
            InitalizeTab(fie);
        }

        private void cmdPCS_Click(object sender, RoutedEventArgs e)
        {
            PCSInspectionView fie = new PCSInspectionView();
            InitalizeTab(fie);
        }

        private void InitalizeTab(UIElement content)
        {
            CloseableTab myTab = new CloseableTab();
            int newTab = MasterTabs.Items.Count + 1;

            myTab.Title = ((IMainTab)content).TabCaption;

            myTab.Picture = new BitmapImage(new Uri(((IMainTab)content).TabIcon));

            Grid sp = new Grid();
            sp.SizeChanged += Grid_SizeChanged;
            sp.Children.Add(content);

            myTab.Content = sp;

            MasterTabs.Items.Add(myTab);
        }
    }
}
