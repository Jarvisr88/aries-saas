# Migration Readiness Report

Generated at: 20241212_135649

## Critical Issues

- [CRITICAL] Found 1 zero dates in tbl_customer.DateofBirth
- [CRITICAL] Found 1 zero dates in tbl_customer.SignatureOnFile
- [CRITICAL] Found 2 zero dates in tbl_customer.SetupDate
- [CRITICAL] Found 911 zero dates in tbl_doctor.LicenseExpired
- [CRITICAL] Found 69 zero dates in tbl_doctor.LastUpdateDatetime
- [CRITICAL] Found 4 zero dates in tbl_referral.LastContacted

## Warnings

- [WARNING] Non-deterministic routine: customer_insurance_fixrank
- [WARNING] Non-deterministic routine: fixInvoicePolicies
- [WARNING] Non-deterministic routine: fixOrderPolicies
- [WARNING] Non-deterministic routine: fix_serial_transactions
- [WARNING] Non-deterministic routine: internal_inventory_transfer
- [WARNING] Non-deterministic routine: InventoryItem_Clone
- [WARNING] Non-deterministic routine: inventory_adjust_2
- [WARNING] Non-deterministic routine: inventory_order_refresh
- [WARNING] Non-deterministic routine: inventory_po_refresh
- [WARNING] Non-deterministic routine: inventory_refresh
- [WARNING] Non-deterministic routine: inventory_transaction_add_adjustment
- [WARNING] Non-deterministic routine: inventory_transaction_order_cleanup
- [WARNING] Non-deterministic routine: inventory_transaction_order_refresh
- [WARNING] Non-deterministic routine: inventory_transaction_po_refresh
- [WARNING] Non-deterministic routine: inventory_transfer
- [WARNING] Non-deterministic routine: InvoiceDetails_AddAutoSubmit
- [WARNING] Non-deterministic routine: InvoiceDetails_AddPayment
- [WARNING] Non-deterministic routine: InvoiceDetails_AddSubmitted
- [WARNING] Non-deterministic routine: InvoiceDetails_InternalAddAutoSubmit
- [WARNING] Non-deterministic routine: InvoiceDetails_InternalAddSubmitted
- [WARNING] Non-deterministic routine: InvoiceDetails_InternalReflag
- [WARNING] Non-deterministic routine: InvoiceDetails_InternalWriteoffBalance
- [WARNING] Non-deterministic routine: InvoiceDetails_RecalculateInternals
- [WARNING] Non-deterministic routine: InvoiceDetails_RecalculateInternals_Single
- [WARNING] Non-deterministic routine: InvoiceDetails_Reflag
- [WARNING] Non-deterministic routine: InvoiceDetails_WriteoffBalance
- [WARNING] Non-deterministic routine: Invoice_AddAutoSubmit
- [WARNING] Non-deterministic routine: Invoice_AddSubmitted
- [WARNING] Non-deterministic routine: Invoice_InternalReflag
- [WARNING] Non-deterministic routine: Invoice_InternalUpdateBalance
- [WARNING] Non-deterministic routine: Invoice_InternalUpdatePendingSubmissions
- [WARNING] Non-deterministic routine: Invoice_Reflag
- [WARNING] Non-deterministic routine: Invoice_UpdateBalance
- [WARNING] Non-deterministic routine: Invoice_UpdatePendingSubmissions
- [WARNING] Non-deterministic routine: mir_update
- [WARNING] Non-deterministic routine: mir_update_cmnform
- [WARNING] Non-deterministic routine: mir_update_customer
- [WARNING] Non-deterministic routine: mir_update_customer_insurance
- [WARNING] Non-deterministic routine: mir_update_doctor
- [WARNING] Non-deterministic routine: mir_update_facility
- [WARNING] Non-deterministic routine: mir_update_insurancecompany
- [WARNING] Non-deterministic routine: mir_update_order
- [WARNING] Non-deterministic routine: mir_update_orderdetails
- [WARNING] Non-deterministic routine: move_serial_on_hand
- [WARNING] Non-deterministic routine: Order_ConvertDepositsIntoPayments
- [WARNING] Non-deterministic routine: Order_InternalProcess
- [WARNING] Non-deterministic routine: Order_InternalUpdateBalance
- [WARNING] Non-deterministic routine: order_process
- [WARNING] Non-deterministic routine: order_process_2
- [WARNING] Non-deterministic routine: order_update_dos
- [WARNING] Non-deterministic routine: process_reoccuring_order
- [WARNING] Non-deterministic routine: process_reoccuring_purchaseorder
- [WARNING] Non-deterministic routine: PurchaseOrder_UpdateTotals
- [WARNING] Non-deterministic routine: retailinvoice_addpayments
- [WARNING] Non-deterministic routine: serials_fix
- [WARNING] Non-deterministic routine: serials_po_refresh
- [WARNING] Non-deterministic routine: serials_refresh
- [WARNING] Non-deterministic routine: serial_add_transaction
- [WARNING] Non-deterministic routine: serial_order_refresh
- [WARNING] Non-deterministic routine: serial_transfer
- [WARNING] Trigger tbl_invoice_transaction_beforeinsert uses row-level references

