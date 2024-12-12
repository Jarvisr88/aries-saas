namespace DMEWorks.Core
{
    using DMEWorks;
    using DMEWorks.Ability;
    using DMEWorks.Forms;
    using DMEWorks.Forms.PaymentPlan;
    using DMEWorks.Forms.Shipping.Ups;
    using DMEWorks.Maintain;
    using DMEWorks.Maintain.Auxilary;
    using DMEWorks.PriceUtilities;
    using DMEWorks.SecureCare;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public class FormFactories
    {
        public static void CheckPermissions(string PermissionName)
        {
            PermissionsStruct? userPermissions = Globals.GetUserPermissions(PermissionName);
            if (userPermissions == null)
            {
                throw new ArgumentOutOfRangeException($"Permission '{PermissionName}' does not exists");
            }
            if (!userPermissions.Value.Allow_VIEW)
            {
                throw new UserNotifyException($"Cannot open form '{PermissionName}' since you do not have such rights");
            }
        }

        public static FormFactory FormAuthorizationType() => 
            new FormAuthorizationTypeFactory();

        public static FormFactory FormBatchPayments() => 
            new FormBatchPaymentsFactory();

        public static FormFactory FormBillingType() => 
            new FormBillingTypeFactory();

        public static FormFactory FormBrowser(Uri url, string text) => 
            new FormBrowserFactory(url, text);

        public static FormFactory FormCallback() => 
            new FormCallbackFactory();

        public static FormFactory FormCMNRX() => 
            new FormCMNRXFactory();

        public static FormFactory FormCompany() => 
            new FormCompanyFactory();

        public static FormFactory FormCompliance() => 
            new FormComplianceFactory();

        public static FormFactory FormCompliancePopup() => 
            new FormCompliancePopupFactory();

        public static FormFactory FormCrystalReport() => 
            new FormCrystalReportFactory();

        public static FormFactory FormCustomer() => 
            new FormCustomerFactory();

        public static FormFactory FormCustomerClass() => 
            new FormCustomerClassFactory();

        public static FormFactory FormCustomerType() => 
            new FormCustomerTypeFactory();

        public static FormFactory FormDenial() => 
            new FormDenialFactory();

        public static FormFactory FormDoctor() => 
            new FormDoctorFactory();

        public static FormFactory FormDoctorType() => 
            new FormDoctorTypeFactory();

        public static FormFactory FormEligibility() => 
            new FormEligibilityFactory();

        public static FormFactory FormFacility() => 
            new FormFacilityFactory();

        public static FormFactory FormHAO() => 
            new FormHAOFactory();

        public static FormFactory FormICD10() => 
            new FormICD10Factory();

        public static FormFactory FormICD9() => 
            new FormICD9Factory();

        public static FormFactory FormImage() => 
            new FormImageFactory();

        public static FormFactory FormImageSearch() => 
            new FormImageSearchFactory();

        public static FormFactory FormInsuranceCompany() => 
            new FormInsuranceCompanyFactory();

        public static FormFactory FormInsuranceCompanyGroup() => 
            new FormInsuranceCompanyGroupFactory();

        public static FormFactory FormInsuranceType() => 
            new FormInsuranceTypeFactory();

        public static FormFactory FormInventory() => 
            new FormInventoryFactory();

        public static FormFactory FormInventoryAdjustment() => 
            new FormInventoryAdjustmentFactory();

        public static FormFactory FormInventoryItem() => 
            new FormInventoryItemFactory();

        public static FormFactory FormInventoryTransactions() => 
            new FormInventoryTransactionsFactory();

        public static FormFactory FormInvoice() => 
            new FormInvoiceFactory();

        public static FormFactory FormInvoiceForm() => 
            new FormInvoiceFormFactory();

        public static FormFactory FormKit() => 
            new FormKitFactory();

        public static FormFactory FormLegalRep() => 
            new FormLegalRepFactory();

        public static FormFactory FormLocation() => 
            new FormLocationFactory();

        public static FormFactory FormManufacturer() => 
            new FormManufacturerFactory();

        public static FormFactory FormMedicalConditions() => 
            new FormMedicalConditionsFactory();

        public static FormFactory FormMissingInformation() => 
            new FormMissingInformationFactory();

        public static FormFactory FormOrder() => 
            new FormOrderFactory();

        public static FormFactory FormOutput() => 
            new FormOutputFactory();

        public static FormFactory FormPaymentPlan(int CustomerID) => 
            new FormPaymentPlanFactory(CustomerID);

        public static FormFactory FormPaymentPlans() => 
            new FormPaymentPlansFactory();

        public static FormFactory FormPlanPayment(int CustomerID) => 
            new FormPlanPaymentFactory(CustomerID);

        public static FormFactory FormPOSType() => 
            new FormPOSTypeFactory();

        public static FormFactory FormPredefinedText() => 
            new FormPredefinedTextFactory();

        public static FormFactory FormPriceCode() => 
            new FormPriceCodeFactory();

        public static FormFactory FormPriceListEditor() => 
            new FormPriceListEditorFactory();

        public static FormFactory FormPriceUpdater() => 
            new FormPriceUpdaterFactory();

        public static FormFactory FormPricing() => 
            new FormPricingFactory();

        public static FormFactory FormPrintInvoices() => 
            new FormPrintInvoicesFactory();

        public static FormFactory FormProcessOrders() => 
            new FormProcessOrdersFactory();

        public static FormFactory FormProcessOxygen() => 
            new FormProcessOxygenFactory();

        public static FormFactory FormProductType() => 
            new FormProductTypeFactory();

        public static FormFactory FormProvider() => 
            new FormProviderFactory();

        public static FormFactory FormProviderNumberType() => 
            new FormProviderNumberTypeFactory();

        public static FormFactory FormPurchaseOrder() => 
            new FormPurchaseOrderFactory();

        public static FormFactory FormReferral() => 
            new FormReferralFactory();

        public static FormFactory FormReferralType() => 
            new FormReferralTypeFactory();

        public static FormFactory FormRentalPickup() => 
            new FormRentalPickupFactory();

        public static FormFactory FormReportSelector() => 
            new FormReportSelectorFactory();

        public static FormFactory FormRetailSales() => 
            new FormRetailSalesFactory();

        public static FormFactory FormSalesRep() => 
            new FormSalesRepFactory();

        public static FormFactory FormSameOrSimilar(IEnumerable<string> BillingCodes, string PolicyNumber) => 
            new FormSameOrSimilarFactory(BillingCodes, PolicyNumber);

        public static FormFactory FormSecureCare() => 
            new FormSecureCareFactory();

        public static FormFactory FormSerial() => 
            new FormSerialFactory();

        public static FormFactory FormSessions() => 
            new FormSessionsFactory();

        public static FormFactory FormShippingMethod() => 
            new FormShippingMethodFactory();

        public static FormFactory FormSurvey() => 
            new FormSurveyFactory();

        public static FormFactory FormTaxRate() => 
            new FormTaxRateFactory();

        public static FormFactory FormUpsShipping(int CustomerID) => 
            new FormUpsShippingFactory(CustomerID);

        public static FormFactory FormUser() => 
            new FormUserFactory();

        public static FormFactory FormVendor() => 
            new FormVendorFactory();

        public static FormFactory FormWarehouse() => 
            new FormWarehouseFactory();

        public static FormFactory FormZipCode() => 
            new FormZipCodeFactory();

        public static FormFactory WizardInventoryTransfer() => 
            new WizardInventoryTransferFactory();

        public static FormFactory WizardReturnSales() => 
            new WizardReturnSalesFactory();

        public class FormAuthorizationTypeFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormAuthorizationType();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormAuthorizationType;
        }

        public class FormBatchPaymentsFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormBatchPayments();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormBatchPayments;
        }

        public class FormBillingTypeFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormBillingType();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormBillingType;
        }

        public class FormBrowserFactory : FormFactory
        {
            private readonly Uri Url;
            private readonly string Text;

            public FormBrowserFactory(Uri url, string text)
            {
                this.Url = url;
                this.Text = text;
            }

            public override Form CreateForm() => 
                new FormBrowser(this.Url, this.Text);

            public override PermissionsStruct GetPermissions() => 
                PermissionsStruct.All;
        }

        public class FormCallbackFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormCallback();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormCallBack;
        }

        public class FormCMNRXFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormCMNRX();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormCMNRX;
        }

        public class FormCompanyFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormCompany();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormCompany;
        }

        public class FormComplianceFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormCompliance();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormCompliance;
        }

        public class FormCompliancePopupFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormCompliancePopup();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormCompliancePopup;
        }

        public class FormCrystalReportFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormCrystalReport();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormCrystalReport;
        }

        public class FormCustomerClassFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormCustomerClass();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormCustomerClass;
        }

        public class FormCustomerFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormCustomer();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormCustomer;
        }

        public class FormCustomerTypeFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormCustomerType();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormCustomerType;
        }

        public class FormDenialFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormDenial();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormDenial;
        }

        public class FormDoctorFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormDoctor();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormDoctor;
        }

        public class FormDoctorTypeFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormDoctorType();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormDoctorType;
        }

        public class FormEligibilityFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormEligibility();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormEligibility;
        }

        public class FormFacilityFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormFacility();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormFacility;
        }

        public class FormHAOFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormHAO();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormHAO;
        }

        public class FormICD10Factory : FormFactory
        {
            public override Form CreateForm() => 
                new FormICD10();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormICD10;
        }

        public class FormICD9Factory : FormFactory
        {
            public override Form CreateForm() => 
                new FormICD9();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormICD9;
        }

        public class FormImageFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormImage();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormImage;
        }

        public class FormImageSearchFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormImageSearch();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormImageSearch;
        }

        public class FormInsuranceCompanyFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormInsuranceCompany();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormInsuranceCompany;
        }

        public class FormInsuranceCompanyGroupFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormInsuranceCompanyGroup();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormInsuranceCompanyGroup;
        }

        public class FormInsuranceTypeFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormInsuranceType();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormInsuranceType;
        }

        public class FormInventoryAdjustmentFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormInventoryAdjustment();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormInventory;
        }

        public class FormInventoryFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormInventory();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormInventory;
        }

        public class FormInventoryItemFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormInventoryItem();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormInventoryItem;
        }

        public class FormInventoryTransactionsFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormInventoryTransactions();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormInventory;
        }

        public class FormInvoiceFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormInvoice();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormInvoice;
        }

        public class FormInvoiceFormFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormInvoiceForm();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormInvoiceForm;
        }

        public class FormKitFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormKit();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormKit;
        }

        public class FormLegalRepFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormLegalRep();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormLegalRep;
        }

        public class FormLocationFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormLocation();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormLocation;
        }

        public class FormManufacturerFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormManufacturer();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormManufacturer;
        }

        public class FormMedicalConditionsFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormMedicalConditions();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormMedicalConditions;
        }

        public class FormMissingInformationFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormMissingInformation();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormMissingInformation;
        }

        public class FormOrderFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormOrder();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormOrder;
        }

        public class FormOutputFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormOutput();

            public override PermissionsStruct GetPermissions() => 
                PermissionsStruct.All;
        }

        public class FormPaymentPlanFactory : FormFactory
        {
            private readonly int CustomerID;

            public FormPaymentPlanFactory(int CustomerID)
            {
                this.CustomerID = CustomerID;
            }

            public override Form CreateForm() => 
                new FormPaymentPlan(this.CustomerID);

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormPaymentPlan;
        }

        public class FormPaymentPlansFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormPaymentPlans();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormPaymentPlan;
        }

        public class FormPlanPaymentFactory : FormFactory
        {
            private readonly int CustomerID;

            public FormPlanPaymentFactory(int CustomerID)
            {
                this.CustomerID = CustomerID;
            }

            public override Form CreateForm() => 
                new FormPlanPayment(this.CustomerID);

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormPlanPayment;
        }

        public class FormPOSTypeFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormPOSType();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormPOSType;
        }

        public class FormPredefinedTextFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormPredefinedText();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormPredefinedText;
        }

        public class FormPriceCodeFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormPriceCode();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormPriceCode;
        }

        public class FormPriceListEditorFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormPriceListEditor();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormPriceListEditor;
        }

        public class FormPriceUpdaterFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormPriceUpdater();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormPriceUpdater;
        }

        public class FormPricingFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormPricing();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormPricing;
        }

        public class FormPrintInvoicesFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormPrintInvoices();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormPrintInvoices;
        }

        public class FormProcessOrdersFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormProcessOrders();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormProcessOrders;
        }

        public class FormProcessOxygenFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormProcessOxygen();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormProcessOxygen;
        }

        public class FormProductTypeFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormProductType();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormProductType;
        }

        public class FormProviderFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormProvider();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormProvider;
        }

        public class FormProviderNumberTypeFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormProviderNumberType();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormProviderNumberType;
        }

        public class FormPurchaseOrderFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormPurchaseOrder();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormPurchaseOrder;
        }

        public class FormReferralFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormReferral();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormReferral;
        }

        public class FormReferralTypeFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormReferralType();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormReferralType;
        }

        public class FormRentalPickupFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormRentalPickup();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormOrder;
        }

        public class FormReportSelectorFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormReportSelector();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormReportSelector;
        }

        public class FormRetailSalesFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormRetailSales();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormRetailSales;
        }

        public class FormSalesRepFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormSalesRep();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormSalesRep;
        }

        public class FormSameOrSimilarFactory : FormFactory
        {
            private readonly IEnumerable<string> BillingCodes;
            private readonly string PolicyNumber;

            public FormSameOrSimilarFactory(IEnumerable<string> BillingCodes, string PolicyNumber)
            {
                this.BillingCodes = BillingCodes;
                this.PolicyNumber = PolicyNumber;
            }

            public override Form CreateForm() => 
                new FormSameOrSimilar(this.BillingCodes, this.PolicyNumber);

            public override PermissionsStruct GetPermissions() => 
                PermissionsStruct.All;
        }

        public class FormSecureCareFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormSecureCare();

            public override PermissionsStruct GetPermissions() => 
                PermissionsStruct.All;
        }

        public class FormSerialFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormSerial();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormSerial;
        }

        public class FormSessionsFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormSessions();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormSessions;
        }

        public class FormShippingMethodFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormShippingMethod();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormShippingMethod;
        }

        public class FormSurveyFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormSurvey();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormSurvey;
        }

        public class FormTaxRateFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormTaxRate();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormTaxRate;
        }

        public class FormUpsShippingFactory : FormFactory
        {
            private readonly int CustomerID;

            public FormUpsShippingFactory(int CustomerID)
            {
                this.CustomerID = CustomerID;
            }

            public override Form CreateForm() => 
                new FormUpsShipping(this.CustomerID);

            public override PermissionsStruct GetPermissions() => 
                PermissionsStruct.All;
        }

        public class FormUserFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormUser();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormUser;
        }

        public class FormVendorFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormVendor();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormVendor;
        }

        public class FormWarehouseFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormWarehouse();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormWarehouse;
        }

        public class FormZipCodeFactory : FormFactory
        {
            public override Form CreateForm() => 
                new FormZipCode();

            public override PermissionsStruct GetPermissions() => 
                Permissions.FormZipCode;
        }

        public class WizardInventoryTransferFactory : FormFactory
        {
            public override Form CreateForm() => 
                new WizardInventoryTransfer();

            public override PermissionsStruct GetPermissions() => 
                PermissionsStruct.All;
        }

        public class WizardReturnSalesFactory : FormFactory
        {
            public override Form CreateForm() => 
                new WizardReturnSales();

            public override PermissionsStruct GetPermissions() => 
                Permissions.WizardReturnSales;
        }
    }
}

