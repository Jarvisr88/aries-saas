namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class PackingListHandlingUnitType
    {
        private PackingListCommodityType[] commodityField;
        private string specialInstructionsField;
        private string totalNumberOfPiecesField;
        private string totalWeightField;
        private FreightShipUnitOfMeasurementType unitOfMeasurementField;
        private string currencyCodeField;

        [XmlElement("Commodity")]
        public PackingListCommodityType[] Commodity
        {
            get => 
                this.commodityField;
            set => 
                this.commodityField = value;
        }

        public string SpecialInstructions
        {
            get => 
                this.specialInstructionsField;
            set => 
                this.specialInstructionsField = value;
        }

        public string TotalNumberOfPieces
        {
            get => 
                this.totalNumberOfPiecesField;
            set => 
                this.totalNumberOfPiecesField = value;
        }

        public string TotalWeight
        {
            get => 
                this.totalWeightField;
            set => 
                this.totalWeightField = value;
        }

        public FreightShipUnitOfMeasurementType UnitOfMeasurement
        {
            get => 
                this.unitOfMeasurementField;
            set => 
                this.unitOfMeasurementField = value;
        }

        public string CurrencyCode
        {
            get => 
                this.currencyCodeField;
            set => 
                this.currencyCodeField = value;
        }
    }
}

