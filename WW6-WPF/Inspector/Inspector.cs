using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace WinWam6
{
    class Inspector
    {

        private string m_ID;
        private InspectorCustom m_custom;
        private TableWrapper liw;

        public string FirstName
        {
            get
            {
                return liw["Insr_First"].ToString();
            }

            set
            {
                liw["Insr_First"] = value;
            }
        }

        public string LastName
        {
            get
            {
                return liw["Insr_Last"].ToString();
            }

            set
            {
                liw["Insr_Last"] = value;
            }
        }

        public string ID_User
        {
            get
            {
                return liw["Insr_ID_User"].ToString();
            }

            set
            {
                liw["Insr_ID_User"] = value;
            }
        }

        public string ID { get { return m_ID; } }      //Main Key - Readonly
        public InspectorCustom CustomFields { get { return m_custom; } }
        
        public bool IsDirty
        {
            get
            {
                return (liw.IsDirty || m_custom.IsDirty);   //if either the wrapper or the custom fields have changed we have changed.
            }
        }

        public Inspector()
        {
            m_custom = new InspectorCustom();
            m_custom.Initialize();
            liw = new TableWrapper("Inspector");
        }

        public Inspector(string ID) 
        {
            m_ID = ID;
            liw = new TableWrapper("Inspector");
            liw["Insr_ID"] = m_ID;
            liw.Load();

            m_custom = new InspectorCustom();
            m_custom.Initialize();
            m_custom.Load(m_ID);
        }

        public bool Save()
        {
            string sql;
            DbCommand cmd;
            
            if (m_ID == "")
            {
                m_ID = System.Guid.NewGuid().ToString();
                sql = liw.Insert();
            }
            else
            {
                sql = liw.Update();
            }

            try
            {

                //TODO Maybe wrap this in a transaction?
                cmd = WWD.GetCommand(sql);
                cmd.ExecuteNonQuery();

                m_custom.Save();

            }
            catch
            {
                //TODO Some kind of error code here?
                return false;
            }

            return true;

        }

        

        
    }
}
