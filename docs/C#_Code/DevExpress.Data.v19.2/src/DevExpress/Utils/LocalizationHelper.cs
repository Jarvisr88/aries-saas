namespace DevExpress.Utils
{
    using System;
    using System.Globalization;
    using System.Threading;

    public class LocalizationHelper
    {
        private const string ciFlag = "ci:";

        public static void SetCurrentCulture(string[] arguments)
        {
            string str = null;
            foreach (string str2 in arguments)
            {
                if (str2.IndexOf("ci:") == 0)
                {
                    str = str2.Substring("ci:".Length);
                }
            }
            if (string.IsNullOrEmpty(str))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CurrentCulture;
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentCulture;
            }
            else
            {
                CultureInfo info = null;
                if (str.StartsWith("ja"))
                {
                    info = new CultureInfo(0x411);
                }
                if (str.StartsWith("ru"))
                {
                    info = new CultureInfo(0x419);
                }
                if (info != null)
                {
                    Thread.CurrentThread.CurrentCulture = info;
                    Thread.CurrentThread.CurrentUICulture = info;
                }
            }
        }

        public static bool IsJapanese =>
            Thread.CurrentThread.CurrentCulture.LCID == 0x411;
    }
}

