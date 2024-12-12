namespace DevExpress.Internal
{
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;

    public class WinApiRegistryMultiKey : DisposableBase
    {
        private List<WinApiRegistryKey> keys = new List<WinApiRegistryKey>();

        public WinApiRegistryMultiKey(RegistryHive hive, string key, WinApiRegistryHelper.ResigtryAccess access)
        {
            WinApiRegistryKey item = new WinApiRegistryKey(hive, key, access | WinApiRegistryHelper.ResigtryAccess.WOW64_32Key);
            if (item.Exists)
            {
                this.keys.Add(item);
            }
            item = new WinApiRegistryKey(hive, key, access | WinApiRegistryHelper.ResigtryAccess.WOW64_64Key);
            if (item.Exists)
            {
                this.keys.Add(item);
            }
        }

        protected override void DisposeManaged()
        {
            foreach (WinApiRegistryKey key in this.keys)
            {
                key.Dispose();
            }
            this.keys.Clear();
            base.DisposeManaged();
        }

        public string[] GetMultiSzValue(string name)
        {
            List<string> list = new List<string>();
            foreach (WinApiRegistryKey key in this.keys)
            {
                string[] multiSzValue = key.GetMultiSzValue(name);
                if (multiSzValue != null)
                {
                    list.AddRange(multiSzValue);
                }
            }
            return ((list.Count > 0) ? list.ToArray() : null);
        }

        public string GetSzValue(string name)
        {
            string str2;
            using (List<WinApiRegistryKey>.Enumerator enumerator = this.keys.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        string szValue = enumerator.Current.GetSzValue(name);
                        if (szValue == null)
                        {
                            continue;
                        }
                        str2 = szValue;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return str2;
        }

        public bool Exists =>
            this.keys.Count > 0;
    }
}

