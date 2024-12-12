namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(AnonymousType=true, Namespace="http://www.ups.com/XMLSchema/XOLTWS/UPSS/v1.0")]
    public class UPSSecurityServiceAccessToken
    {
        private string accessLicenseNumberField;

        public string AccessLicenseNumber
        {
            get => 
                this.accessLicenseNumberField;
            set => 
                this.accessLicenseNumberField = value;
        }
    }
}

