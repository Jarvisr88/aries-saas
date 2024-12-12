﻿namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/IF/v1.0")]
    public class SoldToType : CompanyInfoType
    {
        private string optionField;
        private AddressType addressField;

        public string Option
        {
            get => 
                this.optionField;
            set => 
                this.optionField = value;
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

