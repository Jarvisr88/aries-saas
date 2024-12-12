namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class PackingListContactType
    {
        private string nameField;
        private string attentionNameField;
        private FreightShipAddressType addressField;
        private string phoneNumberField;
        private string eMailAddressField;
        private string phoneExtensionField;

        public string Name
        {
            get => 
                this.nameField;
            set => 
                this.nameField = value;
        }

        public string AttentionName
        {
            get => 
                this.attentionNameField;
            set => 
                this.attentionNameField = value;
        }

        public FreightShipAddressType Address
        {
            get => 
                this.addressField;
            set => 
                this.addressField = value;
        }

        public string PhoneNumber
        {
            get => 
                this.phoneNumberField;
            set => 
                this.phoneNumberField = value;
        }

        public string EMailAddress
        {
            get => 
                this.eMailAddressField;
            set => 
                this.eMailAddressField = value;
        }

        public string PhoneExtension
        {
            get => 
                this.phoneExtensionField;
            set => 
                this.phoneExtensionField = value;
        }
    }
}

