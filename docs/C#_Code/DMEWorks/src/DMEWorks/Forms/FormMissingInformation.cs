namespace DMEWorks.Forms
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using Microsoft.VisualBasic.CompilerServices;
    using My.Resources;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormMissingInformation : DmeForm
    {
        private IContainer components;
        private const string CrLf = "\r\n";

        public FormMissingInformation()
        {
            base.KeyDown += new KeyEventHandler(this.FormMissingInformation_KeyDown);
            this.InitializeComponent();
            this.InitializeGridStyle(this.SearchableGrid1.Appearance);
        }

        private void cmnuGrid_Opening(object sender, CancelEventArgs e)
        {
            this.SearchableGrid1.CurrentRow.GetDataRow();
        }

        private void cmsGrid_CustomerInsurance_1_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dataRow = this.SearchableGrid1.CurrentRow.GetDataRow();
                if (dataRow != null)
                {
                    int? nullable = NullableConvert.ToInt32(dataRow["CustomerID"]);
                    if (nullable != null)
                    {
                        int? nullable2 = NullableConvert.ToInt32(dataRow["CustomerInsuranceID_1"]);
                        if (nullable2 != null)
                        {
                            FormParameters @params = new FormParameters {
                                ["ID"] = nullable.Value,
                                ["CustomerInsuranceID"] = nullable2.Value,
                                ["ShowMissing"] = true
                            };
                            ClassGlobalObjects.ShowForm(FormFactories.FormCustomer(), @params);
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

        private void cmsGrid_CustomerInsurance_2_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dataRow = this.SearchableGrid1.CurrentRow.GetDataRow();
                if (dataRow != null)
                {
                    int? nullable = NullableConvert.ToInt32(dataRow["CustomerID"]);
                    if (nullable != null)
                    {
                        int? nullable2 = NullableConvert.ToInt32(dataRow["CustomerInsuranceID_2"]);
                        if (nullable2 != null)
                        {
                            FormParameters @params = new FormParameters {
                                ["ID"] = nullable.Value,
                                ["CustomerInsuranceID"] = nullable2.Value,
                                ["ShowMissing"] = true
                            };
                            ClassGlobalObjects.ShowForm(FormFactories.FormCustomer(), @params);
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

        private void cmsGrid_Doctor_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dataRow = this.SearchableGrid1.CurrentRow.GetDataRow();
                if (dataRow != null)
                {
                    int? nullable = NullableConvert.ToInt32(dataRow["DoctorID"]);
                    if (nullable != null)
                    {
                        FormParameters @params = new FormParameters {
                            ["ID"] = nullable.Value,
                            ["ShowMissing"] = true
                        };
                        ClassGlobalObjects.ShowForm(FormFactories.FormDoctor(), @params);
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

        private void cmsGrid_Facility_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dataRow = this.SearchableGrid1.CurrentRow.GetDataRow();
                if (dataRow != null)
                {
                    int? nullable = NullableConvert.ToInt32(dataRow["FacilityID"]);
                    if (nullable != null)
                    {
                        FormParameters @params = new FormParameters {
                            ["ID"] = nullable.Value,
                            ["ShowMissing"] = true
                        };
                        ClassGlobalObjects.ShowForm(FormFactories.FormFacility(), @params);
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

        private void cmsGrid_InsuranceCompany_1_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dataRow = this.SearchableGrid1.CurrentRow.GetDataRow();
                if (dataRow != null)
                {
                    int? nullable = NullableConvert.ToInt32(dataRow["InsuranceCompanyID_1"]);
                    if (nullable != null)
                    {
                        FormParameters @params = new FormParameters {
                            ["ID"] = nullable.Value,
                            ["ShowMissing"] = true
                        };
                        ClassGlobalObjects.ShowForm(FormFactories.FormInsuranceCompany(), @params);
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

        private void cmsGrid_InsuranceCompany_2_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dataRow = this.SearchableGrid1.CurrentRow.GetDataRow();
                if (dataRow != null)
                {
                    int? nullable = NullableConvert.ToInt32(dataRow["InsuranceCompanyID_2"]);
                    if (nullable != null)
                    {
                        FormParameters @params = new FormParameters {
                            ["ID"] = nullable.Value,
                            ["ShowMissing"] = true
                        };
                        ClassGlobalObjects.ShowForm(FormFactories.FormInsuranceCompany(), @params);
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

        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FormMissingInformation_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.R) & e.Control)
            {
                this.LoadGrid();
            }
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SearchableGrid1 = new SearchableGrid();
            this.cmsGrid = new ContextMenuStrip(this.components);
            this.cmsGrid_OrderDetails = new ToolStripMenuItem();
            this.cmsGrid_Order = new ToolStripMenuItem();
            this.cmsGrid_Customer = new ToolStripMenuItem();
            this.cmsGrid_CMNForm = new ToolStripMenuItem();
            this.cmsGrid_Doctor = new ToolStripMenuItem();
            this.cmsGrid_CustomerInsurance_1 = new ToolStripMenuItem();
            this.cmsGrid_CustomerInsurance_2 = new ToolStripMenuItem();
            this.cmsGrid_InsuranceCompany_1 = new ToolStripMenuItem();
            this.cmsGrid_InsuranceCompany_2 = new ToolStripMenuItem();
            this.cmsGrid_Facility = new ToolStripMenuItem();
            this.ToolStrip1 = new ToolStrip();
            this.tsbRefresh = new ToolStripButton();
            this.tsbPrint = new ToolStripSplitButton();
            this.tsbFilter = new ToolStripDropDownButton();
            this.tsmiFilterApproved_True = new ToolStripMenuItem();
            this.tsmiFilterApproved_False = new ToolStripMenuItem();
            this.tsmiFilterApproved_Any = new ToolStripMenuItem();
            this.cmsGrid.SuspendLayout();
            this.ToolStrip1.SuspendLayout();
            base.SuspendLayout();
            this.SearchableGrid1.Dock = DockStyle.Fill;
            this.SearchableGrid1.Location = new Point(0, 0x19);
            this.SearchableGrid1.Name = "SearchableGrid1";
            this.SearchableGrid1.Size = new Size(680, 0x1ac);
            this.SearchableGrid1.TabIndex = 2;
            ToolStripItem[] toolStripItems = new ToolStripItem[10];
            toolStripItems[0] = this.cmsGrid_OrderDetails;
            toolStripItems[1] = this.cmsGrid_Order;
            toolStripItems[2] = this.cmsGrid_Customer;
            toolStripItems[3] = this.cmsGrid_CMNForm;
            toolStripItems[4] = this.cmsGrid_Doctor;
            toolStripItems[5] = this.cmsGrid_CustomerInsurance_1;
            toolStripItems[6] = this.cmsGrid_CustomerInsurance_2;
            toolStripItems[7] = this.cmsGrid_InsuranceCompany_1;
            toolStripItems[8] = this.cmsGrid_InsuranceCompany_2;
            toolStripItems[9] = this.cmsGrid_Facility;
            this.cmsGrid.Items.AddRange(toolStripItems);
            this.cmsGrid.Name = "cmsGrid";
            this.cmsGrid.Size = new Size(0xd9, 0xe0);
            this.cmsGrid_OrderDetails.Name = "cmsGrid_OrderDetails";
            this.cmsGrid_OrderDetails.Size = new Size(0xd8, 0x16);
            this.cmsGrid_OrderDetails.Text = "Go to Line Item";
            this.cmsGrid_Order.Name = "cmsGrid_Order";
            this.cmsGrid_Order.Size = new Size(0xd8, 0x16);
            this.cmsGrid_Order.Text = "Go to Order";
            this.cmsGrid_Customer.Name = "cmsGrid_Customer";
            this.cmsGrid_Customer.Size = new Size(0xd8, 0x16);
            this.cmsGrid_Customer.Text = "Go to Customer";
            this.cmsGrid_CMNForm.Name = "cmsGrid_CMNForm";
            this.cmsGrid_CMNForm.Size = new Size(0xd8, 0x16);
            this.cmsGrid_CMNForm.Text = "Go to CMN Form";
            this.cmsGrid_Doctor.Name = "cmsGrid_Doctor";
            this.cmsGrid_Doctor.Size = new Size(0xd8, 0x16);
            this.cmsGrid_Doctor.Text = "Go to Doctor";
            this.cmsGrid_CustomerInsurance_1.Name = "cmsGrid_CustomerInsurance_1";
            this.cmsGrid_CustomerInsurance_1.Size = new Size(0xd8, 0x16);
            this.cmsGrid_CustomerInsurance_1.Text = "Go to Policy #1";
            this.cmsGrid_CustomerInsurance_2.Name = "cmsGrid_CustomerInsurance_2";
            this.cmsGrid_CustomerInsurance_2.Size = new Size(0xd8, 0x16);
            this.cmsGrid_CustomerInsurance_2.Text = "Go to Policy #2";
            this.cmsGrid_InsuranceCompany_1.Name = "cmsGrid_InsuranceCompany_1";
            this.cmsGrid_InsuranceCompany_1.Size = new Size(0xd8, 0x16);
            this.cmsGrid_InsuranceCompany_1.Text = "Go to Insurance Company #1";
            this.cmsGrid_InsuranceCompany_2.Name = "cmsGrid_InsuranceCompany_2";
            this.cmsGrid_InsuranceCompany_2.Size = new Size(0xd8, 0x16);
            this.cmsGrid_InsuranceCompany_2.Text = "Go to Insurance Company #2";
            this.cmsGrid_Facility.Name = "cmsGrid_Facility";
            this.cmsGrid_Facility.Size = new Size(0xd8, 0x16);
            this.cmsGrid_Facility.Text = "Go to Facility";
            ToolStripItem[] itemArray2 = new ToolStripItem[] { this.tsbRefresh, this.tsbPrint, this.tsbFilter };
            this.ToolStrip1.Items.AddRange(itemArray2);
            this.ToolStrip1.Location = new Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new Size(680, 0x19);
            this.ToolStrip1.TabIndex = 3;
            this.ToolStrip1.Text = "ToolStrip1";
            this.tsbRefresh.Image = My.Resources.Resources.ImageRefresh2;
            this.tsbRefresh.ImageScaling = ToolStripItemImageScaling.None;
            this.tsbRefresh.ImageTransparentColor = Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new Size(0x41, 0x16);
            this.tsbRefresh.Text = "Refresh";
            this.tsbPrint.Image = My.Resources.Resources.ImagePrint;
            this.tsbPrint.ImageScaling = ToolStripItemImageScaling.None;
            this.tsbPrint.ImageTransparentColor = Color.Magenta;
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Size = new Size(0x3d, 0x16);
            this.tsbPrint.Text = "Print";
            ToolStripItem[] itemArray3 = new ToolStripItem[] { this.tsmiFilterApproved_True, this.tsmiFilterApproved_False, this.tsmiFilterApproved_Any };
            this.tsbFilter.DropDownItems.AddRange(itemArray3);
            this.tsbFilter.Image = My.Resources.Resources.ImageFilter;
            this.tsbFilter.ImageScaling = ToolStripItemImageScaling.None;
            this.tsbFilter.ImageTransparentColor = Color.Magenta;
            this.tsbFilter.Name = "tsbFilter";
            this.tsbFilter.Size = new Size(60, 0x16);
            this.tsbFilter.Text = "Filter";
            this.tsmiFilterApproved_True.Checked = true;
            this.tsmiFilterApproved_True.CheckState = CheckState.Checked;
            this.tsmiFilterApproved_True.Name = "tsmiFilterApproved_True";
            this.tsmiFilterApproved_True.Size = new Size(140, 0x16);
            this.tsmiFilterApproved_True.Text = "Approved";
            this.tsmiFilterApproved_False.Name = "tsmiFilterApproved_False";
            this.tsmiFilterApproved_False.Size = new Size(140, 0x16);
            this.tsmiFilterApproved_False.Text = "Not approved";
            this.tsmiFilterApproved_Any.Name = "tsmiFilterApproved_Any";
            this.tsmiFilterApproved_Any.Size = new Size(140, 0x16);
            this.tsmiFilterApproved_Any.Text = "All Orders";
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(680, 0x1c5);
            base.Controls.Add(this.SearchableGrid1);
            base.Controls.Add(this.ToolStrip1);
            base.Name = "FormMissingInformation";
            this.Text = "Missing Information";
            this.cmsGrid.ResumeLayout(false);
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InitializeGridStyle(SearchableGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("CustomerName", "Customer", 100);
            Appearance.AddTextColumn("OrderID", "Order#", 70, Appearance.IntegerStyle());
            Appearance.AddTextColumn("OrderDetailsID", "Line Item#", 70, Appearance.IntegerStyle());
            Appearance.AddTextColumn("SaleRentType", "Sale/Rent", 80);
            Appearance.AddTextColumn("BillingCode", "Billing Code", 70);
            Appearance.AddTextColumn("InventoryItem", "Inventory Item", 100);
            Appearance.AddTextColumn("PriceCode", "Price Code", 80);
            Appearance.AddTextColumn("Payers", "Payers", 80);
            Appearance.AddTextColumn("MIR", "Summary", 120);
            Appearance.AddTextColumn("Details", "Details", 200).DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Appearance.ContextMenuStrip = this.cmsGrid;
        }

        [HandleDatabaseChanged("tbl_crystalreport")]
        private void InitPrintMenu()
        {
            this.tsbPrint.DropDownItems.Clear();
            Cache.AddCategory(this.tsbPrint.DropDownItems, "MIR", new EventHandler(this.tsbPrintItem_Click));
        }

        private void LoadGrid()
        {
            try
            {
                StringBuilder builder = new StringBuilder(0x100);
                builder.AppendLine("SELECT *");
                builder.AppendLine("FROM view_mir");
                builder.AppendLine("WHERE (1 = 1)");
                if (IsDemoVersion)
                {
                    builder.AppendLine("  AND (CustomerID BETWEEN 1 and 50)");
                }
                if (this.tsmiFilterApproved_True.Checked)
                {
                    builder.AppendLine("  AND (OrderApproved = 1)");
                }
                else if (this.tsmiFilterApproved_False.Checked)
                {
                    builder.AppendLine("  AND (OrderApproved = 0)");
                }
                builder.AppendLine("ORDER BY CustomerName, CustomerID");
                DataTable dataTable = new DataTable("tbl_missinginformation");
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("CALL mir_update()", connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(builder.ToString(), connection))
                    {
                        adapter.AcceptChangesDuringFill = true;
                        adapter.Fill(dataTable);
                    }
                }
                this.SearchableGrid1.GridSource = dataTable.ToGridSource();
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

        private void mnuGrid_CMNForm_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dataRow = this.SearchableGrid1.CurrentRow.GetDataRow();
                if (dataRow != null)
                {
                    int? nullable = NullableConvert.ToInt32(dataRow["CMNFormID"]);
                    if (nullable != null)
                    {
                        FormParameters @params = new FormParameters {
                            ["ID"] = nullable.Value,
                            ["ShowMissing"] = true
                        };
                        ClassGlobalObjects.ShowForm(FormFactories.FormCMNRX(), @params);
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

        private void mnuGrid_Customer_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dataRow = this.SearchableGrid1.CurrentRow.GetDataRow();
                if (dataRow != null)
                {
                    int? nullable = NullableConvert.ToInt32(dataRow["CustomerID"]);
                    if (nullable != null)
                    {
                        FormParameters @params = new FormParameters {
                            ["ID"] = nullable.Value,
                            ["ShowMissing"] = true
                        };
                        ClassGlobalObjects.ShowForm(FormFactories.FormCustomer(), @params);
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

        private void mnuGrid_Order_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dataRow = this.SearchableGrid1.CurrentRow.GetDataRow();
                if (dataRow != null)
                {
                    int? nullable = NullableConvert.ToInt32(dataRow["OrderID"]);
                    if (nullable != null)
                    {
                        FormParameters @params = new FormParameters {
                            ["ID"] = nullable.Value,
                            ["ShowMissing"] = true
                        };
                        ClassGlobalObjects.ShowForm(FormFactories.FormOrder(), @params);
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

        private void mnuGrid_OrderDetails_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dataRow = this.SearchableGrid1.CurrentRow.GetDataRow();
                if (dataRow != null)
                {
                    int? nullable = NullableConvert.ToInt32(dataRow["OrderID"]);
                    if (nullable != null)
                    {
                        int? nullable2 = NullableConvert.ToInt32(dataRow["OrderDetailsID"]);
                        if (nullable2 != null)
                        {
                            FormParameters @params = new FormParameters {
                                ["ID"] = nullable.Value,
                                ["OrderDetailsID"] = nullable2.Value,
                                ["ShowMissing"] = true
                            };
                            ClassGlobalObjects.ShowForm(FormFactories.FormOrder(), @params);
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.SafeInvoke(new Action(this.InitPrintMenu));
            this.SafeInvoke(new Action(this.LoadGrid));
        }

        private void tsbPrintItem_Click(object sender, EventArgs e)
        {
            try
            {
                ReportToolStripMenuItem item = sender as ReportToolStripMenuItem;
                if (item != null)
                {
                    ClassGlobalObjects.ShowReport(item.ReportFileName);
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

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            this.LoadGrid();
        }

        private void tsmiFilterApproved_Any_Click(object sender, EventArgs e)
        {
            this.tsmiFilterApproved_True.Checked = false;
            this.tsmiFilterApproved_False.Checked = false;
            this.tsmiFilterApproved_Any.Checked = true;
            this.LoadGrid();
        }

        private void tsmiFilterApproved_Click(object sender, EventArgs e)
        {
            this.tsmiFilterApproved_True.Checked = true;
            this.tsmiFilterApproved_False.Checked = false;
            this.tsmiFilterApproved_Any.Checked = false;
            this.LoadGrid();
        }

        private void tsmiFilterApproved_False_Click(object sender, EventArgs e)
        {
            this.tsmiFilterApproved_True.Checked = false;
            this.tsmiFilterApproved_False.Checked = true;
            this.tsmiFilterApproved_Any.Checked = false;
            this.LoadGrid();
        }

        [field: AccessedThroughProperty("SearchableGrid1")]
        private SearchableGrid SearchableGrid1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGrid")]
        private ContextMenuStrip cmsGrid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGrid_Order")]
        private ToolStripMenuItem cmsGrid_Order { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGrid_Customer")]
        private ToolStripMenuItem cmsGrid_Customer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGrid_CMNForm")]
        private ToolStripMenuItem cmsGrid_CMNForm { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGrid_OrderDetails")]
        private ToolStripMenuItem cmsGrid_OrderDetails { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ToolStrip1")]
        private ToolStrip ToolStrip1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbRefresh")]
        private ToolStripButton tsbRefresh { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbPrint")]
        private ToolStripSplitButton tsbPrint { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGrid_Doctor")]
        private ToolStripMenuItem cmsGrid_Doctor { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGrid_CustomerInsurance_1")]
        private ToolStripMenuItem cmsGrid_CustomerInsurance_1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGrid_CustomerInsurance_2")]
        private ToolStripMenuItem cmsGrid_CustomerInsurance_2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGrid_InsuranceCompany_1")]
        private ToolStripMenuItem cmsGrid_InsuranceCompany_1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGrid_InsuranceCompany_2")]
        private ToolStripMenuItem cmsGrid_InsuranceCompany_2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGrid_Facility")]
        private ToolStripMenuItem cmsGrid_Facility { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbFilter")]
        private ToolStripDropDownButton tsbFilter { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiFilterApproved_True")]
        private ToolStripMenuItem tsmiFilterApproved_True { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiFilterApproved_False")]
        private ToolStripMenuItem tsmiFilterApproved_False { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiFilterApproved_Any")]
        private ToolStripMenuItem tsmiFilterApproved_Any { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        private static bool IsDemoVersion =>
            Globals.SerialNumber.IsDemoSerial();
    }
}

