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

namespace WinWam6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CConversionLinq m_conv;
 
        public MainWindow()
        {
            WWD.OpenDatabase();
            m_conv = new CConversionLinq();
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

           DataTable BusTable = WWD.GetTableSchema("InspH");

           TableWrapper ltw = WWD.GetTableWrapper("InspH");

           Clipboard.SetText(ltw.GenerateClass());

           TableWrapper ltw2 = WWD.GetTableWrapper("Business");
           TableWrapper ltw3 = WWD.GetTableWrapper("InspH");
           ltw3.Fields["Insp_ID"].Value = "AA100599";
           ltw3.Fields["InspType"].Value = "D";
           ltw3.Fields["CreateDate"].Value = DateTime.Now;
           ltw3.Fields["AmtPaid"].Value = 150;
            string u = ltw3.Insert();
            Console.Write(u);
            u = ltw3.Update();
            Console.WriteLine(u);
            ltw2.Fields["Bus_ID"].Value = "000009";
            ltw2.Load();
            ltw2.Fields["Addr1"].Value = "59 Totten Drive";
            u = ltw2.Update();

           grdProperties.AutoGenerateColumns = true;    
           grdProperties.ItemsSource = BusTable.DefaultView;

            foreach (DataRow d in BusTable.Rows)
            {
                    
            }

            //lstUnits.Items.Clear();
            //List<string> tableList = WWD.TableList();
            //foreach (string s in tableList)
            //{
            //    lstUnits.Items.Add(s);
            //}

            //WinWam6.DataSets.DataBusiness dbs = WWD.GetBusiness("AA");
            
            
            //DataAdapter da = WWD.GetDataAdapter("Business");
            
       
            //lstDestUnits.Items.Clear();

            //foreach (DataBusiness.BusinessRow r in dbs.Business.Rows)
            //{
            //    lstDestUnits.Items.Add(r.Bus_ID + " " + r.Bus_Name);
            //}

            
        }

        private void cmdCategories_Click(object sender, RoutedEventArgs e)
        {
            BusinessDetail fbd = new BusinessDetail();
            //fbd.Show();
            

            //lstCategories.Items.Clear();

            //lstCategories.ItemsSource = m_conv.Categories;
            /*
            IEnumerable<string> s = m_conv.Categories;

            foreach (string t in s)
            {
                lstCategories.Items.Add(t);
            }
             */

        }

        private void lstUnits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lstCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //lstUnits.Items.Clear();
            //lstDestUnits.Items.Clear();

            //IEnumerable<string> s = m_conv.Units(lstCategories.SelectedItem.ToString());

            //foreach (string t in s)
            //{
            //    lstUnits.Items.Add(t);
            //    lstDestUnits.Items.Add(t);
            //}
        }

        private void cmdConvert_Click(object sender, RoutedEventArgs e)
        {
            fWWMain fwm = new fWWMain();
            fwm.Show();

            //lblOutput.Content = m_conv.Convert(lstCategories.SelectedItem.ToString(), lstUnits.SelectedItem.ToString(), lstDestUnits.SelectedItem.ToString(), 1);
        }
    }
}
