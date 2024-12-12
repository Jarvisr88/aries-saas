namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;

    [Obsolete]
    public class MultiValueLookUpEditorStyleSettings : CheckedTokenComboBoxStyleSettings
    {
        internal bool ShouldCaptureMouseOnPopupInternal { get; set; }

        public override bool ShouldCaptureMouseOnPopup =>
            this.ShouldCaptureMouseOnPopupInternal;
    }
}

