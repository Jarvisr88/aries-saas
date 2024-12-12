namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class CODType
    {
        private CODValueType cODValueField;
        private ShipCodeDescriptionType cODPaymentMethodField;
        private ShipCodeDescriptionType cODBillingOptionField;
        private RemitToType remitToField;

        public CODValueType CODValue
        {
            get => 
                this.cODValueField;
            set => 
                this.cODValueField = value;
        }

        public ShipCodeDescriptionType CODPaymentMethod
        {
            get => 
                this.cODPaymentMethodField;
            set => 
                this.cODPaymentMethodField = value;
        }

        public ShipCodeDescriptionType CODBillingOption
        {
            get => 
                this.cODBillingOptionField;
            set => 
                this.cODBillingOptionField = value;
        }

        public RemitToType RemitTo
        {
            get => 
                this.remitToField;
            set => 
                this.remitToField = value;
        }
    }
}

