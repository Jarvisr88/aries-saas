namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/IF/v1.0")]
    public class PhoneType
    {
        private string numberField;
        private string extensionField;

        public string Number
        {
            get => 
                this.numberField;
            set => 
                this.numberField = value;
        }

        public string Extension
        {
            get => 
                this.extensionField;
            set => 
                this.extensionField = value;
        }
    }
}

