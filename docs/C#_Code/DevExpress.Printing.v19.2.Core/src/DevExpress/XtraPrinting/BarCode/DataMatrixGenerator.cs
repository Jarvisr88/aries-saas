namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Printing;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.BarCode.Native;
    using System;
    using System.ComponentModel;

    public class DataMatrixGenerator : BarCode2DGenerator
    {
        private DataMatrixCompactionMode mode;
        private DataMatrixPatternProcessor ecc200PatternProcessor;

        public DataMatrixGenerator();
        public DataMatrixGenerator(DataMatrixGenerator source);
        protected override bool BinaryCompactionMode();
        protected override BarCodeGeneratorBase CloneGenerator();
        protected override string GetValidCharSet();
        protected void Init(DataMatrixGenerator source);
        protected override bool TextCompactionMode();

        [Description("Gets or sets the bar code matrix size."), DefaultValue(0), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.DataMatrixGenerator.MatrixSize"), XtraSerializableProperty, NotifyParentProperty(true)]
        public DataMatrixSize MatrixSize { get; set; }

        [Description("Gets or sets whether textual information or a byte array should be used as the bar code's data, as well as its encoding."), DefaultValue(0), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.DataMatrixGenerator.CompactionMode"), XtraSerializableProperty, NotifyParentProperty(true)]
        public virtual DataMatrixCompactionMode CompactionMode { get; set; }

        public override BarCodeSymbology SymbologyCode { get; }

        protected override float YRatio { get; }

        protected override IPatternProcessor PatternProcessor { get; }

        private DataMatrixPatternProcessor MatrixPatternProcessor { get; set; }

        protected override bool IsSquareBarcode { get; }
    }
}

