﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WinWam6.Inspection.PCS
{
    class PCSInspection : InspectionBase
    {

        private ObservableCollection<PCSDetail> pcsDetails = new ObservableCollection<PCSDetail>();
        private bool pcsDetailLoaded = false;

        // Package Checking Specific Properties and Methods

        public override string TooltipText
        {
            get
            {
                string s = "Package Inspection" + " Inspection ID " + this.Insp_ID;
                s += "\nLine Item Count: " + this.PCSDetails.Count.ToString()+"\n";
                foreach (PCSDetail pcsDetail in this.PCSDetails)
                {
                    s += pcsDetail.Commodity + " " + pcsDetail.Brand + " " + pcsDetail.LotSize.ToString() + "\n";
                }
                return s;
            }
        }

        public ObservableCollection<PCSDetail> PCSDetails
        {
            get
            {
                if (!pcsDetailLoaded)
                {
                    LoadPCSDetails();
                }
                return pcsDetails;
            }
            set
            {
                pcsDetails = value;
                pcsDetailLoaded = true;
            }
        }


        // Constructor
        public PCSInspection() : base() { }

        public PCSInspection(string InspID) : base()
        {
            this.Load(InspID);
        }

        public override void Load(string InspID)
        {
            base.LoadPrivate(InspID, "P");
        }

        //Methods
        private void LoadPCSDetails()
        {
            PCSDetail pcsDetail;

            //pcsDetails.Clear();

            string sql = "Select Pack_ID from PackD where insp_id ='" + this.Insp_ID + "' order by Pack_ID";
            DbDataReader rdr = WWD.GetReader(sql);

            while (rdr.Read())
            {
                pcsDetail = new PCSDetail();
                pcsDetail.Load(this.Insp_ID, rdr.GetInt16NoNull(0));
                pcsDetails.Add(pcsDetail);
            }
            pcsDetailLoaded = true;
        }

        public bool Save()
        {
            bool result= true;
            foreach (PCSDetail pd in pcsDetails)
            {
                if (!pd.Save())
                    result = false;
            }

            if (!base.lObj.Save())
                result = false;

            return result;
        }
    }

 
 
}
