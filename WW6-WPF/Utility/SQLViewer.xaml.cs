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
using System.Data.Common;
using System.Data;

namespace WinWam6.Utility
{
    /// <summary>
    /// Interaction logic for SQLViewer.xaml
    /// </summary>
    public partial class SQLViewer : UserControl, IMainTab
    {
        public SQLViewerAction sqlViewAction = new SQLViewerAction();
        public event EventHandler<MainTabEventArgs> CreateNewTab;

        public SQLViewer()
        {
            InitializeComponent();
            sqlViewAction.ActionSelected += ActionSelected;
        }

        //IMainTab Interface
        public string TabIcon
        {
            get { return "pack://application:,,,/WW6-WPF;component/Images/16/emblem-question.png"; }
        }
        public string TabCaption
        {
            get { return "Query"; }
        }
        public System.Windows.UIElement ActionPaneContent { get { return sqlViewAction; } }

        private void ActionSelected(object sender, ActionEventArgs e)
        {
            string s = e.Action;

            switch (s)
            {
                case "Execute Query":
                    {
                        BindGridToQuery();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void BindGridToQuery()
        {
            DbDataReader dr = WWD.GetReader(txtQuery.Text);
            DataTable dt = new DataTable();
            dt.Load(dr);
            grdQuery.ItemsSource = dt.AsDataView();
        }

        public void TabRequested(object sender, MainTabEventArgs e)
        {
            CreateNewTab(this, e);
        }
    }
}
