using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WinWam6
{
    
    public partial class Business : Form
    {
        private CBusiness curBus;

        public Business()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Business_Load(object sender, EventArgs e)
        {
            LoadBusinessGrid();
        }
        
/// <summary>
///  Returns a filter to populate the main Business List based on the basic & advanced queries
/// </summary>
/// <returns>string filter (started by WHERE clause)</returns>
        private string GetFilter()
        {
            string sSQL = "";

            if (txtID.Text.Length > 0)
            {
                sSQL = " bus_id like '" + txtID.Text;
                if (sSQL[sSQL.Length - 1].Equals('%'))
                {
                    sSQL += "'"; 
                }
                else
                {
                    sSQL += "%'";  // Add wildcard character if we don't have it already 
                }
            }

            if (txtName.Text.Length > 0)
            {
                if (sSQL.Length > 0) sSQL += " and ";
                sSQL = " bus_name like '" + txtName.Text;
                if (sSQL[sSQL.Length - 1].Equals('%'))
                {
                    sSQL += "'";
                }
                else
                {
                    sSQL += "%'";  // Add wildcard character if we don't have it already 
                }
            }

            if (txtCity.Text.Length > 0)
            {
                if (sSQL.Length > 0) sSQL += " and "; 
                sSQL = " city like '" + txtCity.Text;
                if (sSQL[sSQL.Length - 1].Equals('%'))
                {
                    sSQL += "'";
                }
                else
                {
                    sSQL += "%'";  // Add wildcard character if we don't have it already 
                }
            }

            if (sSQL.Length > 0)
                {
                    sSQL = " where " + sSQL;
                }
            return sSQL;
        }

        private void LoadBusinessGrid()
        {
            grdBusinessList.Rows.Count = 1;

            string sSQL = "select bus_id, bus_name, city, store_ID, addr1 from business " + GetFilter() + " order by bus_id";
            OleDbCommand cmd = new OleDbCommand(sSQL, WWD.gCn);
            OleDbDataReader rdr = cmd.ExecuteReader();

            int i;

            grdBusinessList.Redraw = false;     //turn off drawing to speed things up (dramatically)

            while (rdr.Read())
            {
                grdBusinessList.Rows.Count += 1;

                i = grdBusinessList.Rows.Count - 1;

                for (int j = 0; j < 5; j++)
                {
                    grdBusinessList.SetData(i, j, rdr[j].ToString());
                }

            }

            grdBusinessList.Redraw = true;  //now go ahead and repaint
            
        }

        private void toolStripStatusLabel3_Click(object sender, EventArgs e)
        {

        }

        private void cmdFilter_Click(object sender, EventArgs e)
        {
            LoadBusinessGrid();
        }

        private void grdBusinessList_Click(object sender, EventArgs e)
        {

        }

        private void grdBusinessList_RowColChange(object sender, EventArgs e)
        {

            string newID;

            if (grdBusinessList.Row > 0)
            {
                newID = (string) grdBusinessList.GetData(grdBusinessList.Row, 0);
                if (newID != null)
                {
                    curBus = new CBusiness(newID);
                    MapObjToForm();
                }
            }
        }

        private void MapObjToForm()
        {
            if (curBus != null)
            {
                txtStreetAddr1.Text = curBus.StreetAddress.Street1;
                txtStreetAddr2.Text = curBus.StreetAddress.Street2;
                txtStreetCity.Text = curBus.StreetAddress.City;
                txtStreetState.Text = curBus.StreetAddress.State;
                txtStreetZip.Text = curBus.StreetAddress.Zip;

                
                string s = GoogleMap(curBus.StreetAddress);

                webBrowser1.DocumentText = s;

            }
        }

        private string GoogleMap(CAddress address)
        {
            string sFormat;
            string sOut;
            string sAddress;
            StringBuilder sFormattedAddress = new StringBuilder();

            sAddress = address.Street1 + ",";
            if (address.Street2 != "") sAddress += address.Street2 + ",";
            sAddress += address.City + "," + address.State + "," + address.Zip;

            foreach (char c in sAddress)
            {
                if (c.Equals(' '))
                {
                    sFormattedAddress.Append("+");
                }
                else
                {
                    sFormattedAddress.Append(c);
                }
            }

            sFormat = "<html><header>Business Map</header><body><iframe width=\"640\" height=\"480\" frameborder=\"0\" scrolling=\"no\" marginheight=\"0\" marginwidth=\"0\" src=\"http://maps.google.com/maps?f=q&amp;source=s_q&amp;hl=en&amp;geocode=&amp;q={0}&amp;aq=&amp;sspn=0.039478,0.074072&amp;ie=UTF8&amp;hq=&amp;hnear={0}&amp;t=h&amp;spn=0.031278,0.054932&amp;z=14&amp;iwloc=A&amp;output=embed\"></iframe><br /><small><a href=\"http://maps.google.com/maps?f=q&amp;source=embed&amp;hl=en&amp;geocode=&amp;q={0}&amp;aq=&amp;sspn=0.039478,0.074072&amp;ie=UTF8&amp;hq=&amp;hnear={0}&amp;t=h&amp;spn=0.031278,0.054932&amp;z=14&amp;iwloc=A\" style=\"color:#0000FF;text-align:left\">View Larger Map</a></small></body></html>";
            sOut = string.Format(sFormat, sFormattedAddress);
            return sOut;
        }

    }
}
