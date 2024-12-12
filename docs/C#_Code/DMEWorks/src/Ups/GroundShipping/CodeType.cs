namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/Error/v1.1")]
    public class CodeType
    {
        private string codeField;
        private string descriptionField;
        private string digestField;

        public string Code
        {
            get => 
                this.codeField;
            set => 
                this.codeField = value;
        }

        public string Description
        {
            get => 
                this.descriptionField;
            set => 
                this.descriptionField = value;
        }

        public string Digest
        {
            get => 
                this.digestField;
            set => 
                this.digestField = value;
        }
    }
}

