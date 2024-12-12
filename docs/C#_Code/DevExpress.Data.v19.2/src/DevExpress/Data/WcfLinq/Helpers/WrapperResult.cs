namespace DevExpress.Data.WcfLinq.Helpers
{
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct WrapperResult
    {
        public IList ElementList;
        public IList FieldList;
        public WrapperResult(IList elementList, IList fieldList);
    }
}

