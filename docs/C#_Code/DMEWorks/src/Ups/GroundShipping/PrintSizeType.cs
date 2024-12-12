namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class PrintSizeType
    {
        private string lengthField;
        private string widthField;

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
    }
}

