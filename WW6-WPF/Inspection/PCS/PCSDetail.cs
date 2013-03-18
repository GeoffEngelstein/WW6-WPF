using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WinWam6.Inspection.PCS
{
    public class PCSDetail : INotifyPropertyChanged, IDisposable
    {
        // private variables
        private TableWrapper lObj;
        private bool pcsTestLoaded = false;
        private ObservableCollection<PCSTest> pcsTests;
        private double volumeMultiple=1;
        private int mavError=0;
        private int tareSampleSize=0;
        private int finalTareSampleSize = 0;

        // Enumerations
        public enum PackageType {Standard, Random}
        public enum PCSResults {Pass=1, Fail, Gray, PkgFail, Audit, SingleComm}
        public enum PCSInspectionCategory {CatA, CatB, Audit, SingleComm}
        public enum PCSMAVType {Normal, USDAStd, USDAFluid, Mulch, PE}

        // Properties

        public ObservableCollection<PCSTest> PCSTests
        {
            get
            {
                if (!pcsTestLoaded)
                {
                    LoadPackTests();
                }
                return pcsTests;
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

        public int FinalTareSize
        {
            get 
            { 
                //TODO Tie into finalTareSize
                return (2*InitialTareSize);
            }
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

        public int InitialTareSize
        {
            get
            {
                return tareSampleSize;
            }
        }

        public String InspId
        {
            get { return lObj["Insp_ID"].ToString(); }
            set { lObj["Insp_ID"] = value; NotifyPropertyChanged("Insp_ID"); }
        }

        public PCSInspectionCategory InspCat
        {
            get
            {
                switch (lObj["InspCat"].ToString())
                {
                    case "A":
                        return PCSInspectionCategory.CatA;
                    case "B":
                        return PCSInspectionCategory.CatB;
                    case "U":
                        return PCSInspectionCategory.Audit;
                    case "S":
                        return PCSInspectionCategory.SingleComm;
                    default:
                        return PCSInspectionCategory.CatA;
                }
            }
            set
            {
                switch (value)
                {
                    case PCSInspectionCategory.CatA:
                        {
                            lObj["InspCat"] = "A";
                            break;
                        }
                    case PCSInspectionCategory.CatB:
                        {
                            lObj["InspCat"] = "B";
                            break;
                        }
                    case PCSInspectionCategory.Audit:
                        {
                            lObj["InspCat"] = "U";
                            break;
                        }
                    case PCSInspectionCategory.SingleComm:
                        {
                            lObj["InspCat"] = "S";
                            break;
                        }
                    default:
                        {
                            lObj["InspCat"] = "A";
                            break;
                        }
                }
                
                NotifyPropertyChanged("InspCat");
            }
        }

        public String LotCode
        {
            get { return lObj["LotCode"].ToString(); }
            set { lObj["LotCode"] = value; NotifyPropertyChanged("LotCode"); }
        }

        public Int16 LotSize
        {
            get { return Int16.Parse(lObj["LotSize"].ToString()); }
            set
            {
                lObj["LotSize"] = value;
                CalcSampleSize();
                NotifyPropertyChanged("LotSize");
            }
        }

        public Double Marked
        {
            get { return Double.Parse(lObj["NetWt"].ToString()); }
            set
            {
                lObj["NetWt"] = value;
                if (this.PackType == PackageType.Standard)
                    foreach (var pcsTest in PCSTests)
                        pcsTest.MarkedWeight = value;

                NotifyPropertyChanged("NetWt");
            }
        }
        
        public PCSMAVType MavType
        {
            get
            {
                switch (lObj["MAVType"].ToString())
                {
                    case "N":
                        return PCSMAVType.Normal;
                    case "M":
                        return PCSMAVType.Mulch;
                    case "U":
                        return PCSMAVType.USDAStd;
                    case "F":
                        return PCSMAVType.USDAFluid;
                    case "P":
                        return PCSMAVType.PE;
                    default:
                        return PCSMAVType.Normal;
                }
            }
            set
            {
                switch (value)
                {
                    case PCSMAVType.Normal:
                        lObj["MAVType"] = "N";
                        break;
                    case PCSMAVType.Mulch:
                        lObj["MAVType"] = "M";
                        break;
                    case PCSMAVType.USDAStd:
                        lObj["MAVType"] = "U";
                        break;
                    case PCSMAVType.USDAFluid:
                        lObj["MAVType"] = "F";
                        break;
                    case PCSMAVType.PE:
                        lObj["MAVType"] = "P";
                        break;
                }
                lObj["MAVType"] = value;
                NotifyPropertyChanged("MavType");
            }
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

        public PackageType PackType
        {
            get 
            { 

                switch (Int16.Parse(lObj["PackType"].ToString()))
                {
                    case 1:
                        return PackageType.Standard;

                    case 2:
                        return PackageType.Random;

                    default:
                        //TODO raise an error perhaps?
                        return PackageType.Standard;
                }
            }

            set 
            {   
                int packageType = 0;
                switch (value)
                {
                    case PackageType.Standard:
                        packageType = 1;
                        break;
                    case PackageType.Random:
                        packageType = 2;
                        break;
                }
                lObj["PackType"] = packageType; 
                NotifyPropertyChanged("PackType"); 
            }
        }

        public PCSResults Result
        {
            get
            {
                return (PCSResults)Int16.Parse(lObj["Res"].ToString()); 
            }
            set
            {
                lObj["Res"] = (int)value; 
                NotifyPropertyChanged("Result");
            }
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

        // Constructors

        public PCSDetail()
        {
            lObj = new TableWrapper("PackD");
        }

        public PCSDetail(string insp_ID, int pack_ID)
        {
            lObj = new TableWrapper("PackD");
            Load(insp_ID, pack_ID);
        }

        // Methods

        public void Load(string insp_ID, int pack_ID)
        {
            lObj["Insp_ID"] = insp_ID;
            lObj["Pack_ID"] = pack_ID;
            lObj.Load();
            CalcSampleSize();
        }

        private void LoadPackTests()
        {
            PCSTest pcsTest;

            pcsTests = new ObservableCollection<PCSTest>();

            string sql = "Select Pack_ID, Test from PackTest where insp_id ='" + this.InspId + "' and Pack_ID = "+ this.Pack_ID + " order by Pack_ID, Test";
            DbDataReader rdr = WWD.GetReader(sql);

            while (rdr.Read())
            {
                pcsTest = new PCSTest();
                pcsTest.Load(this.InspId, rdr.GetInt16NoNull(0), rdr.GetInt16NoNull(1));
                pcsTest.Parent = this;
                pcsTests.Add(pcsTest);
            }
            pcsTestLoaded = true;
        }

        // Inspection Calculation Section

        private void CalcSampleSize()
        {
            int oldSampleSize = this.SampleSize;

            switch (this.InspCat)
            {
                case PCSInspectionCategory.CatB:
                    {
                        mavError = 0;
                        if (LotSize < 10)
                        {
                            SampleSize = LotSize;
                            tareSampleSize = 2;
                            break;
                        }

                        if (LotSize < 251)
                        {
                            SampleSize = 10;
                            tareSampleSize = 2;
                            break;
                        }

                        SampleSize = 30;
                        tareSampleSize = 5;
                        break;
                    }

                case PCSInspectionCategory.CatA:
                    {
                        tareSampleSize = 2;

                        if (Glass && (LotSize > 250))
                            tareSampleSize = 3;

                        if (LotSize < 12)
                        {
                            SampleSize = LotSize;
                            mavError = 0;
                            break;
                        }

                        if (LotSize < 251)
                        {
                            SampleSize = 12;
                            mavError = 0;
                            break;
                        }

                        SampleSize = 48;
                        mavError = 1;

                        if (MavType == PCSMAVType.Mulch)
                        {
                            mavError = SampleSize/12;
                        }
                        
                        break;
                    }


                case PCSInspectionCategory.Audit:
                    {
                        if (LotSize > 48)
                        {
                            //ToDo MessageBox alerting user that size > 48
                            SampleSize = 48;
                        }
                        else
                        {
                            SampleSize = LotSize;
                        }
                        break;
                    }

                case PCSInspectionCategory.SingleComm:
                    {
                        if (LotSize > 200)
                        {
                            //ToDo Error Messagebox -- set size to 200 max
                            SampleSize = 200;
                        }
                        else
                        {
                            SampleSize = LotSize;
                        }
                        break;
                    }
            }

            AdjustTestCount();

            // Adjust Tare Sample Size if Sample Size is very low, or for mulch
            if (SampleSize < 2) tareSampleSize = SampleSize;
            
            if (MavType == PCSMAVType.Mulch) tareSampleSize = 0;
        
        
        }

        private void AdjustTestCount()
        {
            // Do we now have too many tests? If so, chop off the extras
            while (PCSTests.Count > SampleSize)
            {
                PCSTests.RemoveAt(PCSTests.Count - 1);
            }

            // Not enough tests? Add more
            while (PCSTests.Count < SampleSize)
            {
                var pcsTest = new PCSTest();
                if (PackageType.Standard == PackType)
                {
                    pcsTest.MarkedWeight = Marked;
                }
                pcsTest.Pack_ID = this.Pack_ID;
                pcsTest.Test = (short)(pcsTests.Count + 1);
                pcsTest.Parent = this;
                PCSTests.Add(pcsTest);
            }
        }

        private double CorrectionFactor(int sampleSize)
        {
            if (SampleSize < 2) return -99.0;
            if (SampleSize == 2) return -8.984;
            if (SampleSize == 3) return -2.484;
            if (SampleSize == 4) return -1.591;
            if (SampleSize == 5) return -1.241;
            if (SampleSize == 6) return -1.05;
            if (SampleSize == 7) return -0.925;
            if (SampleSize == 8) return -0.836;
            if (SampleSize == 9) return -0.769;
            if (SampleSize== 10) return -0.715;
            if (SampleSize == 11) return -0.672;
            if (SampleSize == 12) return -0.635;
            if (SampleSize == 24) return -0.422;
            return -0.291;
        }

        private bool Use2002Tables()
        {
            return true;
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Dispose()
        {
            foreach (PCSTest pcsTest in this.PCSTests)
            {
                pcsTest.Parent = null;
            }
        }
    }
}
