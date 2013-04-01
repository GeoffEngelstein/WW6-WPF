using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WinWam6.Utility
{
    public class LookupTable : SortedDictionary<double, double>
    {
        public enum TableType
        {
            FloorTable,
            CeilingTable
        }

        private TableType mode = TableType.FloorTable;

        public TableType Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        public bool BelowMinimum(double lookupValue)
        {
            return (this.First().Key > lookupValue);
        }

        public bool AboveMaximum(double lookupValue)
        {
            return (this.Last().Key < lookupValue);
        }

        public double LookupValue(double keyValue)
        {
            double lookupValue = 0;

            switch (this.Mode)
            {
                case TableType.FloorTable:
                    {
                        if (BelowMinimum(keyValue))
                        {
                            throw new ArgumentOutOfRangeException();
                            return 0;
                        }

                        lookupValue = this.First().Value;

                        foreach (KeyValuePair<double, double> kv in this)
                        {
                            if (kv.Key > keyValue)
                            {
                                return lookupValue;
                            }
                            else
                            {
                                lookupValue = kv.Value;
                            }
                        }
                        //Made it through the entire table, so return max value
                        return lookupValue;
                    }

                case TableType.CeilingTable:
                    {
                        if (AboveMaximum(keyValue)) throw new ArgumentOutOfRangeException();

                        foreach (KeyValuePair<double, double> kv in this)
                        {
                            if (kv.Key > keyValue)
                            {
                                return kv.Value;
                            }
                            
                        }
                        //theoretically should never get here due to initial AboveMaximum check, but just in case.
                        throw new ArgumentOutOfRangeException();
                        return 0;
                    }
                default:
                    throw new InvalidEnumArgumentException();
                    return 0;
            }
        }


    }
}
