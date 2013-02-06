using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace WinWam6
{
    public partial class ctlDatePicker : UserControl, IC1EmbeddedEditor
    {
        
        public ctlDatePicker()
        {
            InitializeComponent();
        }

        private void ctlDatePicker_Resize(object sender, EventArgs e)
        {
            dtpCustom.Width = this.Width;
            dtpCustom.Height = this.Height;
        }

        public void C1EditorInitialize(object value, System.Collections.IDictionary editorAttributes)
        {
            // Apply the initial value.
            DateTime s;

            try
            {
                s = Convert.ToDateTime(value.ToString());

            }
            catch (FormatException)
            {
                s = DateTime.Today;
            }
            dtpCustom.Value = s;

            // Select the whole entry.
            dtpCustom.Select();
            //Select(0, int.MaxValue);
        }

        // Set the FontHeight so the control honors the rectangle height.
        public void C1EditorUpdateBounds(Rectangle rc)
        {
            base.FontHeight = rc.Height;
            Bounds = rc;
        }

        // Suppress the beeps when a user presses ENTER.
        override protected bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                Parent.Focus();
                if (Parent.Focused) SendKeys.Send("{Down}");
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void ctlDatePicker_Load(object sender, EventArgs e)
        {

        }


        #region IC1EmbeddedEditor Members

        public string C1EditorFormat(object value, string mask)
        {
            return "";
        }

        public System.Drawing.Design.UITypeEditorEditStyle C1EditorGetStyle()
        {
            System.Drawing.Design.UITypeEditorEditStyle cc = new System.Drawing.Design.UITypeEditorEditStyle();
            return cc;
        }

        public object C1EditorGetValue()
        {
            return dtpCustom.Value;
        }

        public bool C1EditorKeyDownFinishEdit(KeyEventArgs e)
        {
            return true;
        }

        public bool C1EditorValueIsValid()
        {
            return true;
        }

        #endregion
    }
}
