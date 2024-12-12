namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class ShipmentTotalWeightType
    {
        private string valueField;
        private FreightShipUnitOfMeasurementType unitOfMeasurementField;

        public string Value
        {
            get => 
                this.valueField;
            set => 
                this.valueField = value;
        }

        public FreightShipUnitOfMeasurementType UnitOfMeasurement
        {
            get => 
                this.unitOfMeasurementField;
            set => 
                this.unitOfMeasurementField = value;
        }
    }
}

