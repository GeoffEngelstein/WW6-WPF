using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Collections.ObjectModel;

namespace WinWam6
{
    class Conversion
    {
        private CConversionCats Categories = new CConversionCats();
        
        public Conversion()
        {
            // Creator - Initialize all class values

        }

        public double Convert(string ActiveCat, string SourceUnit, string DestUnit, double Value)
        {
            //Main conversion routine
            return 0;
        }
    }

    class CConversionCats
    {
        private Dictionary<string, CConversionCat> m_cats = new Dictionary<string, CConversionCat>();
        public Dictionary<string, CConversionCat> Categories { get { return m_cats; } set { m_cats = value; } }
    }

    class CConversionCat
    {
        public string Category {get; set;}
        public CConversionUnits Units {get; set;}
    }

    class CConversionUnits
    {
        private Dictionary<string, CConversionUnit> m_units = new Dictionary<string,CConversionUnit>();

        public Dictionary<string, CConversionUnit> Units {get {return m_units;} set {m_units = value;} }

        public CConversionUnit Add (string Unit, double ConversionFactor)
        {
            CConversionUnit mu = new CConversionUnit(Unit, ConversionFactor);
            m_units.Add(Unit, mu);
            return mu;
        }
    }

    class CConversionUnit
    {
        private string m_Unit;
        private double m_ConversionFactor;

        //Constructor
        public CConversionUnit(string Unit, double ConversionFactor)
        {
            m_Unit = Unit;
            m_ConversionFactor = ConversionFactor;
        }
        public string Unit { get { return m_Unit; } set { m_Unit = value; } }
        public double ConversionFactor { get { return m_ConversionFactor; } set { m_ConversionFactor = value; } }
        
    }

    class CConversionLinq 
    {
        //Create everything as a flat array of classes, and then use LINQ to pull out the relevant values
        private List<CConversionLinqUnit> m_list = new List<CConversionLinqUnit>();

        public CConversionLinq()
        {
            //Constructor initializes the internal array
            
            CConversionLinqUnit cclu;

            WWD.OpenDatabase();
            
            DbDataReader rdr = WWD.GetSysReader("select cat, unit, factor from [convert] order by cat");

            
            while (rdr.Read())
            {
                cclu = new CConversionLinqUnit(rdr.GetStringNoNull(0), rdr.GetStringNoNull(1), rdr.GetDoubleNoNull(2));
                m_list.Add(cclu);
            }
            rdr.Close();
        }

        public ObservableCollection<string> Categories
        {
            get
            {
                var m_output = new ObservableCollection<string>();

                IEnumerable<string> rslt = m_list.Select(p => p.Category).Distinct();

                foreach (string s in rslt)
                {
                    m_output.Add(s);
                }
                return m_output;
            }
        }

        public IEnumerable<string> Units(string Category)
        {
            IEnumerable<string> rslt = m_list.Where(p => p.Category.Equals(Category)).OrderBy(p => p.Unit).Select(p => p.Unit);
            return rslt;
        }

        public Double Convert(string Category, string SrcUnit, string DestUnit, double InputValue)
        {
            double SrcConvFactor = m_list.Where(p => p.Category.Equals(Category)).Where(p => p.Unit.Equals(SrcUnit)).Select(p => p.ConversionFactor).First();
            double DestConvFactor = m_list.Where(p => p.Category.Equals(Category)).Where(p => p.Unit.Equals(DestUnit)).Select(p => p.ConversionFactor).First();

            return InputValue * DestConvFactor / SrcConvFactor;
        }

        public string BindToMe { get { return this.Categories.ElementAt(0); } }
    }

    class CConversionLinqUnit
    {
        public string Category { get; set; }
        public string Unit { get; set; }
        public double ConversionFactor { get; set; }
        
        public CConversionLinqUnit(string Category, string Unit, double ConversionFactor)
        {
            this.Category = Category;
            this.Unit = Unit;
            this.ConversionFactor = ConversionFactor;
        }
    }

}
