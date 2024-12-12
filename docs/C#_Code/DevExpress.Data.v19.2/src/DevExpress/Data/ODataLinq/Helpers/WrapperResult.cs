namespace DevExpress.Data.ODataLinq.Helpers
{
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct WrapperResult
    {
        public readonly IList ElementList;
        public readonly IList FieldList;
        public readonly bool IsEmpty;
        public WrapperResult(IList elementList, IList fieldList);
        public WrapperResult(IList elementList, IList fieldList, bool isEmpty);
    }
}

