using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WinWam6.Inspection.PCS
{
    public class PCSDetail : INotifyPropertyChanged
    {
        private TableWrapper lObj;
        private bool pcsTestLoaded = false;
        private ObservableCollection<PCSTest> pcsTests;


        public ObservableCollection<PCSTest> PCSTests
        {
            get
            {
                if (!pcsTestLoaded)
                {
                    LoadPackTests();
                }
                return PCSTests;
            }
        }
        public Double AvgErr
        {
            get { return Double.Parse(lObj["AvgErr"].ToString()); }
            set { lObj["AvgErr"] = value; NotifyPropertyChanged("AvgErr"); }
        }

        public String Brand
        {
            get { return lObj["Brand"].ToString(); }
            set { lObj["Brand"] = value; NotifyPropertyChanged("Brand"); }
        }

        public String CommClass
        {
            get { return lObj["CommClass"].ToString(); }
            set { lObj["CommClass"] = value; NotifyPropertyChanged("CommClass"); }
        }

        public String Commodity
        {
            get { return lObj["Commodity"].ToString(); }
            set { lObj["Commodity"] = value; NotifyPropertyChanged("Commodity"); }
        }

        public Decimal CostPer
        {
            get { return Decimal.Parse(lObj["CostPer"].ToString()); }
            set { lObj["CostPer"] = value; NotifyPropertyChanged("CostPer"); }
        }

        public String DAd1
        {
            get { return lObj["DAd1"].ToString(); }
            set { lObj["DAd1"] = value; NotifyPropertyChanged("DAd1"); }
        }

        public String DAd2
        {
            get { return lObj["DAd2"].ToString(); }
            set { lObj["DAd2"] = value; NotifyPropertyChanged("DAd2"); }
        }

        public String DCity
        {
            get { return lObj["DCity"].ToString(); }
            set { lObj["DCity"] = value; NotifyPropertyChanged("DCity"); }
        }

        public String DFax
        {
            get { return lObj["DFax"].ToString(); }
            set { lObj["DFax"] = value; NotifyPropertyChanged("DFax"); }
        }

        public String DName
        {
            get { return lObj["DName"].ToString(); }
            set { lObj["DName"] = value; NotifyPropertyChanged("DName"); }
        }

        public String DState
        {
            get { return lObj["DState"].ToString(); }
            set { lObj["DState"] = value; NotifyPropertyChanged("DState"); }
        }

        public String DZip
        {
            get { return lObj["DZip"].ToString(); }
            set { lObj["DZip"] = value; NotifyPropertyChanged("DZip"); }
        }

        public bool ForceActTare
        {
            get { return bool.Parse(lObj["ForceActTare"].ToString()); }
            set { lObj["ForceActTare"] = value; NotifyPropertyChanged("ForceActTare"); }
        }

        public bool Glass
        {
            get { return bool.Parse(lObj["Glass"].ToString()); }
            set { lObj["Glass"] = value; NotifyPropertyChanged("Glass"); }
        }

        public Single GrayPct
        {
            get { return Single.Parse(lObj["GrayPct"].ToString()); }
            set { lObj["GrayPct"] = value; NotifyPropertyChanged("GrayPct"); }
        }

        public String Insp_ID
        {
            get { return lObj["Insp_ID"].ToString(); }
            set { lObj["Insp_ID"] = value; NotifyPropertyChanged("Insp_ID"); }
        }

        public String InspCat
        {
            get { return lObj["InspCat"].ToString(); }
            set { lObj["InspCat"] = value; NotifyPropertyChanged("InspCat"); }
        }

        public String LotCode
        {
            get { return lObj["LotCode"].ToString(); }
            set { lObj["LotCode"] = value; NotifyPropertyChanged("LotCode"); }
        }

        public Int16 LotSize
        {
            get { return Int16.Parse(lObj["LotSize"].ToString()); }
            set { lObj["LotSize"] = value; NotifyPropertyChanged("LotSize"); }
        }

        public String MAVType
        {
            get { return lObj["MAVType"].ToString(); }
            set { lObj["MAVType"] = value; NotifyPropertyChanged("MAVType"); }
        }

        public Double NetWt
        {
            get { return Double.Parse(lObj["NetWt"].ToString()); }
            set { lObj["NetWt"] = value; NotifyPropertyChanged("NetWt"); }
        }

        public String Notes
        {
            get { return lObj["Notes"].ToString(); }
            set { lObj["Notes"] = value; NotifyPropertyChanged("Notes"); }
        }

        public String NUnit
        {
            get { return lObj["NUnit"].ToString(); }
            set { lObj["NUnit"] = value; NotifyPropertyChanged("NUnit"); }
        }

        public Int16 Pack_ID
        {
            get { return Int16.Parse(lObj["Pack_ID"].ToString()); }
            set { lObj["Pack_ID"] = value; NotifyPropertyChanged("Pack_ID"); }
        }

        public Int16 PackType
        {
            get { return Int16.Parse(lObj["PackType"].ToString()); }
            set { lObj["PackType"] = value; NotifyPropertyChanged("PackType"); }
        }

        public Int16 Res
        {
            get { return Int16.Parse(lObj["Res"].ToString()); }
            set { lObj["Res"] = value; NotifyPropertyChanged("Res"); }
        }

        public Int16 SampleSize
        {
            get { return Int16.Parse(lObj["SampleSize"].ToString()); }
            set { lObj["SampleSize"] = value; NotifyPropertyChanged("SampleSize"); }
        }

        public Single SEL
        {
            get { return Single.Parse(lObj["SEL"].ToString()); }
            set { lObj["SEL"] = value; NotifyPropertyChanged("SEL"); }
        }

        public Single StdTare
        {
            get { return Single.Parse(lObj["StdTare"].ToString()); }
            set { lObj["StdTare"] = value; NotifyPropertyChanged("StdTare"); }
        }

        public Single StdVol
        {
            get { return Single.Parse(lObj["StdVol"].ToString()); }
            set { lObj["StdVol"] = value; NotifyPropertyChanged("StdVol"); }
        }

        public String StdVolUnits
        {
            get { return lObj["StdVolUnits"].ToString(); }
            set { lObj["StdVolUnits"] = value; NotifyPropertyChanged("StdVolUnits"); }
        }

        public Double Tare
        {
            get { return Double.Parse(lObj["Tare"].ToString()); }
            set { lObj["Tare"] = value; NotifyPropertyChanged("Tare"); }
        }

        public Single Vol1
        {
            get { return Single.Parse(lObj["Vol1"].ToString()); }
            set { lObj["Vol1"] = value; NotifyPropertyChanged("Vol1"); }
        }

        public Single Vol2
        {
            get { return Single.Parse(lObj["Vol2"].ToString()); }
            set { lObj["Vol2"] = value; NotifyPropertyChanged("Vol2"); }
        }

        public Single VolTemp
        {
            get { return Single.Parse(lObj["VolTemp"].ToString()); }
            set { lObj["VolTemp"] = value; NotifyPropertyChanged("VolTemp"); }
        }

        public PCSDetail()
        {
            lObj = new TableWrapper("PackD");
        }

        public PCSDetail(string insp_ID, int pack_ID)
        {
            lObj = new TableWrapper("PackD");
            Load(insp_ID, pack_ID);
        }

        public void Load(string insp_ID, int pack_ID)
        {
            lObj["Insp_ID"] = insp_ID;
            lObj["Pack_ID"] = pack_ID;
            lObj.Load();
        }

        private void LoadPackTests()
        {
            PCSTest pcsTest;

            pcsTests = new ObservableCollection<PCSTest>();

            string sql = "Select Pack_ID, Test from PackTest where insp_id ='" + this.Insp_ID + "' and Pack_ID = "+ this.Pack_ID + " order by Pack_ID";
            DbDataReader rdr = WWD.GetReader(sql);

            while (rdr.Read())
            {
                pcsTest = new PCSTest();
                pcsTest.Load(this.Insp_ID, rdr.GetInt16NoNull(0), rdr.GetInt16NoNull(1));
                pcsTests.Add(pcsTest);
            }
            pcsTestLoaded = true;
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
