namespace DevExpress.Office.NumberConverters
{
    public class DescriptiveCardinalTurkishNumberConverter : DescriptiveTurkishNumberConverterBase
    {
        protected internal override NumberingFormat Type =>
            NumberingFormat.CardinalText;
    }
}

