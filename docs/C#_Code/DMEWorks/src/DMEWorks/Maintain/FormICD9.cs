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

    [DesignerGenerated]
    public class FormICD9 : FormStringMaintain
    {
        private IContainer components;

        public FormICD9()
        {
            this.InitializeComponent();
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_icd9" };
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
                    if (0 >= command.ExecuteDelete("tbl_icd9"))
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
            messages.ConfirmDeleting = $"Are you really want to delete ICD9 '{this.ObjectID}'?";
            messages.DeletedSuccessfully = $"ICD9 '{this.ObjectID}' was successfully deleted.";
            return messages;
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.lblDescription = new Label();
            this.txtDescription = new TextBox();
            base.tpWorkArea.SuspendLayout();
            ((ISupportInitialize) base.ValidationErrors).BeginInit();
            ((ISupportInitialize) base.ValidationWarnings).BeginInit();
            base.SuspendLayout();
            this.txtObjectID.TabIndex = 3;
            base.tpWorkArea.Controls.Add(this.txtDescription);
            base.tpWorkArea.Controls.Add(this.lblDescription);
            base.tpWorkArea.Size = new Size(0x218, 0x102);
            base.tpWorkArea.Visible = true;
            base.tpWorkArea.Controls.SetChildIndex(this.lblDescription, 0);
            base.tpWorkArea.Controls.SetChildIndex(this.txtDescription, 0);
            base.tpWorkArea.Controls.SetChildIndex(this.txtObjectID, 0);
            base.tpWorkArea.Controls.SetChildIndex(this.lblObjectTypeName, 0);
            this.lblDescription.BackColor = Color.Transparent;
            this.lblDescription.Location = new Point(8, 0x30);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new Size(0x58, 0x18);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "Description";
            this.lblDescription.TextAlign = ContentAlignment.MiddleRight;
            this.txtDescription.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtDescription.Location = new Point(0x68, 0x30);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Size(0x128, 0x16);
            this.txtDescription.TabIndex = 1;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x220, 0x145);
            base.Name = "FormICD9";
            this.Text = "Maintain ICD 9";
            base.tpWorkArea.ResumeLayout(false);
            ((ISupportInitialize) base.ValidationErrors).EndInit();
            ((ISupportInitialize) base.ValidationWarnings).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override void InitPrintMenu()
        {
            ContextMenu menu = new ContextMenu();
            Cache.AddCategory(menu, "ICD9", new EventHandler(this.mnuPrintItem_Click));
            base.SetPrintMenu(menu);
        }

        protected override bool LoadObject(string Code)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT * FROM tbl_icd9 WHERE Code = :Code";
                    command.Parameters.Add("Code", MySqlType.VarChar, 10).Value = Code;
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

        private void mnuPrintItem_Click(object sender, EventArgs e)
        {
            ReportMenuItem item = sender as ReportMenuItem;
            if (item != null)
            {
                ReportParameters @params = new ReportParameters {
                    ["{?tbl_icd9.Code}"] = this.ObjectID
                };
                ClassGlobalObjects.ShowReport(item.ReportFileName, @params);
            }
        }

        protected override void OnTableUpdate()
        {
            string[] tableNames = new string[] { "tbl_icd9" };
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
                    command.Parameters.Add("Code", MySqlType.VarChar, 6).Value = this.txtObjectID.Text;
                    if (IsNew)
                    {
                        flag = 0 < command.ExecuteInsert("tbl_icd9");
                    }
                    else
                    {
                        string[] whereParameters = new string[] { "Code" };
                        flag = 0 < command.ExecuteUpdate("tbl_icd9", whereParameters);
                        flag ??= (0 < command.ExecuteInsert("tbl_icd9"));
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
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT Code, Description, ActiveDate, InactiveDate FROM tbl_icd9 ORDER BY Code", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill((args.Source as DataTableGridSource).Table);
            }
        }

        private void Search_InitializeAppearance(GridAppearanceBase appearance)
        {
            appearance.AutoGenerateColumns = false;
            appearance.Columns.Clear();
            appearance.AddTextColumn("Code", "Code", 50);
            appearance.AddTextColumn("Description", "Description", 250);
            appearance.AddTextColumn("ActiveDate", "ActiveDate", 80, appearance.DateStyle());
            appearance.AddTextColumn("InactiveDate", "InactiveDate", 80, appearance.DateStyle());
        }

        private void Search_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            _Closure$__15-0 e$__- = new _Closure$__15-0 {
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
        internal sealed class _Closure$__15-0
        {
            public NavigatorRowClickEventArgs $VB$Local_args;

            internal object _Lambda$__0() => 
                this.$VB$Local_args.GridRow.GetDataRow()["Code"];
        }
    }
}

