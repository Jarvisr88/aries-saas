namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/Common/v1.0")]
    public class ResponseType
    {
        private CodeDescriptionType responseStatusField;
        private CodeDescriptionType[] alertField;
        private TransactionReferenceType transactionReferenceField;

        public CodeDescriptionType ResponseStatus
        {
            get => 
                this.responseStatusField;
            set => 
                this.responseStatusField = value;
        }

        [XmlElement("Alert")]
        public CodeDescriptionType[] Alert
        {
            get => 
                this.alertField;
            set => 
                this.alertField = value;
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

