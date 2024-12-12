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
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class FormSalesRep : FormAutoIncrementMaintain
    {
        private IContainer components;

        public FormSalesRep()
        {
            this.InitializeComponent();
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_salesrep" };
            options.TableNames = textArray1;
            base.AddNavigator(options);
            base.ChangesTracker.Subscribe(this.CName);
            base.ChangesTracker.Subscribe(this.CAddress);
            base.ChangesTracker.Subscribe(this.txtMobile);
            base.ChangesTracker.Subscribe(this.txtHomePhone);
            base.ChangesTracker.Subscribe(this.txtPager);
            Functions.AttachPhoneAutoInput(this.txtMobile);
            Functions.AttachPhoneAutoInput(this.txtHomePhone);
            Functions.AttachPhoneAutoInput(this.txtPager);
        }

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            Functions.SetComboBoxText(this.CName.cmbCourtesy, DBNull.Value);
            Functions.SetTextBoxText(this.CName.txtFirstName, DBNull.Value);
            Functions.SetTextBoxText(this.CName.txtLastName, DBNull.Value);
            Functions.SetTextBoxText(this.CName.txtMiddleName, DBNull.Value);
            Functions.SetTextBoxText(this.CName.txtSuffix, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtAddress1, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtAddress2, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtCity, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtState, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtZip, DBNull.Value);
            Functions.SetTextBoxText(this.txtHomePhone, DBNull.Value);
            Functions.SetTextBoxText(this.txtMobile, DBNull.Value);
            Functions.SetTextBoxText(this.txtPager, DBNull.Value);
        }

        protected override void DeleteObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    if (0 >= command.ExecuteDelete("tbl_salesrep"))
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
            messages.ConfirmDeleting = $"Are you really want to delete sales person '{this.CName.txtFirstName.Text} {this.CName.txtLastName.Text}'?";
            messages.DeletedSuccessfully = $"Sales person '{this.CName.txtFirstName.Text} {this.CName.txtLastName.Text}' was successfully deleted.";
            return messages;
        }

        protected override void InitDropdowns()
        {
            using (DataTable table = new DataTable("table"))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("SHOW COLUMNS FROM tbl_salesrep", ClassGlobalObjects.ConnectionString_MySql))
                {
                    adapter.AcceptChangesDuringFill = true;
                    adapter.Fill(table);
                }
                Functions.LoadComboBoxItems(this.CName.cmbCourtesy, table, "Courtesy");
            }
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.lblPager = new Label();
            this.lblHomePhone = new Label();
            this.lblMobile = new Label();
            this.CName = new ControlName();
            this.CAddress = new ControlAddress();
            this.txtMobile = new TextBox();
            this.txtHomePhone = new TextBox();
            this.txtPager = new TextBox();
            base.tpWorkArea.SuspendLayout();
            base.SuspendLayout();
            Control[] controls = new Control[] { this.txtPager, this.txtHomePhone, this.txtMobile, this.CName, this.CAddress, this.lblPager, this.lblHomePhone, this.lblMobile };
            base.tpWorkArea.Controls.AddRange(controls);
            base.tpWorkArea.Size = new Size(0x178, 0xdd);
            this.lblPager.BackColor = Color.Transparent;
            this.lblPager.Cursor = Cursors.Default;
            this.lblPager.ForeColor = SystemColors.ControlText;
            this.lblPager.Location = new Point(8, 0xc0);
            this.lblPager.Name = "lblPager";
            this.lblPager.RightToLeft = RightToLeft.No;
            this.lblPager.Size = new Size(0x40, 0x16);
            this.lblPager.TabIndex = 6;
            this.lblPager.Text = "Pager";
            this.lblPager.TextAlign = ContentAlignment.MiddleRight;
            this.lblHomePhone.BackColor = Color.Transparent;
            this.lblHomePhone.Cursor = Cursors.Default;
            this.lblHomePhone.ForeColor = SystemColors.ControlText;
            this.lblHomePhone.Location = new Point(8, 0xa8);
            this.lblHomePhone.Name = "lblHomePhone";
            this.lblHomePhone.RightToLeft = RightToLeft.No;
            this.lblHomePhone.Size = new Size(0x40, 0x16);
            this.lblHomePhone.TabIndex = 4;
            this.lblHomePhone.Text = "Home";
            this.lblHomePhone.TextAlign = ContentAlignment.MiddleRight;
            this.lblMobile.BackColor = Color.Transparent;
            this.lblMobile.Cursor = Cursors.Default;
            this.lblMobile.ForeColor = SystemColors.ControlText;
            this.lblMobile.Location = new Point(8, 0x90);
            this.lblMobile.Name = "lblMobile";
            this.lblMobile.RightToLeft = RightToLeft.No;
            this.lblMobile.Size = new Size(0x40, 0x16);
            this.lblMobile.TabIndex = 2;
            this.lblMobile.Text = "Mobile";
            this.lblMobile.TextAlign = ContentAlignment.MiddleRight;
            this.CName.BackColor = SystemColors.Control;
            this.CName.Location = new Point(40, 8);
            this.CName.Name = "CName";
            this.CName.Size = new Size(0x148, 0x30);
            this.CName.TabIndex = 0;
            this.CAddress.BackColor = SystemColors.Control;
            this.CAddress.Location = new Point(8, 0x40);
            this.CAddress.Name = "CAddress";
            this.CAddress.Size = new Size(360, 0x48);
            this.CAddress.TabIndex = 1;
            this.txtMobile.AutoSize = false;
            this.txtMobile.Location = new Point(80, 0x90);
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new Size(0x120, 0x15);
            this.txtMobile.TabIndex = 3;
            this.txtMobile.Text = "";
            this.txtHomePhone.AutoSize = false;
            this.txtHomePhone.Location = new Point(80, 0xa8);
            this.txtHomePhone.Name = "txtHomePhone";
            this.txtHomePhone.Size = new Size(0x120, 0x15);
            this.txtHomePhone.TabIndex = 5;
            this.txtHomePhone.Text = "";
            this.txtPager.AutoSize = false;
            this.txtPager.Location = new Point(80, 0xc0);
            this.txtPager.Name = "txtPager";
            this.txtPager.Size = new Size(0x120, 0x15);
            this.txtPager.TabIndex = 7;
            this.txtPager.Text = "";
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x180, 0x11d);
            base.Name = "FormSalesRep";
            this.Text = "Maintain Sales Person";
            base.tpWorkArea.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        protected override void InitPrintMenu()
        {
            ContextMenu menu = new ContextMenu();
            Cache.AddCategory(menu, "Sales Rep", new EventHandler(this.mnuPrintItem_Click));
            base.SetPrintMenu(menu);
        }

        protected override bool LoadObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = $"SELECT * FROM tbl_salesrep WHERE ID = {ID}";
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
                            Functions.SetTextBoxText(this.txtPager, reader["Pager"]);
                            Functions.SetTextBoxText(this.txtHomePhone, reader["HomePhone"]);
                            Functions.SetTextBoxText(this.txtMobile, reader["Mobile"]);
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
                    ["{?tbl_salesrep.ID}"] = this.ObjectID
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
            string[] tableNames = new string[] { "tbl_salesrep" };
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
                    command.Parameters.Add("FirstName", MySqlType.VarChar, 0x19).Value = this.CName.txtFirstName.Text;
                    command.Parameters.Add("HomePhone", MySqlType.VarChar, 50).Value = this.txtHomePhone.Text;
                    command.Parameters.Add("LastName", MySqlType.VarChar, 30).Value = this.CName.txtLastName.Text;
                    command.Parameters.Add("MiddleName", MySqlType.Char, 1).Value = this.CName.txtMiddleName.Text;
                    command.Parameters.Add("Mobile", MySqlType.VarChar, 50).Value = this.txtMobile.Text;
                    command.Parameters.Add("Pager", MySqlType.VarChar, 50).Value = this.txtPager.Text;
                    command.Parameters.Add("State", MySqlType.Char, 2).Value = this.CAddress.txtState.Text;
                    command.Parameters.Add("Suffix", MySqlType.VarChar, 4).Value = this.CName.txtSuffix.Text;
                    command.Parameters.Add("Zip", MySqlType.VarChar, 10).Value = this.CAddress.txtZip.Text;
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    if (!IsNew)
                    {
                        command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                        string[] whereParameters = new string[] { "ID" };
                        flag = 0 < command.ExecuteUpdate("tbl_salesrep", whereParameters);
                        flag ??= (0 < command.ExecuteInsert("tbl_salesrep"));
                    }
                    else
                    {
                        flag = 0 < command.ExecuteInsert("tbl_salesrep");
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
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT ID,\r\n       LastName,\r\n       FirstName,\r\n       Address1,\r\n       City,\r\n       State\r\nFROM tbl_salesrep\r\nORDER BY LastName, FirstName", ClassGlobalObjects.ConnectionString_MySql))
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
            appearance.AddTextColumn("LastName", "Last Name", 100);
            appearance.AddTextColumn("FirstName", "First Name", 100);
            appearance.AddTextColumn("Address1", "Address", 150);
            appearance.AddTextColumn("City", "City", 100);
            appearance.AddTextColumn("State", "State", 50);
        }

        private void Search_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            _Closure$__39-0 e$__- = new _Closure$__39-0 {
                $VB$Local_args = args
            };
            base.OpenObject(new Func<object>(e$__-._Lambda$__0));
        }

        protected override void ValidateObject(int ID, bool IsNew)
        {
            base.ValidationErrors.SetError(this.txtMobile, Functions.PhoneValidate(this.txtMobile.Text));
            base.ValidationErrors.SetError(this.txtHomePhone, Functions.PhoneValidate(this.txtHomePhone.Text));
            base.ValidationErrors.SetError(this.txtPager, Functions.PhoneValidate(this.txtPager.Text));
        }

        [field: AccessedThroughProperty("lblPager")]
        private Label lblPager { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblHomePhone")]
        private Label lblHomePhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblMobile")]
        private Label lblMobile { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("CName")]
        private ControlName CName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("CAddress")]
        private ControlAddress CAddress { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtMobile")]
        private TextBox txtMobile { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtHomePhone")]
        private TextBox txtHomePhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPager")]
        private TextBox txtPager { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [CompilerGenerated]
        internal sealed class _Closure$__39-0
        {
            public NavigatorRowClickEventArgs $VB$Local_args;

            internal object _Lambda$__0() => 
                this.$VB$Local_args.GridRow.GetDataRow()["ID"];
        }
    }
}

