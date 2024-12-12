namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.ComponentModel;
    using System.Security;
    using System.Windows.Forms;

    [ToolboxItem(false)]
    public class RichTextBoxEx : RichTextBox
    {
        private const int EM_GETPARAFORMAT = 0x43d;
        private const int EM_SETPARAFORMAT = 0x447;
        private const int EM_SETTYPOGRAPHYOPTIONS = 0x4ca;
        private const int TO_ADVANCEDTYPOGRAPHY = 1;
        private const int PFM_ALIGNMENT = 8;
        private const int SCF_SELECTION = 1;

        protected override void OnHandleCreated(EventArgs e);
        [SecuritySafeCritical]
        private void SetAdditionalOptions();

        public HorizontalAlignmentEx SelectionAlignmentEx { [SecuritySafeCritical] get; [SecuritySafeCritical] set; }
    }
}

