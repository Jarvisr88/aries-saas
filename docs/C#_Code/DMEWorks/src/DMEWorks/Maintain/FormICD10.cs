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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormICD10 : FormStringMaintain
    {
        private IContainer components;

        public FormICD10()
        {
            this.InitializeComponent();
            base.cmnuFilter.Popup += new EventHandler(this.cmnuFilter_Popup);
            if (PagedGrids)
            {
                base.AddPagedNavigator(new SearchNavigatorEventsHandler(this));
            }
            else
            {
                base.AddSimpleNavigator(new SearchNavigatorEventsHandler(this));
            }
            base.ChangesTracker.Subscribe(this.txtObjectID);
            base.ChangesTracker.Subscribe(this.txtDescription);
        }

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            Functions.SetTextBoxText(this.txtDescription, DBNull.Value);
        }

        private void cmnuFilter_Popup(object sender, EventArgs e)
        {
            this.mnuFilterPagedGrids.Checked = PagedGrids;
        }

        protected override void DeleteObject(string Code)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("Code", MySqlType.VarChar, 6).Value = Code;
                    if (0 >= command.ExecuteDelete("tbl_icd10"))
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
            messages.ConfirmDeleting = $"Are you really want to delete ICD10 '{this.ObjectID}'?";
            messages.DeletedSuccessfully = $"ICD10 '{this.ObjectID}' was successfully deleted.";
            return messages;
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.lblDescription = new Label();
            this.txtDescription = new TextBox();
            this.mnuFilterPagedGrids = new MenuItem();
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
            MenuItem[] items = new MenuItem[] { this.mnuFilterPagedGrids };
            base.cmnuFilter.MenuItems.AddRange(items);
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
            this.txtDescription.Size = new Size(0xe8, 20);
            this.txtDescription.TabIndex = 1;
            this.mnuFilterPagedGrids.Index = 0;
            this.mnuFilterPagedGrids.Text = "Paged";
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x220, 0x145);
            base.Name = "FormICD10";
            this.Text = "Maintain ICD 10";
            base.tpWorkArea.ResumeLayout(false);
            base.tpWorkArea.PerformLayout();
            ((ISupportInitialize) base.ValidationErrors).EndInit();
            ((ISupportInitialize) base.ValidationWarnings).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override void InitPrintMenu()
        {
            ContextMenu menu = new ContextMenu();
            Cache.AddCategory(menu, "ICD10", new EventHandler(this.mnuPrintItem_Click));
            base.SetPrintMenu(menu);
        }

        protected override bool LoadObject(string Code)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT * FROM tbl_icd10 WHERE Code = :Code";
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

        private void mnuFilterPagedGrids_Click(object sender, EventArgs e)
        {
            this.mnuFilterPagedGrids.Checked = !this.mnuFilterPagedGrids.Checked;
            PagedGrids = this.mnuFilterPagedGrids.Checked;
            ClassGlobalObjects.ShowForm(FormFactories.FormICD10());
            base.Close();
        }

        private void mnuPrintItem_Click(object sender, EventArgs e)
        {
            ReportMenuItem item = sender as ReportMenuItem;
            if (item != null)
            {
                ReportParameters @params = new ReportParameters {
                    ["{?tbl_icd10.Code}"] = this.ObjectID
                };
                ClassGlobalObjects.ShowReport(item.ReportFileName, @params);
            }
        }

        protected override void OnTableUpdate()
        {
            string[] tableNames = new string[] { "tbl_icd10" };
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
                        flag = 0 < command.ExecuteInsert("tbl_icd10");
                    }
                    else
                    {
                        string[] whereParameters = new string[] { "Code" };
                        flag = 0 < command.ExecuteUpdate("tbl_icd10", whereParameters);
                        flag ??= (0 < command.ExecuteInsert("tbl_icd10"));
                    }
                }
            }
            return flag;
        }

        [field: AccessedThroughProperty("lblDescription")]
        private Label lblDescription { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDescription")]
        private TextBox txtDescription { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuFilterPagedGrids")]
        private MenuItem mnuFilterPagedGrids { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        private static bool PagedGrids
        {
            get => 
                RegistrySettings.GetUserBool("FormIcd10:PagedGrids").GetValueOrDefault(false);
            set => 
                RegistrySettings.SetUserBool("FormIcd10:PagedGrids", value);
        }

        private class SearchNavigatorEventsHandler : NavigatorEventsHandler
        {
            private readonly FormICD10 Form;

            public SearchNavigatorEventsHandler(FormICD10 form)
            {
                if (form == null)
                {
                    throw new ArgumentNullException("form");
                }
                this.Form = form;
            }

            public override void CreateSource(object sender, CreateSourceEventArgs args)
            {
                args.Source = new DataTable().ToGridSource();
            }

            private void FetchData(DataTable DataTable, PagedFilter SearchTerms)
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(GetQuery(SearchTerms), ClassGlobalObjects.ConnectionString_MySql))
                {
                    adapter.AcceptChangesDuringFill = true;
                    adapter.Fill(DataTable);
                }
            }

            public override void FillSource(object sender, FillSourceEventArgs args)
            {
                this.FetchData((args.Source as DataTableGridSource).Table, null);
            }

            public override void FillSource(object sender, PagedFillSourceEventArgs args)
            {
                this.FetchData((args.Source as DataTableGridSource).Table, args.Filter);
            }

            private static string GetQuery(PagedFilter SearchTerms)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("SELECT\r\n  Code\r\n, Description\r\n, ActiveDate\r\n, InactiveDate\r\nFROM tbl_icd10\r\nWHERE (1 = 1)\r\n");
                if ((SearchTerms != null) && !string.IsNullOrEmpty(SearchTerms.Filter))
                {
                    QueryExpression[] expressions = new QueryExpression[] { new QueryExpression("Code", MySqlType.VarChar), new QueryExpression("Description", MySqlType.VarChar) };
                    string str = MySqlFilterUtilities.BuildFilter(expressions, SearchTerms.Filter);
                    if (!string.IsNullOrEmpty(str))
                    {
                        builder.Append("  AND (").Append(str).Append(")\r\n");
                    }
                }
                builder.Append("ORDER BY Code\r\n");
                if (SearchTerms != null)
                {
                    builder.AppendFormat("LIMIT {0}, {1}", SearchTerms.Start, SearchTerms.Count);
                }
                return builder.ToString();
            }

            public override void InitializeAppearance(GridAppearanceBase appearance)
            {
                appearance.AutoGenerateColumns = false;
                appearance.Columns.Clear();
                appearance.AddTextColumn("Code", "Code", 50);
                appearance.AddTextColumn("Description", "Description", 250);
                appearance.AddTextColumn("ActiveDate", "ActiveDate", 80, appearance.DateStyle());
                appearance.AddTextColumn("InactiveDate", "InactiveDate", 80, appearance.DateStyle());
            }

            public override void NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
            {
                _Closure$__10-0 e$__- = new _Closure$__10-0 {
                    $VB$Local_args = args
                };
                this.Form.OpenObject(new Func<object>(e$__-._Lambda$__0));
            }

            public override IEnumerable<string> TableNames =>
                new string[] { "tbl_icd10" };

            [CompilerGenerated]
            internal sealed class _Closure$__10-0
            {
                public NavigatorRowClickEventArgs $VB$Local_args;

                internal object _Lambda$__0() => 
                    this.$VB$Local_args.GridRow.GetDataRow()["Code"];
            }
        }
    }
}

