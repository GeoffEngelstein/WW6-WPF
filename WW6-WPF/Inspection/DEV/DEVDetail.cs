using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WinWam6.Inspection.DEV
{
    public class DEVDetail : INotifyPropertyChanged
    {
        Dictionary<string, DEVAttribute> attributes = new Dictionary<string, DEVAttribute>();
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


        public DEVDetail()
        {
            lObj = new TableWrapper("DevD");
        }

        public DEVDetail(string insp_ID, int dev_ID)
        {
            lObj = new TableWrapper("DevD");
            Load(insp_ID, dev_ID);
        }

        public void Load(string insp_ID, int dev_ID)
        {
            lObj["Insp_ID"] = insp_ID;
            lObj["Dev_ID"] = dev_ID;
            lObj.Load();
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

        public void LoadTemplate(bool ShowPrompt, string TemplateName) { }

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
}
