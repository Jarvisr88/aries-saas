namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class ShipFromType
    {
        private string nameField;
        private string taxIdentificationNumberField;
        private string tariffPointField;
        private FreightShipAddressType addressField;
        private string attentionNameField;
        private FreightShipPhoneType phoneField;
        private string faxNumberField;
        private string eMailAddressField;

        public string Name
        {
            get => 
                this.nameField;
            set => 
                this.nameField = value;
        }

        public string TaxIdentificationNumber
        {
            get => 
                this.taxIdentificationNumberField;
            set => 
                this.taxIdentificationNumberField = value;
        }

        public string TariffPoint
        {
            get => 
                this.tariffPointField;
            set => 
                this.tariffPointField = value;
        }

        public FreightShipAddressType Address
        {
            get => 
                this.addressField;
            set => 
                this.addressField = value;
        }

        public string AttentionName
        {
            get => 
                this.attentionNameField;
            set => 
                this.attentionNameField = value;
        }

        public FreightShipPhoneType Phone
        {
            get => 
                this.phoneField;
            set => 
                this.phoneField = value;
        }

        public string FaxNumber
        {
            get => 
                this.faxNumberField;
            set => 
                this.faxNumberField = value;
        }

        public string EMailAddress
        {
            get => 
                this.eMailAddressField;
            set => 
                this.eMailAddressField = value;
        }
    }
}

