namespace DMEWorks.Maintain
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.CrystalReports;
    using DMEWorks.Data;
    using DMEWorks.Data.MySql;
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

    public class FormReferral : FormAutoIncrementMaintain
    {
        private IContainer components;

        public FormReferral()
        {
            this.InitializeComponent();
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_referral" };
            options.TableNames = textArray1;
            base.AddNavigator(options);
            base.ChangesTracker.Subscribe(this.CName);
            base.ChangesTracker.Subscribe(this.CAddress);
            base.ChangesTracker.Subscribe(this.txtEmployer);
            base.ChangesTracker.Subscribe(this.cmbReferralType);
            base.ChangesTracker.Subscribe(this.txtFax);
            base.ChangesTracker.Subscribe(this.txtMobile);
            base.ChangesTracker.Subscribe(this.txtHomePhone);
            base.ChangesTracker.Subscribe(this.txtWorkPhone);
            base.ChangesTracker.Subscribe(this.dtbLastContacted);
            Functions.AttachPhoneAutoInput(this.txtFax);
            Functions.AttachPhoneAutoInput(this.txtMobile);
            Functions.AttachPhoneAutoInput(this.txtHomePhone);
            Functions.AttachPhoneAutoInput(this.txtWorkPhone);
        }

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            Functions.SetComboBoxText(this.CName.cmbCourtesy, DBNull.Value);
            Functions.SetTextBoxText(this.CName.txtFirstName, DBNull.Value);
            Functions.SetTextBoxText(this.CName.txtMiddleName, DBNull.Value);
            Functions.SetTextBoxText(this.CName.txtLastName, DBNull.Value);
            Functions.SetTextBoxText(this.CName.txtSuffix, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtAddress1, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtAddress2, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtCity, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtState, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtZip, DBNull.Value);
            Functions.SetTextBoxText(this.txtWorkPhone, DBNull.Value);
            Functions.SetTextBoxText(this.txtFax, DBNull.Value);
            Functions.SetTextBoxText(this.txtHomePhone, DBNull.Value);
            Functions.SetTextBoxText(this.txtMobile, DBNull.Value);
            Functions.SetTextBoxText(this.txtEmployer, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbReferralType, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbLastContacted, DBNull.Value);
        }

        protected override void DeleteObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    if (0 >= command.ExecuteDelete("tbl_referral"))
                    {
                        throw new ObjectIsNotFoundException();
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override FormMaintainBase.StandardMessages GetMessages()
        {
            FormMaintainBase.StandardMessages messages = base.GetMessages();
            messages.ConfirmDeleting = $"Are you really want to delete referral '{this.CName.txtFirstName.Text} {this.CName.txtLastName.Text}'?";
            messages.DeletedSuccessfully = $"Referral '{this.CName.txtFirstName.Text} {this.CName.txtLastName.Text}' was successfully deleted.";
            return messages;
        }

        protected override void InitDropdowns()
        {
            using (DataTable table = new DataTable("table"))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("SHOW COLUMNS FROM tbl_referral", ClassGlobalObjects.ConnectionString_MySql))
                {
                    adapter.AcceptChangesDuringFill = true;
                    adapter.Fill(table);
                }
                Functions.LoadComboBoxItems(this.CName.cmbCourtesy, table, "Courtesy");
            }
            Cache.InitDropdown(this.cmbReferralType, "tbl_referraltype", null);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.txtEmployer = new TextBox();
            this.lblHome = new Label();
            this.lblReferralType = new Label();
            this.lblEmp = new Label();
            this.lblMobile = new Label();
            this.lblFax = new Label();
            this.lblWork = new Label();
            this.gbPhones = new GroupBox();
            this.txtHomePhone = new TextBox();
            this.txtMobile = new TextBox();
            this.txtFax = new TextBox();
            this.txtWorkPhone = new TextBox();
            this.CAddress = new ControlAddress();
            this.CName = new ControlName();
            this.cmbReferralType = new Combobox();
            this.dtbLastContacted = new UltraDateTimeEditor();
            this.lblLastContacted = new Label();
            base.tpWorkArea.SuspendLayout();
            this.gbPhones.SuspendLayout();
            base.SuspendLayout();
            Control[] controls = new Control[9];
            controls[0] = this.lblLastContacted;
            controls[1] = this.dtbLastContacted;
            controls[2] = this.cmbReferralType;
            controls[3] = this.CName;
            controls[4] = this.CAddress;
            controls[5] = this.gbPhones;
            controls[6] = this.txtEmployer;
            controls[7] = this.lblReferralType;
            controls[8] = this.lblEmp;
            base.tpWorkArea.Controls.AddRange(controls);
            base.tpWorkArea.Size = new Size(0x198, 0x165);
            base.tpWorkArea.Visible = true;
            this.txtEmployer.AcceptsReturn = true;
            this.txtEmployer.AutoSize = false;
            this.txtEmployer.BackColor = SystemColors.Window;
            this.txtEmployer.Cursor = Cursors.IBeam;
            this.txtEmployer.ForeColor = SystemColors.WindowText;
            this.txtEmployer.Location = new Point(0x60, 0x90);
            this.txtEmployer.MaxLength = 0;
            this.txtEmployer.Name = "txtEmployer";
            this.txtEmployer.RightToLeft = RightToLeft.No;
            this.txtEmployer.Size = new Size(0x130, 0x16);
            this.txtEmployer.TabIndex = 3;
            this.txtEmployer.Text = "";
            this.lblHome.BackColor = Color.Transparent;
            this.lblHome.Cursor = Cursors.Default;
            this.lblHome.ForeColor = SystemColors.ControlText;
            this.lblHome.Location = new Point(0x10, 0x58);
            this.lblHome.Name = "lblHome";
            this.lblHome.RightToLeft = RightToLeft.No;
            this.lblHome.Size = new Size(0x40, 0x16);
            this.lblHome.TabIndex = 6;
            this.lblHome.Text = "Home";
            this.lblHome.TextAlign = ContentAlignment.MiddleRight;
            this.lblReferralType.BackColor = Color.Transparent;
            this.lblReferralType.Cursor = Cursors.Default;
            this.lblReferralType.ForeColor = SystemColors.ControlText;
            this.lblReferralType.Location = new Point(8, 0xb0);
            this.lblReferralType.Name = "lblReferralType";
            this.lblReferralType.RightToLeft = RightToLeft.No;
            this.lblReferralType.Size = new Size(80, 0x16);
            this.lblReferralType.TabIndex = 4;
            this.lblReferralType.Text = "Referral Type";
            this.lblReferralType.TextAlign = ContentAlignment.MiddleRight;
            this.lblEmp.BackColor = Color.Transparent;
            this.lblEmp.Cursor = Cursors.Default;
            this.lblEmp.ForeColor = SystemColors.ControlText;
            this.lblEmp.Location = new Point(8, 0x90);
            this.lblEmp.Name = "lblEmp";
            this.lblEmp.RightToLeft = RightToLeft.No;
            this.lblEmp.Size = new Size(80, 0x16);
            this.lblEmp.TabIndex = 2;
            this.lblEmp.Text = "Employer";
            this.lblEmp.TextAlign = ContentAlignment.MiddleRight;
            this.lblMobile.BackColor = Color.Transparent;
            this.lblMobile.Cursor = Cursors.Default;
            this.lblMobile.ForeColor = SystemColors.ControlText;
            this.lblMobile.Location = new Point(0x10, 0x40);
            this.lblMobile.Name = "lblMobile";
            this.lblMobile.RightToLeft = RightToLeft.No;
            this.lblMobile.Size = new Size(0x40, 0x16);
            this.lblMobile.TabIndex = 4;
            this.lblMobile.Text = "Mobile";
            this.lblMobile.TextAlign = ContentAlignment.MiddleRight;
            this.lblFax.BackColor = Color.Transparent;
            this.lblFax.Cursor = Cursors.Default;
            this.lblFax.ForeColor = SystemColors.ControlText;
            this.lblFax.Location = new Point(0x10, 40);
            this.lblFax.Name = "lblFax";
            this.lblFax.RightToLeft = RightToLeft.No;
            this.lblFax.Size = new Size(0x40, 0x16);
            this.lblFax.TabIndex = 2;
            this.lblFax.Text = "Fax";
            this.lblFax.TextAlign = ContentAlignment.MiddleRight;
            this.lblWork.BackColor = Color.Transparent;
            this.lblWork.Cursor = Cursors.Default;
            this.lblWork.ForeColor = SystemColors.ControlText;
            this.lblWork.Location = new Point(0x10, 0x10);
            this.lblWork.Name = "lblWork";
            this.lblWork.RightToLeft = RightToLeft.No;
            this.lblWork.Size = new Size(0x40, 0x16);
            this.lblWork.TabIndex = 0;
            this.lblWork.Text = "Work";
            this.lblWork.TextAlign = ContentAlignment.MiddleRight;
            this.gbPhones.BackColor = SystemColors.Control;
            Control[] controlArray2 = new Control[] { this.txtHomePhone, this.txtMobile, this.txtFax, this.txtWorkPhone, this.lblHome, this.lblMobile, this.lblFax, this.lblWork };
            this.gbPhones.Controls.AddRange(controlArray2);
            this.gbPhones.Location = new Point(8, 0xe8);
            this.gbPhones.Name = "gbPhones";
            this.gbPhones.Size = new Size(0x188, 120);
            this.gbPhones.TabIndex = 8;
            this.gbPhones.TabStop = false;
            this.gbPhones.Text = "Phones";
            this.txtHomePhone.AutoSize = false;
            this.txtHomePhone.Location = new Point(0x58, 0x58);
            this.txtHomePhone.Name = "txtHomePhone";
            this.txtHomePhone.Size = new Size(0x128, 0x15);
            this.txtHomePhone.TabIndex = 7;
            this.txtHomePhone.Text = "";
            this.txtMobile.AutoSize = false;
            this.txtMobile.Location = new Point(0x58, 0x40);
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new Size(0x128, 0x15);
            this.txtMobile.TabIndex = 5;
            this.txtMobile.Text = "";
            this.txtFax.AutoSize = false;
            this.txtFax.Location = new Point(0x58, 40);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new Size(0x128, 0x15);
            this.txtFax.TabIndex = 3;
            this.txtFax.Text = "";
            this.txtWorkPhone.AutoSize = false;
            this.txtWorkPhone.Location = new Point(0x58, 0x10);
            this.txtWorkPhone.Name = "txtWorkPhone";
            this.txtWorkPhone.Size = new Size(0x128, 0x15);
            this.txtWorkPhone.TabIndex = 1;
            this.txtWorkPhone.Text = "";
            this.CAddress.BackColor = SystemColors.Control;
            this.CAddress.Location = new Point(0x18, 0x40);
            this.CAddress.Name = "CAddress";
            this.CAddress.Size = new Size(0x178, 0x48);
            this.CAddress.TabIndex = 1;
            this.CName.BackColor = SystemColors.Control;
            this.CName.Location = new Point(0x38, 8);
            this.CName.Name = "CName";
            this.CName.Size = new Size(0x158, 0x30);
            this.CName.TabIndex = 0;
            this.cmbReferralType.Location = new Point(0x60, 0xb0);
            this.cmbReferralType.Name = "cmbReferralType";
            this.cmbReferralType.Size = new Size(0x130, 0x15);
            this.cmbReferralType.TabIndex = 5;
            this.dtbLastContacted.Location = new Point(0x60, 0xd0);
            this.dtbLastContacted.Name = "dtbLastContacted";
            this.dtbLastContacted.Size = new Size(0x88, 0x15);
            this.dtbLastContacted.TabIndex = 7;
            this.lblLastContacted.Location = new Point(8, 0xd0);
            this.lblLastContacted.Name = "lblLastContacted";
            this.lblLastContacted.Size = new Size(80, 0x16);
            this.lblLastContacted.TabIndex = 6;
            this.lblLastContacted.Text = "Last Contacted";
            this.lblLastContacted.TextAlign = ContentAlignment.MiddleRight;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x1a0, 0x1a5);
            base.Name = "FormReferral";
            this.Text = "Maintain Referral";
            base.tpWorkArea.ResumeLayout(false);
            this.gbPhones.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        protected override void InitPrintMenu()
        {
            ContextMenu menu = new ContextMenu();
            Cache.AddCategory(menu, "Referral", new EventHandler(this.mnuPrintItem_Click));
            base.SetPrintMenu(menu);
        }

        protected override bool LoadObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = $"SELECT * FROM tbl_referral WHERE ID = {ID}";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.ObjectID = reader["ID"];
                            Functions.SetComboBoxText(this.CName.cmbCourtesy, reader["Courtesy"]);
                            Functions.SetTextBoxText(this.CName.txtFirstName, reader["FirstName"]);
                            Functions.SetTextBoxText(this.CName.txtMiddleName, reader["MiddleName"]);
                            Functions.SetTextBoxText(this.CName.txtLastName, reader["LastName"]);
                            Functions.SetTextBoxText(this.CName.txtSuffix, reader["Suffix"]);
                            Functions.SetTextBoxText(this.CAddress.txtAddress1, reader["Address1"]);
                            Functions.SetTextBoxText(this.CAddress.txtAddress2, reader["Address2"]);
                            Functions.SetTextBoxText(this.CAddress.txtCity, reader["City"]);
                            Functions.SetTextBoxText(this.CAddress.txtState, reader["State"]);
                            Functions.SetTextBoxText(this.CAddress.txtZip, reader["Zip"]);
                            Functions.SetTextBoxText(this.txtWorkPhone, reader["WorkPhone"]);
                            Functions.SetTextBoxText(this.txtFax, reader["Fax"]);
                            Functions.SetTextBoxText(this.txtHomePhone, reader["HomePhone"]);
                            Functions.SetTextBoxText(this.txtMobile, reader["Mobile"]);
                            Functions.SetTextBoxText(this.txtEmployer, reader["Employer"]);
                            Functions.SetComboBoxValue(this.cmbReferralType, reader["ReferralTypeID"]);
                            Functions.SetDateBoxValue(this.dtbLastContacted, reader["LastContacted"]);
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void mnuPrintItem_Click(object sender, EventArgs e)
        {
            ReportMenuItem item = sender as ReportMenuItem;
            if (item != null)
            {
                ReportParameters @params = new ReportParameters {
                    ["{?tbl_referral.ID}"] = this.ObjectID
                };
                try
                {
                    ClassGlobalObjects.ShowReport(item.ReportFileName, @params);
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
        }

        protected override void OnTableUpdate()
        {
            string[] tableNames = new string[] { "tbl_referral" };
            ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
        }

        protected override bool SaveObject(int ID, bool IsNew)
        {
            bool flag;
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("Address1", MySqlType.VarChar, 40).Value = this.CAddress.txtAddress1.Text;
                    command.Parameters.Add("Address2", MySqlType.VarChar, 40).Value = this.CAddress.txtAddress2.Text;
                    command.Parameters.Add("City", MySqlType.VarChar, 0x19).Value = this.CAddress.txtCity.Text;
                    command.Parameters.Add("Courtesy", MySqlType.Char, 4).Value = this.CName.cmbCourtesy.Text;
                    command.Parameters.Add("Employer", MySqlType.VarChar, 50).Value = this.txtEmployer.Text;
                    command.Parameters.Add("Fax", MySqlType.VarChar, 50).Value = this.txtFax.Text;
                    command.Parameters.Add("FirstName", MySqlType.VarChar, 0x19).Value = this.CName.txtFirstName.Text;
                    command.Parameters.Add("HomePhone", MySqlType.VarChar, 50).Value = this.txtHomePhone.Text;
                    command.Parameters.Add("LastName", MySqlType.VarChar, 30).Value = this.CName.txtLastName.Text;
                    command.Parameters.Add("MiddleName", MySqlType.Char, 1).Value = this.CName.txtMiddleName.Text;
                    command.Parameters.Add("Mobile", MySqlType.VarChar, 50).Value = this.txtMobile.Text;
                    command.Parameters.Add("ReferralTypeID", MySqlType.Int).Value = this.cmbReferralType.SelectedValue;
                    command.Parameters.Add("State", MySqlType.Char, 2).Value = this.CAddress.txtState.Text;
                    command.Parameters.Add("Suffix", MySqlType.VarChar, 4).Value = this.CName.txtSuffix.Text;
                    command.Parameters.Add("WorkPhone", MySqlType.VarChar, 50).Value = this.txtWorkPhone.Text;
                    command.Parameters.Add("Zip", MySqlType.VarChar, 10).Value = this.CAddress.txtZip.Text;
                    command.Parameters.Add("LastContacted", MySqlType.Date).Value = Functions.GetDateBoxValue(this.dtbLastContacted);
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    if (!IsNew)
                    {
                        command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                        string[] whereParameters = new string[] { "ID" };
                        flag = 0 < command.ExecuteUpdate("tbl_referral", whereParameters);
                        flag ??= (0 < command.ExecuteInsert("tbl_referral"));
                    }
                    else
                    {
                        flag = 0 < command.ExecuteInsert("tbl_referral");
                        if (flag)
                        {
                            ID = command.GetLastIdentity();
                            this.ObjectID = ID;
                        }
                    }
                }
            }
            return flag;
        }

        private void Search_CreateSource(object sender, CreateSourceEventArgs args)
        {
            args.Source = new DataTable().ToGridSource();
        }

        private void Search_FillSource(object sender, FillSourceEventArgs args)
        {
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT ID,\r\n       CONCAT(LastName, ', ', FirstName) as Name,\r\n       Employer,\r\n       Address1,\r\n       City,\r\n       State\r\nFROM tbl_referral\r\nORDER BY LastName, FirstName", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill((args.Source as DataTableGridSource).Table);
            }
        }

        private void Search_InitializeAppearance(GridAppearanceBase appearance)
        {
            appearance.AutoGenerateColumns = false;
            appearance.Columns.Clear();
            appearance.AddTextColumn("ID", "ID", 50);
            appearance.AddTextColumn("Name", "Name", 150);
            appearance.AddTextColumn("Employer", "Employer", 100);
            appearance.AddTextColumn("Address1", "Address", 100);
            appearance.AddTextColumn("City", "City", 100);
            appearance.AddTextColumn("State", "State", 50);
        }

        private void Search_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            _Closure$__75-0 e$__- = new _Closure$__75-0 {
                $VB$Local_args = args
            };
            base.OpenObject(new Func<object>(e$__-._Lambda$__0));
        }

        protected override void ValidateObject(int ID, bool IsNew)
        {
            base.ValidationErrors.SetError(this.txtFax, Functions.PhoneValidate(this.txtFax.Text));
            base.ValidationErrors.SetError(this.txtMobile, Functions.PhoneValidate(this.txtMobile.Text));
            base.ValidationErrors.SetError(this.txtHomePhone, Functions.PhoneValidate(this.txtHomePhone.Text));
            base.ValidationErrors.SetError(this.txtWorkPhone, Functions.PhoneValidate(this.txtWorkPhone.Text));
        }

        [field: AccessedThroughProperty("txtEmployer")]
        private TextBox txtEmployer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblHome")]
        private Label lblHome { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblEmp")]
        private Label lblEmp { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblMobile")]
        private Label lblMobile { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblFax")]
        private Label lblFax { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblWork")]
        private Label lblWork { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbPhones")]
        private GroupBox gbPhones { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("CAddress")]
        private ControlAddress CAddress { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("CName")]
        private ControlName CName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblReferralType")]
        private Label lblReferralType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbReferralType")]
        private Combobox cmbReferralType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtWorkPhone")]
        private TextBox txtWorkPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtFax")]
        private TextBox txtFax { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtMobile")]
        private TextBox txtMobile { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtHomePhone")]
        private TextBox txtHomePhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbLastContacted")]
        private UltraDateTimeEditor dtbLastContacted { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblLastContacted")]
        private Label lblLastContacted { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [CompilerGenerated]
        internal sealed class _Closure$__75-0
        {
            public NavigatorRowClickEventArgs $VB$Local_args;

            internal object _Lambda$__0() => 
                this.$VB$Local_args.GridRow.GetDataRow()["ID"];
        }
    }
}

