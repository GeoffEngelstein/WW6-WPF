using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WinWam6
{
    public class Address: INotifyPropertyChanged
    {
        private string m_Street1;
        private string m_Street2;
        private string m_City;
        private string m_State;
        private string m_Zip;
        private string m_contact;
        private bool m_IsDirty;
        private readonly bool fieldMode;
        private readonly Field street1Field;
        private readonly Field street2Field;
        private readonly Field cityField;
        private readonly Field stateField;
        private readonly Field zipField;
        private readonly Field contactField;
    
        public Address(string Street1, string Street2, string City, string State, string Zip, string Contact)
        {
            m_Street1 = Street1;
            m_Street2 = Street2;
            m_City = City;
            m_State = State;
            m_Zip = Zip;
            m_contact = Contact;
            m_IsDirty = false;
        }

        public Address()
        {
            m_Street1 = "";
            m_Street2 = "";
            m_City = "";
            m_State = "";
            m_Zip = "";
            m_contact = "";
            m_IsDirty = false;
            fieldMode = false;
        }

        public Address(Field street1, Field street2, Field city, Field state, Field zip, Field contact)
        {
            street1Field = street1;
            m_Street1 = street1.Value.ToString();
            
            street2Field = street2;
            m_Street2 = street2.Value.ToString();

            cityField = city;
            m_City = city.Value.ToString();

            stateField = state;
            m_State = state.Value.ToString();

            zipField = zip;
            m_Zip = zip.Value.ToString();

            contactField = contact;
            m_contact = contact.Value.ToString();

            fieldMode = true;
        }

        public string Street1
        {
            get
            {
                return m_Street1;
            }
            set
            {
                m_Street1 = value;
                if (fieldMode) street1Field.Value = value;
                m_IsDirty = true;
                NotifyPropertyChanged("Street1");
                NotifyPropertyChanged("LocationString");
            }
        }

        public String Street2
        {
            get
            {
                return m_Street2;
            }
            set
            {
                m_Street2 = value;
                if (fieldMode) street2Field.Value = value;
                m_IsDirty = true;
                NotifyPropertyChanged("Street2");
                NotifyPropertyChanged("LocationString");
            }
        }

        public string City
        {
            get
            {
                return m_City;
            }
            set
            {
                m_City = value;
                if (fieldMode) cityField.Value = value;
                m_IsDirty = true;
                NotifyPropertyChanged("City");
                NotifyPropertyChanged("LocationString");
            }
        }

        public string State
        {
            get
            {
                return m_State;
            }
            set
            {
                m_State = value;
                if (fieldMode) stateField.Value = value;
                m_IsDirty = true;
                NotifyPropertyChanged("State");
                NotifyPropertyChanged("LocationString");
            }
        }

        public string Zip
        {
            get
            {
                return m_Zip;
            }
            set
            {
                m_Zip = value;
                if (fieldMode) zipField.Value = value;
                m_IsDirty = true;
                NotifyPropertyChanged("Zip");
                NotifyPropertyChanged("LocationString");
            }
        }

        public string Contact
        {
            get { return m_contact; }
            set { m_contact = value;
                if (fieldMode) contactField.Value = value;
                m_IsDirty = true;
                NotifyPropertyChanged("Contact");
                
            }
        }

        public bool IsDirty
        {
            get
            {
                return m_IsDirty;
            }
            set
            {
                m_IsDirty = value;
            }
        }

        public string LocationString
        {
            get
            {
                return m_Street1 + " " + m_Street2 + " " + m_City + " " + m_State + " " + m_Zip;
            }
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
