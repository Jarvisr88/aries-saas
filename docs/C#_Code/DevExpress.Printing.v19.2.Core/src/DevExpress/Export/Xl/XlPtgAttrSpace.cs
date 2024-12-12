namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlPtgAttrSpace : XlPtgBase
    {
        private int charCount;

        public XlPtgAttrSpace(XlPtgAttrSpaceType spaceType, int charCount)
        {
            this.SpaceType = spaceType;
            this.CharCount = charCount;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int TypeCode =>
            0x4019;

        public XlPtgAttrSpaceType SpaceType { get; set; }

        public int CharCount
        {
            get => 
                this.charCount;
            set
            {
                if ((value < 0) || (value > 0xff))
                {
                    throw new ArgumentOutOfRangeException("CharCount");
                }
                this.charCount = value;
            }
        }
    }
}

