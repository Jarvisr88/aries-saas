namespace DevExpress.Office.Drawing
{
    using System;

    internal class TextShapeLayoutInfo : ShapeLayoutInfo
    {
        private readonly ParagraphLayoutInfoConverter paragraphLayoutInfoConverter;

        public TextShapeLayoutInfo(ParagraphLayoutInfoConverter paragraphLayoutInfoConverter) : base(false)
        {
            this.paragraphLayoutInfoConverter = paragraphLayoutInfoConverter;
        }

        protected override ShapeEffectBuildHelper GetEffectBuilder() => 
            new TextEffectBuildHelper(this, this.paragraphLayoutInfoConverter);

        protected internal override PathInfoBase[] GetPathInfos() => 
            base.Paths.GetPathInfosAndDerived<TextRunPathInfo>();

        protected internal void Reset()
        {
            base.Paths.Clear();
            this.PenInfo = null;
            base.ResetSizes();
        }
    }
}

