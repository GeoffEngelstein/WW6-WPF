using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinWam6
{
    public partial class ctlCustom : UserControl
    {
        private CCustomFields m_custom;

        public ctlCustom()
        {
            InitializeComponent();
        }

        private void ctlCustom_Load(object sender, EventArgs e)
        {

        }

        private void ctlCustom_Resize(object sender, EventArgs e)
        {
            grdCust.Width = this.Width;
            grdCust.Height = this.Height;

        }

        public CCustomFields CustomFields { get { return m_custom; } set { m_custom = value; DisplayCustom(); } }

        private void DisplayCustom()
        {
            int i = 0;

            if (m_custom == null)  
            {
                grdCust.Rows.Count = 0;
            }
            else
            {
                grdCust.Rows.Count = m_custom.CustomLines.Count;

                foreach (CCustomLine cl in m_custom.CustomLines)
                {
                    grdCust.SetData(i, 0, cl.Caption);
                    grdCust.SetData(i, 1, cl.CustomData);
                    i++;
                }
            }
        }

        private void grdCust_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            int i = grdCust.Row;

            //take current data and stash it into custom grid
            m_custom.CustomLines[i].CustomData = grdCust.GetData(i, 1).ToString();

            //Now redisplay it to make sure we show any formatting changes
            grdCust.SetData(i, 1, m_custom.CustomLines[i].CustomData);
        }

        private void grdCust_BeforeEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            // Set up other types of editors if required
            int i = grdCust.Row;

            if (m_custom.CustomLines[i].IsList)
            {
                StringBuilder sPickList = new StringBuilder();
                    
                foreach (string cp in m_custom.CustomLines[i].PickList)
                {
                    if (sPickList.Length > 0) {sPickList.Append("|");}
                    sPickList.Append(cp);
                }
                grdCust.Cols[1].ComboList = sPickList.ToString(); // Set the combolist
            }
            else
            {
                grdCust.Cols[1].ComboList = ""; // Clear Combo List if we aren't a list
            }

            if (m_custom.CustomLines[i].DataType == CCustomLine.CustDataType.cdDate)
            {
                //make sure there is no picklist
                grdCust.Cols[1].ComboList = "";

                grdCust.Cols[1].DataType = typeof(DateTime);
                /*
                 
                ctlDatePicker myDTP = new ctlDatePicker();
                C1.Win.C1FlexGrid.CellStyle cs = grdCust.Styles.Add("myDateEditor");
                // Set the C1DateEdit control as an editor.
                cs.DataType = typeof(DateTime);
                cs.Editor = myDTP;
                // Assign the style to the cell.
                grdCust.SetCellStyle(i, 1, "myDateEditor"); */
            }
            else
            {
                grdCust.Cols[1].DataType = typeof(string);
            }
        }

        private void grdCust_Click(object sender, EventArgs e)
        {

        }

 
    }
}
