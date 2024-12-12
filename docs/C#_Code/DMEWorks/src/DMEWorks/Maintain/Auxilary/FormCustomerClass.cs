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
    public class FormCustomerClass : FormStringMaintain
    {
        private IContainer components;
        private const string TableName = "tbl_customerclass";

        public FormCustomerClass()
        {
            this.InitializeComponent();
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_customerclass" };
            options.TableNames = textArray1;
            base.AddNavigator(options);
            base.ChangesTracker.Subscribe(this.txtObjectID);
            base.ChangesTracker.Subscribe(this.txtDescription);
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
                    if (0 >= command.ExecuteDelete("tbl_customerclass"))
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
            messages.ConfirmDeleting = $"Are you really want to delete Customer class '{this.ObjectID}'?";
            messages.DeletedSuccessfully = $"Customer class '{this.ObjectID}' was successfully deleted.";
            return messages;
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.lblDescription = new Label();
            this.txtDescription = new TextBox();
            base.tpWorkArea.SuspendLayout();
            this.txtObjectID.MaxLength = 2;
            this.txtObjectID.Name = "txtObjectID";
            this.lblObjectTypeName.Name = "lblObjectTypeName";
            base.tpWorkArea.Controls.Add(this.txtDescription);
            base.tpWorkArea.Controls.Add(this.lblDescription);
            base.tpWorkArea.Name = "tpWorkArea";
            base.tpWorkArea.Size = new Size(0x1f8, 250);
            base.tpWorkArea.Visible = true;
            base.tpWorkArea.Controls.SetChildIndex(this.lblDescription, 0);
            base.tpWorkArea.Controls.SetChildIndex(this.txtDescription, 0);
            base.tpWorkArea.Controls.SetChildIndex(this.txtObjectID, 0);
            base.tpWorkArea.Controls.SetChildIndex(this.lblObjectTypeName, 0);
            this.lblDescription.Location = new Point(8, 40);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new Size(0x58, 0x18);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description";
            this.lblDescription.TextAlign = ContentAlignment.MiddleRight;
            this.txtDescription.AutoSize = false;
            this.txtDescription.Location = new Point(0x68, 40);
            this.txtDescription.MaxLength = 50;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Size(0x170, 0x16);
            this.txtDescription.TabIndex = 3;
            this.txtDescription.Text = "";
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x200, 0x13d);
            base.Name = "FormCustomerClass";
            this.Text = "Maintain Customer Class";
            base.tpWorkArea.ResumeLayout(false);
        }

        protected override bool LoadObject(string Code)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT * FROM tbl_customerclass WHERE Code = :Code";
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
            string[] tableNames = new string[] { "tbl_customerclass" };
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
                    command.Parameters.Add("Description", MySqlType.VarChar, 50).Value = this.txtDescription.Text;
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    command.Parameters.Add("Code", MySqlType.VarChar, 6).Value = Code;
                    if (IsNew)
                    {
                        flag = 0 < command.ExecuteInsert("tbl_customerclass");
                    }
                    else
                    {
                        string[] whereParameters = new string[] { "Code" };
                        flag = 0 < command.ExecuteUpdate("tbl_customerclass", whereParameters);
                        flag ??= (0 < command.ExecuteInsert("tbl_customerclass"));
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
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT Code, Description FROM tbl_customerclass ORDER BY Code", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill((args.Source as DataTableGridSource).Table);
            }
        }

        private void Search_InitializeAppearance(GridAppearanceBase appearance)
        {
            appearance.AutoGenerateColumns = false;
            appearance.Columns.Clear();
            appearance.AddTextColumn("Code", "Code", 60);
            appearance.AddTextColumn("Description", "Description", 200);
        }

        private void Search_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            _Closure$__16-0 e$__- = new _Closure$__16-0 {
                $VB$Local_args = args
            };
            base.OpenObject(new Func<object>(e$__-._Lambda$__0));
        }

        [field: AccessedThroughProperty("lblDescription")]
        private Label lblDescription { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDescription")]
        private TextBox txtDescription { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

