namespace DevExpress.XtraPrinting.BarCode
{
    using System;
    using System.Collections;

    public interface IPatternProcessor
    {
        void Assign(IPatternProcessor source);
        void RefreshPattern(object data);

        ArrayList Pattern { get; }
    }
}

