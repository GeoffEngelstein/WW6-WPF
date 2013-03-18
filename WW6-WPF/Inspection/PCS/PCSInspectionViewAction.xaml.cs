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

namespace WinWam6.Inspection.PCS
{
    /// <summary>
    /// Interaction logic for PCSInspectionViewAction.xaml
    /// </summary>
    
    public partial class PCSInspectionViewAction : UserControl
    {
        public event EventHandler<ActionEventArgs> ActionSelected;
        public event EventHandler<PCSDetailChangeArgs> PCSDetailChange; 

        public PCSInspectionViewAction()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string msg = ((TextBlock)sender).Text;
            ActionSelected(this, new ActionEventArgs(msg));
        }

        protected virtual void OnActionSelected (ActionEventArgs e)
        {
            ActionSelected(this, e);
        }

        private void PCSTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            int curDetailIndex;

            if (PCSTree.SelectedItem.GetType() == typeof(PCSDetail))
            {
                curDetailIndex = ((PCSDetail)PCSTree.SelectedItem).Pack_ID;
            }
            else
            {
                if (PCSTree.SelectedItem.GetType() == typeof(PCSTest))
                {
                    curDetailIndex = ((PCSTest)PCSTree.SelectedItem).Pack_ID;
                }
                else
                {
                    return; // give up
                }
            }
            PCSDetailChange(this, new PCSDetailChangeArgs(curDetailIndex));
        }

    }
}
