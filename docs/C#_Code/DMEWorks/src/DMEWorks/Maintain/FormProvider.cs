namespace DMEWorks.Maintain
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
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

    public class FormProvider : FormAutoIncrementMaintain
    {
        private IContainer components;

        public FormProvider()
        {
            this.InitializeComponent();
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_provider", "tbl_insurancecompany", "tbl_location" };
            options.TableNames = textArray1;
            base.AddNavigator(options);
            base.ChangesTracker.Subscribe(this.cmbInsuranceCompany);
            base.ChangesTracker.Subscribe(this.cmbLocation);
            base.ChangesTracker.Subscribe(this.cmbProviderNumberType);
            base.ChangesTracker.Subscribe(this.txtProviderNumber);
            base.ChangesTracker.Subscribe(this.txtPassword);
        }

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            Functions.SetComboBoxValue(this.cmbInsuranceCompany, DBNull.Value);
            Functions.SetTextBoxText(this.txtProviderNumber, DBNull.Value);
            Functions.SetTextBoxText(this.txtPassword, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbLocation, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbProviderNumberType, "1C");
        }

        protected override void DeleteObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    if (0 >= command.ExecuteDelete("tbl_provider"))
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
            messages.ConfirmDeleting = $"Are you really want to delete provider '{this.cmbInsuranceCompany.Text} - {this.cmbLocation.Text}'?";
            messages.DeletedSuccessfully = $"Provider '{this.cmbInsuranceCompany.Text} - {this.cmbLocation.Text}' was successfully deleted.";
            return messages;
        }

        protected override void InitDropdowns()
        {
            Cache.InitDropdown(this.cmbInsuranceCompany, "tbl_insurancecompany", null);
            Cache.InitDropdown(this.cmbLocation, "tbl_location", null);
            Cache.InitDropdown(this.cmbProviderNumberType, "tbl_providernumbertype", null);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.lblInsuranceCompany = new Label();
            this.txtProviderNumber = new TextBox();
            this.lblProviderNumber = new Label();
            this.lblLocation = new Label();
            this.cmbInsuranceCompany = new Combobox();
            this.cmbLocation = new Combobox();
            this.txtPassword = new TextBox();
            this.lblPassword = new Label();
            this.cmbProviderNumberType = new Combobox();
            this.lblProviderNumberType = new Label();
            base.tpWorkArea.SuspendLayout();
            ((ISupportInitialize) base.ValidationErrors).BeginInit();
            base.SuspendLayout();
            base.tpWorkArea.Controls.Add(this.cmbProviderNumberType);
            base.tpWorkArea.Controls.Add(this.lblProviderNumberType);
            base.tpWorkArea.Controls.Add(this.txtPassword);
            base.tpWorkArea.Controls.Add(this.lblPassword);
            base.tpWorkArea.Controls.Add(this.cmbLocation);
            base.tpWorkArea.Controls.Add(this.cmbInsuranceCompany);
            base.tpWorkArea.Controls.Add(this.lblLocation);
            base.tpWorkArea.Controls.Add(this.txtProviderNumber);
            base.tpWorkArea.Controls.Add(this.lblProviderNumber);
            base.tpWorkArea.Controls.Add(this.lblInsuranceCompany);
            base.tpWorkArea.Visible = true;
            this.lblInsuranceCompany.Location = new Point(8, 8);
            this.lblInsuranceCompany.Name = "lblInsuranceCompany";
            this.lblInsuranceCompany.Size = new Size(0x70, 0x15);
            this.lblInsuranceCompany.TabIndex = 0;
            this.lblInsuranceCompany.Text = "Insurance Company";
            this.lblInsuranceCompany.TextAlign = ContentAlignment.MiddleRight;
            this.txtProviderNumber.Location = new Point(0x80, 0x70);
            this.txtProviderNumber.Name = "txtProviderNumber";
            this.txtProviderNumber.Size = new Size(0xe8, 20);
            this.txtProviderNumber.TabIndex = 9;
            this.lblProviderNumber.Location = new Point(8, 0x70);
            this.lblProviderNumber.Name = "lblProviderNumber";
            this.lblProviderNumber.Size = new Size(0x70, 0x15);
            this.lblProviderNumber.TabIndex = 8;
            this.lblProviderNumber.Text = "Provider #";
            this.lblProviderNumber.TextAlign = ContentAlignment.MiddleRight;
            this.lblLocation.Location = new Point(8, 40);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new Size(0x70, 0x15);
            this.lblLocation.TabIndex = 2;
            this.lblLocation.Text = "Location";
            this.lblLocation.TextAlign = ContentAlignment.MiddleRight;
            this.cmbInsuranceCompany.Location = new Point(0x80, 8);
            this.cmbInsuranceCompany.Name = "cmbInsuranceCompany";
            this.cmbInsuranceCompany.Size = new Size(0x127, 0x15);
            this.cmbInsuranceCompany.TabIndex = 1;
            this.cmbLocation.Location = new Point(0x80, 40);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new Size(0x127, 0x15);
            this.cmbLocation.TabIndex = 3;
            this.txtPassword.Location = new Point(0x80, 0x90);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new Size(0xe8, 20);
            this.txtPassword.TabIndex = 11;
            this.lblPassword.Location = new Point(8, 0x90);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new Size(0x70, 0x15);
            this.lblPassword.TabIndex = 10;
            this.lblPassword.Text = "Password";
            this.lblPassword.TextAlign = ContentAlignment.MiddleRight;
            this.cmbProviderNumberType.Location = new Point(0x80, 80);
            this.cmbProviderNumberType.Name = "cmbProviderNumberType";
            this.cmbProviderNumberType.Size = new Size(0x127, 0x15);
            this.cmbProviderNumberType.TabIndex = 7;
            this.lblProviderNumberType.Location = new Point(8, 80);
            this.lblProviderNumberType.Name = "lblProviderNumberType";
            this.lblProviderNumberType.Size = new Size(0x70, 0x15);
            this.lblProviderNumberType.TabIndex = 6;
            this.lblProviderNumberType.Text = "Provider # type";
            this.lblProviderNumberType.TextAlign = ContentAlignment.MiddleRight;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x228, 0x189);
            base.Name = "FormProvider";
            this.Text = "Maintain Provider";
            base.tpWorkArea.ResumeLayout(false);
            base.tpWorkArea.PerformLayout();
            ((ISupportInitialize) base.ValidationErrors).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override bool LoadObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = $"SELECT * FROM tbl_provider WHERE ID = {ID}";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.ObjectID = reader["ID"];
                            Functions.SetComboBoxValue(this.cmbInsuranceCompany, reader["InsuranceCompanyID"]);
                            Functions.SetTextBoxText(this.txtProviderNumber, reader["ProviderNumber"]);
                            Functions.SetTextBoxText(this.txtPassword, reader["Password"]);
                            Functions.SetComboBoxValue(this.cmbLocation, reader["LocationID"]);
                            Functions.SetComboBoxValue(this.cmbProviderNumberType, reader["ProviderNumberType"]);
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
            string[] tableNames = new string[] { "tbl_provider" };
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
                    command.Parameters.Add("LocationID", MySqlType.Int).Value = this.cmbLocation.SelectedValue;
                    command.Parameters.Add("InsuranceCompanyID", MySqlType.Int).Value = this.cmbInsuranceCompany.SelectedValue;
                    command.Parameters.Add("ProviderNumber", MySqlType.VarChar, 0x19).Value = this.txtProviderNumber.Text;
                    command.Parameters.Add("ProviderNumberType", MySqlType.VarChar, 6).Value = this.cmbProviderNumberType.SelectedValue;
                    command.Parameters.Add("Password", MySqlType.VarChar, 0x19).Value = this.txtPassword.Text;
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    if (!IsNew)
                    {
                        command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                        string[] whereParameters = new string[] { "ID" };
                        flag = 0 < command.ExecuteUpdate("tbl_provider", whereParameters);
                        flag ??= (0 < command.ExecuteInsert("tbl_provider"));
                    }
                    else
                    {
                        flag = 0 < command.ExecuteInsert("tbl_provider");
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
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT\r\n  tbl_provider.ID,\r\n  tbl_insurancecompany.Name as InsuranceCompany,\r\n  tbl_location.Name as Location,\r\n  tbl_provider.ProviderNumberType,\r\n  tbl_provider.ProviderNumber\r\nFROM tbl_provider\r\n     LEFT JOIN tbl_insurancecompany ON tbl_insurancecompany.ID = tbl_provider.InsuranceCompanyID\r\n     LEFT JOIN tbl_location ON tbl_location.ID = tbl_provider.LocationID", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill((args.Source as DataTableGridSource).Table);
            }
        }

        private void Search_InitializeAppearance(GridAppearanceBase appearance)
        {
            appearance.AutoGenerateColumns = false;
            appearance.Columns.Clear();
            appearance.AddTextColumn("ID", "ID", 40);
            appearance.AddTextColumn("InsuranceCompany", "Insurance Company", 120);
            appearance.AddTextColumn("Location", "Location", 120);
            appearance.AddTextColumn("ProviderNumberType", "Type", 80);
            appearance.AddTextColumn("ProviderNumber", "Provider#", 80);
        }

        private void Search_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            _Closure$__47-0 e$__- = new _Closure$__47-0 {
                $VB$Local_args = args
            };
            base.OpenObject(new Func<object>(e$__-._Lambda$__0));
        }

        protected override void ValidateObject(int ID, bool IsNew)
        {
            if (!Versioned.IsNumeric(this.cmbInsuranceCompany.SelectedValue))
            {
                base.ValidationErrors.SetError(this.cmbInsuranceCompany, "You must select insurance company");
            }
            if (!Versioned.IsNumeric(this.cmbLocation.SelectedValue))
            {
                base.ValidationErrors.SetError(this.cmbLocation, "You must select location");
            }
            if (string.IsNullOrEmpty(this.cmbProviderNumberType.SelectedValue as string))
            {
                base.ValidationErrors.SetError(this.cmbProviderNumberType, "You must select provider number type");
            }
        }

        [field: AccessedThroughProperty("cmbInsuranceCompany")]
        private Combobox cmbInsuranceCompany { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbLocation")]
        private Combobox cmbLocation { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInsuranceCompany")]
        private Label lblInsuranceCompany { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtProviderNumber")]
        private TextBox txtProviderNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblProviderNumber")]
        private Label lblProviderNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblLocation")]
        private Label lblLocation { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPassword")]
        private TextBox txtPassword { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbProviderNumberType")]
        private Combobox cmbProviderNumberType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblProviderNumberType")]
        private Label lblProviderNumberType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPassword")]
        private Label lblPassword { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [CompilerGenerated]
        internal sealed class _Closure$__47-0
        {
            public NavigatorRowClickEventArgs $VB$Local_args;

            internal object _Lambda$__0() => 
                this.$VB$Local_args.GridRow.GetDataRow()["ID"];
        }
    }
}

