namespace DevExpress.Data
{
    using DevExpress.Utils.Design;
    using System;

    public sealed class CalculatedColumnTypeConverter : UniversalTypeConverter
    {
        protected sealed override bool AllowBinaryType { get; }
    }
}

