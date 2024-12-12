namespace DevExpress.Text.Fonts
{
    using System;
    using System.Runtime.CompilerServices;

    public class DXTextMeasurementResult
    {
        public DXTextMeasurementResult(DXSizeF size, int lineCount, int characterCount)
        {
            this.<Size>k__BackingField = size;
            this.<LineCount>k__BackingField = lineCount;
            this.<CharacterCount>k__BackingField = characterCount;
        }

        public DXSizeF Size { get; }

        public int LineCount { get; }

        public int CharacterCount { get; }
    }
}

