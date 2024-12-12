namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class NMFCCommodityType
    {
        private string primeCodeField;
        private string subCodeField;

        public string PrimeCode
        {
            get => 
                this.primeCodeField;
            set => 
                this.primeCodeField = value;
        }

        public string SubCode
        {
            get => 
                this.subCodeField;
            set => 
                this.subCodeField = value;
        }
    }
}

