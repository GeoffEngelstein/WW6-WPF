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

namespace WinWam6.Inspection.DEV
{
    /// <summary>
    /// Interaction logic for DEVInspectionView.xaml
    /// </summary>
        public partial class DEVInspectionView : UserControl, IMainTab
        {
            private DEVInspection devInspection;
            private DEVInspectionViewAction devAction = new DEVInspectionViewAction();
            public event EventHandler<MainTabEventArgs> CreateNewTab;

            public DEVInspectionView(): this("") {}

            public DEVInspectionView(string InspID)
            {
                if ("" == InspID)
                {
                    devInspection = new DEVInspection();
                }
                else
                {
                    devInspection = new DEVInspection(InspID);
                }
                

                devAction.DEVTree.ItemsSource = this.devInspection.DevDetails;

                this.DataContext = devInspection;

                InitializeComponent();

                this.InspHeader.CreateNewTab += TabRequested;

            }

            
            //IMainTab Interface
            public string TabIcon
            {
                get { return "pack://application:,,,/WW6-WPF;component/Images/16/justice-balance-icon-16.png"; }
            }
            public string TabCaption
            {
                get { return "DEV " + devInspection.Insp_ID; }
            }
            public System.Windows.UIElement ActionPaneContent { get { return devAction; } }

            public void TabRequested(object sender, MainTabEventArgs e)
            {
                CreateNewTab(this, e);
            }
        }
}

