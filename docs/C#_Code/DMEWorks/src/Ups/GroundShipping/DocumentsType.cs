namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class DocumentsType
    {
        private ImageType[] imageField;
        private PackingListType packingListField;
        private InternationalFormType internationalFormsField;

        [XmlElement("Image")]
        public ImageType[] Image
        {
            get => 
                this.imageField;
            set => 
                this.imageField = value;
        }

        public PackingListType PackingList
        {
            get => 
                this.packingListField;
            set => 
                this.packingListField = value;
        }

        public InternationalFormType InternationalForms
        {
            get => 
                this.internationalFormsField;
            set => 
                this.internationalFormsField = value;
        }
    }
}

