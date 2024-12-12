namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Windows.Forms;

    public class SwedishKeyboardDetector
    {
        public static bool IsSwedishKeyboard
        {
            get
            {
                int lCID = InputLanguage.CurrentInputLanguage.Culture.LCID;
                return ((lCID == 0x41d) || (lCID == 0x83b));
            }
        }
    }
}

