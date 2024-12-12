namespace DevExpress.XtraPrinting.BarCode.Native
{
    using System;

    public class DataMatrixX12PatternProcessor : DataMatrixC40PatternProcessor
    {
        private static string validCharset;

        static DataMatrixX12PatternProcessor();
        public override string GetValidCharSet();

        protected override string BasicCharset { get; }

        protected override string Shift2Charset { get; }

        protected override string Shift3Charset { get; }

        protected override byte Latch { get; }

        protected override bool OnlyBasicCharset { get; }

        protected override byte UnknownChar { get; }

        protected override bool UpperShift { get; }
    }
}

