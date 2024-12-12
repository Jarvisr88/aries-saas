namespace DMEWorks.Core
{
    using DMEWorks;
    using System;

    public class Permissions
    {
        public static PermissionsStruct GetPermission(string Name) => 
            Globals.GetUserPermissions(Name).GetValueOrDefault(PermissionsStruct.Empty);

        public static PermissionsStruct AdjustInventory =>
            GetPermission("AdjustInventory");

        public static PermissionsStruct FormAuthorizationType =>
            GetPermission("FormAuthorizationType");

        public static PermissionsStruct FormBatchPayments =>
            GetPermission("FormBatchPayments");

        public static PermissionsStruct FormBillingType =>
            GetPermission("FormBillingType");

        public static PermissionsStruct FormCallBack =>
            GetPermission("FormCallBack");

        public static PermissionsStruct FormCMNRX =>
            GetPermission("FormCMNRX");

        public static PermissionsStruct FormCompany =>
            GetPermission("FormCompany");

        public static PermissionsStruct FormCompliance =>
            GetPermission("FormCompliance");

        public static PermissionsStruct FormCompliancePopup =>
            GetPermission("FormCompliancePopup");

        public static PermissionsStruct FormCompliancePopup_Unrestricted =>
            GetPermission("FormCompliancePopup.Unrestricted");

        public static PermissionsStruct FormCrystalReport =>
            GetPermission("FormCrystalReport");

        public static PermissionsStruct FormCustomer =>
            GetPermission("FormCustomer");

        public static PermissionsStruct FormCustomerClass =>
            GetPermission("FormCustomerClass");

        public static PermissionsStruct FormCustomerInsurance =>
            GetPermission("FormCustomerInsurance");

        public static PermissionsStruct FormCustomerNotes =>
            GetPermission("FormCustomerNotes");

        public static PermissionsStruct FormCustomerType =>
            GetPermission("FormCustomerType");

        public static PermissionsStruct FormDenial =>
            GetPermission("FormDenial");

        public static PermissionsStruct FormDoctor =>
            GetPermission("FormDoctor");

        public static PermissionsStruct FormDoctorType =>
            GetPermission("FormDoctorType");

        public static PermissionsStruct FormEligibility =>
            GetPermission("FormEligibility");

        public static PermissionsStruct FormFacility =>
            GetPermission("FormFacility");

        public static PermissionsStruct FormHAO =>
            GetPermission("FormHAO");

        public static PermissionsStruct FormICD10 =>
            GetPermission("FormICD10");

        public static PermissionsStruct FormICD9 =>
            GetPermission("FormICD9");

        public static PermissionsStruct FormImage =>
            GetPermission("FormImage");

        public static PermissionsStruct FormImageSearch =>
            GetPermission("FormImageSearch");

        public static PermissionsStruct FormInsuranceCompany =>
            GetPermission("FormInsuranceCompany");

        public static PermissionsStruct FormInsuranceCompanyGroup =>
            GetPermission("FormInsuranceCompanyGroup");

        public static PermissionsStruct FormInsuranceType =>
            GetPermission("FormInsuranceType");

        public static PermissionsStruct FormInventory =>
            GetPermission("FormInventory");

        public static PermissionsStruct FormInventoryItem =>
            GetPermission("FormInventoryItem");

        public static PermissionsStruct FormInventoryItem_Commission =>
            GetPermission("FormInventoryItem.Commission");

        public static PermissionsStruct FormInvoice =>
            GetPermission("FormInvoice");

        public static PermissionsStruct FormInvoice_Approved =>
            GetPermission("FormInvoice.Approved");

        public static PermissionsStruct FormInvoiceForm =>
            GetPermission("FormInvoiceForm");

        public static PermissionsStruct FormKit =>
            GetPermission("FormKit");

        public static PermissionsStruct FormLegalRep =>
            GetPermission("FormLegalRep");

        public static PermissionsStruct FormLocation =>
            GetPermission("FormLocation");

        public static PermissionsStruct FormManualPayments =>
            GetPermission("FormManualPayments");

        public static PermissionsStruct FormManufacturer =>
            GetPermission("FormManufacturer");

        public static PermissionsStruct FormMedexETL =>
            GetPermission("FormMedexETL");

        public static PermissionsStruct FormMedicalConditions =>
            GetPermission("FormMedicalConditions");

        public static PermissionsStruct FormMissingInformation =>
            GetPermission("FormMissingInformation");

        public static PermissionsStruct FormOrder =>
            GetPermission("FormOrder");

        public static PermissionsStruct FormOrder_Approved =>
            GetPermission("FormOrder.Approved");

        public static PermissionsStruct FormOrder_ChangeBillingMonth =>
            GetPermission("FormOrder.ChangeBillingMonth");

        public static PermissionsStruct FormOrder_ChangeState =>
            GetPermission("FormOrder.ChangeState");

        public static PermissionsStruct FormPaymentPlan =>
            GetPermission("FormPaymentPlan");

        public static PermissionsStruct FormPlanPayment =>
            GetPermission("FormPlanPayment");

        public static PermissionsStruct FormPOSType =>
            GetPermission("FormPOSType");

        public static PermissionsStruct FormPredefinedText =>
            GetPermission("FormPredefinedText");

        public static PermissionsStruct FormPriceCode =>
            GetPermission("FormPriceCode");

        public static PermissionsStruct FormPriceListEditor =>
            GetPermission("FormPriceListEditor");

        public static PermissionsStruct FormPriceUpdater =>
            GetPermission("FormPriceUpdater");

        public static PermissionsStruct FormPricing =>
            GetPermission("FormPricing");

        public static PermissionsStruct FormPrintInvoices =>
            GetPermission("FormPrintInvoices");

        public static PermissionsStruct FormProcessOrders =>
            GetPermission("FormProcessOrders");

        public static PermissionsStruct FormProcessOxygen =>
            GetPermission("FormProcessOxygen");

        public static PermissionsStruct FormProductType =>
            GetPermission("FormProductType");

        public static PermissionsStruct FormProvider =>
            GetPermission("FormProvider");

        public static PermissionsStruct FormProviderNumberType =>
            GetPermission("FormProviderNumberType");

        public static PermissionsStruct FormPurchaseOrder =>
            GetPermission("FormPurchaseOrder");

        public static PermissionsStruct FormPurchaseOrder_Approved =>
            GetPermission("FormPurchaseOrder.Approved");

        public static PermissionsStruct FormReferral =>
            GetPermission("FormReferral");

        public static PermissionsStruct FormReferralType =>
            GetPermission("FormReferralType");

        public static PermissionsStruct FormReportSelector =>
            GetPermission("FormReportSelector");

        public static PermissionsStruct FormRetailSales =>
            GetPermission("FormRetailSales");

        public static PermissionsStruct FormSalesRep =>
            GetPermission("FormSalesRep");

        public static PermissionsStruct FormSerial =>
            GetPermission("FormSerial");

        public static PermissionsStruct FormSessions =>
            GetPermission("FormSessions");

        public static PermissionsStruct FormShippingMethod =>
            GetPermission("FormShippingMethod");

        public static PermissionsStruct FormSubmitter =>
            GetPermission("FormSubmitter");

        public static PermissionsStruct FormSurvey =>
            GetPermission("FormSurvey");

        public static PermissionsStruct FormTaxRate =>
            GetPermission("FormTaxRate");

        public static PermissionsStruct FormUser =>
            GetPermission("FormUser");

        public static PermissionsStruct FormVendor =>
            GetPermission("FormVendor");

        public static PermissionsStruct FormWarehouse =>
            GetPermission("FormWarehouse");

        public static PermissionsStruct FormWarehouseInventory =>
            GetPermission("FormWarehouseInventory");

        public static PermissionsStruct FormZipCode =>
            GetPermission("FormZipCode");

        public static PermissionsStruct WizardReturnSales =>
            GetPermission("WizardReturnSales");
    }
}

