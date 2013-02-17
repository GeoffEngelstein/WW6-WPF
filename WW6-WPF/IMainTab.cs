using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinWam6
{
    // This interface defines the interaction between the primary objects and the Main Window
    interface IMainTab
    {
        string TabIcon { get; }
        string TabCaption { get; }
        object TreePaneContent { get; }
        object ActionPaneContent { get; }
    }
}
