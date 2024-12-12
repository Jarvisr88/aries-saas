﻿namespace DMEWorks.Maintain.Auxilary
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
    public class FormInsuranceCompanyGroup : FormAutoIncrementMaintain
    {
        private IContainer components;
        private const string TableName = "tbl_insurancecompanygroup";

        public FormInsuranceCompanyGroup()
        {
            this.InitializeComponent();
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_insurancecompanygroup" };
            options.TableNames = textArray1;
            base.AddNavigator(options);
            base.ChangesTracker.Subscribe(this.txtName);
        }

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            Functions.SetTextBoxText(this.txtName, DBNull.Value);
        }

        protected override void DeleteObject(int ID)
        {
            using (MySqlConnection connection = Globals.CreateConnection())
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    if (0 >= command.ExecuteDelete("tbl_insurancecompanygroup"))
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
            messages.ConfirmDeleting = $"Are you really want to delete insurance group '{this.txtName.Text}'?";
            messages.DeletedSuccessfully = $"Insurance group '{this.txtName.Text}' was successfully deleted.";
            return messages;
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.txtName = new TextBox();
            this.lblName = new Label();
            base.tpWorkArea.SuspendLayout();
            ((ISupportInitialize) base.ValidationErrors).BeginInit();
            base.SuspendLayout();
            base.tpWorkArea.Controls.Add(this.txtName);
            base.tpWorkArea.Controls.Add(this.lblName);
            this.txtName.Location = new Point(0x70, 0x10);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(0x128, 20);
            this.txtName.TabIndex = 1;
            this.lblName.Location = new Point(0x10, 0x10);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(0x58, 20);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name";
            this.lblName.TextAlign = ContentAlignment.MiddleRight;
            base.ClientSize = new Size(0x228, 0x189);
            base.Name = "FormInsuranceCompanyGroup";
            this.Text = "Maintain Insurance Company Group";
            base.tpWorkArea.ResumeLayout(false);
            base.tpWorkArea.PerformLayout();
            ((ISupportInitialize) base.ValidationErrors).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override bool LoadObject(int ID)
        {
            using (MySqlConnection connection = Globals.CreateConnection())
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT * FROM tbl_insurancecompanygroup WHERE ID = :ID";
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.ObjectID = reader["ID"];
                            Functions.SetTextBoxText(this.txtName, reader["Name"]);
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
            string[] tableNames = new string[] { "tbl_insurancecompanygroup" };
            ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
        }

        protected override bool SaveObject(int ID, bool IsNew)
        {
            bool flag;
            using (MySqlConnection connection = Globals.CreateConnection())
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("Name", MySqlType.VarChar, 50).Value = this.txtName.Text;
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    if (!IsNew)
                    {
                        command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                        string[] whereParameters = new string[] { "ID" };
                        flag = 0 < command.ExecuteUpdate("tbl_insurancecompanygroup", whereParameters);
                        flag ??= (0 < command.ExecuteInsert("tbl_insurancecompanygroup"));
                    }
                    else
                    {
                        flag = 0 < command.ExecuteInsert("tbl_insurancecompanygroup");
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
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT ID, Name FROM tbl_insurancecompanygroup ORDER BY Name", ClassGlobalObjects.ConnectionString_MySql))
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
            appearance.AddTextColumn("Name", "Name", 250);
        }

        private void Search_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            _Closure$__16-0 e$__- = new _Closure$__16-0 {
                $VB$Local_args = args
            };
            base.OpenObject(new Func<object>(e$__-._Lambda$__0));
        }

        protected override void ValidateObject(int ID, bool IsNew)
        {
            if (string.IsNullOrWhiteSpace(this.txtName.Text))
            {
                base.ValidationErrors.SetError(this.txtName, "Name can not be empty");
            }
            else
            {
                int num;
                if (IsNew)
                {
                    MySqlParameter parameter1 = new MySqlParameter("Name", MySqlType.VarChar, 50);
                    parameter1.Value = this.txtName.Text;
                    MySqlParameter[] parameters = new MySqlParameter[] { parameter1 };
                    num = Convert.ToInt32(Globals.ExecuteScalar("SELECT COUNT(*) as `Count` FROM tbl_insurancecompanygroup WHERE (Name = :Name)", parameters));
                }
                else
                {
                    MySqlParameter parameter2 = new MySqlParameter("Name", MySqlType.VarChar, 50);
                    parameter2.Value = this.txtName.Text;
                    MySqlParameter[] parameters = new MySqlParameter[2];
                    parameters[0] = parameter2;
                    MySqlParameter parameter3 = new MySqlParameter("ID", MySqlType.Int);
                    parameter3.Value = ID;
                    parameters[1] = parameter3;
                    num = Convert.ToInt32(Globals.ExecuteScalar("SELECT COUNT(*) as `Count` FROM tbl_insurancecompanygroup WHERE (Name = :Name) AND (ID != :ID)", parameters));
                }
                if (0 < num)
                {
                    base.ValidationErrors.SetError(this.txtName, "Name is in use already");
                }
            }
        }

        [field: AccessedThroughProperty("txtName")]
        private TextBox txtName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblName")]
        private Label lblName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [CompilerGenerated]
        internal sealed class _Closure$__16-0
        {
            public NavigatorRowClickEventArgs $VB$Local_args;

            internal object _Lambda$__0() => 
                this.$VB$Local_args.GridRow.GetDataRow()["ID"];
        }
    }
}

