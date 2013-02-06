using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinWam6
{
    interface IInspEdit
    {
        string InspType { get; }
        string Layout { get; }
        string InspStr { get; }
        string Caption { get; }

        void Display(string Insp_ID);
        void Delete(string Insp_ID, bool WriteLog = true);
        void PrintInsp(string Insp_ID);
        void NewInsp(string Bus_ID = "");


    }
}
