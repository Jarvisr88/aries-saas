namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Windows.Forms;

    public class FrenchKeyboardDetector
    {
        public static bool IsFrenchKeyboard
        {
            get
            {
                int lCID = InputLanguage.CurrentInputLanguage.Culture.LCID;
                return ((lCID > 0x80c) ? ((lCID == 0x140c) || (lCID == 0x180c)) : ((lCID == 0x40c) || (lCID == 0x80c)));
            }
        }
    }
}

