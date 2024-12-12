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

    [DesignerGenerated, Buttons(ButtonMissing=true)]
    public class FormFacility : FormAutoIncrementMaintain
    {
        private IContainer components;
        private string F_MIR;
        private FormMirHelper F_MirHelper;

        public FormFacility()
        {
            this.InitializeComponent();
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_facility" };
            options.TableNames = textArray1;
            base.AddNavigator(options);
            base.ChangesTracker.Subscribe(this.CAddress);
            base.ChangesTracker.Subscribe(this.cmbDefaultDeliveryWeek);
            base.ChangesTracker.Subscribe(this.cmbPOSType);
            base.ChangesTracker.Subscribe(this.txtContact);
            base.ChangesTracker.Subscribe(this.txtDirections);
            base.ChangesTracker.Subscribe(this.txtFax);
            base.ChangesTracker.Subscribe(this.txtMedicaidID);
            base.ChangesTracker.Subscribe(this.txtMedicareID);
            base.ChangesTracker.Subscribe(this.txtName);
            base.ChangesTracker.Subscribe(this.txtNPI);
            base.ChangesTracker.Subscribe(this.txtPhone);
            base.ChangesTracker.Subscribe(this.txtPhone2);
            Functions.AttachPhoneAutoInput(this.txtPhone);
            Functions.AttachPhoneAutoInput(this.txtFax);
            Functions.AttachPhoneAutoInput(this.txtPhone2);
        }

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            this.F_MIR = "";
            Functions.SetTextBoxText(this.txtName, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtAddress1, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtAddress2, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtCity, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtState, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtZip, DBNull.Value);
            Functions.SetTextBoxText(this.txtMedicareID, DBNull.Value);
            Functions.SetTextBoxText(this.txtMedicaidID, DBNull.Value);
            Functions.SetTextBoxText(this.txtContact, DBNull.Value);
            Functions.SetTextBoxText(this.txtNPI, DBNull.Value);
            this.cmbPOSType.SelectedValue = DBNull.Value;
            Functions.SetTextBoxText(this.txtPhone, DBNull.Value);
            Functions.SetTextBoxText(this.txtFax, DBNull.Value);
            Functions.SetTextBoxText(this.txtPhone2, DBNull.Value);
            Functions.SetTextBoxText(this.txtDirections, DBNull.Value);
            Functions.SetComboBoxText(this.cmbDefaultDeliveryWeek, DBNull.Value);
        }

        protected override void DeleteObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    if (0 >= command.ExecuteDelete("tbl_facility"))
                    {
                        throw new ObjectIsNotFoundException();
                    }
                }
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

        protected override FormMaintainBase.StandardMessages GetMessages()
        {
            FormMaintainBase.StandardMessages messages = base.GetMessages();
            messages.ConfirmDeleting = $"Are you really want to delete facility '{this.txtName.Text}'?";
            messages.DeletedSuccessfully = $"Facility '{this.txtName.Text}' was successfully deleted.";
            return messages;
        }

        protected override void InitDropdowns()
        {
            using (DataTable table = new DataTable("table"))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("SHOW COLUMNS FROM tbl_facility", ClassGlobalObjects.ConnectionString_MySql))
                {
                    adapter.AcceptChangesDuringFill = true;
                    adapter.Fill(table);
                }
                Functions.LoadComboBoxItems(this.cmbDefaultDeliveryWeek, table, "DefaultDeliveryWeek");
            }
            Cache.InitDropdown(this.cmbPOSType, "tbl_postype", null);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ComponentResourceManager manager1 = new ComponentResourceManager(typeof(FormFacility));
            this.txtDirections = new TextBox();
            this.cmbDefaultDeliveryWeek = new ComboBox();
            this.txtContact = new TextBox();
            this.txtMedicaidID = new TextBox();
            this.txtMedicareID = new TextBox();
            this.txtName = new TextBox();
            this.lblDefaultDeliveryWeek = new Label();
            this.lblDir = new Label();
            this.lblPhone2 = new Label();
            this.lblFax = new Label();
            this.lblPhone = new Label();
            this.lblName = new Label();
            this.lblContact = new Label();
            this.lblMedcaidID = new Label();
            this.lblMedicareID = new Label();
            this.lblPosType = new Label();
            this.CAddress = new ControlAddress();
            this.cmbPOSType = new Combobox();
            this.txtPhone2 = new TextBox();
            this.txtPhone = new TextBox();
            this.txtFax = new TextBox();
            this.txtNPI = new TextBox();
            this.lblNPI = new Label();
            base.tpWorkArea.SuspendLayout();
            ((ISupportInitialize) base.ValidationErrors).BeginInit();
            ((ISupportInitialize) base.ValidationWarnings).BeginInit();
            ((ISupportInitialize) base.MissingData).BeginInit();
            base.SuspendLayout();
            base.tpWorkArea.Controls.Add(this.txtNPI);
            base.tpWorkArea.Controls.Add(this.lblNPI);
            base.tpWorkArea.Controls.Add(this.txtPhone2);
            base.tpWorkArea.Controls.Add(this.txtPhone);
            base.tpWorkArea.Controls.Add(this.txtFax);
            base.tpWorkArea.Controls.Add(this.cmbPOSType);
            base.tpWorkArea.Controls.Add(this.CAddress);
            base.tpWorkArea.Controls.Add(this.txtDirections);
            base.tpWorkArea.Controls.Add(this.cmbDefaultDeliveryWeek);
            base.tpWorkArea.Controls.Add(this.txtContact);
            base.tpWorkArea.Controls.Add(this.txtMedicaidID);
            base.tpWorkArea.Controls.Add(this.txtMedicareID);
            base.tpWorkArea.Controls.Add(this.txtName);
            base.tpWorkArea.Controls.Add(this.lblDefaultDeliveryWeek);
            base.tpWorkArea.Controls.Add(this.lblDir);
            base.tpWorkArea.Controls.Add(this.lblPhone2);
            base.tpWorkArea.Controls.Add(this.lblFax);
            base.tpWorkArea.Controls.Add(this.lblPhone);
            base.tpWorkArea.Controls.Add(this.lblName);
            base.tpWorkArea.Controls.Add(this.lblContact);
            base.tpWorkArea.Controls.Add(this.lblMedcaidID);
            base.tpWorkArea.Controls.Add(this.lblMedicareID);
            base.tpWorkArea.Controls.Add(this.lblPosType);
            base.tpWorkArea.Size = new Size(0x158, 0x1da);
            base.tpWorkArea.Visible = true;
            this.txtDirections.AcceptsReturn = true;
            this.txtDirections.Location = new Point(8, 0x158);
            this.txtDirections.MaxLength = 0;
            this.txtDirections.Multiline = true;
            this.txtDirections.Name = "txtDirections";
            this.txtDirections.RightToLeft = RightToLeft.No;
            this.txtDirections.ScrollBars = ScrollBars.Vertical;
            this.txtDirections.Size = new Size(0x148, 0x60);
            this.txtDirections.TabIndex = 20;
            this.cmbDefaultDeliveryWeek.Location = new Point(0x88, 0x1c0);
            this.cmbDefaultDeliveryWeek.Name = "cmbDefaultDeliveryWeek";
            this.cmbDefaultDeliveryWeek.Size = new Size(0x68, 0x15);
            this.cmbDefaultDeliveryWeek.TabIndex = 0x16;
            this.txtContact.AcceptsReturn = true;
            this.txtContact.BackColor = SystemColors.Window;
            this.txtContact.Cursor = Cursors.IBeam;
            this.txtContact.ForeColor = SystemColors.WindowText;
            this.txtContact.Location = new Point(0x60, 0xb8);
            this.txtContact.MaxLength = 0;
            this.txtContact.Name = "txtContact";
            this.txtContact.RightToLeft = RightToLeft.No;
            this.txtContact.Size = new Size(240, 20);
            this.txtContact.TabIndex = 10;
            this.txtMedicaidID.AcceptsReturn = true;
            this.txtMedicaidID.BackColor = SystemColors.Window;
            this.txtMedicaidID.ForeColor = SystemColors.WindowText;
            this.txtMedicaidID.Location = new Point(0x60, 0x88);
            this.txtMedicaidID.MaxLength = 0;
            this.txtMedicaidID.Name = "txtMedicaidID";
            this.txtMedicaidID.RightToLeft = RightToLeft.No;
            this.txtMedicaidID.Size = new Size(240, 20);
            this.txtMedicaidID.TabIndex = 6;
            this.txtMedicareID.AcceptsReturn = true;
            this.txtMedicareID.BackColor = SystemColors.Window;
            this.txtMedicareID.ForeColor = SystemColors.WindowText;
            this.txtMedicareID.Location = new Point(0x60, 0x70);
            this.txtMedicareID.MaxLength = 0;
            this.txtMedicareID.Name = "txtMedicareID";
            this.txtMedicareID.RightToLeft = RightToLeft.No;
            this.txtMedicareID.Size = new Size(240, 20);
            this.txtMedicareID.TabIndex = 4;
            this.txtName.AcceptsReturn = true;
            this.txtName.BackColor = Color.White;
            this.txtName.Cursor = Cursors.IBeam;
            this.txtName.ForeColor = SystemColors.WindowText;
            this.txtName.Location = new Point(0x60, 8);
            this.txtName.MaxLength = 0;
            this.txtName.Name = "txtName";
            this.txtName.RightToLeft = RightToLeft.No;
            this.txtName.Size = new Size(240, 20);
            this.txtName.TabIndex = 1;
            this.lblDefaultDeliveryWeek.BackColor = Color.Transparent;
            this.lblDefaultDeliveryWeek.Cursor = Cursors.Default;
            this.lblDefaultDeliveryWeek.ForeColor = SystemColors.ControlText;
            this.lblDefaultDeliveryWeek.Location = new Point(8, 0x1c0);
            this.lblDefaultDeliveryWeek.Name = "lblDefaultDeliveryWeek";
            this.lblDefaultDeliveryWeek.RightToLeft = RightToLeft.No;
            this.lblDefaultDeliveryWeek.Size = new Size(0x79, 0x16);
            this.lblDefaultDeliveryWeek.TabIndex = 0x15;
            this.lblDefaultDeliveryWeek.Text = "Default Delivery Week";
            this.lblDefaultDeliveryWeek.TextAlign = ContentAlignment.MiddleRight;
            this.lblDir.BackColor = Color.FromArgb(0xc0, 0xff, 0xc0);
            this.lblDir.Cursor = Cursors.Default;
            this.lblDir.ForeColor = SystemColors.ControlText;
            this.lblDir.Location = new Point(8, 320);
            this.lblDir.Name = "lblDir";
            this.lblDir.RightToLeft = RightToLeft.No;
            this.lblDir.Size = new Size(0x148, 0x11);
            this.lblDir.TabIndex = 0x13;
            this.lblDir.Text = "Directions:";
            this.lblPhone2.BackColor = Color.Transparent;
            this.lblPhone2.Cursor = Cursors.Default;
            this.lblPhone2.ForeColor = SystemColors.ControlText;
            this.lblPhone2.Location = new Point(8, 0x120);
            this.lblPhone2.Name = "lblPhone2";
            this.lblPhone2.RightToLeft = RightToLeft.No;
            this.lblPhone2.Size = new Size(80, 0x15);
            this.lblPhone2.TabIndex = 0x11;
            this.lblPhone2.Text = "Phone 2";
            this.lblPhone2.TextAlign = ContentAlignment.MiddleRight;
            this.lblFax.BackColor = Color.Transparent;
            this.lblFax.Cursor = Cursors.Default;
            this.lblFax.ForeColor = SystemColors.ControlText;
            this.lblFax.Location = new Point(8, 0x108);
            this.lblFax.Name = "lblFax";
            this.lblFax.RightToLeft = RightToLeft.No;
            this.lblFax.Size = new Size(80, 0x15);
            this.lblFax.TabIndex = 15;
            this.lblFax.Text = "Fax";
            this.lblFax.TextAlign = ContentAlignment.MiddleRight;
            this.lblPhone.BackColor = Color.Transparent;
            this.lblPhone.Cursor = Cursors.Default;
            this.lblPhone.ForeColor = SystemColors.ControlText;
            this.lblPhone.Location = new Point(8, 240);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.RightToLeft = RightToLeft.No;
            this.lblPhone.Size = new Size(80, 0x15);
            this.lblPhone.TabIndex = 13;
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
            this.lblName.Text = "Facility Name";
            this.lblName.TextAlign = ContentAlignment.MiddleRight;
            this.lblContact.BackColor = Color.Transparent;
            this.lblContact.Cursor = Cursors.Default;
            this.lblContact.ForeColor = SystemColors.ControlText;
            this.lblContact.Location = new Point(8, 0xb8);
            this.lblContact.Name = "lblContact";
            this.lblContact.RightToLeft = RightToLeft.No;
            this.lblContact.Size = new Size(80, 0x15);
            this.lblContact.TabIndex = 9;
            this.lblContact.Text = "Contact";
            this.lblContact.TextAlign = ContentAlignment.MiddleRight;
            this.lblMedcaidID.BackColor = Color.Transparent;
            this.lblMedcaidID.ForeColor = SystemColors.ControlText;
            this.lblMedcaidID.Location = new Point(8, 0x88);
            this.lblMedcaidID.Name = "lblMedcaidID";
            this.lblMedcaidID.Size = new Size(80, 0x15);
            this.lblMedcaidID.TabIndex = 5;
            this.lblMedcaidID.Text = "Medicaid ID";
            this.lblMedcaidID.TextAlign = ContentAlignment.MiddleRight;
            this.lblMedicareID.BackColor = Color.Transparent;
            this.lblMedicareID.ForeColor = SystemColors.ControlText;
            this.lblMedicareID.Location = new Point(8, 0x70);
            this.lblMedicareID.Name = "lblMedicareID";
            this.lblMedicareID.Size = new Size(80, 0x15);
            this.lblMedicareID.TabIndex = 3;
            this.lblMedicareID.Text = "Medicare ID";
            this.lblMedicareID.TextAlign = ContentAlignment.MiddleRight;
            this.lblPosType.BackColor = Color.Transparent;
            this.lblPosType.Cursor = Cursors.Default;
            this.lblPosType.ForeColor = SystemColors.ControlText;
            this.lblPosType.Location = new Point(8, 0xd0);
            this.lblPosType.Name = "lblPosType";
            this.lblPosType.RightToLeft = RightToLeft.No;
            this.lblPosType.Size = new Size(80, 0x15);
            this.lblPosType.TabIndex = 11;
            this.lblPosType.Text = "POS Type";
            this.lblPosType.TextAlign = ContentAlignment.MiddleRight;
            this.CAddress.BackColor = SystemColors.Control;
            this.CAddress.Location = new Point(0x18, 0x20);
            this.CAddress.Name = "CAddress";
            this.CAddress.Size = new Size(0x138, 0x48);
            this.CAddress.TabIndex = 2;
            this.cmbPOSType.Location = new Point(0x60, 0xd0);
            this.cmbPOSType.Name = "cmbPOSType";
            this.cmbPOSType.Size = new Size(240, 0x15);
            this.cmbPOSType.TabIndex = 12;
            this.txtPhone2.Location = new Point(0x60, 0x120);
            this.txtPhone2.Name = "txtPhone2";
            this.txtPhone2.Size = new Size(240, 20);
            this.txtPhone2.TabIndex = 0x12;
            this.txtPhone.Location = new Point(0x60, 240);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new Size(240, 20);
            this.txtPhone.TabIndex = 14;
            this.txtFax.Location = new Point(0x60, 0x108);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new Size(240, 20);
            this.txtFax.TabIndex = 0x10;
            this.txtNPI.Location = new Point(0x60, 160);
            this.txtNPI.MaxLength = 10;
            this.txtNPI.Name = "txtNPI";
            this.txtNPI.Size = new Size(0x70, 20);
            this.txtNPI.TabIndex = 8;
            this.lblNPI.BackColor = Color.Transparent;
            this.lblNPI.Cursor = Cursors.Default;
            this.lblNPI.ForeColor = SystemColors.ControlText;
            this.lblNPI.Location = new Point(8, 160);
            this.lblNPI.Name = "lblNPI";
            this.lblNPI.Size = new Size(80, 0x15);
            this.lblNPI.TabIndex = 7;
            this.lblNPI.Text = "NPI";
            this.lblNPI.TextAlign = ContentAlignment.MiddleRight;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x160, 0x21d);
            base.Name = "FormFacility";
            this.Text = "Maintain Facility";
            base.tpWorkArea.ResumeLayout(false);
            base.tpWorkArea.PerformLayout();
            ((ISupportInitialize) base.ValidationErrors).EndInit();
            ((ISupportInitialize) base.ValidationWarnings).EndInit();
            ((ISupportInitialize) base.MissingData).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override void InitPrintMenu()
        {
            ContextMenu menu = new ContextMenu();
            Cache.AddCategory(menu, "Facility", new EventHandler(this.mnuPrintItem_Click));
            base.SetPrintMenu(menu);
        }

        protected override bool LoadObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT * FROM tbl_facility WHERE ID = :ID";
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.ObjectID = reader["ID"];
                            this.F_MIR = NullableConvert.ToString(reader["MIR"]);
                            Functions.SetTextBoxText(this.txtName, reader["Name"]);
                            Functions.SetTextBoxText(this.CAddress.txtAddress1, reader["Address1"]);
                            Functions.SetTextBoxText(this.CAddress.txtAddress2, reader["Address2"]);
                            Functions.SetTextBoxText(this.CAddress.txtCity, reader["City"]);
                            Functions.SetTextBoxText(this.CAddress.txtState, reader["State"]);
                            Functions.SetTextBoxText(this.CAddress.txtZip, reader["Zip"]);
                            Functions.SetTextBoxText(this.txtMedicareID, reader["MedicareID"]);
                            Functions.SetTextBoxText(this.txtMedicaidID, reader["MedicaidID"]);
                            Functions.SetTextBoxText(this.txtContact, reader["Contact"]);
                            Functions.SetTextBoxText(this.txtNPI, reader["NPI"]);
                            this.cmbPOSType.SelectedValue = reader["POSTypeID"];
                            Functions.SetTextBoxText(this.txtPhone, reader["Phone"]);
                            Functions.SetTextBoxText(this.txtFax, reader["Fax"]);
                            Functions.SetTextBoxText(this.txtPhone2, reader["Phone2"]);
                            Functions.SetTextBoxText(this.txtDirections, reader["Directions"]);
                            Functions.SetComboBoxText(this.cmbDefaultDeliveryWeek, reader["DefaultDeliveryWeek"]);
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
                    ["{?tbl_facility.ID}"] = this.ObjectID
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
            string[] tableNames = new string[] { "tbl_facility" };
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
                    command.Parameters.Add("State", MySqlType.Char, 2).Value = this.CAddress.txtState.Text;
                    command.Parameters.Add("Zip", MySqlType.VarChar, 10).Value = this.CAddress.txtZip.Text;
                    command.Parameters.Add("MedicaidID", MySqlType.VarChar, 50).Value = this.txtMedicaidID.Text;
                    command.Parameters.Add("MedicareID", MySqlType.VarChar, 50).Value = this.txtMedicareID.Text;
                    command.Parameters.Add("Contact", MySqlType.VarChar, 50).Value = this.txtContact.Text;
                    command.Parameters.Add("NPI", MySqlType.VarChar, 10).Value = this.txtNPI.Text;
                    command.Parameters.Add("DefaultDeliveryWeek", MySqlType.Char, 0x11).Value = this.cmbDefaultDeliveryWeek.Text;
                    command.Parameters.Add("Directions", MySqlType.Text, 0xffffff).Value = this.txtDirections.Text;
                    command.Parameters.Add("Fax", MySqlType.VarChar, 50).Value = this.txtFax.Text;
                    command.Parameters.Add("Name", MySqlType.VarChar, 50).Value = this.txtName.Text;
                    command.Parameters.Add("Phone", MySqlType.VarChar, 50).Value = this.txtPhone.Text;
                    command.Parameters.Add("Phone2", MySqlType.VarChar, 50).Value = this.txtPhone2.Text;
                    command.Parameters.Add("POSTypeID", MySqlType.Int).Value = this.cmbPOSType.SelectedValue;
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    if (IsNew)
                    {
                        flag = 0 < command.ExecuteInsert("tbl_facility");
                        if (flag)
                        {
                            this.ObjectID = command.GetLastIdentity();
                        }
                    }
                    else
                    {
                        command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                        string[] whereParameters = new string[] { "ID" };
                        flag = 0 < command.ExecuteUpdate("tbl_facility", whereParameters);
                        flag ??= (0 < command.ExecuteInsert("tbl_facility"));
                    }
                }
                try
                {
                    using (MySqlCommand command2 = new MySqlCommand("", connection))
                    {
                        command2.Parameters.Add("P_FacilityID", MySqlType.Int).Value = ID;
                        command2.ExecuteProcedure("mir_update_facility");
                    }
                    using (MySqlCommand command3 = new MySqlCommand("", connection))
                    {
                        command3.CommandText = "SELECT MIR FROM tbl_facility WHERE ID = :ID";
                        command3.Parameters.Add("ID", MySqlType.Int, 4).Value = ID;
                        this.F_MIR = NullableConvert.ToString(command3.ExecuteScalar());
                    }
                }
                catch (Exception exception1)
                {
                    Exception ex = exception1;
                    ProjectData.SetProjectError(ex);
                    TraceHelper.TraceException(ex);
                    ProjectData.ClearProjectError();
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
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT ID,\r\n       Name,\r\n       City,\r\n       State\r\nFROM tbl_facility\r\nORDER BY Name", ClassGlobalObjects.ConnectionString_MySql))
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
            appearance.AddTextColumn("Name", "Name", 120);
            appearance.AddTextColumn("City", "City", 80);
            appearance.AddTextColumn("State", "State", 40);
        }

        private void Search_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            _Closure$__99-0 e$__- = new _Closure$__99-0 {
                $VB$Local_args = args
            };
            base.OpenObject(new Func<object>(e$__-._Lambda$__0));
        }

        protected override void SetParameters(FormParameters Params)
        {
            base.ProcessParameter_EntityCreatedListener(Params);
            base.ProcessParameter_TabPage(Params);
            base.ProcessParameter_ID(Params);
            base.ProcessParameter_ShowMissing(Params);
        }

        protected override void ShowMissingInformation(bool Show)
        {
            if (Show)
            {
                this.MirHelper.ShowMissingInformation(base.MissingData, this.F_MIR);
            }
            else
            {
                this.MirHelper.ShowMissingInformation(base.MissingData, "");
            }
        }

        protected override void ValidateObject(int ID, bool IsNew)
        {
            base.ValidationErrors.SetError(this.txtPhone, Functions.PhoneValidate(this.txtPhone.Text));
            base.ValidationErrors.SetError(this.txtPhone2, Functions.PhoneValidate(this.txtPhone2.Text));
            base.ValidationErrors.SetError(this.txtFax, Functions.PhoneValidate(this.txtFax.Text));
            base.ValidationErrors.SetError(this.txtNPI, Functions.NpiValidate(this.txtNPI.Text, false));
        }

        [field: AccessedThroughProperty("cmbDefaultDeliveryWeek")]
        private ComboBox cmbDefaultDeliveryWeek { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblContact")]
        private Label lblContact { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDefaultDeliveryWeek")]
        private Label lblDefaultDeliveryWeek { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDir")]
        private Label lblDir { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblFax")]
        private Label lblFax { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblMedcaidID")]
        private Label lblMedcaidID { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblMedicareID")]
        private Label lblMedicareID { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

        [field: AccessedThroughProperty("lblPosType")]
        private Label lblPosType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtContact")]
        private TextBox txtContact { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDirections")]
        private TextBox txtDirections { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtMedicaidID")]
        private TextBox txtMedicaidID { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtMedicareID")]
        private TextBox txtMedicareID { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtName")]
        private TextBox txtName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("CAddress")]
        private ControlAddress CAddress { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbPOSType")]
        private Combobox cmbPOSType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPhone2")]
        private TextBox txtPhone2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPhone")]
        private TextBox txtPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtNPI")]
        private TextBox txtNPI { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblNPI")]
        private Label lblNPI { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtFax")]
        private TextBox txtFax { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public FormMirHelper MirHelper
        {
            get
            {
                if (this.F_MirHelper == null)
                {
                    this.F_MirHelper = new FormMirHelper();
                    this.F_MirHelper.Add("NPI", this.txtNPI, "NPI is required");
                    this.F_MirHelper.Add("Name", this.txtName, "Name is required");
                    this.F_MirHelper.Add("Address1", this.CAddress.txtAddress1, "Address1 is required");
                    this.F_MirHelper.Add("City", this.CAddress.txtCity, "City is required");
                    this.F_MirHelper.Add("State", this.CAddress.txtState, "State is required");
                    this.F_MirHelper.Add("Zip", this.CAddress.txtZip, "Zip is required");
                    this.F_MirHelper.Add("POSTypeID", this.cmbPOSType, "POS Type is required");
                }
                return this.F_MirHelper;
            }
        }

        [CompilerGenerated]
        internal sealed class _Closure$__99-0
        {
            public NavigatorRowClickEventArgs $VB$Local_args;

            internal object _Lambda$__0() => 
                this.$VB$Local_args.GridRow.GetDataRow()["ID"];
        }
    }
}

