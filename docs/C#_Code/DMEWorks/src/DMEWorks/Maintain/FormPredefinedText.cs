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

    [DesignerGenerated]
    public class FormPredefinedText : FormAutoIncrementMaintain
    {
        private IContainer components;

        public FormPredefinedText()
        {
            this.InitializeComponent();
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_predefinedtext" };
            options.TableNames = textArray1;
            base.AddNavigator(options);
            base.ChangesTracker.Subscribe(this.txtName);
            base.ChangesTracker.Subscribe(this.cmbType);
            base.ChangesTracker.Subscribe(this.txtText);
        }

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            Functions.SetTextBoxText(this.txtName, DBNull.Value);
            Functions.SetComboBoxText(this.cmbType, DBNull.Value);
            Functions.SetTextBoxText(this.txtText, DBNull.Value);
        }

        protected override void DeleteObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    if (0 >= command.ExecuteDelete("tbl_predefinedtext"))
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
            messages.ConfirmDeleting = $"Are you really want to delete predefined text '{this.txtName.Text}'?";
            messages.DeletedSuccessfully = $"Predefined text '{this.txtName.Text}' was successfully deleted.";
            return messages;
        }

        protected override void InitDropdowns()
        {
            using (DataTable table = new DataTable("table"))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("SHOW COLUMNS FROM tbl_predefinedtext", ClassGlobalObjects.ConnectionString_MySql))
                {
                    adapter.AcceptChangesDuringFill = true;
                    adapter.Fill(table);
                }
                Functions.LoadComboBoxItems(this.cmbType, table, "Type");
            }
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.cmbType = new ComboBox();
            this.lblType = new Label();
            this.txtText = new TextBox();
            this.lblText = new Label();
            this.txtName = new TextBox();
            this.lblName = new Label();
            base.tpWorkArea.SuspendLayout();
            base.tpWorkArea.Controls.Add(this.cmbType);
            base.tpWorkArea.Controls.Add(this.lblType);
            base.tpWorkArea.Controls.Add(this.txtText);
            base.tpWorkArea.Controls.Add(this.lblText);
            base.tpWorkArea.Controls.Add(this.txtName);
            base.tpWorkArea.Controls.Add(this.lblName);
            base.tpWorkArea.Name = "tpWorkArea";
            base.tpWorkArea.Size = new Size(0x178, 0x102);
            this.cmbType.Location = new Point(0x40, 0x20);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new Size(0xe8, 0x15);
            this.cmbType.TabIndex = 5;
            this.lblType.BackColor = Color.Transparent;
            this.lblType.Location = new Point(8, 0x20);
            this.lblType.Name = "lblType";
            this.lblType.Size = new Size(0x30, 0x15);
            this.lblType.TabIndex = 4;
            this.lblType.Text = "Type";
            this.lblType.TextAlign = ContentAlignment.MiddleRight;
            this.txtText.AcceptsReturn = true;
            this.txtText.AutoSize = false;
            this.txtText.BackColor = SystemColors.Window;
            this.txtText.Cursor = Cursors.IBeam;
            this.txtText.ForeColor = SystemColors.WindowText;
            this.txtText.Location = new Point(0x40, 0x38);
            this.txtText.MaxLength = 0;
            this.txtText.Multiline = true;
            this.txtText.Name = "txtText";
            this.txtText.ScrollBars = ScrollBars.Both;
            this.txtText.Size = new Size(0x130, 0xc0);
            this.txtText.TabIndex = 7;
            this.txtText.Text = "";
            this.txtText.WordWrap = false;
            this.lblText.BackColor = Color.Transparent;
            this.lblText.Location = new Point(8, 0x38);
            this.lblText.Name = "lblText";
            this.lblText.Size = new Size(0x30, 0x15);
            this.lblText.TabIndex = 6;
            this.lblText.Text = "Text";
            this.lblText.TextAlign = ContentAlignment.MiddleRight;
            this.txtName.AcceptsReturn = true;
            this.txtName.AutoSize = false;
            this.txtName.BackColor = SystemColors.Window;
            this.txtName.Cursor = Cursors.IBeam;
            this.txtName.ForeColor = SystemColors.WindowText;
            this.txtName.Location = new Point(0x40, 8);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.RightToLeft = RightToLeft.No;
            this.txtName.Size = new Size(0xe8, 0x16);
            this.txtName.TabIndex = 3;
            this.txtName.Text = "";
            this.lblName.BackColor = Color.Transparent;
            this.lblName.Location = new Point(8, 8);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(0x30, 0x15);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Name";
            this.lblName.TextAlign = ContentAlignment.MiddleRight;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x180, 0x145);
            base.Name = "FormPredefinedText";
            this.Text = "Maintain Predefined Text";
            base.tpWorkArea.ResumeLayout(false);
        }

        protected override bool LoadObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = $"SELECT * FROM tbl_predefinedtext WHERE ID = {ID}";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.ObjectID = reader["ID"];
                            Functions.SetTextBoxText(this.txtName, reader["Name"]);
                            Functions.SetComboBoxText(this.cmbType, reader["Type"]);
                            Functions.SetTextBoxText(this.txtText, reader["Text"]);
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
            string[] tableNames = new string[] { "tbl_predefinedtext", "tbl_predefinedtext_compliancenotes", "tbl_predefinedtext_customernotes", "tbl_predefinedtext_invoicenotes" };
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
                    command.Parameters.Add("Name", MySqlType.VarChar, 50).Value = this.txtName.Text;
                    command.Parameters.Add("Type", MySqlType.VarChar, 0x19).Value = this.cmbType.Text;
                    command.Parameters.Add("Text", MySqlType.Text, 0x10000).Value = this.txtText.Text;
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    if (!IsNew)
                    {
                        command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                        string[] whereParameters = new string[] { "ID" };
                        flag = 0 < command.ExecuteUpdate("tbl_predefinedtext", whereParameters);
                        flag ??= (0 < command.ExecuteInsert("tbl_predefinedtext"));
                    }
                    else
                    {
                        flag = 0 < command.ExecuteInsert("tbl_predefinedtext");
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
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT ID, Name, Type FROM tbl_predefinedtext ORDER BY Name", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill((args.Source as DataTableGridSource).Table);
            }
        }

        private void Search_InitializeAppearance(GridAppearanceBase appearance)
        {
            appearance.AutoGenerateColumns = false;
            appearance.Columns.Clear();
            appearance.AddTextColumn("ID", "ID", 60);
            appearance.AddTextColumn("Name", "Name", 200);
            appearance.AddTextColumn("Type", "Type", 120);
        }

        private void Search_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            _Closure$__31-0 e$__- = new _Closure$__31-0 {
                $VB$Local_args = args
            };
            base.OpenObject(new Func<object>(e$__-._Lambda$__0));
        }

        [field: AccessedThroughProperty("txtName")]
        private TextBox txtName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblName")]
        private Label lblName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbType")]
        private ComboBox cmbType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblType")]
        private Label lblType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtText")]
        private TextBox txtText { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblText")]
        private Label lblText { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [CompilerGenerated]
        internal sealed class _Closure$__31-0
        {
            public NavigatorRowClickEventArgs $VB$Local_args;

            internal object _Lambda$__0() => 
                this.$VB$Local_args.GridRow.GetDataRow()["ID"];
        }
    }
}

