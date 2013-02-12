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
        public ObservableCollection<InspectionBase> inspectionList;

        public InspectionEdit()
        {
            inspectionList = GetAllInspectionHeaders();
            InitializeComponent();
            tvInspEdit.ItemsSource = inspectionList;
        }

        private ObservableCollection<InspectionBase> GetAllInspectionHeaders()
        {
            var inspList = new ObservableCollection<InspectionBase>();
            InspectionBase inspectionBase;

            DbDataReader dr = WWD.GetReader("select insp_id, insptype from insph order by insp_date");

            while (dr.Read())
            {
                switch (dr[1].ToString())
                {
                    case "P": 
                        inspectionBase = new PackageInspection();
                        inspectionBase.Load(dr[0].ToString());
                        break;

                    case "D":
                        inspectionBase = new DeviceInspection();
                        inspectionBase.Load(dr[0].ToString());
                        break;

                    case "U":
                        inspectionBase = new UPCInspection();
                        inspectionBase.Load(dr[0].ToString());
                        break;

                    case "Q":
                        inspectionBase = new QSTInspection();
                        inspectionBase.Load(dr[0].ToString());
                        break;

                    case "R":
                        inspectionBase = new QSTDInspection();
                        inspectionBase.Load(dr[0].ToString());
                        break;

                    default:
                        inspectionBase = new PackageInspection();
                        inspectionBase.Load(dr[0].ToString());
                        break;
                }
                
                inspList.Add(inspectionBase);

            }
            dr.Dispose();

            return inspList;

        }

    }
}
