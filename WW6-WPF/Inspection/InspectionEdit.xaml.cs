﻿using System;
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
using WinWam6.Inspection.PCS;
using WinWam6.Inspection.DEV;
using WinWam6.Inspection.UPC;
using WinWam6.Inspection.QST;
using WinWam6.Inspection.QSTD;


namespace WinWam6
{
    /// <summary>
    /// Interaction logic for InspectionEdit.xaml
    /// </summary>
    public partial class InspectionEdit : UserControl, IMainTab 
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
                        inspectionBase = new PCSInspection();
                        inspectionBase.Load(dr[0].ToString());
                        break;

                    case "D":
                        inspectionBase = new DEVInspection();
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
                        inspectionBase = new PCSInspection();
                        inspectionBase.Load(dr[0].ToString());
                        break;
                }
                
                inspList.Add(inspectionBase);

            }
            dr.Dispose();

            return inspList;

        }

        public string TabIcon
        {
            get { return "pack://application:,,,/WW6-WPF;component/Images/16/edit-find-4.png"; }
        }
        public string TabCaption
        {
            get { return "Search Inspections"; }
        }
        public object TreePaneContent { get { return null; } }
        public object ActionPaneContent { get { return null; } }

    }
}