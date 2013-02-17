using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.ComponentModel;

namespace WinWam6.Inspection.QST
{
    class QSTInspection : InspectionBase
    {


        public QSTInspection() : base() { }

        public override void Load(string InspID)
        {
            base.LoadPrivate(InspID, "Q");
        }

        public override string TooltipText
        {
            get
            {
                return "QST Inspection" + " Inspection ID " + this.Insp_ID;
            }
        }
    }
}
