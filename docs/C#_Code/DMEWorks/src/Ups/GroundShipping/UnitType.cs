namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/IF/v1.0")]
    public class UnitType
    {
        private string numberField;
        private UnitOfMeasurementType unitOfMeasurementField;
        private string valueField;

        public string Number
        {
            get => 
                this.numberField;
            set => 
                this.numberField = value;
        }

        public UnitOfMeasurementType UnitOfMeasurement
        {
            get => 
                this.unitOfMeasurementField;
            set => 
                this.unitOfMeasurementField = value;
        }

        public string Value
        {
            get => 
                this.valueField;
            set => 
                this.valueField = value;
        }
    }
}

