namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(AnonymousType=true, Namespace="http://www.ups.com/XMLSchema/XOLTWS/UPSS/v1.0")]
    public class UPSSecurityUsernameToken
    {
        private string usernameField;
        private string passwordField;

        public string Username
        {
            get => 
                this.usernameField;
            set => 
                this.usernameField = value;
        }

        public string Password
        {
            get => 
                this.passwordField;
            set => 
                this.passwordField = value;
        }
    }
}

