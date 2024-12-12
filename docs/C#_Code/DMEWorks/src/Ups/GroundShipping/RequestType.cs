namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/Common/v1.0")]
    public class RequestType
    {
        private string[] requestOptionField;
        private TransactionReferenceType transactionReferenceField;

        [XmlElement("RequestOption")]
        public string[] RequestOption
        {
            get => 
                this.requestOptionField;
            set => 
                this.requestOptionField = value;
        }

        public TransactionReferenceType TransactionReference
        {
            get => 
                this.transactionReferenceField;
            set => 
                this.transactionReferenceField = value;
        }
    }
}

