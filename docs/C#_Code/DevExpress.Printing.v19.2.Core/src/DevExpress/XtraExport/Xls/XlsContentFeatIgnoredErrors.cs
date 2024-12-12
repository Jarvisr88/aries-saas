namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentFeatIgnoredErrors : XlsContentFeat
    {
        public XlsContentFeatIgnoredErrors()
        {
            base.FeatureType = XlsFeatureType.IgnoredErrors;
        }

        public override int GetSize() => 
            base.GetSize() + 4;

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            uint num = 0;
            if (this.CalculationErrors)
            {
                num |= 1;
            }
            if (this.EmptyCellRef)
            {
                num |= 2;
            }
            if (this.NumberStoredAsText)
            {
                num |= 4;
            }
            if (this.InconsistRange)
            {
                num |= 8;
            }
            if (this.InconsistFormula)
            {
                num |= (uint) 0x10;
            }
            if (this.TextDateInsuff)
            {
                num |= (uint) 0x20;
            }
            if (this.UnprotectedFormula)
            {
                num |= (uint) 0x40;
            }
            if (this.DataValidation)
            {
                num |= 0x80;
            }
            writer.Write(num);
        }

        public bool CalculationErrors { get; set; }

        public bool EmptyCellRef { get; set; }

        public bool NumberStoredAsText { get; set; }

        public bool InconsistRange { get; set; }

        public bool InconsistFormula { get; set; }

        public bool TextDateInsuff { get; set; }

        public bool UnprotectedFormula { get; set; }

        public bool DataValidation { get; set; }
    }
}

