using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.Common;

namespace WinWam6
{
    public class InspectionReasons : Dictionary<string, string>
    {
        public InspectionReasons()
            : base()
        {
            if (WWD.DatabaseIsOpen())
            {
                DbDataReader dr = WWD.GetReader("Select SysFile_Desc from wwsysfiles where sysClass_id = 3 order by SysFile_Desc");

                while (dr.Read())
                {
                    this.Add(dr[0].ToString(), dr[0].ToString());
                }

            }
        }
    }
    public class CommodityCategories : Dictionary<string, string>
    {
        public CommodityCategories()
            : base()
        {
            if (WWD.DatabaseIsOpen())
            {
                DbDataReader dr = WWD.GetReader("Select SysFile_Desc from wwsysfiles where sysClass_id = 2 order by SysFile_Desc");

                while (dr.Read())
                {
                    this.Add(dr[0].ToString(), dr[0].ToString());
                }

            }
        }
    }
}
