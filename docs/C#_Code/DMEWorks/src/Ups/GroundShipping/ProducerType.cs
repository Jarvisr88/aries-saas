namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/IF/v1.0")]
    public class ProducerType
    {
        private string optionField;
        private string companyNameField;
        private string taxIdentificationNumberField;
        private AddressType addressField;

        public string Option
        {
            get => 
                this.optionField;
            set => 
                this.optionField = value;
        }

        public string CompanyName
        {
            get => 
                this.companyNameField;
            set => 
                this.companyNameField = value;
        }

        public string TaxIdentificationNumber
        {
            get => 
                this.taxIdentificationNumberField;
            set => 
                this.taxIdentificationNumberField = value;
        }

        public AddressType Address
        {
            get => 
                this.addressField;
            set => 
                this.addressField = value;
        }
    }
}

