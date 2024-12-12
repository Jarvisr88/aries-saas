namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class ExistingShipmentIDType
    {
        private string shipmentNumberField;
        private ConfirmationNumberType confirmationNumberField;
        private string creationDateField;

        public string ShipmentNumber
        {
            get => 
                this.shipmentNumberField;
            set => 
                this.shipmentNumberField = value;
        }

        public ConfirmationNumberType ConfirmationNumber
        {
            get => 
                this.confirmationNumberField;
            set => 
                this.confirmationNumberField = value;
        }

        public string CreationDate
        {
            get => 
                this.creationDateField;
            set => 
                this.creationDateField = value;
        }
    }
}

