namespace My
{
    using DMEWorks;
    using DMEWorks.Core;
    using DMEWorks.Details;
    using DMEWorks.Forms;
    using DMEWorks.Forms.PaymentPlan;
    using DMEWorks.Forms.Shipping.Ups;
    using DMEWorks.Maintain;
    using DMEWorks.Maintain.Auxilary;
    using DMEWorks.SecureCare;
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.ApplicationServices;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.CodeDom.Compiler;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using Ups.GroundShipping;

    [StandardModule, HideModuleName, GeneratedCode("MyTemplate", "11.0.0.0")]
    internal sealed class MyProject
    {
        private static readonly ThreadSafeObjectProvider<My.MyComputer> m_ComputerObjectProvider = new ThreadSafeObjectProvider<My.MyComputer>();
        private static readonly ThreadSafeObjectProvider<My.MyApplication> m_AppObjectProvider = new ThreadSafeObjectProvider<My.MyApplication>();
        private static readonly ThreadSafeObjectProvider<Microsoft.VisualBasic.ApplicationServices.User> m_UserObjectProvider = new ThreadSafeObjectProvider<Microsoft.VisualBasic.ApplicationServices.User>();
        private static ThreadSafeObjectProvider<MyForms> m_MyFormsObjectProvider = new ThreadSafeObjectProvider<MyForms>();
        private static readonly ThreadSafeObjectProvider<MyWebServices> m_MyWebServicesObjectProvider = new ThreadSafeObjectProvider<MyWebServices>();

        [HelpKeyword("My.Computer")]
        internal static My.MyComputer Computer =>
            m_ComputerObjectProvider.GetInstance;

        [HelpKeyword("My.Application")]
        internal static My.MyApplication Application =>
            m_AppObjectProvider.GetInstance;

        [HelpKeyword("My.User")]
        internal static Microsoft.VisualBasic.ApplicationServices.User User =>
            m_UserObjectProvider.GetInstance;

        [HelpKeyword("My.Forms")]
        internal static MyForms Forms =>
            m_MyFormsObjectProvider.GetInstance;

        [HelpKeyword("My.WebServices")]
        internal static MyWebServices WebServices =>
            m_MyWebServicesObjectProvider.GetInstance;

        [EditorBrowsable(EditorBrowsableState.Never), MyGroupCollection("System.Windows.Forms.Form", "Create__Instance__", "Dispose__Instance__", "My.MyProject.Forms")]
        internal sealed class MyForms
        {
            [ThreadStatic]
            private static Hashtable m_FormBeingCreated;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DialogApproveParameters m_DialogApproveParameters;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.DialogBatchPaymentsReminder m_DialogBatchPaymentsReminder;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.DialogEndSale m_DialogEndSale;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.DialogLocation m_DialogLocation;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Details.DialogReorder m_DialogReorder;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Details.DialogWarehouse m_DialogWarehouse;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.Auxilary.FormAuthorizationType m_FormAuthorizationType;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Core.FormAutoIncrementMaintain m_FormAutoIncrementMaintain;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.FormBatchPayments m_FormBatchPayments;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.Auxilary.FormBillingType m_FormBillingType;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.FormCallback m_FormCallback;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public FormCmnList2 m_FormCmnList2;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormCMNRX m_FormCMNRX;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormCompany m_FormCompany;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormCompliance m_FormCompliance;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.FormCompliancePopup m_FormCompliancePopup;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormCrystalReport m_FormCrystalReport;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormCustomer m_FormCustomer;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.Auxilary.FormCustomerClass m_FormCustomerClass;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.Auxilary.FormCustomerType m_FormCustomerType;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormDenial m_FormDenial;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Core.FormDetails m_FormDetails;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.FormDisconnectionAlert m_FormDisconnectionAlert;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormDoctor m_FormDoctor;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.Auxilary.FormDoctorType m_FormDoctorType;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.FormEligibility m_FormEligibility;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormFacility m_FormFacility;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormHAO m_FormHAO;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormICD10 m_FormICD10;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormICD9 m_FormICD9;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormImage m_FormImage;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public FormImages m_FormImages;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.FormImageSearch m_FormImageSearch;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormInsuranceCompany m_FormInsuranceCompany;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.Auxilary.FormInsuranceCompanyGroup m_FormInsuranceCompanyGroup;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.Auxilary.FormInsuranceType m_FormInsuranceType;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Core.FormIntegerMaintain m_FormIntegerMaintain;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.FormInventory m_FormInventory;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.FormInventoryAdjustment m_FormInventoryAdjustment;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormInventoryItem m_FormInventoryItem;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.FormInventoryTransactions m_FormInventoryTransactions;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormInvoice m_FormInvoice;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Details.FormInvoiceDetail m_FormInvoiceDetail;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.Auxilary.FormInvoiceForm m_FormInvoiceForm;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Details.FormInvoiceTransaction m_FormInvoiceTransaction;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormKit m_FormKit;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Details.FormKitDetails m_FormKitDetails;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormLegalRep m_FormLegalRep;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormLocation m_FormLocation;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.FormMain m_FormMain;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormManufacturer m_FormManufacturer;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormMedicalConditions m_FormMedicalConditions;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.FormMissingInformation m_FormMissingInformation;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Details.FormNewInvoiceTransaction m_FormNewInvoiceTransaction;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Details.FormNewPayment m_FormNewPayment;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormOrder m_FormOrder;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Details.FormOrderDetail m_FormOrderDetail;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Details.FormOrderDetailBase m_FormOrderDetailBase;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.FormOutput m_FormOutput;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.PaymentPlan.FormPaymentPlans m_FormPaymentPlans;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.Auxilary.FormPOSType m_FormPOSType;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormPredefinedText m_FormPredefinedText;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.Auxilary.FormPriceCode m_FormPriceCode;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormPricing m_FormPricing;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.FormPrintInvoices m_FormPrintInvoices;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.FormProcessOrders m_FormProcessOrders;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.FormProcessOxygen m_FormProcessOxygen;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.Auxilary.FormProductType m_FormProductType;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormProvider m_FormProvider;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.Auxilary.FormProviderNumberType m_FormProviderNumberType;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormPurchaseOrder m_FormPurchaseOrder;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Details.FormPurchaseOrderDetail m_FormPurchaseOrderDetail;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.Shipping.Ups.FormPurchaseOrderDetail2 m_FormPurchaseOrderDetail2;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormReferral m_FormReferral;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.Auxilary.FormReferralType m_FormReferralType;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.FormRentalPickup m_FormRentalPickup;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.FormReportSelector m_FormReportSelector;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.FormRetailSales m_FormRetailSales;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormSalesRep m_FormSalesRep;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.SecureCare.FormSecureCare m_FormSecureCare;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormSerial m_FormSerial;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Details.FormSerialMaintenance m_FormSerialMaintenance;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Details.FormSerialTransaction m_FormSerialTransaction;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.FormSessions m_FormSessions;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.Auxilary.FormShippingMethod m_FormShippingMethod;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Core.FormStringMaintain m_FormStringMaintain;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormSurvey m_FormSurvey;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormTaxRate m_FormTaxRate;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public FormTips m_FormTips;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormUser m_FormUser;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormVendor m_FormVendor;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormWarehouse m_FormWarehouse;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Maintain.FormZipCode m_FormZipCode;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public frmAbout m_frmAbout;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.VBDateBox m_VBDateBox;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.VBInputBox m_VBInputBox;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.VBSelectBox m_VBSelectBox;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.WizardInventoryTransfer m_WizardInventoryTransfer;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.WizardReturnSales m_WizardReturnSales;
            [EditorBrowsable(EditorBrowsableState.Never)]
            public DMEWorks.Forms.WizardSelectKit m_WizardSelectKit;

            [DebuggerHidden]
            private static T Create__Instance__<T>(T Instance) where T: Form, new()
            {
                T local;
                if ((Instance != null) && !Instance.IsDisposed)
                {
                    local = Instance;
                }
                else
                {
                    bool flag1;
                    if (m_FormBeingCreated == null)
                    {
                        m_FormBeingCreated = new Hashtable();
                    }
                    else if (m_FormBeingCreated.ContainsKey(typeof(T)))
                    {
                        throw new InvalidOperationException(Utils.GetResourceString("WinForms_RecursiveFormCreate", new string[0]));
                    }
                    m_FormBeingCreated.Add(typeof(T), null);
                    try
                    {
                        local = Activator.CreateInstance<T>();
                    }
                    catch (TargetInvocationException exception1) when ((() => // NOTE: To create compilable code, filter at IL offset 0074 was represented using lambda expression.
                    {
                        TargetInvocationException ex = exception1;
                        ProjectData.SetProjectError(ex);
                        flag1 = ex.InnerException != null;
                        return (T) flag1;
                    })())
                    {
                        TargetInvocationException exception;
                        string[] args = new string[] { exception.InnerException.Message };
                        throw new InvalidOperationException(Utils.GetResourceString("WinForms_SeeInnerException", args), exception.InnerException);
                    }
                    finally
                    {
                        m_FormBeingCreated.Remove(typeof(T));
                    }
                }
                return local;
            }

            [DebuggerHidden]
            private void Dispose__Instance__<T>(ref T instance) where T: Form
            {
                instance.Dispose();
                instance = default(T);
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public override bool Equals(object o) => 
                base.Equals(o);

            [EditorBrowsable(EditorBrowsableState.Never)]
            public override int GetHashCode() => 
                base.GetHashCode();

            [EditorBrowsable(EditorBrowsableState.Never)]
            internal Type GetType() => 
                typeof(My.MyProject.MyForms);

            [EditorBrowsable(EditorBrowsableState.Never)]
            public override string ToString() => 
                base.ToString();

            public DialogApproveParameters DialogApproveParameters
            {
                get
                {
                    this.m_DialogApproveParameters = Create__Instance__<DialogApproveParameters>(this.m_DialogApproveParameters);
                    return this.m_DialogApproveParameters;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_DialogApproveParameters))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DialogApproveParameters>(ref this.m_DialogApproveParameters);
                    }
                }
            }

            public DMEWorks.Forms.DialogBatchPaymentsReminder DialogBatchPaymentsReminder
            {
                get
                {
                    this.m_DialogBatchPaymentsReminder = Create__Instance__<DMEWorks.Forms.DialogBatchPaymentsReminder>(this.m_DialogBatchPaymentsReminder);
                    return this.m_DialogBatchPaymentsReminder;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_DialogBatchPaymentsReminder))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.DialogBatchPaymentsReminder>(ref this.m_DialogBatchPaymentsReminder);
                    }
                }
            }

            public DMEWorks.Forms.DialogEndSale DialogEndSale
            {
                get
                {
                    this.m_DialogEndSale = Create__Instance__<DMEWorks.Forms.DialogEndSale>(this.m_DialogEndSale);
                    return this.m_DialogEndSale;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_DialogEndSale))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.DialogEndSale>(ref this.m_DialogEndSale);
                    }
                }
            }

            public DMEWorks.DialogLocation DialogLocation
            {
                get
                {
                    this.m_DialogLocation = Create__Instance__<DMEWorks.DialogLocation>(this.m_DialogLocation);
                    return this.m_DialogLocation;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_DialogLocation))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.DialogLocation>(ref this.m_DialogLocation);
                    }
                }
            }

            public DMEWorks.Details.DialogReorder DialogReorder
            {
                get
                {
                    this.m_DialogReorder = Create__Instance__<DMEWorks.Details.DialogReorder>(this.m_DialogReorder);
                    return this.m_DialogReorder;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_DialogReorder))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Details.DialogReorder>(ref this.m_DialogReorder);
                    }
                }
            }

            public DMEWorks.Details.DialogWarehouse DialogWarehouse
            {
                get
                {
                    this.m_DialogWarehouse = Create__Instance__<DMEWorks.Details.DialogWarehouse>(this.m_DialogWarehouse);
                    return this.m_DialogWarehouse;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_DialogWarehouse))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Details.DialogWarehouse>(ref this.m_DialogWarehouse);
                    }
                }
            }

            public DMEWorks.Maintain.Auxilary.FormAuthorizationType FormAuthorizationType
            {
                get
                {
                    this.m_FormAuthorizationType = Create__Instance__<DMEWorks.Maintain.Auxilary.FormAuthorizationType>(this.m_FormAuthorizationType);
                    return this.m_FormAuthorizationType;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormAuthorizationType))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.Auxilary.FormAuthorizationType>(ref this.m_FormAuthorizationType);
                    }
                }
            }

            public DMEWorks.Core.FormAutoIncrementMaintain FormAutoIncrementMaintain
            {
                get
                {
                    this.m_FormAutoIncrementMaintain = Create__Instance__<DMEWorks.Core.FormAutoIncrementMaintain>(this.m_FormAutoIncrementMaintain);
                    return this.m_FormAutoIncrementMaintain;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormAutoIncrementMaintain))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Core.FormAutoIncrementMaintain>(ref this.m_FormAutoIncrementMaintain);
                    }
                }
            }

            public DMEWorks.Forms.FormBatchPayments FormBatchPayments
            {
                get
                {
                    this.m_FormBatchPayments = Create__Instance__<DMEWorks.Forms.FormBatchPayments>(this.m_FormBatchPayments);
                    return this.m_FormBatchPayments;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormBatchPayments))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.FormBatchPayments>(ref this.m_FormBatchPayments);
                    }
                }
            }

            public DMEWorks.Maintain.Auxilary.FormBillingType FormBillingType
            {
                get
                {
                    this.m_FormBillingType = Create__Instance__<DMEWorks.Maintain.Auxilary.FormBillingType>(this.m_FormBillingType);
                    return this.m_FormBillingType;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormBillingType))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.Auxilary.FormBillingType>(ref this.m_FormBillingType);
                    }
                }
            }

            public DMEWorks.Forms.FormCallback FormCallback
            {
                get
                {
                    this.m_FormCallback = Create__Instance__<DMEWorks.Forms.FormCallback>(this.m_FormCallback);
                    return this.m_FormCallback;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormCallback))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.FormCallback>(ref this.m_FormCallback);
                    }
                }
            }

            public FormCmnList2 FormCmnList2
            {
                get
                {
                    this.m_FormCmnList2 = Create__Instance__<FormCmnList2>(this.m_FormCmnList2);
                    return this.m_FormCmnList2;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormCmnList2))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<FormCmnList2>(ref this.m_FormCmnList2);
                    }
                }
            }

            public DMEWorks.Maintain.FormCMNRX FormCMNRX
            {
                get
                {
                    this.m_FormCMNRX = Create__Instance__<DMEWorks.Maintain.FormCMNRX>(this.m_FormCMNRX);
                    return this.m_FormCMNRX;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormCMNRX))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormCMNRX>(ref this.m_FormCMNRX);
                    }
                }
            }

            public DMEWorks.Maintain.FormCompany FormCompany
            {
                get
                {
                    this.m_FormCompany = Create__Instance__<DMEWorks.Maintain.FormCompany>(this.m_FormCompany);
                    return this.m_FormCompany;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormCompany))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormCompany>(ref this.m_FormCompany);
                    }
                }
            }

            public DMEWorks.Maintain.FormCompliance FormCompliance
            {
                get
                {
                    this.m_FormCompliance = Create__Instance__<DMEWorks.Maintain.FormCompliance>(this.m_FormCompliance);
                    return this.m_FormCompliance;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormCompliance))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormCompliance>(ref this.m_FormCompliance);
                    }
                }
            }

            public DMEWorks.Forms.FormCompliancePopup FormCompliancePopup
            {
                get
                {
                    this.m_FormCompliancePopup = Create__Instance__<DMEWorks.Forms.FormCompliancePopup>(this.m_FormCompliancePopup);
                    return this.m_FormCompliancePopup;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormCompliancePopup))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.FormCompliancePopup>(ref this.m_FormCompliancePopup);
                    }
                }
            }

            public DMEWorks.Maintain.FormCrystalReport FormCrystalReport
            {
                get
                {
                    this.m_FormCrystalReport = Create__Instance__<DMEWorks.Maintain.FormCrystalReport>(this.m_FormCrystalReport);
                    return this.m_FormCrystalReport;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormCrystalReport))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormCrystalReport>(ref this.m_FormCrystalReport);
                    }
                }
            }

            public DMEWorks.Maintain.FormCustomer FormCustomer
            {
                get
                {
                    this.m_FormCustomer = Create__Instance__<DMEWorks.Maintain.FormCustomer>(this.m_FormCustomer);
                    return this.m_FormCustomer;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormCustomer))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormCustomer>(ref this.m_FormCustomer);
                    }
                }
            }

            public DMEWorks.Maintain.Auxilary.FormCustomerClass FormCustomerClass
            {
                get
                {
                    this.m_FormCustomerClass = Create__Instance__<DMEWorks.Maintain.Auxilary.FormCustomerClass>(this.m_FormCustomerClass);
                    return this.m_FormCustomerClass;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormCustomerClass))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.Auxilary.FormCustomerClass>(ref this.m_FormCustomerClass);
                    }
                }
            }

            public DMEWorks.Maintain.Auxilary.FormCustomerType FormCustomerType
            {
                get
                {
                    this.m_FormCustomerType = Create__Instance__<DMEWorks.Maintain.Auxilary.FormCustomerType>(this.m_FormCustomerType);
                    return this.m_FormCustomerType;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormCustomerType))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.Auxilary.FormCustomerType>(ref this.m_FormCustomerType);
                    }
                }
            }

            public DMEWorks.Maintain.FormDenial FormDenial
            {
                get
                {
                    this.m_FormDenial = Create__Instance__<DMEWorks.Maintain.FormDenial>(this.m_FormDenial);
                    return this.m_FormDenial;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormDenial))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormDenial>(ref this.m_FormDenial);
                    }
                }
            }

            public DMEWorks.Core.FormDetails FormDetails
            {
                get
                {
                    this.m_FormDetails = Create__Instance__<DMEWorks.Core.FormDetails>(this.m_FormDetails);
                    return this.m_FormDetails;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormDetails))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Core.FormDetails>(ref this.m_FormDetails);
                    }
                }
            }

            public DMEWorks.Forms.FormDisconnectionAlert FormDisconnectionAlert
            {
                get
                {
                    this.m_FormDisconnectionAlert = Create__Instance__<DMEWorks.Forms.FormDisconnectionAlert>(this.m_FormDisconnectionAlert);
                    return this.m_FormDisconnectionAlert;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormDisconnectionAlert))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.FormDisconnectionAlert>(ref this.m_FormDisconnectionAlert);
                    }
                }
            }

            public DMEWorks.Maintain.FormDoctor FormDoctor
            {
                get
                {
                    this.m_FormDoctor = Create__Instance__<DMEWorks.Maintain.FormDoctor>(this.m_FormDoctor);
                    return this.m_FormDoctor;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormDoctor))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormDoctor>(ref this.m_FormDoctor);
                    }
                }
            }

            public DMEWorks.Maintain.Auxilary.FormDoctorType FormDoctorType
            {
                get
                {
                    this.m_FormDoctorType = Create__Instance__<DMEWorks.Maintain.Auxilary.FormDoctorType>(this.m_FormDoctorType);
                    return this.m_FormDoctorType;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormDoctorType))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.Auxilary.FormDoctorType>(ref this.m_FormDoctorType);
                    }
                }
            }

            public DMEWorks.Forms.FormEligibility FormEligibility
            {
                get
                {
                    this.m_FormEligibility = Create__Instance__<DMEWorks.Forms.FormEligibility>(this.m_FormEligibility);
                    return this.m_FormEligibility;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormEligibility))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.FormEligibility>(ref this.m_FormEligibility);
                    }
                }
            }

            public DMEWorks.Maintain.FormFacility FormFacility
            {
                get
                {
                    this.m_FormFacility = Create__Instance__<DMEWorks.Maintain.FormFacility>(this.m_FormFacility);
                    return this.m_FormFacility;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormFacility))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormFacility>(ref this.m_FormFacility);
                    }
                }
            }

            public DMEWorks.Maintain.FormHAO FormHAO
            {
                get
                {
                    this.m_FormHAO = Create__Instance__<DMEWorks.Maintain.FormHAO>(this.m_FormHAO);
                    return this.m_FormHAO;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormHAO))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormHAO>(ref this.m_FormHAO);
                    }
                }
            }

            public DMEWorks.Maintain.FormICD10 FormICD10
            {
                get
                {
                    this.m_FormICD10 = Create__Instance__<DMEWorks.Maintain.FormICD10>(this.m_FormICD10);
                    return this.m_FormICD10;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormICD10))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormICD10>(ref this.m_FormICD10);
                    }
                }
            }

            public DMEWorks.Maintain.FormICD9 FormICD9
            {
                get
                {
                    this.m_FormICD9 = Create__Instance__<DMEWorks.Maintain.FormICD9>(this.m_FormICD9);
                    return this.m_FormICD9;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormICD9))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormICD9>(ref this.m_FormICD9);
                    }
                }
            }

            public DMEWorks.Maintain.FormImage FormImage
            {
                get
                {
                    this.m_FormImage = Create__Instance__<DMEWorks.Maintain.FormImage>(this.m_FormImage);
                    return this.m_FormImage;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormImage))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormImage>(ref this.m_FormImage);
                    }
                }
            }

            public FormImages FormImages
            {
                get
                {
                    this.m_FormImages = Create__Instance__<FormImages>(this.m_FormImages);
                    return this.m_FormImages;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormImages))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<FormImages>(ref this.m_FormImages);
                    }
                }
            }

            public DMEWorks.Forms.FormImageSearch FormImageSearch
            {
                get
                {
                    this.m_FormImageSearch = Create__Instance__<DMEWorks.Forms.FormImageSearch>(this.m_FormImageSearch);
                    return this.m_FormImageSearch;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormImageSearch))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.FormImageSearch>(ref this.m_FormImageSearch);
                    }
                }
            }

            public DMEWorks.Maintain.FormInsuranceCompany FormInsuranceCompany
            {
                get
                {
                    this.m_FormInsuranceCompany = Create__Instance__<DMEWorks.Maintain.FormInsuranceCompany>(this.m_FormInsuranceCompany);
                    return this.m_FormInsuranceCompany;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormInsuranceCompany))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormInsuranceCompany>(ref this.m_FormInsuranceCompany);
                    }
                }
            }

            public DMEWorks.Maintain.Auxilary.FormInsuranceCompanyGroup FormInsuranceCompanyGroup
            {
                get
                {
                    this.m_FormInsuranceCompanyGroup = Create__Instance__<DMEWorks.Maintain.Auxilary.FormInsuranceCompanyGroup>(this.m_FormInsuranceCompanyGroup);
                    return this.m_FormInsuranceCompanyGroup;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormInsuranceCompanyGroup))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.Auxilary.FormInsuranceCompanyGroup>(ref this.m_FormInsuranceCompanyGroup);
                    }
                }
            }

            public DMEWorks.Maintain.Auxilary.FormInsuranceType FormInsuranceType
            {
                get
                {
                    this.m_FormInsuranceType = Create__Instance__<DMEWorks.Maintain.Auxilary.FormInsuranceType>(this.m_FormInsuranceType);
                    return this.m_FormInsuranceType;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormInsuranceType))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.Auxilary.FormInsuranceType>(ref this.m_FormInsuranceType);
                    }
                }
            }

            public DMEWorks.Core.FormIntegerMaintain FormIntegerMaintain
            {
                get
                {
                    this.m_FormIntegerMaintain = Create__Instance__<DMEWorks.Core.FormIntegerMaintain>(this.m_FormIntegerMaintain);
                    return this.m_FormIntegerMaintain;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormIntegerMaintain))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Core.FormIntegerMaintain>(ref this.m_FormIntegerMaintain);
                    }
                }
            }

            public DMEWorks.Forms.FormInventory FormInventory
            {
                get
                {
                    this.m_FormInventory = Create__Instance__<DMEWorks.Forms.FormInventory>(this.m_FormInventory);
                    return this.m_FormInventory;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormInventory))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.FormInventory>(ref this.m_FormInventory);
                    }
                }
            }

            public DMEWorks.Forms.FormInventoryAdjustment FormInventoryAdjustment
            {
                get
                {
                    this.m_FormInventoryAdjustment = Create__Instance__<DMEWorks.Forms.FormInventoryAdjustment>(this.m_FormInventoryAdjustment);
                    return this.m_FormInventoryAdjustment;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormInventoryAdjustment))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.FormInventoryAdjustment>(ref this.m_FormInventoryAdjustment);
                    }
                }
            }

            public DMEWorks.Maintain.FormInventoryItem FormInventoryItem
            {
                get
                {
                    this.m_FormInventoryItem = Create__Instance__<DMEWorks.Maintain.FormInventoryItem>(this.m_FormInventoryItem);
                    return this.m_FormInventoryItem;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormInventoryItem))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormInventoryItem>(ref this.m_FormInventoryItem);
                    }
                }
            }

            public DMEWorks.Forms.FormInventoryTransactions FormInventoryTransactions
            {
                get
                {
                    this.m_FormInventoryTransactions = Create__Instance__<DMEWorks.Forms.FormInventoryTransactions>(this.m_FormInventoryTransactions);
                    return this.m_FormInventoryTransactions;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormInventoryTransactions))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.FormInventoryTransactions>(ref this.m_FormInventoryTransactions);
                    }
                }
            }

            public DMEWorks.Maintain.FormInvoice FormInvoice
            {
                get
                {
                    this.m_FormInvoice = Create__Instance__<DMEWorks.Maintain.FormInvoice>(this.m_FormInvoice);
                    return this.m_FormInvoice;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormInvoice))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormInvoice>(ref this.m_FormInvoice);
                    }
                }
            }

            public DMEWorks.Details.FormInvoiceDetail FormInvoiceDetail
            {
                get
                {
                    this.m_FormInvoiceDetail = Create__Instance__<DMEWorks.Details.FormInvoiceDetail>(this.m_FormInvoiceDetail);
                    return this.m_FormInvoiceDetail;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormInvoiceDetail))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Details.FormInvoiceDetail>(ref this.m_FormInvoiceDetail);
                    }
                }
            }

            public DMEWorks.Maintain.Auxilary.FormInvoiceForm FormInvoiceForm
            {
                get
                {
                    this.m_FormInvoiceForm = Create__Instance__<DMEWorks.Maintain.Auxilary.FormInvoiceForm>(this.m_FormInvoiceForm);
                    return this.m_FormInvoiceForm;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormInvoiceForm))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.Auxilary.FormInvoiceForm>(ref this.m_FormInvoiceForm);
                    }
                }
            }

            public DMEWorks.Details.FormInvoiceTransaction FormInvoiceTransaction
            {
                get
                {
                    this.m_FormInvoiceTransaction = Create__Instance__<DMEWorks.Details.FormInvoiceTransaction>(this.m_FormInvoiceTransaction);
                    return this.m_FormInvoiceTransaction;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormInvoiceTransaction))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Details.FormInvoiceTransaction>(ref this.m_FormInvoiceTransaction);
                    }
                }
            }

            public DMEWorks.Maintain.FormKit FormKit
            {
                get
                {
                    this.m_FormKit = Create__Instance__<DMEWorks.Maintain.FormKit>(this.m_FormKit);
                    return this.m_FormKit;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormKit))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormKit>(ref this.m_FormKit);
                    }
                }
            }

            public DMEWorks.Details.FormKitDetails FormKitDetails
            {
                get
                {
                    this.m_FormKitDetails = Create__Instance__<DMEWorks.Details.FormKitDetails>(this.m_FormKitDetails);
                    return this.m_FormKitDetails;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormKitDetails))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Details.FormKitDetails>(ref this.m_FormKitDetails);
                    }
                }
            }

            public DMEWorks.Maintain.FormLegalRep FormLegalRep
            {
                get
                {
                    this.m_FormLegalRep = Create__Instance__<DMEWorks.Maintain.FormLegalRep>(this.m_FormLegalRep);
                    return this.m_FormLegalRep;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormLegalRep))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormLegalRep>(ref this.m_FormLegalRep);
                    }
                }
            }

            public DMEWorks.Maintain.FormLocation FormLocation
            {
                get
                {
                    this.m_FormLocation = Create__Instance__<DMEWorks.Maintain.FormLocation>(this.m_FormLocation);
                    return this.m_FormLocation;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormLocation))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormLocation>(ref this.m_FormLocation);
                    }
                }
            }

            public DMEWorks.Forms.FormMain FormMain
            {
                get
                {
                    this.m_FormMain = Create__Instance__<DMEWorks.Forms.FormMain>(this.m_FormMain);
                    return this.m_FormMain;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormMain))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.FormMain>(ref this.m_FormMain);
                    }
                }
            }

            public DMEWorks.Maintain.FormManufacturer FormManufacturer
            {
                get
                {
                    this.m_FormManufacturer = Create__Instance__<DMEWorks.Maintain.FormManufacturer>(this.m_FormManufacturer);
                    return this.m_FormManufacturer;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormManufacturer))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormManufacturer>(ref this.m_FormManufacturer);
                    }
                }
            }

            public DMEWorks.Maintain.FormMedicalConditions FormMedicalConditions
            {
                get
                {
                    this.m_FormMedicalConditions = Create__Instance__<DMEWorks.Maintain.FormMedicalConditions>(this.m_FormMedicalConditions);
                    return this.m_FormMedicalConditions;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormMedicalConditions))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormMedicalConditions>(ref this.m_FormMedicalConditions);
                    }
                }
            }

            public DMEWorks.Forms.FormMissingInformation FormMissingInformation
            {
                get
                {
                    this.m_FormMissingInformation = Create__Instance__<DMEWorks.Forms.FormMissingInformation>(this.m_FormMissingInformation);
                    return this.m_FormMissingInformation;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormMissingInformation))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.FormMissingInformation>(ref this.m_FormMissingInformation);
                    }
                }
            }

            public DMEWorks.Details.FormNewInvoiceTransaction FormNewInvoiceTransaction
            {
                get
                {
                    this.m_FormNewInvoiceTransaction = Create__Instance__<DMEWorks.Details.FormNewInvoiceTransaction>(this.m_FormNewInvoiceTransaction);
                    return this.m_FormNewInvoiceTransaction;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormNewInvoiceTransaction))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Details.FormNewInvoiceTransaction>(ref this.m_FormNewInvoiceTransaction);
                    }
                }
            }

            public DMEWorks.Details.FormNewPayment FormNewPayment
            {
                get
                {
                    this.m_FormNewPayment = Create__Instance__<DMEWorks.Details.FormNewPayment>(this.m_FormNewPayment);
                    return this.m_FormNewPayment;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormNewPayment))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Details.FormNewPayment>(ref this.m_FormNewPayment);
                    }
                }
            }

            public DMEWorks.Maintain.FormOrder FormOrder
            {
                get
                {
                    this.m_FormOrder = Create__Instance__<DMEWorks.Maintain.FormOrder>(this.m_FormOrder);
                    return this.m_FormOrder;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormOrder))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormOrder>(ref this.m_FormOrder);
                    }
                }
            }

            public DMEWorks.Details.FormOrderDetail FormOrderDetail
            {
                get
                {
                    this.m_FormOrderDetail = Create__Instance__<DMEWorks.Details.FormOrderDetail>(this.m_FormOrderDetail);
                    return this.m_FormOrderDetail;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormOrderDetail))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Details.FormOrderDetail>(ref this.m_FormOrderDetail);
                    }
                }
            }

            public DMEWorks.Details.FormOrderDetailBase FormOrderDetailBase
            {
                get
                {
                    this.m_FormOrderDetailBase = Create__Instance__<DMEWorks.Details.FormOrderDetailBase>(this.m_FormOrderDetailBase);
                    return this.m_FormOrderDetailBase;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormOrderDetailBase))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Details.FormOrderDetailBase>(ref this.m_FormOrderDetailBase);
                    }
                }
            }

            public DMEWorks.Forms.FormOutput FormOutput
            {
                get
                {
                    this.m_FormOutput = Create__Instance__<DMEWorks.Forms.FormOutput>(this.m_FormOutput);
                    return this.m_FormOutput;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormOutput))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.FormOutput>(ref this.m_FormOutput);
                    }
                }
            }

            public DMEWorks.Forms.PaymentPlan.FormPaymentPlans FormPaymentPlans
            {
                get
                {
                    this.m_FormPaymentPlans = Create__Instance__<DMEWorks.Forms.PaymentPlan.FormPaymentPlans>(this.m_FormPaymentPlans);
                    return this.m_FormPaymentPlans;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormPaymentPlans))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.PaymentPlan.FormPaymentPlans>(ref this.m_FormPaymentPlans);
                    }
                }
            }

            public DMEWorks.Maintain.Auxilary.FormPOSType FormPOSType
            {
                get
                {
                    this.m_FormPOSType = Create__Instance__<DMEWorks.Maintain.Auxilary.FormPOSType>(this.m_FormPOSType);
                    return this.m_FormPOSType;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormPOSType))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.Auxilary.FormPOSType>(ref this.m_FormPOSType);
                    }
                }
            }

            public DMEWorks.Maintain.FormPredefinedText FormPredefinedText
            {
                get
                {
                    this.m_FormPredefinedText = Create__Instance__<DMEWorks.Maintain.FormPredefinedText>(this.m_FormPredefinedText);
                    return this.m_FormPredefinedText;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormPredefinedText))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormPredefinedText>(ref this.m_FormPredefinedText);
                    }
                }
            }

            public DMEWorks.Maintain.Auxilary.FormPriceCode FormPriceCode
            {
                get
                {
                    this.m_FormPriceCode = Create__Instance__<DMEWorks.Maintain.Auxilary.FormPriceCode>(this.m_FormPriceCode);
                    return this.m_FormPriceCode;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormPriceCode))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.Auxilary.FormPriceCode>(ref this.m_FormPriceCode);
                    }
                }
            }

            public DMEWorks.Maintain.FormPricing FormPricing
            {
                get
                {
                    this.m_FormPricing = Create__Instance__<DMEWorks.Maintain.FormPricing>(this.m_FormPricing);
                    return this.m_FormPricing;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormPricing))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormPricing>(ref this.m_FormPricing);
                    }
                }
            }

            public DMEWorks.Forms.FormPrintInvoices FormPrintInvoices
            {
                get
                {
                    this.m_FormPrintInvoices = Create__Instance__<DMEWorks.Forms.FormPrintInvoices>(this.m_FormPrintInvoices);
                    return this.m_FormPrintInvoices;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormPrintInvoices))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.FormPrintInvoices>(ref this.m_FormPrintInvoices);
                    }
                }
            }

            public DMEWorks.Forms.FormProcessOrders FormProcessOrders
            {
                get
                {
                    this.m_FormProcessOrders = Create__Instance__<DMEWorks.Forms.FormProcessOrders>(this.m_FormProcessOrders);
                    return this.m_FormProcessOrders;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormProcessOrders))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.FormProcessOrders>(ref this.m_FormProcessOrders);
                    }
                }
            }

            public DMEWorks.Forms.FormProcessOxygen FormProcessOxygen
            {
                get
                {
                    this.m_FormProcessOxygen = Create__Instance__<DMEWorks.Forms.FormProcessOxygen>(this.m_FormProcessOxygen);
                    return this.m_FormProcessOxygen;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormProcessOxygen))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.FormProcessOxygen>(ref this.m_FormProcessOxygen);
                    }
                }
            }

            public DMEWorks.Maintain.Auxilary.FormProductType FormProductType
            {
                get
                {
                    this.m_FormProductType = Create__Instance__<DMEWorks.Maintain.Auxilary.FormProductType>(this.m_FormProductType);
                    return this.m_FormProductType;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormProductType))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.Auxilary.FormProductType>(ref this.m_FormProductType);
                    }
                }
            }

            public DMEWorks.Maintain.FormProvider FormProvider
            {
                get
                {
                    this.m_FormProvider = Create__Instance__<DMEWorks.Maintain.FormProvider>(this.m_FormProvider);
                    return this.m_FormProvider;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormProvider))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormProvider>(ref this.m_FormProvider);
                    }
                }
            }

            public DMEWorks.Maintain.Auxilary.FormProviderNumberType FormProviderNumberType
            {
                get
                {
                    this.m_FormProviderNumberType = Create__Instance__<DMEWorks.Maintain.Auxilary.FormProviderNumberType>(this.m_FormProviderNumberType);
                    return this.m_FormProviderNumberType;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormProviderNumberType))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.Auxilary.FormProviderNumberType>(ref this.m_FormProviderNumberType);
                    }
                }
            }

            public DMEWorks.Maintain.FormPurchaseOrder FormPurchaseOrder
            {
                get
                {
                    this.m_FormPurchaseOrder = Create__Instance__<DMEWorks.Maintain.FormPurchaseOrder>(this.m_FormPurchaseOrder);
                    return this.m_FormPurchaseOrder;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormPurchaseOrder))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormPurchaseOrder>(ref this.m_FormPurchaseOrder);
                    }
                }
            }

            public DMEWorks.Details.FormPurchaseOrderDetail FormPurchaseOrderDetail
            {
                get
                {
                    this.m_FormPurchaseOrderDetail = Create__Instance__<DMEWorks.Details.FormPurchaseOrderDetail>(this.m_FormPurchaseOrderDetail);
                    return this.m_FormPurchaseOrderDetail;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormPurchaseOrderDetail))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Details.FormPurchaseOrderDetail>(ref this.m_FormPurchaseOrderDetail);
                    }
                }
            }

            public DMEWorks.Forms.Shipping.Ups.FormPurchaseOrderDetail2 FormPurchaseOrderDetail2
            {
                get
                {
                    this.m_FormPurchaseOrderDetail2 = Create__Instance__<DMEWorks.Forms.Shipping.Ups.FormPurchaseOrderDetail2>(this.m_FormPurchaseOrderDetail2);
                    return this.m_FormPurchaseOrderDetail2;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormPurchaseOrderDetail2))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.Shipping.Ups.FormPurchaseOrderDetail2>(ref this.m_FormPurchaseOrderDetail2);
                    }
                }
            }

            public DMEWorks.Maintain.FormReferral FormReferral
            {
                get
                {
                    this.m_FormReferral = Create__Instance__<DMEWorks.Maintain.FormReferral>(this.m_FormReferral);
                    return this.m_FormReferral;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormReferral))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormReferral>(ref this.m_FormReferral);
                    }
                }
            }

            public DMEWorks.Maintain.Auxilary.FormReferralType FormReferralType
            {
                get
                {
                    this.m_FormReferralType = Create__Instance__<DMEWorks.Maintain.Auxilary.FormReferralType>(this.m_FormReferralType);
                    return this.m_FormReferralType;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormReferralType))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.Auxilary.FormReferralType>(ref this.m_FormReferralType);
                    }
                }
            }

            public DMEWorks.Forms.FormRentalPickup FormRentalPickup
            {
                get
                {
                    this.m_FormRentalPickup = Create__Instance__<DMEWorks.Forms.FormRentalPickup>(this.m_FormRentalPickup);
                    return this.m_FormRentalPickup;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormRentalPickup))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.FormRentalPickup>(ref this.m_FormRentalPickup);
                    }
                }
            }

            public DMEWorks.Forms.FormReportSelector FormReportSelector
            {
                get
                {
                    this.m_FormReportSelector = Create__Instance__<DMEWorks.Forms.FormReportSelector>(this.m_FormReportSelector);
                    return this.m_FormReportSelector;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormReportSelector))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.FormReportSelector>(ref this.m_FormReportSelector);
                    }
                }
            }

            public DMEWorks.Forms.FormRetailSales FormRetailSales
            {
                get
                {
                    this.m_FormRetailSales = Create__Instance__<DMEWorks.Forms.FormRetailSales>(this.m_FormRetailSales);
                    return this.m_FormRetailSales;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormRetailSales))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.FormRetailSales>(ref this.m_FormRetailSales);
                    }
                }
            }

            public DMEWorks.Maintain.FormSalesRep FormSalesRep
            {
                get
                {
                    this.m_FormSalesRep = Create__Instance__<DMEWorks.Maintain.FormSalesRep>(this.m_FormSalesRep);
                    return this.m_FormSalesRep;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormSalesRep))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormSalesRep>(ref this.m_FormSalesRep);
                    }
                }
            }

            public DMEWorks.SecureCare.FormSecureCare FormSecureCare
            {
                get
                {
                    this.m_FormSecureCare = Create__Instance__<DMEWorks.SecureCare.FormSecureCare>(this.m_FormSecureCare);
                    return this.m_FormSecureCare;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormSecureCare))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.SecureCare.FormSecureCare>(ref this.m_FormSecureCare);
                    }
                }
            }

            public DMEWorks.Maintain.FormSerial FormSerial
            {
                get
                {
                    this.m_FormSerial = Create__Instance__<DMEWorks.Maintain.FormSerial>(this.m_FormSerial);
                    return this.m_FormSerial;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormSerial))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormSerial>(ref this.m_FormSerial);
                    }
                }
            }

            public DMEWorks.Details.FormSerialMaintenance FormSerialMaintenance
            {
                get
                {
                    this.m_FormSerialMaintenance = Create__Instance__<DMEWorks.Details.FormSerialMaintenance>(this.m_FormSerialMaintenance);
                    return this.m_FormSerialMaintenance;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormSerialMaintenance))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Details.FormSerialMaintenance>(ref this.m_FormSerialMaintenance);
                    }
                }
            }

            public DMEWorks.Details.FormSerialTransaction FormSerialTransaction
            {
                get
                {
                    this.m_FormSerialTransaction = Create__Instance__<DMEWorks.Details.FormSerialTransaction>(this.m_FormSerialTransaction);
                    return this.m_FormSerialTransaction;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormSerialTransaction))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Details.FormSerialTransaction>(ref this.m_FormSerialTransaction);
                    }
                }
            }

            public DMEWorks.Forms.FormSessions FormSessions
            {
                get
                {
                    this.m_FormSessions = Create__Instance__<DMEWorks.Forms.FormSessions>(this.m_FormSessions);
                    return this.m_FormSessions;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormSessions))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.FormSessions>(ref this.m_FormSessions);
                    }
                }
            }

            public DMEWorks.Maintain.Auxilary.FormShippingMethod FormShippingMethod
            {
                get
                {
                    this.m_FormShippingMethod = Create__Instance__<DMEWorks.Maintain.Auxilary.FormShippingMethod>(this.m_FormShippingMethod);
                    return this.m_FormShippingMethod;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormShippingMethod))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.Auxilary.FormShippingMethod>(ref this.m_FormShippingMethod);
                    }
                }
            }

            public DMEWorks.Core.FormStringMaintain FormStringMaintain
            {
                get
                {
                    this.m_FormStringMaintain = Create__Instance__<DMEWorks.Core.FormStringMaintain>(this.m_FormStringMaintain);
                    return this.m_FormStringMaintain;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormStringMaintain))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Core.FormStringMaintain>(ref this.m_FormStringMaintain);
                    }
                }
            }

            public DMEWorks.Maintain.FormSurvey FormSurvey
            {
                get
                {
                    this.m_FormSurvey = Create__Instance__<DMEWorks.Maintain.FormSurvey>(this.m_FormSurvey);
                    return this.m_FormSurvey;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormSurvey))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormSurvey>(ref this.m_FormSurvey);
                    }
                }
            }

            public DMEWorks.Maintain.FormTaxRate FormTaxRate
            {
                get
                {
                    this.m_FormTaxRate = Create__Instance__<DMEWorks.Maintain.FormTaxRate>(this.m_FormTaxRate);
                    return this.m_FormTaxRate;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormTaxRate))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormTaxRate>(ref this.m_FormTaxRate);
                    }
                }
            }

            public FormTips FormTips
            {
                get
                {
                    this.m_FormTips = Create__Instance__<FormTips>(this.m_FormTips);
                    return this.m_FormTips;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormTips))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<FormTips>(ref this.m_FormTips);
                    }
                }
            }

            public DMEWorks.Maintain.FormUser FormUser
            {
                get
                {
                    this.m_FormUser = Create__Instance__<DMEWorks.Maintain.FormUser>(this.m_FormUser);
                    return this.m_FormUser;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormUser))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormUser>(ref this.m_FormUser);
                    }
                }
            }

            public DMEWorks.Maintain.FormVendor FormVendor
            {
                get
                {
                    this.m_FormVendor = Create__Instance__<DMEWorks.Maintain.FormVendor>(this.m_FormVendor);
                    return this.m_FormVendor;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormVendor))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormVendor>(ref this.m_FormVendor);
                    }
                }
            }

            public DMEWorks.Maintain.FormWarehouse FormWarehouse
            {
                get
                {
                    this.m_FormWarehouse = Create__Instance__<DMEWorks.Maintain.FormWarehouse>(this.m_FormWarehouse);
                    return this.m_FormWarehouse;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormWarehouse))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormWarehouse>(ref this.m_FormWarehouse);
                    }
                }
            }

            public DMEWorks.Maintain.FormZipCode FormZipCode
            {
                get
                {
                    this.m_FormZipCode = Create__Instance__<DMEWorks.Maintain.FormZipCode>(this.m_FormZipCode);
                    return this.m_FormZipCode;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FormZipCode))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Maintain.FormZipCode>(ref this.m_FormZipCode);
                    }
                }
            }

            public frmAbout frmAbout
            {
                get
                {
                    this.m_frmAbout = Create__Instance__<frmAbout>(this.m_frmAbout);
                    return this.m_frmAbout;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_frmAbout))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<frmAbout>(ref this.m_frmAbout);
                    }
                }
            }

            public DMEWorks.Forms.VBDateBox VBDateBox
            {
                get
                {
                    this.m_VBDateBox = Create__Instance__<DMEWorks.Forms.VBDateBox>(this.m_VBDateBox);
                    return this.m_VBDateBox;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_VBDateBox))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.VBDateBox>(ref this.m_VBDateBox);
                    }
                }
            }

            public DMEWorks.Forms.VBInputBox VBInputBox
            {
                get
                {
                    this.m_VBInputBox = Create__Instance__<DMEWorks.Forms.VBInputBox>(this.m_VBInputBox);
                    return this.m_VBInputBox;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_VBInputBox))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.VBInputBox>(ref this.m_VBInputBox);
                    }
                }
            }

            public DMEWorks.Forms.VBSelectBox VBSelectBox
            {
                get
                {
                    this.m_VBSelectBox = Create__Instance__<DMEWorks.Forms.VBSelectBox>(this.m_VBSelectBox);
                    return this.m_VBSelectBox;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_VBSelectBox))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.VBSelectBox>(ref this.m_VBSelectBox);
                    }
                }
            }

            public DMEWorks.Forms.WizardInventoryTransfer WizardInventoryTransfer
            {
                get
                {
                    this.m_WizardInventoryTransfer = Create__Instance__<DMEWorks.Forms.WizardInventoryTransfer>(this.m_WizardInventoryTransfer);
                    return this.m_WizardInventoryTransfer;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_WizardInventoryTransfer))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.WizardInventoryTransfer>(ref this.m_WizardInventoryTransfer);
                    }
                }
            }

            public DMEWorks.Forms.WizardReturnSales WizardReturnSales
            {
                get
                {
                    this.m_WizardReturnSales = Create__Instance__<DMEWorks.Forms.WizardReturnSales>(this.m_WizardReturnSales);
                    return this.m_WizardReturnSales;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_WizardReturnSales))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.WizardReturnSales>(ref this.m_WizardReturnSales);
                    }
                }
            }

            public DMEWorks.Forms.WizardSelectKit WizardSelectKit
            {
                get
                {
                    this.m_WizardSelectKit = Create__Instance__<DMEWorks.Forms.WizardSelectKit>(this.m_WizardSelectKit);
                    return this.m_WizardSelectKit;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_WizardSelectKit))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<DMEWorks.Forms.WizardSelectKit>(ref this.m_WizardSelectKit);
                    }
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), MyGroupCollection("System.Web.Services.Protocols.SoapHttpClientProtocol", "Create__Instance__", "Dispose__Instance__", "")]
        internal sealed class MyWebServices
        {
            [EditorBrowsable(EditorBrowsableState.Never)]
            public Ups.GroundShipping.FreightShipService m_FreightShipService;

            [DebuggerHidden]
            private static T Create__Instance__<T>(T instance) where T: new() => 
                (instance != null) ? instance : Activator.CreateInstance<T>();

            [DebuggerHidden]
            private void Dispose__Instance__<T>(ref T instance)
            {
                instance = default(T);
            }

            [EditorBrowsable(EditorBrowsableState.Never), DebuggerHidden]
            public override bool Equals(object o) => 
                base.Equals(o);

            [EditorBrowsable(EditorBrowsableState.Never), DebuggerHidden]
            public override int GetHashCode() => 
                base.GetHashCode();

            [EditorBrowsable(EditorBrowsableState.Never), DebuggerHidden]
            internal Type GetType() => 
                typeof(My.MyProject.MyWebServices);

            [EditorBrowsable(EditorBrowsableState.Never), DebuggerHidden]
            public override string ToString() => 
                base.ToString();

            public Ups.GroundShipping.FreightShipService FreightShipService
            {
                get
                {
                    this.m_FreightShipService = Create__Instance__<Ups.GroundShipping.FreightShipService>(this.m_FreightShipService);
                    return this.m_FreightShipService;
                }
                set
                {
                    if (!ReferenceEquals(value, this.m_FreightShipService))
                    {
                        if (value != null)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.Dispose__Instance__<Ups.GroundShipping.FreightShipService>(ref this.m_FreightShipService);
                    }
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), ComVisible(false)]
        internal sealed class ThreadSafeObjectProvider<T> where T: new()
        {
            [CompilerGenerated, ThreadStatic]
            private static T m_ThreadStaticValue;

            internal T GetInstance
            {
                [DebuggerHidden]
                get
                {
                    if (My.MyProject.ThreadSafeObjectProvider<T>.m_ThreadStaticValue == null)
                    {
                        My.MyProject.ThreadSafeObjectProvider<T>.m_ThreadStaticValue = Activator.CreateInstance<T>();
                    }
                    return My.MyProject.ThreadSafeObjectProvider<T>.m_ThreadStaticValue;
                }
            }
        }
    }
}

