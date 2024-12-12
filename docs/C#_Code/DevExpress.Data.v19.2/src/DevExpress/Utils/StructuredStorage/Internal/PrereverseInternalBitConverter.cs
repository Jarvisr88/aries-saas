namespace DevExpress.Utils.StructuredStorage.Internal
{
    using System;

    internal class PrereverseInternalBitConverter : InternalBitConverter
    {
        protected internal override void Preprocess(byte[] value)
        {
            Array.Reverse(value);
        }
    }
}

