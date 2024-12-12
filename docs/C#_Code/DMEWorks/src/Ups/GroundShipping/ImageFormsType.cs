namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class ImageFormsType
    {
        private ImageCodeDescriptionType typeField;
        private string graphicImageField;
        private ImageCodeDescriptionType formatField;

        public ImageCodeDescriptionType Type
        {
            get => 
                this.typeField;
            set => 
                this.typeField = value;
        }

        public string GraphicImage
        {
            get => 
                this.graphicImageField;
            set => 
                this.graphicImageField = value;
        }

        public ImageCodeDescriptionType Format
        {
            get => 
                this.formatField;
            set => 
                this.formatField = value;
        }
    }
}

