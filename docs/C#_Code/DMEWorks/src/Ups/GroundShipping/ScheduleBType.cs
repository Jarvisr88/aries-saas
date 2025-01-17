﻿namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/IF/v1.0")]
    public class ScheduleBType
    {
        private string numberField;
        private string quantityField;
        private UnitOfMeasurementType unitOfMeasurementField;

        public string Number
        {
            get => 
                this.numberField;
            set => 
                this.numberField = value;
        }

        public string Quantity
        {
            get => 
                this.quantityField;
            set => 
                this.quantityField = value;
        }

        public UnitOfMeasurementType UnitOfMeasurement
        {
            get => 
                this.unitOfMeasurementField;
            set => 
                this.unitOfMeasurementField = value;
        }
    }
}

