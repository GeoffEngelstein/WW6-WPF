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

        static public string InspectionCode(InspectionHeader.InspectionType inspectionType)
        {
            switch (inspectionType)
            {
                case InspectionHeader.InspectionType.PCS:
                    return "P";

                case InspectionHeader.InspectionType.DEV:
                    return "D";

                case InspectionHeader.InspectionType.UPC:
                    return "U";

                case InspectionHeader.InspectionType.QST:
                    return "Q";

                case InspectionHeader.InspectionType.QSTD:
                    return "D";

                default: return "";
            }
        }

    }

}
