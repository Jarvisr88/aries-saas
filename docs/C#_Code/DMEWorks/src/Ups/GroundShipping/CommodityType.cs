namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class CommodityType
    {
        private string commodityIDField;
        private string descriptionField;
        private WeightType weightField;
        private DimensionsType dimensionsField;
        private string numberOfPiecesField;
        private ShipCodeDescriptionType packagingTypeField;
        private string dangerousGoodsIndicatorField;
        private CommodityValueType commodityValueField;
        private string freightClassField;
        private string nMFCCommodityCodeField;
        private NMFCCommodityType nMFCCommodityField;

        public string CommodityID
        {
            get => 
                this.commodityIDField;
            set => 
                this.commodityIDField = value;
        }

        public string Description
        {
            get => 
                this.descriptionField;
            set => 
                this.descriptionField = value;
        }

        public WeightType Weight
        {
            get => 
                this.weightField;
            set => 
                this.weightField = value;
        }

        public DimensionsType Dimensions
        {
            get => 
                this.dimensionsField;
            set => 
                this.dimensionsField = value;
        }

        public string NumberOfPieces
        {
            get => 
                this.numberOfPiecesField;
            set => 
                this.numberOfPiecesField = value;
        }

        public ShipCodeDescriptionType PackagingType
        {
            get => 
                this.packagingTypeField;
            set => 
                this.packagingTypeField = value;
        }

        public string DangerousGoodsIndicator
        {
            get => 
                this.dangerousGoodsIndicatorField;
            set => 
                this.dangerousGoodsIndicatorField = value;
        }

        public CommodityValueType CommodityValue
        {
            get => 
                this.commodityValueField;
            set => 
                this.commodityValueField = value;
        }

        public string FreightClass
        {
            get => 
                this.freightClassField;
            set => 
                this.freightClassField = value;
        }

        public string NMFCCommodityCode
        {
            get => 
                this.nMFCCommodityCodeField;
            set => 
                this.nMFCCommodityCodeField = value;
        }

        public NMFCCommodityType NMFCCommodity
        {
            get => 
                this.nMFCCommodityField;
            set => 
                this.nMFCCommodityField = value;
        }
    }
}

