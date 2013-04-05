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
using WinWam6.Inspection.PCS;

namespace WinWam6
{
    /// <summary>
    /// Interaction logic for PCSInspectionView.xaml
    /// </summary>
    public partial class PCSInspectionView : UserControl, IMainTab
    {
        private PCSInspection pcsInspection;
        private PCSInspectionViewAction pcsAction = new PCSInspectionViewAction();
        private int currentDetailIndex=0;
        public event EventHandler<MainTabEventArgs> CreateNewTab;

        public PCSInspectionView(string InspID)
        {
            if ("" == InspID)
            {
                pcsInspection = new PCSInspection();
            }
            else
            {
                pcsInspection = new PCSInspection(InspID);
            }

            pcsAction.PCSTree.ItemsSource = this.pcsInspection.PCSDetails;
            pcsAction.ActionSelected += ActionSelected;
            pcsAction.PCSDetailChange += DetailChanged;

            

            this.DataContext = pcsInspection;

            InitializeComponent();
            grdDetail.DataContext = this.ActiveDetail;
            this.InspHeader.CreateNewTab += TabRequested;

        }

        public PCSDetail ActiveDetail
        {
            get {
                if (0 == pcsInspection.PCSDetails.Count)
                {
                    pcsInspection.PCSDetails.Add(new PCSDetail());
                }
                return pcsInspection.PCSDetails[currentDetailIndex]; 
            }
        }

        //IMainTab Interface
        public string TabIcon
        {
            get { return "pack://application:,,,/WW6-WPF;component/Images/16/emblem-package.png"; }
        }
        public string TabCaption
        {
           get { return "PCS " + pcsInspection.Insp_ID; }
        }
        public System.Windows.UIElement ActionPaneContent { get { return pcsAction; } }

        private void ActionSelected(object sender, ActionEventArgs e)
        {
            switch (e.Action)
            {
                case "Save Inspection":
                    {
                        pcsInspection.Save();
                        break;
                    }
                default:
                {
                    break;
                }

            }
        }

        private void DetailChanged(object sender, PCSDetailChangeArgs e)
        {
            if (currentDetailIndex != e.DetailIndex - 1)
            {
                currentDetailIndex = e.DetailIndex - 1;
                grdDetail.DataContext = null;
                grdDetail.DataContext = this.ActiveDetail;
            }
        }

        public void TabRequested(object sender, MainTabEventArgs e)
        {
            CreateNewTab(this, e);
        }

        private void cmdCommodityUp_Click(object sender, RoutedEventArgs e)
        {
            if (currentDetailIndex < pcsInspection.PCSDetails.Count - 1)
            {
                ChangeCommodity(currentDetailIndex + 1);
            }
        }
        
        private void cmdCommodityDown_Click(object sender, RoutedEventArgs e)
        {
            if (currentDetailIndex > 0)
            {
                ChangeCommodity(currentDetailIndex - 1);
            }
        }

        private void ChangeCommodity(int newIndex)
        {
            if (newIndex > -1 && newIndex < pcsInspection.PCSDetails.Count)
            {
                DetailChanged(this, new PCSDetailChangeArgs(newIndex+1));
            }
        }

        private void cmdGetTare_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdGetGrossWt_Click(object sender, RoutedEventArgs e)
        {

        }
    
    }
}
