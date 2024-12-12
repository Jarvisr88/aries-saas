namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class PickupRequestType
    {
        private string additionalCommentsField;
        private RequesterType requesterField;
        private string pickupDateField;
        private string earliestTimeReadyField;
        private string latestTimeReadyField;
        private string pickupTimeReadyField;
        private string dropoffTimeReadyField;

        public string AdditionalComments
        {
            get => 
                this.additionalCommentsField;
            set => 
                this.additionalCommentsField = value;
        }

        public RequesterType Requester
        {
            get => 
                this.requesterField;
            set => 
                this.requesterField = value;
        }

        public string PickupDate
        {
            get => 
                this.pickupDateField;
            set => 
                this.pickupDateField = value;
        }

        public string EarliestTimeReady
        {
            get => 
                this.earliestTimeReadyField;
            set => 
                this.earliestTimeReadyField = value;
        }

        public string LatestTimeReady
        {
            get => 
                this.latestTimeReadyField;
            set => 
                this.latestTimeReadyField = value;
        }

        public string PickupTimeReady
        {
            get => 
                this.pickupTimeReadyField;
            set => 
                this.pickupTimeReadyField = value;
        }

        public string DropoffTimeReady
        {
            get => 
                this.dropoffTimeReadyField;
            set => 
                this.dropoffTimeReadyField = value;
        }
    }
}

