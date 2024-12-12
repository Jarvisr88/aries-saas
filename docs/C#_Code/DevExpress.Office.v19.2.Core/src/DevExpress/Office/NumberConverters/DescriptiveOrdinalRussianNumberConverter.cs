namespace DevExpress.Office.NumberConverters
{
    public class DescriptiveOrdinalRussianNumberConverter : SlavicOrdinalBase
    {
        protected internal override INumericsProvider GenerateBillionProvider() => 
            new CardinalRussianNumericsProvider();

        protected internal override INumericsProvider GenerateHundredProvider() => 
            new CardinalRussianNumericsProvider();

        protected internal override INumericsProvider GenerateMillionProvider() => 
            new CardinalRussianNumericsProvider();

        protected internal override INumericsProvider GenerateQuadrillionProvider() => 
            new CardinalRussianNumericsProvider();

        protected internal override INumericsProvider GenerateQuintillionProvider() => 
            new CardinalRussianNumericsProvider();

        protected internal override INumericsProvider GenerateSinglesProvider() => 
            new CardinalRussianNumericsProvider();

        protected internal override INumericsProvider GenerateTeensProvider() => 
            new CardinalRussianNumericsProvider();

        protected internal override INumericsProvider GenerateTenthsProvider() => 
            new CardinalRussianNumericsProvider();

        protected internal override INumericsProvider GenerateThousandProvider() => 
            new CardinalRussianNumericsProvider();

        protected internal override INumericsProvider GenerateTrillionProvider() => 
            new CardinalRussianNumericsProvider();

        protected internal override INumericsProvider GetOrdinalSlavicNumericsProvider() => 
            new OrdinalRussianNumericsProvider();

        protected internal override INumericsProvider GetProvider() => 
            new ThousandRussianNumericsProvider();

        protected internal override NumberingFormat Type =>
            NumberingFormat.OrdinalText;
    }
}

