namespace DMEWorks.Details
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Core;
    using DMEWorks.Data.MySql;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class ControlInvoiceDetails : ControlDetails
    {
        private IContainer components;
        private int? _CustomerID = null;
        private int? _OrderID = null;

        public event EventHandler CustomerIDChanged;

        public event EventHandler OrderIDChanged;

        public ControlInvoiceDetails()
        {
            this.InitializeComponent();
        }

        private void Appearance_CellFormatting(object sender, GridCellFormattingEventArgs e)
        {
            try
            {
                DataRow dataRow = e.Row.GetDataRow();
                if (dataRow != null)
                {
                    DataColumn column = dataRow.Table.Columns["Returned"];
                    if ((column != null) && NullableConvert.ToBoolean(dataRow[column], false))
                    {
                        e.CellStyle.BackColor = Color.LightSteelBlue;
                    }
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
        }

        public void ClearGrid()
        {
            base.CloseDialogs();
            int? nullable = null;
            TableInvoiceDetails table = this.GetTable();
            table.CustomerID = nullable;
            nullable = null;
            table.InvoiceID = nullable;
            table.Clear();
            table.AcceptChanges();
        }

        protected override FormDetails CreateDialog(object param)
        {
            FormInvoiceDetail dialog = new FormInvoiceDetail(this);
            base.AddDialog(dialog);
            return dialog;
        }

        protected override ControlDetails.TableDetails CreateTable() => 
            new TableInvoiceDetails();

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void DoAdd(object param)
        {
            throw new NotSupportedException("method is not supported");
        }

        protected override void DoDelete()
        {
            throw new NotSupportedException("method is not supported");
        }

        private void F_TableNotes_Changed(object sender, EventArgs e)
        {
            base.OnChanged(e);
        }

        private void F_TableTransactions_Changed(object sender, EventArgs e)
        {
            base.OnChanged(e);
        }

        protected TableInvoiceDetails GetTable() => 
            (TableInvoiceDetails) base.F_TableDetails;

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.Panel1.SuspendLayout();
            base.SuspendLayout();
            this.Panel1.Visible = false;
            base.Name = "ControlInvoiceDetails";
            this.Panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        protected override void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("ID", "#", 50, Appearance.IntegerStyle());
            Appearance.AddTextColumn("InventoryItem", "Inventory Item", 200);
            Appearance.AddTextColumn("BillingCode", "Billing Code", 70);
            Appearance.AddTextColumn("PriceCode", "Price Code", 70);
            Appearance.AddTextColumn("Quantity", "Quantity", 50, Appearance.IntegerStyle());
            Appearance.AddTextColumn("BillableAmount", "Billable", 60, Appearance.PriceStyle());
            Appearance.AddTextColumn("BALANCE", "Balance", 60, Appearance.PriceStyle());
            Appearance.CellFormatting += new EventHandler<GridCellFormattingEventArgs>(this.Appearance_CellFormatting);
        }

        public void LoadGrid(MySqlConnection cnn, int CustomerID, int InvoiceID)
        {
            base.CloseDialogs();
            TableInvoiceDetails table = this.GetTable();
            int? nullable = null;
            table.CustomerID = nullable;
            nullable = null;
            table.InvoiceID = nullable;
            table.Clear();
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("", cnn))
            {
                adapter.SelectCommand.CommandText = "SELECT tbl_inventoryitem.Name as InventoryItem,\r\n       tbl_pricecode.Name as PriceCode,\r\n       tbl_cmnform.CMNType as CMNFormType,\r\n       IF(tbl_orderdetails.NextOrderID IS NULL, 0, 1) as Returned,\r\n       tbl_invoicedetails.*\r\nFROM tbl_invoicedetails\r\n     LEFT JOIN tbl_inventoryitem ON tbl_invoicedetails.InventoryItemID = tbl_inventoryitem.ID\r\n     LEFT JOIN tbl_pricecode ON tbl_invoicedetails.PriceCodeID = tbl_pricecode.ID\r\n     LEFT JOIN tbl_cmnform ON tbl_invoicedetails.CMNFormID = tbl_cmnform.ID\r\n     LEFT JOIN tbl_orderdetails ON tbl_invoicedetails.CustomerID = tbl_orderdetails.CustomerID\r\n                               AND tbl_invoicedetails.OrderID = tbl_orderdetails.OrderID\r\n                               AND tbl_invoicedetails.OrderDetailsID = tbl_orderdetails.ID\r\nWHERE (tbl_invoicedetails.CustomerID = :CustomerID)\r\n  AND (tbl_invoicedetails.InvoiceID  = :InvoiceID )";
                adapter.SelectCommand.Parameters.Add("InvoiceID", MySqlType.Int).Value = InvoiceID;
                adapter.SelectCommand.Parameters.Add("CustomerID", MySqlType.Int).Value = CustomerID;
                adapter.MissingSchemaAction = MissingSchemaAction.Ignore;
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill(table);
            }
            table.CustomerID = new int?(CustomerID);
            table.InvoiceID = new int?(InvoiceID);
            table.AcceptChanges();
        }

        protected void OnCustomerIDChanged(EventArgs args)
        {
            EventHandler customerIDChangedEvent = this.CustomerIDChangedEvent;
            if (customerIDChangedEvent != null)
            {
                customerIDChangedEvent(this, args);
            }
        }

        protected void OnOrderIDChanged(EventArgs args)
        {
            EventHandler orderIDChangedEvent = this.OrderIDChangedEvent;
            if (orderIDChangedEvent != null)
            {
                orderIDChangedEvent(this, args);
            }
        }

        public void SaveGrid(MySqlConnection cnn, int CustomerID, int InvoiceID)
        {
            TableInvoiceDetails table = this.GetTable();
            if (!ValuesAreEqual(table.CustomerID, new int?(CustomerID)) || !ValuesAreEqual(table.InvoiceID, new int?(InvoiceID)))
            {
                throw new Exception("Stored keys differ from parameters");
            }
            TableInvoiceDetails changes = (TableInvoiceDetails) table.GetChanges();
            TableInvoiceDetails details3 = (TableInvoiceDetails) table.GetChanges();
            if (changes != null)
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                try
                {
                    MySqlTransaction transaction = cnn.BeginTransaction();
                    try
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter())
                        {
                            adapter.ContinueUpdateOnError = false;
                            adapter.UpdateCommand = new MySqlCommand();
                            adapter.UpdateCommand.Connection = cnn;
                            adapter.UpdateCommand.Transaction = transaction;
                            adapter.UpdateCommand.Parameters.Add("InventoryItemID", MySqlType.Int, 0, "InventoryItemID");
                            adapter.UpdateCommand.Parameters.Add("PriceCodeID", MySqlType.Int, 0, "PriceCodeID");
                            adapter.UpdateCommand.Parameters.Add("OrderID", MySqlType.Int, 0, "OrderID");
                            adapter.UpdateCommand.Parameters.Add("OrderDetailsID", MySqlType.Int, 0, "OrderDetailsID");
                            adapter.UpdateCommand.Parameters.Add("Quantity", MySqlType.Double, 0, "Quantity");
                            adapter.UpdateCommand.Parameters.Add("InvoiceDate", MySqlType.Date, 0, "InvoiceDate");
                            adapter.UpdateCommand.Parameters.Add("DOSFrom", MySqlType.Date, 0, "DOSFrom");
                            adapter.UpdateCommand.Parameters.Add("DOSTo", MySqlType.Date, 0, "DOSTo");
                            adapter.UpdateCommand.Parameters.Add("ShowSpanDates", MySqlType.Bit, 0, "ShowSpanDates");
                            adapter.UpdateCommand.Parameters.Add("BillingCode", MySqlType.VarChar, 50, "BillingCode");
                            adapter.UpdateCommand.Parameters.Add("Modifier1", MySqlType.VarChar, 8, "Modifier1");
                            adapter.UpdateCommand.Parameters.Add("Modifier2", MySqlType.VarChar, 8, "Modifier2");
                            adapter.UpdateCommand.Parameters.Add("Modifier3", MySqlType.VarChar, 8, "Modifier3");
                            adapter.UpdateCommand.Parameters.Add("Modifier4", MySqlType.VarChar, 8, "Modifier4");
                            adapter.UpdateCommand.Parameters.Add("DXPointer", MySqlType.VarChar, 50, "DXPointer");
                            adapter.UpdateCommand.Parameters.Add("DXPointer10", MySqlType.VarChar, 50, "DXPointer10");
                            adapter.UpdateCommand.Parameters.Add("DrugNoteField", MySqlType.VarChar, 20, "DrugNoteField");
                            adapter.UpdateCommand.Parameters.Add("DrugControlNumber", MySqlType.VarChar, 50, "DrugControlNumber");
                            adapter.UpdateCommand.Parameters.Add("BillIns1", MySqlType.Bit, 0, "BillIns1");
                            adapter.UpdateCommand.Parameters.Add("BillIns2", MySqlType.Bit, 0, "BillIns2");
                            adapter.UpdateCommand.Parameters.Add("BillIns3", MySqlType.Bit, 0, "BillIns3");
                            adapter.UpdateCommand.Parameters.Add("BillIns4", MySqlType.Bit, 0, "BillIns4");
                            adapter.UpdateCommand.Parameters.Add("NopayIns1", MySqlType.Bit, 0, "NopayIns1");
                            adapter.UpdateCommand.Parameters.Add("BillingMonth", MySqlType.Int, 0, "BillingMonth");
                            adapter.UpdateCommand.Parameters.Add("SendCMN_RX_w_invoice", MySqlType.Bit, 0, "SendCMN_RX_w_invoice");
                            adapter.UpdateCommand.Parameters.Add("SpecialCode", MySqlType.VarChar, 50, "SpecialCode");
                            adapter.UpdateCommand.Parameters.Add("ReviewCode", MySqlType.VarChar, 50, "ReviewCode");
                            adapter.UpdateCommand.Parameters.Add("MedicallyUnnecessary", MySqlType.Bit, 0, "MedicallyUnnecessary");
                            adapter.UpdateCommand.Parameters.Add("CMNFormID", MySqlType.Int, 0, "CMNFormID");
                            adapter.UpdateCommand.Parameters.Add("HaoDescription", MySqlType.VarChar, 10, "HaoDescription");
                            adapter.UpdateCommand.Parameters.Add("AuthorizationNumber", MySqlType.VarChar, 50, "AuthorizationNumber");
                            adapter.UpdateCommand.Parameters.Add("AuthorizationTypeID", MySqlType.Int, 4, "AuthorizationTypeID");
                            adapter.UpdateCommand.Parameters.Add("InvoiceNotes", MySqlType.VarChar, 0xff, "InvoiceNotes");
                            adapter.UpdateCommand.Parameters.Add("InvoiceRecord", MySqlType.VarChar, 0xff, "InvoiceRecord");
                            adapter.UpdateCommand.Parameters.Add("CustomerID", MySqlType.Int).Value = CustomerID;
                            adapter.UpdateCommand.Parameters.Add("InvoiceID", MySqlType.Int).Value = InvoiceID;
                            adapter.UpdateCommand.Parameters.Add("ID", MySqlType.Int, 0, "ID").SourceVersion = DataRowVersion.Original;
                            string[] whereParameters = new string[] { "CustomerID", "InvoiceID", "ID" };
                            adapter.UpdateCommand.GenerateUpdateCommand("tbl_invoicedetails", whereParameters);
                            adapter.Update(changes);
                        }
                        using (MySqlCommand command = new MySqlCommand())
                        {
                            command.Connection = cnn;
                            command.Transaction = transaction;
                            command.CommandType = CommandType.Text;
                            command.CommandText = "INSERT INTO `tbl_invoice_transaction`\r\n(`InvoiceDetailsID`\r\n,`InvoiceID`\r\n,`CustomerID`\r\n,`InsuranceCompanyID`\r\n,`CustomerInsuranceID`\r\n,`TransactionTypeID`\r\n,`TransactionDate`\r\n,`Amount`\r\n,`Quantity`\r\n,`Comments`\r\n,`LastUpdateUserID`)\r\nSELECT\r\n :P_InvoiceDetailsID\r\n,:P_InvoiceID\r\n,:P_CustomerID\r\n,NULL as InsuranceCompanyID\r\n,NULL as CustomerInsuranceID\r\n,ID as TransactionTypeID\r\n,CURRENT_DATE() as TransactionDate\r\n,:P_Amount as Amount\r\n,0 as Quantity\r\n,'Must be changed in trigger' as Comments\r\n,:P_LastUpdateUserID as LastUpdateUserID\r\nFROM tbl_invoice_transactiontype\r\nWHERE (Name = :P_TranType)";
                            command.Parameters.Add(new MySqlParameter("P_InvoiceDetailsID", MySqlType.Int));
                            MySqlParameter parameter1 = new MySqlParameter("P_InvoiceID", MySqlType.Int);
                            parameter1.Value = InvoiceID;
                            command.Parameters.Add(parameter1);
                            MySqlParameter parameter2 = new MySqlParameter("P_CustomerID", MySqlType.Int);
                            parameter2.Value = CustomerID;
                            command.Parameters.Add(parameter2);
                            command.Parameters.Add(new MySqlParameter("P_Amount", MySqlType.Double));
                            MySqlParameter parameter3 = new MySqlParameter("P_LastUpdateUserID", MySqlType.SmallInt);
                            parameter3.Value = Globals.CompanyUserID;
                            command.Parameters.Add(parameter3);
                            command.Parameters.Add(new MySqlParameter("P_TranType", MySqlType.VarChar, 50));
                            foreach (DataRow row in details3.Select())
                            {
                                double? nullable = NullableConvert.ToDouble(row[details3.Col_AllowableAmount, DataRowVersion.Original]);
                                double? nullable2 = NullableConvert.ToDouble(row[details3.Col_AllowableAmount, DataRowVersion.Current]);
                                if ((nullable != null) && ((nullable2 != null) && (0.001 < Math.Abs((double) (nullable.Value - nullable2.Value)))))
                                {
                                    command.Parameters["P_InvoiceDetailsID"].Value = row[details3.Col_ID];
                                    command.Parameters["P_Amount"].Value = nullable2.Value;
                                    command.Parameters["P_TranType"].Value = "Adjust Allowable";
                                    if (command.ExecuteNonQuery() != 1)
                                    {
                                        throw new UserNotifyException("Table tbl_invoice_transactiontype is damaged");
                                    }
                                }
                                double? nullable3 = NullableConvert.ToDouble(row[details3.Col_BillableAmount, DataRowVersion.Original]);
                                double? nullable4 = NullableConvert.ToDouble(row[details3.Col_BillableAmount, DataRowVersion.Current]);
                                if ((nullable3 != null) && ((nullable4 != null) && (0.001 < Math.Abs((double) (nullable3.Value - nullable4.Value)))))
                                {
                                    command.Parameters["P_InvoiceDetailsID"].Value = row[details3.Col_ID];
                                    command.Parameters["P_Amount"].Value = nullable4.Value;
                                    command.Parameters["P_TranType"].Value = "Adjust Customary";
                                    if (command.ExecuteNonQuery() != 1)
                                    {
                                        throw new UserNotifyException("Table tbl_invoice_transactiontype is damaged");
                                    }
                                }
                                double? nullable5 = NullableConvert.ToDouble(row[details3.Col_Taxes, DataRowVersion.Original]);
                                double? nullable6 = NullableConvert.ToDouble(row[details3.Col_Taxes, DataRowVersion.Current]);
                                if ((nullable5 != null) && ((nullable6 != null) && (0.001 < Math.Abs((double) (nullable5.Value - nullable6.Value)))))
                                {
                                    command.Parameters["P_InvoiceDetailsID"].Value = row[details3.Col_ID];
                                    command.Parameters["P_Amount"].Value = nullable6.Value;
                                    command.Parameters["P_TranType"].Value = "Adjust Taxes";
                                    if (command.ExecuteNonQuery() != 1)
                                    {
                                        throw new UserNotifyException("Table tbl_invoice_transactiontype is damaged");
                                    }
                                }
                            }
                        }
                        transaction.Commit();
                        table.AcceptChanges();
                    }
                    catch (Exception exception1)
                    {
                        Exception ex = exception1;
                        ProjectData.SetProjectError(ex);
                        Exception innerException = ex;
                        transaction.Rollback();
                        throw new Exception("SaveGrid", innerException);
                    }
                }
                finally
                {
                    bool flag;
                    if (flag)
                    {
                        cnn.Close();
                    }
                }
            }
        }

        public void ShowDetails(object InvoiceDetailsID)
        {
            try
            {
                DataTable tableSource = this.Grid.GetTableSource<DataTable>();
                if (tableSource != null)
                {
                    foreach (DataRow row in tableSource.Select($"[ID] = {InvoiceDetailsID}", "", DataViewRowState.CurrentRows))
                    {
                        base.EditRow(Conversions.ToInteger(row["RowID"]));
                    }
                }
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.ShowException(exception, "ShowDetails");
                ProjectData.ClearProjectError();
            }
        }

        private static bool ValuesAreEqual(int? X, int? Y) => 
            !((X != null) & (Y != null)) ? ((X != null) == (Y != null)) : (X.Value == Y.Value);

        public int? CustomerID
        {
            get => 
                this._CustomerID;
            set
            {
                if (!this._CustomerID.Equals(value))
                {
                    this._CustomerID = value;
                    this.OnCustomerIDChanged(EventArgs.Empty);
                }
            }
        }

        public int? OrderID
        {
            get => 
                this._OrderID;
            set
            {
                if (!this._OrderID.Equals(value))
                {
                    this._OrderID = value;
                    this.OnOrderIDChanged(EventArgs.Empty);
                }
            }
        }

        internal class ColumnNames
        {
            public const string ID = "ID";
            public const string InvoiceID = "InvoiceID";
            public const string CustomerID = "CustomerID";
            public const string InventoryItem = "InventoryItem";
            public const string InventoryItemID = "InventoryItemID";
            public const string PriceCode = "PriceCode";
            public const string PriceCodeID = "PriceCodeID";
            public const string OrderID = "OrderID";
            public const string OrderDetailsID = "OrderDetailsID";
            public const string BALANCE = "BALANCE";
            public const string BillableAmount = "BillableAmount";
            public const string AllowableAmount = "AllowableAmount";
            public const string Taxes = "Taxes";
            public const string Quantity = "Quantity";
            public const string InvoiceDate = "InvoiceDate";
            public const string DOSFrom = "DOSFrom";
            public const string DOSTo = "DOSTo";
            public const string ShowSpanDates = "ShowSpanDates";
            public const string BillingCode = "BillingCode";
            public const string Modifier1 = "Modifier1";
            public const string Modifier2 = "Modifier2";
            public const string Modifier3 = "Modifier3";
            public const string Modifier4 = "Modifier4";
            public const string DXPointer9 = "DXPointer";
            public const string DXPointer10 = "DXPointer10";
            public const string BillingMonth = "BillingMonth";
            public const string SendCMN_RX_w_invoice = "SendCMN_RX_w_invoice";
            public const string SpecialCode = "SpecialCode";
            public const string ReviewCode = "ReviewCode";
            public const string MedicallyUnnecessary = "MedicallyUnnecessary";
            public const string CMNFormType = "CMNFormType";
            public const string AuthorizationNumber = "AuthorizationNumber";
            public const string AuthorizationTypeID = "AuthorizationTypeID";
            public const string InvoiceNotes = "InvoiceNotes";
            public const string InvoiceRecord = "InvoiceRecord";
            public const string CMNFormID = "CMNFormID";
            public const string HaoDescription = "HaoDescription";
            public const string Returned = "Returned";
            public const string BillIns1 = "BillIns1";
            public const string BillIns2 = "BillIns2";
            public const string BillIns3 = "BillIns3";
            public const string BillIns4 = "BillIns4";
            public const string NopayIns1 = "NopayIns1";
            public const string Hardship = "Hardship";
            public const string AcceptAssignment = "AcceptAssignment";
            public const string DrugNoteField = "DrugNoteField";
            public const string DrugControlNumber = "DrugControlNumber";
        }

        public class TableInvoiceDetails : ControlDetails.TableDetails
        {
            public DataColumn Col_ID;
            public DataColumn Col_InventoryItem;
            public DataColumn Col_InventoryItemID;
            public DataColumn Col_PriceCode;
            public DataColumn Col_PriceCodeID;
            public DataColumn Col_OrderID;
            public DataColumn Col_OrderDetailsID;
            public DataColumn Col_BALANCE;
            public DataColumn Col_BillableAmount;
            public DataColumn Col_AllowableAmount;
            public DataColumn Col_Taxes;
            public DataColumn Col_Quantity;
            public DataColumn Col_InvoiceDate;
            public DataColumn Col_DOSFrom;
            public DataColumn Col_DOSTo;
            public DataColumn Col_ShowSpanDates;
            public DataColumn Col_BillingCode;
            public DataColumn Col_Modifier1;
            public DataColumn Col_Modifier2;
            public DataColumn Col_Modifier3;
            public DataColumn Col_Modifier4;
            public DataColumn Col_DXPointer9;
            public DataColumn Col_DXPointer10;
            public DataColumn Col_BillingMonth;
            public DataColumn Col_SendCMN_RX_w_invoice;
            public DataColumn Col_SpecialCode;
            public DataColumn Col_ReviewCode;
            public DataColumn Col_MedicallyUnnecessary;
            public DataColumn Col_CMNFormType;
            public DataColumn Col_CMNFormID;
            public DataColumn Col_HaoDescription;
            public DataColumn Col_AuthorizationTypeID;
            public DataColumn Col_AuthorizationNumber;
            public DataColumn Col_InvoiceNotes;
            public DataColumn Col_InvoiceRecord;
            public DataColumn Col_Returned;
            public DataColumn Col_BillIns1;
            public DataColumn Col_BillIns2;
            public DataColumn Col_BillIns3;
            public DataColumn Col_BillIns4;
            public DataColumn Col_NopayIns1;
            public DataColumn Col_Hardship;
            public DataColumn Col_AcceptAssignment;
            public DataColumn Col_DrugNoteField;
            public DataColumn Col_DrugControlNumber;

            public TableInvoiceDetails() : this("tbl_invoicedetails")
            {
            }

            public TableInvoiceDetails(string TableName) : base(TableName)
            {
            }

            protected int? GetProperty(string Name) => 
                NullableConvert.ToInt32(base.ExtendedProperties[Name]);

            protected override void Initialize()
            {
                base.Initialize();
                this.Col_ID = base.Columns["ID"];
                this.Col_InventoryItem = base.Columns["InventoryItem"];
                this.Col_InventoryItemID = base.Columns["InventoryItemID"];
                this.Col_PriceCode = base.Columns["PriceCode"];
                this.Col_PriceCodeID = base.Columns["PriceCodeID"];
                this.Col_OrderID = base.Columns["OrderID"];
                this.Col_OrderDetailsID = base.Columns["OrderDetailsID"];
                this.Col_BALANCE = base.Columns["BALANCE"];
                this.Col_BillableAmount = base.Columns["BillableAmount"];
                this.Col_AllowableAmount = base.Columns["AllowableAmount"];
                this.Col_Taxes = base.Columns["Taxes"];
                this.Col_Quantity = base.Columns["Quantity"];
                this.Col_InvoiceDate = base.Columns["InvoiceDate"];
                this.Col_DOSFrom = base.Columns["DOSFrom"];
                this.Col_DOSTo = base.Columns["DOSTo"];
                this.Col_ShowSpanDates = base.Columns["ShowSpanDates"];
                this.Col_BillingCode = base.Columns["BillingCode"];
                this.Col_Modifier1 = base.Columns["Modifier1"];
                this.Col_Modifier2 = base.Columns["Modifier2"];
                this.Col_Modifier3 = base.Columns["Modifier3"];
                this.Col_Modifier4 = base.Columns["Modifier4"];
                this.Col_DXPointer9 = base.Columns["DXPointer"];
                this.Col_DXPointer10 = base.Columns["DXPointer10"];
                this.Col_BillingMonth = base.Columns["BillingMonth"];
                this.Col_SendCMN_RX_w_invoice = base.Columns["SendCMN_RX_w_invoice"];
                this.Col_SpecialCode = base.Columns["SpecialCode"];
                this.Col_ReviewCode = base.Columns["ReviewCode"];
                this.Col_MedicallyUnnecessary = base.Columns["MedicallyUnnecessary"];
                this.Col_CMNFormType = base.Columns["CMNFormType"];
                this.Col_AuthorizationNumber = base.Columns["AuthorizationNumber"];
                this.Col_AuthorizationTypeID = base.Columns["AuthorizationTypeID"];
                this.Col_InvoiceNotes = base.Columns["InvoiceNotes"];
                this.Col_InvoiceRecord = base.Columns["InvoiceRecord"];
                this.Col_CMNFormID = base.Columns["CMNFormID"];
                this.Col_HaoDescription = base.Columns["HaoDescription"];
                this.Col_Returned = base.Columns["Returned"];
                this.Col_BillIns1 = base.Columns["BillIns1"];
                this.Col_BillIns2 = base.Columns["BillIns2"];
                this.Col_BillIns3 = base.Columns["BillIns3"];
                this.Col_BillIns4 = base.Columns["BillIns4"];
                this.Col_NopayIns1 = base.Columns["NopayIns1"];
                this.Col_Hardship = base.Columns["Hardship"];
                this.Col_AcceptAssignment = base.Columns["AcceptAssignment"];
                this.Col_DrugNoteField = base.Columns["DrugNoteField"];
                this.Col_DrugControlNumber = base.Columns["DrugControlNumber"];
            }

            protected override void InitializeClass()
            {
                base.InitializeClass();
                DataColumn[] columns = new DataColumn[0x2d];
                columns[0] = new DataColumn("ID", typeof(int));
                columns[1] = new DataColumn("InventoryItem", typeof(string));
                DataColumn column1 = new DataColumn("InventoryItemID", typeof(int));
                column1.AllowDBNull = false;
                columns[2] = column1;
                columns[3] = new DataColumn("PriceCode", typeof(string));
                DataColumn column2 = new DataColumn("PriceCodeID", typeof(int));
                column2.AllowDBNull = false;
                columns[4] = column2;
                columns[5] = new DataColumn("OrderID", typeof(int));
                columns[6] = new DataColumn("OrderDetailsID", typeof(int));
                DataColumn column3 = new DataColumn("BALANCE", typeof(double));
                column3.AllowDBNull = false;
                columns[7] = column3;
                DataColumn column4 = new DataColumn("BillableAmount", typeof(double));
                column4.AllowDBNull = false;
                column4.DefaultValue = 0.0;
                columns[8] = column4;
                DataColumn column5 = new DataColumn("AllowableAmount", typeof(double));
                column5.AllowDBNull = false;
                column5.DefaultValue = 0.0;
                columns[9] = column5;
                columns[10] = new DataColumn("Taxes", typeof(double));
                DataColumn column6 = new DataColumn("Quantity", typeof(double));
                column6.AllowDBNull = false;
                column6.DefaultValue = 0;
                columns[11] = column6;
                columns[12] = new DataColumn("InvoiceDate", typeof(DateTime));
                DataColumn column7 = new DataColumn("DOSFrom", typeof(DateTime));
                column7.AllowDBNull = false;
                columns[13] = column7;
                columns[14] = new DataColumn("DOSTo", typeof(DateTime));
                columns[15] = new DataColumn("ShowSpanDates", typeof(bool));
                columns[0x10] = new DataColumn("BillingCode", typeof(string));
                DataColumn column8 = new DataColumn("Modifier1", typeof(string));
                column8.AllowDBNull = false;
                columns[0x11] = column8;
                DataColumn column9 = new DataColumn("Modifier2", typeof(string));
                column9.AllowDBNull = false;
                columns[0x12] = column9;
                DataColumn column10 = new DataColumn("Modifier3", typeof(string));
                column10.AllowDBNull = false;
                columns[0x13] = column10;
                DataColumn column11 = new DataColumn("Modifier4", typeof(string));
                column11.AllowDBNull = false;
                columns[20] = column11;
                columns[0x15] = new DataColumn("DXPointer", typeof(string));
                columns[0x16] = new DataColumn("DXPointer10", typeof(string));
                DataColumn column12 = new DataColumn("BillingMonth", typeof(int));
                column12.AllowDBNull = false;
                column12.DefaultValue = 0;
                columns[0x17] = column12;
                DataColumn column13 = new DataColumn("SendCMN_RX_w_invoice", typeof(bool));
                column13.AllowDBNull = false;
                columns[0x18] = column13;
                columns[0x19] = new DataColumn("SpecialCode", typeof(string));
                columns[0x1a] = new DataColumn("ReviewCode", typeof(string));
                DataColumn column14 = new DataColumn("MedicallyUnnecessary", typeof(bool));
                column14.AllowDBNull = false;
                columns[0x1b] = column14;
                columns[0x1c] = new DataColumn("CMNFormType", typeof(string));
                columns[0x1d] = new DataColumn("AuthorizationNumber", typeof(string));
                columns[30] = new DataColumn("AuthorizationTypeID", typeof(int));
                columns[0x1f] = new DataColumn("InvoiceNotes", typeof(string));
                columns[0x20] = new DataColumn("InvoiceRecord", typeof(string));
                columns[0x21] = new DataColumn("CMNFormID", typeof(int));
                columns[0x22] = new DataColumn("HaoDescription", typeof(string));
                DataColumn column15 = new DataColumn("Returned", typeof(bool));
                column15.AllowDBNull = false;
                column15.DefaultValue = false;
                columns[0x23] = column15;
                DataColumn column16 = new DataColumn("BillIns1", typeof(bool));
                column16.AllowDBNull = false;
                column16.DefaultValue = false;
                columns[0x24] = column16;
                DataColumn column17 = new DataColumn("BillIns2", typeof(bool));
                column17.AllowDBNull = false;
                column17.DefaultValue = false;
                columns[0x25] = column17;
                DataColumn column18 = new DataColumn("BillIns3", typeof(bool));
                column18.AllowDBNull = false;
                column18.DefaultValue = false;
                columns[0x26] = column18;
                DataColumn column19 = new DataColumn("BillIns4", typeof(bool));
                column19.AllowDBNull = false;
                column19.DefaultValue = false;
                columns[0x27] = column19;
                DataColumn column20 = new DataColumn("NopayIns1", typeof(bool));
                column20.AllowDBNull = false;
                column20.DefaultValue = false;
                columns[40] = column20;
                DataColumn column21 = new DataColumn("Hardship", typeof(bool));
                column21.AllowDBNull = false;
                column21.DefaultValue = false;
                columns[0x29] = column21;
                DataColumn column22 = new DataColumn("AcceptAssignment", typeof(bool));
                column22.AllowDBNull = false;
                column22.DefaultValue = false;
                columns[0x2a] = column22;
                columns[0x2b] = new DataColumn("DrugNoteField", typeof(string));
                columns[0x2c] = new DataColumn("DrugControlNumber", typeof(string));
                base.Columns.AddRange(columns);
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

            public int? InvoiceID
            {
                get => 
                    this.GetProperty("InvoiceID");
                set => 
                    this.SetProperty("InvoiceID", value);
            }

            public int? CustomerID
            {
                get => 
                    this.GetProperty("CustomerID");
                set => 
                    this.SetProperty("CustomerID", value);
            }
        }
    }
}

