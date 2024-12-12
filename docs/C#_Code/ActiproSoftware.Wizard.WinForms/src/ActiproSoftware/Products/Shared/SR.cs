namespace ActiproSoftware.Products.Shared
{
    using ActiproSoftware.Products;
    using System;
    using System.Resources;
    using System.Runtime.CompilerServices;

    public sealed class SR : SRBase
    {
        private static volatile ActiproSoftware.Products.Shared.SR #gd;

        private SR()
        {
        }

        public static void ClearCustomStrings()
        {
            Instance.ClearCustomStringsCore();
        }

        public static bool ContainsCustomString(string name) => 
            Instance.ContainsCustomStringCore(name);

        public static string GetCustomString(string name) => 
            Instance.GetCustomStringCore(name);

        public static string GetString(string name) => 
            Instance.GetStringCore(name, null);

        public static string GetString(string name, params object[] args) => 
            Instance.GetStringCore(name, args);

        public static void RemoveCustomString(string name)
        {
            Instance.RemoveCustomStringCore(name);
        }

        public static void SetCustomString(string name, string value)
        {
            Instance.SetCustomStringCore(name, value);
        }

        private static ActiproSoftware.Products.Shared.SR Instance
        {
            get
            {
                #gd ??= new ActiproSoftware.Products.Shared.SR();
                return #gd;
            }
        }

        public override System.Resources.ResourceManager ResourceManager =>
            Resources.ResourceManager;
    }
}

