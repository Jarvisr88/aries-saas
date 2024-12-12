namespace DMEWorks.Details
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using DMEWorks.Forms;
    using Infragistics.Win.UltraWinEditors;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class FormInvoiceNotes : FormDetails
    {
        private IContainer components;
        private FormSelector FDialog;

        public FormInvoiceNotes(ControlInvoiceNotes Parent) : base(Parent)
        {
            this.InitializeComponent();
        }

        private void btnPredefined_Click(object sender, EventArgs e)
        {
            this.FDialog ??= new FormSelector();
            Cache.DropdownHelper dropdownHelper = Cache.GetDropdownHelper("tbl_predefinedtext_invoicenotes");
            InitDialogEventArgs args = new InitDialogEventArgs(this.FDialog.GridAppearance) {
                Caption = this.FDialog.Text
            };
            dropdownHelper.InitDialog(this, args);
            this.FDialog.Text = args.Caption;
            this.FDialog.DataSource = dropdownHelper.GetTable().ToGridSource();
            Rectangle workingArea = SystemInformation.WorkingArea;
            this.FDialog.DesktopLocation = new Point((workingArea.Width - this.FDialog.Width) / 2, (workingArea.Height - this.FDialog.Height) / 2);
            if (this.FDialog.ShowDialog() == DialogResult.OK)
            {
                DataRow dataRow = this.FDialog.SelectedRow.GetDataRow();
                if (dataRow != null)
                {
                    object obj2 = dataRow["ID"];
                    if ((obj2 != null) && !(obj2 is DBNull))
                    {
                        using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                        {
                            connection.Open();
                            using (MySqlCommand command = new MySqlCommand("", connection))
                            {
                                command.CommandText = "SELECT Text FROM tbl_predefinedtext WHERE ID = " + Convert.ToString(obj2);
                                using (MySqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow | CommandBehavior.SingleResult))
                                {
                                    if (reader.Read())
                                    {
                                        this.txtNotes.Text = Convert.ToString(reader["Text"]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        protected override void Clear()
        {
            Functions.SetTextBoxText(this.txtNotes, DBNull.Value);
            this.txtNotes.ReadOnly = false;
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
            this.txtNotes = new TextBox();
            this.pnlBottom = new Panel();
            this.lblCallbackDate = new Label();
            this.dtbCallbackDate = new UltraDateTimeEditor();
            this.chkDone = new CheckBox();
            this.btnPredefined = new Button();
            ((ISupportInitialize) this.ValidationErrors).BeginInit();
            this.pnlBottom.SuspendLayout();
            base.SuspendLayout();
            this.btnCancel.Location = new Point(0x220, 8);
            this.btnCancel.TabIndex = 5;
            this.btnOK.Location = new Point(0x1d0, 8);
            this.btnOK.TabIndex = 4;
            this.txtNotes.Dock = DockStyle.Fill;
            this.txtNotes.Font = new Font("Courier New", 9f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.txtNotes.Location = new Point(0, 0);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ScrollBars = ScrollBars.Vertical;
            this.txtNotes.Size = new Size(0x278, 0x135);
            this.txtNotes.TabIndex = 0;
            this.txtNotes.WordWrap = false;
            this.pnlBottom.Controls.Add(this.btnPredefined);
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnOK);
            this.pnlBottom.Controls.Add(this.lblCallbackDate);
            this.pnlBottom.Controls.Add(this.dtbCallbackDate);
            this.pnlBottom.Controls.Add(this.chkDone);
            this.pnlBottom.Dock = DockStyle.Bottom;
            this.pnlBottom.Location = new Point(0, 0x135);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new Size(0x278, 40);
            this.pnlBottom.TabIndex = 1;
            this.pnlBottom.Controls.SetChildIndex(this.chkDone, 0);
            this.pnlBottom.Controls.SetChildIndex(this.dtbCallbackDate, 0);
            this.pnlBottom.Controls.SetChildIndex(this.lblCallbackDate, 0);
            this.pnlBottom.Controls.SetChildIndex(this.btnOK, 0);
            this.pnlBottom.Controls.SetChildIndex(this.btnCancel, 0);
            this.pnlBottom.Controls.SetChildIndex(this.btnPredefined, 0);
            this.lblCallbackDate.Location = new Point(0x58, 8);
            this.lblCallbackDate.Name = "lblCallbackDate";
            this.lblCallbackDate.Size = new Size(80, 0x15);
            this.lblCallbackDate.TabIndex = 1;
            this.lblCallbackDate.Text = "Callback Date";
            this.lblCallbackDate.TextAlign = ContentAlignment.MiddleRight;
            this.dtbCallbackDate.Location = new Point(0xb0, 8);
            this.dtbCallbackDate.Name = "dtbCallbackDate";
            this.dtbCallbackDate.Size = new Size(0x68, 0x15);
            this.dtbCallbackDate.TabIndex = 2;
            this.chkDone.CheckAlign = ContentAlignment.MiddleRight;
            this.chkDone.Location = new Point(0x120, 8);
            this.chkDone.Name = "chkDone";
            this.chkDone.Size = new Size(0x40, 0x15);
            this.chkDone.TabIndex = 3;
            this.chkDone.Text = "Done";
            this.btnPredefined.Location = new Point(8, 8);
            this.btnPredefined.Name = "btnPredefined";
            this.btnPredefined.Size = new Size(0x4b, 0x18);
            this.btnPredefined.TabIndex = 0;
            this.btnPredefined.Text = "Predefined";
            base.AcceptButton = null;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.CancelButton = null;
            base.ClientSize = new Size(0x278, 0x15d);
            base.Controls.Add(this.txtNotes);
            base.Controls.Add(this.pnlBottom);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FormInvoiceNotes";
            this.Text = "Form Invoice Notes";
            ((ISupportInitialize) this.ValidationErrors).EndInit();
            this.pnlBottom.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override void LoadFromRow(DataRow Row)
        {
            if (Row.Table is ControlInvoiceNotes.TableInvoiceNotes)
            {
                ControlInvoiceNotes.TableInvoiceNotes table = (ControlInvoiceNotes.TableInvoiceNotes) Row.Table;
                Functions.SetTextBoxText(this.txtNotes, Row[table.Col_Notes]);
                Functions.SetCheckBoxChecked(this.chkDone, Row[table.Col_Done]);
                Functions.SetDateBoxValue(this.dtbCallbackDate, Row[table.Col_CallbackDate]);
                this.txtNotes.ReadOnly = this.chkDone.Checked;
                this.chkDone.Enabled = !this.chkDone.Checked;
            }
        }

        protected override void SaveToRow(DataRow Row)
        {
            if (Row.Table is ControlInvoiceNotes.TableInvoiceNotes)
            {
                ControlInvoiceNotes.TableInvoiceNotes table = (ControlInvoiceNotes.TableInvoiceNotes) Row.Table;
                if (!(Row[table.Col_Done] as bool) || !Conversions.ToBoolean(Row[table.Col_Done]))
                {
                    Row[table.Col_Done] = this.chkDone.Checked;
                    Row[table.Col_CallbackDate] = Functions.GetDateBoxValue(this.dtbCallbackDate);
                    Row[table.Col_Notes] = this.txtNotes.Text;
                    Row[table.Col_Operator] = Globals.CompanyUserName;
                }
            }
        }

        [field: AccessedThroughProperty("txtNotes")]
        private TextBox txtNotes { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCallbackDate")]
        private Label lblCallbackDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chkDone")]
        private CheckBox chkDone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbCallbackDate")]
        private UltraDateTimeEditor dtbCallbackDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnPredefined")]
        private Button btnPredefined { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlBottom")]
        private Panel pnlBottom { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }
    }
}

