namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXCompositeResolutionLevel : JPXResolutionLevel
    {
        private readonly JPXLHSubBand lhSubBand;
        private readonly JPXHLSubBand hlSubBand;
        private readonly JPXHHSubBand hhSubBand;

        public JPXCompositeResolutionLevel(JPXTileComponent component, int level, JPXCodingStyleComponent codingStyle) : base(component, level, codingStyle)
        {
            int resolutionLevelNumber = codingStyle.DecompositionLevelCount - level;
            int codeBlockWidth = base.CodeBlockWidth;
            int codeBlockHeight = base.CodeBlockHeight;
            this.lhSubBand = new JPXLHSubBand(component, resolutionLevelNumber, codeBlockWidth, codeBlockHeight);
            this.hlSubBand = new JPXHLSubBand(component, resolutionLevelNumber, codeBlockWidth, codeBlockHeight);
            this.hhSubBand = new JPXHHSubBand(component, resolutionLevelNumber, codeBlockWidth, codeBlockHeight);
            base.AppendPrecincts(this.hlSubBand);
            base.AppendPrecincts(this.lhSubBand);
            base.AppendPrecincts(this.hhSubBand);
        }

        public JPXLHSubBand LHSubBand =>
            this.lhSubBand;

        public JPXHLSubBand HLSubBand =>
            this.hlSubBand;

        public JPXHHSubBand HHSubBand =>
            this.hhSubBand;
    }
}

