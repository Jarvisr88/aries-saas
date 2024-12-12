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

    public class FormTaxRate : FormAutoIncrementMaintain
    {
        private IContainer components;

        public FormTaxRate()
        {
            this.InitializeComponent();
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_taxrate" };
            options.TableNames = textArray1;
            base.AddNavigator(options);
            base.ChangesTracker.Subscribe(this.txtName);
            base.ChangesTracker.Subscribe(this.nmbStateTax);
            base.ChangesTracker.Subscribe(this.nmbCountyTax);
            base.ChangesTracker.Subscribe(this.nmbCityTax);
            base.ChangesTracker.Subscribe(this.nmbOtherTax);
        }

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            Functions.SetTextBoxText(this.txtName, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbCityTax, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbCountyTax, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbOtherTax, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbStateTax, DBNull.Value);
        }

        protected override void DeleteObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    if (0 >= command.ExecuteDelete("tbl_taxrate"))
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
            messages.ConfirmDeleting = $"Are you really want to delete tax rate '{this.txtName.Text}'?";
            messages.DeletedSuccessfully = $"Tax rate '{this.txtName.Text}' was successfully deleted.";
            return messages;
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.lblName = new Label();
            this.lblStateTax = new Label();
            this.lblCountyTax = new Label();
            this.lblCityTax = new Label();
            this.lblOtherTax = new Label();
            this.lblTotalTax = new Label();
            this.nmbStateTax = new NumericBox();
            this.txtName = new TextBox();
            this.nmbOtherTax = new NumericBox();
            this.nmbCityTax = new NumericBox();
            this.nmbTotalTax = new NumericBox();
            this.nmbCountyTax = new NumericBox();
            base.tpWorkArea.SuspendLayout();
            base.SuspendLayout();
            Control[] controls = new Control[12];
            controls[0] = this.nmbCountyTax;
            controls[1] = this.nmbTotalTax;
            controls[2] = this.nmbCityTax;
            controls[3] = this.nmbOtherTax;
            controls[4] = this.txtName;
            controls[5] = this.nmbStateTax;
            controls[6] = this.lblTotalTax;
            controls[7] = this.lblOtherTax;
            controls[8] = this.lblCityTax;
            controls[9] = this.lblCountyTax;
            controls[10] = this.lblStateTax;
            controls[11] = this.lblName;
            base.tpWorkArea.Controls.AddRange(controls);
            base.tpWorkArea.Size = new Size(0x158, 0xdd);
            base.tpWorkArea.Visible = true;
            this.lblName.BackColor = Color.Transparent;
            this.lblName.Location = new Point(8, 8);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(0x58, 0x16);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name";
            this.lblName.TextAlign = ContentAlignment.MiddleRight;
            this.lblStateTax.BackColor = Color.Transparent;
            this.lblStateTax.Location = new Point(8, 40);
            this.lblStateTax.Name = "lblStateTax";
            this.lblStateTax.Size = new Size(0x58, 0x16);
            this.lblStateTax.TabIndex = 2;
            this.lblStateTax.Text = "State Tax";
            this.lblStateTax.TextAlign = ContentAlignment.MiddleRight;
            this.lblCountyTax.BackColor = Color.Transparent;
            this.lblCountyTax.Location = new Point(8, 0x40);
            this.lblCountyTax.Name = "lblCountyTax";
            this.lblCountyTax.Size = new Size(0x58, 0x16);
            this.lblCountyTax.TabIndex = 4;
            this.lblCountyTax.Text = "County Tax";
            this.lblCountyTax.TextAlign = ContentAlignment.MiddleRight;
            this.lblCityTax.BackColor = Color.Transparent;
            this.lblCityTax.Location = new Point(8, 0x58);
            this.lblCityTax.Name = "lblCityTax";
            this.lblCityTax.Size = new Size(0x58, 0x16);
            this.lblCityTax.TabIndex = 6;
            this.lblCityTax.Text = "City Tax";
            this.lblCityTax.TextAlign = ContentAlignment.MiddleRight;
            this.lblOtherTax.BackColor = Color.Transparent;
            this.lblOtherTax.Location = new Point(8, 0x70);
            this.lblOtherTax.Name = "lblOtherTax";
            this.lblOtherTax.Size = new Size(0x58, 0x16);
            this.lblOtherTax.TabIndex = 8;
            this.lblOtherTax.Text = "Other Tax";
            this.lblOtherTax.TextAlign = ContentAlignment.MiddleRight;
            this.lblTotalTax.BackColor = Color.Transparent;
            this.lblTotalTax.Location = new Point(8, 0x88);
            this.lblTotalTax.Name = "lblTotalTax";
            this.lblTotalTax.Size = new Size(0x58, 0x16);
            this.lblTotalTax.TabIndex = 10;
            this.lblTotalTax.Text = "Total Tax";
            this.lblTotalTax.TextAlign = ContentAlignment.MiddleRight;
            this.nmbStateTax.BorderStyle = BorderStyle.Fixed3D;
            this.nmbStateTax.Location = new Point(0x68, 40);
            this.nmbStateTax.Name = "nmbStateTax";
            this.nmbStateTax.Size = new Size(0x60, 20);
            this.nmbStateTax.TabIndex = 3;
            this.txtName.AutoSize = false;
            this.txtName.Location = new Point(0x68, 8);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(0xe8, 0x16);
            this.txtName.TabIndex = 1;
            this.txtName.Text = "";
            this.nmbOtherTax.BorderStyle = BorderStyle.Fixed3D;
            this.nmbOtherTax.Location = new Point(0x68, 0x70);
            this.nmbOtherTax.Name = "nmbOtherTax";
            this.nmbOtherTax.Size = new Size(0x60, 20);
            this.nmbOtherTax.TabIndex = 9;
            this.nmbCityTax.BorderStyle = BorderStyle.Fixed3D;
            this.nmbCityTax.Location = new Point(0x68, 0x58);
            this.nmbCityTax.Name = "nmbCityTax";
            this.nmbCityTax.Size = new Size(0x60, 20);
            this.nmbCityTax.TabIndex = 7;
            this.nmbTotalTax.BorderStyle = BorderStyle.Fixed3D;
            this.nmbTotalTax.Enabled = false;
            this.nmbTotalTax.Location = new Point(0x68, 0x88);
            this.nmbTotalTax.Name = "nmbTotalTax";
            this.nmbTotalTax.Size = new Size(0x60, 20);
            this.nmbTotalTax.TabIndex = 11;
            this.nmbCountyTax.BorderStyle = BorderStyle.Fixed3D;
            this.nmbCountyTax.Location = new Point(0x68, 0x40);
            this.nmbCountyTax.Name = "nmbCountyTax";
            this.nmbCountyTax.Size = new Size(0x60, 20);
            this.nmbCountyTax.TabIndex = 5;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x160, 0x11d);
            base.Name = "FormTaxRate";
            this.Text = "Maintain Tax Rate";
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
                    command.CommandText = $"SELECT * FROM tbl_taxrate WHERE ID = {ID}";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.ObjectID = reader["ID"];
                            Functions.SetTextBoxText(this.txtName, reader["Name"]);
                            Functions.SetNumericBoxValue(this.nmbCityTax, reader["CityTax"]);
                            Functions.SetNumericBoxValue(this.nmbCountyTax, reader["CountyTax"]);
                            Functions.SetNumericBoxValue(this.nmbOtherTax, reader["OtherTax"]);
                            Functions.SetNumericBoxValue(this.nmbStateTax, reader["StateTax"]);
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

        private void nmbOtherTax_TextChanged(object sender, EventArgs e)
        {
            this.nmbTotalTax.AsDouble = new double?(((this.nmbStateTax.AsDouble.GetValueOrDefault(0.0) + this.nmbCountyTax.AsDouble.GetValueOrDefault(0.0)) + this.nmbCityTax.AsDouble.GetValueOrDefault(0.0)) + this.nmbOtherTax.AsDouble.GetValueOrDefault(0.0));
        }

        protected override void OnTableUpdate()
        {
            string[] tableNames = new string[] { "tbl_taxrate" };
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
                    command.Parameters.Add("CityTax", MySqlType.Double).Value = this.nmbCityTax.AsDouble.GetValueOrDefault(0.0);
                    command.Parameters.Add("CountyTax", MySqlType.Double).Value = this.nmbCountyTax.AsDouble.GetValueOrDefault(0.0);
                    command.Parameters.Add("Name", MySqlType.VarChar, 50).Value = this.txtName.Text;
                    command.Parameters.Add("OtherTax", MySqlType.Double).Value = this.nmbOtherTax.AsDouble.GetValueOrDefault(0.0);
                    command.Parameters.Add("StateTax", MySqlType.Double).Value = this.nmbStateTax.AsDouble.GetValueOrDefault(0.0);
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    if (!IsNew)
                    {
                        command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                        string[] whereParameters = new string[] { "ID" };
                        flag = 0 < command.ExecuteUpdate("tbl_taxrate", whereParameters);
                        flag ??= (0 < command.ExecuteInsert("tbl_taxrate"));
                    }
                    else
                    {
                        flag = 0 < command.ExecuteInsert("tbl_taxrate");
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
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT ID, Name FROM tbl_taxrate ORDER BY Name", ClassGlobalObjects.ConnectionString_MySql))
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
            appearance.AddTextColumn("Name", "Name", 200);
        }

        private void Search_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            _Closure$__55-0 e$__- = new _Closure$__55-0 {
                $VB$Local_args = args
            };
            base.OpenObject(new Func<object>(e$__-._Lambda$__0));
        }

        [field: AccessedThroughProperty("lblName")]
        private Label lblName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblStateTax")]
        private Label lblStateTax { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCountyTax")]
        private Label lblCountyTax { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCityTax")]
        private Label lblCityTax { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblOtherTax")]
        private Label lblOtherTax { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTotalTax")]
        private Label lblTotalTax { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbStateTax")]
        private NumericBox nmbStateTax { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtName")]
        private TextBox txtName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbOtherTax")]
        private NumericBox nmbOtherTax { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbCityTax")]
        private NumericBox nmbCityTax { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbTotalTax")]
        private NumericBox nmbTotalTax { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbCountyTax")]
        private NumericBox nmbCountyTax { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [CompilerGenerated]
        internal sealed class _Closure$__55-0
        {
            public NavigatorRowClickEventArgs $VB$Local_args;

            internal object _Lambda$__0() => 
                this.$VB$Local_args.GridRow.GetDataRow()["ID"];
        }
    }
}

