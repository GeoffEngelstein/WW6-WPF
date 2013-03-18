using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinWam6;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WinWam6.Inspection.PCS 
{
    public class PCSInpsectionViewModel : INotifyPropertyChanged
    {
        private PCSInspectionView pcsView;
        private PCSInspectionViewAction pcsViewAction;
        private PCSInspection pcsInspection;

        // Constructors
        public PCSInpsectionViewModel() { }

        public PCSInpsectionViewModel(PCSInspectionView pcsView)
        {
            this.PCSView = pcsView;
        }

        // Properties
        public PCSInspectionView PCSView
        {
            get
            {
                return pcsView;
            }
            private set
            {
                pcsView = value;
                pcsViewAction = (PCSInspectionViewAction)value.ActionPaneContent;
                pcsViewAction.ActionSelected += ActionSelected;
            }
        }

        // Inspection Header values
        public string InspectionID
        {
            get
            {
                return pcsInspection.Insp_ID;
            }
            set
            {
                pcsInspection.Insp_ID = value;
                NotifyPropertyChanged("InspectionID");
            }
        }
        public DateTime? InspectionDate
        {
            get
            {
                return pcsInspection.Insp_Date;
            }
            set
            {
                pcsInspection.Insp_Date = value;
                NotifyPropertyChanged("InspectionDate");
            }
        }
        public string BusinessID
        {
            get
            {
                return pcsInspection.Bus_ID;
            }
            set
            {
                pcsInspection.Insp_ID = value;
                NotifyPropertyChanged("BusinessID");
            }
        }

        public ObservableCollection<PCSDetail> PCSDetails
        {
            get
            {
                return pcsInspection.PCSDetails;
            }
        }




        // Events and Methods
        private void ActionSelected(object sender, ActionEventArgs e)
        {
            string s = e.Action;
        }


        //INotifyPropertyChanged
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    
    }
}
