namespace DevExpress.Export.Xl
{
    using System;

    public abstract class XlPtgMemBase : XlPtgWithDataType
    {
        private int innerPtgCount;

        protected XlPtgMemBase(int innerPtgCount)
        {
            this.innerPtgCount = innerPtgCount;
        }

        protected XlPtgMemBase(int innerPtgCount, XlPtgDataType dataType) : base(dataType)
        {
            this.innerPtgCount = innerPtgCount;
        }

        public int InnerThingCount
        {
            get => 
                this.innerPtgCount;
            set => 
                this.innerPtgCount = value;
        }
    }
}

