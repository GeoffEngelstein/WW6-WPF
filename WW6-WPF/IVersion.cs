using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinWam6
{
    interface IVersion
    {
        int Version { get; set; }
        void SwitchToUnreleased();
        bool Release();
        bool Rollback();
    }


}
