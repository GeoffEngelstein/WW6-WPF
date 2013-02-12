using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.ComponentModel;

namespace WinWam6
{
    class UPCInspection : InspectionBase
    {


        public UPCInspection() { }

        public override void Load(string InspID)
        {
            base.LoadPrivate(InspID, "U");
        }

        public override string TooltipText
        {
            get
            {
                return "UPC Inspection" + " Inspection ID " + this.Insp_ID;
            }
        }
    }
}