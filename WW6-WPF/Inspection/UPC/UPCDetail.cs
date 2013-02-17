using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WinWam6.Inspection.UPC
{
    class UPCDetail : INotifyPropertyChanged
    {
        private TableWrapper lObj;

        public String BarCodeType
        {

            get { return lObj["BarCodeType"].ToString(); }

            set { lObj["BarCodeType"] = value; NotifyPropertyChanged("BarCodeType"); }


        }
        public String Commodity
        {

            get { return lObj["Commodity"].ToString(); }

            set { lObj["Commodity"] = value; NotifyPropertyChanged("Commodity"); }


        }
        public String Insp_ID
        {

            get { return lObj["Insp_ID"].ToString(); }

            set { lObj["Insp_ID"] = value; NotifyPropertyChanged("Insp_ID"); }


        }
        public Int16 IntUC
        {

            get { return Int16.Parse(lObj["IntUC"].ToString()); }

            set { lObj["IntUC"] = value; NotifyPropertyChanged("IntUC"); }


        }
        public String Location
        {

            get { return lObj["Location"].ToString(); }

            set { lObj["Location"] = value; NotifyPropertyChanged("Location"); }


        }
        public String Notes
        {

            get { return lObj["Notes"].ToString(); }

            set { lObj["Notes"] = value; NotifyPropertyChanged("Notes"); }


        }
        public Decimal Scan
        {

            get { return Decimal.Parse(lObj["Scan"].ToString()); }

            set { lObj["Scan"] = value; NotifyPropertyChanged("Scan"); }


        }
        public Decimal Shelf
        {

            get { return Decimal.Parse(lObj["Shelf"].ToString()); }

            set { lObj["Shelf"] = value; NotifyPropertyChanged("Shelf"); }


        }
        public String UPC
        {

            get { return lObj["UPC"].ToString(); }

            set { lObj["UPC"] = value; NotifyPropertyChanged("UPC"); }


        }
        public Int16 UPC_ID
        {

            get { return Int16.Parse(lObj["UPC_ID"].ToString()); }

            set { lObj["UPC_ID"] = value; NotifyPropertyChanged("UPC_ID"); }


        }
        public Int32 LotSize
        {

            get { return Int32.Parse(lObj["LotSize"].ToString()); }

            set { lObj["LotSize"] = value; NotifyPropertyChanged("LotSize"); }


        }

        public UPCDetail()
        {
            lObj = new TableWrapper("UPCD");
        }

        public UPCDetail(string insp_ID, int UPC_ID)
        {
            lObj = new TableWrapper("UPCD");
            Load(insp_ID, UPC_ID);
        }

        public void Load(string insp_ID, int UPC_ID)
        {
            lObj["Insp_ID"] = insp_ID;
            lObj["UPC_ID"] = UPC_ID;
            lObj.Load();
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
