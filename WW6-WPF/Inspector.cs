using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace WinWam6
{
    public partial class Inspector : Form
    {
        CInspector lInsr = new CInspector();
        List<CInspector> lInsrs;
        PropertyManager pm;

        public Inspector()
        {
            InitializeComponent();
        }


        private void SetBindings()
        {
            pm = null;
            lblFirst.DataBindings.Clear();
            lblLast.DataBindings.Clear();
            lblFirst.DataBindings.Add("Text", lInsr, "FirstName");
            lblLast.DataBindings.Add("Text", lInsr, "LastName");

        	pm = (PropertyManager)this.BindingContext[lInsr];
        }

        private void Inspector_Load(object sender, EventArgs e)
        {
            
            LoadInsrs();
            LoadInsrGrid();
        
        }

        private void LoadInsrs()
        {
            OleDbCommand cmd = new OleDbCommand("select insr_id from inspector order by insr_id", WWD.gCn);
            OleDbDataReader rdr = cmd.ExecuteReader();
            string sID;

            lInsrs = new List<CInspector>();

            while (rdr.Read())
            {
                sID = rdr.GetString(0);
                lInsrs.Add(new CInspector(sID));
            }
        }

        private void LoadInsrGrid()
        {
            int i;

            grdInsr.Rows.Count = lInsrs.Count+1;
            i = 1;

            foreach (CInspector mInsr in lInsrs)
            {
                grdInsr.SetData(i, 0, mInsr.ID);
                grdInsr.SetData(i, 1, mInsr.LastName + ", " + mInsr.FirstName);
                i++;
            }
            grdInsr.AutoResize = true;
        }

        private void lblFirst_Click(object sender, EventArgs e)
        {

        }

        private void grdInsr_RowColChange(object sender, EventArgs e)
        {
            if ((grdInsr.Row > 0) && (grdInsr.GetData(grdInsr.Row, 0) != null))
            {
                lInsr = GetInspector(grdInsr.GetData(grdInsr.Row, 0).ToString());
                //lblFirst.Text = lInsr.FirstName;
                //lblLast.Text = lInsr.LastName;
                SetBindings();

                label1.Text = lInsr.CustomFields.CustomLines[0].Caption;

                ctlCustom.CustomFields = lInsr.CustomFields;
                
            }
        }

        private void grdInsr_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var fBus = new Business();
            fBus.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DisplayGInspectors();
        }

        private void DisplayGInspectors()
        {
            grdInsr.Rows.Count = 1; // Delete the old data

            int i;

            i = 1;

            IEnumerable<CInspector> filteredInspectors = System.Linq.Enumerable.Where(lInsrs, n => n.LastName.Contains("e"));
            foreach (CInspector ci in filteredInspectors)
            {
                grdInsr.Rows.Count = i+1;
                grdInsr.SetData(i, 0, ci.ID);
                grdInsr.SetData(i, 1, ci.LastName + ", " + ci.FirstName);
                i++;
            }
            grdInsr.AutoResize = true;

        }

        private CInspector GetInspector(string ID)
        {
            CInspector FindInsr = System.Linq.Enumerable.Where(lInsrs, n => n.ID.Equals(ID)).First();

            return FindInsr;

        }
    }
}
