namespace DevExpress.Utils.About
{
    using Microsoft.Win32;
    using System;

    internal class AlgoProvider
    {
        private static Guid? id;

        private static Guid? GetId()
        {
            Guid? nullable;
            try
            {
                Guid guid = Guid.NewGuid();
                string name = @"Licenses\5FCB5B35-D11E-4374-AD58-19A608D8228C";
                RegistryKey key = Registry.ClassesRoot.OpenSubKey(name);
                if (key == null)
                {
                    Registry.ClassesRoot.CreateSubKey(name).SetValue(null, guid.ToString());
                }
                else
                {
                    guid = new Guid((string) key.GetValue(null));
                }
                nullable = new Guid?(guid);
            }
            catch
            {
                return null;
            }
            return nullable;
        }

        internal static Guid? Id
        {
            get
            {
                if (id == null)
                {
                    id = GetId();
                }
                return id;
            }
        }
    }
}

