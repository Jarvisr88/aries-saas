namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class PackingListCommodityType
    {
        private string numberOfPiecesField;
        private ShipCodeDescriptionType packagingTypeField;
        private string dangerousGoodsIndicatorField;
        private string descriptionField;
        private string nMFCCommodityCodeField;
        private string freightClassField;
        private PackingListDimensionsType dimensionsField;
        private string weightField;
        private string commodityValueField;

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

        public string Description
        {
            get => 
                this.descriptionField;
            set => 
                this.descriptionField = value;
        }

        public string NMFCCommodityCode
        {
            get => 
                this.nMFCCommodityCodeField;
            set => 
                this.nMFCCommodityCodeField = value;
        }

        public string FreightClass
        {
            get => 
                this.freightClassField;
            set => 
                this.freightClassField = value;
        }

        public PackingListDimensionsType Dimensions
        {
            get => 
                this.dimensionsField;
            set => 
                this.dimensionsField = value;
        }

        public string Weight
        {
            get => 
                this.weightField;
            set => 
                this.weightField = value;
        }

        public string CommodityValue
        {
            get => 
                this.commodityValueField;
            set => 
                this.commodityValueField = value;
        }
    }
}

