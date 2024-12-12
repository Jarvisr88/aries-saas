namespace DMEWorks.Maintain.Auxilary
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
    public class FormProviderNumberType : FormStringMaintain
    {
        private IContainer components;
        private const string TableName = "tbl_providernumbertype";

        public FormProviderNumberType()
        {
            this.InitializeComponent();
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_providernumbertype" };
            options.TableNames = textArray1;
            base.AddNavigator(options);
            base.ChangesTracker.Subscribe(this.txtDescription);
            base.ChangesTracker.Subscribe(this.txtObjectID);
        }

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            Functions.SetTextBoxText(this.txtDescription, DBNull.Value);
        }

        protected override void DeleteObject(string Code)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("Code", MySqlType.VarChar, 6).Value = Code;
                    if (0 >= command.ExecuteDelete("tbl_providernumbertype"))
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
            messages.ConfirmDeleting = $"Are you really want to delete provider number type '{this.ObjectID}'?";
            messages.DeletedSuccessfully = $"Provider number type '{this.ObjectID}' was successfully deleted.";
            return messages;
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.txtDescription = new TextBox();
            this.lblDescription = new Label();
            base.tpWorkArea.SuspendLayout();
            ((ISupportInitialize) base.ValidationErrors).BeginInit();
            base.SuspendLayout();
            base.tpWorkArea.Controls.Add(this.txtDescription);
            base.tpWorkArea.Controls.Add(this.lblDescription);
            base.tpWorkArea.Size = new Size(0x1f8, 0x128);
            base.tpWorkArea.Controls.SetChildIndex(this.txtObjectID, 0);
            base.tpWorkArea.Controls.SetChildIndex(this.lblObjectTypeName, 0);
            base.tpWorkArea.Controls.SetChildIndex(this.lblDescription, 0);
            base.tpWorkArea.Controls.SetChildIndex(this.txtDescription, 0);
            this.txtDescription.Location = new Point(0x68, 40);
            this.txtDescription.MaxLength = 40;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Size(0x188, 20);
            this.txtDescription.TabIndex = 5;
            this.lblDescription.BackColor = Color.Transparent;
            this.lblDescription.Location = new Point(8, 40);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new Size(0x58, 0x18);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Text = "Description";
            this.lblDescription.TextAlign = ContentAlignment.MiddleRight;
            base.ClientSize = new Size(0x200, 0x16b);
            base.Name = "FormProviderNumberType";
            this.Text = "Maintain Provider Number Type";
            base.tpWorkArea.ResumeLayout(false);
            base.tpWorkArea.PerformLayout();
            ((ISupportInitialize) base.ValidationErrors).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override bool LoadObject(string Code)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT Code, Description FROM tbl_providernumbertype WHERE Code = :Code";
                    command.Parameters.Add("Code", MySqlType.VarChar, 6).Value = Code;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.ObjectID = reader["Code"];
                            Functions.SetTextBoxText(this.txtDescription, reader["Description"]);
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
            string[] tableNames = new string[] { "tbl_providernumbertype" };
            ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
        }

        protected override bool SaveObject(string Code, bool IsNew)
        {
            bool flag;
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("Description", MySqlType.VarChar, 100).Value = this.txtDescription.Text;
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    command.Parameters.Add("Code", MySqlType.VarChar, 6).Value = this.txtObjectID.Text;
                    if (IsNew)
                    {
                        flag = 0 < command.ExecuteInsert("tbl_providernumbertype");
                    }
                    else
                    {
                        string[] whereParameters = new string[] { "Code" };
                        flag = 0 < command.ExecuteUpdate("tbl_providernumbertype", whereParameters);
                        flag ??= (0 < command.ExecuteInsert("tbl_providernumbertype"));
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
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT Code, Description FROM tbl_providernumbertype ORDER BY Code", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill((args.Source as DataTableGridSource).Table);
            }
        }

        private void Search_InitializeAppearance(GridAppearanceBase Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("Code", "Code", 50);
            Appearance.AddTextColumn("Description", "Description", 250);
        }

        private void Search_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            _Closure$__16-0 e$__- = new _Closure$__16-0 {
                $VB$Local_args = args
            };
            base.OpenObject(new Func<object>(e$__-._Lambda$__0));
        }

        protected override void ValidateObject(string Code, bool IsNew)
        {
            int? nullable;
            if (this.txtDescription.Text.Trim() == "")
            {
                base.ValidationErrors.SetError(this.txtDescription, "You should input nonempty string for name");
            }
            if (!IsNew)
            {
                nullable = null;
            }
            else
            {
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("", connection))
                    {
                        command.CommandText = "SELECT COUNT(Code) as `Count` FROM tbl_providernumbertype WHERE Code = :Code";
                        command.Parameters.Add("Code", MySqlType.VarChar, 6).Value = Code;
                        nullable = NullableConvert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            if ((nullable != null) && (0 < nullable.Value))
            {
                base.ValidationErrors.SetError(this.txtObjectID, "Provider number type with such ID already exists");
            }
        }

        [field: AccessedThroughProperty("txtDescription")]
        private TextBox txtDescription { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDescription")]
        private Label lblDescription { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [CompilerGenerated]
        internal sealed class _Closure$__16-0
        {
            public NavigatorRowClickEventArgs $VB$Local_args;

            internal object _Lambda$__0() => 
                this.$VB$Local_args.GridRow.GetDataRow()["Code"];
        }
    }
}

