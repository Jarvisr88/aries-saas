namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class AmountType
    {
        private string currencyCodeField;
        private string monetaryValueField;

        public string CurrencyCode
        {
            get => 
                this.currencyCodeField;
            set => 
                this.currencyCodeField = value;
        }

        public string MonetaryValue
        {
            get => 
                this.monetaryValueField;
            set => 
                this.monetaryValueField = value;
        }
    }
}

