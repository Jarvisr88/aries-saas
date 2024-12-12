namespace DMEWorks.Forms
{
    using Devart.Data.MySql;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using Edi.Data;
    using Edi.Eligibility;
    using Edi.Parsing;
    using Edi.Version4;
    using Edi.Version4.T271;
    using Edi.Version5;
    using Edi.Version5.T271;
    using Edi.Viewers;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using System.Xml;

    [DesignerGenerated]
    public class FormEligibility : DmeForm, IParameters
    {
        private IContainer components;
        private const string CrLf = "\r\n";
        private bool FLoadGridEnabled = true;

        public FormEligibility()
        {
            this.InitializeComponent();
            this.InitializeGrid(this.Grid.Appearance);
        }

        private void cmbInventoryItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }

        private void cmbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }

        private void cmsGrid_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            DataRow[] rowArray = this.Grid.GetSelectedRows().GetDataRows().ToArray<DataRow>();
            if (rowArray.Length == 1)
            {
                DataRow row = rowArray[0];
                if (!row.IsNull("ResponseTime"))
                {
                    this.cmsGrid.Tag = row;
                    e.Cancel = false;
                }
            }
        }

        private void cmsGridPrint_Click(object sender, EventArgs e)
        {
            this.ProcessContextMenuCommand((_Closure$__.$I87-0 == null) ? (_Closure$__.$I87-0 = new Action<ITextBatch>(_Closure$__.$I._Lambda$__87-0)) : _Closure$__.$I87-0);
        }

        private void cmsGridViewResponseEdi_Click(object sender, EventArgs e)
        {
            this.ProcessContextMenuCommand((_Closure$__.$I88-0 == null) ? (_Closure$__.$I88-0 = new Action<ITextBatch>(_Closure$__.$I._Lambda$__88-0)) : _Closure$__.$I88-0);
        }

        private void cmsGridViewResponseFormatted_Click(object sender, EventArgs e)
        {
            _Closure$__91-0 e$__- = new _Closure$__91-0();
            HtmlConversionOptions options1 = new HtmlConversionOptions();
            options1.ExcludedCodes = "|";
            options1.PreferedCodes = "|";
            e$__-.$VB$Local_options = options1;
            if (sender != this.cmsGridViewResponseFormattedCompact)
            {
                if (sender != this.cmsGridViewResponseFormattedOther)
                {
                    if (sender != this.cmsGridViewResponseFormattedRaw)
                    {
                        if (sender != this.cmsGridViewResponseFormattedVerbose)
                        {
                            return;
                        }
                        else
                        {
                            e$__-.$VB$Local_options.Format = HtmlFormat.Verbose;
                        }
                    }
                    else
                    {
                        e$__-.$VB$Local_options.Format = HtmlFormat.Raw;
                    }
                }
                else
                {
                    e$__-.$VB$Local_options.Format = HtmlFormat.Other;
                }
            }
            else
            {
                e$__-.$VB$Local_options.Format = HtmlFormat.Compact;
            }
            this.ProcessContextMenuCommand(new Action<ITextBatch>(e$__-._Lambda$__0));
        }

        private void cmsGridViewResponseText_Click(object sender, EventArgs e)
        {
            this.ProcessContextMenuCommand((_Closure$__.$I89-0 == null) ? (_Closure$__.$I89-0 = new Action<ITextBatch>(_Closure$__.$I._Lambda$__89-0)) : _Closure$__.$I89-0);
        }

        private void cmsGridViewResponseTree_Click(object sender, EventArgs e)
        {
            this.ProcessContextMenuCommand((_Closure$__.$I90-0 == null) ? (_Closure$__.$I90-0 = new Action<ITextBatch>(_Closure$__.$I._Lambda$__90-0)) : _Closure$__.$I90-0);
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

        protected override void InitDropdowns()
        {
            Cache.InitDropdown(this.cmbCustomer, "tbl_customer", null);
            Cache.InitDropdown(this.cmbInsuranceCompany, "tbl_insurancecompany", null);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Grid = new SearchableGrid();
            this.Panel1 = new Panel();
            this.lblCustomer = new Label();
            this.cmbInsuranceCompany = new Combobox();
            this.lblInsuranceCompany = new Label();
            this.cmbCustomer = new Combobox();
            this.cmsGrid = new ContextMenuStrip(this.components);
            this.tsmiHeader = new ToolStripMenuItem();
            this.tsmiDelimiter = new ToolStripSeparator();
            this.cmsGridPrintResponse = new ToolStripMenuItem();
            this.cmsGridViewResponseEdi = new ToolStripMenuItem();
            this.cmsGridViewResponseTree = new ToolStripMenuItem();
            this.cmsGridViewResponseText = new ToolStripMenuItem();
            this.cmsGridViewResponseFormatted = new ToolStripMenuItem();
            this.cmsGridViewResponseFormattedCompact = new ToolStripMenuItem();
            this.cmsGridViewResponseFormattedOther = new ToolStripMenuItem();
            this.cmsGridViewResponseFormattedRaw = new ToolStripMenuItem();
            this.cmsGridViewResponseFormattedVerbose = new ToolStripMenuItem();
            this.Panel1.SuspendLayout();
            this.cmsGrid.SuspendLayout();
            base.SuspendLayout();
            this.Grid.Dock = DockStyle.Fill;
            this.Grid.Location = new Point(0, 60);
            this.Grid.Name = "Grid";
            this.Grid.Size = new Size(0x278, 0x189);
            this.Grid.TabIndex = 3;
            this.Panel1.Controls.Add(this.lblCustomer);
            this.Panel1.Controls.Add(this.cmbInsuranceCompany);
            this.Panel1.Controls.Add(this.lblInsuranceCompany);
            this.Panel1.Controls.Add(this.cmbCustomer);
            this.Panel1.Dock = DockStyle.Top;
            this.Panel1.Location = new Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(0x278, 60);
            this.Panel1.TabIndex = 2;
            this.lblCustomer.Location = new Point(8, 8);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new Size(80, 0x15);
            this.lblCustomer.TabIndex = 0;
            this.lblCustomer.Text = "Customer";
            this.lblCustomer.TextAlign = ContentAlignment.MiddleRight;
            this.cmbInsuranceCompany.Location = new Point(0x60, 0x20);
            this.cmbInsuranceCompany.Name = "cmbInsuranceCompany";
            this.cmbInsuranceCompany.Size = new Size(400, 0x15);
            this.cmbInsuranceCompany.TabIndex = 3;
            this.lblInsuranceCompany.Location = new Point(8, 0x20);
            this.lblInsuranceCompany.Name = "lblInsuranceCompany";
            this.lblInsuranceCompany.Size = new Size(80, 0x15);
            this.lblInsuranceCompany.TabIndex = 2;
            this.lblInsuranceCompany.Text = "Ins Company";
            this.lblInsuranceCompany.TextAlign = ContentAlignment.MiddleRight;
            this.cmbCustomer.Location = new Point(0x60, 8);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new Size(400, 0x15);
            this.cmbCustomer.TabIndex = 1;
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.tsmiHeader, this.tsmiDelimiter, this.cmsGridPrintResponse, this.cmsGridViewResponseEdi, this.cmsGridViewResponseTree, this.cmsGridViewResponseText, this.cmsGridViewResponseFormatted };
            this.cmsGrid.Items.AddRange(toolStripItems);
            this.cmsGrid.Name = "cmsGrid";
            this.cmsGrid.Size = new Size(0xaf, 0x8e);
            this.tsmiHeader.Enabled = false;
            this.tsmiHeader.Name = "tsmiHeader";
            this.tsmiHeader.Size = new Size(0xae, 0x16);
            this.tsmiHeader.Text = "Response";
            this.tsmiDelimiter.Name = "tsmiDelimiter";
            this.tsmiDelimiter.Size = new Size(0xab, 6);
            this.cmsGridPrintResponse.Name = "cmsGridPrintResponse";
            this.cmsGridPrintResponse.Size = new Size(0xae, 0x16);
            this.cmsGridPrintResponse.Text = "Print";
            this.cmsGridViewResponseEdi.Name = "cmsGridViewResponseEdi";
            this.cmsGridViewResponseEdi.Size = new Size(0xae, 0x16);
            this.cmsGridViewResponseEdi.Text = "View In Edi Viewer";
            this.cmsGridViewResponseTree.Name = "cmsGridViewResponseTree";
            this.cmsGridViewResponseTree.Size = new Size(0xae, 0x16);
            this.cmsGridViewResponseTree.Text = "View In Tree Viewer";
            this.cmsGridViewResponseText.Name = "cmsGridViewResponseText";
            this.cmsGridViewResponseText.Size = new Size(0xae, 0x16);
            this.cmsGridViewResponseText.Text = "View In Text Viewer";
            ToolStripItem[] itemArray2 = new ToolStripItem[] { this.cmsGridViewResponseFormattedCompact, this.cmsGridViewResponseFormattedVerbose, this.cmsGridViewResponseFormattedRaw, this.cmsGridViewResponseFormattedOther };
            this.cmsGridViewResponseFormatted.DropDownItems.AddRange(itemArray2);
            this.cmsGridViewResponseFormatted.Name = "cmsGridViewResponseFormatted";
            this.cmsGridViewResponseFormatted.Size = new Size(0xae, 0x16);
            this.cmsGridViewResponseFormatted.Text = "View Formatted";
            this.cmsGridViewResponseFormattedCompact.Name = "cmsGridViewResponseFormattedCompact";
            this.cmsGridViewResponseFormattedCompact.Size = new Size(180, 0x16);
            this.cmsGridViewResponseFormattedCompact.Text = "Compact";
            this.cmsGridViewResponseFormattedOther.Name = "cmsGridViewResponseFormattedOther";
            this.cmsGridViewResponseFormattedOther.Size = new Size(180, 0x16);
            this.cmsGridViewResponseFormattedOther.Text = "Other";
            this.cmsGridViewResponseFormattedRaw.Name = "cmsGridViewResponseFormattedRaw";
            this.cmsGridViewResponseFormattedRaw.Size = new Size(180, 0x16);
            this.cmsGridViewResponseFormattedRaw.Text = "Raw";
            this.cmsGridViewResponseFormattedVerbose.Name = "cmsGridViewResponseFormattedVerbose";
            this.cmsGridViewResponseFormattedVerbose.Size = new Size(180, 0x16);
            this.cmsGridViewResponseFormattedVerbose.Text = "Verbose";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x278, 0x1c5);
            base.Controls.Add(this.Grid);
            base.Controls.Add(this.Panel1);
            base.Name = "FormEligibility";
            this.Text = "Eligibility";
            this.Panel1.ResumeLayout(false);
            this.cmsGrid.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void InitializeGrid(SearchableGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Appearance.MultiSelect = false;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("CustomerName", "Customer", 80);
            Appearance.AddTextColumn("Relationship", "Relation", 60);
            Appearance.AddTextColumn("InsuredName", "Insured", 80);
            Appearance.AddTextColumn("InsuranceCompanyName", "Ins. Company", 80);
            Appearance.AddTextColumn("Status", "Status", 80);
            Appearance.AddTextColumn("RequestTime", "Requested", 80, Appearance.DateStyle());
            Appearance.AddTextColumn("ResponseTime", "Responded", 80, Appearance.DateStyle());
            Appearance.ContextMenuStrip = this.cmsGrid;
        }

        [HandleDatabaseChanged("tbl_ability_eligibility_request")]
        public void LoadGrid()
        {
            if (this.FLoadGridEnabled)
            {
                int? nullable = NullableConvert.ToInt32(this.cmbInsuranceCompany.SelectedValue);
                int? nullable2 = NullableConvert.ToInt32(this.cmbCustomer.SelectedValue);
                string selectCommandText = "SELECT\r\n  request.ID\r\n, customer.ID as CustomerID\r\n, CONCAT(customer.LastName, ', ', customer.FirstName) as CustomerName\r\n, tbl_relationship.Description as Relationship\r\n, CASE WHEN policy.RelationshipCode = '18' \r\n       THEN CONCAT(customer.LastName, ', ', customer.FirstName)\r\n       ELSE CONCAT(policy  .LastName, ', ', policy.FirstName) END as InsuredName\r\n, tbl_insurancecompany.ID   as InsuranceCompanyID\r\n, tbl_insurancecompany.Name as InsuranceCompanyName\r\n, CASE WHEN IFNULL(request.ResponseText, '') = '' \r\n       THEN 'Waiting For Response'\r\n       ELSE 'Got Response' END as Status\r\n, request.RequestTime\r\n, request.ResponseTime\r\nFROM tbl_ability_eligibility_request as request\r\n     LEFT JOIN tbl_customer as customer ON request.CustomerID = customer.ID\r\n     LEFT JOIN tbl_customer_insurance AS policy ON request.CustomerID          = policy.CustomerID\r\n                                               AND request.CustomerInsuranceID = policy.ID\r\n     LEFT JOIN tbl_relationship ON policy.RelationshipCode = tbl_relationship.Code\r\n     LEFT JOIN tbl_insurancecompany ON policy.InsuranceCompanyID = tbl_insurancecompany.ID\r\nWHERE (1 = 1)";
                if (nullable2 != null)
                {
                    selectCommandText = selectCommandText + "\r\n" + $"  AND (customer.ID = {nullable2.Value})";
                }
                if (nullable != null)
                {
                    selectCommandText = selectCommandText + "\r\n" + $"  AND (tbl_insurancecompany.ID = {nullable.Value})";
                }
                DataTable dataTable = new DataTable("tbl_eligibility");
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(selectCommandText, ClassGlobalObjects.ConnectionString_MySql))
                {
                    adapter.AcceptChangesDuringFill = true;
                    adapter.Fill(dataTable);
                }
                this.Grid.GridSource = dataTable.ToGridSource();
            }
        }

        private void ProcessContextMenuCommand(Action<ITextBatch> action)
        {
            try
            {
                ITextBatch batch;
                DataRow tag = this.cmsGrid.Tag as DataRow;
                if (tag != null)
                {
                    if (!tag.IsNull("ID"))
                    {
                        using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                        {
                            using (MySqlCommand command = new MySqlCommand("", connection))
                            {
                                command.CommandText = "SELECT ID, ResponseText as Text FROM tbl_ability_eligibility_request WHERE (ID = " + Conversions.ToString(tag["ID"]) + ")";
                                connection.Open();
                                using (MySqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        batch = new BatchWrapper(Convert.ToInt32(reader["ID"]), Convert.ToString(reader["Text"]));
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
                action(batch);
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

        protected void SetParameters(FormParameters Params)
        {
            try
            {
                this.FLoadGridEnabled = false;
                try
                {
                    if (Params != null)
                    {
                        if (Params.ContainsKey("InsuranceCompanyID"))
                        {
                            int? nullable = NullableConvert.ToInt32(Params["InsuranceCompanyID"]);
                            if (nullable != null)
                            {
                                this.cmbInsuranceCompany.SelectedValue = nullable.Value;
                            }
                        }
                        if (Params.ContainsKey("CustomerID"))
                        {
                            int? nullable2 = NullableConvert.ToInt32(Params["CustomerID"]);
                            if (nullable2 != null)
                            {
                                this.cmbCustomer.SelectedValue = nullable2.Value;
                            }
                        }
                    }
                }
                finally
                {
                    this.FLoadGridEnabled = true;
                }
                this.LoadGrid();
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

        [field: AccessedThroughProperty("Grid")]
        private SearchableGrid Grid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel1")]
        private Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbInsuranceCompany")]
        private Combobox cmbInsuranceCompany { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInsuranceCompany")]
        private Label lblInsuranceCompany { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbCustomer")]
        private Combobox cmbCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGrid")]
        private ContextMenuStrip cmsGrid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCustomer")]
        private Label lblCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGridPrintResponse")]
        private ToolStripMenuItem cmsGridPrintResponse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiHeader")]
        private ToolStripMenuItem tsmiHeader { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiDelimiter")]
        private ToolStripSeparator tsmiDelimiter { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGridViewResponseEdi")]
        private ToolStripMenuItem cmsGridViewResponseEdi { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGridViewResponseTree")]
        private ToolStripMenuItem cmsGridViewResponseTree { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGridViewResponseText")]
        private ToolStripMenuItem cmsGridViewResponseText { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGridViewResponseFormatted")]
        private ToolStripMenuItem cmsGridViewResponseFormatted { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGridViewResponseFormattedCompact")]
        private ToolStripMenuItem cmsGridViewResponseFormattedCompact { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGridViewResponseFormattedOther")]
        private ToolStripMenuItem cmsGridViewResponseFormattedOther { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGridViewResponseFormattedRaw")]
        private ToolStripMenuItem cmsGridViewResponseFormattedRaw { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGridViewResponseFormattedVerbose")]
        private ToolStripMenuItem cmsGridViewResponseFormattedVerbose { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [Serializable, CompilerGenerated]
        internal sealed class _Closure$__
        {
            public static readonly FormEligibility._Closure$__ $I = new FormEligibility._Closure$__();
            public static Action<ITextBatch> $I87-0;
            public static Action<ITextBatch> $I88-0;
            public static Action<ITextBatch> $I89-0;
            public static Action<ITextBatch> $I90-0;

            internal void _Lambda$__87-0(ITextBatch batch)
            {
                Directory.CreateDirectory(Path.GetTempPath());
                string tempFileName = Path.GetTempFileName();
                Edi.Parsing.Report271.SaveDataset(batch, tempFileName);
                string dstFilename = Path.GetTempFileName();
                Edi.Parsing.Report271.SaveReportTemplate(dstFilename);
                ClassGlobalObjects.ShowFileReport(dstFilename, tempFileName, null, false);
            }

            internal void _Lambda$__88-0(ITextBatch batch)
            {
                FormEdiViewer viewer1 = new FormEdiViewer();
                viewer1.MdiParent = ClassGlobalObjects.frmMain;
                viewer1.OpenText(batch.Text);
                viewer1.Show();
            }

            internal void _Lambda$__89-0(ITextBatch batch)
            {
                FormTextViewer viewer1 = new FormTextViewer();
                viewer1.MdiParent = ClassGlobalObjects.frmMain;
                viewer1.OpenText(batch.Text);
                viewer1.Show();
            }

            internal void _Lambda$__90-0(ITextBatch batch)
            {
                Envelope envelope = null;
                Delimiters5010 delimiters;
                if (Edi.Version5.Parser.TryParse271(batch.Text, out envelope, out delimiters))
                {
                    FormTreeViewer viewer1 = new FormTreeViewer();
                    viewer1.MdiParent = ClassGlobalObjects.frmMain;
                    viewer1.OpenTree(envelope);
                    viewer1.Show();
                }
                else
                {
                    Delimiters4010 delimiters2;
                    File271 file = null;
                    if (!Edi.Version4.Parser.TryParse271(batch.Text, out file, out delimiters2))
                    {
                        throw new UserNotifyException("Response cannot be parsed.");
                    }
                    FormTreeViewer viewer2 = new FormTreeViewer();
                    viewer2.MdiParent = ClassGlobalObjects.frmMain;
                    viewer2.OpenTree(file, delimiters2);
                    viewer2.Show();
                }
            }
        }

        [CompilerGenerated]
        internal sealed class _Closure$__91-0
        {
            public HtmlConversionOptions $VB$Local_options;

            internal void _Lambda$__0(ITextBatch batch)
            {
                using (StringWriter writer = new StringWriter())
                {
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.CloseOutput = false;
                    XmlWriter writer2 = XmlWriter.Create(writer, settings);
                    try
                    {
                        Converter.ConvertToHtml(batch.Text, this.$VB$Local_options, writer2);
                    }
                    catch (Exception exception1)
                    {
                        Exception ex = exception1;
                        ProjectData.SetProjectError(ex);
                        Exception innerException = ex;
                        throw new UserNotifyException(innerException.Message, innerException);
                    }
                    finally
                    {
                        if (writer2 != null)
                        {
                            writer2.Dispose();
                        }
                    }
                    FormBrowser browser1 = new FormBrowser(null, writer.ToString());
                    browser1.MdiParent = ClassGlobalObjects.frmMain;
                    browser1.Show();
                }
            }
        }

        private class BatchWrapper : ITextBatch
        {
            public BatchWrapper(int ID, string Text)
            {
                this._ID = ID;
                this._Name = "Request #" + Conversions.ToString(ID);
                this._Text = Text;
            }

            public int ID { get; }

            public string Name { get; }

            public string Text { get; }

            public int Edi.Data.ITextBatch.ID =>
                this._ID;

            public string Edi.Data.ITextBatch.Name =>
                this._Name;

            public string Edi.Data.ITextBatch.Text =>
                this._Text;
        }
    }
}

