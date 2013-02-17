using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.ComponentModel;

namespace WinWam6.Inspection.PCS
{
    public class PCSTest : INotifyPropertyChanged
    {
        private TableWrapper lObj;

        public Single Gross
        {
            get { return Single.Parse(lObj["Gross"].ToString()); }
            set { lObj["Gross"] = value; NotifyPropertyChanged("Gross"); }
        }

        public String Insp_ID
        {
            get { return lObj["Insp_ID"].ToString(); }
            set { lObj["Insp_ID"] = value; NotifyPropertyChanged("Insp_ID"); }
        }

        public Single MAV
        {
            get { return Single.Parse(lObj["MAV"].ToString()); }
            set { lObj["MAV"] = value; NotifyPropertyChanged("MAV"); }
        }

        public Double MWeight
        {
            get { return Double.Parse(lObj["MWeight"].ToString()); }
            set { lObj["MWeight"] = value; NotifyPropertyChanged("MWeight"); }
        }

        public Int16 Pack_ID
        {
            get { return Int16.Parse(lObj["Pack_ID"].ToString()); }
            set { lObj["Pack_ID"] = value; NotifyPropertyChanged("Pack_ID"); }
        }

        public Double PWeight
        {
            get { return Double.Parse(lObj["PWeight"].ToString()); }
            set { lObj["PWeight"] = value; NotifyPropertyChanged("PWeight"); }
        }

        public Single Tare
        {
            get { return Single.Parse(lObj["Tare"].ToString()); }
            set { lObj["Tare"] = value; NotifyPropertyChanged("Tare"); }
        }

        public Int16 Test
        {
            get { return Int16.Parse(lObj["Test"].ToString()); }
            set { lObj["Test"] = value; NotifyPropertyChanged("Test"); }
        }

        public Single Volume
        {
            get { return Single.Parse(lObj["Volume"].ToString()); }
            set { lObj["Volume"] = value; NotifyPropertyChanged("Volume"); }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public PCSTest()
        {
            lObj = new TableWrapper("PackTest");
        }

        public PCSTest(string insp_ID, int pack_ID, int test_id)
        {
            lObj = new TableWrapper("PackTest");
            Load(insp_ID, pack_ID, test_id);
        }

        public void Load(string insp_ID, int pack_ID, int test_id)
        {
            lObj["Insp_ID"] = insp_ID;
            lObj["Pack_ID"] = pack_ID;
            lObj["Test"] = test_id;
            lObj.Load();
        }

    }
}
