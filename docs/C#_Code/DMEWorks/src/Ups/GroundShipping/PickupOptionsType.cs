namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class PickupOptionsType
    {
        private string holidayPickupIndicatorField;
        private string insidePickupIndicatorField;
        private string residentialPickupIndicatorField;
        private string weekendPickupIndicatorField;
        private string liftGateRequiredIndicatorField;
        private string holdAtAirportForPickupField;
        private string pickupFromDoorIndicatorField;

        public string HolidayPickupIndicator
        {
            get => 
                this.holidayPickupIndicatorField;
            set => 
                this.holidayPickupIndicatorField = value;
        }

        public string InsidePickupIndicator
        {
            get => 
                this.insidePickupIndicatorField;
            set => 
                this.insidePickupIndicatorField = value;
        }

        public string ResidentialPickupIndicator
        {
            get => 
                this.residentialPickupIndicatorField;
            set => 
                this.residentialPickupIndicatorField = value;
        }

        public string WeekendPickupIndicator
        {
            get => 
                this.weekendPickupIndicatorField;
            set => 
                this.weekendPickupIndicatorField = value;
        }

        public string LiftGateRequiredIndicator
        {
            get => 
                this.liftGateRequiredIndicatorField;
            set => 
                this.liftGateRequiredIndicatorField = value;
        }

        public string HoldAtAirportForPickup
        {
            get => 
                this.holdAtAirportForPickupField;
            set => 
                this.holdAtAirportForPickupField = value;
        }

        public string PickupFromDoorIndicator
        {
            get => 
                this.pickupFromDoorIndicatorField;
            set => 
                this.pickupFromDoorIndicatorField = value;
        }
    }
}

