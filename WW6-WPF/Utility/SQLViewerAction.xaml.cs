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

namespace WinWam6.Utility
{
    /// <summary>
    /// Interaction logic for SQLViewerAction.xaml
    /// </summary>
    public partial class SQLViewerAction : UserControl
    {

        public event EventHandler<ActionEventArgs> ActionSelected;
        
        public SQLViewerAction()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string msg = ((TextBlock)sender).Text;
            if (ActionSelected != null) ActionSelected(this, new ActionEventArgs(msg));
        }

        protected virtual void OnActionSelected (ActionEventArgs e)
        {
            ActionSelected(this, e);
        }
    }
}
