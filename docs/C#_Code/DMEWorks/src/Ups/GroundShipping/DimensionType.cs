namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class DimensionType
    {
        private string volumeField;
        private string heightField;
        private string lengthField;
        private string widthField;
        private FreightShipUnitOfMeasurementType unitOfMeasurementField;

        public string Volume
        {
            get => 
                this.volumeField;
            set => 
                this.volumeField = value;
        }

        public string Height
        {
            get => 
                this.heightField;
            set => 
                this.heightField = value;
        }

        public string Length
        {
            get => 
                this.lengthField;
            set => 
                this.lengthField = value;
        }

        public string Width
        {
            get => 
                this.widthField;
            set => 
                this.widthField = value;
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

