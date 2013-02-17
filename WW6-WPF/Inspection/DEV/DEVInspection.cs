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
        private ObservableCollection<DevDetail> devDetails;
        private bool devDLoaded;

        public override string TooltipText
        {
            get
            {
                string s = "Device Inspection" + " Inspection ID " + this.Insp_ID;
                s += "\nLine Item Count: " + this.DevDs.Count.ToString() + "\n";
                foreach (DevDetail devD in this.DevDs)
                {
                    s += devD.Ref.ToString() + "\n";
                }
                return s;
            }
        }

        public ObservableCollection<DevDetail> DevDs
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

        //Methods
        private void LoadDevDetails()
        {
            DevDetail devDetail;

            devDetails = new ObservableCollection<DevDetail>();

            string sql = "Select Dev_ID from DevD where insp_id ='" + this.Insp_ID + "' order by Dev_ID";
            DbDataReader rdr = WWD.GetReader(sql);

            while (rdr.Read())
            {
                devDetail = new DevDetail();
                devDetail.Load(this.Insp_ID, rdr.GetInt16NoNull(0));
                devDetails.Add(devDetail);
            }
            devDLoaded = true;
        }

    }

 

    


 
}
