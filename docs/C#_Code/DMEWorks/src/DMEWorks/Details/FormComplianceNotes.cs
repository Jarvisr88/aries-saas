namespace DMEWorks.Details
{
    using Devart.Data.MySql;
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

    [DesignerGenerated]
    public class FormComplianceNotes : FormDetails
    {
        private IContainer components;
        private FormSelector FDialog;

        public FormComplianceNotes(ControlComplianceNotes Parent) : base(Parent)
        {
            this.InitializeComponent();
        }

        private void btnPredefined_Click(object sender, EventArgs e)
        {
            this.FDialog ??= new FormSelector();
            Cache.DropdownHelper dropdownHelper = Cache.GetDropdownHelper("tbl_predefinedtext_compliancenotes");
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
            Functions.SetComboBoxValue(this.cmbAssignedTo, DBNull.Value);
            this.txtNotes.ReadOnly = false;
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
            this.dtbDate = new UltraDateTimeEditor();
            this.lblDate = new Label();
            this.chkDone = new CheckBox();
            this.pnlBottom = new Panel();
            this.btnPredefined = new Button();
            this.txtNotes = new TextBox();
            this.lblAssignedTo = new Label();
            this.cmbAssignedTo = new Combobox();
            ((ISupportInitialize) this.ValidationErrors).BeginInit();
            this.pnlBottom.SuspendLayout();
            base.SuspendLayout();
            this.btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnCancel.Location = new Point(440, 40);
            this.btnCancel.TabIndex = 5;
            this.btnOK.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnOK.Location = new Point(360, 40);
            this.btnOK.TabIndex = 4;
            this.dtbDate.Location = new Point(160, 40);
            this.dtbDate.Name = "dtbDate";
            this.dtbDate.Size = new Size(0x68, 0x15);
            this.dtbDate.TabIndex = 2;
            this.lblDate.Location = new Point(0x58, 40);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new Size(0x40, 0x15);
            this.lblDate.TabIndex = 1;
            this.lblDate.Text = "Date";
            this.lblDate.TextAlign = ContentAlignment.MiddleRight;
            this.chkDone.CheckAlign = ContentAlignment.MiddleRight;
            this.chkDone.Location = new Point(0x110, 40);
            this.chkDone.Name = "chkDone";
            this.chkDone.Size = new Size(0x38, 0x15);
            this.chkDone.TabIndex = 3;
            this.chkDone.Text = "Done";
            this.pnlBottom.Controls.Add(this.cmbAssignedTo);
            this.pnlBottom.Controls.Add(this.lblAssignedTo);
            this.pnlBottom.Controls.Add(this.dtbDate);
            this.pnlBottom.Controls.Add(this.btnPredefined);
            this.pnlBottom.Controls.Add(this.lblDate);
            this.pnlBottom.Controls.Add(this.chkDone);
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnOK);
            this.pnlBottom.Dock = DockStyle.Bottom;
            this.pnlBottom.Location = new Point(0, 0x128);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new Size(520, 0x45);
            this.pnlBottom.TabIndex = 1;
            this.pnlBottom.Controls.SetChildIndex(this.btnOK, 0);
            this.pnlBottom.Controls.SetChildIndex(this.btnCancel, 0);
            this.pnlBottom.Controls.SetChildIndex(this.chkDone, 0);
            this.pnlBottom.Controls.SetChildIndex(this.lblDate, 0);
            this.pnlBottom.Controls.SetChildIndex(this.btnPredefined, 0);
            this.pnlBottom.Controls.SetChildIndex(this.dtbDate, 0);
            this.pnlBottom.Controls.SetChildIndex(this.lblAssignedTo, 0);
            this.pnlBottom.Controls.SetChildIndex(this.cmbAssignedTo, 0);
            this.btnPredefined.Location = new Point(8, 8);
            this.btnPredefined.Name = "btnPredefined";
            this.btnPredefined.Size = new Size(0x4b, 0x18);
            this.btnPredefined.TabIndex = 0;
            this.btnPredefined.Text = "Predefined";
            this.txtNotes.Dock = DockStyle.Fill;
            this.txtNotes.Location = new Point(0, 0);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ScrollBars = ScrollBars.Vertical;
            this.txtNotes.Size = new Size(520, 0x128);
            this.txtNotes.TabIndex = 0;
            this.txtNotes.WordWrap = false;
            this.lblAssignedTo.Location = new Point(0x58, 8);
            this.lblAssignedTo.Name = "lblAssignedTo";
            this.lblAssignedTo.Size = new Size(0x40, 0x15);
            this.lblAssignedTo.TabIndex = 6;
            this.lblAssignedTo.Text = "Assigned";
            this.lblAssignedTo.TextAlign = ContentAlignment.MiddleRight;
            this.cmbAssignedTo.EditButton = false;
            this.cmbAssignedTo.Location = new Point(160, 8);
            this.cmbAssignedTo.Name = "cmbAssignedTo";
            this.cmbAssignedTo.NewButton = false;
            this.cmbAssignedTo.Size = new Size(0x160, 0x15);
            this.cmbAssignedTo.TabIndex = 7;
            base.AcceptButton = null;
            base.CancelButton = null;
            base.ClientSize = new Size(520, 0x16d);
            base.Controls.Add(this.txtNotes);
            base.Controls.Add(this.pnlBottom);
            base.Name = "FormComplianceNotes";
            this.Text = "Form Compliance Notes";
            ((ISupportInitialize) this.ValidationErrors).EndInit();
            this.pnlBottom.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public override void LoadComboBoxes()
        {
            Cache.InitDropdown(this.cmbAssignedTo, "tbl_user", null);
        }

        protected override void LoadFromRow(DataRow Row)
        {
            if (Row.Table is ControlComplianceNotes.TableComplianceNotes)
            {
                ControlComplianceNotes.TableComplianceNotes table = (ControlComplianceNotes.TableComplianceNotes) Row.Table;
                Functions.SetTextBoxText(this.txtNotes, Row[table.Col_Notes]);
                Functions.SetCheckBoxChecked(this.chkDone, Row[table.Col_Done]);
                Functions.SetDateBoxValue(this.dtbDate, Row[table.Col_Date]);
                Functions.SetComboBoxValue(this.cmbAssignedTo, Row[table.Col_AssignedToId]);
                this.dtbDate.Enabled = Row.IsNull(table.Col_ID);
                this.cmbAssignedTo.Enabled = !this.chkDone.Checked;
                this.txtNotes.ReadOnly = this.chkDone.Checked;
                this.chkDone.Enabled = !this.chkDone.Checked;
            }
        }

        protected override void SaveToRow(DataRow Row)
        {
            if (Row.Table is ControlComplianceNotes.TableComplianceNotes)
            {
                ControlComplianceNotes.TableComplianceNotes table = (ControlComplianceNotes.TableComplianceNotes) Row.Table;
                if (!(Row[table.Col_Done] as bool) || !Conversions.ToBoolean(Row[table.Col_Done]))
                {
                    Row[table.Col_Done] = this.chkDone.Checked;
                    if (Row.IsNull(table.Col_ID))
                    {
                        Row[table.Col_Date] = Functions.GetDateBoxValue(this.dtbDate);
                    }
                    Row[table.Col_Notes] = this.txtNotes.Text;
                    Row[table.Col_AssignedToId] = this.cmbAssignedTo.SelectedValue;
                    Row[table.Col_AssignedToName] = this.cmbAssignedTo.Text;
                }
            }
        }

        protected override void ValidateObject()
        {
            if (!(Functions.GetDateBoxValue(this.dtbDate) is DateTime))
            {
                this.ValidationErrors.SetError(this.dtbDate, "Please enter the date");
            }
            else
            {
                this.ValidationErrors.SetError(this.dtbDate, "");
            }
        }

        [field: AccessedThroughProperty("dtbDate")]
        private UltraDateTimeEditor dtbDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDate")]
        private Label lblDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chkDone")]
        private CheckBox chkDone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlBottom")]
        private Panel pnlBottom { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnPredefined")]
        private Button btnPredefined { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtNotes")]
        private TextBox txtNotes { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbAssignedTo")]
        private Combobox cmbAssignedTo { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAssignedTo")]
        private Label lblAssignedTo { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }
    }
}

