using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace WinWam6.Utility
{
    /// <summary>
    /// Interaction logic for SQLViewerAction.xaml
    /// </summary>
    public partial class SQLViewerAction
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
