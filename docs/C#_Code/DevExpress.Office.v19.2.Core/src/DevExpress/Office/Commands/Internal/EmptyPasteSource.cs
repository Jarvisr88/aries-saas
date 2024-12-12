namespace DevExpress.Office.Commands.Internal
{
    using System;

    public class EmptyPasteSource : PasteSource
    {
        public override bool ContainsData(string format, bool autoConvert) => 
            false;

        public override object GetData(string format, bool autoConvert) => 
            null;

        public override bool IsEmpty =>
            true;
    }
}

