namespace DMEWorks.Details
{
    using Devart.Data.MySql;
    using DMEWorks.Billing;
    using DMEWorks.Core;
    using DMEWorks.Data.MySql;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class ControlOrderDetailsBase : ControlDetails
    {
        private IContainer components;
        private bool F_ShowMissingInformation;
        private DateTime F_DefaultDOSFrom = DateTime.Today;
        private int? _CustomerID = null;
        private int? _OrderID = null;

        public event EventHandler CustomerIDChanged;

        public event EventHandler OrderIDChanged;

        public ControlOrderDetailsBase()
        {
            this.InitializeComponent();
        }

        public void CalculateTotal(ref double TotalBillable, ref double TotalAllowable)
        {
            TotalBillable = 0.0;
            TotalAllowable = 0.0;
            TableOrderDetailsBase table = this.GetTable();
            DataRow[] rowArray = table.Select("", "", DataViewRowState.CurrentRows);
            int upperBound = rowArray.GetUpperBound(0);
            for (int i = rowArray.GetLowerBound(0); i <= upperBound; i++)
            {
                DataRow row = rowArray[i];
                DataRowVersion version = !row.HasVersion(DataRowVersion.Proposed) ? DataRowVersion.Default : DataRowVersion.Proposed;
                double num3 = row.IsNull(table.Col_BillablePrice, version) ? 0.0 : Conversions.ToDouble(row[table.Col_BillablePrice, version]);
                num3 = Math.Round(num3, 2);
                double num4 = row.IsNull(table.Col_AllowablePrice, version) ? 0.0 : Conversions.ToDouble(row[table.Col_AllowablePrice, version]);
                num4 = Math.Round(num4, 2);
                double deliveredConverter = row.IsNull(table.Col_DeliveryConverter, version) ? 1.0 : Conversions.ToDouble(row[table.Col_DeliveryConverter, version]);
                double billedConverter = row.IsNull(table.Col_BilledConverter, version) ? 1.0 : Conversions.ToDouble(row[table.Col_BilledConverter, version]);
                double num7 = 0.0;
                if (!row.IsNull(table.Col_DeliveryQuantity, version))
                {
                    try
                    {
                        num7 = Converter.DeliveredQty2BilledQty(Conversions.ToDouble(row[table.Col_DeliveryQuantity, version]), deliveredConverter, billedConverter);
                    }
                    catch (Exception exception1)
                    {
                        ProjectData.SetProjectError(exception1);
                        ProjectData.ClearProjectError();
                    }
                }
                TotalAllowable += Math.Round((double) (num7 * num4), 2);
                TotalBillable += Math.Round((double) (num7 * num3), 2);
            }
        }

        public void CalculateTotal(double TaxPercent, double DisPercent, ref double SubTotal, ref double DisTotal, ref double TaxTotal)
        {
            DisPercent /= 100.0;
            TaxPercent /= 100.0;
            SubTotal = 0.0;
            TaxTotal = 0.0;
            TableOrderDetailsBase table = this.GetTable();
            DataRow[] rowArray = table.Select("", "", DataViewRowState.CurrentRows);
            int upperBound = rowArray.GetUpperBound(0);
            for (int i = rowArray.GetLowerBound(0); i <= upperBound; i++)
            {
                double num8;
                double num9;
                double num10;
                DataRow row = rowArray[i];
                DataRowVersion version = !row.HasVersion(DataRowVersion.Proposed) ? DataRowVersion.Default : DataRowVersion.Proposed;
                double num3 = row.IsNull(table.Col_BillablePrice, version) ? 0.0 : Conversions.ToDouble(row[table.Col_BillablePrice, version]);
                num3 = Math.Round(num3, 2);
                double num4 = row.IsNull(table.Col_AllowablePrice, version) ? 0.0 : Conversions.ToDouble(row[table.Col_AllowablePrice, version]);
                num4 = Math.Round(num4, 2);
                bool flag = !row.IsNull(table.Col_Taxable, version) && Conversions.ToBoolean(row[table.Col_Taxable, version]);
                double deliveredConverter = row.IsNull(table.Col_DeliveryConverter, version) ? 1.0 : Conversions.ToDouble(row[table.Col_DeliveryConverter, version]);
                double billedConverter = row.IsNull(table.Col_BilledConverter, version) ? 1.0 : Conversions.ToDouble(row[table.Col_BilledConverter, version]);
                double num7 = 0.0;
                if (!row.IsNull(table.Col_DeliveryQuantity, version))
                {
                    try
                    {
                        num7 = Converter.DeliveredQty2BilledQty(Conversions.ToDouble(row[table.Col_DeliveryQuantity, version]), deliveredConverter, billedConverter);
                    }
                    catch (Exception exception1)
                    {
                        ProjectData.SetProjectError(exception1);
                        ProjectData.ClearProjectError();
                    }
                }
                if (flag)
                {
                    num8 = num7 * num4;
                    num9 = Math.Round((double) (num8 * DisPercent), 2);
                    num10 = Math.Round((double) ((num8 - num9) * TaxPercent), 2);
                }
                else
                {
                    num8 = num7 * num3;
                    num9 = Math.Round((double) (num8 * DisPercent), 2);
                    num10 = 0.0;
                }
                SubTotal += num8;
                DisTotal += num9;
                TaxTotal += num10;
            }
        }

        public void ClearGrid_Delivery()
        {
            TableOrderDetailsBase table = this.GetTable();
            base.CloseDialogs();
            table.BeginUpdate();
            try
            {
                table.Clear();
                table.AcceptChanges();
            }
            finally
            {
                table.EndUpdate();
            }
        }

        protected override FormDetails CreateDialog(object param)
        {
            FormOrderDetailBase dialog = new FormOrderDetailBase(this);
            dialog.ShowMissingInformation(this.F_ShowMissingInformation);
            return base.AddDialog(dialog);
        }

        protected override ControlDetails.TableDetails CreateTable() => 
            new TableOrderDetailsBase();

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected static void GenerateDeleteCommand_OrderDetails(MySqlCommand cmd, int OrderID, int CustomerID)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE tbl_orderdetails\r\nFROM tbl_orderdetails\r\n     LEFT JOIN tbl_depositdetails ON tbl_depositdetails.CustomerID     = tbl_orderdetails.CustomerID\r\n                                 AND tbl_depositdetails.OrderID        = tbl_orderdetails.OrderID\r\n                                 AND tbl_depositdetails.OrderDetailsID = tbl_orderdetails.ID\r\nWHERE tbl_orderdetails.CustomerID   = :CustomerID\r\n  AND tbl_orderdetails.OrderID      = :OrderID\r\n  AND tbl_orderdetails.ID           = :ID\r\n  AND tbl_orderdetails.BillingMonth = :BillingMonth\r\n  AND tbl_depositdetails.CustomerID IS NULL\r\n";
            cmd.Parameters.Add("CustomerID", MySqlType.Int).Value = CustomerID;
            cmd.Parameters.Add("OrderID", MySqlType.Int).Value = OrderID;
            cmd.Parameters.Add("ID", MySqlType.Int, 0, "ID").SourceVersion = DataRowVersion.Original;
            cmd.Parameters.Add("BillingMonth", MySqlType.Int, 0, "BillingMonth").SourceVersion = DataRowVersion.Original;
        }

        public static void GenerateInsertCommand_OrderDetails(MySqlCommand cmd, int OrderID, int CustomerID)
        {
            cmd.Parameters.Add("SerialNumber", MySqlType.VarChar, 50, "SerialNumber");
            cmd.Parameters.Add("InventoryItemID", MySqlType.Int, 0, "InventoryItemID");
            cmd.Parameters.Add("PriceCodeID", MySqlType.Int, 0, "PriceCodeID");
            cmd.Parameters.Add("SaleRentType", MySqlType.VarChar, 30, "SaleRentType");
            cmd.Parameters.Add("SerialID", MySqlType.Int, 0, "SerialID");
            cmd.Parameters.Add("WarehouseID", MySqlType.Int, 0, "WarehouseID");
            cmd.Parameters.Add("BillablePrice", MySqlType.Double, 0, "BillablePrice");
            cmd.Parameters.Add("AllowablePrice", MySqlType.Double, 0, "AllowablePrice");
            cmd.Parameters.Add("Taxable", MySqlType.Bit, 0, "Taxable");
            cmd.Parameters.Add("FlatRate", MySqlType.Bit, 0, "FlatRate");
            cmd.Parameters.Add("DOSFrom", MySqlType.Date, 0, "DOSFrom");
            cmd.Parameters.Add("DOSTo", MySqlType.Date, 0, "DOSTo");
            cmd.Parameters.Add("PickupDate", MySqlType.Date, 0, "PickupDate");
            cmd.Parameters.Add("ShowSpanDates", MySqlType.Bit, 0, "ShowSpanDates");
            cmd.Parameters.Add("OrderedQuantity", MySqlType.Double, 8, "OrderedQuantity");
            cmd.Parameters.Add("OrderedUnits", MySqlType.VarChar, 50, "OrderedUnits");
            cmd.Parameters.Add("OrderedWhen", MySqlType.VarChar, 20, "OrderedWhen");
            cmd.Parameters.Add("OrderedConverter", MySqlType.Double, 8, "OrderedConverter");
            cmd.Parameters.Add("BilledQuantity", MySqlType.Double, 8, "BilledQuantity");
            cmd.Parameters.Add("BilledUnits", MySqlType.VarChar, 50, "BilledUnits");
            cmd.Parameters.Add("BilledWhen", MySqlType.VarChar, 20, "BilledWhen");
            cmd.Parameters.Add("BilledConverter", MySqlType.Double, 8, "BilledConverter");
            cmd.Parameters.Add("DeliveryQuantity", MySqlType.Double, 8, "DeliveryQuantity");
            cmd.Parameters.Add("DeliveryUnits", MySqlType.VarChar, 50, "DeliveryUnits");
            cmd.Parameters.Add("DeliveryConverter", MySqlType.Double, 8, "DeliveryConverter");
            cmd.Parameters.Add("BillingCode", MySqlType.VarChar, 50, "BillingCode");
            cmd.Parameters.Add("Modifier1", MySqlType.VarChar, 8, "Modifier1");
            cmd.Parameters.Add("Modifier2", MySqlType.VarChar, 8, "Modifier2");
            cmd.Parameters.Add("Modifier3", MySqlType.VarChar, 8, "Modifier3");
            cmd.Parameters.Add("Modifier4", MySqlType.VarChar, 8, "Modifier4");
            cmd.Parameters.Add("DXPointer", MySqlType.VarChar, 50, "DXPointer");
            cmd.Parameters.Add("DXPointer10", MySqlType.VarChar, 50, "DXPointer10");
            cmd.Parameters.Add("DrugNoteField", MySqlType.VarChar, 20, "DrugNoteField");
            cmd.Parameters.Add("DrugControlNumber", MySqlType.VarChar, 50, "DrugControlNumber");
            cmd.Parameters.Add("BillingMonth", MySqlType.Int, 50, "BillingMonth");
            cmd.Parameters.Add("BillItemOn", MySqlType.VarChar, 50, "BillItemOn");
            cmd.Parameters.Add("AuthorizationNumber", MySqlType.VarChar, 50, "AuthorizationNumber");
            cmd.Parameters.Add("AuthorizationTypeID", MySqlType.Int, 4, "AuthorizationTypeID");
            cmd.Parameters.Add("AuthorizationExpirationDate", MySqlType.Date, 4, "AuthorizationExpirationDate");
            cmd.Parameters.Add("ReasonForPickup", MySqlType.VarChar, 50, "ReasonForPickup");
            cmd.Parameters.Add("SendCMN_RX_w_invoice", MySqlType.Bit, 1, "SendCMN_RX_w_invoice");
            cmd.Parameters.Add("MedicallyUnnecessary", MySqlType.Bit, 1, "MedicallyUnnecessary");
            cmd.Parameters.Add("SpecialCode", MySqlType.VarChar, 50, "SpecialCode");
            cmd.Parameters.Add("ReviewCode", MySqlType.VarChar, 50, "ReviewCode");
            cmd.Parameters.Add("UserField1", MySqlType.VarChar, 100, "UserField1");
            cmd.Parameters.Add("UserField2", MySqlType.VarChar, 100, "UserField2");
            cmd.Parameters.Add("AcceptAssignment", MySqlType.Bit, 1, "AcceptAssignment");
            cmd.Parameters.Add("BillIns1", MySqlType.Bit, 1, "BillIns1");
            cmd.Parameters.Add("BillIns2", MySqlType.Bit, 1, "BillIns2");
            cmd.Parameters.Add("BillIns3", MySqlType.Bit, 1, "BillIns3");
            cmd.Parameters.Add("BillIns4", MySqlType.Bit, 1, "BillIns4");
            cmd.Parameters.Add("NopayIns1", MySqlType.Bit, 1, "NopayIns1");
            cmd.Parameters.Add("State", MySqlType.VarChar, 20, "State");
            cmd.Parameters.Add("EndDate", MySqlType.Date, 8, "EndDate");
            cmd.Parameters.Add("CMNFormID", MySqlType.Int, 4, "CMNFormID");
            cmd.Parameters.Add("HaoDescription", MySqlType.VarChar, 10, "HaoDescription");
            cmd.Parameters.Add("CustomerID", MySqlType.Int).Value = CustomerID;
            cmd.Parameters.Add("OrderID", MySqlType.Int).Value = OrderID;
            cmd.GenerateInsertCommand("tbl_orderdetails");
        }

        protected static void GenerateUpdateCommand_OrderDetails(MySqlCommand cmd, int OrderID, int CustomerID)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE tbl_orderdetails SET\r\n  SerialNumber                = :SerialNumber\r\n, InventoryItemID             = :InventoryItemID\r\n, PriceCodeID                 = :PriceCodeID\r\n, SaleRentType                = :SaleRentType\r\n, SerialID                    = :SerialID\r\n, WarehouseID                 = :WarehouseID\r\n, BillablePrice               = :BillablePrice\r\n, AllowablePrice              = :AllowablePrice\r\n, Taxable                     = :Taxable\r\n, FlatRate                    = :FlatRate\r\n, DOSFrom                     = :DOSFrom\r\n, DOSTo                       = :DOSTo\r\n, PickupDate                  = :PickupDate\r\n, ShowSpanDates               = :ShowSpanDates\r\n, OrderedQuantity             = :OrderedQuantity\r\n, OrderedUnits                = :OrderedUnits\r\n, OrderedWhen                 = :OrderedWhen\r\n, OrderedConverter            = :OrderedConverter\r\n, BilledQuantity              = :BilledQuantity\r\n, BilledUnits                 = :BilledUnits\r\n, BilledWhen                  = :BilledWhen\r\n, BilledConverter             = :BilledConverter\r\n, DeliveryQuantity            = :DeliveryQuantity\r\n, DeliveryUnits               = :DeliveryUnits\r\n, DeliveryConverter           = :DeliveryConverter\r\n, BillingCode                 = :BillingCode\r\n, Modifier1                   = :Modifier1\r\n, Modifier2                   = :Modifier2\r\n, Modifier3                   = :Modifier3\r\n, Modifier4                   = :Modifier4\r\n, DXPointer                  = :DXPointer\r\n, DXPointer10                 = :DXPointer10\r\n, DrugNoteField               = :DrugNoteField\r\n, DrugControlNumber           = :DrugControlNumber\r\n, BillingMonth                = :BillingMonth\r\n, BillItemOn                  = :BillItemOn\r\n, AuthorizationNumber         = :AuthorizationNumber\r\n, AuthorizationTypeID         = :AuthorizationTypeID\r\n, AuthorizationExpirationDate = :AuthorizationExpirationDate\r\n, ReasonForPickup             = :ReasonForPickup\r\n, SendCMN_RX_w_invoice        = :SendCMN_RX_w_invoice\r\n, MedicallyUnnecessary        = :MedicallyUnnecessary\r\n, SpecialCode                 = :SpecialCode\r\n, ReviewCode                  = :ReviewCode\r\n, UserField1                  = :UserField1\r\n, UserField2                  = :UserField2\r\n, AcceptAssignment            = :AcceptAssignment\r\n, BillIns1                    = :BillIns1\r\n, BillIns2                    = :BillIns2\r\n, BillIns3                    = :BillIns3\r\n, BillIns4                    = :BillIns4\r\n, NopayIns1                   = :NopayIns1\r\n, State                       = :State\r\n, EndDate                     = :EndDate\r\n, CMNFormID                   = :CMNFormID\r\n, HaoDescription              = :HaoDescription\r\nWHERE CustomerID   = :W_CustomerID\r\n  AND OrderID      = :W_OrderID\r\n  AND ID           = :W_ID\r\n  AND BillingMonth = :W_BillingMonth\r\n";
            cmd.Parameters.Add("SerialNumber", MySqlType.VarChar, 50, "SerialNumber");
            cmd.Parameters.Add("InventoryItemID", MySqlType.Int, 0, "InventoryItemID");
            cmd.Parameters.Add("PriceCodeID", MySqlType.Int, 0, "PriceCodeID");
            cmd.Parameters.Add("SaleRentType", MySqlType.VarChar, 30, "SaleRentType");
            cmd.Parameters.Add("SerialID", MySqlType.Int, 0, "SerialID");
            cmd.Parameters.Add("WarehouseID", MySqlType.Int, 0, "WarehouseID");
            cmd.Parameters.Add("BillablePrice", MySqlType.Double, 0, "BillablePrice");
            cmd.Parameters.Add("AllowablePrice", MySqlType.Double, 0, "AllowablePrice");
            cmd.Parameters.Add("Taxable", MySqlType.Bit, 0, "Taxable");
            cmd.Parameters.Add("FlatRate", MySqlType.Bit, 0, "FlatRate");
            cmd.Parameters.Add("DOSFrom", MySqlType.Date, 0, "DOSFrom");
            cmd.Parameters.Add("DOSTo", MySqlType.Date, 0, "DOSTo");
            cmd.Parameters.Add("PickupDate", MySqlType.Date, 0, "PickupDate");
            cmd.Parameters.Add("ShowSpanDates", MySqlType.Bit, 0, "ShowSpanDates");
            cmd.Parameters.Add("OrderedQuantity", MySqlType.Double, 0, "OrderedQuantity");
            cmd.Parameters.Add("OrderedUnits", MySqlType.VarChar, 50, "OrderedUnits");
            cmd.Parameters.Add("OrderedWhen", MySqlType.VarChar, 20, "OrderedWhen");
            cmd.Parameters.Add("OrderedConverter", MySqlType.Double, 0, "OrderedConverter");
            cmd.Parameters.Add("BilledQuantity", MySqlType.Double, 0, "BilledQuantity");
            cmd.Parameters.Add("BilledUnits", MySqlType.VarChar, 50, "BilledUnits");
            cmd.Parameters.Add("BilledWhen", MySqlType.VarChar, 20, "BilledWhen");
            cmd.Parameters.Add("BilledConverter", MySqlType.Double, 0, "BilledConverter");
            cmd.Parameters.Add("DeliveryQuantity", MySqlType.Double, 0, "DeliveryQuantity");
            cmd.Parameters.Add("DeliveryUnits", MySqlType.VarChar, 50, "DeliveryUnits");
            cmd.Parameters.Add("DeliveryConverter", MySqlType.Double, 0, "DeliveryConverter");
            cmd.Parameters.Add("BillingCode", MySqlType.VarChar, 50, "BillingCode");
            cmd.Parameters.Add("Modifier1", MySqlType.VarChar, 8, "Modifier1");
            cmd.Parameters.Add("Modifier2", MySqlType.VarChar, 8, "Modifier2");
            cmd.Parameters.Add("Modifier3", MySqlType.VarChar, 8, "Modifier3");
            cmd.Parameters.Add("Modifier4", MySqlType.VarChar, 8, "Modifier4");
            cmd.Parameters.Add("DXPointer", MySqlType.VarChar, 50, "DXPointer");
            cmd.Parameters.Add("DXPointer10", MySqlType.VarChar, 50, "DXPointer10");
            cmd.Parameters.Add("DrugNoteField", MySqlType.VarChar, 20, "DrugNoteField");
            cmd.Parameters.Add("DrugControlNumber", MySqlType.VarChar, 50, "DrugControlNumber");
            cmd.Parameters.Add("BillingMonth", MySqlType.Int, 0, "BillingMonth");
            cmd.Parameters.Add("BillItemOn", MySqlType.VarChar, 50, "BillItemOn");
            cmd.Parameters.Add("AuthorizationNumber", MySqlType.VarChar, 50, "AuthorizationNumber");
            cmd.Parameters.Add("AuthorizationTypeID", MySqlType.Int, 4, "AuthorizationTypeID");
            cmd.Parameters.Add("AuthorizationExpirationDate", MySqlType.Date, 4, "AuthorizationExpirationDate");
            cmd.Parameters.Add("ReasonForPickup", MySqlType.VarChar, 50, "ReasonForPickup");
            cmd.Parameters.Add("SendCMN_RX_w_invoice", MySqlType.Bit, 0, "SendCMN_RX_w_invoice");
            cmd.Parameters.Add("MedicallyUnnecessary", MySqlType.Bit, 0, "MedicallyUnnecessary");
            cmd.Parameters.Add("SpecialCode", MySqlType.VarChar, 50, "SpecialCode");
            cmd.Parameters.Add("ReviewCode", MySqlType.VarChar, 50, "ReviewCode");
            cmd.Parameters.Add("UserField1", MySqlType.VarChar, 100, "UserField1");
            cmd.Parameters.Add("UserField2", MySqlType.VarChar, 100, "UserField2");
            cmd.Parameters.Add("AcceptAssignment", MySqlType.Bit, 0, "AcceptAssignment");
            cmd.Parameters.Add("BillIns1", MySqlType.Bit, 0, "BillIns1");
            cmd.Parameters.Add("BillIns2", MySqlType.Bit, 0, "BillIns2");
            cmd.Parameters.Add("BillIns3", MySqlType.Bit, 0, "BillIns3");
            cmd.Parameters.Add("BillIns4", MySqlType.Bit, 0, "BillIns4");
            cmd.Parameters.Add("NopayIns1", MySqlType.Bit, 0, "NopayIns1");
            cmd.Parameters.Add("State", MySqlType.VarChar, 20, "State");
            cmd.Parameters.Add("EndDate", MySqlType.Date, 0, "EndDate");
            cmd.Parameters.Add("CMNFormID", MySqlType.Int, 0, "CMNFormID");
            cmd.Parameters.Add("HaoDescription", MySqlType.VarChar, 10, "HaoDescription");
            cmd.Parameters.Add("W_CustomerID", MySqlType.Int).Value = CustomerID;
            cmd.Parameters.Add("W_OrderID", MySqlType.Int).Value = OrderID;
            cmd.Parameters.Add("W_ID", MySqlType.Int, 0, "ID").SourceVersion = DataRowVersion.Original;
            cmd.Parameters.Add("W_BillingMonth", MySqlType.Int, 0, "BillingMonth").SourceVersion = DataRowVersion.Original;
        }

        public string[] GetBillingCodes()
        {
            List<string> list = new List<string>();
            foreach (DataRow row in this.GetTable().Select())
            {
                list.Add(Convert.ToString(row["BillingCode"]));
            }
            return list.ToArray();
        }

        protected TableOrderDetailsBase GetTable() => 
            (TableOrderDetailsBase) base.F_TableDetails;

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            base.Name = "ControlOrderDetailsBase";
        }

        protected override void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("ID", "ID", 40);
            Appearance.AddTextColumn("InventoryItemName", "InventoryItem", 100);
            Appearance.AddTextColumn("PriceCodeName", "PriceCode", 100);
            Appearance.AddTextColumn("SaleRentType", "SaleRentType", 80);
        }

        protected DataRow InternalAddNewItem(IDataRecord record, bool RetailSales)
        {
            TableOrderDetailsBase table = this.GetTable();
            DataRow row = table.NewRow();
            row[table.Col_PriceCodeID] = record["PriceCodeID"];
            row[table.Col_PriceCodeName] = record["PriceCodeName"];
            row[table.Col_InventoryItemID] = record["InventoryItemID"];
            row[table.Col_InventoryItemName] = record["InventoryItemName"];
            row[table.Col_SaleRentType] = record["SaleRentType"];
            row[table.Col_SerialID] = DBNull.Value;
            row[table.Col_WarehouseID] = record["WarehouseID"];
            row[table.Col_WarehouseName] = record["WarehouseName"];
            row[table.Col_AllowablePrice] = record["AllowablePrice"];
            row[table.Col_BillablePrice] = record["BillablePrice"];
            row[table.Col_Sale_AllowablePrice] = record["Sale_AllowablePrice"];
            row[table.Col_Sale_BillablePrice] = record["Sale_BillablePrice"];
            row[table.Col_Rent_AllowablePrice] = record["Rent_AllowablePrice"];
            row[table.Col_Rent_BillablePrice] = record["Rent_BillablePrice"];
            row[table.Col_DefaultCMNType] = record["DefaultCMNType"];
            row[table.Col_BillingCode] = record["BillingCode"];
            row[table.Col_Modifier1] = record["Modifier1"];
            row[table.Col_Modifier2] = record["Modifier2"];
            row[table.Col_Modifier3] = record["Modifier3"];
            row[table.Col_Modifier4] = record["Modifier4"];
            row[table.Col_DrugNoteField] = record["DrugNoteField"];
            row[table.Col_DrugControlNumber] = record["DrugControlNumber"];
            row[table.Col_FlatRate] = record["FlatRate"];
            row[table.Col_Taxable] = record["Taxable"];
            row[table.Col_OrderedQuantity] = record["OrderedQuantity"];
            row[table.Col_OrderedUnits] = record["OrderedUnits"];
            row[table.Col_OrderedWhen] = record["OrderedWhen"];
            row[table.Col_OrderedConverter] = record["OrderedConverter"];
            row[table.Col_BilledUnits] = record["BilledUnits"];
            row[table.Col_BilledWhen] = record["BilledWhen"];
            row[table.Col_BilledConverter] = record["BilledConverter"];
            row[table.Col_DeliveryUnits] = record["DeliveryUnits"];
            row[table.Col_DeliveryConverter] = record["DeliveryConverter"];
            row[table.Col_BillItemOn] = record["BillItemOn"];
            row[table.Col_ShowSpanDates] = record["ShowSpanDates"];
            row[table.Col_CMNFormID] = record["CMNFormID"];
            if (RetailSales)
            {
                row[table.Col_BillIns1] = false;
                row[table.Col_BillIns2] = false;
                row[table.Col_BillIns3] = false;
                row[table.Col_BillIns4] = false;
                row[table.Col_AcceptAssignment] = false;
            }
            else
            {
                row[table.Col_BillIns1] = record["BillIns1"];
                row[table.Col_BillIns2] = record["BillIns2"];
                row[table.Col_BillIns3] = record["BillIns3"];
                row[table.Col_BillIns4] = record["BillIns4"];
                row[table.Col_AcceptAssignment] = record["AcceptAssignment"];
            }
            row[table.Col_NopayIns1] = false;
            Actualizer actualizer = new Actualizer(Convert.ToString(row[table.Col_SaleRentType]), Convert.ToString(row[table.Col_BillItemOn]), Convert.ToString(row[table.Col_OrderedWhen]), Convert.ToString(row[table.Col_BilledWhen]));
            DateTime defaultDOSFrom = this.DefaultDOSFrom;
            DateTime toDate = actualizer.ActualDosTo(defaultDOSFrom, defaultDOSFrom.AddMonths(1).AddDays(-1.0));
            row[table.Col_DOSFrom] = defaultDOSFrom;
            row[table.Col_DOSTo] = toDate;
            if (!SaleRentTypeHelper.IsSale(actualizer.ActualSaleRentType))
            {
                try
                {
                    row[table.Col_DeliveryQuantity] = row[table.Col_OrderedQuantity];
                }
                catch (Exception exception3)
                {
                    ProjectData.SetProjectError(exception3);
                    row[table.Col_DeliveryQuantity] = 0;
                    ProjectData.ClearProjectError();
                }
            }
            else
            {
                try
                {
                    row[table.Col_DeliveryQuantity] = Converter.OrderedQty2DeliveredQty(defaultDOSFrom, toDate, Conversions.ToDouble(row[table.Col_OrderedQuantity]), actualizer.ActualOrderedWhen, actualizer.ActualBilledWhen, Conversions.ToDouble(row[table.Col_OrderedConverter]), Conversions.ToDouble(row[table.Col_DeliveryConverter]), Conversions.ToDouble(row[table.Col_BilledConverter]));
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    row[table.Col_DeliveryQuantity] = 0;
                    ProjectData.ClearProjectError();
                }
                try
                {
                    row[table.Col_BilledQuantity] = Converter.OrderedQty2BilledQty(defaultDOSFrom, toDate, Conversions.ToDouble(row[table.Col_OrderedQuantity]), actualizer.ActualOrderedWhen, actualizer.ActualBilledWhen, Conversions.ToDouble(row[table.Col_OrderedConverter]), Conversions.ToDouble(row[table.Col_DeliveryConverter]), Conversions.ToDouble(row[table.Col_BilledConverter]));
                }
                catch (Exception exception2)
                {
                    ProjectData.SetProjectError(exception2);
                    row[table.Col_BilledQuantity] = 0;
                    ProjectData.ClearProjectError();
                }
                goto TR_0000;
            }
            try
            {
                row[table.Col_BilledQuantity] = row[table.Col_OrderedQuantity];
            }
            catch (Exception exception4)
            {
                ProjectData.SetProjectError(exception4);
                row[table.Col_BilledQuantity] = 0;
                ProjectData.ClearProjectError();
            }
        TR_0000:
            row[table.Col_AuthorizationTypeID] = DBNull.Value;
            row[table.Col_SendCMN_RX_w_invoice] = true;
            row[table.Col_MedicallyUnnecessary] = false;
            table.Rows.Add(row);
            return row;
        }

        protected DataRow InternalAddNewItem(int InventoryItemID, int PriceCodeID, int? WarehouseID, int? Quantity, bool RetailSales)
        {
            DataRow row;
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT\r\n  tbl_pricecode.ID   as PriceCodeID\r\n, tbl_pricecode.Name as PriceCodeName\r\n, tbl_inventoryitem.ID   as InventoryItemID\r\n, tbl_inventoryitem.Name as InventoryItemName\r\n, tbl_warehouse.ID   as WarehouseID\r\n, tbl_warehouse.Name as WarehouseName\r\n, IF(pricing.DefaultOrderType = 'Rental'\r\n    ,IF(IFNULL(pricing.RentalType, '') <> '', pricing.RentalType, 'Monthly Rental')\r\n    ,IF(pricing.ReoccuringSale = 0, 'One Time Sale', 'Re-occurring Sale')) as SaleRentType\r\n, IF(pricing.DefaultOrderType = 'Rental', pricing.Rent_AllowablePrice, pricing.Sale_AllowablePrice) as AllowablePrice\r\n, IF(pricing.DefaultOrderType = 'Rental', pricing.Rent_BillablePrice, pricing.Sale_BillablePrice) as BillablePrice\r\n, pricing.Sale_AllowablePrice\r\n, pricing.Rent_AllowablePrice\r\n, pricing.Sale_BillablePrice\r\n, pricing.Rent_BillablePrice\r\n, pricing.DefaultCMNType\r\n, pricing.BillingCode\r\n, pricing.Modifier1\r\n, pricing.Modifier2\r\n, pricing.Modifier3\r\n, pricing.Modifier4\r\n, pricing.DrugNoteField\r\n, pricing.DrugControlNumber\r\n, pricing.FlatRate\r\n, pricing.Taxable\r\n, IFNULL(:W_OrderedQuantity, pricing.OrderedQuantity) as OrderedQuantity\r\n, pricing.OrderedUnits\r\n, pricing.OrderedWhen\r\n, pricing.OrderedConverter\r\n, pricing.BilledUnits\r\n, pricing.BilledWhen\r\n, pricing.BilledConverter\r\n, pricing.DeliveryUnits\r\n, pricing.DeliveryConverter\r\n, pricing.BillItemOn\r\n, pricing.AcceptAssignment\r\n, pricing.ShowSpanDates\r\n, pricing.BillInsurance as BillIns1\r\n, pricing.BillInsurance as BillIns2\r\n, pricing.BillInsurance as BillIns3\r\n, pricing.BillInsurance as BillIns4\r\n, cmn.ID as CMNFormID\r\nFROM tbl_pricecode_item as pricing\r\n     INNER JOIN tbl_pricecode ON pricing.PriceCodeID = tbl_pricecode.ID\r\n     INNER JOIN tbl_inventoryitem ON pricing.InventoryItemID = tbl_inventoryitem.ID\r\n     LEFT JOIN tbl_warehouse ON tbl_warehouse.ID = :W_WarehouseID\r\n     LEFT JOIN tbl_cmnform as cmn\r\n       ON cmn.CustomerID = :W_CustomerID\r\n      AND cmn.CMNType = pricing.DefaultCMNType\r\n      AND cmn.InitialDate           IS NOT NULL\r\n      AND cmn.EstimatedLengthOfNeed IS NOT NULL\r\n      AND (\r\n            (\r\n              99 <= cmn.EstimatedLengthOfNeed\r\n            )\r\n            OR\r\n            (\r\n              cmn.CMNType IN ('DMERC 484.2', 'DME 484.03')\r\n              AND\r\n              :W_DOSFrom < DATE_ADD(IFNULL(cmn.RecertificationDate, cmn.InitialDate), INTERVAL 12 MONTH)\r\n            )\r\n            OR\r\n            (\r\n              cmn.CMNType NOT IN ('DMERC 484.2', 'DME 484.03')\r\n              AND\r\n              :W_DOSFrom < DATE_ADD(cmn.InitialDate, INTERVAL cmn.EstimatedLengthOfNeed MONTH)\r\n            )\r\n          )\r\nWHERE (tbl_inventoryitem.ID = :W_InventoryItemID)\r\n  AND (tbl_pricecode.ID = :W_PriceCodeID)\r\nORDER BY cmn.ID DESC LIMIT 0, 1";
                    command.Parameters.Add("W_CustomerID", MySqlType.Int).Value = NullableConvert.ToDb(this.CustomerID);
                    command.Parameters.Add("W_DOSFrom", MySqlType.Date).Value = this.DefaultDOSFrom;
                    command.Parameters.Add("W_InventoryItemID", MySqlType.Int).Value = InventoryItemID;
                    command.Parameters.Add("W_PriceCodeID", MySqlType.Int).Value = PriceCodeID;
                    command.Parameters.Add("W_WarehouseID", MySqlType.Int).Value = NullableConvert.ToDb(WarehouseID);
                    command.Parameters.Add("W_OrderedQuantity", MySqlType.Int).Value = NullableConvert.ToDb(Quantity);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        row = reader.Read() ? this.InternalAddNewItem(reader, RetailSales) : null;
                    }
                }
            }
            return row;
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

        protected static void RunStoredProcedures(MySqlConnection cnn, MySqlTransaction tran, int OrderID, int CustomerID)
        {
            using (MySqlCommand command = new MySqlCommand("", cnn, tran))
            {
                command.CommandText = $"CALL mir_update_orderdetails({OrderID})";
                command.ExecuteNonQuery();
                command.CommandText = $"CALL inventory_transaction_order_refresh({OrderID})";
                command.ExecuteNonQuery();
                command.CommandText = $"CALL inventory_order_refresh({OrderID})";
                command.ExecuteNonQuery();
                command.CommandText = $"CALL serial_order_refresh({OrderID})";
                command.ExecuteNonQuery();
                command.CommandText = $"CALL Order_ConvertDepositsIntoPayments({OrderID})";
                command.ExecuteNonQuery();
            }
        }

        public void ShowMissingInformation(bool Show)
        {
            _Closure$__21-0 e$__- = new _Closure$__21-0 {
                $VB$Local_Show = Show
            };
            this.F_ShowMissingInformation = e$__-.$VB$Local_Show;
            base.DoForAllDialogs<FormOrderDetail>(new Action<FormOrderDetail>(e$__-._Lambda$__0));
        }

        public void ShowReorderDialog(bool AllItems)
        {
            this.ShowReorderDialog(this.GetTable(), AllItems);
        }

        protected void ShowReorderDialog(TableOrderDetailsBase Details, bool AllItems)
        {
            try
            {
                IEnumerator enumerator;
                DialogReorder.TableGridData details = DialogReorder.FillDetailsTable(Details, AllItems);
                int num = 0;
                try
                {
                    enumerator = details.Rows.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        if (!Convert.ToBoolean(((DataRow) enumerator.Current)[details.Col_Checked]))
                        {
                            continue;
                        }
                        num++;
                    }
                }
                finally
                {
                    if (enumerator is IDisposable)
                    {
                        (enumerator as IDisposable).Dispose();
                    }
                }
                if (num != 0)
                {
                    using (DialogReorder reorder = new DialogReorder())
                    {
                        reorder.InitializeDialog(this.CustomerID, details);
                        reorder.ShowDialog();
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

        public override AllowStateEnum AllowState
        {
            get => 
                base.AllowState;
            set
            {
                base.AllowState = value;
                this.Panel1.Visible = ((value & AllowStateEnum.AllowDelete) == AllowStateEnum.AllowDelete) || ((value & AllowStateEnum.AllowNew) == AllowStateEnum.AllowNew);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool DefaultBillIns1
        {
            get
            {
                object defaultValue = this.GetTable().Col_BillIns1.DefaultValue;
                return ((defaultValue as bool) && ((bool) defaultValue));
            }
            set => 
                this.GetTable().Col_BillIns1.DefaultValue = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool DefaultBillIns2
        {
            get
            {
                object defaultValue = this.GetTable().Col_BillIns2.DefaultValue;
                return ((defaultValue as bool) && ((bool) defaultValue));
            }
            set => 
                this.GetTable().Col_BillIns2.DefaultValue = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool DefaultBillIns3
        {
            get
            {
                object defaultValue = this.GetTable().Col_BillIns3.DefaultValue;
                return ((defaultValue as bool) && ((bool) defaultValue));
            }
            set => 
                this.GetTable().Col_BillIns3.DefaultValue = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool DefaultBillIns4
        {
            get
            {
                object defaultValue = this.GetTable().Col_BillIns4.DefaultValue;
                return ((defaultValue as bool) && ((bool) defaultValue));
            }
            set => 
                this.GetTable().Col_BillIns4.DefaultValue = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string DefaultDXPointer9
        {
            get => 
                (this.GetTable().Col_DXPointer9.DefaultValue as string) ?? "";
            set => 
                this.GetTable().Col_DXPointer9.DefaultValue = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string DefaultDXPointer10
        {
            get => 
                (this.GetTable().Col_DXPointer10.DefaultValue as string) ?? "";
            set => 
                this.GetTable().Col_DXPointer10.DefaultValue = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime DefaultDOSFrom
        {
            get => 
                this.F_DefaultDOSFrom;
            set => 
                this.F_DefaultDOSFrom = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [CompilerGenerated]
        internal sealed class _Closure$__21-0
        {
            public bool $VB$Local_Show;

            internal void _Lambda$__0(FormOrderDetail d)
            {
                d.ShowMissingInformation(this.$VB$Local_Show);
            }
        }

        internal class ColumnNames
        {
            public const string ID = "ID";
            public const string CustomerID = "CustomerID";
            public const string OrderID = "OrderID";
            public const string SerialNumber = "SerialNumber";
            public const string InventoryItemID = "InventoryItemID";
            public const string InventoryItemName = "InventoryItemName";
            public const string PriceCodeID = "PriceCodeID";
            public const string PriceCodeName = "PriceCodeName";
            public const string SaleRentType = "SaleRentType";
            public const string SerialID = "SerialID";
            public const string WarehouseID = "WarehouseID";
            public const string WarehouseName = "WarehouseName";
            public const string BillablePrice = "BillablePrice";
            public const string AllowablePrice = "AllowablePrice";
            public const string Sale_BillablePrice = "Sale_BillablePrice";
            public const string Sale_AllowablePrice = "Sale_AllowablePrice";
            public const string Rent_BillablePrice = "Rent_BillablePrice";
            public const string Rent_AllowablePrice = "Rent_AllowablePrice";
            public const string DefaultCMNType = "DefaultCMNType";
            public const string Amount = "Amount";
            public const string Taxable = "Taxable";
            public const string FlatRate = "FlatRate";
            public const string DOSFrom = "DOSFrom";
            public const string DOSTo = "DOSTo";
            public const string PickupDate = "PickupDate";
            public const string ShowSpanDates = "ShowSpanDates";
            public const string OrderedQuantity = "OrderedQuantity";
            public const string OrderedUnits = "OrderedUnits";
            public const string OrderedWhen = "OrderedWhen";
            public const string OrderedConverter = "OrderedConverter";
            public const string BilledQuantity = "BilledQuantity";
            public const string BilledUnits = "BilledUnits";
            public const string BilledWhen = "BilledWhen";
            public const string BilledConverter = "BilledConverter";
            public const string DeliveryQuantity = "DeliveryQuantity";
            public const string DeliveryUnits = "DeliveryUnits";
            public const string DeliveryConverter = "DeliveryConverter";
            public const string BillingCode = "BillingCode";
            public const string Modifier1 = "Modifier1";
            public const string Modifier2 = "Modifier2";
            public const string Modifier3 = "Modifier3";
            public const string Modifier4 = "Modifier4";
            public const string DXPointer9 = "DXPointer";
            public const string DXPointer10 = "DXPointer10";
            public const string DrugNoteField = "DrugNoteField";
            public const string DrugControlNumber = "DrugControlNumber";
            public const string CMNFormID = "CMNFormID";
            public const string HaoDescription = "HaoDescription";
            public const string BillingMonth = "BillingMonth";
            public const string BillItemOn = "BillItemOn";
            public const string AuthorizationNumber = "AuthorizationNumber";
            public const string AuthorizationTypeID = "AuthorizationTypeID";
            public const string AuthorizationExpirationDate = "AuthorizationExpirationDate";
            public const string ReasonForPickup = "ReasonForPickup";
            public const string SendCMN_RX_w_invoice = "SendCMN_RX_w_invoice";
            public const string MedicallyUnnecessary = "MedicallyUnnecessary";
            public const string SpecialCode = "SpecialCode";
            public const string ReviewCode = "ReviewCode";
            public const string UserField1 = "UserField1";
            public const string UserField2 = "UserField2";
            public const string State = "State";
            public const string Returned = "Returned";
            public const string AcceptAssignment = "AcceptAssignment";
            public const string BillIns1 = "BillIns1";
            public const string BillIns2 = "BillIns2";
            public const string BillIns3 = "BillIns3";
            public const string BillIns4 = "BillIns4";
            public const string NopayIns1 = "NopayIns1";
            public const string EndDate = "EndDate";
            public const string MIR = "MIR";
        }

        public class TableOrderDetailsBase : ControlDetails.TableDetails
        {
            public DataColumn Col_ID;
            public DataColumn Col_SerialNumber;
            public DataColumn Col_InventoryItemID;
            public DataColumn Col_InventoryItemName;
            public DataColumn Col_PriceCodeID;
            public DataColumn Col_PriceCodeName;
            public DataColumn Col_SaleRentType;
            public DataColumn Col_SerialID;
            public DataColumn Col_WarehouseID;
            public DataColumn Col_WarehouseName;
            public DataColumn Col_BillablePrice;
            public DataColumn Col_AllowablePrice;
            public DataColumn Col_Sale_BillablePrice;
            public DataColumn Col_Sale_AllowablePrice;
            public DataColumn Col_Rent_BillablePrice;
            public DataColumn Col_Rent_AllowablePrice;
            public DataColumn Col_DefaultCMNType;
            public DataColumn Col_Taxable;
            public DataColumn Col_FlatRate;
            public DataColumn Col_DOSFrom;
            public DataColumn Col_DOSTo;
            public DataColumn Col_PickupDate;
            public DataColumn Col_ShowSpanDates;
            public DataColumn Col_OrderedQuantity;
            public DataColumn Col_OrderedUnits;
            public DataColumn Col_OrderedWhen;
            public DataColumn Col_OrderedConverter;
            public DataColumn Col_BilledQuantity;
            public DataColumn Col_BilledUnits;
            public DataColumn Col_BilledWhen;
            public DataColumn Col_BilledConverter;
            public DataColumn Col_DeliveryQuantity;
            public DataColumn Col_DeliveryUnits;
            public DataColumn Col_DeliveryConverter;
            public DataColumn Col_BillingCode;
            public DataColumn Col_Modifier1;
            public DataColumn Col_Modifier2;
            public DataColumn Col_Modifier3;
            public DataColumn Col_Modifier4;
            public DataColumn Col_DXPointer9;
            public DataColumn Col_DXPointer10;
            public DataColumn Col_DrugNoteField;
            public DataColumn Col_DrugControlNumber;
            public DataColumn Col_CMNFormID;
            public DataColumn Col_HaoDescription;
            public DataColumn Col_BillingMonth;
            public DataColumn Col_BillItemOn;
            public DataColumn Col_AuthorizationNumber;
            public DataColumn Col_AuthorizationTypeID;
            public DataColumn Col_AuthorizationExpirationDate;
            public DataColumn Col_ReasonForPickup;
            public DataColumn Col_SendCMN_RX_w_invoice;
            public DataColumn Col_MedicallyUnnecessary;
            public DataColumn Col_SpecialCode;
            public DataColumn Col_ReviewCode;
            public DataColumn Col_State;
            public DataColumn Col_Returned;
            public DataColumn Col_BillIns1;
            public DataColumn Col_BillIns2;
            public DataColumn Col_BillIns3;
            public DataColumn Col_BillIns4;
            public DataColumn Col_NopayIns1;
            public DataColumn Col_EndDate;
            public DataColumn Col_MIR;
            public DataColumn Col_AcceptAssignment;
            public DataColumn Col_UserField1;
            public DataColumn Col_UserField2;

            public TableOrderDetailsBase() : this("tbl_orderdetailsbase")
            {
            }

            public TableOrderDetailsBase(string TableName) : base(TableName)
            {
            }

            protected override void Initialize()
            {
                base.Initialize();
                this.Col_ID = base.Columns["ID"];
                this.Col_SerialNumber = base.Columns["SerialNumber"];
                this.Col_InventoryItemID = base.Columns["InventoryItemID"];
                this.Col_InventoryItemName = base.Columns["InventoryItemName"];
                this.Col_PriceCodeID = base.Columns["PriceCodeID"];
                this.Col_PriceCodeName = base.Columns["PriceCodeName"];
                this.Col_SaleRentType = base.Columns["SaleRentType"];
                this.Col_SerialID = base.Columns["SerialID"];
                this.Col_WarehouseID = base.Columns["WarehouseID"];
                this.Col_WarehouseName = base.Columns["WarehouseName"];
                this.Col_BillablePrice = base.Columns["BillablePrice"];
                this.Col_AllowablePrice = base.Columns["AllowablePrice"];
                this.Col_Sale_BillablePrice = base.Columns["Sale_BillablePrice"];
                this.Col_Sale_AllowablePrice = base.Columns["Sale_AllowablePrice"];
                this.Col_Rent_BillablePrice = base.Columns["Rent_BillablePrice"];
                this.Col_Rent_AllowablePrice = base.Columns["Rent_AllowablePrice"];
                this.Col_DefaultCMNType = base.Columns["DefaultCMNType"];
                this.Col_Taxable = base.Columns["Taxable"];
                this.Col_FlatRate = base.Columns["FlatRate"];
                this.Col_DOSFrom = base.Columns["DOSFrom"];
                this.Col_DOSTo = base.Columns["DOSTo"];
                this.Col_PickupDate = base.Columns["PickupDate"];
                this.Col_ShowSpanDates = base.Columns["ShowSpanDates"];
                this.Col_OrderedQuantity = base.Columns["OrderedQuantity"];
                this.Col_OrderedUnits = base.Columns["OrderedUnits"];
                this.Col_OrderedWhen = base.Columns["OrderedWhen"];
                this.Col_OrderedConverter = base.Columns["OrderedConverter"];
                this.Col_BilledQuantity = base.Columns["BilledQuantity"];
                this.Col_BilledUnits = base.Columns["BilledUnits"];
                this.Col_BilledWhen = base.Columns["BilledWhen"];
                this.Col_BilledConverter = base.Columns["BilledConverter"];
                this.Col_DeliveryQuantity = base.Columns["DeliveryQuantity"];
                this.Col_DeliveryUnits = base.Columns["DeliveryUnits"];
                this.Col_DeliveryConverter = base.Columns["DeliveryConverter"];
                this.Col_BillingCode = base.Columns["BillingCode"];
                this.Col_Modifier1 = base.Columns["Modifier1"];
                this.Col_Modifier2 = base.Columns["Modifier2"];
                this.Col_Modifier3 = base.Columns["Modifier3"];
                this.Col_Modifier4 = base.Columns["Modifier4"];
                this.Col_DXPointer9 = base.Columns["DXPointer"];
                this.Col_DXPointer10 = base.Columns["DXPointer10"];
                this.Col_DrugNoteField = base.Columns["DrugNoteField"];
                this.Col_DrugControlNumber = base.Columns["DrugControlNumber"];
                this.Col_CMNFormID = base.Columns["CMNFormID"];
                this.Col_HaoDescription = base.Columns["HaoDescription"];
                this.Col_BillingMonth = base.Columns["BillingMonth"];
                this.Col_BillItemOn = base.Columns["BillItemOn"];
                this.Col_AuthorizationNumber = base.Columns["AuthorizationNumber"];
                this.Col_AuthorizationTypeID = base.Columns["AuthorizationTypeID"];
                this.Col_AuthorizationExpirationDate = base.Columns["AuthorizationExpirationDate"];
                this.Col_ReasonForPickup = base.Columns["ReasonForPickup"];
                this.Col_SendCMN_RX_w_invoice = base.Columns["SendCMN_RX_w_invoice"];
                this.Col_MedicallyUnnecessary = base.Columns["MedicallyUnnecessary"];
                this.Col_SpecialCode = base.Columns["SpecialCode"];
                this.Col_ReviewCode = base.Columns["ReviewCode"];
                this.Col_UserField1 = base.Columns["UserField1"];
                this.Col_UserField2 = base.Columns["UserField2"];
                this.Col_State = base.Columns["State"];
                this.Col_Returned = base.Columns["Returned"];
                this.Col_AcceptAssignment = base.Columns["AcceptAssignment"];
                this.Col_BillIns1 = base.Columns["BillIns1"];
                this.Col_BillIns2 = base.Columns["BillIns2"];
                this.Col_BillIns3 = base.Columns["BillIns3"];
                this.Col_BillIns4 = base.Columns["BillIns4"];
                this.Col_NopayIns1 = base.Columns["NopayIns1"];
                this.Col_EndDate = base.Columns["EndDate"];
                this.Col_MIR = base.Columns["MIR"];
            }

            protected override void InitializeClass()
            {
                base.InitializeClass();
                this.Col_ID = base.Columns.Add("ID", typeof(int));
                this.Col_SerialNumber = base.Columns.Add("SerialNumber", typeof(string));
                this.Col_InventoryItemID = base.Columns.Add("InventoryItemID", typeof(int));
                this.Col_InventoryItemID.AllowDBNull = false;
                this.Col_InventoryItemName = base.Columns.Add("InventoryItemName", typeof(string));
                this.Col_PriceCodeID = base.Columns.Add("PriceCodeID", typeof(int));
                this.Col_PriceCodeID.AllowDBNull = false;
                this.Col_PriceCodeName = base.Columns.Add("PriceCodeName", typeof(string));
                this.Col_SaleRentType = base.Columns.Add("SaleRentType", typeof(string));
                this.Col_SaleRentType.AllowDBNull = false;
                this.Col_SerialID = base.Columns.Add("SerialID", typeof(int));
                this.Col_WarehouseID = base.Columns.Add("WarehouseID", typeof(int));
                this.Col_WarehouseName = base.Columns.Add("WarehouseName", typeof(string));
                this.Col_BillablePrice = base.Columns.Add("BillablePrice", typeof(double));
                this.Col_BillablePrice.AllowDBNull = false;
                this.Col_AllowablePrice = base.Columns.Add("AllowablePrice", typeof(double));
                this.Col_AllowablePrice.AllowDBNull = false;
                this.Col_Sale_BillablePrice = base.Columns.Add("Sale_BillablePrice", typeof(double));
                this.Col_Sale_BillablePrice.AllowDBNull = false;
                this.Col_Sale_BillablePrice.DefaultValue = 0.0;
                this.Col_Sale_AllowablePrice = base.Columns.Add("Sale_AllowablePrice", typeof(double));
                this.Col_Sale_AllowablePrice.AllowDBNull = false;
                this.Col_Sale_AllowablePrice.DefaultValue = 0.0;
                this.Col_Rent_BillablePrice = base.Columns.Add("Rent_BillablePrice", typeof(double));
                this.Col_Rent_BillablePrice.AllowDBNull = false;
                this.Col_Rent_BillablePrice.DefaultValue = 0.0;
                this.Col_Rent_AllowablePrice = base.Columns.Add("Rent_AllowablePrice", typeof(double));
                this.Col_Rent_AllowablePrice.AllowDBNull = false;
                this.Col_Rent_AllowablePrice.DefaultValue = 0.0;
                this.Col_DefaultCMNType = base.Columns.Add("DefaultCMNType", typeof(string));
                this.Col_Taxable = base.Columns.Add("Taxable", typeof(bool));
                this.Col_FlatRate = base.Columns.Add("FlatRate", typeof(bool));
                this.Col_DOSFrom = base.Columns.Add("DOSFrom", typeof(DateTime));
                this.Col_DOSFrom.AllowDBNull = false;
                this.Col_DOSTo = base.Columns.Add("DOSTo", typeof(DateTime));
                this.Col_PickupDate = base.Columns.Add("PickupDate", typeof(DateTime));
                this.Col_ShowSpanDates = base.Columns.Add("ShowSpanDates", typeof(bool));
                this.Col_ShowSpanDates.AllowDBNull = false;
                this.Col_ShowSpanDates.DefaultValue = false;
                this.Col_OrderedQuantity = base.Columns.Add("OrderedQuantity", typeof(double));
                this.Col_OrderedQuantity.AllowDBNull = false;
                this.Col_OrderedQuantity.DefaultValue = 0.0;
                this.Col_OrderedUnits = base.Columns.Add("OrderedUnits", typeof(string));
                this.Col_OrderedWhen = base.Columns.Add("OrderedWhen", typeof(string));
                this.Col_OrderedConverter = base.Columns.Add("OrderedConverter", typeof(double));
                this.Col_OrderedConverter.AllowDBNull = false;
                this.Col_BilledQuantity = base.Columns.Add("BilledQuantity", typeof(double));
                this.Col_BilledQuantity.AllowDBNull = false;
                this.Col_BilledUnits = base.Columns.Add("BilledUnits", typeof(string));
                this.Col_BilledWhen = base.Columns.Add("BilledWhen", typeof(string));
                this.Col_BilledConverter = base.Columns.Add("BilledConverter", typeof(double));
                this.Col_BilledConverter.AllowDBNull = false;
                this.Col_DeliveryQuantity = base.Columns.Add("DeliveryQuantity", typeof(double));
                this.Col_DeliveryQuantity.AllowDBNull = false;
                this.Col_DeliveryUnits = base.Columns.Add("DeliveryUnits", typeof(string));
                this.Col_DeliveryConverter = base.Columns.Add("DeliveryConverter", typeof(double));
                this.Col_DeliveryConverter.AllowDBNull = false;
                this.Col_BillingCode = base.Columns.Add("BillingCode", typeof(string));
                this.Col_Modifier1 = base.Columns.Add("Modifier1", typeof(string));
                this.Col_Modifier1.AllowDBNull = false;
                this.Col_Modifier1.DefaultValue = "";
                this.Col_Modifier2 = base.Columns.Add("Modifier2", typeof(string));
                this.Col_Modifier2.AllowDBNull = false;
                this.Col_Modifier2.DefaultValue = "";
                this.Col_Modifier3 = base.Columns.Add("Modifier3", typeof(string));
                this.Col_Modifier3.AllowDBNull = false;
                this.Col_Modifier3.DefaultValue = "";
                this.Col_Modifier4 = base.Columns.Add("Modifier4", typeof(string));
                this.Col_Modifier4.AllowDBNull = false;
                this.Col_Modifier4.DefaultValue = "";
                this.Col_DXPointer9 = base.Columns.Add("DXPointer", typeof(string));
                this.Col_DXPointer10 = base.Columns.Add("DXPointer10", typeof(string));
                this.Col_DrugNoteField = base.Columns.Add("DrugNoteField", typeof(string));
                this.Col_DrugControlNumber = base.Columns.Add("DrugControlNumber", typeof(string));
                this.Col_CMNFormID = base.Columns.Add("CMNFormID", typeof(int));
                this.Col_HaoDescription = base.Columns.Add("HaoDescription", typeof(string));
                this.Col_BillingMonth = base.Columns.Add("BillingMonth", typeof(int));
                this.Col_BillingMonth.AllowDBNull = false;
                this.Col_BillingMonth.DefaultValue = 1;
                this.Col_BillItemOn = base.Columns.Add("BillItemOn", typeof(string));
                this.Col_AuthorizationNumber = base.Columns.Add("AuthorizationNumber", typeof(string));
                this.Col_AuthorizationTypeID = base.Columns.Add("AuthorizationTypeID", typeof(int));
                this.Col_AuthorizationExpirationDate = base.Columns.Add("AuthorizationExpirationDate", typeof(DateTime));
                this.Col_ReasonForPickup = base.Columns.Add("ReasonForPickup", typeof(string));
                this.Col_SendCMN_RX_w_invoice = base.Columns.Add("SendCMN_RX_w_invoice", typeof(bool));
                this.Col_SendCMN_RX_w_invoice.AllowDBNull = false;
                this.Col_MedicallyUnnecessary = base.Columns.Add("MedicallyUnnecessary", typeof(bool));
                this.Col_MedicallyUnnecessary.AllowDBNull = false;
                this.Col_SpecialCode = base.Columns.Add("SpecialCode", typeof(string));
                this.Col_ReviewCode = base.Columns.Add("ReviewCode", typeof(string));
                this.Col_UserField1 = base.Columns.Add("UserField1", typeof(string));
                this.Col_UserField1.AllowDBNull = false;
                this.Col_UserField1.DefaultValue = "";
                this.Col_UserField1.MaxLength = 100;
                this.Col_UserField2 = base.Columns.Add("UserField2", typeof(string));
                this.Col_UserField2.AllowDBNull = false;
                this.Col_UserField2.DefaultValue = "";
                this.Col_UserField2.MaxLength = 100;
                this.Col_State = base.Columns.Add("State", typeof(string));
                this.Col_State.AllowDBNull = false;
                this.Col_State.DefaultValue = "New";
                this.Col_Returned = base.Columns.Add("Returned", typeof(bool));
                this.Col_Returned.AllowDBNull = false;
                this.Col_Returned.DefaultValue = false;
                this.Col_AcceptAssignment = base.Columns.Add("AcceptAssignment", typeof(bool));
                this.Col_AcceptAssignment.AllowDBNull = false;
                this.Col_AcceptAssignment.DefaultValue = false;
                this.Col_BillIns1 = base.Columns.Add("BillIns1", typeof(bool));
                this.Col_BillIns1.AllowDBNull = false;
                this.Col_BillIns1.DefaultValue = false;
                this.Col_BillIns2 = base.Columns.Add("BillIns2", typeof(bool));
                this.Col_BillIns2.AllowDBNull = false;
                this.Col_BillIns2.DefaultValue = false;
                this.Col_BillIns3 = base.Columns.Add("BillIns3", typeof(bool));
                this.Col_BillIns3.AllowDBNull = false;
                this.Col_BillIns3.DefaultValue = false;
                this.Col_BillIns4 = base.Columns.Add("BillIns4", typeof(bool));
                this.Col_BillIns4.AllowDBNull = false;
                this.Col_BillIns4.DefaultValue = false;
                this.Col_NopayIns1 = base.Columns.Add("NopayIns1", typeof(bool));
                this.Col_NopayIns1.AllowDBNull = false;
                this.Col_NopayIns1.DefaultValue = false;
                this.Col_EndDate = base.Columns.Add("EndDate", typeof(DateTime));
                this.Col_MIR = base.Columns.Add("MIR", typeof(string));
            }
        }
    }
}

