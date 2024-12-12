namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Collections.Generic;

    public static class UniqueNameHelper
    {
        private static readonly string mdiBarManagerName = "internalMDIBarManager";
        private static readonly string mdiBarName = "MDIMenuBar";
        private static int mdiBarManagerCount = 0;
        private static int mdiBarCount;

        public static string GetMDIBarManagerName() => 
            mdiBarManagerName + ++mdiBarManagerCount;

        public static string GetMDIBarName() => 
            mdiBarName + ++mdiBarCount;

        public static string GetUniqueName(string prefix, ICollection<string> names, int initialValue)
        {
            int num = initialValue;
            while (true)
            {
                int num2 = num;
                num = num2 + 1;
                string item = prefix + num2.ToString();
                if (!names.Contains(item))
                {
                    return item;
                }
            }
        }
    }
}

