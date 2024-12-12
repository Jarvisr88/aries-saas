namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class EMailType
    {
        private string[] eMailAddressField;
        private string eMailTextField;
        private string undeliverableEMailAddressField;
        private string subjectField;

        [XmlElement("EMailAddress")]
        public string[] EMailAddress
        {
            get => 
                this.eMailAddressField;
            set => 
                this.eMailAddressField = value;
        }

        public string EMailText
        {
            get => 
                this.eMailTextField;
            set => 
                this.eMailTextField = value;
        }

        public string UndeliverableEMailAddress
        {
            get => 
                this.undeliverableEMailAddressField;
            set => 
                this.undeliverableEMailAddressField = value;
        }

        public string Subject
        {
            get => 
                this.subjectField;
            set => 
                this.subjectField = value;
        }
    }
}

