namespace DevExpress.Xpf.Editors.Internal
{
    using System;

    public class TokenEditorWrapLineFromEndMeasureStrategy : TokenEditorWrapLineMeasureStrategy
    {
        public TokenEditorWrapLineFromEndMeasureStrategy(TokenEditorPanel panel) : base(panel)
        {
        }

        public override int ConvertToEditableIndex(int visualIndex) => 
            base.IsNewToken(visualIndex) ? base.TokensCount : visualIndex;

        public override int ConvertToVisibleIndex(int editableIndex) => 
            (editableIndex == base.TokensCount) ? this.NewTokenVisibleIndex : editableIndex;

        protected override int NewTokenVisibleIndex =>
            base.TokensCount;
    }
}

