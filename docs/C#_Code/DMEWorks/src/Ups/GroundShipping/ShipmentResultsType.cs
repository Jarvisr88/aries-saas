namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class ShipmentResultsType
    {
        private AirFreightStatusType airFreightPickupRequestField;
        private string shipperNumberField;
        private string creationDateField;
        private string customerServiceInformationField;
        private string originServiceCenterCodeField;
        private string destinationServiceCenterCodeField;
        private string labelServiceCodeField;
        private string airFreightModularIDField;
        private string pickupRequestConfirmationNumberField;
        private string deliveryDateField;
        private string shipmentNumberField;
        private string bOLIDField;
        private object guaranteedIndicatorField;
        private object minimumChargeAppliedIndicatorField;
        private RateType[] rateField;
        private TotalShipmentChargeType totalShipmentChargeField;
        private string minimumBillableWeightAppliedIndicatorField;
        private WeightType billableShipmentWeightField;
        private ShipCodeDescriptionType ratingScheduleField;
        private WeightType dimensionalWeightField;
        private ShipCodeDescriptionType serviceField;
        private DocumentType documentsField;
        private string holdAtAirportPickupDateField;
        private string nextAvailablePickupDateField;

        public AirFreightStatusType AirFreightPickupRequest
        {
            get => 
                this.airFreightPickupRequestField;
            set => 
                this.airFreightPickupRequestField = value;
        }

        public string ShipperNumber
        {
            get => 
                this.shipperNumberField;
            set => 
                this.shipperNumberField = value;
        }

        public string CreationDate
        {
            get => 
                this.creationDateField;
            set => 
                this.creationDateField = value;
        }

        public string CustomerServiceInformation
        {
            get => 
                this.customerServiceInformationField;
            set => 
                this.customerServiceInformationField = value;
        }

        public string OriginServiceCenterCode
        {
            get => 
                this.originServiceCenterCodeField;
            set => 
                this.originServiceCenterCodeField = value;
        }

        public string DestinationServiceCenterCode
        {
            get => 
                this.destinationServiceCenterCodeField;
            set => 
                this.destinationServiceCenterCodeField = value;
        }

        public string LabelServiceCode
        {
            get => 
                this.labelServiceCodeField;
            set => 
                this.labelServiceCodeField = value;
        }

        public string AirFreightModularID
        {
            get => 
                this.airFreightModularIDField;
            set => 
                this.airFreightModularIDField = value;
        }

        public string PickupRequestConfirmationNumber
        {
            get => 
                this.pickupRequestConfirmationNumberField;
            set => 
                this.pickupRequestConfirmationNumberField = value;
        }

        public string DeliveryDate
        {
            get => 
                this.deliveryDateField;
            set => 
                this.deliveryDateField = value;
        }

        public string ShipmentNumber
        {
            get => 
                this.shipmentNumberField;
            set => 
                this.shipmentNumberField = value;
        }

        public string BOLID
        {
            get => 
                this.bOLIDField;
            set => 
                this.bOLIDField = value;
        }

        public object GuaranteedIndicator
        {
            get => 
                this.guaranteedIndicatorField;
            set => 
                this.guaranteedIndicatorField = value;
        }

        public object MinimumChargeAppliedIndicator
        {
            get => 
                this.minimumChargeAppliedIndicatorField;
            set => 
                this.minimumChargeAppliedIndicatorField = value;
        }

        [XmlElement("Rate")]
        public RateType[] Rate
        {
            get => 
                this.rateField;
            set => 
                this.rateField = value;
        }

        public TotalShipmentChargeType TotalShipmentCharge
        {
            get => 
                this.totalShipmentChargeField;
            set => 
                this.totalShipmentChargeField = value;
        }

        public string MinimumBillableWeightAppliedIndicator
        {
            get => 
                this.minimumBillableWeightAppliedIndicatorField;
            set => 
                this.minimumBillableWeightAppliedIndicatorField = value;
        }

        public WeightType BillableShipmentWeight
        {
            get => 
                this.billableShipmentWeightField;
            set => 
                this.billableShipmentWeightField = value;
        }

        public ShipCodeDescriptionType RatingSchedule
        {
            get => 
                this.ratingScheduleField;
            set => 
                this.ratingScheduleField = value;
        }

        public WeightType DimensionalWeight
        {
            get => 
                this.dimensionalWeightField;
            set => 
                this.dimensionalWeightField = value;
        }

        public ShipCodeDescriptionType Service
        {
            get => 
                this.serviceField;
            set => 
                this.serviceField = value;
        }

        public DocumentType Documents
        {
            get => 
                this.documentsField;
            set => 
                this.documentsField = value;
        }

        public string HoldAtAirportPickupDate
        {
            get => 
                this.holdAtAirportPickupDateField;
            set => 
                this.holdAtAirportPickupDateField = value;
        }

        public string NextAvailablePickupDate
        {
            get => 
                this.nextAvailablePickupDateField;
            set => 
                this.nextAvailablePickupDateField = value;
        }
    }
}

