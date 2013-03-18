using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data.Common;
using System.Data;

namespace WinWam6
{
    
    
    static class WWH
    {
        static public decimal CurrencyVal(string s)
        {
            //First strip a dollar sign if it exists
            if (s.Length > 0 && s.Substring(0, 1) == "$")
            {
                s = s.Substring(1, s.Length - 1);
            }
            //Now do the conversion
            try
            {
                decimal d ;
                decimal.TryParse(s, out d);
                return d;
            }
            catch (FormatException)
            {
                return 0;
            }
        }

        static public string QuoteString(string s)
        {
            return s.Replace("'","''");
        }

        static public string InspectionCode(InspectionBase.InspectionType inspectionType)
        {
            switch (inspectionType)
            {
                case InspectionBase.InspectionType.PCS:
                    return "P";

                case InspectionBase.InspectionType.DEV:
                    return "D";

                case InspectionBase.InspectionType.UPC:
                    return "U";

                case InspectionBase.InspectionType.QST:
                    return "Q";

                case InspectionBase.InspectionType.QSTD:
                    return "D";

                default: return "";
            }
        }

 /*       static public List<Inspector> Inspectors()
        {
            DbDataReader dr = WWD.GetReader("Select insr_id from inspector order by insr_id_user");
            
            var inspectors = new List<Inspector>();

            while (dr.Read())
            {
                Inspector inspector = new Inspector(dr[0].ToString());
                inspectors.Add(inspector);
            }

            return inspectors;
        }
        */
    }
       
}
