namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using System;

    public class DateTimePickerIndexCalculator : IndexCalculator
    {
        public override double CalcStart(double viewport) => 
            !viewport.AreClose(0.0) ? ((viewport / 2.0) - 50.0) : 0.0;
    }
}

