namespace DevExpress.Office.Utils
{
    using System;

    public abstract class OfficeDrawingTerminatedStringPropertyValueBase : OfficeDrawingStringPropertyValueBase
    {
        protected OfficeDrawingTerminatedStringPropertyValueBase()
        {
        }

        public string TrimmedData =>
            base.Data.TrimEnd(new char[1]);
    }
}

