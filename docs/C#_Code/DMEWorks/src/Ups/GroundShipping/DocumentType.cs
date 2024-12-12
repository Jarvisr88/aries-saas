namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class DocumentType
    {
        private ImageFormsType[] imageField;
        private ImageFormsType[] formsField;

        [XmlElement("Image")]
        public ImageFormsType[] Image
        {
            get => 
                this.imageField;
            set => 
                this.imageField = value;
        }

        [XmlElement("Forms")]
        public ImageFormsType[] Forms
        {
            get => 
                this.formsField;
            set => 
                this.formsField = value;
        }
    }
}

