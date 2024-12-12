namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class ShipmentServiceOptionsType
    {
        private EMailInformationType[] eMailInformationField;
        private PickupOptionsType pickupOptionsField;
        private DeliveryOptionsType deliveryOptionsField;
        private OverSeasLegType overSeasLegField;
        private CODType cODField;
        private DangerousGoodsType dangerousGoodsField;
        private SortingAndSegregatingType sortingAndSegregatingField;
        private DeclaredValueType declaredValueField;
        private CustomsValueType customsValueField;
        private string deliveryDutiesPaidIndicatorField;
        private string deliveryDutiesUnpaidIndicatorField;
        private HandlingChargeType handlingChargeField;
        private string customsClearanceIndicatorField;

        [XmlElement("EMailInformation")]
        public EMailInformationType[] EMailInformation
        {
            get => 
                this.eMailInformationField;
            set => 
                this.eMailInformationField = value;
        }

        public PickupOptionsType PickupOptions
        {
            get => 
                this.pickupOptionsField;
            set => 
                this.pickupOptionsField = value;
        }

        public DeliveryOptionsType DeliveryOptions
        {
            get => 
                this.deliveryOptionsField;
            set => 
                this.deliveryOptionsField = value;
        }

        public OverSeasLegType OverSeasLeg
        {
            get => 
                this.overSeasLegField;
            set => 
                this.overSeasLegField = value;
        }

        public CODType COD
        {
            get => 
                this.cODField;
            set => 
                this.cODField = value;
        }

        public DangerousGoodsType DangerousGoods
        {
            get => 
                this.dangerousGoodsField;
            set => 
                this.dangerousGoodsField = value;
        }

        public SortingAndSegregatingType SortingAndSegregating
        {
            get => 
                this.sortingAndSegregatingField;
            set => 
                this.sortingAndSegregatingField = value;
        }

        public DeclaredValueType DeclaredValue
        {
            get => 
                this.declaredValueField;
            set => 
                this.declaredValueField = value;
        }

        public CustomsValueType CustomsValue
        {
            get => 
                this.customsValueField;
            set => 
                this.customsValueField = value;
        }

        public string DeliveryDutiesPaidIndicator
        {
            get => 
                this.deliveryDutiesPaidIndicatorField;
            set => 
                this.deliveryDutiesPaidIndicatorField = value;
        }

        public string DeliveryDutiesUnpaidIndicator
        {
            get => 
                this.deliveryDutiesUnpaidIndicatorField;
            set => 
                this.deliveryDutiesUnpaidIndicatorField = value;
        }

        public HandlingChargeType HandlingCharge
        {
            get => 
                this.handlingChargeField;
            set => 
                this.handlingChargeField = value;
        }

        public string CustomsClearanceIndicator
        {
            get => 
                this.customsClearanceIndicatorField;
            set => 
                this.customsClearanceIndicatorField = value;
        }
    }
}

