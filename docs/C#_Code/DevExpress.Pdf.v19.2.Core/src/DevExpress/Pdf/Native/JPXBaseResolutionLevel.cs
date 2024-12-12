namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXBaseResolutionLevel : JPXResolutionLevel
    {
        private readonly JPXLLSubBand llSubBand;

        public JPXBaseResolutionLevel(JPXTileComponent component, int level, JPXCodingStyleComponent codingStyle) : base(component, level, codingStyle)
        {
            this.llSubBand = new JPXLLSubBand(component, codingStyle.DecompositionLevelCount - base.LevelNumber, base.CodeBlockWidth, base.CodeBlockHeight);
            base.AppendPrecincts(this.llSubBand);
        }

        public JPXLLSubBand LLSubBand =>
            this.llSubBand;
    }
}

