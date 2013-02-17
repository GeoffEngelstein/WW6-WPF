using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.ComponentModel;

namespace WinWam6.Inspection.DEV
{
    public class DEVAttribute : INotifyPropertyChanged
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
}
