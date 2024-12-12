﻿namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class PaymentInformationType
    {
        private PayerType payerField;
        private ShipCodeDescriptionType shipmentBillingOptionField;

        public PayerType Payer
        {
            get => 
                this.payerField;
            set => 
                this.payerField = value;
        }

        public ShipCodeDescriptionType ShipmentBillingOption
        {
            get => 
                this.shipmentBillingOptionField;
            set => 
                this.shipmentBillingOptionField = value;
        }
    }
}
