using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

[DesignerGenerated]
public class DialogApproveParameters : Form
{
    private IContainer components;

    public DialogApproveParameters()
    {
        base.Load += new EventHandler(this.DialogApproveParameters_Load);
        this.InitializeComponent();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        base.DialogResult = DialogResult.Cancel;
        base.Close();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        base.DialogResult = DialogResult.OK;
        base.Close();
    }

    private void chbBillDate_CheckedChanged(object sender, EventArgs e)
    {
        this.dtpBillDate.Enabled = this.chbBillDate_SetIfNotEmpty.Checked | this.chbBillDate_SetIfEmpty.Checked;
    }

    private void chbDateOfService_CheckedChanged(object sender, EventArgs e)
    {
        this.dtpDateOfService.Enabled = this.chbDateOfService_Update.Checked;
    }

    private void chbDeliveryDate_CheckedChanged(object sender, EventArgs e)
    {
        this.dtpDeliveryDate.Enabled = this.chbDeliveryDate_SetIfNotEmpty.Checked | this.chbDeliveryDate_SetIfEmpty.Checked;
    }

    private void DialogApproveParameters_Load(object sender, EventArgs e)
    {
    }

    [DebuggerNonUserCode]
    protected override void Dispose(bool disposing)
    {
        try
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
        }
        finally
        {
            base.Dispose(disposing);
        }
    }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
        this.Label1 = new Label();
        this.gbDeliveryDate = new GroupBox();
        this.chbDeliveryDate_SetIfNotEmpty = new CheckBox();
        this.chbDeliveryDate_SetIfEmpty = new CheckBox();
        this.dtpDeliveryDate = new DateTimePicker();
        this.btnOK = new Button();
        this.btnCancel = new Button();
        this.gbBillDate = new GroupBox();
        this.chbBillDate_SetIfNotEmpty = new CheckBox();
        this.chbBillDate_SetIfEmpty = new CheckBox();
        this.dtpBillDate = new DateTimePicker();
        this.gbDateOfService = new GroupBox();
        this.chbDateOfService_Update = new CheckBox();
        this.dtpDateOfService = new DateTimePicker();
        this.gbDeliveryDate.SuspendLayout();
        this.gbBillDate.SuspendLayout();
        this.gbDateOfService.SuspendLayout();
        base.SuspendLayout();
        this.Label1.Location = new Point(8, 8);
        this.Label1.Name = "Label1";
        this.Label1.Size = new Size(0x138, 0x17);
        this.Label1.TabIndex = 0;
        this.Label1.Text = "Approve selected orders";
        this.gbDeliveryDate.Controls.Add(this.chbDeliveryDate_SetIfNotEmpty);
        this.gbDeliveryDate.Controls.Add(this.chbDeliveryDate_SetIfEmpty);
        this.gbDeliveryDate.Controls.Add(this.dtpDeliveryDate);
        this.gbDeliveryDate.Location = new Point(8, 0x20);
        this.gbDeliveryDate.Name = "gbDeliveryDate";
        this.gbDeliveryDate.Size = new Size(0x138, 0x30);
        this.gbDeliveryDate.TabIndex = 1;
        this.gbDeliveryDate.TabStop = false;
        this.gbDeliveryDate.Text = "Delivery Date";
        this.chbDeliveryDate_SetIfNotEmpty.Location = new Point(0x68, 0x10);
        this.chbDeliveryDate_SetIfNotEmpty.Name = "chbDeliveryDate_SetIfNotEmpty";
        this.chbDeliveryDate_SetIfNotEmpty.Size = new Size(0x68, 0x15);
        this.chbDeliveryDate_SetIfNotEmpty.TabIndex = 1;
        this.chbDeliveryDate_SetIfNotEmpty.Text = "Set if not empty";
        this.chbDeliveryDate_SetIfNotEmpty.UseVisualStyleBackColor = true;
        this.chbDeliveryDate_SetIfEmpty.Location = new Point(8, 0x10);
        this.chbDeliveryDate_SetIfEmpty.Name = "chbDeliveryDate_SetIfEmpty";
        this.chbDeliveryDate_SetIfEmpty.Size = new Size(0x58, 0x15);
        this.chbDeliveryDate_SetIfEmpty.TabIndex = 0;
        this.chbDeliveryDate_SetIfEmpty.Text = "Set if empty";
        this.chbDeliveryDate_SetIfEmpty.UseVisualStyleBackColor = true;
        this.dtpDeliveryDate.Enabled = false;
        this.dtpDeliveryDate.Format = DateTimePickerFormat.Short;
        this.dtpDeliveryDate.Location = new Point(0xd8, 0x10);
        this.dtpDeliveryDate.Name = "dtpDeliveryDate";
        this.dtpDeliveryDate.Size = new Size(0x58, 20);
        this.dtpDeliveryDate.TabIndex = 2;
        this.btnOK.Location = new Point(0xa8, 200);
        this.btnOK.Name = "btnOK";
        this.btnOK.Size = new Size(0x48, 0x19);
        this.btnOK.TabIndex = 4;
        this.btnOK.Text = "OK";
        this.btnOK.UseVisualStyleBackColor = true;
        this.btnCancel.Location = new Point(0xf8, 200);
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.Size = new Size(0x48, 0x19);
        this.btnCancel.TabIndex = 5;
        this.btnCancel.Text = "Cancel";
        this.btnCancel.UseVisualStyleBackColor = true;
        this.gbBillDate.Controls.Add(this.chbBillDate_SetIfNotEmpty);
        this.gbBillDate.Controls.Add(this.chbBillDate_SetIfEmpty);
        this.gbBillDate.Controls.Add(this.dtpBillDate);
        this.gbBillDate.Location = new Point(8, 0x58);
        this.gbBillDate.Name = "gbBillDate";
        this.gbBillDate.Size = new Size(0x138, 0x30);
        this.gbBillDate.TabIndex = 2;
        this.gbBillDate.TabStop = false;
        this.gbBillDate.Text = "Bill Date";
        this.chbBillDate_SetIfNotEmpty.Location = new Point(0x68, 0x10);
        this.chbBillDate_SetIfNotEmpty.Name = "chbBillDate_SetIfNotEmpty";
        this.chbBillDate_SetIfNotEmpty.Size = new Size(0x68, 0x15);
        this.chbBillDate_SetIfNotEmpty.TabIndex = 1;
        this.chbBillDate_SetIfNotEmpty.Text = "Set if not empty";
        this.chbBillDate_SetIfNotEmpty.UseVisualStyleBackColor = true;
        this.chbBillDate_SetIfEmpty.Location = new Point(8, 0x10);
        this.chbBillDate_SetIfEmpty.Name = "chbBillDate_SetIfEmpty";
        this.chbBillDate_SetIfEmpty.Size = new Size(0x58, 0x15);
        this.chbBillDate_SetIfEmpty.TabIndex = 0;
        this.chbBillDate_SetIfEmpty.Text = "Set if empty";
        this.chbBillDate_SetIfEmpty.UseVisualStyleBackColor = true;
        this.dtpBillDate.Enabled = false;
        this.dtpBillDate.Format = DateTimePickerFormat.Short;
        this.dtpBillDate.Location = new Point(0xd8, 0x10);
        this.dtpBillDate.Name = "dtpBillDate";
        this.dtpBillDate.Size = new Size(0x58, 20);
        this.dtpBillDate.TabIndex = 2;
        this.gbDateOfService.Controls.Add(this.chbDateOfService_Update);
        this.gbDateOfService.Controls.Add(this.dtpDateOfService);
        this.gbDateOfService.Location = new Point(8, 0x90);
        this.gbDateOfService.Name = "gbDateOfService";
        this.gbDateOfService.Size = new Size(0x138, 0x30);
        this.gbDateOfService.TabIndex = 3;
        this.gbDateOfService.TabStop = false;
        this.gbDateOfService.Text = "Bill Date";
        this.chbDateOfService_Update.Location = new Point(8, 0x10);
        this.chbDateOfService_Update.Name = "chbDateOfService_Update";
        this.chbDateOfService_Update.Size = new Size(0xa8, 0x15);
        this.chbDateOfService_Update.TabIndex = 0;
        this.chbDateOfService_Update.Text = "Update and adjust quantities";
        this.chbDateOfService_Update.UseVisualStyleBackColor = true;
        this.dtpDateOfService.Enabled = false;
        this.dtpDateOfService.Format = DateTimePickerFormat.Short;
        this.dtpDateOfService.Location = new Point(0xd8, 0x10);
        this.dtpDateOfService.Name = "dtpDateOfService";
        this.dtpDateOfService.Size = new Size(0x58, 20);
        this.dtpDateOfService.TabIndex = 1;
        base.AcceptButton = this.btnOK;
        base.AutoScaleDimensions = new SizeF(6f, 13f);
        base.AutoScaleMode = AutoScaleMode.Font;
        base.ClientSize = new Size(0x148, 0xea);
        base.Controls.Add(this.gbDateOfService);
        base.Controls.Add(this.gbBillDate);
        base.Controls.Add(this.btnCancel);
        base.Controls.Add(this.btnOK);
        base.Controls.Add(this.gbDeliveryDate);
        base.Controls.Add(this.Label1);
        base.Name = "DialogApproveParameters";
        this.Text = "Approve ...";
        this.gbDeliveryDate.ResumeLayout(false);
        this.gbBillDate.ResumeLayout(false);
        this.gbDateOfService.ResumeLayout(false);
        base.ResumeLayout(false);
    }

    [field: AccessedThroughProperty("Label1")]
    private Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("gbDeliveryDate")]
    private GroupBox gbDeliveryDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("chbDeliveryDate_SetIfNotEmpty")]
    private CheckBox chbDeliveryDate_SetIfNotEmpty { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("chbDeliveryDate_SetIfEmpty")]
    private CheckBox chbDeliveryDate_SetIfEmpty { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("dtpDeliveryDate")]
    private DateTimePicker dtpDeliveryDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("btnCancel")]
    private Button btnCancel { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("gbBillDate")]
    private GroupBox gbBillDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("chbBillDate_SetIfNotEmpty")]
    private CheckBox chbBillDate_SetIfNotEmpty { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("chbBillDate_SetIfEmpty")]
    private CheckBox chbBillDate_SetIfEmpty { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("dtpBillDate")]
    private DateTimePicker dtpBillDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("gbDateOfService")]
    private GroupBox gbDateOfService { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("chbDateOfService_Update")]
    private CheckBox chbDateOfService_Update { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("dtpDateOfService")]
    private DateTimePicker dtpDateOfService { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("btnOK")]
    private Button btnOK { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    public bool SetBillDate_IfEmpty =>
        this.chbBillDate_SetIfEmpty.Checked;

    public bool SetBillDate_IfNotEmpty =>
        this.chbBillDate_SetIfNotEmpty.Checked;

    public DateTime BillDate =>
        this.dtpBillDate.Value.Date;

    public bool SetDeliveryDate_IfEmpty =>
        this.chbDeliveryDate_SetIfEmpty.Checked;

    public bool SetDeliveryDate_IfNotEmpty =>
        this.chbDeliveryDate_SetIfNotEmpty.Checked;

    public DateTime DeliveryDate =>
        this.dtpDeliveryDate.Value.Date;

    public bool SetDateOfService =>
        this.chbDateOfService_Update.Checked;

    public DateTime DateOfService =>
        this.dtpDateOfService.Value.Date;
}

