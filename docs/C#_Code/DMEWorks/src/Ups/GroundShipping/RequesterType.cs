namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class RequesterType
    {
        private string thirdPartyIndicatorField;
        private string attentionNameField;
        private string eMailAddressField;
        private string nameField;
        private FreightShipPhoneType phoneField;

        public string ThirdPartyIndicator
        {
            get => 
                this.thirdPartyIndicatorField;
            set => 
                this.thirdPartyIndicatorField = value;
        }

        public string AttentionName
        {
            get => 
                this.attentionNameField;
            set => 
                this.attentionNameField = value;
        }

        public string EMailAddress
        {
            get => 
                this.eMailAddressField;
            set => 
                this.eMailAddressField = value;
        }

        public string Name
        {
            get => 
                this.nameField;
            set => 
                this.nameField = value;
        }

        public FreightShipPhoneType Phone
        {
            get => 
                this.phoneField;
            set => 
                this.phoneField = value;
        }
    }
}

