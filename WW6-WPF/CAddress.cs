using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinWam6
{
    public class CAddress
    {
        private string m_Street1;
        private string m_Street2;
        private string m_City;
        private string m_State;
        private string m_Zip;
        private bool m_IsDirty;
    
        public CAddress(string Street1, string Street2, string City, string State, string Zip)
        {
            m_Street1 = Street1;
            m_Street2 = Street2;
            m_City = City;
            m_State = State;
            m_Zip = Zip;
            m_IsDirty = false;
        }

        public CAddress()
        {
            m_Street1 = "";
            m_Street2 = "";
            m_City = "";
            m_State = "";
            m_Zip = "";
            m_IsDirty = false;
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
