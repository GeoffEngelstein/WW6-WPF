using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinWam6
{
    public class ActionEventArgs : System.EventArgs
    {
        public readonly string Action;
        public ActionEventArgs(string action)
        {
            Action = action;
        }
    }

    public class MainTabEventArgs : System.EventArgs
    {
        public enum TabType { Business, PCS, DEV, UPC, ViewInspections, Inspector }
        public readonly string Index;
        public readonly TabType TabStyle;

        public MainTabEventArgs(TabType tabStyle, string index)
        {
            TabStyle = tabStyle;
            Index = index;
        }
    }

    public class PCSDetailChangeArgs : System.EventArgs
    {
        public readonly int DetailIndex;
        public PCSDetailChangeArgs(int detailIndex)
        {
            DetailIndex = detailIndex;
        }
    }
}
