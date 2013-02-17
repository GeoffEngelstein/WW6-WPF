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

        public PCSInspectionView()
        {
            pcsInspection = new PCSInspection("AI980003");
            
            this.DataContext = pcsInspection;

            InitializeComponent();
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
        public object TreePaneContent {
            get 
            { 
                return new StackPanel(); 
            } 
        }
        public object ActionPaneContent { get { return new StackPanel(); } }
    }
}
