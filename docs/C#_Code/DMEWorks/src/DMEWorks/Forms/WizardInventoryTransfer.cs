namespace DMEWorks.Forms
{
    using ActiproSoftware.Wizard;
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using DMEWorks.Data.MySql;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class WizardInventoryTransfer : DmeForm, IParameters
    {
        private IContainer components;
        private const string CrLf = "\r\n";

        public WizardInventoryTransfer()
        {
            this.InitializeComponent();
            this.InitializeGrid(this.gridSerials.Appearance);
        }

        private void AttachTable(TableSerials table)
        {
            TableSerials tableSource = this.gridSerials.GetTableSource<TableSerials>();
            if (!ReferenceEquals(tableSource, table))
            {
                if (tableSource != null)
                {
                    tableSource.RowChanged -= new DataRowChangeEventHandler(this.Table_RowChanged);
                }
                this.gridSerials.GridSource = table.ToGridSource();
                if (table != null)
                {
                    table.RowChanged += new DataRowChangeEventHandler(this.Table_RowChanged);
                }
            }
        }

        private void cmbDstWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? srcWarehouseID = this.GetSrcWarehouseID();
            int? dstWarehouseID = this.GetDstWarehouseID();
            if (((dstWarehouseID != null) & (srcWarehouseID != null)) && (dstWarehouseID.Value != srcWarehouseID.Value))
            {
                this.wpDstWarehouse.NextButtonEnabled = WizardButtonEnabledDefault.Auto;
            }
            else
            {
                this.wpDstWarehouse.NextButtonEnabled = WizardButtonEnabledDefault.False;
            }
        }

        private void cmbInventoryItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.GetInventoryItemID() != null)
            {
                this.wpInventoryItem.NextButtonEnabled = WizardButtonEnabledDefault.Auto;
            }
            else
            {
                this.wpInventoryItem.NextButtonEnabled = WizardButtonEnabledDefault.False;
            }
        }

        private void cmbSrcWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.GetSrcWarehouseID() != null)
            {
                this.wpSrcWarehouse.NextButtonEnabled = WizardButtonEnabledDefault.Auto;
            }
            else
            {
                this.wpSrcWarehouse.NextButtonEnabled = WizardButtonEnabledDefault.False;
            }
        }

        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (this.components != null))
                {
                    this.components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        private int? GetDstWarehouseID() => 
            NullableConvert.ToInt32(this.cmbDstWarehouse.SelectedValue);

        private string GetDstWarehouseName()
        {
            DataRow selectedRow = this.cmbDstWarehouse.SelectedRow;
            return ((selectedRow != null) ? Convert.ToString(selectedRow[this.cmbDstWarehouse.DisplayMember]) : "");
        }

        private int? GetInventoryItemID() => 
            NullableConvert.ToInt32(this.cmbInventoryItem.SelectedValue);

        private string GetInventoryItemName()
        {
            DataRow selectedRow = this.cmbInventoryItem.SelectedRow;
            return ((selectedRow != null) ? Convert.ToString(selectedRow[this.cmbInventoryItem.DisplayMember]) : "");
        }

        private bool GetInventoryItemSerialized()
        {
            DataRow selectedRow = this.cmbInventoryItem.SelectedRow;
            return ((selectedRow != null) ? SqlString.Equals(Convert.ToString(selectedRow["Serialized"]), "Y") : false);
        }

        private int GetNonserializedQuantity() => 
            Convert.ToInt32(this.nudQuantity.Value);

        private int? GetSrcWarehouseID() => 
            NullableConvert.ToInt32(this.cmbSrcWarehouse.SelectedValue);

        private string GetSrcWarehouseName()
        {
            DataRow selectedRow = this.cmbSrcWarehouse.SelectedRow;
            return ((selectedRow != null) ? Convert.ToString(selectedRow[this.cmbSrcWarehouse.DisplayMember]) : "");
        }

        protected override void InitDropdowns()
        {
            Cache.InitDropdown(this.cmbInventoryItem, "tbl_inventoryitem", null);
            SetComboBoxValue(this.cmbInventoryItem);
            Cache.InitDropdown(this.cmbSrcWarehouse, "tbl_warehouse", null);
            SetComboBoxValue(this.cmbSrcWarehouse);
            Cache.InitDropdown(this.cmbDstWarehouse, "tbl_warehouse", null);
            SetComboBoxValue(this.cmbDstWarehouse);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            WindowsClassicWizardRenderer renderer = new WindowsClassicWizardRenderer();
            this.wizard = new ActiproSoftware.Wizard.Wizard();
            this.wpWelcome = new WizardWelcomePage();
            this.Label3 = new Label();
            this.Label2 = new Label();
            this.Label1 = new Label();
            this.wpInventoryItem = new WizardPage();
            this.cmbInventoryItem = new Combobox();
            this.lblInventoryItem = new Label();
            this.wpSrcWarehouse = new WizardPage();
            this.cmbSrcWarehouse = new Combobox();
            this.lblSrcWarehouse = new Label();
            this.wpDstWarehouse = new WizardPage();
            this.cmbDstWarehouse = new Combobox();
            this.lblDstWarehouse = new Label();
            this.wpSerialized = new WizardPage();
            this.gridSerials = new FilteredGrid();
            this.lblSerials = new Label();
            this.wpNonSerialized = new WizardPage();
            this.nudQuantity = new NumericUpDown();
            this.lblQuantity = new Label();
            this.wpSummary = new WizardPage();
            this.txtSummary = new TextBox();
            this.wpReport = new WizardPage();
            this.txtReport = new TextBox();
            ((ISupportInitialize) this.wizard).BeginInit();
            this.wpWelcome.SuspendLayout();
            this.wpInventoryItem.SuspendLayout();
            this.wpSrcWarehouse.SuspendLayout();
            this.wpDstWarehouse.SuspendLayout();
            this.wpSerialized.SuspendLayout();
            this.wpNonSerialized.SuspendLayout();
            this.nudQuantity.BeginInit();
            this.wpSummary.SuspendLayout();
            this.wpReport.SuspendLayout();
            base.SuspendLayout();
            this.wizard.Dock = DockStyle.Fill;
            this.wizard.Location = new Point(0, 0);
            this.wizard.Name = "wizard";
            WizardPage[] pages = new WizardPage[] { this.wpWelcome, this.wpInventoryItem, this.wpSrcWarehouse, this.wpDstWarehouse, this.wpSerialized, this.wpNonSerialized, this.wpSummary, this.wpReport };
            this.wizard.Pages.AddRange(pages);
            this.wizard.Renderer = renderer;
            this.wizard.Size = new Size(0x1fd, 0x169);
            this.wizard.TabIndex = 0;
            this.wpWelcome.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wpWelcome.BackColor = SystemColors.Window;
            this.wpWelcome.Controls.Add(this.Label3);
            this.wpWelcome.Controls.Add(this.Label2);
            this.wpWelcome.Controls.Add(this.Label1);
            this.wpWelcome.HelpButtonVisible = false;
            this.wpWelcome.IsInteriorPage = false;
            this.wpWelcome.Location = new Point(0, 0);
            this.wpWelcome.Name = "wpWelcome";
            this.wpWelcome.PageCaption = "";
            this.wpWelcome.PageDescription = "";
            this.wpWelcome.Size = new Size(0x1fd, 0x142);
            this.wpWelcome.TabIndex = 2;
            this.Label3.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.Label3.AutoSize = true;
            this.Label3.Location = new Point(0xb2, 0x126);
            this.Label3.Name = "Label3";
            this.Label3.Size = new Size(120, 13);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "To continue, click Next.";
            this.Label2.Location = new Point(0xb2, 0x45);
            this.Label2.Name = "Label2";
            this.Label2.Size = new Size(0x13c, 0xf4);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "Enter a brief description of the wizard here.";
            this.Label1.Font = new Font("Verdana", 12f, FontStyle.Bold);
            this.Label1.Location = new Point(0xb0, 13);
            this.Label1.Name = "Label1";
            this.Label1.Size = new Size(0x135, 0x37);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Welcome to the Inventory Transfer Wizard";
            this.wpInventoryItem.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wpInventoryItem.Controls.Add(this.cmbInventoryItem);
            this.wpInventoryItem.Controls.Add(this.lblInventoryItem);
            this.wpInventoryItem.HelpButtonVisible = false;
            this.wpInventoryItem.Location = new Point(0x10, 0x4c);
            this.wpInventoryItem.Name = "wpInventoryItem";
            this.wpInventoryItem.PageCaption = "Select inventory item";
            this.wpInventoryItem.PageDescription = "Select inventory that you would like to transfer";
            this.wpInventoryItem.PageTitleBarText = "Select inventory item";
            this.wpInventoryItem.Size = new Size(0x2ae, 0x123);
            this.wpInventoryItem.TabIndex = 0;
            this.cmbInventoryItem.EditButton = false;
            this.cmbInventoryItem.Location = new Point(0x10, 0x20);
            this.cmbInventoryItem.Name = "cmbInventoryItem";
            this.cmbInventoryItem.NewButton = false;
            this.cmbInventoryItem.Size = new Size(0x158, 0x15);
            this.cmbInventoryItem.TabIndex = 1;
            this.lblInventoryItem.Location = new Point(0, 0);
            this.lblInventoryItem.Name = "lblInventoryItem";
            this.lblInventoryItem.Size = new Size(100, 0x15);
            this.lblInventoryItem.TabIndex = 0;
            this.lblInventoryItem.Text = "Inventory Item :";
            this.wpSrcWarehouse.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wpSrcWarehouse.Controls.Add(this.cmbSrcWarehouse);
            this.wpSrcWarehouse.Controls.Add(this.lblSrcWarehouse);
            this.wpSrcWarehouse.HelpButtonVisible = false;
            this.wpSrcWarehouse.Location = new Point(0x10, 0x4c);
            this.wpSrcWarehouse.Name = "wpSrcWarehouse";
            this.wpSrcWarehouse.PageCaption = "Select source warehouse";
            this.wpSrcWarehouse.PageDescription = "Select warehouse from which you would like to transfer inventory";
            this.wpSrcWarehouse.PageTitleBarText = "Select source warehouse";
            this.wpSrcWarehouse.Size = new Size(0x2ae, 0x123);
            this.wpSrcWarehouse.TabIndex = 5;
            this.cmbSrcWarehouse.EditButton = false;
            this.cmbSrcWarehouse.Location = new Point(0x10, 0x20);
            this.cmbSrcWarehouse.Name = "cmbSrcWarehouse";
            this.cmbSrcWarehouse.NewButton = false;
            this.cmbSrcWarehouse.Size = new Size(0x158, 0x15);
            this.cmbSrcWarehouse.TabIndex = 3;
            this.lblSrcWarehouse.Location = new Point(0, 0);
            this.lblSrcWarehouse.Name = "lblSrcWarehouse";
            this.lblSrcWarehouse.Size = new Size(480, 0x15);
            this.lblSrcWarehouse.TabIndex = 2;
            this.lblSrcWarehouse.Text = "Source Warehouse :";
            this.wpDstWarehouse.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wpDstWarehouse.Controls.Add(this.cmbDstWarehouse);
            this.wpDstWarehouse.Controls.Add(this.lblDstWarehouse);
            this.wpDstWarehouse.HelpButtonVisible = false;
            this.wpDstWarehouse.Location = new Point(0x10, 0x4c);
            this.wpDstWarehouse.Name = "wpDstWarehouse";
            this.wpDstWarehouse.PageCaption = "Select destination warehouse";
            this.wpDstWarehouse.PageDescription = "Select warehouse to which you would like to transfer inventory";
            this.wpDstWarehouse.PageTitleBarText = "Select destination warehouse";
            this.wpDstWarehouse.Size = new Size(0x2ae, 0x123);
            this.wpDstWarehouse.TabIndex = 6;
            this.cmbDstWarehouse.EditButton = false;
            this.cmbDstWarehouse.Location = new Point(0x10, 0x20);
            this.cmbDstWarehouse.Name = "cmbDstWarehouse";
            this.cmbDstWarehouse.NewButton = false;
            this.cmbDstWarehouse.Size = new Size(0x158, 0x15);
            this.cmbDstWarehouse.TabIndex = 3;
            this.lblDstWarehouse.Location = new Point(0, 0);
            this.lblDstWarehouse.Name = "lblDstWarehouse";
            this.lblDstWarehouse.Size = new Size(480, 0x15);
            this.lblDstWarehouse.TabIndex = 2;
            this.lblDstWarehouse.Text = "Destination Warehouse :";
            this.wpSerialized.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wpSerialized.Controls.Add(this.gridSerials);
            this.wpSerialized.Controls.Add(this.lblSerials);
            this.wpSerialized.HelpButtonVisible = false;
            this.wpSerialized.Location = new Point(0x10, 0x4c);
            this.wpSerialized.Name = "wpSerialized";
            this.wpSerialized.PageCaption = "Select serials";
            this.wpSerialized.PageDescription = "Select serials that you would like to transfer";
            this.wpSerialized.PageTitleBarText = "Select serials";
            this.wpSerialized.Size = new Size(0x2ae, 0x123);
            this.wpSerialized.TabIndex = 1;
            this.wpSerialized.Text = "WizardPage1";
            this.gridSerials.Dock = DockStyle.Fill;
            this.gridSerials.Location = new Point(0, 0x17);
            this.gridSerials.Name = "gridSerials";
            this.gridSerials.Size = new Size(0x2ae, 0x10c);
            this.gridSerials.TabIndex = 2;
            this.lblSerials.Dock = DockStyle.Top;
            this.lblSerials.Location = new Point(0, 0);
            this.lblSerials.Name = "lblSerials";
            this.lblSerials.Size = new Size(0x2ae, 0x17);
            this.lblSerials.TabIndex = 1;
            this.lblSerials.Text = "Serials :";
            this.wpNonSerialized.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wpNonSerialized.Controls.Add(this.nudQuantity);
            this.wpNonSerialized.Controls.Add(this.lblQuantity);
            this.wpNonSerialized.HelpButtonVisible = false;
            this.wpNonSerialized.Location = new Point(0x10, 0x4c);
            this.wpNonSerialized.Name = "wpNonSerialized";
            this.wpNonSerialized.PageCaption = "Quantity to transfer";
            this.wpNonSerialized.PageDescription = "Enter quantity that you would like to transfer";
            this.wpNonSerialized.PageTitleBarText = "Quantity to transfer";
            this.wpNonSerialized.Size = new Size(0x2ae, 0x123);
            this.wpNonSerialized.TabIndex = 3;
            this.nudQuantity.Location = new Point(0x10, 0x20);
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new Size(120, 20);
            this.nudQuantity.TabIndex = 13;
            this.lblQuantity.Location = new Point(0, 0);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new Size(480, 0x15);
            this.lblQuantity.TabIndex = 11;
            this.lblQuantity.Text = "Quantity :";
            this.wpSummary.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wpSummary.Controls.Add(this.txtSummary);
            this.wpSummary.HelpButtonVisible = false;
            this.wpSummary.Location = new Point(0x10, 0x4c);
            this.wpSummary.Name = "wpSummary";
            this.wpSummary.PageCaption = "Summary";
            this.wpSummary.PageDescription = "Review transfer summary";
            this.wpSummary.PageTitleBarText = "Summary";
            this.wpSummary.Size = new Size(0x2ae, 0x123);
            this.wpSummary.TabIndex = 4;
            this.txtSummary.Dock = DockStyle.Fill;
            this.txtSummary.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.txtSummary.Location = new Point(0, 0);
            this.txtSummary.MaxLength = 0;
            this.txtSummary.Multiline = true;
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.ReadOnly = true;
            this.txtSummary.ScrollBars = ScrollBars.Both;
            this.txtSummary.Size = new Size(0x2ae, 0x123);
            this.txtSummary.TabIndex = 0;
            this.txtSummary.Text = "Some text goes here";
            this.wpReport.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wpReport.BackButtonVisible = false;
            this.wpReport.CancelButtonVisible = false;
            this.wpReport.Controls.Add(this.txtReport);
            this.wpReport.HelpButtonVisible = false;
            this.wpReport.Location = new Point(0x10, 0x4c);
            this.wpReport.Name = "wpReport";
            this.wpReport.NextButtonVisible = false;
            this.wpReport.PageCaption = "Report";
            this.wpReport.PageDescription = "Inventory transfer report";
            this.wpReport.PageTitleBarText = "Report";
            this.wpReport.Size = new Size(0x2ae, 0x123);
            this.wpReport.TabIndex = 7;
            this.txtReport.Dock = DockStyle.Fill;
            this.txtReport.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.txtReport.Location = new Point(0, 0);
            this.txtReport.MaxLength = 0;
            this.txtReport.Multiline = true;
            this.txtReport.Name = "txtReport";
            this.txtReport.ReadOnly = true;
            this.txtReport.ScrollBars = ScrollBars.Both;
            this.txtReport.Size = new Size(0x2ae, 0x123);
            this.txtReport.TabIndex = 1;
            this.txtReport.Text = "Some text goes here";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1fd, 0x169);
            base.Controls.Add(this.wizard);
            base.Name = "WizardInventoryTransfer";
            this.Text = "Inventory Transfer";
            ((ISupportInitialize) this.wizard).EndInit();
            this.wpWelcome.ResumeLayout(false);
            this.wpWelcome.PerformLayout();
            this.wpInventoryItem.ResumeLayout(false);
            this.wpSrcWarehouse.ResumeLayout(false);
            this.wpDstWarehouse.ResumeLayout(false);
            this.wpSerialized.ResumeLayout(false);
            this.wpNonSerialized.ResumeLayout(false);
            this.nudQuantity.EndInit();
            this.wpSummary.ResumeLayout(false);
            this.wpSummary.PerformLayout();
            this.wpReport.ResumeLayout(false);
            this.wpReport.PerformLayout();
            base.ResumeLayout(false);
        }

        private void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AllowEdit = true;
            Appearance.AllowNew = false;
            Appearance.AllowDelete = false;
            Appearance.AutoGenerateColumns = false;
            Appearance.EditMode = DataGridViewEditMode.EditOnEnter;
            Appearance.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle(Appearance.BoolStyle()) {
                BackColor = Color.LightSteelBlue
            };
            Appearance.Columns.Clear();
            DataGridViewCheckBoxColumn column1 = Appearance.AddBoolColumn("Selected", "...", 50, cellStyle);
            column1.ReadOnly = false;
            column1.ThreeState = false;
            Appearance.AddTextColumn("SerialNumber", "Serial#", 120);
        }

        private void InitStage_DstWarehouse()
        {
            this.cmbDstWarehouse_SelectedIndexChanged(this.cmbDstWarehouse, EventArgs.Empty);
        }

        private void InitStage_InventoryItem()
        {
            this.cmbInventoryItem_SelectedIndexChanged(this.cmbInventoryItem, EventArgs.Empty);
        }

        private void InitStage_NonSerialized()
        {
            int? inventoryItemID = this.GetInventoryItemID();
            int? srcWarehouseID = this.GetSrcWarehouseID();
            if (!((inventoryItemID != null) & (srcWarehouseID != null)))
            {
                this.nudQuantity.Tag = null;
                this.nudQuantity.Value = 0M;
                this.nudQuantity.Maximum = 0M;
            }
            else
            {
                Pair tag = this.nudQuantity.Tag as Pair;
                if ((tag == null) || ((tag.InventoryItemID != inventoryItemID.Value) || (tag.WarehouseID != srcWarehouseID.Value)))
                {
                    tag = new Pair(inventoryItemID.Value, srcWarehouseID.Value);
                    using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                    {
                        using (MySqlCommand command = new MySqlCommand("", connection))
                        {
                            command.CommandText = "SELECT OnHand\r\nFROM tbl_inventory\r\nWHERE (InventoryItemID = :InventoryItemID)\r\n  AND (WarehouseID     = :WarehouseID)";
                            command.Parameters.Add("WarehouseID", MySqlType.Int).Value = srcWarehouseID.Value;
                            command.Parameters.Add("InventoryItemID", MySqlType.Int).Value = inventoryItemID.Value;
                            connection.Open();
                            int num = Math.Max(0, NullableConvert.ToInt32(command.ExecuteScalar()).GetValueOrDefault(0));
                            this.nudQuantity.Tag = tag;
                            this.nudQuantity.Value = Math.Min(this.nudQuantity.Value, new decimal(num));
                            this.nudQuantity.Maximum = new decimal(num);
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            this.nudQuantity_ValueChanged(this.nudQuantity, EventArgs.Empty);
        }

        private void InitStage_Report()
        {
            bool flag = false;
            try
            {
                int? inventoryItemID = this.GetInventoryItemID();
                int? srcWarehouseID = this.GetSrcWarehouseID();
                int? dstWarehouseID = this.GetDstWarehouseID();
                if (inventoryItemID == null)
                {
                    throw new UserNotifyException("Inventory item is not selected");
                }
                if (srcWarehouseID == null)
                {
                    throw new UserNotifyException("Source warehouse is not selected");
                }
                if (dstWarehouseID == null)
                {
                    throw new UserNotifyException("Destination warehouse is not selected");
                }
                using (StringWriter writer = new StringWriter())
                {
                    if (this.GetInventoryItemSerialized())
                    {
                        TableSerials tableSource = this.gridSerials.GetTableSource<TableSerials>();
                        if (tableSource == null)
                        {
                            throw new Exception("Unknown error");
                        }
                        using (MySqlConnection connection2 = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                        {
                            using (MySqlCommand command2 = new MySqlCommand("", connection2))
                            {
                                command2.Parameters.Add("P_SerialID", MySqlType.Int);
                                command2.Parameters.Add("P_SrcWarehouseID", MySqlType.Int).Value = srcWarehouseID.Value;
                                command2.Parameters.Add("P_DstWarehouseID", MySqlType.Int).Value = dstWarehouseID.Value;
                                command2.Parameters.Add("P_LastUpdateUserID", MySqlType.Int).Value = Globals.CompanyUserID;
                                connection2.Open();
                                DataRow[] rowArray = tableSource.Select("[Selected] = 1");
                                int index = 0;
                                while (true)
                                {
                                    if (index >= rowArray.Length)
                                    {
                                        flag = true;
                                        break;
                                    }
                                    DataRow row = rowArray[index];
                                    command2.Parameters["P_SerialID"].Value = row[tableSource.Col_ID];
                                    command2.ExecuteProcedure("serial_transfer");
                                    writer.WriteLine("Serial '{0}' was successfully transferred", row[tableSource.Col_SerialNumber]);
                                    flag = true;
                                    index++;
                                }
                            }
                        }
                    }
                    else
                    {
                        int nonserializedQuantity = this.GetNonserializedQuantity();
                        using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                        {
                            using (MySqlCommand command = new MySqlCommand("", connection))
                            {
                                command.Parameters.Add("P_InventoryItemID", MySqlType.Int).Value = inventoryItemID.Value;
                                command.Parameters.Add("P_SrcWarehouseID", MySqlType.Int).Value = srcWarehouseID.Value;
                                command.Parameters.Add("P_DstWarehouseID", MySqlType.Int).Value = dstWarehouseID.Value;
                                command.Parameters.Add("P_Quantity", MySqlType.Int).Value = nonserializedQuantity;
                                command.Parameters.Add("P_LastUpdateUserID", MySqlType.Int).Value = Globals.CompanyUserID;
                                connection.Open();
                                command.ExecuteProcedure("inventory_transfer");
                                writer.WriteLine("Inventory was successfully transferred");
                                flag = true;
                            }
                        }
                        goto TR_000F;
                    }
                    goto TR_001C;
                TR_000F:
                    this.txtReport.Text = writer.ToString();
                    goto TR_0000;
                TR_001C:
                    writer.WriteLine("Serials were successfully transferred");
                    goto TR_000F;
                }
            }
            catch (UserNotifyException exception1)
            {
                UserNotifyException ex = exception1;
                ProjectData.SetProjectError(ex);
                UserNotifyException exception = ex;
                this.txtReport.Text = exception.Message;
                ProjectData.ClearProjectError();
            }
            catch (Exception exception3)
            {
                Exception ex = exception3;
                ProjectData.SetProjectError(ex);
                this.txtReport.Text = "Exception\r\n\r\n" + ex.ToString();
                ProjectData.ClearProjectError();
            }
        TR_0000:
            this.wpReport.FinishButtonVisible = flag;
            this.wpReport.CancelButtonVisible = !flag;
            this.wpReport.NextButtonVisible = !flag;
            this.wpReport.BackButtonVisible = !flag;
            string[] tableNames = new string[] { "tbl_inventory_transaction", "tbl_inventory" };
            ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
        }

        private void InitStage_Serialized()
        {
            int? inventoryItemID = this.GetInventoryItemID();
            int? srcWarehouseID = this.GetSrcWarehouseID();
            if ((inventoryItemID == null) | (srcWarehouseID == null))
            {
                this.AttachTable(null);
                this.wpSerialized.NextButtonEnabled = WizardButtonEnabledDefault.False;
            }
            else
            {
                TableSerials tableSource = this.gridSerials.GetTableSource<TableSerials>();
                if ((tableSource == null) || ((tableSource.InventoryItemID != inventoryItemID.Value) || (tableSource.WarehouseID != srcWarehouseID.Value)))
                {
                    tableSource = new TableSerials(inventoryItemID.Value, srcWarehouseID.Value);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter("", ClassGlobalObjects.ConnectionString_MySql))
                    {
                        adapter.SelectCommand.CommandText = "SELECT\r\n  tbl_serial.ID\r\n, tbl_serial.SerialNumber\r\nFROM tbl_serial\r\nWHERE (Status = 'On Hand')\r\n  AND (InventoryItemID = :InventoryItemID)\r\n  AND (WarehouseID     = :WarehouseID)";
                        adapter.SelectCommand.Parameters.Add("InventoryItemID", MySqlType.Int).Value = tableSource.InventoryItemID;
                        adapter.SelectCommand.Parameters.Add("WarehouseID", MySqlType.Int).Value = tableSource.WarehouseID;
                        adapter.AcceptChangesDuringFill = true;
                        adapter.MissingSchemaAction = MissingSchemaAction.Ignore;
                        adapter.Fill(tableSource);
                    }
                    this.AttachTable(tableSource);
                    this.wpSerialized.NextButtonEnabled = WizardButtonEnabledDefault.False;
                }
            }
        }

        private void InitStage_SrcWarehouse()
        {
            this.cmbSrcWarehouse_SelectedIndexChanged(this.cmbSrcWarehouse, EventArgs.Empty);
        }

        private void InitStage_Summary()
        {
            using (StringWriter writer = new StringWriter())
            {
                writer.WriteLine("Inventory item: ({0}) {1}", this.GetInventoryItemID(), this.GetInventoryItemName());
                writer.WriteLine("Source warehouse: ({0}) {1}", this.GetSrcWarehouseID(), this.GetSrcWarehouseName());
                writer.WriteLine("Destination warehouse: ({0}) {1}", this.GetDstWarehouseID(), this.GetDstWarehouseName());
                if (!this.GetInventoryItemSerialized())
                {
                    writer.WriteLine("Quantity: {0}", this.GetNonserializedQuantity());
                }
                else
                {
                    TableSerials tableSource = this.gridSerials.GetTableSource<TableSerials>();
                    if (tableSource == null)
                    {
                        writer.WriteLine("Nothing selected");
                    }
                    else
                    {
                        DataRow[] rowArray = tableSource.Select("[Selected] = 1");
                        if (rowArray.Length == 0)
                        {
                            writer.WriteLine("Nothing selected");
                        }
                        else
                        {
                            writer.WriteLine("Selected serials:");
                            foreach (DataRow row in rowArray)
                            {
                                writer.WriteLine(row[tableSource.Col_SerialNumber]);
                            }
                        }
                    }
                }
                this.txtSummary.Text = writer.ToString();
            }
        }

        private void nudQuantity_KeyUp(object sender, KeyEventArgs e)
        {
            this.nudQuantity_ValueChanged(sender, e);
        }

        private void nudQuantity_ValueChanged(object sender, EventArgs e)
        {
            if (0 < this.GetNonserializedQuantity())
            {
                this.wpNonSerialized.NextButtonEnabled = WizardButtonEnabledDefault.Auto;
            }
            else
            {
                this.wpNonSerialized.NextButtonEnabled = WizardButtonEnabledDefault.False;
            }
        }

        protected void ProcessParameter_DstWarehouseID(FormParameters Params)
        {
            if (Params != null)
            {
                int? nullable = NullableConvert.ToInt32(Params["DstWarehouseID"]);
                if (nullable != null)
                {
                    SetComboBoxValue(this.cmbDstWarehouse, nullable.Value);
                }
                else
                {
                    nullable = NullableConvert.ToInt32(Params["DestinationWarehouseID"]);
                    if (nullable != null)
                    {
                        SetComboBoxValue(this.cmbDstWarehouse, nullable.Value);
                    }
                    else
                    {
                        nullable = NullableConvert.ToInt32(Params["ToWarehouseID"]);
                        if (nullable != null)
                        {
                            SetComboBoxValue(this.cmbDstWarehouse, nullable.Value);
                        }
                    }
                }
            }
        }

        protected void ProcessParameter_InventoryItemID(FormParameters Params)
        {
            if (Params != null)
            {
                int? nullable = NullableConvert.ToInt32(Params["InventoryItemID"]);
                if (nullable != null)
                {
                    SetComboBoxValue(this.cmbInventoryItem, nullable.Value);
                }
            }
        }

        protected void ProcessParameter_SrcWarehouseID(FormParameters Params)
        {
            if (Params != null)
            {
                int? nullable = NullableConvert.ToInt32(Params["SrcWarehouseID"]);
                if (nullable != null)
                {
                    SetComboBoxValue(this.cmbSrcWarehouse, nullable.Value);
                }
                else
                {
                    nullable = NullableConvert.ToInt32(Params["SourceWarehouseID"]);
                    if (nullable != null)
                    {
                        SetComboBoxValue(this.cmbSrcWarehouse, nullable.Value);
                    }
                    else
                    {
                        nullable = NullableConvert.ToInt32(Params["FromWarehouseID"]);
                        if (nullable != null)
                        {
                            SetComboBoxValue(this.cmbSrcWarehouse, nullable.Value);
                        }
                    }
                }
            }
        }

        private static void SetComboBoxValue(Combobox combobox)
        {
            if ((combobox.Tag != null) && (combobox.DataSource != null))
            {
                Functions.SetComboBoxValue(combobox, combobox.Tag);
                combobox.Tag = null;
            }
        }

        private static void SetComboBoxValue(Combobox combobox, object Value)
        {
            if (combobox.DataSource != null)
            {
                Functions.SetComboBoxValue(combobox, Value);
            }
            else
            {
                combobox.Tag = Value;
            }
        }

        public void SetParameters(FormParameters Params)
        {
            this.ProcessParameter_InventoryItemID(Params);
            this.ProcessParameter_DstWarehouseID(Params);
            this.ProcessParameter_SrcWarehouseID(Params);
        }

        private void Table_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (0 < NullableConvert.ToInt32(e.Row.Table.Compute("COUNT([Selected])", "[Selected] = true")).GetValueOrDefault(0))
            {
                this.wpSerialized.NextButtonEnabled = WizardButtonEnabledDefault.Auto;
            }
            else
            {
                this.wpSerialized.NextButtonEnabled = WizardButtonEnabledDefault.False;
            }
        }

        private void wizard_BackButtonClick(object sender, WizardPageCancelEventArgs e)
        {
            e.Cancel = true;
            if (ReferenceEquals(this.wizard.SelectedPage, this.wpWelcome))
            {
                this.wizard.SelectedPage = this.wpWelcome;
            }
            else if (ReferenceEquals(this.wizard.SelectedPage, this.wpInventoryItem))
            {
                this.wizard.SelectedPage = this.wpWelcome;
            }
            else if (ReferenceEquals(this.wizard.SelectedPage, this.wpSrcWarehouse))
            {
                this.wizard.SelectedPage = this.wpInventoryItem;
            }
            else if (ReferenceEquals(this.wizard.SelectedPage, this.wpDstWarehouse))
            {
                this.wizard.SelectedPage = this.wpSrcWarehouse;
            }
            else if (ReferenceEquals(this.wizard.SelectedPage, this.wpNonSerialized))
            {
                this.wizard.SelectedPage = this.wpDstWarehouse;
            }
            else if (ReferenceEquals(this.wizard.SelectedPage, this.wpSerialized))
            {
                this.wizard.SelectedPage = this.wpDstWarehouse;
            }
            else if (ReferenceEquals(this.wizard.SelectedPage, this.wpSummary))
            {
                if (this.GetInventoryItemSerialized())
                {
                    this.wizard.SelectedPage = this.wpSerialized;
                }
                else
                {
                    this.wizard.SelectedPage = this.wpNonSerialized;
                }
            }
        }

        private void wizard_CancelButtonClick(object sender, EventArgs e)
        {
            base.Close();
        }

        private void wizard_FinishButtonClick(object sender, EventArgs e)
        {
            base.Close();
        }

        private void wizard_NextButtonClick(object sender, WizardPageCancelEventArgs e)
        {
            e.Cancel = true;
            if (ReferenceEquals(this.wizard.SelectedPage, this.wpWelcome))
            {
                this.wizard.SelectedPage = this.wpInventoryItem;
            }
            else if (ReferenceEquals(this.wizard.SelectedPage, this.wpInventoryItem))
            {
                this.wizard.SelectedPage = this.wpSrcWarehouse;
            }
            else if (ReferenceEquals(this.wizard.SelectedPage, this.wpSrcWarehouse))
            {
                this.wizard.SelectedPage = this.wpDstWarehouse;
            }
            else if (ReferenceEquals(this.wizard.SelectedPage, this.wpDstWarehouse))
            {
                if (this.GetInventoryItemSerialized())
                {
                    this.wizard.SelectedPage = this.wpSerialized;
                }
                else
                {
                    this.wizard.SelectedPage = this.wpNonSerialized;
                }
            }
            else if (ReferenceEquals(this.wizard.SelectedPage, this.wpSerialized))
            {
                this.wizard.SelectedPage = this.wpSummary;
            }
            else if (ReferenceEquals(this.wizard.SelectedPage, this.wpNonSerialized))
            {
                this.wizard.SelectedPage = this.wpSummary;
            }
            else if (ReferenceEquals(this.wizard.SelectedPage, this.wpSummary))
            {
                this.wizard.SelectedPage = this.wpReport;
            }
            else if (ReferenceEquals(this.wizard.SelectedPage, this.wpReport))
            {
                this.wizard.SelectedPage = this.wpReport;
            }
        }

        private void wizard_SelectionChanged(object sender, EventArgs e)
        {
            if (!ReferenceEquals(this.wizard.SelectedPage, this.wpWelcome))
            {
                if (ReferenceEquals(this.wizard.SelectedPage, this.wpInventoryItem))
                {
                    this.InitStage_InventoryItem();
                }
                else if (ReferenceEquals(this.wizard.SelectedPage, this.wpSrcWarehouse))
                {
                    this.InitStage_SrcWarehouse();
                }
                else if (ReferenceEquals(this.wizard.SelectedPage, this.wpDstWarehouse))
                {
                    this.InitStage_DstWarehouse();
                }
                else if (ReferenceEquals(this.wizard.SelectedPage, this.wpSerialized))
                {
                    this.InitStage_Serialized();
                }
                else if (ReferenceEquals(this.wizard.SelectedPage, this.wpNonSerialized))
                {
                    this.InitStage_NonSerialized();
                }
                else if (ReferenceEquals(this.wizard.SelectedPage, this.wpSummary))
                {
                    this.InitStage_Summary();
                }
                else if (ReferenceEquals(this.wizard.SelectedPage, this.wpReport))
                {
                    this.InitStage_Report();
                }
            }
        }

        [field: AccessedThroughProperty("wpSerialized")]
        private WizardPage wpSerialized { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("wpWelcome")]
        private WizardWelcomePage wpWelcome { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label3")]
        internal virtual Label Label3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label2")]
        internal virtual Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label1")]
        private Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("wpInventoryItem")]
        private WizardPage wpInventoryItem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("wpNonSerialized")]
        private WizardPage wpNonSerialized { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("wpSummary")]
        private WizardPage wpSummary { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbInventoryItem")]
        private Combobox cmbInventoryItem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInventoryItem")]
        private Label lblInventoryItem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gridSerials")]
        private FilteredGrid gridSerials { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSerials")]
        private Label lblSerials { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuantity")]
        private Label lblQuantity { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("wizard")]
        private ActiproSoftware.Wizard.Wizard wizard { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtSummary")]
        private TextBox txtSummary { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbSrcWarehouse")]
        private Combobox cmbSrcWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSrcWarehouse")]
        private Label lblSrcWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbDstWarehouse")]
        private Combobox cmbDstWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDstWarehouse")]
        private Label lblDstWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("wpSrcWarehouse")]
        private WizardPage wpSrcWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("wpDstWarehouse")]
        private WizardPage wpDstWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nudQuantity")]
        internal virtual NumericUpDown nudQuantity { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtReport")]
        private TextBox txtReport { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("wpReport")]
        private WizardPage wpReport { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        private class Pair
        {
            private int F_InventoryItemID;
            private int F_WarehouseID;

            public Pair(int InventoryItemID, int WarehouseID)
            {
                this.InventoryItemID = InventoryItemID;
                this.WarehouseID = WarehouseID;
            }

            public int InventoryItemID
            {
                get => 
                    this.F_InventoryItemID;
                private set => 
                    this.F_InventoryItemID = value;
            }

            public int WarehouseID
            {
                get => 
                    this.F_WarehouseID;
                private set => 
                    this.F_WarehouseID = value;
            }
        }

        private class TableSerials : TableBase
        {
            private DataColumn _col_ID;
            private DataColumn _col_SerialNumber;
            private DataColumn _col_Selected;

            public TableSerials(int InventoryItemID, int WarehouseID) : base("tbl_serials")
            {
                this.InventoryItemID = InventoryItemID;
                this.WarehouseID = WarehouseID;
            }

            protected int? GetProperty(string Name) => 
                NullableConvert.ToInt32(base.ExtendedProperties[Name]);

            protected override void Initialize()
            {
                this._col_ID = base.Columns["ID"];
                this._col_SerialNumber = base.Columns["SerialNumber"];
                this._col_Selected = base.Columns["Selected"];
            }

            protected override void InitializeClass()
            {
                base.Columns.Add("ID", typeof(int));
                base.Columns.Add("SerialNumber", typeof(string));
                DataColumn column1 = base.Columns.Add("Selected", typeof(bool));
                column1.AllowDBNull = false;
                column1.DefaultValue = false;
            }

            protected override void OnColumnChanged(DataColumnChangeEventArgs e)
            {
                if (ReferenceEquals(e.Column, this.Col_Selected))
                {
                    e.Row.BeginEdit();
                    e.Row.EndEdit();
                }
                base.OnColumnChanged(e);
            }

            protected void SetProperty(string Name, int? Value)
            {
                if (Value != null)
                {
                    base.ExtendedProperties[Name] = Value.Value;
                }
                else
                {
                    base.ExtendedProperties.Remove(Name);
                }
            }

            public int InventoryItemID
            {
                get => 
                    this.GetProperty("InventoryItemID").Value;
                private set => 
                    this.SetProperty("InventoryItemID", new int?(value));
            }

            public int WarehouseID
            {
                get => 
                    this.GetProperty("WarehouseID").Value;
                private set => 
                    this.SetProperty("WarehouseID", new int?(value));
            }

            public DataColumn Col_ID =>
                this._col_ID;

            public DataColumn Col_Selected =>
                this._col_Selected;

            public DataColumn Col_SerialNumber =>
                this._col_SerialNumber;
        }
    }
}

