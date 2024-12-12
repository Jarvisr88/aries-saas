namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(AnonymousType=true, Namespace="http://www.ups.com/XMLSchema/XOLTWS/UPSS/v1.0"), XmlRoot(Namespace="http://www.ups.com/XMLSchema/XOLTWS/UPSS/v1.0", IsNullable=false)]
    public class UPSSecurity : SoapHeader
    {
        private UPSSecurityUsernameToken usernameTokenField;
        private UPSSecurityServiceAccessToken serviceAccessTokenField;

        public UPSSecurityUsernameToken UsernameToken
        {
            get => 
                this.usernameTokenField;
            set => 
                this.usernameTokenField = value;
        }

        public UPSSecurityServiceAccessToken ServiceAccessToken
        {
            get => 
                this.serviceAccessTokenField;
            set => 
                this.serviceAccessTokenField = value;
        }
    }
}

