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
using System.Data;
using System.Data.Common;
using System.Xml.Linq;
using WinWam6.Utility;

namespace WinWam6.Business
{
    /// <summary>
    /// Interaction logic for BusinessDetailView.xaml
    /// </summary>
    public partial class BusinessDetailView : UserControl, IMainTab
    {
        public Business curBus { get; set; }
        private Business lBus;
        public event EventHandler<MainTabEventArgs> CreateNewTab;
        private BusinessDetailViewAction businessDetailViewAction = new BusinessDetailViewAction();

        public BusinessDetailView()
        {
            lBus = new Business();
            curBus = lBus;
            DataContext = this;
            businessDetailViewAction.ActionSelected += ActionSelected;

            InitializeComponent();

        }


        public BusinessDetailView(string BusinessID)
        {
            lBus = new Business(BusinessID);
            curBus = lBus;
            DataContext = this;
            businessDetailViewAction.ActionSelected += ActionSelected;

            InitializeComponent();
            
            //mapPhysical.ShowMapFromAddress(curBus.PhysicalAddress.LocationString);
        }




        //IMainTab Interface
        public string TabIcon
        {
            get { return "pack://application:,,,/WW6-WPF;component/Images/16/building-icon-16.png"; }
        }
        public string TabCaption
        {
            get { return "Bus "+ this.curBus.Bus_ID; }
        }

        private void ActionSelected(object sender, ActionEventArgs e)
        {
            string s = e.Action;
            if ("Save" == s) curBus.Save();
        }

        public System.Windows.UIElement ActionPaneContent { get { return businessDetailViewAction; } }

        public void TabRequested(object sender, MainTabEventArgs e)
        {
            CreateNewTab(this, e);
        }
    }
}

