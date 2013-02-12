using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.ComponentModel;

namespace WinWam6
{
    public class Business : INotifyPropertyChanged
    {
        private TableWrapper lObj;
        private BusinessCustom m_custom;


        public bool IsDirty
        {
            get
            {
                return (lObj.IsDirty || m_custom.IsDirty);
            }
        }

        public BusinessCustom Custom
        {
            get
            {
                return m_custom;
            }
            set
            {
                m_custom = value;
            }
        }

        public Business(string ID)
        {
            lObj = new TableWrapper("Business");
            lObj.Fields["Bus_ID"].Value = ID;
            lObj.Load();

            PhysicalAddress = new Address(
                lObj["Addr1"].ToString(),
                lObj["Addr2"].ToString(),
                lObj["City"].ToString(),
                lObj["State"].ToString(),
                lObj["Zip"].ToString()
                );

            MailingAddress = new Address(
                lObj["BAddr1"].ToString(),
                lObj["BAddr2"].ToString(),
                lObj["B_City"].ToString(),
                lObj["B_State"].ToString(),
                lObj["B_Zip"].ToString()
                );

            BillingAddress = new Address(
                lObj["MAddr1"].ToString(),
                lObj["MAddr2"].ToString(),
                lObj["M_City"].ToString(),
                lObj["M_State"].ToString(),
                lObj["M_Zip"].ToString()
                );

            m_custom = new BusinessCustom();
            m_custom.Initialize();
            m_custom.Load(ID);
        }

        public Business()
        {
            lObj = new TableWrapper("Business");
            m_custom = new BusinessCustom();
            m_custom.Initialize();
        }

        public Int16 Active
        {

            get { return Int16.Parse(lObj["Active"].ToString()); }

            set { lObj["Active"] = value; NotifyPropertyChanged("Active"); }

        }


        public String BName
        {

            get { return lObj["BName"].ToString(); }

            set { lObj["BName"] = value; NotifyPropertyChanged("BName"); }

        }


        public String Bus_ID
        {

            get { return lObj["Bus_ID"].ToString(); }

            set { lObj["Bus_ID"] = value; NotifyPropertyChanged("Bus_ID"); }

        }


        public String Bus_Name
        {

            get { return lObj["Bus_Name"].ToString(); }

            set { lObj["Bus_Name"] = value; NotifyPropertyChanged("Bus_Name"); }

        }


        public Int16 BusDays
        {

            get { return Int16.Parse(lObj["BusDays"].ToString()); }

            set { lObj["BusDays"] = value; NotifyPropertyChanged("BusDays"); }

        }


        public String BusType
        {

            get { return lObj["BusType"].ToString(); }

            set { lObj["BusType"] = value; NotifyPropertyChanged("BusType"); }

        }

        public String Contact
        {

            get { return lObj["Contact"].ToString(); }

            set { lObj["Contact"] = value; NotifyPropertyChanged("Contact"); }

        }


        public String County
        {

            get { return lObj["County"].ToString(); }

            set { lObj["County"] = value; NotifyPropertyChanged("County"); }

        }


        public Int16 DEVActive
        {

            get { return Int16.Parse(lObj["DEVActive"].ToString()); }

            set { lObj["DEVActive"] = value; NotifyPropertyChanged("DEVActive"); }

        }


        public Int16 EGGActive
        {

            get { return Int16.Parse(lObj["EGGActive"].ToString()); }

            set { lObj["EGGActive"] = value; NotifyPropertyChanged("EGGActive"); }

        }


        public Int16 Exported
        {

            get { return Int16.Parse(lObj["Exported"].ToString()); }

            set { lObj["Exported"] = value; NotifyPropertyChanged("Exported"); }

        }


        public String Fax
        {

            get { return lObj["Fax"].ToString(); }

            set { lObj["Fax"] = value; NotifyPropertyChanged("Fax"); }

        }


        public String FID
        {

            get { return lObj["FID"].ToString(); }

            set { lObj["FID"] = value; NotifyPropertyChanged("FID"); }

        }


        public DateTime LastDEVInsp
        {

            get { return DateTime.Parse(lObj["LastDEVInsp"].ToString()); }

            set { lObj["LastDEVInsp"] = value; NotifyPropertyChanged("LastDEVInsp"); }

        }


        public DateTime LastEggInsp
        {

            get { return DateTime.Parse(lObj["LastEggInsp"].ToString()); }

            set { lObj["LastEggInsp"] = value; NotifyPropertyChanged("LastEggInsp"); }

        }


        public DateTime LastInsp
        {

            get { return DateTime.Parse(lObj["LastInsp"].ToString()); }

            set { lObj["LastInsp"] = value; NotifyPropertyChanged("LastInsp"); }

        }


        public DateTime LastPCSInsp
        {

            get { return DateTime.Parse(lObj["LastPCSInsp"].ToString()); }

            set { lObj["LastPCSInsp"] = value; NotifyPropertyChanged("LastPCSInsp"); }

        }


        public DateTime LastQSTInsp
        {

            get { return DateTime.Parse(lObj["LastQSTInsp"].ToString()); }

            set { lObj["LastQSTInsp"] = value; NotifyPropertyChanged("LastQSTInsp"); }

        }


        public DateTime LastUPCInsp
        {

            get { return DateTime.Parse(lObj["LastUPCInsp"].ToString()); }

            set { lObj["LastUPCInsp"] = value; NotifyPropertyChanged("LastUPCInsp"); }

        }


 

        public DateTime NextBusInsp
        {

            get { return DateTime.Parse(lObj["NextBusInsp"].ToString()); }

            set { lObj["NextBusInsp"] = value; NotifyPropertyChanged("NextBusInsp"); }

        }


        public DateTime NextDEVInsp
        {

            get { return DateTime.Parse(lObj["NextDEVInsp"].ToString()); }

            set { lObj["NextDEVInsp"] = value; NotifyPropertyChanged("NextDEVInsp"); }

        }


        public DateTime NextEggInsp
        {

            get { return DateTime.Parse(lObj["NextEggInsp"].ToString()); }

            set { lObj["NextEggInsp"] = value; NotifyPropertyChanged("NextEggInsp"); }

        }


        public DateTime NextInsp
        {

            get { return DateTime.Parse(lObj["NextInsp"].ToString()); }

            set { lObj["NextInsp"] = value; NotifyPropertyChanged("NextInsp"); }

        }


        public DateTime NextPCSInsp
        {

            get { return DateTime.Parse(lObj["NextPCSInsp"].ToString()); }

            set { lObj["NextPCSInsp"] = value; NotifyPropertyChanged("NextPCSInsp"); }

        }


        public DateTime NextQSTInsp
        {

            get { return DateTime.Parse(lObj["NextQSTInsp"].ToString()); }

            set { lObj["NextQSTInsp"] = value; NotifyPropertyChanged("NextQSTInsp"); }

        }


        public DateTime NextUPCInsp
        {

            get { return DateTime.Parse(lObj["NextUPCInsp"].ToString()); }

            set { lObj["NextUPCInsp"] = value; NotifyPropertyChanged("NextUPCInsp"); }

        }


        public String Notes
        {

            get { return lObj["Notes"].ToString(); }

            set { lObj["Notes"] = value; NotifyPropertyChanged("Notes"); }

        }


        public Int16 PCSActive
        {

            get { return Int16.Parse(lObj["PCSActive"].ToString()); }

            set { lObj["PCSActive"] = value; NotifyPropertyChanged("PCSActive"); }

        }


        public String Phone
        {

            get { return lObj["Phone"].ToString(); }

            set { lObj["Phone"] = value; NotifyPropertyChanged("Phone"); }

        }


        public Int16 QSTActive
        {

            get { return Int16.Parse(lObj["QSTActive"].ToString()); }

            set { lObj["QSTActive"] = value; NotifyPropertyChanged("QSTActive"); }

        }

        public String Store_ID
        {

            get { return lObj["Store_ID"].ToString(); }

            set { lObj["Store_ID"] = value; NotifyPropertyChanged("Store_ID"); }

        }


        public Int16 UPCActive
        {

            get { return Int16.Parse(lObj["UPCActive"].ToString()); }

            set { lObj["UPCActive"] = value; NotifyPropertyChanged("UPCActive"); }

        }


        public String Soundex
        {

            get { return lObj["Soundex"].ToString(); }

            set { lObj["Soundex"] = value; NotifyPropertyChanged("Soundex"); }

        }


        public DateTime FirstInsp
        {

            get { return DateTime.Parse(lObj["FirstInsp"].ToString()); }

            set { lObj["FirstInsp"] = value; NotifyPropertyChanged("FirstInsp"); }

        }


        public DateTime InactiveDate
        {

            get { return DateTime.Parse(lObj["InactiveDate"].ToString()); }

            set { lObj["InactiveDate"] = value; NotifyPropertyChanged("InactiveDate"); }

        }


        public DateTime CreateDate
        {

            get { return DateTime.Parse(lObj["CreateDate"].ToString()); }

            set { lObj["CreateDate"] = value; NotifyPropertyChanged("CreateDate"); }

        }


        public String CreateID
        {

            get { return lObj["CreateID"].ToString(); }

            set { lObj["CreateID"] = value; NotifyPropertyChanged("CreateID"); }

        }


        public DateTime ModifyDate
        {

            get { return DateTime.Parse(lObj["ModifyDate"].ToString()); }

            set { lObj["ModifyDate"] = value; NotifyPropertyChanged("ModifyDate"); }

        }


        public String ModifyID
        {

            get { return lObj["ModifyID"].ToString(); }

            set { lObj["ModifyID"] = value; NotifyPropertyChanged("ModifyID"); }

        }


        public String Email
        {

            get { return lObj["Email"].ToString(); }

            set { lObj["Email"] = value; NotifyPropertyChanged("Email"); }

        }


        public Int16 QST2Active
        {

            get { return Int16.Parse(lObj["QST2Active"].ToString()); }

            set { lObj["QST2Active"] = value; NotifyPropertyChanged("QST2Active"); }

        }


        public DateTime NextQST2Insp
        {

            get { return DateTime.Parse(lObj["NextQST2Insp"].ToString()); }

            set { lObj["NextQST2Insp"] = value; NotifyPropertyChanged("NextQST2Insp"); }

        }


        public DateTime LastQST2Insp
        {

            get { return DateTime.Parse(lObj["LastQST2Insp"].ToString()); }

            set { lObj["LastQST2Insp"] = value; NotifyPropertyChanged("LastQST2Insp"); }

        }


        public String MContact
        {

            get { return lObj["MContact"].ToString(); }

            set { lObj["MContact"] = value; NotifyPropertyChanged("MContact"); }

        }


        public String BContact
        {

            get { return lObj["BContact"].ToString(); }

            set { lObj["BContact"] = value; NotifyPropertyChanged("BContact"); }

        }


        public String MName
        {

            get { return lObj["MName"].ToString(); }

            set { lObj["MName"] = value; NotifyPropertyChanged("MName"); }

        }


        public String Bus_ID_User
        {

            get { return lObj["Bus_ID_User"].ToString(); }

            set { lObj["Bus_ID_User"] = value; NotifyPropertyChanged("Bus_ID_User"); }

        }

        
        public double Latitude 
        {
            get {
                    double d = 0;
                    if (double.TryParse(lObj["Latitude"].ToString(), out d))
                    {
                        return d;
                    }
                    else
                    {
                        return 0;
                    }
                }

            set { lObj["Latitude"] = value; NotifyPropertyChanged("Latitude"); }
        }

        public double Longitude
        {
            get { return double.Parse(lObj["Longitude"].ToString()); }

            set { lObj["Longitude"] = value; NotifyPropertyChanged("Longitude"); }
        }

        public Address PhysicalAddress { get; set; }
        public Address MailingAddress { get; set; }
        public Address BillingAddress { get; set; }
        
        public bool Save()
        {
            bool rtn;

            //REMAP THE ADDRESS OBJECTS INTO THE WRAPPER
            if (PhysicalAddress.IsDirty) {
                lObj["Addr1"] = PhysicalAddress.Street1;
                lObj["Addr1"] = PhysicalAddress.Street2;
                lObj["City"] = PhysicalAddress.City;
                lObj["State"] = PhysicalAddress.State;
                lObj["Zip"] = PhysicalAddress.Zip;
            }

            if (MailingAddress.IsDirty)
            {
                lObj["MAddr1"] = MailingAddress.Street1;
                lObj["MAddr1"] = MailingAddress.Street2;
                lObj["M_City"] = MailingAddress.City;
                lObj["M_State"] = MailingAddress.State;
                lObj["M_Zip"] = MailingAddress.Zip;
            }

            if (BillingAddress.IsDirty)
            {
                lObj["BAddr1"] = BillingAddress.Street1;
                lObj["BAddr1"] = BillingAddress.Street2;
                lObj["B_City"] = BillingAddress.City;
                lObj["B_State"] = BillingAddress.State;
                lObj["B_Zip"] = BillingAddress.Zip;
            }

            rtn = lObj.Save();

            try
            {
                
                //TODO Maybe wrap this in a transaction?
            
                m_custom.Save();

            }
            catch
            {
                //TODO Some kind of error code here?
                return rtn;
            }
            
            return rtn;

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
