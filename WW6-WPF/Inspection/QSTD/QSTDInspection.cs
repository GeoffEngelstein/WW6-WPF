using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.ComponentModel;

namespace WinWam6.Inspection.QSTD
{
    class QSTDInspection : InspectionBase
    {

        public QSTDInspection() : base() { }

        public override void Load(string InspID)
        {
            base.LoadPrivate(InspID, "R");
        }

        public override string TooltipText
        {
            get
            {
                return "QST Deluxe Inspection" + " Inspection ID " + this.Insp_ID;
            }
        }
    }
}