using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WinWam6.Inspection.DEV
{
    public class DEVInspection : InspectionBase
    {
        private ObservableCollection<DEVDetail> devDetails;
        private bool devDLoaded;

        public override string TooltipText
        {
            get
            {
                string s = "Device Inspection" + " Inspection ID " + this.Insp_ID;
                s += "\nLine Item Count: " + this.DevDetails.Count.ToString() + "\n";
                foreach (DEVDetail devD in this.DevDetails)
                {
                    s += devD.Ref.ToString() + "\n";
                }
                return s;
            }
        }

        public ObservableCollection<DEVDetail> DevDetails
        {
            get
            {
                if (!devDLoaded)
                {
                    LoadDevDetails();
                }
                return devDetails;
            }
            set
            {
                devDetails = value;
                devDLoaded = true;
            }
        }

        public DEVInspection() : base() { }

        public override void Load(string InspID)
        {
            base.LoadPrivate(InspID, "D");
        }

        public DEVInspection(string InspID)
            : base()
        {
            this.Load(InspID);
        }

        //Methods
        private void LoadDevDetails()
        {
            DEVDetail devDetail;

            devDetails = new ObservableCollection<DEVDetail>();

            string sql = "Select Dev_ID from DevD where insp_id ='" + this.Insp_ID + "' order by Dev_ID";
            DbDataReader rdr = WWD.GetReader(sql);

            while (rdr.Read())
            {
                devDetail = new DEVDetail();
                devDetail.Load(this.Insp_ID, rdr.GetInt16NoNull(0));
                devDetails.Add(devDetail);
            }
            devDLoaded = true;
        }

    }

 

    


 
}
