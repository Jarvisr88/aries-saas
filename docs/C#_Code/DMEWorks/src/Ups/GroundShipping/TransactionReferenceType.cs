namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/Common/v1.0")]
    public class TransactionReferenceType
    {
        private string customerContextField;
        private string transactionIdentifierField;

        public string CustomerContext
        {
            get => 
                this.customerContextField;
            set => 
                this.customerContextField = value;
        }

        public string TransactionIdentifier
        {
            get => 
                this.transactionIdentifierField;
            set => 
                this.transactionIdentifierField = value;
        }
    }
}

