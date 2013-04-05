using System;
using System.Windows;
using System.Data.Common;
using System.Data;

namespace WinWam6.Utility
{
    /// <summary>
    /// Interaction logic for SQLViewer.xaml
    /// </summary>
    public partial class SQLViewer : IMainTab
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
        public UIElement ActionPaneContent { get { return sqlViewAction; } }

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
            if (txtQuery.Text != string.Empty)
            {
                try
                {
                    DbDataReader dr = WWD.GetReader(txtQuery.Text);
                    var dt = new DataTable();
                    dt.Load(dr);
                    grdQuery.ItemsSource = dt.AsDataView();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message,"Query Error");
                }
            }
        }

        public void TabRequested(object sender, MainTabEventArgs e)
        {
            CreateNewTab(this, e);
        }
    }
}
