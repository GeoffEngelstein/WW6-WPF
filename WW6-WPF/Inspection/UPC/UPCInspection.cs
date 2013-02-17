using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WinWam6.Inspection.UPC
{
    class UPCInspection : InspectionBase
    {
        private ObservableCollection<UPCDetail> upcDetails;
        private bool upcDetailLoaded = false;

        public UPCInspection() { }

        public override void Load(string InspID)
        {
            base.LoadPrivate(InspID, "U");
        }

        public override string TooltipText
        {
            get
            {
                string s = "UPC Inspection" + " Inspection ID " + this.Insp_ID + "\n";
                s += "Record count: " + this.UPCDetails.Count().ToString() + "\n";

                foreach (UPCDetail upcDetail in this.UPCDetails)
                {
                    s += upcDetail.Commodity + " " + upcDetail.Scan.ToString() + " " + upcDetail.Shelf.ToString() + "\n";
                }

                return s;
            }
        }

        public ObservableCollection<UPCDetail> UPCDetails
        {
            get
            {
                if (!upcDetailLoaded)
                {
                    LoadUPCDetails();
                }
                return upcDetails;
            }
            set
            {
                upcDetails = value;
                upcDetailLoaded = true;
            }
        }

        //Methods
        private void LoadUPCDetails()
        {
            UPCDetail upcDetail;

            upcDetails = new ObservableCollection<UPCDetail>();

            string sql = "Select UPC_ID from UPCD where insp_id ='" + this.Insp_ID + "' order by UPC_ID";
            DbDataReader rdr = WWD.GetReader(sql);

            while (rdr.Read())
            {
                upcDetail = new UPCDetail();
                upcDetail.Load(this.Insp_ID, rdr.GetInt16NoNull(0));
                upcDetails.Add(upcDetail);
            }
            upcDetailLoaded = true;
        }

    }


}