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
        private static bool lbMAVisLoaded = false;
        private static LookupTable lbMAVTable;

        public static LookupTable lbMAV
        {
            get
            {

                if (!lbMAVisLoaded)
                {
                    lbMAVTable = LoadMAVTable("MAV");
                    lbMAVisLoaded = true;
                }
                return lbMAVTable;

            }
        }

        // VOLUME MAV
        private static bool VolumeMAVisLoaded = false;
        private static LookupTable VolumeMAVTable;

        public static LookupTable VolumeMAV
        {
            get
            {

                if (!VolumeMAVisLoaded)
                {
                    VolumeMAVTable = LoadMAVTable("VolumeMAV");
                    VolumeMAVisLoaded = true;
                }
                return VolumeMAVTable;
            }
        }

        //USDA Normal
        private static bool USDANMAVisLoaded = false;
        private static LookupTable USDANMAVTable;

        public static LookupTable USDANMAV
        {
            get
            {

                if (!USDANMAVisLoaded)
                {
                    USDANMAVTable = LoadMAVTable("USDANMAV");
                    USDANMAVisLoaded = true;
                }
                return USDANMAVTable;
            }
        }

        //Count
        private static bool CountMAVisLoaded = false;
        private static LookupTable CountMAVTable;

        public static LookupTable CountMAV
        {
            get
            {

                if (!CountMAVisLoaded)
                {
                    CountMAVTable = LoadMAVTable("CountMAV");
                    CountMAVisLoaded = true;
                }
                return CountMAVTable;
            }
        }

        //Fluid Ounces
        private static bool flozMAVisLoaded = false;
        private static LookupTable flozMAVTable;

        public static LookupTable flozMAV
        {
            get
            {

                if (!flozMAVisLoaded)
                {
                    flozMAVTable = LoadMAVTable("flozMAV");
                    flozMAVisLoaded = true;
                }
                return flozMAVTable;
            }
        }

        //Yards
        private static bool YardMAVisLoaded = false;
        private static LookupTable YardMAVTable;

        public static LookupTable YardMAV
        {
            get
            {
                if (!YardMAVisLoaded)
                {
                    YardMAVTable = LoadMAVTable("YardMAV");
                    YardMAVisLoaded = true;
                }
                return YardMAVTable;
            }
        }

        //Meters
        private static bool MeterMAVisLoaded = false;
        private static LookupTable MeterMAVTable;

        public static LookupTable MeterMAV
        {
            get
            {

                if (!MeterMAVisLoaded)
                {
                    MeterMAVTable = LoadMAVTable("MeterMAV");
                    MeterMAVisLoaded = true;
                }
                return MeterMAVTable;
            }
        }

        //cubic inches
        private static bool cuinMAVisLoaded = false;
        private static LookupTable cuinMAVTable;

        public static LookupTable cuinMAV
        {
            get
            {
                if (!cuinMAVisLoaded)
                {
                    cuinMAVTable = LoadMAVTable("cuinMAV");
                    cuinMAVisLoaded = true;
                }
                return cuinMAVTable;
            }
        }

        //ounces
        private static bool ozMAVisLoaded = false;
        private static LookupTable ozMAVTable;

        public static LookupTable ounceMAV
        {
            get
            {

                if (!ozMAVisLoaded)
                {
                    ozMAVTable = LoadMAVTable("MAVoz");
                    ozMAVisLoaded = true;
                }
                return ozMAVTable;
            }
        }

        //ounces
        private static bool gramMAVisLoaded = false;
        private static LookupTable gramMAVTable;

        public static LookupTable gramMAV
        {
            get
            {

                if (!gramMAVisLoaded)
                {
                    gramMAVTable = LoadMAVTable("MAVgram");
                    gramMAVisLoaded = true;
                }
                return gramMAVTable;
            }
        }
    }
}
