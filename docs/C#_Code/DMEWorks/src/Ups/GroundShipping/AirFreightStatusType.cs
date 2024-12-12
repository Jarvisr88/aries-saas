namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class AirFreightStatusType
    {
        private StatusType statusField;
        private PreAuthReasonType[] preAuthorizationReasonField;

        public StatusType Status
        {
            get => 
                this.statusField;
            set => 
                this.statusField = value;
        }

        [XmlElement("PreAuthorizationReason")]
        public PreAuthReasonType[] PreAuthorizationReason
        {
            get => 
                this.preAuthorizationReasonField;
            set => 
                this.preAuthorizationReasonField = value;
        }
    }
}

