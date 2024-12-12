namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class ReferenceType
    {
        private ReferenceNumberType numberField;
        private string barCodeIndicatorField;
        private string numberOfCartonsField;
        private WeightType weightField;

        public ReferenceNumberType Number
        {
            get => 
                this.numberField;
            set => 
                this.numberField = value;
        }

        public string BarCodeIndicator
        {
            get => 
                this.barCodeIndicatorField;
            set => 
                this.barCodeIndicatorField = value;
        }

        public string NumberOfCartons
        {
            get => 
                this.numberOfCartonsField;
            set => 
                this.numberOfCartonsField = value;
        }

        public WeightType Weight
        {
            get => 
                this.weightField;
            set => 
                this.weightField = value;
        }
    }
}

