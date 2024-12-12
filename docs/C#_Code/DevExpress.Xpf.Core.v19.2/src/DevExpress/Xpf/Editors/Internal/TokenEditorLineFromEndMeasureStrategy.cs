namespace DevExpress.Xpf.Editors.Internal
{
    using System;

    public class TokenEditorLineFromEndMeasureStrategy : TokenEditorLineMeasureStrategy
    {
        public TokenEditorLineFromEndMeasureStrategy(TokenEditorPanel panel) : base(panel)
        {
        }

        public override int ConvertToEditableIndex(int visualIndex) => 
            base.IsNewToken(visualIndex) ? base.TokensCount : visualIndex;

        public override int ConvertToVisibleIndex(int editableIndex) => 
            (editableIndex == base.TokensCount) ? this.NewTokenVisibleIndex : editableIndex;

        protected override int NewTokenVisibleIndex =>
            base.TokensCount;

        public override int LinesCount =>
            1;
    }
}

