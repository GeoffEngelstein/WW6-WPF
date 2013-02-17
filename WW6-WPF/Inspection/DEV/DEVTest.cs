using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WinWam6.Inspection.DEV
{
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
