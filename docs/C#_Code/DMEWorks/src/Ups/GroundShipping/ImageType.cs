namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class ImageType
    {
        private ShipCodeDescriptionType typeField;
        private string labelsPerPageField;
        private ShipCodeDescriptionType formatField;
        private ShipCodeDescriptionType printFormatField;
        private PrintSizeType printSizeField;

        public ShipCodeDescriptionType Type
        {
            get => 
                this.typeField;
            set => 
                this.typeField = value;
        }

        public string LabelsPerPage
        {
            get => 
                this.labelsPerPageField;
            set => 
                this.labelsPerPageField = value;
        }

        public ShipCodeDescriptionType Format
        {
            get => 
                this.formatField;
            set => 
                this.formatField = value;
        }

        public ShipCodeDescriptionType PrintFormat
        {
            get => 
                this.printFormatField;
            set => 
                this.printFormatField = value;
        }

        public PrintSizeType PrintSize
        {
            get => 
                this.printSizeField;
            set => 
                this.printSizeField = value;
        }
    }
}

