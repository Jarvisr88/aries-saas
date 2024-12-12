namespace DMEWorks.Details
{
    using Devart.Data.MySql;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using DMEWorks.Forms;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class FormCustomerNotes : FormDetails
    {
        private IContainer components;
        private FormSelector FDialog;

        public FormCustomerNotes(ControlCustomerNotes Parent) : base(Parent)
        {
            this.InitializeComponent();
        }

        private void btnPredefined_Click(object sender, EventArgs e)
        {
            this.FDialog ??= new FormSelector();
            Cache.DropdownHelper dropdownHelper = Cache.GetDropdownHelper("tbl_predefinedtext_customernotes");
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
            this.chkActive = new CheckBox();
            this.Panel1 = new Panel();
            this.btnPredefined = new Button();
            ((ISupportInitialize) this.ValidationErrors).BeginInit();
            this.Panel1.SuspendLayout();
            base.SuspendLayout();
            this.btnCancel.Location = new Point(0x220, 8);
            this.btnCancel.TabIndex = 3;
            this.btnOK.Location = new Point(0x1d0, 8);
            this.btnOK.TabIndex = 2;
            this.txtNotes.Dock = DockStyle.Fill;
            this.txtNotes.Location = new Point(0, 0);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ScrollBars = ScrollBars.Vertical;
            this.txtNotes.Size = new Size(0x278, 0x135);
            this.txtNotes.TabIndex = 0;
            this.txtNotes.WordWrap = false;
            this.chkActive.CheckAlign = ContentAlignment.MiddleRight;
            this.chkActive.Location = new Point(0x70, 8);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new Size(0x40, 0x18);
            this.chkActive.TabIndex = 1;
            this.chkActive.Text = "Active";
            this.Panel1.Controls.Add(this.btnPredefined);
            this.Panel1.Controls.Add(this.chkActive);
            this.Panel1.Controls.Add(this.btnCancel);
            this.Panel1.Controls.Add(this.btnOK);
            this.Panel1.Dock = DockStyle.Bottom;
            this.Panel1.Location = new Point(0, 0x135);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(0x278, 40);
            this.Panel1.TabIndex = 1;
            this.Panel1.Controls.SetChildIndex(this.btnOK, 0);
            this.Panel1.Controls.SetChildIndex(this.btnCancel, 0);
            this.Panel1.Controls.SetChildIndex(this.chkActive, 0);
            this.Panel1.Controls.SetChildIndex(this.btnPredefined, 0);
            this.btnPredefined.Location = new Point(8, 8);
            this.btnPredefined.Name = "btnPredefined";
            this.btnPredefined.Size = new Size(0x4b, 0x18);
            this.btnPredefined.TabIndex = 0;
            this.btnPredefined.Text = "Predefined";
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x278, 0x15d);
            base.Controls.Add(this.txtNotes);
            base.Controls.Add(this.Panel1);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FormCustomerNotes";
            this.Text = "FormCustomerNotes";
            ((ISupportInitialize) this.ValidationErrors).EndInit();
            this.Panel1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override void LoadFromRow(DataRow Row)
        {
            if (Row.Table is ControlCustomerNotes.TableCustomerNotes)
            {
                ControlCustomerNotes.TableCustomerNotes table = (ControlCustomerNotes.TableCustomerNotes) Row.Table;
                Functions.SetTextBoxText(this.txtNotes, Row[table.Col_Notes]);
                bool flag = (this.AllowState & AllowStateEnum.AllowEdit00) == AllowStateEnum.AllowEdit00;
                this.txtNotes.ReadOnly = !flag && !Row.IsNull(table.Col_ID);
                Functions.SetCheckBoxChecked(this.chkActive, Row[table.Col_Active]);
            }
        }

        protected override void SaveToRow(DataRow Row)
        {
            if (Row.Table is ControlCustomerNotes.TableCustomerNotes)
            {
                ControlCustomerNotes.TableCustomerNotes table = (ControlCustomerNotes.TableCustomerNotes) Row.Table;
                Row[table.Col_Active] = this.chkActive.Checked;
                if (!this.txtNotes.ReadOnly)
                {
                    Row[table.Col_Notes] = this.txtNotes.Text;
                }
            }
        }

        [field: AccessedThroughProperty("txtNotes")]
        private TextBox txtNotes { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chkActive")]
        private CheckBox chkActive { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnPredefined")]
        private Button btnPredefined { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel1")]
        private Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }
    }
}

