namespace DMEWorks.Maintain
{
    using Devart.Data.MySql;
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

    public class FormZipCode : FormStringMaintain
    {
        private IContainer components;

        public FormZipCode()
        {
            this.InitializeComponent();
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_zipcode" };
            options.TableNames = textArray1;
            base.AddNavigator(options);
            base.ChangesTracker.Subscribe(this.txtObjectID);
            base.ChangesTracker.Subscribe(this.txtCity);
            base.ChangesTracker.Subscribe(this.txtState);
        }

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            Functions.SetTextBoxText(this.txtCity, DBNull.Value);
            Functions.SetTextBoxText(this.txtState, DBNull.Value);
        }

        protected override void DeleteObject(string Zip)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("Zip", MySqlType.VarChar, 10).Value = Zip;
                    if (0 >= command.ExecuteDelete("tbl_zipcode"))
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
            messages.ConfirmDeleting = $"Are you really want to delete zip code '{this.ObjectID}'?";
            messages.DeletedSuccessfully = $"Zip code '{this.ObjectID}' was successfully deleted.";
            return messages;
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.lblCity = new Label();
            this.txtCity = new TextBox();
            this.txtState = new TextBox();
            this.lblState = new Label();
            base.tpWorkArea.SuspendLayout();
            base.SuspendLayout();
            this.txtObjectID.Location = new Point(80, 8);
            this.txtObjectID.MaxLength = 10;
            this.txtObjectID.Visible = true;
            this.lblObjectTypeName.Size = new Size(0x40, 0x18);
            this.lblObjectTypeName.Text = "Zip";
            this.lblObjectTypeName.Visible = true;
            Control[] controls = new Control[] { this.txtState, this.lblState, this.txtCity, this.lblCity };
            base.tpWorkArea.Controls.AddRange(controls);
            base.tpWorkArea.Size = new Size(0x218, 0x105);
            base.tpWorkArea.Visible = true;
            this.lblCity.BackColor = Color.Transparent;
            this.lblCity.Location = new Point(8, 0x30);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new Size(0x40, 0x18);
            this.lblCity.TabIndex = 2;
            this.lblCity.Text = "City";
            this.lblCity.TextAlign = ContentAlignment.MiddleRight;
            this.txtCity.AutoSize = false;
            this.txtCity.Location = new Point(80, 0x30);
            this.txtCity.MaxLength = 30;
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new Size(360, 0x15);
            this.txtCity.TabIndex = 3;
            this.txtCity.Text = "";
            this.txtState.AutoSize = false;
            this.txtState.Location = new Point(80, 80);
            this.txtState.MaxLength = 2;
            this.txtState.Name = "txtState";
            this.txtState.Size = new Size(0x30, 0x15);
            this.txtState.TabIndex = 5;
            this.txtState.Text = "";
            this.lblState.BackColor = Color.Transparent;
            this.lblState.Location = new Point(8, 80);
            this.lblState.Name = "lblState";
            this.lblState.Size = new Size(0x40, 0x18);
            this.lblState.TabIndex = 4;
            this.lblState.Text = "State";
            this.lblState.TextAlign = ContentAlignment.MiddleRight;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x220, 0x145);
            base.Name = "FormZipCode";
            this.Text = "Maintain Zip Code";
            base.tpWorkArea.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        protected override bool LoadObject(string Zip)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT * FROM tbl_zipcode WHERE Zip = :Zip";
                    command.Parameters.Add("Zip", MySqlType.VarChar, 10).Value = Zip;
                    using (MySqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleResult))
                    {
                        if (reader.Read())
                        {
                            this.ObjectID = reader["Zip"];
                            Functions.SetTextBoxText(this.txtCity, reader["City"]);
                            Functions.SetTextBoxText(this.txtState, reader["State"]);
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
            string[] tableNames = new string[] { "tbl_zipcode" };
            ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
        }

        protected void ProcessParameter_Zip(FormParameters Params)
        {
            if ((Params != null) && Params.ContainsKey("Zip"))
            {
                base.OpenObject(Params["Zip"]);
            }
        }

        protected override bool SaveObject(string Zip, bool IsNew)
        {
            bool flag;
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("City", MySqlType.VarChar, 30).Value = this.txtCity.Text;
                    command.Parameters.Add("State", MySqlType.VarChar, 2).Value = this.txtState.Text;
                    command.Parameters.Add("Zip", MySqlType.VarChar, 10).Value = this.txtObjectID.Text;
                    if (IsNew)
                    {
                        flag = 0 < command.ExecuteInsert("tbl_zipcode");
                    }
                    else
                    {
                        string[] whereParameters = new string[] { "Zip" };
                        flag = 0 < command.ExecuteUpdate("tbl_zipcode", whereParameters);
                        flag ??= (0 < command.ExecuteInsert("tbl_zipcode"));
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
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT Zip, State, City FROM tbl_zipcode", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill((args.Source as DataTableGridSource).Table);
            }
        }

        private void Search_InitializeAppearance(GridAppearanceBase appearance)
        {
            appearance.AutoGenerateColumns = false;
            appearance.Columns.Clear();
            appearance.AddTextColumn("Zip", "Zip", 60);
            appearance.AddTextColumn("State", "State", 40);
            appearance.AddTextColumn("City", "City", 200);
        }

        private void Search_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            _Closure$__25-0 e$__- = new _Closure$__25-0 {
                $VB$Local_args = args
            };
            base.OpenObject(new Func<object>(e$__-._Lambda$__0));
        }

        protected override void SetParameters(FormParameters Params)
        {
            base.ProcessParameter_EntityCreatedListener(Params);
            base.ProcessParameter_TabPage(Params);
            this.ProcessParameter_Zip(Params);
        }

        [field: AccessedThroughProperty("lblCity")]
        private Label lblCity { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCity")]
        private TextBox txtCity { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtState")]
        private TextBox txtState { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblState")]
        private Label lblState { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [CompilerGenerated]
        internal sealed class _Closure$__25-0
        {
            public NavigatorRowClickEventArgs $VB$Local_args;

            internal object _Lambda$__0() => 
                this.$VB$Local_args.GridRow.GetDataRow()["Zip"];
        }
    }
}

