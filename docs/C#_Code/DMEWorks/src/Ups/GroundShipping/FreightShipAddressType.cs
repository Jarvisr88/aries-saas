namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class FreightShipAddressType
    {
        private string[] addressLineField;
        private string cityField;
        private string stateProvinceCodeField;
        private string townField;
        private string postalCodeField;
        private string countryCodeField;

        [XmlElement("AddressLine")]
        public string[] AddressLine
        {
            get => 
                this.addressLineField;
            set => 
                this.addressLineField = value;
        }

        public string City
        {
            get => 
                this.cityField;
            set => 
                this.cityField = value;
        }

        public string StateProvinceCode
        {
            get => 
                this.stateProvinceCodeField;
            set => 
                this.stateProvinceCodeField = value;
        }

        public string Town
        {
            get => 
                this.townField;
            set => 
                this.townField = value;
        }

        public string PostalCode
        {
            get => 
                this.postalCodeField;
            set => 
                this.postalCodeField = value;
        }

        public string CountryCode
        {
            get => 
                this.countryCodeField;
            set => 
                this.countryCodeField = value;
        }
    }
}

