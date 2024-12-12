namespace DMEWorks.Forms
{
    using Devart.Data.MySql;
    using DMEWorks.Core;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class DialogBatchPaymentsReminder : DmeForm
    {
        private IContainer components;
        private int? FSelectedID;

        public DialogBatchPaymentsReminder()
        {
            base.Load += new EventHandler(this.DialogBatchPaymentsReminder_Load);
            this.FSelectedID = null;
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                this.FSelectedID = this.GetID();
                base.DialogResult = DialogResult.OK;
                base.Close();
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

        private void DialogBatchPaymentsReminder_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadGrid();
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

        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private int? GetID()
        {
            int? nullable;
            DataGridViewRow currentRow = this.Grid.CurrentRow;
            if (currentRow == null)
            {
                nullable = null;
            }
            else
            {
                DataRow dataRow = currentRow.GetDataRow();
                nullable = (dataRow != null) ? NullableConvert.ToInt32(dataRow["ID"]) : null;
            }
            return nullable;
        }

        private void Grid_DoubleClick(object sender, EventArgs ne)
        {
            try
            {
                this.FSelectedID = this.GetID();
                base.DialogResult = DialogResult.OK;
                base.Close();
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

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.Grid = new SearchableGrid();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.lblCaption = new Label();
            base.SuspendLayout();
            this.Grid.Location = new Point(8, 0x30);
            this.Grid.Name = "Grid";
            this.Grid.Size = new Size(360, 0xb0);
            this.Grid.TabIndex = 1;
            this.btnOK.Location = new Point(0x70, 0xe8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x19);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0xc0, 0xe8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x19);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.lblCaption.Location = new Point(8, 8);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new Size(360, 40);
            this.lblCaption.TabIndex = 0;
            this.lblCaption.Text = "There exists some checks that have nonzero amount left. Please select one of them.";
            base.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.CancelButton = this.btnCancel;
            base.ClientSize = new Size(0x17a, 0x10f);
            base.Controls.Add(this.lblCaption);
            base.Controls.Add(this.Grid);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnCancel);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Name = "DialogBatchPaymentsReminder";
            this.Text = "Batch Payments Reminder";
            base.ResumeLayout(false);
        }

        private void LoadGrid()
        {
            DataTable dataTable = new DataTable("tbl_batchpayment");
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT\r\n  bp.ID\r\n, bp.LastUpdateDatetime as Date\r\n, ic.Name as InsuranceCompany\r\n, bp.CheckNumber\r\n, bp.CheckDate\r\n, bp.CheckAmount\r\n, bp.CheckAmount - bp.AmountUsed as AmountLeft\r\nFROM tbl_batchpayment as bp\r\n     INNER JOIN tbl_insurancecompany as ic ON bp.InsuranceCompanyID = ic.ID\r\nORDER BY bp.LastUpdateDatetime", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill(dataTable);
            }
            this.Grid.GridSource = dataTable.ToGridSource();
        }

        [field: AccessedThroughProperty("btnOK")]
        private Button btnOK { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnCancel")]
        private Button btnCancel { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCaption")]
        private Label lblCaption { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Grid")]
        private SearchableGrid Grid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public int? SelectedID =>
            this.FSelectedID;
    }
}

