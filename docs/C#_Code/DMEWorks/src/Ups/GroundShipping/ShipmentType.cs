namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class ShipmentType
    {
        private ShipFromType shipFromField;
        private string shipperNumberField;
        private ShipToType shipToField;
        private PaymentInformationType paymentInformationField;
        private CountryOfManufactureType[] manufactureInformationField;
        private ShipCodeDescriptionType serviceField;
        private HandlingUnitType handlingUnitOneField;
        private HandlingUnitType handlingUnitTwoField;
        private ExistingShipmentIDType existingShipmentIDField;
        private string handlingInstructionsField;
        private string deliveryInstructionsField;
        private string pickupInstructionsField;
        private string specialInstructionsField;
        private ShipmentTotalWeightType shipmentTotalWeightField;
        private CommodityType[] commodityField;
        private ReferenceType[] referenceField;
        private ShipmentServiceOptionsType shipmentServiceOptionsField;
        private PickupRequestType pickupRequestField;
        private DocumentsType documentsField;
        private string iTNNumberField;
        private TaxIDType taxIDField;
        private string movementReferenceNumberField;
        private string eICNumberAndStatementField;

        public ShipFromType ShipFrom
        {
            get => 
                this.shipFromField;
            set => 
                this.shipFromField = value;
        }

        public string ShipperNumber
        {
            get => 
                this.shipperNumberField;
            set => 
                this.shipperNumberField = value;
        }

        public ShipToType ShipTo
        {
            get => 
                this.shipToField;
            set => 
                this.shipToField = value;
        }

        public PaymentInformationType PaymentInformation
        {
            get => 
                this.paymentInformationField;
            set => 
                this.paymentInformationField = value;
        }

        [XmlElement("ManufactureInformation")]
        public CountryOfManufactureType[] ManufactureInformation
        {
            get => 
                this.manufactureInformationField;
            set => 
                this.manufactureInformationField = value;
        }

        public ShipCodeDescriptionType Service
        {
            get => 
                this.serviceField;
            set => 
                this.serviceField = value;
        }

        public HandlingUnitType HandlingUnitOne
        {
            get => 
                this.handlingUnitOneField;
            set => 
                this.handlingUnitOneField = value;
        }

        public HandlingUnitType HandlingUnitTwo
        {
            get => 
                this.handlingUnitTwoField;
            set => 
                this.handlingUnitTwoField = value;
        }

        public ExistingShipmentIDType ExistingShipmentID
        {
            get => 
                this.existingShipmentIDField;
            set => 
                this.existingShipmentIDField = value;
        }

        public string HandlingInstructions
        {
            get => 
                this.handlingInstructionsField;
            set => 
                this.handlingInstructionsField = value;
        }

        public string DeliveryInstructions
        {
            get => 
                this.deliveryInstructionsField;
            set => 
                this.deliveryInstructionsField = value;
        }

        public string PickupInstructions
        {
            get => 
                this.pickupInstructionsField;
            set => 
                this.pickupInstructionsField = value;
        }

        public string SpecialInstructions
        {
            get => 
                this.specialInstructionsField;
            set => 
                this.specialInstructionsField = value;
        }

        public ShipmentTotalWeightType ShipmentTotalWeight
        {
            get => 
                this.shipmentTotalWeightField;
            set => 
                this.shipmentTotalWeightField = value;
        }

        [XmlElement("Commodity")]
        public CommodityType[] Commodity
        {
            get => 
                this.commodityField;
            set => 
                this.commodityField = value;
        }

        [XmlElement("Reference")]
        public ReferenceType[] Reference
        {
            get => 
                this.referenceField;
            set => 
                this.referenceField = value;
        }

        public ShipmentServiceOptionsType ShipmentServiceOptions
        {
            get => 
                this.shipmentServiceOptionsField;
            set => 
                this.shipmentServiceOptionsField = value;
        }

        public PickupRequestType PickupRequest
        {
            get => 
                this.pickupRequestField;
            set => 
                this.pickupRequestField = value;
        }

        public DocumentsType Documents
        {
            get => 
                this.documentsField;
            set => 
                this.documentsField = value;
        }

        public string ITNNumber
        {
            get => 
                this.iTNNumberField;
            set => 
                this.iTNNumberField = value;
        }

        public TaxIDType TaxID
        {
            get => 
                this.taxIDField;
            set => 
                this.taxIDField = value;
        }

        public string MovementReferenceNumber
        {
            get => 
                this.movementReferenceNumberField;
            set => 
                this.movementReferenceNumberField = value;
        }

        public string EICNumberAndStatement
        {
            get => 
                this.eICNumberAndStatementField;
            set => 
                this.eICNumberAndStatementField = value;
        }
    }
}

