using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.ComponentModel;
using WinWam6.Utility;

namespace WinWam6.Inspection.PCS
{
    public class PCSTest : INotifyPropertyChanged, IDisposable
    {
        public enum PCSTareType
        {
            None,
            Initial,
            Final
        }

        private readonly TableWrapper lObj;

        public PCSDetail Parent { get; set; }

        public Single GrossWeight
        {
            get { return (Single)lObj["Gross"]; }
            set
            {
                lObj["Gross"] = value;
                if (lObj.Fields["Gross"].RecentChange)
                {
                    NotifyPropertyChanged("GrossWeight");
                    RowChanged();
                }
            }
        }

        public String Insp_ID
        {
            get { return lObj["Insp_ID"] as string; }
            set { lObj["Insp_ID"] = value; if (lObj.Fields["Insp_ID"].RecentChange) NotifyPropertyChanged("Insp_ID"); }
        }

        public double MAV
        {
            //get { return Single.Parse(lObj["MAV"].ToString()); }
            get
            {
                double newMAV = Parent.Units.CalcMAV(NetWeight, PCSDetail.PCSMAVType.Normal);
                MAV = newMAV; 
                return newMAV;
            }

            set 
            { 
                lObj["MAV"] = value;
                if (lObj.Fields["MAV"].RecentChange)
                ResultChangeNotify(); 
            }
        }

        public Double NetWeight
        {
            get { return (double)lObj["MWeight"]; }
            set
            {
                lObj["MWeight"] = value;
                if (lObj.Fields["MWeight"].RecentChange)
                {
                    RowChanged();
                }
            }
        }

        public Int16 Pack_ID
        {
            get { return Int16.Parse(lObj["Pack_ID"].ToString()); }
            set { lObj["Pack_ID"] = value; NotifyPropertyChanged("Pack_ID"); }
        }

        public Double MarkedWeight
        {
            get { return (Double)lObj["PWeight"]; }
            set
            {
                lObj["PWeight"] = value;
                if (lObj.Fields["Pweight"].RecentChange)
                {
                  RowChanged();
                }
            }
        }

        public Single Tare
        {
            get { return (Single)lObj["Tare"]; }
            set
            {
                lObj["Tare"] = value;
                if (lObj.Fields["Tare"].RecentChange) 
                { 
                    NotifyPropertyChanged("Tare");
                    TareUpdateNotify();
                }                    
            }
        }

        public Int16 Test
        {
            get { return (Int16)lObj["Test"]; }
            set { lObj["Test"] = value; NotifyPropertyChanged("Test"); }
        }

        public double Volume
        {
            get
            {
                double volOut = Measured * Parent.VolumeMultiple;
                volOut = Math.Max(volOut, 0);
                if (Parent.Units.Category == "Count")
                    volOut = Math.Ceiling(volOut);
                Volume = volOut;
                return volOut; 
            }
            private set
            {
                lObj["Volume"] = value;
                if (lObj.Fields["Volume"].RecentChange)
                {
                    RowChanged();
                }
            }
        }

        public double Error
        {
            get { return (Volume - MarkedWeight); }
        }

        public decimal CostError
        {
            get { return ( (decimal)Error * Parent.CostPer); }
        }

        public PCSTareType TareType
        {
            get
            {
                if (Test > Parent.FinalTareSize) return PCSTareType.None;
                if (Test > Parent.InitialTareSize) return PCSTareType.Final;
                return PCSTareType.Initial;
            }
        }

        public double Measured
        {
            get
            {
                double GrayTotal = 0;

                if (Parent.GrayPct > 0)
                {
                    GrayTotal = MarkedWeight * (Parent.GrayPct / 100);
                }

                if (Parent.IsZeroTare())
                return (GrossWeight + GrayTotal);

                if (Parent.UseLineTare)
                {
                    return GrossWeight - Tare + GrayTotal;
                }
                else
                {
                    return GrossWeight - Parent.AverageTare;
                }
            }
        }


        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public PCSTest()
        {
            lObj = new TableWrapper("PackTest");
        }

        public PCSTest(string insp_ID, int pack_ID, int test_id)
        {
            lObj = new TableWrapper("PackTest");
            Load(insp_ID, pack_ID, test_id);
        }

        public void Load(string insp_ID, int pack_ID, int test_id)
        {
            lObj["Insp_ID"] = insp_ID;
            lObj["Pack_ID"] = pack_ID;
            lObj["Test"] = test_id;
            lObj.Load();
        }

        public bool Save()
        {
            return lObj.Save();
        }

        /// <summary>
        /// Centralized handling of changes to the row that are interrelated due to calculation, etc.
        /// </summary>
        private void RowChanged()
        {
            // First do the Result processing to make sure Average Tare, etc, is all correct
            
            ResultChangeNotify(); 
            
            // Now we can update the form if necessary

            NotifyPropertyChanged("MAV");
            NotifyPropertyChanged("NetWeight");
            NotifyPropertyChanged("MarkedWeight");
            NotifyPropertyChanged("Volume");
            NotifyPropertyChanged("Error");
            NotifyPropertyChanged("CostError");
            NotifyPropertyChanged("Measured");
        }

        public event EventHandler<EventArgs> PCSUpdateRequired; 

        private void ResultChangeNotify()
        {
            if (!Parent.UpdateInProcess)    //Help prevent cascades
            {
                if (PCSUpdateRequired != null)
                    PCSUpdateRequired(this, new EventArgs());
            }
        }

        public event EventHandler<EventArgs> PCSTareUpdate;

        /// <summary>
        /// Send a message to PCSDetail that the Tare has changed, to trigger Tare calculations. 
        /// This will automatically force an update to each Test row since Average Tare may have changed, so no need to deal with that here.
        /// </summary>
        private void TareUpdateNotify()
        {
            if (PCSTareUpdate != null) PCSTareUpdate(this, new EventArgs());
        }

        public void Dispose()
        {
            Parent = null;
        }

    }
}
