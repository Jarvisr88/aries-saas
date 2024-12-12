namespace DevExpress.Office.NumberConverters
{
    public class DescriptiveCardinalThaiNumberConverter : DescriptiveThaiNumberConverterBase
    {
        protected internal override NumberingFormat Type =>
            NumberingFormat.CardinalText;
    }
}

