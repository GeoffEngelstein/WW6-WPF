using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WinWam6.Utility;

using System.Data.Common;

namespace WinWam6.Inspection.PCS
{
    public static class MAVTables
    {
        private static LookupTable LoadMAVTable(string TableName)
        {
            var lookupTable = new LookupTable();

            string sql = "select Wt, MAV from " + TableName + " order by Wt";

            DbDataReader dr = WWD.GetSysReader(sql);

            while (dr.Read())
            {
                lookupTable.Add(dr.GetFloat(0), dr.GetFloat(1));
            }

            dr.Close();

            return lookupTable;
        }

        //LB MAV
        private static LookupTable lbMAVTable;

        public static LookupTable lbMAV
        {
            get
            {
                if (null == lbMAVTable)
                {
                    lbMAVTable = LoadMAVTable("MAV");
                }
                return lbMAVTable;
            }
        }

        // VOLUME MAV
        private static LookupTable VolumeMAVTable;

        public static LookupTable VolumeMAV
        {
            get
            {
                if (null == VolumeMAVTable)
                {
                    VolumeMAVTable = LoadMAVTable("VolumeMAV");
                }
                return VolumeMAVTable;
            }
        }

        //USDA Normal
        private static LookupTable USDANMAVTable;

        public static LookupTable USDANMAV
        {
            get
            {
                if (null == USDANMAVTable)
                {
                    USDANMAVTable = LoadMAVTable("USDANMAV");
                }
                return USDANMAVTable;
            }
        }

        //Count
        private static LookupTable CountMAVTable;

        public static LookupTable CountMAV
        {
            get
            {
                if (null == CountMAVTable)
                {
                    CountMAVTable = LoadMAVTable("CountMAV");
                }
                return CountMAVTable;
            }
        }

        //Fluid Ounces
        private static LookupTable flozMAVTable;

        public static LookupTable flozMAV
        {
            get
            {
                if (null == flozMAVTable)
                {
                    flozMAVTable = LoadMAVTable("flozMAV");
                }
                return flozMAVTable;
            }
        }

        //Yards
        private static LookupTable YardMAVTable;

        public static LookupTable YardMAV
        {
            get
            {
                if (null == YardMAVTable)
                {
                    YardMAVTable = LoadMAVTable("YardMAV");
                }
                return YardMAVTable;
            }
        }

        //Meters
        private static LookupTable MeterMAVTable;

        public static LookupTable MeterMAV
        {
            get
            {
                if (null == MeterMAVTable)
                {
                    MeterMAVTable = LoadMAVTable("MeterMAV");
                }
                return MeterMAVTable;
            }
        }

        //cubic inches
        private static LookupTable cuinMAVTable;

        public static LookupTable cuinMAV
        {
            get
            {
                if (null == cuinMAVTable)
                {
                    cuinMAVTable = LoadMAVTable("cuinMAV");
                }
                return cuinMAVTable;
            }
        }

        //ounces
        private static LookupTable ozMAVTable;

        public static LookupTable ounceMAV
        {
            get
            {
                if (null == ozMAVTable)
                {
                    ozMAVTable = LoadMAVTable("MAVoz");
                }
                return ozMAVTable;
            }
        }

        //ounces
        private static LookupTable gramMAVTable;

        public static LookupTable gramMAV
        {
            get
            {
                if (null == gramMAVTable)
                {
                    gramMAVTable = LoadMAVTable("MAVgram");
                }
                return gramMAVTable;
            }
        }
    }
}