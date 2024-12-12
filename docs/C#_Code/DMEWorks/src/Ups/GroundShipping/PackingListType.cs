namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class PackingListType
    {
        private PackingListContactType shipFromField;
        private PackingListContactType shipToField;
        private PackingListReferenceType[] referenceField;
        private PackingListHandlingUnitType handlingUnitField;

        public PackingListContactType ShipFrom
        {
            get => 
                this.shipFromField;
            set => 
                this.shipFromField = value;
        }

        public PackingListContactType ShipTo
        {
            get => 
                this.shipToField;
            set => 
                this.shipToField = value;
        }

        [XmlElement("Reference")]
        public PackingListReferenceType[] Reference
        {
            get => 
                this.referenceField;
            set => 
                this.referenceField = value;
        }

        public PackingListHandlingUnitType HandlingUnit
        {
            get => 
                this.handlingUnitField;
            set => 
                this.handlingUnitField = value;
        }
    }
}

