namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/Error/v1.1")]
    public class LocationType
    {
        private string locationElementNameField;
        private string xPathOfElementField;
        private string originalValueField;

        public string LocationElementName
        {
            get => 
                this.locationElementNameField;
            set => 
                this.locationElementNameField = value;
        }

        public string XPathOfElement
        {
            get => 
                this.xPathOfElementField;
            set => 
                this.xPathOfElementField = value;
        }

        public string OriginalValue
        {
            get => 
                this.originalValueField;
            set => 
                this.originalValueField = value;
        }
    }
}

