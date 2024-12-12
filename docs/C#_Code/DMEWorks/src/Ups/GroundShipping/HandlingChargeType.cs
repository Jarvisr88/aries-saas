namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class HandlingChargeType
    {
        private string percentageField;
        private HandlingChargeAmountType amountField;

        public string Percentage
        {
            get => 
                this.percentageField;
            set => 
                this.percentageField = value;
        }

        public HandlingChargeAmountType Amount
        {
            get => 
                this.amountField;
            set => 
                this.amountField = value;
        }
    }
}

