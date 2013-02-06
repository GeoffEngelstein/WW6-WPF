namespace WinWam6
{
    partial class ctlCustom
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grdCust = new C1.Win.C1FlexGrid.C1FlexGrid();
            ((System.ComponentModel.ISupportInitialize)(this.grdCust)).BeginInit();
            this.SuspendLayout();
            // 
            // grdCust
            // 
            this.grdCust.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.grdCust.ColumnInfo = "2,1,0,0,0,85,Columns:0{Width:107;}\t1{Caption:\"Custom Data\";}\t";
            this.grdCust.ExtendLastCol = true;
            this.grdCust.Location = new System.Drawing.Point(0, 0);
            this.grdCust.Name = "grdCust";
            this.grdCust.Rows.DefaultSize = 17;
            this.grdCust.Size = new System.Drawing.Size(230, 219);
            this.grdCust.TabIndex = 0;
            this.grdCust.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.grdCust_BeforeEdit);
            this.grdCust.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.grdCust_AfterEdit);
            this.grdCust.Click += new System.EventHandler(this.grdCust_Click);
            // 
            // ctlCustom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdCust);
            this.Name = "ctlCustom";
            this.Size = new System.Drawing.Size(291, 280);
            this.Load += new System.EventHandler(this.ctlCustom_Load);
            this.Resize += new System.EventHandler(this.ctlCustom_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.grdCust)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid grdCust;
    }
}
