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
using Xceed.Wpf.Toolkit;

namespace WinWam6
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class CustomFieldEntry : UserControl
    {
        private CCustomFields m_custom;

        public CustomFieldEntry()
        {
            InitializeComponent();
        }

        public CCustomFields CustomFields
        {
            get
            {
                return m_custom;
            }

            set
            {
                m_custom = value;
                InitializeControl();
                PopulateControl();
            }
        }

        public void AddNewRow(int Row, string Caption)
        {
            System.Windows.Controls.TextBlock tb = new TextBlock();
            tb.SetValue(Grid.RowProperty,Row);
            tb.SetValue(Grid.ColumnProperty,0);
            tb.Text = Caption;

            System.Windows.Controls.TextBox txtbx = new TextBox();
            txtbx.SetValue(Grid.RowProperty,Row);
            txtbx.SetValue(Grid.ColumnProperty,1);
            
            ControlGrid.Children.Add(tb);
            ControlGrid.Children.Add(txtbx);
        }

        //Sets up the controls for the custom control
        private void InitializeControl()
        {
            int i = 0;

            foreach (CCustomLine cl in this.CustomFields.CustomLines)
            {
                //Add new row to the grid
                RowDefinition rd = new RowDefinition();
                rd.MinHeight = 30;
                rd.MaxHeight = 30;
                ControlGrid.RowDefinitions.Add(rd);
                
                System.Windows.Controls.TextBlock tb = new TextBlock();
                tb.SetValue(Grid.RowProperty, i);
                tb.SetValue(Grid.ColumnProperty, 0);
                tb.TextAlignment = TextAlignment.Right;
                tb.Margin = new Thickness(3);
                tb.Text = cl.Caption;
                ControlGrid.Children.Add(tb);

                 switch (cl.DataType)
                {
                    case CCustomLine.CustDataType.cdText:

                        if (cl.IsList)
                        {
                            System.Windows.Controls.ComboBox cbx = new ComboBox();
                            cbx.SetValue(Grid.RowProperty, i);
                            cbx.SetValue(Grid.ColumnProperty, 1);
                            cbx.Margin = new Thickness(3);
                            cbx.Tag = cl.Caption;
                            cbx.SelectionChanged += CustomDataChanged;


                            foreach (string sl in cl.PickList)
                            {
                                cbx.Items.Add(sl);
                            }
                            ControlGrid.Children.Add(cbx);
                        }
                        else
                        {
                            System.Windows.Controls.TextBox tbx = new TextBox();
                            tbx.SetValue(Grid.RowProperty, i);
                            tbx.SetValue(Grid.ColumnProperty, 1);
                            tbx.Margin = new Thickness(3);
                            tbx.Tag = cl.Caption;
                            tbx.Text = cl.Caption;
                            tbx.TextChanged += CustomDataChanged;
                            ControlGrid.Children.Add(tbx);
                        }
                        break;

                    case CCustomLine.CustDataType.cdDate:

                        System.Windows.Controls.DatePicker dtp = new DatePicker();
                        dtp.SetValue(Grid.RowProperty, i);
                        dtp.SetValue(Grid.ColumnProperty, 1);
                        dtp.Margin = new Thickness(3);
                        dtp.Tag = cl.Caption;
                        dtp.SelectedDateChanged += CustomDataChanged;
                        ControlGrid.Children.Add(dtp);
                        break;

                    case CCustomLine.CustDataType.cdInteger:

                        Xceed.Wpf.Toolkit.IntegerUpDown iud = new IntegerUpDown();
                        iud.SetValue(Grid.RowProperty, i);
                        iud.SetValue(Grid.ColumnProperty, 1);
                        iud.Margin = new Thickness(3);
                        iud.Tag = cl.Caption;
                        iud.ValueChanged += CustomDataChanged;
                        ControlGrid.Children.Add(iud);
                        break;

                    case CCustomLine.CustDataType.cdNumber:

                        Xceed.Wpf.Toolkit.CalculatorUpDown dud = new CalculatorUpDown();
                        dud.SetValue(Grid.RowProperty, i);
                        dud.SetValue(Grid.ColumnProperty, 1);
                        dud.Margin = new Thickness(3);
                        dud.Tag = cl.Caption;
                        dud.ValueChanged += CustomDataChanged;
                        ControlGrid.Children.Add(dud);
                        break;

                    case CCustomLine.CustDataType.cdCurrency:

                        Xceed.Wpf.Toolkit.CalculatorUpDown cud = new CalculatorUpDown();
                        cud.SetValue(Grid.RowProperty, i);
                        cud.SetValue(Grid.ColumnProperty, 1);
                        cud.Margin = new Thickness(3);
                        cud.Tag = cl.Caption;
                        cud.FormatString = "C2";
                        cud.ValueChanged += CustomDataChanged;
                        ControlGrid.Children.Add(cud);
                        break;

                    case CCustomLine.CustDataType.cdBoolean:

                        System.Windows.Controls.CheckBox chkb = new CheckBox();
                        chkb.SetValue(Grid.RowProperty, i);
                        chkb.SetValue(Grid.ColumnProperty, 1);
                        chkb.Margin = new Thickness(3); //Do we need this?
                        chkb.Tag = cl.Caption;
                        chkb.Checked += CustomDataChanged;
                        ControlGrid.Children.Add(chkb);
                        break;

                    case CCustomLine.CustDataType.cdBusinessLink:
                    case CCustomLine.CustDataType.cdInspectorLink:
                         
                        System.Windows.Controls.TextBlock LinkTB = new TextBlock();
                        LinkTB.SetValue(Grid.RowProperty, i);
                        LinkTB.SetValue(Grid.ColumnProperty, 1);
                        LinkTB.Margin = new Thickness(3); //Do we need this?
                        LinkTB.Tag = cl.Caption;
                        ControlGrid.Children.Add(LinkTB);
                        break;                         
                         
                }



                i++;
            }
        }

                //Copies the actual data into the controls
        private void PopulateControl()
        {
            //Iterate through the child controls of the grid looking for controls with a TAG set

            foreach (UIElement uie in ControlGrid.Children)
            {
                //First get the tag of the sender so we know which caption to find
                FrameworkElement fe = (FrameworkElement)uie;

                if (fe.Tag != null)
                {
                    string caption = fe.Tag.ToString();

                    if (caption != "")
                    {
                        //We found an item that is tied to a caption. Now find the matching caption and copy the data
                        CCustomLine cl = m_custom[caption];

                        switch (cl.DataType)
                        {
                            case CCustomLine.CustDataType.cdBoolean:
                                ((CheckBox)uie).IsChecked = bool.Parse(cl.CustomData);
                                break;

                            case CCustomLine.CustDataType.cdText:
                                if (cl.IsList)
                                {
                                    ((ComboBox)uie).Text = cl.CustomData;
                                }
                                else
                                {
                                    ((TextBox)uie).Text = cl.CustomData;
                                }
                                break;

                            case CCustomLine.CustDataType.cdDate:
                                ((DatePicker)uie).Text = cl.CustomData;
                                break;

                            case CCustomLine.CustDataType.cdInteger:
                                ((IntegerUpDown)uie).Text = cl.CustomData;
                                break;

                            case CCustomLine.CustDataType.cdCurrency:
                            case CCustomLine.CustDataType.cdNumber:
                                ((CalculatorUpDown)uie).Text = cl.CustomData;
                                break;

                            case CCustomLine.CustDataType.cdBusinessLink:
                            case CCustomLine.CustDataType.cdInspectorLink:
                                ((TextBlock)uie).Text = cl.CustomData;
                                break;

                            default:

                                //should never be here
                                break;
                        }
                    }


                }
            }
        }

        // All data entry controls get wired to this event handler
        private void CustomDataChanged(object sender, RoutedEventArgs e)
        {
            string newData;

            //First get the tag of the sender so we know which caption to find
            FrameworkElement fe = (FrameworkElement)sender;
            string caption = fe.Tag.ToString();
            
            //Find the right line. Should probably be a dictionary or use LINQ?
            CCustomLine cl = this.CustomFields[caption];

            switch (cl.DataType)
            {
                case CCustomLine.CustDataType.cdBoolean:
                    newData = ((CheckBox)sender).IsChecked.ToString();
                    break;

                case CCustomLine.CustDataType.cdText:
                    if (cl.IsList)
                    {
                        newData = ((ComboBox)sender).SelectedItem.ToString();
                    }
                    else
                    {
                        newData = ((TextBox)sender).Text;
                    }
                    break;

                case CCustomLine.CustDataType.cdDate:
                    newData = ((DatePicker)sender).Text;
                    break;

                case CCustomLine.CustDataType.cdInteger:
                    newData = ((IntegerUpDown)sender).Text;
                    break;

                case CCustomLine.CustDataType.cdCurrency:
                case CCustomLine.CustDataType.cdNumber:
                    newData = ((CalculatorUpDown)sender).Text;
                    break;

                case CCustomLine.CustDataType.cdBusinessLink:
                case CCustomLine.CustDataType.cdInspectorLink: 
                    newData = ((TextBlock)sender).Text;
                    break;

                default:
                    newData = "";
                    //should never be here
                    break;
            }

            cl.CustomData = newData;
                            
        }
    }
}
