namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, XmlInclude(typeof(SoldToType)), GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/IF/v1.0")]
    public class CompanyInfoType
    {
        private string nameField;
        private string attentionNameField;
        private string taxIdentificationNumberField;
        private PhoneType phoneField;

        public string Name
        {
            get => 
                this.nameField;
            set => 
                this.nameField = value;
        }

        public string AttentionName
        {
            get => 
                this.attentionNameField;
            set => 
                this.attentionNameField = value;
        }

        public string TaxIdentificationNumber
        {
            get => 
                this.taxIdentificationNumberField;
            set => 
                this.taxIdentificationNumberField = value;
        }

        public PhoneType Phone
        {
            get => 
                this.phoneField;
            set => 
                this.phoneField = value;
        }
    }
}

