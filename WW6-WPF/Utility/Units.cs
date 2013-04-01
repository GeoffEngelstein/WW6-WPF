using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using WinWam6.Inspection.PCS;

namespace WinWam6.Utility
{
    public enum MAVType {Normal, USDAStd, USDAFluid, Mulch, PE, Manual}
    
    public interface IMeasurementUnit
    {
        double CalcMAV(double netWeightIn, MAVType mavType);
        string StandardVolumeCaption { get; }
        string VolumeHeader { get; }
        bool UseVolumeFlag { get; }
        string StandardVolumeUnits { get; }
        string Name { get; }
        string FieldName { get; }
        double ConversionFactor { get; }
        string Category { get; }
        bool AllowPCS { get; }
        
        bool IsUnitName(string checkString);
    }

    class MeasurementUnits : Collection<IMeasurementUnit>
    {
       public MeasurementUnits() : base()
       {
           this.Add(new UnitOunce());
           this.Add(new UnitPound());
           this.Add(new Unitkg());
           this.Add(new Unitcm());
           this.Add(new UnitInch());
           this.Add(new Unitfoot());
           this.Add(new Unitmeter());
       }
    }

    class UnitOunce : IMeasurementUnit
    {
                public double CalcMAV(double netWeightIn, MAVType mavType)
        {
            return 0;
        }
        
        public string StandardVolumeCaption { get { return "oz"; } }
        public string VolumeHeader { get { return "oz"; } }
        public bool UseVolumeFlag { get { return false; } }
        public string StandardVolumeUnits
        {
            get { throw new NotImplementedException(); }
        }

        public string Name { get { return "ounce"; } }
        public string FieldName { get { return "oz"; } }
        public double ConversionFactor { get { return (1.0/16.0); } }
        public string Category { get { return "Weight"; } }
        public bool AllowPCS 
        {
            get { return true; }
        }

        public bool IsUnitName(string checkString)
        {
            string[] alternateNames = {"ounce", "oz", "ounces", "oz."};

            return alternateNames.Any(s => s == checkString.ToLower());
        }
    }


    public class UnitPound : IMeasurementUnit
    {
        public double CalcMAV(double netWeightIn, MAVType mavType)
        {
            switch (mavType)
            {
                case MAVType.Normal:
                    {
                        if (MAVTables.lbMAV.BelowMinimum(netWeightIn)) return (netWeightIn*0.1);
                        if (netWeightIn > 54.4) return (netWeightIn*0.02);
                        return (MAVTables.lbMAV.LookupValue(netWeightIn));
                    }

                default:
                    return 0;
            }

        }
        
        public string StandardVolumeCaption { get { return "lb"; } }
        public string VolumeHeader { get { return "lb"; } }
        public bool UseVolumeFlag { get { return false; } }
        public string StandardVolumeUnits
        {
            get { throw new NotImplementedException(); }
        }

        public string Name { get { return "pound"; } }
        public string FieldName { get { return "lb"; } }
        public double ConversionFactor { get { return 1; } }
        public string Category { get { return "Weight"; } }
        public bool AllowPCS 
        {
            get { return true; }
        }

        public bool IsUnitName(string checkString)
        {
            string[] alternateNames = {"lb", "lbs", "lb.", "pounds", "pound"};

            return alternateNames.Any(s => s == checkString.ToLower());
        }
    }

    class Unitkg : IMeasurementUnit
    {
        public double CalcMAV(double netWeightIn, MAVType mavType)
        {
            return 0;
        }

        public string StandardVolumeCaption { get { return "kg"; } }
        public string VolumeHeader { get { return "kg"; } }
        public bool UseVolumeFlag { get { return false; } }
        public string StandardVolumeUnits
        {
            get { throw new NotImplementedException(); }
        }

        public string Name { get { return "kilogram"; } }
        public string FieldName { get { return "kg"; } }
        public double ConversionFactor { get { return (2.2); } }
        public string Category { get { return "Weight"; } }
        public bool AllowPCS
        {
            get { return true; }
        }

        public bool IsUnitName(string checkString)
        {
            string[] alternateNames = { "kg", "kilogram", "kilograms" };

            return alternateNames.Any(s => s == checkString.ToLower());
        }
    }

    class UnitInch : IMeasurementUnit
    {
        public double CalcMAV(double netWeightIn, MAVType mavType)
        {
            return 0;
        }

        public string StandardVolumeCaption { get { return "in"; } }
        public string VolumeHeader { get { return "in"; } }
        public bool UseVolumeFlag { get { return false; } }
        public string StandardVolumeUnits
        {
            get { throw new NotImplementedException(); }
        }

        public string Name { get { return "inch"; } }
        public string FieldName { get { return "in"; } }
        public double ConversionFactor { get { return (2.2); } }
        public string Category { get { return "Length"; } }
        public bool AllowPCS
        {
            get { return true; }
        }

        public bool IsUnitName(string checkString)
        {
            string[] alternateNames = { "in", "inches", "ins.", "in." };

            return alternateNames.Any(s => s == checkString.ToLower());
        }
    }

    class Unitcm : IMeasurementUnit
    {
        public double CalcMAV(double netWeightIn, MAVType mavType)
        {
            return 0;
        }

        public string StandardVolumeCaption { get { return "cm"; } }
        public string VolumeHeader { get { return "cm"; } }
        public bool UseVolumeFlag { get { return false; } }
        public string StandardVolumeUnits
        {
            get { throw new NotImplementedException(); }
        }

        public string Name { get { return "centimeter"; } }
        public string FieldName { get { return "cm"; } }
        public double ConversionFactor { get { return (2.2); } }
        public string Category { get { return "Length"; } }
        public bool AllowPCS
        {
            get { return true; }
        }

        public bool IsUnitName(string checkString)
        {
            string[] alternateNames = { "cm", "centimeter", "centimetre" };

            return alternateNames.Any(s => s == checkString.ToLower());
        }
    }

    class Unitfoot : IMeasurementUnit
    {
        public double CalcMAV(double netWeightIn, MAVType mavType)
        {
            return 0;
        }

        public string StandardVolumeCaption { get { return "ft"; } }
        public string VolumeHeader { get { return "ft"; } }
        public bool UseVolumeFlag { get { return false; } }
        public string StandardVolumeUnits
        {
            get { throw new NotImplementedException(); }
        }

        public string Name { get { return "foot"; } }
        public string FieldName { get { return "ft"; } }
        public double ConversionFactor { get { return (2.2); } }
        public string Category { get { return "Length"; } }
        public bool AllowPCS
        {
            get { return true; }
        }

        public bool IsUnitName(string checkString)
        {
            string[] alternateNames = { "foot", "feet", "ft", "ft." };

            return alternateNames.Any(s => s == checkString.ToLower());
        }
    }

    class Unitmeter : IMeasurementUnit
    {
        public double CalcMAV(double netWeightIn, MAVType mavType)
        {
            return 0;
        }

        public string StandardVolumeCaption { get { return "m"; } }
        public string VolumeHeader { get { return "m"; } }
        public bool UseVolumeFlag { get { return false; } }
        public string StandardVolumeUnits
        {
            get { throw new NotImplementedException(); }
        }

        public string Name { get { return "meter"; } }
        public string FieldName { get { return "m"; } }
        public double ConversionFactor { get { return (2.2); } }
        public string Category { get { return "Length"; } }
        public bool AllowPCS
        {
            get { return true; }
        }

        public bool IsUnitName(string checkString)
        {
            string[] alternateNames = { "m", "meter", "metre", "m." };

            return alternateNames.Any(s => s == checkString.ToLower());
        }
    }
}
