using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinWam6
{
    public class Address
    {
        private string m_Street1;
        private string m_Street2;
        private string m_City;
        private string m_State;
        private string m_Zip;
        private bool m_IsDirty;
        private bool fieldMode = false;
        private Field street1Field;
        private Field street2Field;
        private Field cityField;
        private Field stateField;
        private Field zipField;
    
        public Address(string Street1, string Street2, string City, string State, string Zip)
        {
            m_Street1 = Street1;
            m_Street2 = Street2;
            m_City = City;
            m_State = State;
            m_Zip = Zip;
            m_IsDirty = false;
        }

        public Address()
        {
            m_Street1 = "";
            m_Street2 = "";
            m_City = "";
            m_State = "";
            m_Zip = "";
            m_IsDirty = false;
            fieldMode = false;
        }

        public Address(Field street1, Field street2, Field city, Field state, Field zip)
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
    }
}
