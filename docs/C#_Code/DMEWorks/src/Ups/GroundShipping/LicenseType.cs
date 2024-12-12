namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/IF/v1.0")]
    public class LicenseType
    {
        private string numberField;
        private string dateField;
        private string exceptionCodeField;

        public string Number
        {
            get => 
                this.numberField;
            set => 
                this.numberField = value;
        }

        public string Date
        {
            get => 
                this.dateField;
            set => 
                this.dateField = value;
        }

        public string ExceptionCode
        {
            get => 
                this.exceptionCodeField;
            set => 
                this.exceptionCodeField = value;
        }
    }
}

