using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinWam6
{
    class CDeviceInspection : IInspection
    {
        public bool ReadOnly { get; set; }
        public bool Locked { get { return false; } }
        public bool LockOverride { get; set; }
        //TODO
        //public CTimeInTimeOuts TimeInTimeOuts {get; set;}
        public DateTime OpenTime { get; set; }
        public bool NextInspDateOverride { get; set; }
        public string PrintedName { get; set; }
        public DateTime CreateDate { get; set; }
        //TODO
        //public eInspType InspType {get; set;}
        public string InspCode { get { return "D"; } }
        public System.Drawing.Bitmap BusSig { get; set; }
        public System.Drawing.Bitmap InspSig { get; set; }
        public string Notes { get; set; }
        public DateTime NextInspDate { get; set; }
        public DateTime AutoNextInspDate() { return System.DateTime.Now; }
        public CInspector Inspector { get; set; }
        public CBusiness Business { get; set; }
        public string Reason { get; set; }
        public DateTime InspDate { get; set; }
        public string Insp_ID { get; set; }
        public bool Save() {return true;}
        public bool Load(string InspID) {return true;}
    
    }

    class CDevD
    {

        public enum eDevTestType { Maintenance, Acceptance }

        public enum eResultWarning { OK, PassShouldFail, FailShouldPass }

        private struct tPreponderance
        {
            string Grade;
            double CumErr;
            int CumCount;
            double Average;
        }

        //public event ProdUsedChanged() {};

        bool ProdUsedOverride { get; set; }
        bool WarnedForResult { get; set; }
        eResultWarning ResultWarning
        {
            get
            {
                //TODO
                return eResultWarning.OK;
            }
        }

        string Notes { get; set; }

        //TODO
        //CDeviceInsp Parent {set;}

        bool Valid(bool CheckResult)
        {
            return true;
        }

        eDevTestType DevTestType { get; set; }

        float ProdUsed { get; set; }
        decimal Fee { get; set; }
        int Result { get; set; }
        string Seal { get; set; }
        string Other { get; set; }
        CDeviceInspectionCustom Custom { get; set; }  // Probably want to link to an instantiation of a particular Custom Fields class
        //TODO
        //CDevTests Tests {get; set;}
        Dictionary<string, CDevAttribute> Attributes { get; set; }
        //TODO
        //CDevice Device {get; set;}

        ////////////// Constructors

        CDevD()
        {
            //new Device
            Attributes = new Dictionary<string, CDevAttribute>();
            //new Tests
            Custom = new CDeviceInspectionCustom();
            this.WarnedForResult = false;
        }


        // Update the results for a single row in the inspection
        public void UpdateSingleResults(int row) { }

        private void UpdateWIPreponderance(/*curTest as CDevTest*/) { }

        private void UpdateVAPreponderance(/*curTest as CDevTest*/) { }

        private void UpdateGAPreponderance(/*curTest as CDevTest*/) { }

        private int PrepGradeIndex(string Grade, tPreponderance[] PrepArr) { return 0; }

        public void UpdateAllResuls() { }

        private void DeviceClassChanged() { }

        public void LoadSingleTemplate(/*CDevTemplate Template*/) { }

        public void LoadTemplate(bool ShowPrompt) { }

        public void LoadTempalte(bool ShowPrompt, string TemplateName) { }

        public void Load(string InspID, int curLine) { }

        public decimal SetDefaultFee() { return 0; }


    }

    
    class CDevAttribute
    {

        public enum DevAttributeStatus { Pass, Fail, NotChecked }

        DevAttributeStatus Status { get; set; }
        string Description { get; set; }

    }

}
