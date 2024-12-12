namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(AnonymousType=true, Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class FreightShipRequest
    {
        private RequestType requestField;
        private ShipmentType shipmentField;

        [XmlElement(Namespace="http://www.ups.com/XMLSchema/XOLTWS/Common/v1.0")]
        public RequestType Request
        {
            get => 
                this.requestField;
            set => 
                this.requestField = value;
        }

        public ShipmentType Shipment
        {
            get => 
                this.shipmentField;
            set => 
                this.shipmentField = value;
        }
    }
}

