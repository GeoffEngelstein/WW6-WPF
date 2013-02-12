using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.ComponentModel;

namespace WinWam6
{
    class DeviceInspection : InspectionBase
    {

        public override string TooltipText
        {
            get
            {
                return "Device Inspection" + " Inspection ID " + this.Insp_ID;
            }
        }

        public DeviceInspection() : base() { }

        public override void Load(string InspID)
        {
            base.LoadPrivate(InspID, "D");
        }

    }

    class DevD : INotifyPropertyChanged
    {
        Dictionary<string, DeviceAttribute> attributes = new Dictionary<string, DeviceAttribute>();
        DeviceInspectionCustom custom = new DeviceInspectionCustom();
        TableWrapper lObj = new TableWrapper("DevD");

        public Int16 Acc_Main
        {

            get { return Int16.Parse(lObj["Acc_Main"].ToString()); }

            set { lObj["Acc_Main"] = value; NotifyPropertyChanged("Acc_Main"); }


        }
        public Int16 Dev_ID
        {

            get { return Int16.Parse(lObj["Dev_ID"].ToString()); }

            set { lObj["Dev_ID"] = value; NotifyPropertyChanged("Dev_ID"); }


        }
        public Decimal Fee
        {

            get { return Decimal.Parse(lObj["Fee"].ToString()); }

            set { lObj["Fee"] = value; NotifyPropertyChanged("Fee"); }


        }
        public String Insp_ID
        {

            get { return lObj["Insp_ID"].ToString(); }

            set { lObj["Insp_ID"] = value; NotifyPropertyChanged("Insp_ID"); }


        }
        public String Notes
        {

            get { return lObj["Notes"].ToString(); }

            set { lObj["Notes"] = value; NotifyPropertyChanged("Notes"); }


        }
        public String Other
        {

            get { return lObj["Other"].ToString(); }

            set { lObj["Other"] = value; NotifyPropertyChanged("Other"); }


        }
        public Single ProdUsed
        {

            get { return Single.Parse(lObj["ProdUsed"].ToString()); }

            set { lObj["ProdUsed"] = value; NotifyPropertyChanged("ProdUsed"); }


        }
        public Int32 Ref
        {

            get { return Int32.Parse(lObj["Ref"].ToString()); }

            set { lObj["Ref"] = value; NotifyPropertyChanged("Ref"); }


        }
        public Int16 Results
        {

            get { return Int16.Parse(lObj["Results"].ToString()); }

            set { lObj["Results"] = value; NotifyPropertyChanged("Results"); }


        }
        public String Seal
        {

            get { return lObj["Seal"].ToString(); }

            set { lObj["Seal"] = value; NotifyPropertyChanged("Seal"); }


        }
        public String TolUnit
        {

            get { return lObj["TolUnit"].ToString(); }

            set { lObj["TolUnit"] = value; NotifyPropertyChanged("TolUnit"); }


        }
        public bool ProdUsedOverride
        {

            get { return bool.Parse(lObj["ProdUsedOverride"].ToString()); }

            set { lObj["ProdUsedOverride"] = value; NotifyPropertyChanged("ProdUsedOverride"); }


        }




        DevD()
        {
            //new Tests
            
        }


        // Update the results for a single row in the inspection
        public void UpdateSingleResults(int row) { }

        private void UpdateWIPreponderance(/*curTest as CDevTest*/) { }

        private void UpdateVAPreponderance(/*curTest as CDevTest*/) { }

        private void UpdateGAPreponderance(/*curTest as CDevTest*/) { }

//        private int PrepGradeIndex(string Grade, tPreponderance[] PrepArr) { return 0; }

        public void UpdateAllResuls() { }

        private void DeviceClassChanged() { }

        public void LoadSingleTemplate(/*CDevTemplate Template*/) { }

        public void LoadTemplate(bool ShowPrompt) { }

        public void LoadTempalte(bool ShowPrompt, string TemplateName) { }

        public void Load(string InspID, int curLine) { }

        public decimal SetDefaultFee() { return 0; }


        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }

    
    class DeviceAttribute : INotifyPropertyChanged
    {

        TableWrapper lObj = new TableWrapper("DevAttr");

        public enum DevAttributeStatus { Pass, Fail, NotChecked }

        DevAttributeStatus Status { get; set; }
        string Description { get; set; }

        public Int16 Dev_ID
        {

            get { return Int16.Parse(lObj["Dev_ID"].ToString()); }

            set { lObj["Dev_ID"] = value; NotifyPropertyChanged("Dev_ID"); }


        }
        public String Insp_ID
        {

            get { return lObj["Insp_ID"].ToString(); }

            set { lObj["Insp_ID"] = value; NotifyPropertyChanged("Insp_ID"); }


        }
        public Int16 Result
        {

            get { return Int16.Parse(lObj["Result"].ToString()); }

            set { lObj["Result"] = value; NotifyPropertyChanged("Result"); }


        }
        public String Test
        {

            get { return lObj["Test"].ToString(); }

            set { lObj["Test"] = value; NotifyPropertyChanged("Test"); }


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

    class DeviceTest : INotifyPropertyChanged
    {

        TableWrapper lObj = new TableWrapper("DevTest");

        public Single Actual
        {

            get { return Single.Parse(lObj["Actual"].ToString()); }

            set { lObj["Actual"] = value; NotifyPropertyChanged("Actual"); }


        }
        public Int16 Dev_ID
        {

            get { return Int16.Parse(lObj["Dev_ID"].ToString()); }

            set { lObj["Dev_ID"] = value; NotifyPropertyChanged("Dev_ID"); }


        }
        public Single Display
        {

            get { return Single.Parse(lObj["Display"].ToString()); }

            set { lObj["Display"] = value; NotifyPropertyChanged("Display"); }


        }
        public Single Error
        {

            get { return Single.Parse(lObj["Error"].ToString()); }

            set { lObj["Error"] = value; NotifyPropertyChanged("Error"); }


        }
        public Single Increment
        {

            get { return Single.Parse(lObj["Increment"].ToString()); }

            set { lObj["Increment"] = value; NotifyPropertyChanged("Increment"); }


        }
        public String Insp_ID
        {

            get { return lObj["Insp_ID"].ToString(); }

            set { lObj["Insp_ID"] = value; NotifyPropertyChanged("Insp_ID"); }


        }
        public String Notes
        {

            get { return lObj["Notes"].ToString(); }

            set { lObj["Notes"] = value; NotifyPropertyChanged("Notes"); }


        }
        public Int16 Results
        {

            get { return Int16.Parse(lObj["Results"].ToString()); }

            set { lObj["Results"] = value; NotifyPropertyChanged("Results"); }


        }
        public String Supplement
        {

            get { return lObj["Supplement"].ToString(); }

            set { lObj["Supplement"] = value; NotifyPropertyChanged("Supplement"); }


        }
        public String Test
        {

            get { return lObj["Test"].ToString(); }

            set { lObj["Test"] = value; NotifyPropertyChanged("Test"); }


        }
        public Int16 Test_ID
        {

            get { return Int16.Parse(lObj["Test_ID"].ToString()); }

            set { lObj["Test_ID"] = value; NotifyPropertyChanged("Test_ID"); }


        }
        public Single Tolerance
        {

            get { return Single.Parse(lObj["Tolerance"].ToString()); }

            set { lObj["Tolerance"] = value; NotifyPropertyChanged("Tolerance"); }


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
