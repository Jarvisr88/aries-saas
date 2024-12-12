namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Media;

    public static class BeepOnErrorHelper
    {
        public static void Process()
        {
            try
            {
                SystemSounds.Beep.Play();
            }
            catch
            {
            }
        }
    }
}

