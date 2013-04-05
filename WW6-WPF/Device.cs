using System;
using System.Globalization;
using System.ComponentModel;

namespace WinWam6
{
    class Device : INotifyPropertyChanged
    {
        private readonly TableWrapper lObj;

        //Constructors

        public Device()
        {
            Custom = new DeviceCustom();
        }

        public Device(int Ref)
        {
            lObj = new TableWrapper("Device");
            lObj.Fields["Ref"].Value = Ref;
            lObj.Load();

            Custom = new DeviceCustom();
            Custom.Initialize();
            Custom.Load(Ref.ToString(CultureInfo.InvariantCulture));
        }

        //Properties

        public bool Active
        {
            get { return bool.Parse(lObj["Active"].ToString()); }

            set
            {
                lObj["Active"] = value;
                NotifyPropertyChanged("Active");
            }
        }

        public String Bus_ID
        {
            get { return lObj["Bus_ID"].ToString(); }

            set
            {
                lObj["Bus_ID"] = value;
                NotifyPropertyChanged("Bus_ID");
            }
        }

        public Int16 Class
        {
            get { return Int16.Parse(lObj["Class"].ToString()); }

            set
            {
                lObj["Class"] = value;
                NotifyPropertyChanged("Class");
            }
        }

        public Int16 Exported
        {
            get { return Int16.Parse(lObj["Exported"].ToString()); }

            set
            {
                lObj["Exported"] = value;
                NotifyPropertyChanged("Exported");
            }
        }

        public bool IsDirty
        {
            get { return (lObj.IsDirty || Custom.IsDirty); }
        }

        public DateTime LastInsp
        {
            get { return DateTime.Parse(lObj["LastInsp"].ToString()); }

            set
            {
                lObj["LastInsp"] = value;
                NotifyPropertyChanged("LastInsp");
            }
        }

        public Int16 LastResults
        {
            get { return Int16.Parse(lObj["LastResults"].ToString()); }

            set
            {
                lObj["LastResults"] = value;
                NotifyPropertyChanged("LastResults");
            }
        }

        public String Location
        {
            get { return lObj["Location"].ToString(); }

            set
            {
                lObj["Location"] = value;
                NotifyPropertyChanged("Location");
            }
        }

        public String Make
        {
            get { return lObj["Make"].ToString(); }

            set
            {
                lObj["Make"] = value;
                NotifyPropertyChanged("Make");
            }
        }

        public String Model
        {
            get { return lObj["Model"].ToString(); }

            set
            {
                lObj["Model"] = value;
                NotifyPropertyChanged("Model");
            }
        }

        public Int32 Ref
        {
            get { return Int32.Parse(lObj["Ref"].ToString()); }

            set
            {
                lObj["Ref"] = value;
                NotifyPropertyChanged("Ref");
            }
        }

        public String Serial
        {
            get { return lObj["Serial"].ToString(); }

            set
            {
                lObj["Serial"] = value;
                NotifyPropertyChanged("Serial");
            }
        }

        public String Subclass
        {
            get { return lObj["Subclass"].ToString(); }

            set
            {
                lObj["Subclass"] = value;
                NotifyPropertyChanged("Subclass");
            }
        }

        public DateTime CreateDate
        {
            get { return DateTime.Parse(lObj["CreateDate"].ToString()); }

            set
            {
                lObj["CreateDate"] = value;
                NotifyPropertyChanged("CreateDate");
            }
        }

        public String CreateID
        {
            get { return lObj["CreateID"].ToString(); }

            set
            {
                lObj["CreateID"] = value;
                NotifyPropertyChanged("CreateID");
            }
        }

        public DateTime ModifyDate
        {
            get { return DateTime.Parse(lObj["ModifyDate"].ToString()); }

            set
            {
                lObj["ModifyDate"] = value;
                NotifyPropertyChanged("ModifyDate");
            }
        }

        public String ModifyID
        {
            get { return lObj["ModifyID"].ToString(); }

            set
            {
                lObj["ModifyID"] = value;
                NotifyPropertyChanged("ModifyID");
            }
        }

        public Int16 Sort
        {
            get { return Int16.Parse(lObj["Sort"].ToString()); }

            set
            {
                lObj["Sort"] = value;
                NotifyPropertyChanged("Sort");
            }
        }

        public String Subpart
        {
            get { return lObj["Subpart"].ToString(); }

            set
            {
                lObj["Subpart"] = value;
                NotifyPropertyChanged("Subpart");
            }
        }

        public DeviceCustom Custom { get; set; }

        //Constructors

        //Methods

        public bool Delete(bool silent, bool writeLog = true)
        {
            return true;
        }

        public bool LoadMMS(string make, string model, string serial)
        {
            return true;
        }

        public bool LoadRef(long deviceRef)
        {
            return true;
        }

        public bool Save()
        {
            return true;
        }

        public void NextRef()
        {
        }

        #region INotifyPropertyChanged Members

        /// Need to implement this interface in order to get data binding
        /// to work properly.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}