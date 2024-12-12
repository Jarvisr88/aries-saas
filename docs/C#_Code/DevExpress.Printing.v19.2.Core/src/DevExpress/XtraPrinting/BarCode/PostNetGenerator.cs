namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BarCode.Native;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;

    public class PostNetGenerator : BarCodeGeneratorBase
    {
        private static string validCharSet;
        private static Hashtable charPattern;

        static PostNetGenerator();
        public PostNetGenerator();
        private PostNetGenerator(PostNetGenerator source);
        protected override float CalcBarCodeWidth(ArrayList pattern, double module);
        private static char CalcCheckDigit(string text);
        protected override BarCodeGeneratorBase CloneGenerator();
        protected override void DrawBarCode(IGraphicsBase gr, RectangleF barBounds, RectangleF textBounds, IBarCodeData data, float xModule, float yModule);
        protected override Hashtable GetPatternTable();
        protected override float GetPatternWidth(char pattern);
        protected override string GetValidCharSet();
        protected override char[] PrepareText(string text);

        [DefaultValue(true), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public override bool CalcCheckSum { get; set; }

        [Description("For internal use. Gets the bar code symbology for the current generator object.")]
        public override BarCodeSymbology SymbologyCode { get; }
    }
}

