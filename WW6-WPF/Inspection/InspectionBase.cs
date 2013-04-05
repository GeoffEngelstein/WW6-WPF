using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;



namespace WinWam6
{
    public abstract class InspectionBase : INotifyPropertyChanged
    {
        protected TableWrapper lObj;
        public enum InspectionType {PCS, DEV, UPC, QST, QSTD};

        protected TableWrapper Wrapper
        {
            get { return lObj; }
        }

        public Decimal AmtPaid
        {
            get { return Decimal.Parse(lObj["AmtPaid"].ToString()); }
            set { lObj["AmtPaid"] = value; NotifyPropertyChanged("AmtPaid"); }
        }
        public String Bus_ID
        {

            get { return lObj["Bus_ID"].ToString(); }

            set { lObj["Bus_ID"] = value; NotifyPropertyChanged("Bus_ID"); }


        }
        public DateTime? CreateDate
        {

            get { DateTime d; if (DateTime.TryParse(lObj["CreateDate"].ToString(), out d)) { return d; } else { return null; } }

            set { lObj["CreateDate"] = value; NotifyPropertyChanged("CreateDate"); }


        }
        public String CreateID
        {

            get { return lObj["CreateID"].ToString(); }

            set { lObj["CreateID"] = value; NotifyPropertyChanged("CreateID"); }


        }
        public DateTime? DatePaid
        {

            get { DateTime d; if (DateTime.TryParse(lObj["DatePaid"].ToString(), out d)) { return d; } else { return null; } }

            set { lObj["DatePaid"] = value; NotifyPropertyChanged("DatePaid"); }


        }
        public Byte Exported
        {

            get { return Byte.Parse(lObj["Exported"].ToString()); }

            set { lObj["Exported"] = value; NotifyPropertyChanged("Exported"); }


        }
        public Single Failure
        {

            get { return Single.Parse(lObj["Failure"].ToString()); }

            set { lObj["Failure"] = value; NotifyPropertyChanged("Failure"); }


        }
        public bool FeePaid
        {

            get { return bool.Parse(lObj["FeePaid"].ToString()); }

            set { lObj["FeePaid"] = value; NotifyPropertyChanged("FeePaid"); }


        }
        public String FeeRef
        {

            get { return lObj["FeeRef"].ToString(); }

            set { lObj["FeeRef"] = value; NotifyPropertyChanged("FeeRef"); }


        }
        public DateTime? Insp_Date
        {

            get { DateTime d; if (DateTime.TryParse(lObj["Insp_Date"].ToString(), out d)) { return d; } else { return null; } }

            set { lObj["Insp_Date"] = value; NotifyPropertyChanged("Insp_Date"); }


        }
        public String Insp_ID
        {

            get { return lObj["Insp_ID"].ToString(); }

            set { lObj["Insp_ID"] = value; NotifyPropertyChanged("Insp_ID"); }


        }
        public String Insp_Reason
        {

            get { return lObj["Insp_Reason"].ToString(); }

            set { lObj["Insp_Reason"] = value; NotifyPropertyChanged("Insp_Reason"); }


        }
        public Decimal InspFee
        {

            get { return Decimal.Parse(lObj["InspFee"].ToString()); }

            set { lObj["InspFee"] = value; NotifyPropertyChanged("InspFee"); }


        }
        public String InspNote
        {

            get { return lObj["InspNote"].ToString(); }

            set { lObj["InspNote"] = value; NotifyPropertyChanged("InspNote"); }


        }
        public String InspType
        {

            get { return lObj["InspType"].ToString(); }

            set { lObj["InspType"] = value; NotifyPropertyChanged("InspType"); }


        }
        public String Insr_ID
        {

            get { return lObj["Insr_ID"].ToString(); }

            set { lObj["Insr_ID"] = value; NotifyPropertyChanged("Insr_ID"); }


        }
        public Int16 ManSampleSize
        {

            get { return Int16.Parse(lObj["ManSampleSize"].ToString()); }

            set { lObj["ManSampleSize"] = value; NotifyPropertyChanged("ManSampleSize"); }


        }
        public DateTime? ModifyDate
        {

            get { DateTime d; if (DateTime.TryParse(lObj["ModifyDate"].ToString(), out d)) { return d; } else { return null; } }

            set { lObj["ModifyDate"] = value; NotifyPropertyChanged("ModifyDate"); }


        }
        public String ModifyID
        {

            get { return lObj["ModifyID"].ToString(); }

            set { lObj["ModifyID"] = value; NotifyPropertyChanged("ModifyID"); }


        }
        public DateTime? NextInsp
        {

            get { DateTime d; if (DateTime.TryParse(lObj["NextInsp"].ToString(), out d)) { return d; } else { return null; } }

            set { lObj["NextInsp"] = value; NotifyPropertyChanged("NextInsp"); }


        }
        public Single OtherProdUsed
        {

            get { return Single.Parse(lObj["OtherProdUsed"].ToString()); }

            set { lObj["OtherProdUsed"] = value; NotifyPropertyChanged("OtherProdUsed"); }


        }
        public Int16 Results
        {

            get { return Int16.Parse(lObj["Results"].ToString()); }

            set { lObj["Results"] = value; NotifyPropertyChanged("Results"); }


        }
        public Int16 SampleSize
        {

            get { return Int16.Parse(lObj["SampleSize"].ToString()); }

            set { lObj["SampleSize"] = value; NotifyPropertyChanged("SampleSize"); }


        }
        public Decimal TotInspFee
        {

            get { return Decimal.Parse(lObj["TotInspFee"].ToString()); }

            set { lObj["TotInspFee"] = value; NotifyPropertyChanged("TotInspFee"); }


        }
        public Single TotProdUsed
        {

            get { return Single.Parse(lObj["TotProdUsed"].ToString()); }

            set { lObj["TotProdUsed"] = value; NotifyPropertyChanged("TotProdUsed"); }


        }
        public String InspSig
        {

            get { return lObj["InspSig"].ToString(); }

            set { lObj["InspSig"] = value; NotifyPropertyChanged("InspSig"); }


        }
        public String BusSig
        {

            get { return lObj["BusSig"].ToString(); }

            set { lObj["BusSig"] = value; NotifyPropertyChanged("BusSig"); }


        }
        public String PrintedName
        {

            get { return lObj["PrintedName"].ToString(); }

            set { lObj["PrintedName"] = value; NotifyPropertyChanged("PrintedName"); }


        }
        public Int16 OriginalSampleSize
        {

            get { return Int16.Parse(lObj["OriginalSampleSize"].ToString()); }

            set { lObj["OriginalSampleSize"] = value; NotifyPropertyChanged("OriginalSampleSize"); }


        }
        
        public abstract string TooltipText { get; }
           
        
        //Constructors
        public InspectionBase(string InspID, string inspectionType )
        {
            lObj = new TableWrapper("InspH");
            lObj["Insp_ID"] = InspID;
            lObj["InspType"] = inspectionType;
            lObj.Load();
        }

        protected InspectionBase() 
        {
            lObj = new TableWrapper("InspH");
        }

        protected void LoadPrivate(string InspID, string inspectionType)
        {
            lObj["Insp_ID"] = InspID;
            lObj["InspType"] = inspectionType;
            lObj.Load();
        }

        public virtual void Load(string InspID) { }

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
