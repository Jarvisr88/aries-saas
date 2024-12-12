namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls.Primitives;

    public class DropDownButtonChrome : ButtonChrome
    {
        private void GetRenderElements();
        protected override bool IsReadyForUpdate(ButtonBase buttonBase);
        protected override void OnApplyRenderTemplate();
        protected override void SetFocusState();
        private void UpdateArrowPart(DropDownButtonBase dropDownBase);
        private void UpdateContentAndArrowPart(DropDownButtonBase dropDownBase);
        private void UpdateContentPart(DropDownButtonBase dropDownBase);
        protected internal override void UpdateStates();

        public RenderButtonBorderContext ArrowPart { get; private set; }

        public RenderButtonBorderContext ContentAndArrowPart { get; private set; }

        public RenderControlContext DefaultArrowGlyph { get; private set; }
    }
}

