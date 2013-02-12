using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinWam6
{
    public class BusinessCustom : CCustomFields //,IVersion
    {
        private string MyKey="";

        public override void Initialize()
        {
            string sCap = "Select caption, list, DataType, [Default] from BusCustomDef order by sort";
            string sList = "Select [Text] from BusCustomList where caption = '{0}' order by sort";

            base.InitializeHelper(sCap, sList);

            foreach (CCustomLine cl in this.CustomLines)
            {
                cl.MaxLength = 50;
            }
        }

        public override void Load(string Key, string Key2 = "")
        {
            string DataSQL = "Select [Data] from BusCust where bus_id = '" + Key + "' and Caption = '{0}'";
            LoadHelper(DataSQL);
            MyKey = Key;
        }

        public override void Save()
        {
            TableWrapper tw = new TableWrapper("BusCust");

            foreach (CCustomLine cl in this.CustomLines)
            {
                tw["Bus_ID"] = MyKey;
                tw["Caption"] = cl.Caption;
                tw["Data"] = cl.CustomData;
                tw.Save();
            }
        }
    }

    public class InspectorCustom : CCustomFields
    {
        public override void Initialize()
        {
            string sCap = "Select caption, list, DataType, [Default] from InsrCustomDef order by sort";
            string sList = "Select [Text] from InsrCustomList where caption = '{0}' order by sort";

            base.InitializeHelper(sCap, sList);

            foreach (CCustomLine cl in this.CustomLines)
            {
                cl.MaxLength = 50;
            }
        }

        public override void Load(string Key, string Key2 = "")
        {
            string DataSQL = "Select [Data] from InsrCust where bus_id = '" + Key + "' and Caption = '{0}'";
            LoadHelper(DataSQL);
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }

    public class QSTCustom : CCustomFields
    {
        public override void Initialize()
        {
            string sCap = "Select caption, list, DataType, [Default] from QSTCustomDef where ref = {0} order by sort";
            string sList = "Select [Text] from QSTCustomList where caption = '{0}' and ref = {1} order by sort";

            base.InitializeHelper(sCap, sList);

            foreach (CCustomLine cl in this.CustomLines)
            {
                cl.MaxLength = 50;
            }
        }

        public override void Load(string Key, string Key2 = "")
        {
            string DataSQL = "Select [Data] from QSTCust where insp_id = '" + Key + "' and qst_id = " + Key2 + " and Caption = '{0}'";
            LoadHelper(DataSQL);
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }

    public class QST2Custom : CCustomFields
    {
        public override void Initialize()
        {
            string sCap = "Select caption, list, DataType, [Default] from QST2CustomDef where QST_ID = {0} order by sort";
            string sList = "Select [Text] from QST2CustomList where caption = '{0}' and QST_ID = {1} order by sort";

            base.InitializeHelper(sCap, sList);

            foreach (CCustomLine cl in this.CustomLines)
            {
                cl.MaxLength = 50;
            }
        }

        public override void Load(string Key, string Key2 = "")
        {
            string DataSQL = "Select [Data] from QST2Cust where insp_id = '" + Key + "' and qst_num = " + Key2 + " and Caption = '{0}'";
            LoadHelper(DataSQL);
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }

    public class DeviceCustom : CCustomFields
    {
        public override void Initialize()
        {
            string sCap = "Select CodeText as caption, CodeCmb as list, DataType, [Default] from DevCode where class = {0} and classcode > 3 and CodeStorage <> 0 order by sort";
            string sList = "Select CodeData as [Text] from DevData where ClassCode = {0} and Class = {1} order by sort";

            base.InitializeHelper(sCap, sList);

            foreach (CCustomLine cl in this.CustomLines)
            {
                cl.MaxLength = 24;
            }
        }

        public override void Load(string Key, string Key2 = "")
        {
            string DataSQL = "Select [Data] from DevSpec where ref = " + Key + " and Code = '{0}'";
            LoadHelper(DataSQL);
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }

    public class DeviceInspectionCustom : CCustomFields
    {
        public override void Initialize()
        {
            string sCap = "Select CodeText as caption, CodeCmb as list, DataType, [Default] from DevCode  where class = {0} and classcode > 3 order by sort";
            string sList = "Select CodeData as [Text] from DevData where ClassCode = {0} and Class = {1} order by sort";

            base.InitializeHelper(sCap, sList);

            foreach (CCustomLine cl in this.CustomLines)
            {
                cl.MaxLength = 24;
            }
        }

        public override void Load(string Key, string Key2 = "")
        {
            string DataSQL = "Select [Data] from DevDSpec where insp_id = '" + Key + "' and dev_id = " + Key2 + " and Code = '{0}'";
            LoadHelper(DataSQL);
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }
}
