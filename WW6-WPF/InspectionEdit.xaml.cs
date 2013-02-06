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
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;

namespace WinWam6
{
    /// <summary>
    /// Interaction logic for InspectionEdit.xaml
    /// </summary>
    public partial class InspectionEdit : UserControl
    {
        public ObservableCollection<InspectionHeader> inspectionList;

        public InspectionEdit()
        {
            inspectionList = GetAllInspectionHeaders();
            InitializeComponent();
            tvInspEdit.ItemsSource = inspectionList;
        }

        private ObservableCollection<InspectionHeader> GetAllInspectionHeaders()
        {
            var inspList = new ObservableCollection<InspectionHeader>();
            InspectionHeader inspHeader;

            DbDataReader dr = WWD.GetReader("select insp_id, insptype from insph order by insp_date");

            while (dr.Read())
            {
                inspHeader = new InspectionHeader(dr[0].ToString(), dr[1].ToString());
                inspList.Add(inspHeader);
            }
            dr.Dispose();

            return inspList;

        }
    }
}
