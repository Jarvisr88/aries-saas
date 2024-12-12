namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class TaxIDType
    {
        private TaxIDCodeDescType typeField;
        private string numberField;

        public TaxIDCodeDescType Type
        {
            get => 
                this.typeField;
            set => 
                this.typeField = value;
        }

        public string Number
        {
            get => 
                this.numberField;
            set => 
                this.numberField = value;
        }
    }
}

