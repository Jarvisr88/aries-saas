namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(AnonymousType=true, Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class FreightShipResponse
    {
        private ResponseType responseField;
        private ShipmentResultsType shipmentResultsField;

        [XmlElement(Namespace="http://www.ups.com/XMLSchema/XOLTWS/Common/v1.0")]
        public ResponseType Response
        {
            get => 
                this.responseField;
            set => 
                this.responseField = value;
        }

        public ShipmentResultsType ShipmentResults
        {
            get => 
                this.shipmentResultsField;
            set => 
                this.shipmentResultsField = value;
        }
    }
}

