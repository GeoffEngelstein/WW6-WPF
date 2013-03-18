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
using WinWam6.Inspection.DEV;
using WinWam6.Utility;

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
            InitalizeTab(fbd);
        }

        public void ShowBusiness(string BusinessID)
        {
            BusinessDetailView fbd = new BusinessDetailView(BusinessID);
            InitalizeTab(fbd);
        }

        public void ShowPCS(string InspID)
        {
            var fbd = new PCSInspectionView(InspID);
            InitalizeTab(fbd);
        }

        public void ShowDEV(string InspID)
        {
            var fbd = new DEVInspectionView(InspID);
            InitalizeTab(fbd);
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
            TableWrapper ltw = new TableWrapper("Inspector");
            Clipboard.SetText(ltw.GenerateClass());
            InspectionReasons inspectors = new InspectionReasons();
            int i = 0;
        }

        private void cmdFindInsp_Click(object sender, RoutedEventArgs e)
        {
            InspectionEdit fie = new InspectionEdit();
            InitalizeTab(fie);
        }

        private void cmdPCS_Click(object sender, RoutedEventArgs e)
        {
            ShowPCS("AA000089");
            //PCSInspectionView fie = new PCSInspectionView();
            //InitalizeTab(fie);
        }

        private void TabRequested(object sender, MainTabEventArgs e)
        {
            switch (e.TabStyle)
            {
                case MainTabEventArgs.TabType.Business:
                    {
                        ShowBusiness(e.Index);
                        break;
                    }

                case MainTabEventArgs.TabType.PCS:
                    {
                        ShowPCS(e.Index);
                        break;
                    }

                case MainTabEventArgs.TabType.DEV:
                    {
                        ShowDEV(e.Index);
                        break;
                    }
            }
        }

        private void cmdDEV_Click(object sender, RoutedEventArgs e)
        {
            DEVInspectionView fie = new DEVInspectionView();
            InitalizeTab(fie);
        }

        private void cmdUnitConversion_Click(object sender, RoutedEventArgs e)
        {
            var unitConversionView = new UnitConversion();
            var unitConversionViewModel = new UnitConversionViewModel();
            InitalizeTab(unitConversionView);

        }

        private void InitalizeTab(UIElement content)
        {
            CloseableTab myTab = new CloseableTab();
            int newTab = MasterTabs.Items.Count + 1;

            myTab.Title = ((IMainTab)content).TabCaption;

            myTab.Picture = new BitmapImage(new Uri(((IMainTab)content).TabIcon));

            AttachEventHandlersToTab((IMainTab)content);

            Grid sp = new Grid();
            sp.SizeChanged += Grid_SizeChanged;
            sp.Children.Add(content);

            myTab.Content = sp;

            MasterTabs.Items.Add(myTab);
            MasterTabs.SelectedIndex = MasterTabs.Items.Count-1;

            InitializeSidePanels();
            //ActionPaneDock.Children.Add(((IMainTab)content).ActionPaneContent);
            //TreePaneDock.Children.Add(((IMainTab)content).TreePaneContent);
        }

        private void AttachEventHandlersToTab(IMainTab mainTab)
        {
            mainTab.CreateNewTab += TabRequested;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Maximized;
            DashboardView dbv = new DashboardView();
            InitalizeTab(dbv);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InitializeSidePanels();
        }
        private void InitializeSidePanels() {

            ActionPaneDock.Children.Clear();

            
            //Set up Action Panel
            if (MasterTabs.SelectedIndex > -1)
            {
                TabItem myTab = (TabItem)MasterTabs.Items.GetItemAt(MasterTabs.SelectedIndex);
                Grid myGrid = (Grid)myTab.Content;
                System.Windows.UIElement content = new System.Windows.UIElement();
                content = myGrid.Children[0];
                ActionPaneDock.Children.Add(((IMainTab)content).ActionPaneContent);
            }
        }


    }
}
