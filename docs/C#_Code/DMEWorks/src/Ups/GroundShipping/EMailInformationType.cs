namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class EMailInformationType
    {
        private ShipCodeDescriptionType eMailTypeField;
        private Ups.GroundShipping.EMailType eMailField;

        public ShipCodeDescriptionType EMailType
        {
            get => 
                this.eMailTypeField;
            set => 
                this.eMailTypeField = value;
        }

        public Ups.GroundShipping.EMailType EMail
        {
            get => 
                this.eMailField;
            set => 
                this.eMailField = value;
        }
    }
}

