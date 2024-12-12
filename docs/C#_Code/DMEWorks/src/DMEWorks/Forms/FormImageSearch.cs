namespace DMEWorks.Forms
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using DMEWorks.Maintain;
    using Microsoft.VisualBasic.CompilerServices;
    using My.Resources;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormImageSearch : DmeForm, IParameters
    {
        private IContainer components;
        private DataTable F_Order = null;
        private DataTable F_Invoice = null;
        private DataTable F_CmnForm = null;
        private static Uri blank = new Uri("about:blank");

        public FormImageSearch()
        {
            this.InitializeComponent();
        }

        private void btnCMNForm_Click(object sender, EventArgs e)
        {
            using (FormSelector selector = new FormSelector())
            {
                selector.DataSource = this.Table_CmnForm.ToGridSource();
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    DataRow dataRow = selector.SelectedRow.GetDataRow();
                    if (dataRow != null)
                    {
                        Functions.SetTextBoxText(this.txtCMNForm, dataRow["ID"]);
                    }
                }
            }
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            using (FormSelector selector = new FormSelector())
            {
                selector.DataSource = this.Table_Invoice.ToGridSource();
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    DataRow dataRow = selector.SelectedRow.GetDataRow();
                    if (dataRow != null)
                    {
                        Functions.SetTextBoxText(this.txtInvoice, dataRow["ID"]);
                    }
                }
            }
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            using (FormSelector selector = new FormSelector())
            {
                selector.DataSource = this.Table_Order.ToGridSource();
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    DataRow dataRow = selector.SelectedRow.GetDataRow();
                    if (dataRow != null)
                    {
                        Functions.SetTextBoxText(this.txtOrder, dataRow["ID"]);
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.WebBrowser1.DocumentText = this.BuildHtml();
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.ShowException(exception, "Search");
                ProjectData.ClearProjectError();
            }
        }

        private string BuildHtml()
        {
            int? nullable = NullableConvert.ToInt32(this.cmbCustomer.SelectedValue);
            int? nullable2 = NullableConvert.ToInt32(this.cmbDoctor.SelectedValue);
            int? nullable3 = NullableConvert.ToInt32(this.txtCMNForm.Text);
            int? nullable4 = NullableConvert.ToInt32(this.txtOrder.Text);
            int? nullable5 = NullableConvert.ToInt32(this.txtInvoice.Text);
            string commandText = "SELECT\r\n   tbl_image.ID\r\n  ,tbl_image.Name\r\n  ,tbl_image.Description\r\n  ,tbl_image.OrderID\r\n  ,tbl_image.InvoiceID\r\n  ,tbl_cmnform.ID as CmnFormID\r\n  ,tbl_cmnform.CMNType as CmnType\r\n  ,tbl_customer.ID as CustomerID\r\n  ,CONCAT(tbl_customer.LastName, ', ', tbl_customer.FirstName) as CustomerName\r\n  ,tbl_doctor.ID as DoctorID\r\n  ,CONCAT(tbl_doctor.LastName, ', ', tbl_doctor.FirstName) as DoctorName\r\nFROM tbl_image\r\n     LEFT JOIN tbl_customer ON tbl_image.CustomerID = tbl_customer.ID\r\n     LEFT JOIN tbl_doctor   ON tbl_image.DoctorID   = tbl_doctor  .ID\r\n     LEFT JOIN tbl_cmnform  ON tbl_image.CMNFormID  = tbl_cmnform .ID\r\nWHERE (1 = 1)\r\n";
            if (nullable != null)
            {
                commandText = commandText + $"  AND (tbl_image.CustomerID = {nullable.Value})" + "\r\n";
            }
            if (nullable2 != null)
            {
                commandText = commandText + $"  AND (tbl_image.DoctorID = {nullable2.Value})" + "\r\n";
            }
            if (nullable3 != null)
            {
                commandText = commandText + $"  AND (tbl_image.CMNFormID = {nullable3.Value})" + "\r\n";
            }
            if (nullable4 != null)
            {
                commandText = commandText + $"  AND (tbl_image.OrderID = {nullable4.Value})" + "\r\n";
            }
            if (nullable5 != null)
            {
                commandText = commandText + $"  AND (tbl_image.InvoiceID = {nullable5.Value})" + "\r\n";
            }
            commandText = commandText + "ORDER BY tbl_image.ID DESC";
            string relativeUri = "Thumbnail.aspx?company=" + Globals.CompanyDatabase + "&index=";
            relativeUri = new Uri(Globals.CompanyImagingUri, relativeUri).ToString();
            using (StringWriter writer = new StringWriter())
            {
                writer.NewLine = Environment.NewLine;
                writer.WriteLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
                writer.WriteLine("<html xmlns='http://www.w3.org/1999/xhtml' >");
                writer.WriteLine("<head><title>DMEWorks! Images search</title></head>");
                writer.WriteLine("<body>");
                writer.WriteLine("<table cellspacing='2' cellpadding='2' border='0'>");
                writer.WriteLine("<col width='40px'>");
                writer.WriteLine("<col width='130px'>");
                writer.WriteLine("<col>");
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(commandText, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                writer.WriteLine("<tr>");
                                string[] textArray1 = new string[] { "<td><a href='dme:FormImage?ID=", Convert.ToString(reader["ID"]), "'>", Convert.ToString(reader["ID"]), "</a></td>" };
                                writer.WriteLine(string.Concat(textArray1));
                                writer.WriteLine("<td><img src='" + relativeUri + Convert.ToString(reader["ID"]) + "'></td>");
                                writer.WriteLine("<td>");
                                string str4 = Convert.ToString(reader["Name"]);
                                if (!string.IsNullOrEmpty(str4))
                                {
                                    writer.WriteLine(str4 + "<br/>");
                                }
                                string str5 = Convert.ToString(reader["Description"]);
                                if (!string.IsNullOrEmpty(str5))
                                {
                                    writer.WriteLine("<pre>" + str5 + "</pre><br/>");
                                }
                                StringBuilder builder = new StringBuilder();
                                if (!reader.IsDBNull(reader.GetOrdinal("OrderID")))
                                {
                                    builder.AppendFormat("<a href='dme:FormOrder?ID={0}'>Order #{0}<a>", reader["OrderID"]);
                                }
                                if (!reader.IsDBNull(reader.GetOrdinal("InvoiceID")))
                                {
                                    if (0 < builder.Length)
                                    {
                                        builder.Append(",&nbsp;");
                                    }
                                    builder.AppendFormat("<a href='dme:FormInvoice?ID={0}'>Invoice #{0}<a>", reader["InvoiceID"]);
                                }
                                if (!reader.IsDBNull(reader.GetOrdinal("CMNFormID")))
                                {
                                    if (0 < builder.Length)
                                    {
                                        builder.Append(",&nbsp;");
                                    }
                                    builder.AppendFormat("<a href='dme:FormCMNRX?ID={0}'>CMNRX #{0}<a>", reader["CMNFormID"]);
                                }
                                if ((0 < builder.Length) || (!reader.IsDBNull(reader.GetOrdinal("CustomerID")) || !reader.IsDBNull(reader.GetOrdinal("DoctorID"))))
                                {
                                    writer.WriteLine("References:<br/>");
                                    if (!reader.IsDBNull(reader.GetOrdinal("CustomerID")))
                                    {
                                        writer.WriteLine("<a href='dme:FormCustomer?ID={0}'>Customer #{0}, {1}<a><br/>", reader["CustomerID"], reader["CustomerName"]);
                                    }
                                    if (!reader.IsDBNull(reader.GetOrdinal("DoctorID")))
                                    {
                                        writer.WriteLine("<a href='dme:FormDoctor?ID={0}'>Doctor #{0}, {1}<a><br/>", reader["DoctorID"], reader["DoctorName"]);
                                    }
                                    if (0 < builder.Length)
                                    {
                                        writer.WriteLine(builder.ToString() + "<br/>");
                                    }
                                }
                                writer.WriteLine("</td>");
                                writer.WriteLine("</tr>");
                            }
                        }
                    }
                }
                writer.WriteLine("</table>");
                writer.WriteLine("</body>");
                writer.WriteLine("</html>");
                return writer.ToString();
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

        protected override void InitDropdowns()
        {
            Cache.InitDropdown(this.cmbCustomer, "tbl_customer", null);
            Cache.InitDropdown(this.cmbDoctor, "tbl_doctor", null);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormImageSearch));
            this.gbRelations = new GroupBox();
            this.btnSearch = new Button();
            this.btnCMNForm = new Button();
            this.btnInvoice = new Button();
            this.btnOrder = new Button();
            this.txtCMNForm = new TextBox();
            this.txtInvoice = new TextBox();
            this.txtOrder = new TextBox();
            this.lblInvoice = new Label();
            this.lblOrder = new Label();
            this.cmbDoctor = new Combobox();
            this.lblDoctor = new Label();
            this.cmbCustomer = new Combobox();
            this.lblCustomer = new Label();
            this.lblCMNForm = new Label();
            this.WebBrowser1 = new WebBrowser();
            this.gbRelations.SuspendLayout();
            base.SuspendLayout();
            this.gbRelations.Controls.Add(this.btnSearch);
            this.gbRelations.Controls.Add(this.btnCMNForm);
            this.gbRelations.Controls.Add(this.btnInvoice);
            this.gbRelations.Controls.Add(this.btnOrder);
            this.gbRelations.Controls.Add(this.txtCMNForm);
            this.gbRelations.Controls.Add(this.txtInvoice);
            this.gbRelations.Controls.Add(this.txtOrder);
            this.gbRelations.Controls.Add(this.lblInvoice);
            this.gbRelations.Controls.Add(this.lblOrder);
            this.gbRelations.Controls.Add(this.cmbDoctor);
            this.gbRelations.Controls.Add(this.lblDoctor);
            this.gbRelations.Controls.Add(this.cmbCustomer);
            this.gbRelations.Controls.Add(this.lblCustomer);
            this.gbRelations.Controls.Add(this.lblCMNForm);
            this.gbRelations.Dock = DockStyle.Top;
            this.gbRelations.Location = new Point(0, 0);
            this.gbRelations.Name = "gbRelations";
            this.gbRelations.Size = new Size(0x250, 0x60);
            this.gbRelations.TabIndex = 0;
            this.gbRelations.TabStop = false;
            this.gbRelations.Text = "Relations";
            this.btnSearch.FlatStyle = FlatStyle.Flat;
            this.btnSearch.Location = new Point(490, 0x10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new Size(0x48, 0x48);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "Search";
            this.btnCMNForm.FlatStyle = FlatStyle.Flat;
            this.btnCMNForm.Image = (Image) manager.GetObject("btnCMNForm.Image");
            this.btnCMNForm.Location = new Point(0x1c0, 0x40);
            this.btnCMNForm.Name = "btnCMNForm";
            this.btnCMNForm.Size = new Size(0x15, 0x15);
            this.btnCMNForm.TabIndex = 12;
            this.btnInvoice.FlatStyle = FlatStyle.Flat;
            this.btnInvoice.Image = (Image) manager.GetObject("btnInvoice.Image");
            this.btnInvoice.Location = new Point(0x1c0, 40);
            this.btnInvoice.Margin = new Padding(0);
            this.btnInvoice.Name = "btnInvoice";
            this.btnInvoice.Size = new Size(0x15, 0x15);
            this.btnInvoice.TabIndex = 9;
            this.btnOrder.FlatStyle = FlatStyle.Flat;
            this.btnOrder.Image = My.Resources.Resources.ImageSpyglass;
            this.btnOrder.Location = new Point(0x1c0, 0x10);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new Size(0x15, 0x15);
            this.btnOrder.TabIndex = 6;
            this.txtCMNForm.Location = new Point(360, 0x40);
            this.txtCMNForm.Name = "txtCMNForm";
            this.txtCMNForm.Size = new Size(80, 20);
            this.txtCMNForm.TabIndex = 11;
            this.txtInvoice.Location = new Point(360, 40);
            this.txtInvoice.Name = "txtInvoice";
            this.txtInvoice.Size = new Size(80, 20);
            this.txtInvoice.TabIndex = 8;
            this.txtOrder.Location = new Point(360, 0x10);
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.Size = new Size(80, 20);
            this.txtOrder.TabIndex = 5;
            this.lblInvoice.Location = new Point(0x128, 40);
            this.lblInvoice.Name = "lblInvoice";
            this.lblInvoice.Size = new Size(0x38, 20);
            this.lblInvoice.TabIndex = 7;
            this.lblInvoice.Text = "Invoice #";
            this.lblInvoice.TextAlign = ContentAlignment.MiddleRight;
            this.lblOrder.Location = new Point(0x128, 0x10);
            this.lblOrder.Name = "lblOrder";
            this.lblOrder.Size = new Size(0x38, 20);
            this.lblOrder.TabIndex = 4;
            this.lblOrder.Text = "Order #";
            this.lblOrder.TextAlign = ContentAlignment.MiddleRight;
            this.cmbDoctor.Location = new Point(0x48, 40);
            this.cmbDoctor.Name = "cmbDoctor";
            this.cmbDoctor.Size = new Size(0xd8, 0x15);
            this.cmbDoctor.TabIndex = 3;
            this.lblDoctor.Location = new Point(8, 40);
            this.lblDoctor.Name = "lblDoctor";
            this.lblDoctor.Size = new Size(0x38, 20);
            this.lblDoctor.TabIndex = 2;
            this.lblDoctor.Text = "Doctor";
            this.lblDoctor.TextAlign = ContentAlignment.MiddleRight;
            this.cmbCustomer.Location = new Point(0x48, 0x10);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new Size(0xd8, 0x15);
            this.cmbCustomer.TabIndex = 1;
            this.lblCustomer.Location = new Point(8, 0x10);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new Size(0x38, 20);
            this.lblCustomer.TabIndex = 0;
            this.lblCustomer.Text = "Customer";
            this.lblCustomer.TextAlign = ContentAlignment.MiddleRight;
            this.lblCMNForm.Location = new Point(0x128, 0x40);
            this.lblCMNForm.Name = "lblCMNForm";
            this.lblCMNForm.Size = new Size(0x38, 20);
            this.lblCMNForm.TabIndex = 10;
            this.lblCMNForm.Text = "CMN #";
            this.lblCMNForm.TextAlign = ContentAlignment.MiddleRight;
            this.WebBrowser1.Dock = DockStyle.Fill;
            this.WebBrowser1.Location = new Point(0, 0x60);
            this.WebBrowser1.MinimumSize = new Size(20, 20);
            this.WebBrowser1.Name = "WebBrowser1";
            this.WebBrowser1.Size = new Size(0x250, 0x147);
            this.WebBrowser1.TabIndex = 2;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x250, 0x1a7);
            base.Controls.Add(this.WebBrowser1);
            base.Controls.Add(this.gbRelations);
            base.Name = "FormImageSearch";
            base.ShowInTaskbar = false;
            this.Text = "Search for images";
            this.gbRelations.ResumeLayout(false);
            this.gbRelations.PerformLayout();
            base.ResumeLayout(false);
        }

        [HandleDatabaseChanged("tbl_cmnform")]
        private void Invalidate_Table_cmnform()
        {
            this.Table_CmnForm = null;
        }

        [HandleDatabaseChanged("tbl_invoice")]
        private void Invalidate_Table_invoice()
        {
            this.Table_Invoice = null;
        }

        [HandleDatabaseChanged("tbl_order")]
        private void Invalidate_Table_order()
        {
            this.Table_Order = null;
        }

        public void SetParameters(FormParameters Params)
        {
            if (Params != null)
            {
                Functions.SetTextBoxText(this.txtInvoice, NullableConvert.ToDb(NullableConvert.ToInt32(Params["InvoiceID"])));
                Functions.SetTextBoxText(this.txtOrder, NullableConvert.ToDb(NullableConvert.ToInt32(Params["OrderID"])));
                Functions.SetTextBoxText(this.txtCMNForm, NullableConvert.ToDb(NullableConvert.ToInt32(Params["CMNFormID"])));
                Functions.SetComboBoxValue(this.cmbCustomer, NullableConvert.ToDb(NullableConvert.ToInt32(Params["CustomerID"])));
                Functions.SetComboBoxValue(this.cmbDoctor, NullableConvert.ToDb(NullableConvert.ToInt32(Params["DoctorID"])));
            }
        }

        private void WebBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (Uri.Compare(e.Url, blank, UriComponents.AbsoluteUri, UriFormat.UriEscaped, StringComparison.InvariantCulture) != 0)
            {
                e.Cancel = true;
                if (string.Compare(e.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped), "dme:", true) == 0)
                {
                    FormFactory factory;
                    string components = e.Url.GetComponents(UriComponents.Query, UriFormat.Unescaped);
                    string a = e.Url.GetComponents(UriComponents.Path, UriFormat.Unescaped);
                    if (!string.Equals(a, "FormImage", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!string.Equals(a, "FormOrder", StringComparison.OrdinalIgnoreCase))
                        {
                            if (!string.Equals(a, "FormInvoice", StringComparison.OrdinalIgnoreCase))
                            {
                                if (!string.Equals(a, "FormCMNRX", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (!string.Equals(a, "FormCustomer", StringComparison.OrdinalIgnoreCase))
                                    {
                                        if (!string.Equals(a, "FormDoctor", StringComparison.OrdinalIgnoreCase))
                                        {
                                            return;
                                        }
                                        else
                                        {
                                            factory = FormFactories.FormDoctor();
                                        }
                                    }
                                    else
                                    {
                                        factory = FormFactories.FormCustomer();
                                    }
                                }
                                else
                                {
                                    factory = FormFactories.FormCMNRX();
                                }
                            }
                            else
                            {
                                factory = FormFactories.FormInvoice();
                            }
                        }
                        else
                        {
                            factory = FormFactories.FormOrder();
                        }
                    }
                    else
                    {
                        factory = FormFactories.FormImage();
                    }
                    ClassGlobalObjects.ShowForm(factory, new FormParameters(components));
                }
            }
        }

        [field: AccessedThroughProperty("gbRelations")]
        private GroupBox gbRelations { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnCMNForm")]
        private Button btnCMNForm { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnInvoice")]
        private Button btnInvoice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnOrder")]
        private Button btnOrder { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCMNForm")]
        private TextBox txtCMNForm { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtInvoice")]
        private TextBox txtInvoice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtOrder")]
        private TextBox txtOrder { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInvoice")]
        private Label lblInvoice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblOrder")]
        private Label lblOrder { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbDoctor")]
        private Combobox cmbDoctor { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDoctor")]
        private Label lblDoctor { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbCustomer")]
        private Combobox cmbCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCustomer")]
        private Label lblCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCMNForm")]
        private Label lblCMNForm { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnSearch")]
        private Button btnSearch { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("WebBrowser1")]
        private WebBrowser WebBrowser1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        private DataTable Table_Order
        {
            get
            {
                if (this.F_Order == null)
                {
                    this.F_Order = new DataTable("tbl_order");
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(Conversions.ToString(FormOrder.GetQuery()[0]), ClassGlobalObjects.ConnectionString_MySql))
                    {
                        adapter.AcceptChangesDuringFill = true;
                        adapter.Fill(this.F_Order);
                    }
                }
                return this.F_Order;
            }
            set => 
                this.F_Order = null;
        }

        private DataTable Table_Invoice
        {
            get
            {
                if (this.F_Invoice == null)
                {
                    this.F_Invoice = new DataTable("tbl_invoice");
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(Conversions.ToString(FormInvoice.GetQuery()[0]), ClassGlobalObjects.ConnectionString_MySql))
                    {
                        adapter.AcceptChangesDuringFill = true;
                        adapter.Fill(this.F_Invoice);
                    }
                }
                return this.F_Invoice;
            }
            set => 
                this.F_Invoice = null;
        }

        private DataTable Table_CmnForm
        {
            get
            {
                if (this.F_CmnForm == null)
                {
                    this.F_CmnForm = new DataTable("tbl_cmnform");
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(FormCMNRX.GetQuery(), ClassGlobalObjects.ConnectionString_MySql))
                    {
                        adapter.AcceptChangesDuringFill = true;
                        adapter.Fill(this.F_CmnForm);
                    }
                }
                return this.F_CmnForm;
            }
            set => 
                this.F_CmnForm = null;
        }
    }
}

