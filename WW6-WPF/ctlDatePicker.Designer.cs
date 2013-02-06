namespace WinWam6
{
    partial class ctlDatePicker
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
            this.dtpCustom = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // dtpCustom
            // 
            this.dtpCustom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCustom.Location = new System.Drawing.Point(5, 56);
            this.dtpCustom.Name = "dtpCustom";
            this.dtpCustom.Size = new System.Drawing.Size(130, 20);
            this.dtpCustom.TabIndex = 0;
            // 
            // ctlDatePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtpCustom);
            this.Name = "ctlDatePicker";
            this.Load += new System.EventHandler(this.ctlDatePicker_Load);
            this.Resize += new System.EventHandler(this.ctlDatePicker_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpCustom;
    }
}
