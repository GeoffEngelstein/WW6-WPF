using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WinWam6
{
    public class Inspector : INotifyPropertyChanged
    {

        private string m_ID;
        private InspectorCustom m_custom;
        private TableWrapper lObj;

        public Int16 Exported
        {

            get { return Int16.Parse(lObj["Exported"].ToString()); }

            set { lObj["Exported"] = value; NotifyPropertyChanged("Exported"); }


        }
        public String Insr_First
        {

            get { return lObj["Insr_First"].ToString(); }

            set { lObj["Insr_First"] = value; NotifyPropertyChanged("Insr_First"); }


        }
        public String Insr_ID
        {

            get { return lObj["Insr_ID"].ToString(); }



        }
        public String Insr_Last
        {

            get { return lObj["Insr_Last"].ToString(); }

            set { lObj["Insr_Last"] = value; NotifyPropertyChanged("Insr_Last"); }


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
        public String Insr_ID_User
        {

            get { return lObj["Insr_ID_User"].ToString(); }

            set { lObj["Insr_ID_User"] = value; NotifyPropertyChanged("Insr_ID_User"); }


        }

        public InspectorCustom CustomFields { get { return m_custom; } }
        
        public bool IsDirty
        {
            get
            {
                return (lObj.IsDirty || m_custom.IsDirty);   //if either the wrapper or the custom fields have changed we have changed.
            }
        }

        public Inspector()
        {
            m_custom = new InspectorCustom();
            m_custom.Initialize();
            lObj = new TableWrapper("Inspector");
            lObj["Insr_ID"] = new Guid().ToString();
        }

        public Inspector(string ID) 
        {
            lObj = new TableWrapper("Inspector");
            lObj["Insr_ID"] = ID;
            lObj.Load();

            m_custom = new InspectorCustom();
            m_custom.Initialize();
            //m_custom.Load(m_ID);
        }

        public bool Save()
        {

            lObj.Save();

            try
            {

                //TODO Maybe wrap this in a transaction?
                
                m_custom.Save();

            }
            catch
            {
                //TODO Some kind of error code here?
                return false;
            }

            return true;

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

    public class Inspectors : Dictionary<string, Inspector>
    {
        public Inspectors(): base()
        {
            if (WWD.DatabaseIsOpen())
            {
                DbDataReader dr = WWD.GetReader("Select insr_id from inspector order by insr_id_user");

                while (dr.Read())
                {
                    Inspector inspector = new Inspector(dr[0].ToString());
                    //this.Add(inspector.Insr_ID_User,inspector.Insr_ID_User + " "+ inspector.Insr_Last+", "+inspector.Insr_First);
                    this.Add(inspector.Insr_ID_User, inspector);
                }
            }
        }
    }
}
