using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinWam6.Utility;
using System.Data.Common;

namespace WinWam6.Inspection.PCS
{
    class RcRt
    {
        LookupTable b10 = new LookupTable();
        LookupTable b30 = new LookupTable();
        LookupTable a12 = new LookupTable();
        LookupTable a24 = new LookupTable();
        LookupTable a48 = new LookupTable();

        private static readonly RcRt instance = new RcRt(); 

        private RcRt()
        {
            if (b10 == null)
            {
                string sql = "select rcrt, b10, b30, a12, a24, a48 from AltTare2002 order by rcrt";
                DbDataReader dr = WWD.GetSysReader(sql);

                while (dr.Read())
                {
                    b10.Add(dr.GetFloat(0), dr.GetFloat(1));
                    b30.Add(dr.GetFloat(0), dr.GetFloat(2));
                    a12.Add(dr.GetFloat(0), dr.GetFloat(3));
                    a24.Add(dr.GetFloat(0), dr.GetFloat(4));
                    a48.Add(dr.GetFloat(0), dr.GetFloat(5));
                }
                dr.Close();
            }
        }

        public static RcRt Instance
        {
            get { return instance; }
        }

        public int AlternateTare(double RcRt, int SampleSize, int InitialTare)
        {
            switch (SampleSize)
            {
                case 10:
                    {
                        if (b10.AboveMaximum(RcRt))
                            return SampleSize;
                        return (int)b10.LookupValue(RcRt);
                    }
                case 30:
                    {
                        if (b30.AboveMaximum(RcRt))
                            return SampleSize;
                        return (int)b30.LookupValue(RcRt);
                    }
                case 12:
                    {
                        if (a12.AboveMaximum(RcRt))
                            return SampleSize;
                        return (int)a12.LookupValue(RcRt);
                    }
                case 24:
                    {
                        if (a24.AboveMaximum(RcRt))
                            return SampleSize;
                        return (int)a24.LookupValue(RcRt);
                    }
                case 48:
                    {
                        if (a48.AboveMaximum(RcRt))
                            return SampleSize;
                        return (int)a48.LookupValue(RcRt);
                    }
                default:
                {
                    return InitialTare;
                }
            }
        }
    }
}
