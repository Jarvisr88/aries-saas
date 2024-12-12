namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class DangerousGoodsType
    {
        private string nameField;
        private FreightShipPhoneType phoneField;
        private ShipCodeDescriptionType transportationModeField;

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

        public ShipCodeDescriptionType TransportationMode
        {
            get => 
                this.transportationModeField;
            set => 
                this.transportationModeField = value;
        }
    }
}

