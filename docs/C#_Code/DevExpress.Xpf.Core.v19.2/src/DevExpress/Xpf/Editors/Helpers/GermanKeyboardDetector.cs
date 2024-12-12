namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Windows.Forms;

    public class GermanKeyboardDetector
    {
        public static bool IsGermanKeyboard
        {
            get
            {
                int lCID = InputLanguage.CurrentInputLanguage.Culture.LCID;
                return ((lCID > 0x807) ? ((lCID == 0xc07) || ((lCID == 0x1007) || (lCID == 0x1407))) : ((lCID == 0x407) || (lCID == 0x807)));
            }
        }
    }
}

