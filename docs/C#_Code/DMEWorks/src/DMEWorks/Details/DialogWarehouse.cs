namespace DMEWorks.Details
{
    using DMEWorks.Data;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class DialogWarehouse : Form
    {
        private IContainer components;

        public DialogWarehouse()
        {
            base.Load += new EventHandler(this.DialogWarehouse_Load);
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (Versioned.IsNumeric(this.cmbWarehouse.SelectedValue))
            {
                base.DialogResult = DialogResult.OK;
                base.Close();
            }
        }

        private void cmbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshButtons();
        }

        private void DialogWarehouse_Load(object sender, EventArgs e)
        {
            this.LoadComboboxes();
            this.RefreshButtons();
        }

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
            this.lblMessage = new Label();
            this.cmbWarehouse = new ComboBox();
            this.btnCancel = new Button();
            this.btnOK = new Button();
            base.SuspendLayout();
            this.lblMessage.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblMessage.Location = new Point(8, 8);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new Size(280, 0x17);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "Please select warehouse";
            this.cmbWarehouse.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbWarehouse.Location = new Point(8, 40);
            this.cmbWarehouse.Name = "cmbWarehouse";
            this.cmbWarehouse.Size = new Size(0x110, 0x15);
            this.cmbWarehouse.TabIndex = 1;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x98, 0x68);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnOK.Location = new Point(0x40, 0x68);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            base.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.CancelButton = this.btnCancel;
            base.ClientSize = new Size(0x124, 0x8d);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.cmbWarehouse);
            base.Controls.Add(this.lblMessage);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Name = "DialogWarehouse";
            base.SizeGripStyle = SizeGripStyle.Hide;
            this.Text = "Select Warehouse";
            base.ResumeLayout(false);
        }

        public void Load_Table_Warehouse()
        {
            Cache.InitDropdown(this.cmbWarehouse, "tbl_warehouse", null);
            int? defaultWarehouseID = ClassGlobalObjects.DefaultWarehouseID;
            if (defaultWarehouseID != null)
            {
                this.cmbWarehouse.SelectedValue = defaultWarehouseID.Value;
            }
        }

        private void LoadComboboxes()
        {
            this.Load_Table_Warehouse();
        }

        private void RefreshButtons()
        {
            this.btnOK.Enabled = Versioned.IsNumeric(this.cmbWarehouse.SelectedValue);
        }

        [field: AccessedThroughProperty("lblMessage")]
        private Label lblMessage { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbWarehouse")]
        private ComboBox cmbWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnCancel")]
        private Button btnCancel { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnOK")]
        private Button btnOK { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public object WarehouseID =>
            this.cmbWarehouse.SelectedValue;
    }
}

