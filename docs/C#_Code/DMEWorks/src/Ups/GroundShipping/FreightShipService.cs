namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Web.Services;
    using System.Web.Services.Description;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;

    [GeneratedCode("System.Web.Services", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), WebServiceBinding(Name="FreightShipBinding", Namespace="http://www.ups.com/WSDL/XOLTWS/FreightShip/v1.1"), XmlInclude(typeof(ErrorDetailType)), XmlInclude(typeof(AmountType)), XmlInclude(typeof(ValidAccessorialType)), XmlInclude(typeof(ValidServiceType)), XmlInclude(typeof(HoldAtAirportForPickupType)), XmlInclude(typeof(CompanyInfoType))]
    public class FreightShipService : SoapHttpClientProtocol
    {
        private UPSSecurity uPSSecurityValueField;
        private SendOrPostCallback ProcessShipmentOperationCompleted;
        private bool useDefaultCredentialsSetExplicitly;

        public event ProcessShipmentCompletedEventHandler ProcessShipmentCompleted;

        public FreightShipService()
        {
            this.Url = "https://wwwcie.ups.com/webservices/FreightShip";
            if (!this.IsLocalFileSystemWebService(this.Url))
            {
                this.useDefaultCredentialsSetExplicitly = true;
            }
            else
            {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
        }

        public void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }

        private bool IsLocalFileSystemWebService(string url)
        {
            bool flag;
            if ((url == null) || ReferenceEquals(url, string.Empty))
            {
                flag = false;
            }
            else
            {
                Uri uri = new Uri(url);
                flag = (uri.Port >= 0x400) && (string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0);
            }
            return flag;
        }

        private void OnProcessShipmentOperationCompleted(object arg)
        {
            if (this.ProcessShipmentCompletedEvent != null)
            {
                InvokeCompletedEventArgs args = (InvokeCompletedEventArgs) arg;
                ProcessShipmentCompletedEventHandler processShipmentCompletedEvent = this.ProcessShipmentCompletedEvent;
                if (processShipmentCompletedEvent != null)
                {
                    processShipmentCompletedEvent(this, new ProcessShipmentCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
                }
            }
        }

        [return: XmlElement("FreightShipResponse", Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
        [SoapHeader("UPSSecurityValue"), SoapDocumentMethod("http://onlinetools.ups.com/webservices/FreightShipBinding/v1.1", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Bare)]
        public FreightShipResponse ProcessShipment([XmlElement(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")] FreightShipRequest FreightShipRequest)
        {
            object[] parameters = new object[] { FreightShipRequest };
            return (FreightShipResponse) base.Invoke("ProcessShipment", parameters)[0];
        }

        public void ProcessShipmentAsync(FreightShipRequest FreightShipRequest)
        {
            this.ProcessShipmentAsync(FreightShipRequest, null);
        }

        public void ProcessShipmentAsync(FreightShipRequest FreightShipRequest, object userState)
        {
            this.ProcessShipmentOperationCompleted ??= new SendOrPostCallback(this.OnProcessShipmentOperationCompleted);
            object[] parameters = new object[] { FreightShipRequest };
            base.InvokeAsync("ProcessShipment", parameters, this.ProcessShipmentOperationCompleted, userState);
        }

        public UPSSecurity UPSSecurityValue
        {
            get => 
                this.uPSSecurityValueField;
            set => 
                this.uPSSecurityValueField = value;
        }

        public string Url
        {
            get => 
                base.Url;
            set
            {
                if (this.IsLocalFileSystemWebService(base.Url) && (!this.useDefaultCredentialsSetExplicitly && !this.IsLocalFileSystemWebService(value)))
                {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }

        public bool UseDefaultCredentials
        {
            get => 
                base.UseDefaultCredentials;
            set
            {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
    }
}

