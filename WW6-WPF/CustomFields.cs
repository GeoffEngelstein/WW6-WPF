using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace WinWam6
{
    public abstract class CCustomFields 
    {

        private List<CCustomLine> m_customlines = new List<CCustomLine>();
        private bool m_isdirty;

        public List<CCustomLine> CustomLines
        {
            get
            {
                return m_customlines;
            }
            set
            {
                m_customlines = value;
                m_isdirty = true;
            }
        }

        public bool IsDirty
        {
            get
            {
                return m_isdirty;
            }
        }

        abstract public void Initialize();

        public CCustomLine this[string caption]
        {
            get
            {
                foreach (CCustomLine cl in m_customlines)
                {
                    if (cl.Caption == caption)
                    {
                        return cl;
                    }
                }
                //Did not find it - Raise an error? For now return blank row
                CCustomLine ncl = new CCustomLine();
                return ncl;
            }
        }
        
        /// <param name="CaptionSQL">SQL that gives the captions and format data. Caption, List, DataType, Default</param>
        /// <param name="ListSQL">SQL that gives the PickListData - {0} in string for CAPTION reference</param>
        protected void InitializeHelper(string CaptionSQL, string ListSQL)
        {
            CCustomLine cl;
            DbDataReader rdr = WWD.GetReader(CaptionSQL);
            string sPickSQL;

            while (rdr.Read())
            {
                cl = new CCustomLine();
                cl.Caption = rdr.GetString(0);
                cl.IsList = (rdr.GetInt16NoNull(1) != 0);

                // Check to see if we need to read the PickList data
                if (cl.IsList)
                {
                    sPickSQL = string.Format(ListSQL,cl.Caption);
                    DbDataReader rdrPick = WWD.GetReader(sPickSQL);
                    
                    while (rdrPick.Read())
                    {
                        cl.PickList.Add(rdrPick.GetStringNoNull(0));
                    }
                }

                cl.DataTypeCode = rdr.GetStringNoNull(2);
                cl.DefaultData = rdr.GetStringNoNull(3);
                cl.CustomData = cl.DefaultData;  // Set up initial default value. Will be overwritten on Load
                m_customlines.Add(cl);
            }



        }

        abstract public void Load(string Key, string Key2 = "");

        /// <summary>
        /// Assists the subclasses with Loading in a coherent way.
        /// </summary>
        /// <param name="DataSQL"></param>
        protected void LoadHelper(string DataSQL)
        {
            string sSQL;

            foreach (CCustomLine cl in m_customlines){
                sSQL = string.Format(DataSQL, cl.Caption);      //plug in the right caption into the SQL
                DbDataReader rdr = WWD.GetReader(sSQL);
                while (rdr.Read())
                {
                    cl.CustomData = rdr.GetStringNoNull(0);
                }

            }
            m_isdirty = false;
            
        }

        abstract public void Save();

        public void ClearDirtyFlag()
        {
            foreach (CCustomLine cl in m_customlines)
            {
                cl.IsDirty = false;
            }
        }



        /// <param name="DataSQL">SQL for the data fields, keyed by caption</param>
        protected void SaveHelper(string DataSQL)
        {
            throw new System.NotImplementedException();
        }
    }

    public class CCustomLine
    {

        public enum CustDataType { 
           cdText,
           cdDate,
           cdInteger,
           cdNumber,
           cdCurrency,
           cdBusinessLink,
           cdInspectorLink,
           cdBoolean
           }
        
        private string m_caption;
        private string m_customdata;
        private CustDataType m_datatype;
        private string m_defaultdata;
        private bool m_isdirty;
        private bool m_islist;
        private int m_maxlength;
        private List<string> m_picklist = new List<string>();
        private int m_storagemode;


   

        public string Caption
        {
            get
            {
                return m_caption;
                
            }
            set
            {
                m_caption = value;
                m_isdirty = true;
            }
        }

        public string CustomData
        {
            get
            {
                return m_customdata;
            }
            set
            {
                //Cast the data into the correct format
                switch (this.DataType)
                {
                    case CustDataType.cdText:
                        if (value.Length > m_maxlength)
                        { m_customdata = value.Substring(0, m_maxlength); }
                        else
                        { m_customdata = value; }
                        break;
               
                    case CustDataType.cdDate:
                        DateTime d;
                        try
                        {
                            DateTime.TryParse(value, out d);
                            if (d.Year < 1900) 
                            {
                                m_customdata = "";
                            }
                            else
                            {
                                m_customdata = d.ToShortDateString();
                            }
                        }
                        catch (FormatException)
                        {
                            m_customdata = "";
                        }
                        break;
                    
                    case CustDataType.cdInteger:
                        try
                        {
                            int i;
                            int.TryParse(value, out i);
                            m_customdata = i.ToString();
                        }
                        catch (FormatException)
                        {
                            m_customdata = "0";
                        }
                        break;

                    case CustDataType.cdNumber:
                        try
                        {
                            double f;
                            double.TryParse(value, out f);
                            m_customdata = f.ToString();
                        }
                        catch (FormatException)
                        {
                            m_customdata = "0";
                        }
                        break;

                    case CustDataType.cdCurrency:
                        m_customdata = WWH.CurrencyVal(value).ToString("C");
                        break;

                    case CustDataType.cdInspectorLink:
                        //TODO - Need to implement Inspector Link
                        m_customdata = "";
                        break;

                    case CustDataType.cdBusinessLink:
                        //TODO - Need to implement Business Link
                        m_customdata = "";
                        break;

                    case CustDataType.cdBoolean:
                        bool b = Convert.ToBoolean(value);
                        m_customdata = b.ToString();
                        break;

                    default:
                        m_customdata = value;
                        break;
                }
                m_isdirty = true;
            }
        }

        public bool IsList
        {
            get
            {
                return m_islist;
            }
            set
            {
                m_islist = value;
                m_isdirty = true;
            }
        }

        public int StorageMode
        {
            get
            {
                return m_storagemode;
            }
            set
            {
                m_storagemode = value;
                m_isdirty= true;
            }
        }

        public  bool IsDirty
        {
            get
            {
                return m_isdirty;
            }
            set
            {
                m_isdirty = value;
            }
        }

        public CustDataType DataType
        {
            get
            {
                return m_datatype;
            }
            set
            {
                m_datatype = value;
                m_isdirty = true;
            }
        }

        public string DataTypeCode
        {
            get
            {
                switch (m_datatype) 
                {
                    case CustDataType.cdText:
                        return "T";
                    case CustDataType.cdDate:
                        return "D";
                    case CustDataType.cdInteger:
                        return "I";
                    case CustDataType.cdNumber:
                        return "N";
                    case CustDataType.cdCurrency:
                        return "C";
                    case CustDataType.cdBusinessLink:
                        return "B";
                    case CustDataType.cdInspectorLink:
                        return "R";
                default:
                        return "T";
                }

            }
            set
            {
                switch (value) 
                {
                    case "T":
                       m_datatype= CustDataType.cdText;
                       break;
                    case "D":
                       m_datatype = CustDataType.cdDate;
                       break;
                    case "I":
                       m_datatype = CustDataType.cdInteger;
                       break;
                    case "N":
                       m_datatype = CustDataType.cdNumber;
                       break;
                    case "C":
                       m_datatype = CustDataType.cdCurrency;
                       break;
                    case "B":
                       m_datatype = CustDataType.cdBusinessLink;
                       break;
                    case "R":
                       m_datatype = CustDataType.cdInspectorLink;
                       break;
                    default:
                       m_datatype = CustDataType.cdText;
                        break;
                }

            }
        }
        public string DefaultData
        {
            get
            {
                return m_defaultdata;
            }
            set
            {
                m_defaultdata = value;
                m_isdirty = true;
            }
        }

        public List<string> PickList
        {
            get
            {
                return m_picklist;
            }
            set
            {
                m_picklist = value;
                m_isdirty = true;
            }
        }

        public int MaxLength
        {
            get
            {
                return m_maxlength;
            }
            set
            {
                m_maxlength = value;
                m_isdirty = true;
            }
        }

        /// <summary>
        /// Converts the data type enum to/from a string
        /// </summary>
        public string DataTypeText
         {
            get
            {
   
                    switch (m_datatype) 
                    
                    {

                        case CustDataType.cdText:
                            return "Text";
                        case CustDataType.cdDate:
                            return "Date";
                        case CustDataType.cdInteger:
                            return "Integer";
                        case CustDataType.cdNumber:
                            return "Number";
                        case CustDataType.cdCurrency:
                            return "Currency";
                        case CustDataType.cdBusinessLink:
                            return "Business Link";
                        case CustDataType.cdInspectorLink:
                            return "Inspector Link";
                        case CustDataType.cdBoolean:
                            return "Boolean";
                        default:
                            return "Text";
         
                    }
            }
            set
            {
                
                switch (value.ToUpper())
                {
                    case "TEXT":
                        m_datatype = CustDataType.cdText;
                        break;
                    case "DATE":
                        m_datatype = CustDataType.cdDate;
                        break;
                    case "INTEGER":
                        m_datatype = CustDataType.cdInteger;
                        break;
                    case "NUMBER":
                        m_datatype = CustDataType.cdNumber;
                        break;
                    case "CURRENCY":
                        m_datatype = CustDataType.cdCurrency;
                        break;
                    case "BUSINESS LINK":
                        m_datatype = CustDataType.cdBusinessLink;
                        break;
                    case "INSPECTOR LINK":
                        m_datatype = CustDataType.cdInspectorLink;
                        break;
                    case "BOOLEAN":
                        m_datatype = CustDataType.cdBoolean;
                        break;
                    default:
                        m_datatype = CustDataType.cdText;
                        break;

                }
            }
            
        }


    }
}
