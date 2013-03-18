using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace WinWam6.Utility
{
    public class UnitConversionViewModel
    {
        private MeasurementUnits units = new MeasurementUnits();

        public ObservableCollection<string> Categories
        {
            get
            {
                var mOutput = new ObservableCollection<string>();

                IEnumerable<string> rslt = units.Select(p => p.Category).Distinct();

                foreach (string s in rslt)
                {
                    mOutput.Add(s);
                }
                return mOutput;
            }
        }

        public IEnumerable<IMeasurementUnit> SourceUnits(string category)
        {
            IEnumerable<IMeasurementUnit> rslt = units.Where(p => category.Equals(p.Category)).OrderBy(p => p.Name).Select(p => p);
            return rslt;
        }
        
        public IEnumerable<IMeasurementUnit> DestinationUnits(string category)
        {
            IEnumerable<IMeasurementUnit> rslt = units.Where(p => category.Equals(p.Category)).OrderBy(p => p.Name).Select(p => p);
            return rslt;
        }

        public double SourceValue { get; set; }
        public double DestinationValue { get; set; }
        public int DecimalPlaces { get; set; }
    }
}
