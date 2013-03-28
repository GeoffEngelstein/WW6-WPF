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

namespace WinWam6.Business
{
    /// <summary>
    /// Interaction logic for BusinessDetailViewAction.xaml
    /// </summary>
    public partial class BusinessDetailViewAction : UserControl
    {
        public event EventHandler<ActionEventArgs> ActionSelected;

        public BusinessDetailViewAction()
        {
            InitializeComponent();
        }

        protected virtual void OnActionSelected(ActionEventArgs e)
        {
            ActionSelected(this, e);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            string msg = (sender as Button).Tag.ToString();
            ActionSelected(this, new ActionEventArgs(msg));
        }
    }
}
