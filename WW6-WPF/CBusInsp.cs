using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace WinWam6
{
    class CBusInsp
    {
        private bool m_Active;
        private string m_Insr;
        private bool m_IsDirty;
        private DateTime m_LastInsp;
        private DateTime m_NextInsp;
        private Int32 m_Ref;

        public bool Active { get {return m_Active;} set {m_Active = value; m_IsDirty = true; }}
        public string Insr {get {return m_Insr;} set {m_Insr = value; m_IsDirty = true;}}
        public bool IsDirty {get {return m_IsDirty;}}
        public DateTime LastInsp {get {return m_LastInsp;} set {m_LastInsp = value; m_IsDirty = true;}}
        public DateTime NextInsp { get { return m_NextInsp; } set { m_NextInsp = value; m_IsDirty = true; } }
        public Int32 Ref { get { return m_Ref; } set { m_Ref = value; m_IsDirty = true; } }

        public void ClearDirtyFlag() {m_IsDirty = false;}
    }

    class CBusInsps
    {
        private Dictionary<Int32, CBusInsp> m_BusInsps;
        private bool m_Active;
        private string m_InspType;
        private bool m_IsDirty;
        private string m_Bus_ID;

        public Dictionary<Int32, CBusInsp> BusInsps { get { return m_BusInsps; } set { m_BusInsps = value; m_IsDirty = true; } }
        public bool Active { get { return m_Active; } set { m_Active = value; m_IsDirty = true; } }
        public string InspType { get { return m_InspType; } set { m_InspType = value; m_IsDirty = true; } }
        public bool IsDirty { get { return m_IsDirty; } }

        public CBusInsps(string InspType, string Bus_ID)        //Initialize the collection
        {

            CBusInsp cbi;

            m_InspType = InspType;
            m_Bus_ID = Bus_ID;

            m_BusInsps = new Dictionary<Int32, CBusInsp>(); //Instantiate the dictionary

            DbDataReader rdr = WWD.GetReader("Select Ref, NextInspDate, PrevInspDate from BusInspDates where bus_id = '" + Bus_ID + "' and insptype = '" + InspType + "'");

            while (rdr.Read())
            {

                cbi = new CBusInsp();

                cbi.Ref = rdr.GetInt32NoNull(0);
                cbi.NextInsp = rdr.GetDateTimeNoNull(1);
                cbi.LastInsp = rdr.GetDateTimeNoNull(2);

                cbi.ClearDirtyFlag();

                BusInsps.Add(cbi.Ref, cbi);

            }

            DbDataReader rdrInsp = WWD.GetReader("Select class, insr_id from InsrBus where bus_id = '" + Bus_ID + "' and insptype = '" + InspType + "'");

            Int32 lref;

            while (rdrInsp.Read())
            {
                lref = rdrInsp.GetInt32NoNull(0);               //Get class reference
                if (!BusInsps.TryGetValue(lref, out cbi))        //Check to see if the entry exists in the dictionary
                {
                    cbi = new CBusInsp();                       // If it doesn't create a new one
                    cbi.Ref = lref;
                    BusInsps.Add(cbi.Ref, cbi);
                }
                cbi.Insr = rdrInsp.GetStringNoNull(1);      //Set the inspector assignment
                cbi.ClearDirtyFlag();
            }

        }

        public bool Save()
        {
            string sql;
            bool rtn;       //return value

            rtn = false;

            foreach(CBusInsp lbi in BusInsps.Values)
            {
            
                sql = "select count(*) from BusInspDates where bus_id = '" + WWH.QuoteString(m_Bus_ID) + "' and insptype = '" + m_InspType + "' and ref = " + lbi.Ref;

                DbCommand cmdInsp = WWD.GetCommand(sql);
                Int32 InspRowCount = (Int32) cmdInsp.ExecuteScalar();

                if (InspRowCount == 0)
                {
                    //Create new record
                    sql = "insert into BusInspDates (Bus_ID, InspType, Ref, PrevInspDate, NextInspDate) values (";
                    sql += "'" + WWH.QuoteString(m_Bus_ID) + "', ";
                    sql += "'" + m_InspType + "', ";
                    sql += lbi.Ref.ToString() + ", ";
                    sql += "'" + lbi.LastInsp.ToString() + ", ";
                    sql += "'" + lbi.NextInsp.ToString() + "')";
                }
                else
                {
                    sql = "update BusInspDates set PrevInspDate = '" + lbi.LastInsp.ToString() + "', NextInspDate = '" + lbi.NextInsp.ToString() + "' where bus_id = '" + WWH.QuoteString(m_Bus_ID) + "' and insptype = '" + m_InspType + "' and ref = " + lbi.Ref;                
                }
                //Now execute the command

                cmdInsp = WWD.GetCommand(sql);
                cmdInsp.ExecuteNonQuery();

                //Next update the InsrBus table

                if (lbi.Insr == "<None>")
                {
                    sql = "Delete from InsrBus where bus_id = '" + WWH.QuoteString(m_Bus_ID) + "' and insptype = '" + m_InspType + "' and class = " + lbi.Ref;
                    cmdInsp = WWD.GetCommand(sql);
                    cmdInsp.ExecuteNonQuery();
                }
                else
                {
                    if (lbi.Insr.Length > 0)    //Update the existing data or add a new row
                    {
                        sql = "select count(*) from insrbus where bus_id = '" + WWH.QuoteString(m_Bus_ID) + "' and insptype = '" + m_InspType + "' and class = " + lbi.Ref;
                        cmdInsp.CommandText = sql;
                        if ((Int32) cmdInsp.ExecuteScalar() > 0)
                        {
                            //The row already exists
                            sql = "update insrbus set Insr_Id = '" + WWH.QuoteString(lbi.Insr) + "' where bus_id = '" + WWH.QuoteString(m_Bus_ID) + "' and insptype = '" + WWH.QuoteString(m_InspType) + "' and class = " + lbi.Ref;

                        }
                        else
	                    {
                            //Didn't find anything, so add new row
                            sql = "insert into insrbus (Bus_ID, InspType, Class, Insr) values (";
                            sql += "'" + WWH.QuoteString(m_Bus_ID) + "', ";
                            sql += "'" + m_InspType + "', ";
                            sql += lbi.Ref.ToString() + ", ";
                            sql += "'" + WWH.QuoteString(lbi.Insr) + "') ";
	                    }
                        cmdInsp.CommandText = sql;
                        cmdInsp.ExecuteNonQuery();
                    }
                }
            }

            m_IsDirty = false;
            rtn = true;
            return rtn;
        }
    }

}
