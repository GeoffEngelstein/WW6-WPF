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

            get { return (Decimal)lObj["AmtPaid"]; }

            set { lObj["AmtPaid"] = value; if (lObj.Fields["AmtPaid"].RecentChange) NotifyPropertyChanged("AmtPaid"); }


        }
        public String Bus_ID
        {

            get { return lObj["Bus_ID"] as string; }

            set { lObj["Bus_ID"] = value; if (lObj.Fields["Bus_ID"].RecentChange) NotifyPropertyChanged("Bus_ID"); }


        }
        public DateTime? CreateDate
        {

            get { return (lObj["CreateDate"] as DateTime?); }

            set { lObj["CreateDate"] = value; if (lObj.Fields["CreateDate"].RecentChange) NotifyPropertyChanged("CreateDate"); }


        }
        public String CreateID
        {

            get { return lObj["CreateID"] as string; }

            set { lObj["CreateID"] = value; if (lObj.Fields["CreateID"].RecentChange) NotifyPropertyChanged("CreateID"); }


        }
        public DateTime? DatePaid
        {

            get { return (lObj["DatePaid"] as DateTime?); }

            set { lObj["DatePaid"] = value; if (lObj.Fields["DatePaid"].RecentChange) NotifyPropertyChanged("DatePaid"); }


        }
        public Byte Exported
        {

            get { return (Byte)lObj["Exported"]; }

            set { lObj["Exported"] = value; if (lObj.Fields["Exported"].RecentChange) NotifyPropertyChanged("Exported"); }


        }
        public Single Failure
        {

            get { return (Single)lObj["Failure"]; }

            set { lObj["Failure"] = value; if (lObj.Fields["Failure"].RecentChange) NotifyPropertyChanged("Failure"); }


        }
        public bool FeePaid
        {

            get { return (bool)lObj["FeePaid"]; }

            set { lObj["FeePaid"] = value; if (lObj.Fields["FeePaid"].RecentChange) NotifyPropertyChanged("FeePaid"); }


        }
        public String FeeRef
        {

            get { return lObj["FeeRef"] as string; }

            set { lObj["FeeRef"] = value; if (lObj.Fields["FeeRef"].RecentChange) NotifyPropertyChanged("FeeRef"); }


        }
        public DateTime? Insp_Date
        {

            get { return (lObj["Insp_Date"] as DateTime?); }

            set { lObj["Insp_Date"] = value; if (lObj.Fields["Insp_Date"].RecentChange) NotifyPropertyChanged("Insp_Date"); }


        }
        public String Insp_ID
        {

            get { return lObj["Insp_ID"] as string; }

            set { lObj["Insp_ID"] = value; if (lObj.Fields["Insp_ID"].RecentChange) NotifyPropertyChanged("Insp_ID"); }


        }
        public String Insp_Reason
        {

            get { return lObj["Insp_Reason"] as string; }

            set { lObj["Insp_Reason"] = value; if (lObj.Fields["Insp_Reason"].RecentChange) NotifyPropertyChanged("Insp_Reason"); }


        }
        public Decimal InspFee
        {

            get { return (Decimal)lObj["InspFee"]; }

            set { lObj["InspFee"] = value; if (lObj.Fields["InspFee"].RecentChange) NotifyPropertyChanged("InspFee"); }


        }
        public String InspNote
        {

            get { return lObj["InspNote"] as string; }

            set { lObj["InspNote"] = value; if (lObj.Fields["InspNote"].RecentChange) NotifyPropertyChanged("InspNote"); }


        }
        public String InspType
        {

            get { return lObj["InspType"] as string; }

            set { lObj["InspType"] = value; if (lObj.Fields["InspType"].RecentChange) NotifyPropertyChanged("InspType"); }


        }
        public String Insr_ID
        {

            get { return lObj["Insr_ID"] as string; }

            set { lObj["Insr_ID"] = value; if (lObj.Fields["Insr_ID"].RecentChange) NotifyPropertyChanged("Insr_ID"); }


        }
        public Int16 ManSampleSize
        {

            get { return (Int16)lObj["ManSampleSize"]; }

            set { lObj["ManSampleSize"] = value; if (lObj.Fields["ManSampleSize"].RecentChange) NotifyPropertyChanged("ManSampleSize"); }


        }
        public DateTime? ModifyDate
        {

            get { return (lObj["ModifyDate"] as DateTime?); }

            set { lObj["ModifyDate"] = value; if (lObj.Fields["ModifyDate"].RecentChange) NotifyPropertyChanged("ModifyDate"); }


        }
        public String ModifyID
        {

            get { return lObj["ModifyID"] as string; }

            set { lObj["ModifyID"] = value; if (lObj.Fields["ModifyID"].RecentChange) NotifyPropertyChanged("ModifyID"); }


        }
        public DateTime? NextInsp
        {

            get { return (lObj["NextInsp"] as DateTime?); }

            set { lObj["NextInsp"] = value; if (lObj.Fields["NextInsp"].RecentChange) NotifyPropertyChanged("NextInsp"); }


        }
        public Single OtherProdUsed
        {

            get { return (Single)lObj["OtherProdUsed"]; }

            set { lObj["OtherProdUsed"] = value; if (lObj.Fields["OtherProdUsed"].RecentChange) NotifyPropertyChanged("OtherProdUsed"); }


        }
        public Int16 Results
        {

            get { return (Int16)lObj["Results"]; }

            set { lObj["Results"] = value; if (lObj.Fields["Results"].RecentChange) NotifyPropertyChanged("Results"); }


        }
        public Int16 SampleSize
        {

            get { return (Int16)lObj["SampleSize"]; }

            set { lObj["SampleSize"] = value; if (lObj.Fields["SampleSize"].RecentChange) NotifyPropertyChanged("SampleSize"); }


        }
        public Decimal TotInspFee
        {

            get { return (Decimal)lObj["TotInspFee"]; }

            set { lObj["TotInspFee"] = value; if (lObj.Fields["TotInspFee"].RecentChange) NotifyPropertyChanged("TotInspFee"); }


        }
        public Single TotProdUsed
        {

            get { return (Single)lObj["TotProdUsed"]; }

            set { lObj["TotProdUsed"] = value; if (lObj.Fields["TotProdUsed"].RecentChange) NotifyPropertyChanged("TotProdUsed"); }


        }
        public String InspSig
        {

            get { return lObj["InspSig"] as string; }

            set { lObj["InspSig"] = value; if (lObj.Fields["InspSig"].RecentChange) NotifyPropertyChanged("InspSig"); }


        }
        public String BusSig
        {

            get { return lObj["BusSig"] as string; }

            set { lObj["BusSig"] = value; if (lObj.Fields["BusSig"].RecentChange) NotifyPropertyChanged("BusSig"); }


        }
        public String PrintedName
        {

            get { return lObj["PrintedName"] as string; }

            set { lObj["PrintedName"] = value; if (lObj.Fields["PrintedName"].RecentChange) NotifyPropertyChanged("PrintedName"); }


        }
        public Int16 OriginalSampleSize
        {

            get { return (Int16)lObj["OriginalSampleSize"]; }

            set { lObj["OriginalSampleSize"] = value; if (lObj.Fields["OriginalSampleSize"].RecentChange) NotifyPropertyChanged("OriginalSampleSize"); }


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
