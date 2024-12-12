namespace DevExpress.ReportServer.Printing
{
    using DevExpress.DocumentServices.ServiceModel;
    using DevExpress.DocumentServices.ServiceModel.Client;
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using DevExpress.Printing;
    using DevExpress.Printing.Core.ReportServer.Services;
    using DevExpress.ReportServer.ServiceModel.Client;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Native.DrillDown;
    using DevExpress.XtraPrinting.Native.Interaction;
    using DevExpress.XtraReports;
    using DevExpress.XtraReports.Native;
    using DevExpress.XtraReports.Parameters;
    using DevExpress.XtraReports.UI;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.ServiceModel;
    using System.Threading;

    [Designer("DevExpress.XtraPrinting.Design.RemoteDocumentSourceDesigner,DevExpress.XtraPrinting.v19.2.Design"), ToolboxBitmap(typeof(ResFinder), "Bitmaps256.RemoteDocumentSource.bmp"), ToolboxItem(false)]
    public class RemoteDocumentSource : Component, IReport, IDocumentSource, ILink, IComponent, IDisposable, IServiceProvider, IExtensionsProvider
    {
        private readonly string authenticationPattern = "_Authentication";
        private readonly string useSSLPattern = "_SSL";
        private readonly string formsAuthenticationPattern = "_Forms";
        private readonly string winAuthenticationPattern = "_Win";
        private readonly string reportServerFacadePath = "ReportServerFacade.svc";
        private readonly string winAuthenticationServicePath = "WindowsAuthentication/AuthenticationService.svc";
        private readonly string formsAuthenticationServicePath = "AuthenticationService.svc";
        private RemotePrintingSystem ps;
        private string reportName;
        private InstanceIdentity reportIdentity;
        private string serviceUri;
        private bool needToLogin = true;
        private DevExpress.ReportServer.Printing.AuthenticationType authenticationType;
        private string endpointConfigurationPrefix;
        private FormsAuthenticationEndpointBehavior behavior;
        private IParameterContainer parameters;
        private IReportPrintTool printTool;
        private IDrillDownServiceBase drillDownService;
        private IInteractionService interactionService;
        private IDictionary<string, string> extensions = new Dictionary<string, string>();

        [Category("Printing")]
        public event EventHandler<AuthenticationServiceClientDemandedEventArgs> AuthenticationServiceClientDemanded;

        event EventHandler<ParametersRequestEventArgs> IReport.ParametersRequestSubmit
        {
            add
            {
            }
            remove
            {
            }
        }

        [Category("Printing")]
        public event EventHandler<ParametersRequestEventArgs> ParametersRequestBeforeShow;

        [Category("Printing")]
        public event EventHandler<ParametersRequestValueChangedEventArgs> ParametersRequestValueChanged;

        [Category("Printing")]
        public event CredentialsDemandedEventHandler ReportServerCredentialsDemanded;

        [Category("Printing")]
        public event EventHandler<ReportServiceClientDemandedEventArgs> ReportServiceClientDemanded;

        private static bool CheckUseSSL(string url) => 
            new Uri(url).Scheme == Uri.UriSchemeHttps;

        private void Clear()
        {
            this.parameters = null;
        }

        private IAuthenticationServiceClient CreateAuthenticationClient()
        {
            AuthenticationServiceClientFactory factory;
            if (this.AuthenticationServiceClientDemanded != null)
            {
                AuthenticationServiceClientDemandedEventArgs e = new AuthenticationServiceClientDemandedEventArgs();
                this.AuthenticationServiceClientDemanded(this, e);
                if (e.Client != null)
                {
                    return e.Client;
                }
            }
            if (!string.IsNullOrEmpty(this.EndpointConfigurationPrefix))
            {
                EndpointAddress endpointAddress = this.GetEndpointAddress(true);
                factory = new AuthenticationServiceClientFactory(this.GetEndpointName(ContractType.IAuthenticationService, endpointAddress), endpointAddress);
            }
            else
            {
                this.behavior = new FormsAuthenticationEndpointBehavior();
                factory = new AuthenticationServiceClientFactory(this.GetEndpointAddress(true), this.AuthenticationType);
                factory.ChannelFactory.Endpoint.EndpointBehaviors.Add(this.behavior);
            }
            return factory.Create();
        }

        public void CreateDocument()
        {
            this.Clear();
            this.CreateDocumentCore();
        }

        public void CreateDocument(DefaultValueParameterContainer defaultParameterValues)
        {
            this.Clear();
            this.parameters = defaultParameterValues;
            this.CreateDocumentCore();
        }

        private void CreateDocumentCore()
        {
            if ((this.ReportIdentity == null) && string.IsNullOrWhiteSpace(this.ReportName))
            {
                this.RaiseCreateDocumentError(new InvalidOperationException("It is impossible to create a document because neither ReportIdentity nor ReportName properties are set."));
            }
            this.reportIdentity ??= new ReportNameIdentity(this.ReportName);
            if ((this.AuthenticationType != DevExpress.ReportServer.Printing.AuthenticationType.None) && this.NeedToLogin)
            {
                this.Login();
            }
            else
            {
                this.RequestRemoteDocument();
            }
        }

        private RemotePrintingSystem CreatePrintingSystem()
        {
            RemotePrintingSystem container = new RemotePrintingSystem();
            container.AddService<IReport>(this);
            return container;
        }

        protected virtual IReportServiceClient CreateReportServiceClient()
        {
            IReportServiceClientFactory factory;
            if (this.ReportServiceClientDemanded != null)
            {
                ReportServiceClientDemandedEventArgs e = new ReportServiceClientDemandedEventArgs();
                this.ReportServiceClientDemanded(this, e);
                if (e.Client != null)
                {
                    return e.Client;
                }
            }
            EndpointAddress endpointAddress = new EndpointAddress(this.ServiceUri);
            if (this.AuthenticationType == DevExpress.ReportServer.Printing.AuthenticationType.None)
            {
                factory = new ReportServiceClientFactory(new EndpointAddress(this.ServiceUri));
            }
            else if (string.IsNullOrEmpty(this.EndpointConfigurationPrefix))
            {
                factory = new ReportServerClientFactory(this.GetEndpointAddress(false));
                ((ReportServerClientFactory) factory).ChannelFactory.Endpoint.EndpointBehaviors.Add(this.behavior);
            }
            else
            {
                if (string.IsNullOrEmpty(this.GetEndpointName(ContractType.IReportServerFacadeAsync, endpointAddress)))
                {
                    throw new InvalidOperationException("Endpoint configuration name are not specified.");
                }
                factory = new ReportServerClientFactory(this.GetEndpointName(ContractType.IAsyncReportService, endpointAddress), this.GetEndpointAddress(false));
            }
            return factory.Create();
        }

        private void CredentialsService_RequestCredentialsFailed(object sender, EventArgs e)
        {
            this.CredentialsService.RequestCredentialsFailed -= new EventHandler(this.CredentialsService_RequestCredentialsFailed);
        }

        void ILink.CreateDocument(bool buildForInstantPreview)
        {
            this.CreateDocumentCore();
        }

        void IReport.ApplyExportOptions(ExportOptions destinationOptions)
        {
            throw new NotSupportedException();
        }

        void IReport.ApplyPageSettings(XtraPageSettingsBase destSettings)
        {
            throw new NotImplementedException();
        }

        void IReport.CollectParameters(IList<Parameter> list, Predicate<Parameter> condition)
        {
        }

        void IReport.InitializeDocumentCreation()
        {
        }

        void IReport.RaiseParametersRequestBeforeShow(IList<ParameterInfo> parametersInfo)
        {
            if (this.ParametersRequestBeforeShow != null)
            {
                this.ParametersRequestBeforeShow(this, new ParametersRequestEventArgs(parametersInfo));
            }
        }

        void IReport.RaiseParametersRequestSubmit(IList<ParameterInfo> parametersInfo, bool createDocument)
        {
            try
            {
                this.PrintingSystem.SubmitParameters();
            }
            catch (Exception exception)
            {
                if (this.RaiseCreateDocumentException(exception))
                {
                    throw;
                }
            }
        }

        void IReport.RaiseParametersRequestValueChanged(IList<ParameterInfo> parametersInfo, ParameterInfo changedParameterInfo)
        {
            if (this.ParametersRequestValueChanged != null)
            {
                this.ParametersRequestValueChanged(this, new ParametersRequestValueChangedEventArgs(parametersInfo, changedParameterInfo));
            }
        }

        void IReport.ReleasePrintingSystem()
        {
            RemotePrintingSystem printingSystem = this.PrintingSystem;
            this.ps = null;
            this.parameters = printingSystem.RemoteDocument.ParameterContainer;
        }

        void IReport.StopPageBuilding()
        {
            throw new NotImplementedException();
        }

        void IReport.UpdatePageSettings(XtraPageSettingsBase sourceSettings, string paperName)
        {
            throw new NotImplementedException();
        }

        protected virtual EndpointAddress GetEndpointAddress(bool isAuthenticationServiceEnpoint)
        {
            Uri baseUri = new Uri(this.ServiceUri);
            return (!isAuthenticationServiceEnpoint ? new EndpointAddress(new Uri(baseUri, this.reportServerFacadePath), new AddressHeader[0]) : ((this.AuthenticationType == DevExpress.ReportServer.Printing.AuthenticationType.Windows) ? new EndpointAddress(new Uri(baseUri, this.winAuthenticationServicePath), new AddressHeader[0]) : new EndpointAddress(new Uri(baseUri, this.formsAuthenticationServicePath), new AddressHeader[0])));
        }

        protected virtual string GetEndpointName(ContractType contractType, EndpointAddress endpointAddress)
        {
            if (this.AuthenticationType == DevExpress.ReportServer.Printing.AuthenticationType.None)
            {
                return string.Empty;
            }
            string endpointConfigurationPrefix = this.EndpointConfigurationPrefix;
            if (contractType == ContractType.IAuthenticationService)
            {
                endpointConfigurationPrefix = endpointConfigurationPrefix + this.authenticationPattern;
                if (this.AuthenticationType == DevExpress.ReportServer.Printing.AuthenticationType.Windows)
                {
                    endpointConfigurationPrefix = endpointConfigurationPrefix + this.winAuthenticationPattern;
                }
                else if ((this.AuthenticationType == DevExpress.ReportServer.Printing.AuthenticationType.Forms) || (this.AuthenticationType == DevExpress.ReportServer.Printing.AuthenticationType.Guest))
                {
                    endpointConfigurationPrefix = endpointConfigurationPrefix + this.formsAuthenticationPattern;
                }
            }
            if (CheckUseSSL(this.ServiceUri))
            {
                endpointConfigurationPrefix = endpointConfigurationPrefix + this.useSSLPattern;
            }
            return endpointConfigurationPrefix;
        }

        private void Login()
        {
            if (this.AuthenticationType == DevExpress.ReportServer.Printing.AuthenticationType.Forms)
            {
                this.RequestCredentials();
            }
            else
            {
                this.LoginCore(null, null);
            }
        }

        private void LoginCore(string userName, string password)
        {
            if (this.PrintingSystem.GetService<PageInfoDataProviderBase>() == null)
            {
                this.PrintingSystem.AddService<PageInfoDataProviderBase>(new RemotePageInfoDataProvider(userName));
            }
            IAuthenticationServiceClient client = this.CreateAuthenticationClient();
            try
            {
                client.Login(userName, password, null, delegate (ScalarOperationCompletedEventArgs<bool> args) {
                    if (args.Error != null)
                    {
                        this.RaiseCreateDocumentError(args.Error);
                    }
                    else if (!args.Result)
                    {
                        this.RaiseCreateDocumentError(new Exception("Invalid User Credentials."));
                    }
                    else
                    {
                        this.needToLogin = false;
                        this.RequestRemoteDocument();
                    }
                });
            }
            catch (EndpointNotFoundException exception)
            {
                if (this.RaiseCreateDocumentException(exception))
                {
                    throw;
                }
            }
        }

        private void RaiseCreateDocumentError(Exception e)
        {
            if (this.PrintingSystem.RaiseCreateDocumentException(e))
            {
                throw e;
            }
        }

        private bool RaiseCreateDocumentException(Exception e) => 
            this.PrintingSystem.RaiseCreateDocumentException(e);

        private void RequestCredentials()
        {
            CredentialsEventArgs e = new CredentialsEventArgs();
            if (this.ReportServerCredentialsDemanded != null)
            {
                this.ReportServerCredentialsDemanded(this, e);
                if (e.Handled)
                {
                    this.LoginCore(e.UserName, e.Password);
                    return;
                }
            }
            if (this.CredentialsService == null)
            {
                this.RaiseCreateDocumentError(new InvalidOperationException("ReportServer credentials are not specified."));
            }
            else
            {
                this.CredentialsService.RequestCredentialsFailed += new EventHandler(this.CredentialsService_RequestCredentialsFailed);
                this.CredentialsService.RequestCredentials(new Action<string, string>(this.LoginCore));
            }
        }

        private void RequestRemoteDocument()
        {
            IReportServiceClient client;
            try
            {
                client = this.CreateReportServiceClient();
            }
            catch (Exception exception)
            {
                if (this.PrintingSystem.RaiseCreateDocumentException(exception))
                {
                    throw;
                }
                return;
            }
            this.PrintingSystem.EnsureClient(client);
            try
            {
                IParameterContainer parameters = this.parameters;
                if (this.parameters == null)
                {
                    IParameterContainer local1 = this.parameters;
                    parameters = new DefaultValueParameterContainer();
                }
                this.PrintingSystem.RequestRemoteDocument(this.ReportIdentity, parameters);
            }
            catch (Exception exception2)
            {
                if (this.RaiseCreateDocumentException(exception2))
                {
                    throw;
                }
            }
        }

        object IServiceProvider.GetService(Type serviceType)
        {
            if (serviceType == typeof(IDrillDownServiceBase))
            {
                IDrillDownServiceBase drillDownService = this.drillDownService;
                if (this.drillDownService == null)
                {
                    IDrillDownServiceBase local1 = this.drillDownService;
                    drillDownService = this.drillDownService = new RemoteDrillDownService();
                }
                return drillDownService;
            }
            if (!(serviceType == typeof(IInteractionService)))
            {
                return base.GetService(serviceType);
            }
            IInteractionService interactionService = this.interactionService;
            if (this.interactionService == null)
            {
                IInteractionService local2 = this.interactionService;
                interactionService = this.interactionService = new RemoteInteractionService();
            }
            return interactionService;
        }

        private static EndpointAddress TryCreateEndpointAddress(string uri) => 
            new EndpointAddress(uri);

        [DefaultValue((string) null), Category("Printing")]
        public string ServiceUri
        {
            get => 
                this.serviceUri;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    TryCreateEndpointAddress(value);
                }
                if (this.serviceUri != value)
                {
                    this.serviceUri = value;
                    this.needToLogin = true;
                }
            }
        }

        [DefaultValue((string) null), Browsable(false), Category("Printing")]
        public string ReportName
        {
            get => 
                this.reportName;
            set
            {
                if (!string.IsNullOrEmpty(value) && (this.ReportIdentity != null))
                {
                    ExceptionHelper.ThrowInvalidOperationException("Use either report name or report identity, but not both of them.");
                }
                else
                {
                    this.reportName = value;
                }
            }
        }

        [DefaultValue((string) null), Browsable(false), Category("Printing")]
        public InstanceIdentity ReportIdentity
        {
            get => 
                this.reportIdentity;
            set
            {
                if ((value != null) && !string.IsNullOrEmpty(this.ReportName))
                {
                    ExceptionHelper.ThrowInvalidOperationException("Use either report name or report identity, but not both of them.");
                }
                else
                {
                    this.reportIdentity = value;
                }
            }
        }

        [DefaultValue(0), Category("Printing")]
        public DevExpress.ReportServer.Printing.AuthenticationType AuthenticationType
        {
            get => 
                this.authenticationType;
            set
            {
                if (this.authenticationType != value)
                {
                    this.authenticationType = value;
                    this.needToLogin = true;
                }
            }
        }

        [Category("Printing"), DefaultValue((string) null)]
        public string EndpointConfigurationPrefix
        {
            get => 
                this.endpointConfigurationPrefix;
            set => 
                this.endpointConfigurationPrefix = value;
        }

        protected bool NeedToLogin =>
            this.needToLogin && (this.AuthenticationType != DevExpress.ReportServer.Printing.AuthenticationType.None);

        private ICredentialsService CredentialsService =>
            this.PrintingSystem.GetService<ICredentialsService>();

        protected RemotePrintingSystem PrintingSystem =>
            ((IDocumentSource) this).PrintingSystemBase as RemotePrintingSystem;

        PrintingSystemBase IDocumentSource.PrintingSystemBase
        {
            get
            {
                this.ps ??= this.CreatePrintingSystem();
                return this.ps;
            }
        }

        IPrintingSystem ILink.PrintingSystem =>
            this.PrintingSystem;

        EventHandlerList IReport.Events =>
            null;

        Func<PrintingSystemBase, PrintingDocument> IReport.InstantiateDocument
        {
            get => 
                null;
            set
            {
                throw new NotSupportedException();
            }
        }

        Watermark IReport.Watermark
        {
            get
            {
                throw new NotSupportedException("Cannot edit the watermark settings of a remote document.");
            }
        }

        bool IReport.IsDisposed =>
            false;

        bool IReport.ShowPreviewMarginLines =>
            false;

        IReportPrintTool IReport.PrintTool
        {
            get => 
                this.printTool;
            set => 
                this.printTool = value;
        }

        bool IReport.RequestParameters =>
            false;

        IDictionary<string, string> IExtensionsProvider.Extensions =>
            this.extensions;

        public enum ContractType
        {
            IAsyncReportService,
            IReportServerFacadeAsync,
            IAuthenticationService
        }

        private class RemotePageInfoDataProvider : PageInfoDataProviderBase
        {
            private readonly string userName;

            public RemotePageInfoDataProvider(string userName)
            {
                this.userName = userName;
            }

            public override string GetText(PrintingSystemBase ps, PageInfoTextBrickBase brick) => 
                (brick.PageInfo != PageInfo.UserName) ? null : this.userName;
        }
    }
}

