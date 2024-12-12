using System;
using System.Data;

public class BatchPayment_PaymentTable : DataTable
{
    public readonly DataColumn Col_ID;
    public readonly DataColumn Col_InvoiceDetailsID;
    public readonly DataColumn Col_InvoiceID;
    public readonly DataColumn Col_CustomerID;
    public readonly DataColumn Col_CustomerName;
    public readonly DataColumn Col_InventoryItemName;
    public readonly DataColumn Col_BillableAmount;
    public readonly DataColumn Col_AllowableAmount;
    public readonly DataColumn Col_TotalPaid;
    public readonly DataColumn Col_Rank;
    public readonly DataColumn Col_Basis;
    public readonly DataColumn Col_Percent;
    public readonly DataColumn Col_Approved;
    public readonly DataColumn Col_Amount;
    public readonly DataColumn Col_AmountLeft;

    public BatchPayment_PaymentTable()
    {
        base.TableName = "tbl_invoice_payment";
        this.Col_ID = base.Columns.Add("ID", typeof(int));
        this.Col_ID.AllowDBNull = true;
        this.Col_InvoiceDetailsID = base.Columns.Add("InvoiceDetailsID", typeof(int));
        this.Col_InvoiceDetailsID.AllowDBNull = false;
        this.Col_InvoiceID = base.Columns.Add("InvoiceID", typeof(int));
        this.Col_InvoiceID.AllowDBNull = false;
        this.Col_CustomerID = base.Columns.Add("CustomerID", typeof(int));
        this.Col_CustomerID.AllowDBNull = false;
        this.Col_CustomerName = base.Columns.Add("CustomerName", typeof(string));
        this.Col_CustomerName.AllowDBNull = false;
        this.Col_InventoryItemName = base.Columns.Add("InventroryItemName", typeof(string));
        this.Col_InventoryItemName.AllowDBNull = false;
        this.Col_BillableAmount = base.Columns.Add("BillableAmount", typeof(double));
        this.Col_BillableAmount.AllowDBNull = false;
        this.Col_AllowableAmount = base.Columns.Add("AllowableAmount", typeof(double));
        this.Col_AllowableAmount.AllowDBNull = false;
        this.Col_TotalPaid = base.Columns.Add("TotalPaid", typeof(double));
        this.Col_TotalPaid.AllowDBNull = false;
        this.Col_Rank = base.Columns.Add("Rank", typeof(int));
        this.Col_Rank.AllowDBNull = false;
        this.Col_Basis = base.Columns.Add("Basis", typeof(string));
        this.Col_Basis.AllowDBNull = false;
        this.Col_Percent = base.Columns.Add("Percent", typeof(double));
        this.Col_Percent.AllowDBNull = false;
        this.Col_Approved = base.Columns.Add("Approved", typeof(bool));
        this.Col_Approved.AllowDBNull = false;
        this.Col_Amount = base.Columns.Add("CheckAmount", typeof(double));
        this.Col_Amount.AllowDBNull = true;
        this.Col_AmountLeft = base.Columns.Add("AmountLeft", typeof(double));
        this.Col_AmountLeft.AllowDBNull = true;
        base.PrimaryKey = new DataColumn[] { this.Col_InvoiceDetailsID };
    }
}

