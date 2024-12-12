namespace DMEWorks
{
    using Devart.Data.MySql;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class DialogLocation : DmeForm
    {
        private IContainer components;
        private const string CrLf = "\r\n";
        private const string Key_ShowAtStartup = "DialogSelectLocation.ShowAtStartup";
        private int? FLocationID;

        public DialogLocation()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.FLocationID = NullableConvert.ToInt32(this.cmbLocation.SelectedValue);
            base.DialogResult = DialogResult.OK;
        }

        private void chbShowAtStartup_CheckedChanged(object sender, EventArgs e)
        {
            ShowAtStartup = this.chbShowAtStartup.Checked;
        }

        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.cmbLocation = new ComboBox();
            this.lblLocation = new Label();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.chbShowAtStartup = new CheckBox();
            base.SuspendLayout();
            this.cmbLocation.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbLocation.Location = new Point(8, 0x30);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new Size(0x158, 0x15);
            this.cmbLocation.TabIndex = 1;
            this.lblLocation.Location = new Point(8, 0x18);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new Size(100, 0x15);
            this.lblLocation.TabIndex = 0;
            this.lblLocation.Text = "Location :";
            this.btnOK.Location = new Point(0xa8, 0x70);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x19);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x100, 0x70);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x19);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.chbShowAtStartup.Location = new Point(8, 0x70);
            this.chbShowAtStartup.Name = "chbShowAtStartup";
            this.chbShowAtStartup.Size = new Size(0x88, 0x15);
            this.chbShowAtStartup.TabIndex = 2;
            this.chbShowAtStartup.TabStop = false;
            this.chbShowAtStartup.Text = "Show At Startup";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x166, 0x99);
            base.Controls.Add(this.chbShowAtStartup);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.lblLocation);
            base.Controls.Add(this.cmbLocation);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "DialogLocation";
            base.ShowInTaskbar = false;
            base.SizeGripStyle = SizeGripStyle.Hide;
            this.Text = "Select Location";
            base.ResumeLayout(false);
        }

        private void Load_Table_Location()
        {
            DataTable dataTable = new DataTable("");
            DataRow row = dataTable.NewRow();
            dataTable.Rows.Add(row);
            row.AcceptChanges();
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT\r\n  ID\r\n, Name\r\n, Address1\r\n, City\r\n, State\r\nFROM tbl_location\r\nWHERE ID IN (SELECT LocationID FROM tbl_user_location WHERE UserID = " + Conversions.ToString((int) Globals.CompanyUserID) + ")\r\nORDER BY Name", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill(dataTable);
            }
            Functions.AssignDatasource(this.cmbLocation, dataTable, "Name", "ID");
            Functions.SetComboBoxValue(this.cmbLocation, NullableConvert.ToDb(this.FLocationID));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.chbShowAtStartup.Checked = ShowAtStartup;
            try
            {
                this.Load_Table_Location();
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.ShowException(exception);
                ProjectData.ClearProjectError();
            }
        }

        [field: AccessedThroughProperty("cmbLocation")]
        private ComboBox cmbLocation { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblLocation")]
        private Label lblLocation { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnOK")]
        private Button btnOK { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnCancel")]
        private Button btnCancel { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbShowAtStartup")]
        private CheckBox chbShowAtStartup { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public static bool ShowAtStartup
        {
            get => 
                UserSettings.GetBool("DialogSelectLocation.ShowAtStartup", true);
            set => 
                UserSettings.SetBool("DialogSelectLocation.ShowAtStartup", value);
        }

        public int? LocationID
        {
            get => 
                this.FLocationID;
            set => 
                this.FLocationID = value;
        }
    }
}

