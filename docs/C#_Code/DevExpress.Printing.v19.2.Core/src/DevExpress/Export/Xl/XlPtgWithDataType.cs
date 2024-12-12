namespace DevExpress.Export.Xl
{
    using System;

    public abstract class XlPtgWithDataType : XlPtgBase
    {
        protected XlPtgWithDataType()
        {
        }

        protected XlPtgWithDataType(XlPtgDataType dataType)
        {
            base.DataType = dataType;
        }

        protected override XlPtgDataType GetPtgDataType(XlPtgDataType ptgDataType) => 
            (ptgDataType != XlPtgDataType.None) ? ptgDataType : XlPtgDataType.Reference;
    }
}

