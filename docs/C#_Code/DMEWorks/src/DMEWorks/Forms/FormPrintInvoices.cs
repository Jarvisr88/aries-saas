namespace DMEWorks.Forms
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Core;
    using DMEWorks.CrystalReports;
    using Microsoft.VisualBasic.CompilerServices;
    using My.Resources;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class FormPrintInvoices : DmeForm
    {
        private IContainer components;
        private const string CrLf = "\r\n";
        private const string SInvoicePrinting = "Invoice Printing";

        public FormPrintInvoices()
        {
            this.InitializeComponent();
            this.InitializeGridStyle(this.Grid.Appearance);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Grid_GridKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space)
            {
                e.Handled = true;
                try
                {
                    bool? nullable = null;
                    SetChecked(this.Grid.GetSelectedRows(), nullable);
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

        private void Grid_RowDoubleClick(object sender, GridMouseEventArgs e)
        {
            try
            {
                DataRow dataRow = e.Row.GetDataRow();
                if (dataRow != null)
                {
                    DataColumn column = dataRow.Table.Columns["InvoiceID"];
                    if (column != null)
                    {
                        int? nullable = NullableConvert.ToInt32(dataRow[column]);
                        if (nullable != null)
                        {
                            FormParameters @params = new FormParameters("ID", nullable);
                            ClassGlobalObjects.ShowForm(FormFactories.FormInvoice(), @params);
                        }
                    }
                }
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

        private void GridCheckAll_Click(object sender, EventArgs e)
        {
            try
            {
                this.Grid.EndEdit();
                SetChecked(this.Grid.GetRows(), true);
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

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormPrintInvoices));
            this.Grid = new SearchableGrid();
            this.cmsGrid = new ContextMenuStrip(this.components);
            this.tsmiGridCheckAll = new ToolStripMenuItem();
            this.tsmiGridUncheckAll = new ToolStripMenuItem();
            this.tsmiCheckSelected = new ToolStripMenuItem();
            this.tsmiGridUncheckSelected = new ToolStripMenuItem();
            this.ToolStrip1 = new ToolStrip();
            this.tsdbCheck = new ToolStripDropDownButton();
            this.tsbCheckSelected = new ToolStripMenuItem();
            this.tsbCheckAll = new ToolStripMenuItem();
            this.tsdbUncheck = new ToolStripDropDownButton();
            this.tsbUncheckSelected = new ToolStripMenuItem();
            this.tsbUncheckAll = new ToolStripMenuItem();
            this.tsbPrint = new ToolStripButton();
            this.tsdbShow = new ToolStripDropDownButton();
            this.tsmiShowPaperOnly = new ToolStripMenuItem();
            this.tsmiShowAll = new ToolStripMenuItem();
            this.tsmiShowEdiOnly = new ToolStripMenuItem();
            this.cmsGrid.SuspendLayout();
            this.ToolStrip1.SuspendLayout();
            base.SuspendLayout();
            this.Grid.Dock = DockStyle.Fill;
            this.Grid.Location = new Point(0, 0x19);
            this.Grid.Name = "Grid";
            this.Grid.Size = new Size(0x250, 0x18e);
            this.Grid.TabIndex = 1;
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.tsmiGridCheckAll, this.tsmiGridUncheckAll, this.tsmiCheckSelected, this.tsmiGridUncheckSelected };
            this.cmsGrid.Items.AddRange(toolStripItems);
            this.cmsGrid.Name = "cmsGrid";
            this.cmsGrid.Size = new Size(0xa8, 0x5c);
            this.tsmiGridCheckAll.Name = "tsmiGridCheckAll";
            this.tsmiGridCheckAll.Size = new Size(0xa7, 0x16);
            this.tsmiGridCheckAll.Text = "Check All";
            this.tsmiGridUncheckAll.Name = "tsmiGridUncheckAll";
            this.tsmiGridUncheckAll.Size = new Size(0xa7, 0x16);
            this.tsmiGridUncheckAll.Text = "Uncheck All";
            this.tsmiCheckSelected.Name = "tsmiCheckSelected";
            this.tsmiCheckSelected.Size = new Size(0xa7, 0x16);
            this.tsmiCheckSelected.Text = "Check Selected";
            this.tsmiGridUncheckSelected.Name = "tsmiGridUncheckSelected";
            this.tsmiGridUncheckSelected.Size = new Size(0xa7, 0x16);
            this.tsmiGridUncheckSelected.Text = "Uncheck Selected";
            ToolStripItem[] itemArray2 = new ToolStripItem[] { this.tsdbCheck, this.tsdbUncheck, this.tsdbShow, this.tsbPrint };
            this.ToolStrip1.Items.AddRange(itemArray2);
            this.ToolStrip1.Location = new Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new Size(0x250, 0x19);
            this.ToolStrip1.TabIndex = 0;
            this.ToolStrip1.Text = "ToolStrip1";
            this.tsdbCheck.DisplayStyle = ToolStripItemDisplayStyle.Text;
            ToolStripItem[] itemArray3 = new ToolStripItem[] { this.tsbCheckSelected, this.tsbCheckAll };
            this.tsdbCheck.DropDownItems.AddRange(itemArray3);
            this.tsdbCheck.Image = (Image) manager.GetObject("tsdbCheck.Image");
            this.tsdbCheck.ImageTransparentColor = Color.Magenta;
            this.tsdbCheck.Name = "tsdbCheck";
            this.tsdbCheck.Size = new Size(0x35, 0x16);
            this.tsdbCheck.Text = "Check";
            this.tsbCheckSelected.Name = "tsbCheckSelected";
            this.tsbCheckSelected.Size = new Size(0x76, 0x16);
            this.tsbCheckSelected.Text = "Selected";
            this.tsbCheckAll.Name = "tsbCheckAll";
            this.tsbCheckAll.Size = new Size(0x76, 0x16);
            this.tsbCheckAll.Text = "All";
            this.tsdbUncheck.DisplayStyle = ToolStripItemDisplayStyle.Text;
            ToolStripItem[] itemArray4 = new ToolStripItem[] { this.tsbUncheckSelected, this.tsbUncheckAll };
            this.tsdbUncheck.DropDownItems.AddRange(itemArray4);
            this.tsdbUncheck.Image = (Image) manager.GetObject("tsdbUncheck.Image");
            this.tsdbUncheck.Name = "tsdbUncheck";
            this.tsdbUncheck.Size = new Size(0x42, 0x16);
            this.tsdbUncheck.Text = "Uncheck";
            this.tsbUncheckSelected.Name = "tsbUncheckSelected";
            this.tsbUncheckSelected.Size = new Size(0x98, 0x16);
            this.tsbUncheckSelected.Text = "Selected";
            this.tsbUncheckAll.Name = "tsbUncheckAll";
            this.tsbUncheckAll.Size = new Size(0x98, 0x16);
            this.tsbUncheckAll.Text = "All";
            this.tsbPrint.Image = My.Resources.Resources.ImagePrint;
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Size = new Size(0x34, 0x16);
            this.tsbPrint.Text = "Print";
            this.tsdbShow.DisplayStyle = ToolStripItemDisplayStyle.Text;
            ToolStripItem[] itemArray5 = new ToolStripItem[] { this.tsmiShowPaperOnly, this.tsmiShowEdiOnly, this.tsmiShowAll };
            this.tsdbShow.DropDownItems.AddRange(itemArray5);
            this.tsdbShow.Image = (Image) manager.GetObject("tsdbShow.Image");
            this.tsdbShow.Name = "tsdbShow";
            this.tsdbShow.Size = new Size(0x31, 0x16);
            this.tsdbShow.Text = "Show";
            this.tsmiShowPaperOnly.Checked = true;
            this.tsmiShowPaperOnly.CheckState = CheckState.Checked;
            this.tsmiShowPaperOnly.Name = "tsmiShowPaperOnly";
            this.tsmiShowPaperOnly.Size = new Size(0x98, 0x16);
            this.tsmiShowPaperOnly.Text = "Paper Only";
            this.tsmiShowAll.Name = "tsmiShowAll";
            this.tsmiShowAll.Size = new Size(0x98, 0x16);
            this.tsmiShowAll.Text = "All";
            this.tsmiShowEdiOnly.Name = "tsmiShowEdiOnly";
            this.tsmiShowEdiOnly.Size = new Size(0x98, 0x16);
            this.tsmiShowEdiOnly.Text = "EDI Only";
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x250, 0x1a7);
            base.Controls.Add(this.Grid);
            base.Controls.Add(this.ToolStrip1);
            base.Name = "FormPrintInvoices";
            this.Text = "Form Invoices Print";
            this.cmsGrid.ResumeLayout(false);
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InitializeGridStyle(SearchableGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.AllowDelete = false;
            Appearance.AllowEdit = true;
            Appearance.AllowNew = false;
            Appearance.MultiSelect = true;
            Appearance.ContextMenuStrip = this.cmsGrid;
            Appearance.Columns.Clear();
            Appearance.AddBoolColumn("Checked", "...", 0x23).ReadOnly = false;
            Appearance.AddTextColumn("InvoiceID", "Invoice #", 60);
            Appearance.AddTextColumn("CustomerName", "Customer", 120);
            Appearance.AddTextColumn("SubmitTo", "Payer", 0x2d);
            Appearance.AddTextColumn("Balance", "Balance", 50, Appearance.PriceStyle());
            Appearance.AddTextColumn("InsuranceCompanyName", "Insurance Company", 120);
            Appearance.AddTextColumn("InvoiceFormName", "Invoice Form", 100);
        }

        private void LoadGrid()
        {
            string str = !this.tsmiShowPaperOnly.Checked ? (!this.tsmiShowEdiOnly.Checked ? "(1 = 1)" : "(tbl_insurancecompany.ECSFormat IN ('Region A', 'Region B', 'Region C', 'Region D', 'Ability', 'Zirmed', 'Medi-Cal', 'Availity', 'Office Ally'))") : "((tbl_insurancecompany.ECSFormat NOT IN ('Region A','Region B','Region C','Region D','Ability','Zirmed','Medi-Cal', 'Availity', 'Office Ally')) OR(statistics.CurrentPayer <> 'Ins1'))";
            TableInvoices dataTable = new TableInvoices();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    connection.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter("", connection))
                    {
                        adapter.SelectCommand.CommandText = "SELECT DISTINCT\r\n  statistics.InvoiceID,\r\n  statistics.CurrentPayer as SubmitTo,\r\n  statistics.BillableAmount - statistics.PaymentAmount - statistics.WriteoffAmount as Balance,\r\n  tbl_customer.ID as CustomerID,\r\n  CONCAT(tbl_customer.LastName, ', ', tbl_customer.FirstName) as CustomerName,\r\n  tbl_insurancecompany.ID as InsuranceCompanyID,\r\n  tbl_insurancecompany.Name as InsuranceCompanyName,\r\n  tbl_insurancecompany.ECSFormat as ECSFormat,\r\n  tbl_invoiceform.ID as InvoiceFormID,\r\n  tbl_invoiceform.Name as InvoiceFormName,\r\n  tbl_invoiceform.ReportFileName\r\nFROM view_invoicetransaction_statistics as statistics\r\n     INNER JOIN tbl_customer ON tbl_customer.ID = statistics.CustomerID\r\n     LEFT JOIN tbl_insurancecompany ON tbl_insurancecompany.ID = statistics.CurrentInsuranceCompanyID\r\n     LEFT JOIN tbl_invoiceform ON tbl_invoiceform.ID =\r\n        CASE WHEN statistics.CurrentInsuranceCompanyID IS NULL THEN tbl_customer.InvoiceFormID\r\n             ELSE tbl_insurancecompany.InvoiceFormID END\r\nWHERE (IFNULL(tbl_invoiceform.ReportFileName, '') <> '')\r\n  AND (statistics.InvoiceSubmitted = 0)\r\n  AND (statistics.BillableAmount - statistics.PaymentAmount - statistics.WriteoffAmount >= 0.01)\r\n  AND (statistics.CurrentPayer IN ('Ins1','Ins2','Ins3','Ins4','Patient'))\r\n  AND (" + str + ")\r\nORDER BY tbl_invoiceform.Name";
                        adapter.MissingSchemaAction = MissingSchemaAction.Ignore;
                        adapter.AcceptChangesDuringFill = true;
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.ShowException(exception);
                ProjectData.ClearProjectError();
            }
            this.Grid.GridSource = dataTable.ToGridSource();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.SafeInvoke(new Action(this.LoadGrid));
        }

        private void PrintInvoices()
        {
            this.Grid.EndEdit();
            TableInvoices tableSource = this.Grid.GetTableSource<TableInvoices>();
            Dictionary<string, string> dictionary = new Dictionary<string, string>(5);
            foreach (DataRow row in tableSource.Select("([Checked] = True)", "", DataViewRowState.CurrentRows))
            {
                object obj2 = row[tableSource.Col_ReportFileName];
                if (obj2 is string)
                {
                    string str = (obj2 as string).TrimEnd(new char[0]);
                    if (str != "")
                    {
                        dictionary[str] = row[tableSource.Col_InvoiceFormName] as string;
                    }
                }
            }
            if (dictionary.Count < 0)
            {
                MessageBox.Show("There are no invoices to print", "Invoice Printing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                foreach (KeyValuePair<string, string> pair in dictionary)
                {
                    if (!this.PrintInvoices(tableSource, pair.Key, pair.Value))
                    {
                        break;
                    }
                }
            }
        }

        private bool PrintInvoices(TableInvoices FTable, string Report, string Description)
        {
            bool flag;
            IEnumerator enumerator;
            try
            {
                Dictionary<int, int> dictionary;
                enumerator = Enum.GetValues(typeof(PayerEnum)).GetEnumerator();
                goto TR_0031;
            TR_0024:
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    connection.Open();
                    MySqlTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        using (MySqlCommand command = new MySqlCommand("", connection, transaction))
                        {
                            Dictionary<int, int>.KeyCollection.Enumerator enumerator2;
                            command.CommandText = "CALL Invoice_AddSubmitted(:InvoiceID, :SubmittedTo, 'Paper', Null, :LastUpdateUserID)";
                            command.Parameters.Add("InvoiceID", MySqlType.Int);
                            command.Parameters.Add("SubmittedTo", MySqlType.VarChar, 50);
                            command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                            try
                            {
                                enumerator2 = dictionary.Keys.GetEnumerator();
                                while (enumerator2.MoveNext())
                                {
                                    int current = enumerator2.Current;
                                    foreach (DataRow row2 in FTable.Select($"[InvoiceID] = '{current}'", "", DataViewRowState.CurrentRows))
                                    {
                                        command.Parameters["InvoiceID"].Value = row2[FTable.Col_InvoiceID];
                                        command.Parameters["SubmittedTo"].Value = row2[FTable.Col_SubmitTo];
                                        command.ExecuteNonQuery();
                                    }
                                }
                            }
                            finally
                            {
                                enumerator2.Dispose();
                            }
                        }
                        transaction.Commit();
                    }
                    catch (Exception exception5)
                    {
                        Exception ex = exception5;
                        ProjectData.SetProjectError(ex);
                        Exception exception = ex;
                        transaction.Rollback();
                        this.ShowException(exception);
                        ProjectData.ClearProjectError();
                    }
                }
            TR_0031:
                while (true)
                {
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                    PayerEnum enum2 = (PayerEnum) Conversions.ToInteger(enumerator.Current);
                    string filterExpression = $"([ReportFileName] = '{Report.Replace("'", "''")}') AND ([SubmitTo] = '{enum2.ToString()}') AND ([Checked] = True)";
                    dictionary = new Dictionary<int, int>();
                    DataRow[] rowArray = FTable.Select(filterExpression, "", DataViewRowState.CurrentRows);
                    int index = 0;
                    while (true)
                    {
                        if (index < rowArray.Length)
                        {
                            DataRow row = rowArray[index];
                            dictionary[NullableConvert.ToInt32(row[FTable.Col_InvoiceID], 0)] = NullableConvert.ToInt32(row[FTable.Col_InvoiceID], 0);
                            index++;
                            continue;
                        }
                        if (dictionary.Count <= 0)
                        {
                            break;
                        }
                        ReportParameters @params = new ReportParameters {
                            ["{?tbl_invoice.ID}"] = dictionary.Keys,
                            ["{?SubmitTo}"] = enum2.ToString()
                        };
                        DialogResult result = MessageBox.Show($"Prepare form '{Description}' for printing.

Click 'No' to skip form and mark invoices as submitted.
Click 'Cancel' to abort printing.", "Invoice Printing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.Cancel)
                        {
                            flag = false;
                        }
                        else
                        {
                            if (result == DialogResult.No)
                            {
                                goto TR_0024;
                            }
                            else
                            {
                                try
                                {
                                    ClassGlobalObjects.ShowReport(Report, @params, true);
                                    goto TR_0024;
                                }
                                catch (UserNotifyException exception1)
                                {
                                    UserNotifyException ex = exception1;
                                    ProjectData.SetProjectError(ex);
                                    UserNotifyException exception = ex;
                                    if (MessageBox.Show(string.Format(("Error printing form '{0}'\r\n\r\n" + exception.Message + "\r\n") + "\r\nProceed with printing?", Description), "Invoice Printing", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                                    {
                                        flag = false;
                                        ProjectData.ClearProjectError();
                                        return flag;
                                    }
                                    else
                                    {
                                        ProjectData.ClearProjectError();
                                    }
                                }
                                catch (Exception exception4)
                                {
                                    Exception ex = exception4;
                                    ProjectData.SetProjectError(ex);
                                    Exception exception2 = ex;
                                    Trace.WriteLine(exception2, "ERROR");
                                    Trace.Flush();
                                    if (MessageBox.Show(string.Format(("Error printing form '{0}'\r\n\r\n" + exception2.ToString() + "\r\n") + "\r\nProceed with printing?", Description), "Invoice Printing", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
                                    {
                                        flag = false;
                                        ProjectData.ClearProjectError();
                                        return flag;
                                    }
                                    else
                                    {
                                        ProjectData.ClearProjectError();
                                    }
                                }
                            }
                            break;
                        }
                        return flag;
                    }
                }
                return true;
            }
            finally
            {
                if (enumerator is IDisposable)
                {
                    (enumerator as IDisposable).Dispose();
                }
            }
            return flag;
        }

        private static void SetChecked(DataGridViewRow[] Rows, bool? value)
        {
            if ((Rows != null) && (Rows.Length != 0))
            {
                if (value != null)
                {
                    DataGridViewRow[] rowArray = Rows;
                    for (int i = 0; i < rowArray.Length; i++)
                    {
                        DataRow dataRow = rowArray[i].GetDataRow();
                        if (dataRow != null)
                        {
                            dataRow["Checked"] = value;
                        }
                    }
                }
                else
                {
                    DataGridViewRow[] rowArray2 = Rows;
                    for (int i = 0; i < rowArray2.Length; i++)
                    {
                        DataRow dataRow = rowArray2[i].GetDataRow();
                        if (dataRow != null)
                        {
                            dataRow["Checked"] = !NullableConvert.ToBoolean(dataRow["Checked"], false);
                        }
                    }
                }
            }
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            this.PrintInvoices();
        }

        private void tsmiCheckSelected_Click(object sender, EventArgs e)
        {
            try
            {
                this.Grid.EndEdit();
                SetChecked(this.Grid.GetSelectedRows(), true);
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

        private void tsmiGridUncheckAll_Click(object sender, EventArgs e)
        {
            try
            {
                this.Grid.EndEdit();
                SetChecked(this.Grid.GetRows(), false);
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

        private void tsmiGridUncheckSelected_Click(object sender, EventArgs e)
        {
            try
            {
                this.Grid.EndEdit();
                SetChecked(this.Grid.GetSelectedRows(), false);
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

        private void tsmiShowAll_Click(object sender, EventArgs e)
        {
            this.tsmiShowPaperOnly.Checked = false;
            this.tsmiShowEdiOnly.Checked = false;
            this.tsmiShowAll.Checked = true;
            this.LoadGrid();
        }

        private void tsmiShowEdiOnly_Click(object sender, EventArgs e)
        {
            this.tsmiShowPaperOnly.Checked = false;
            this.tsmiShowEdiOnly.Checked = true;
            this.tsmiShowAll.Checked = false;
            this.LoadGrid();
        }

        private void tsmiShowPaperOnly_Click(object sender, EventArgs e)
        {
            this.tsmiShowPaperOnly.Checked = true;
            this.tsmiShowEdiOnly.Checked = false;
            this.tsmiShowAll.Checked = false;
            this.LoadGrid();
        }

        [field: AccessedThroughProperty("Grid")]
        private SearchableGrid Grid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGrid")]
        private ContextMenuStrip cmsGrid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiGridCheckAll")]
        private ToolStripMenuItem tsmiGridCheckAll { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiGridUncheckAll")]
        private ToolStripMenuItem tsmiGridUncheckAll { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiCheckSelected")]
        private ToolStripMenuItem tsmiCheckSelected { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiGridUncheckSelected")]
        private ToolStripMenuItem tsmiGridUncheckSelected { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ToolStrip1")]
        private ToolStrip ToolStrip1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsdbCheck")]
        private ToolStripDropDownButton tsdbCheck { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsdbUncheck")]
        private ToolStripDropDownButton tsdbUncheck { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbPrint")]
        private ToolStripButton tsbPrint { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbCheckSelected")]
        private ToolStripMenuItem tsbCheckSelected { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbCheckAll")]
        private ToolStripMenuItem tsbCheckAll { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbUncheckSelected")]
        private ToolStripMenuItem tsbUncheckSelected { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbUncheckAll")]
        private ToolStripMenuItem tsbUncheckAll { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsdbShow")]
        private ToolStripDropDownButton tsdbShow { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiShowPaperOnly")]
        private ToolStripMenuItem tsmiShowPaperOnly { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiShowEdiOnly")]
        private ToolStripMenuItem tsmiShowEdiOnly { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiShowAll")]
        private ToolStripMenuItem tsmiShowAll { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        private bool IsDemoVersion =>
            Globals.SerialNumber.IsDemoSerial();

        private enum PayerEnum
        {
            Ins1,
            Ins2,
            Ins3,
            Ins4,
            Patient
        }

        private class TableInvoices : TableBase
        {
            public DataColumn Col_Checked;
            public DataColumn Col_InvoiceID;
            public DataColumn Col_SubmittedDate;
            public DataColumn Col_SubmitTo;
            public DataColumn Col_Balance;
            public DataColumn Col_CustomerID;
            public DataColumn Col_CustomerName;
            public DataColumn Col_InsuranceCompanyID;
            public DataColumn Col_InsuranceCompanyName;
            public DataColumn Col_InvoiceFormID;
            public DataColumn Col_InvoiceFormName;
            public DataColumn Col_ReportFileName;

            public TableInvoices()
            {
            }

            public TableInvoices(string TableName) : base(TableName)
            {
            }

            protected override void Initialize()
            {
                this.Col_Checked = base.Columns["Checked"];
                this.Col_InvoiceID = base.Columns["InvoiceID"];
                this.Col_SubmittedDate = base.Columns["SubmittedDate"];
                this.Col_SubmitTo = base.Columns["SubmitTo"];
                this.Col_Balance = base.Columns["Balance"];
                this.Col_CustomerID = base.Columns["CustomerID"];
                this.Col_CustomerName = base.Columns["CustomerName"];
                this.Col_InsuranceCompanyID = base.Columns["InsuranceCompanyID"];
                this.Col_InsuranceCompanyName = base.Columns["InsuranceCompanyName"];
                this.Col_InvoiceFormID = base.Columns["InvoiceFormID"];
                this.Col_InvoiceFormName = base.Columns["InvoiceFormName"];
                this.Col_ReportFileName = base.Columns["ReportFileName"];
            }

            protected override void InitializeClass()
            {
                this.Col_Checked = base.Columns.Add("Checked", typeof(bool));
                this.Col_Checked.AllowDBNull = false;
                this.Col_Checked.DefaultValue = true;
                this.Col_InvoiceID = base.Columns.Add("InvoiceID", typeof(int));
                this.Col_SubmittedDate = base.Columns.Add("SubmittedDate", typeof(DateTime));
                this.Col_SubmitTo = base.Columns.Add("SubmitTo", typeof(string));
                this.Col_Balance = base.Columns.Add("Balance", typeof(double));
                this.Col_CustomerID = base.Columns.Add("CustomerID", typeof(int));
                this.Col_CustomerName = base.Columns.Add("CustomerName", typeof(string));
                this.Col_InsuranceCompanyID = base.Columns.Add("InsuranceCompanyID", typeof(int));
                this.Col_InsuranceCompanyName = base.Columns.Add("InsuranceCompanyName", typeof(string));
                this.Col_InvoiceFormID = base.Columns.Add("InvoiceFormID", typeof(int));
                this.Col_InvoiceFormName = base.Columns.Add("InvoiceFormName", typeof(string));
                this.Col_ReportFileName = base.Columns.Add("ReportFileName", typeof(string));
            }
        }
    }
}

