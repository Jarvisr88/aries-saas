namespace DMEWorks.Maintain
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using DMEWorks.Data.MySql;
    using DMEWorks.Forms;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class FormManufacturer : FormAutoIncrementMaintain
    {
        private IContainer components;

        public FormManufacturer()
        {
            this.InitializeComponent();
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_manufacturer" };
            options.TableNames = textArray1;
            base.AddNavigator(options);
            base.ChangesTracker.Subscribe(this.txtName);
            base.ChangesTracker.Subscribe(this.txtAccountNumber);
            base.ChangesTracker.Subscribe(this.CAddress);
            base.ChangesTracker.Subscribe(this.txtContact);
            base.ChangesTracker.Subscribe(this.txtPhone);
            base.ChangesTracker.Subscribe(this.txtFax);
            base.ChangesTracker.Subscribe(this.txtPhone2);
            Functions.AttachPhoneAutoInput(this.txtFax);
            Functions.AttachPhoneAutoInput(this.txtPhone);
            Functions.AttachPhoneAutoInput(this.txtPhone2);
        }

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            Functions.SetTextBoxText(this.txtName, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtAddress1, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtAddress2, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtCity, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtState, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtZip, DBNull.Value);
            Functions.SetTextBoxText(this.txtAccountNumber, DBNull.Value);
            Functions.SetTextBoxText(this.txtContact, DBNull.Value);
            Functions.SetTextBoxText(this.txtPhone, DBNull.Value);
            Functions.SetTextBoxText(this.txtFax, DBNull.Value);
            Functions.SetTextBoxText(this.txtPhone2, DBNull.Value);
        }

        protected override void DeleteObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    if (0 >= command.ExecuteDelete("tbl_manufacturer"))
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
            messages.ConfirmDeleting = $"Are you really want to delete manufacturer '{this.txtName.Text}'?";
            messages.DeletedSuccessfully = $"Manufacturer '{this.txtName.Text}' was successfully deleted.";
            return messages;
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.txtContact = new TextBox();
            this.txtName = new TextBox();
            this.lblPhone2 = new Label();
            this.lblFax = new Label();
            this.lblPhone = new Label();
            this.lblName = new Label();
            this.lblContact = new Label();
            this.CAddress = new ControlAddress();
            this.txtAccountNumber = new TextBox();
            this.lblAccountNumber = new Label();
            this.txtPhone2 = new TextBox();
            this.txtPhone = new TextBox();
            this.txtFax = new TextBox();
            base.tpWorkArea.SuspendLayout();
            base.SuspendLayout();
            Control[] controls = new Control[13];
            controls[0] = this.txtPhone2;
            controls[1] = this.txtPhone;
            controls[2] = this.txtFax;
            controls[3] = this.txtAccountNumber;
            controls[4] = this.lblAccountNumber;
            controls[5] = this.CAddress;
            controls[6] = this.txtContact;
            controls[7] = this.txtName;
            controls[8] = this.lblPhone2;
            controls[9] = this.lblFax;
            controls[10] = this.lblPhone;
            controls[11] = this.lblName;
            controls[12] = this.lblContact;
            base.tpWorkArea.Controls.AddRange(controls);
            base.tpWorkArea.Size = new Size(0x180, 0xfd);
            base.tpWorkArea.Visible = true;
            this.txtContact.AcceptsReturn = true;
            this.txtContact.AutoSize = false;
            this.txtContact.BackColor = SystemColors.Window;
            this.txtContact.Cursor = Cursors.IBeam;
            this.txtContact.ForeColor = SystemColors.WindowText;
            this.txtContact.Location = new Point(0x60, 40);
            this.txtContact.MaxLength = 0;
            this.txtContact.Name = "txtContact";
            this.txtContact.RightToLeft = RightToLeft.No;
            this.txtContact.Size = new Size(280, 0x15);
            this.txtContact.TabIndex = 3;
            this.txtContact.Text = "";
            this.txtName.AcceptsReturn = true;
            this.txtName.AutoSize = false;
            this.txtName.BackColor = Color.White;
            this.txtName.Cursor = Cursors.IBeam;
            this.txtName.ForeColor = SystemColors.WindowText;
            this.txtName.Location = new Point(0x60, 8);
            this.txtName.MaxLength = 0;
            this.txtName.Name = "txtName";
            this.txtName.RightToLeft = RightToLeft.No;
            this.txtName.Size = new Size(280, 0x15);
            this.txtName.TabIndex = 1;
            this.txtName.Text = "";
            this.lblPhone2.BackColor = Color.Transparent;
            this.lblPhone2.Cursor = Cursors.Default;
            this.lblPhone2.ForeColor = SystemColors.ControlText;
            this.lblPhone2.Location = new Point(8, 0xe0);
            this.lblPhone2.Name = "lblPhone2";
            this.lblPhone2.RightToLeft = RightToLeft.No;
            this.lblPhone2.Size = new Size(80, 0x16);
            this.lblPhone2.TabIndex = 11;
            this.lblPhone2.Text = "Phone 2";
            this.lblPhone2.TextAlign = ContentAlignment.MiddleRight;
            this.lblFax.BackColor = Color.Transparent;
            this.lblFax.Cursor = Cursors.Default;
            this.lblFax.ForeColor = SystemColors.ControlText;
            this.lblFax.Location = new Point(8, 200);
            this.lblFax.Name = "lblFax";
            this.lblFax.RightToLeft = RightToLeft.No;
            this.lblFax.Size = new Size(80, 0x16);
            this.lblFax.TabIndex = 9;
            this.lblFax.Text = "Fax";
            this.lblFax.TextAlign = ContentAlignment.MiddleRight;
            this.lblPhone.BackColor = Color.Transparent;
            this.lblPhone.Cursor = Cursors.Default;
            this.lblPhone.ForeColor = SystemColors.ControlText;
            this.lblPhone.Location = new Point(8, 0xb0);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.RightToLeft = RightToLeft.No;
            this.lblPhone.Size = new Size(80, 0x16);
            this.lblPhone.TabIndex = 7;
            this.lblPhone.Text = "Phone ";
            this.lblPhone.TextAlign = ContentAlignment.MiddleRight;
            this.lblName.BackColor = Color.Transparent;
            this.lblName.Cursor = Cursors.Default;
            this.lblName.ForeColor = SystemColors.ControlText;
            this.lblName.Location = new Point(8, 8);
            this.lblName.Name = "lblName";
            this.lblName.RightToLeft = RightToLeft.No;
            this.lblName.Size = new Size(80, 0x16);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Manufacturer Name";
            this.lblName.TextAlign = ContentAlignment.MiddleRight;
            this.lblContact.BackColor = Color.Transparent;
            this.lblContact.Cursor = Cursors.Default;
            this.lblContact.ForeColor = SystemColors.ControlText;
            this.lblContact.Location = new Point(8, 40);
            this.lblContact.Name = "lblContact";
            this.lblContact.RightToLeft = RightToLeft.No;
            this.lblContact.Size = new Size(80, 0x16);
            this.lblContact.TabIndex = 2;
            this.lblContact.Text = "Contact";
            this.lblContact.TextAlign = ContentAlignment.MiddleRight;
            this.CAddress.BackColor = SystemColors.Control;
            this.CAddress.Location = new Point(0x18, 0x60);
            this.CAddress.Name = "CAddress";
            this.CAddress.Size = new Size(0x160, 0x48);
            this.CAddress.TabIndex = 6;
            this.txtAccountNumber.AcceptsReturn = true;
            this.txtAccountNumber.AutoSize = false;
            this.txtAccountNumber.BackColor = SystemColors.Window;
            this.txtAccountNumber.Cursor = Cursors.IBeam;
            this.txtAccountNumber.ForeColor = SystemColors.WindowText;
            this.txtAccountNumber.Location = new Point(0x60, 0x48);
            this.txtAccountNumber.MaxLength = 0;
            this.txtAccountNumber.Name = "txtAccountNumber";
            this.txtAccountNumber.RightToLeft = RightToLeft.No;
            this.txtAccountNumber.Size = new Size(280, 0x15);
            this.txtAccountNumber.TabIndex = 5;
            this.txtAccountNumber.Text = "";
            this.lblAccountNumber.BackColor = Color.Transparent;
            this.lblAccountNumber.Cursor = Cursors.Default;
            this.lblAccountNumber.ForeColor = SystemColors.ControlText;
            this.lblAccountNumber.Location = new Point(8, 0x48);
            this.lblAccountNumber.Name = "lblAccountNumber";
            this.lblAccountNumber.RightToLeft = RightToLeft.No;
            this.lblAccountNumber.Size = new Size(80, 0x16);
            this.lblAccountNumber.TabIndex = 4;
            this.lblAccountNumber.Text = "Account #";
            this.lblAccountNumber.TextAlign = ContentAlignment.MiddleRight;
            this.txtPhone2.Location = new Point(0x60, 0xe0);
            this.txtPhone2.Name = "txtPhone2";
            this.txtPhone2.Size = new Size(280, 20);
            this.txtPhone2.TabIndex = 12;
            this.txtPhone2.Text = "";
            this.txtPhone.Location = new Point(0x60, 0xb0);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new Size(280, 20);
            this.txtPhone.TabIndex = 8;
            this.txtPhone.Text = "";
            this.txtFax.Location = new Point(0x60, 200);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new Size(280, 20);
            this.txtFax.TabIndex = 10;
            this.txtFax.Text = "";
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x188, 0x13d);
            this.MinimumSize = new Size(400, 0x158);
            base.Name = "FormManufacturer";
            this.Text = "Maintain Manufacturer";
            base.tpWorkArea.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        protected override bool LoadObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = $"SELECT * FROM tbl_manufacturer WHERE ID = {ID}";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.ObjectID = reader["ID"];
                            Functions.SetTextBoxText(this.txtName, reader["Name"]);
                            Functions.SetTextBoxText(this.CAddress.txtAddress1, reader["Address1"]);
                            Functions.SetTextBoxText(this.CAddress.txtAddress2, reader["Address2"]);
                            Functions.SetTextBoxText(this.CAddress.txtCity, reader["City"]);
                            Functions.SetTextBoxText(this.CAddress.txtState, reader["State"]);
                            Functions.SetTextBoxText(this.CAddress.txtZip, reader["Zip"]);
                            Functions.SetTextBoxText(this.txtAccountNumber, reader["AccountNumber"]);
                            Functions.SetTextBoxText(this.txtContact, reader["Contact"]);
                            Functions.SetTextBoxText(this.txtPhone, reader["Phone"]);
                            Functions.SetTextBoxText(this.txtFax, reader["Fax"]);
                            Functions.SetTextBoxText(this.txtPhone2, reader["Phone2"]);
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

        protected override void OnTableUpdate()
        {
            string[] tableNames = new string[] { "tbl_manufacturer" };
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
                    command.Parameters.Add("AccountNumber", MySqlType.VarChar, 40).Value = this.txtAccountNumber.Text;
                    command.Parameters.Add("Address1", MySqlType.VarChar, 40).Value = this.CAddress.txtAddress1.Text;
                    command.Parameters.Add("Address2", MySqlType.VarChar, 40).Value = this.CAddress.txtAddress2.Text;
                    command.Parameters.Add("City", MySqlType.VarChar, 0x19).Value = this.CAddress.txtCity.Text;
                    command.Parameters.Add("Contact", MySqlType.VarChar, 50).Value = this.txtContact.Text;
                    command.Parameters.Add("Fax", MySqlType.VarChar, 50).Value = this.txtFax.Text;
                    command.Parameters.Add("Name", MySqlType.VarChar, 50).Value = this.txtName.Text;
                    command.Parameters.Add("Phone", MySqlType.VarChar, 50).Value = this.txtPhone.Text;
                    command.Parameters.Add("Phone2", MySqlType.VarChar, 50).Value = this.txtPhone2.Text;
                    command.Parameters.Add("State", MySqlType.Char, 2).Value = this.CAddress.txtState.Text;
                    command.Parameters.Add("Zip", MySqlType.VarChar, 10).Value = this.CAddress.txtZip.Text;
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    if (!IsNew)
                    {
                        command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                        string[] whereParameters = new string[] { "ID" };
                        flag = 0 < command.ExecuteUpdate("tbl_manufacturer", whereParameters);
                        flag ??= (0 < command.ExecuteInsert("tbl_manufacturer"));
                    }
                    else
                    {
                        flag = 0 < command.ExecuteInsert("tbl_manufacturer");
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
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT ID,\r\n       Name,\r\n       Address1,\r\n       City,\r\n       State\r\nFROM tbl_manufacturer\r\nORDER BY Name", ClassGlobalObjects.ConnectionString_MySql))
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
            appearance.AddTextColumn("Name", "Name", 100);
            appearance.AddTextColumn("Address1", "Address", 150);
            appearance.AddTextColumn("City", "City", 100);
            appearance.AddTextColumn("State", "State", 50);
        }

        private void Search_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            _Closure$__59-0 e$__- = new _Closure$__59-0 {
                $VB$Local_args = args
            };
            base.OpenObject(new Func<object>(e$__-._Lambda$__0));
        }

        protected override void ValidateObject(int ID, bool IsNew)
        {
            base.ValidationErrors.SetError(this.txtPhone, Functions.PhoneValidate(this.txtPhone.Text));
            base.ValidationErrors.SetError(this.txtPhone2, Functions.PhoneValidate(this.txtPhone2.Text));
            base.ValidationErrors.SetError(this.txtFax, Functions.PhoneValidate(this.txtFax.Text));
        }

        [field: AccessedThroughProperty("lblContact")]
        private Label lblContact { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblFax")]
        private Label lblFax { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblName")]
        private Label lblName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPhone")]
        private Label lblPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPhone2")]
        private Label lblPhone2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtContact")]
        private TextBox txtContact { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtName")]
        private TextBox txtName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("CAddress")]
        private ControlAddress CAddress { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAccountNumber")]
        private Label lblAccountNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAccountNumber")]
        private TextBox txtAccountNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPhone2")]
        private TextBox txtPhone2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPhone")]
        private TextBox txtPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtFax")]
        private TextBox txtFax { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [CompilerGenerated]
        internal sealed class _Closure$__59-0
        {
            public NavigatorRowClickEventArgs $VB$Local_args;

            internal object _Lambda$__0() => 
                this.$VB$Local_args.GridRow.GetDataRow()["ID"];
        }
    }
}

