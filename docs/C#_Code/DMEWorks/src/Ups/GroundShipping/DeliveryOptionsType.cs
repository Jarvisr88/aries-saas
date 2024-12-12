namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class DeliveryOptionsType
    {
        private string callBeforeDeliveryIndicatorField;
        private string constructionSiteDeliveryIndicatorField;
        private string holidayDeliveryIndicatorField;
        private string insideDeliveryIndicatorField;
        private string residentialDeliveryIndicatorField;
        private string weekendDeliveryIndicatorField;
        private string liftGateRequiredIndicatorField;
        private string saturdayDeliveryIndicatorField;
        private string deliveryToDoorIndicatorField;

        public string CallBeforeDeliveryIndicator
        {
            get => 
                this.callBeforeDeliveryIndicatorField;
            set => 
                this.callBeforeDeliveryIndicatorField = value;
        }

        public string ConstructionSiteDeliveryIndicator
        {
            get => 
                this.constructionSiteDeliveryIndicatorField;
            set => 
                this.constructionSiteDeliveryIndicatorField = value;
        }

        public string HolidayDeliveryIndicator
        {
            get => 
                this.holidayDeliveryIndicatorField;
            set => 
                this.holidayDeliveryIndicatorField = value;
        }

        public string InsideDeliveryIndicator
        {
            get => 
                this.insideDeliveryIndicatorField;
            set => 
                this.insideDeliveryIndicatorField = value;
        }

        public string ResidentialDeliveryIndicator
        {
            get => 
                this.residentialDeliveryIndicatorField;
            set => 
                this.residentialDeliveryIndicatorField = value;
        }

        public string WeekendDeliveryIndicator
        {
            get => 
                this.weekendDeliveryIndicatorField;
            set => 
                this.weekendDeliveryIndicatorField = value;
        }

        public string LiftGateRequiredIndicator
        {
            get => 
                this.liftGateRequiredIndicatorField;
            set => 
                this.liftGateRequiredIndicatorField = value;
        }

        public string SaturdayDeliveryIndicator
        {
            get => 
                this.saturdayDeliveryIndicatorField;
            set => 
                this.saturdayDeliveryIndicatorField = value;
        }

        public string DeliveryToDoorIndicator
        {
            get => 
                this.deliveryToDoorIndicatorField;
            set => 
                this.deliveryToDoorIndicatorField = value;
        }
    }
}

