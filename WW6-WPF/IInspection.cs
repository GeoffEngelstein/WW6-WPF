using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinWam6
{
    interface IInspection
    {
        bool ReadOnly { get; set; }
        bool Locked { get; }
        bool LockOverride { get; set; }
        //TODO
        //public CTimeInTimeOuts TimeInTimeOuts {get; set;}
        DateTime OpenTime { get; set; }
        bool NextInspDateOverride { get; set; }
        string PrintedName { get; set; }
        DateTime CreateDate { get; set; }
        //TODO
        //public eInspType InspType {get; set;}
        string InspCode { get; }
        System.Drawing.Bitmap InspSig { get; set; }
        System.Drawing.Bitmap BusSig { get; set; }
        string Notes { get; set; }
        DateTime NextInspDate { get; set; }
        DateTime AutoNextInspDate();
        CInspector Inspector { get; set; }
        CBusiness Business { get; set; }
        string Reason { get; set; }
        DateTime InspDate { get; set; }
        string Insp_ID { get; set; }
        bool Save();
        bool Load(string InspID);
          
    
    }
}
