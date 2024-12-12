namespace DevExpress.Utils.Serializing
{
    using DevExpress.Utils.Serializing.Helpers;
    using Microsoft.Win32;
    using System;
    using System.Collections;

    public class RegistryXtraSerializer : XtraSerializer
    {
        private static string[] BaseKeyNames = new string[] { "HKEY_CLASSES_ROOT", "HKEY_CURRENT_USER", "HKEY_USERS", "HKEY_LOCAL_MACHINE", "HKEY_CURRENT_CONFIG" };
        private static RegistryKey[] BaseKeys = new RegistryKey[] { Registry.ClassesRoot, Registry.CurrentUser, Registry.Users, Registry.LocalMachine, Registry.CurrentConfig };

        private object CheckNullValue(object val) => 
            (!(val is string) || (val.ToString() != "~Xtra#NULL")) ? val : null;

        private static void DeleteKey(RegistryKey key, string relPath, string keyName, string appName)
        {
            if (((appName != null) && (appName.Trim().Length != 0)) && (keyName == appName))
            {
                RegistryKey key2 = key.OpenSubKey(relPath + keyName);
                if (key2 != null)
                {
                    key2.Close();
                    key.DeleteSubKeyTree(relPath + keyName);
                }
            }
        }

        protected override IXtraPropertyCollection Deserialize(string path, string appName, IList objects)
        {
            appName = appName + "Data";
            RegistryKey baseKey = GetBaseKey(path);
            string str = this.GetPath(path);
            if (str.Length == 0)
            {
                return null;
            }
            if (!str.EndsWith(@"\"))
            {
                str = str + @"\";
            }
            IXtraPropertyCollection propertys = null;
            RegistryKey key = baseKey.OpenSubKey(str + appName);
            if (key == null)
            {
                return null;
            }
            try
            {
                propertys = this.DeserializeLevel(key);
            }
            finally
            {
                key.Close();
            }
            return propertys;
        }

        private IXtraPropertyCollection DeserializeLevel(RegistryKey key)
        {
            string[] valueNames = key.GetValueNames();
            string[] subKeyNames = key.GetSubKeyNames();
            IXtraPropertyCollection propertys = new XtraPropertyCollection(valueNames.Length + subKeyNames.Length);
            foreach (string str in valueNames)
            {
                propertys.Add(new XtraPropertyInfo(str, null, this.CheckNullValue(key.GetValue(str))));
            }
            foreach (string str2 in subKeyNames)
            {
                RegistryKey key2 = key.OpenSubKey(str2);
                try
                {
                    XtraPropertyInfo prop = new XtraPropertyInfo(str2, null, this.CheckNullValue(key2.GetValue(null)), true);
                    propertys.Add(prop);
                    prop.ChildProperties.AddRange(this.DeserializeLevel(key2));
                }
                finally
                {
                    key2.Close();
                }
            }
            return propertys;
        }

        private static RegistryKey GetBaseKey(string path)
        {
            for (int i = 0; i < BaseKeyNames.Length; i++)
            {
                if (path.StartsWith(BaseKeyNames[i]))
                {
                    return BaseKeys[i];
                }
            }
            return Registry.CurrentUser;
        }

        private string GetPath(string path)
        {
            int index = 0;
            while (true)
            {
                if (index < BaseKeyNames.Length)
                {
                    if (!path.StartsWith(BaseKeyNames[index]))
                    {
                        index++;
                        continue;
                    }
                    path = path.Remove(0, BaseKeyNames[index].Length);
                }
                while ((path.Length > 0) && (path[0] == '\\'))
                {
                    path = path.Remove(0, 1);
                }
                return path;
            }
        }

        protected override bool Serialize(string path, IXtraPropertyCollection props, string appName)
        {
            appName = appName + "Data";
            RegistryKey baseKey = GetBaseKey(path);
            string relPath = this.GetPath(path);
            if (relPath.Length == 0)
            {
                return false;
            }
            if (!relPath.EndsWith(@"\"))
            {
                relPath = relPath + @"\";
            }
            DeleteKey(baseKey, relPath, appName, appName);
            RegistryKey key = baseKey.CreateSubKey(relPath + appName);
            try
            {
                this.SerializeLevel(key, props);
            }
            finally
            {
                key.Close();
            }
            return true;
        }

        private void SerializeLevel(RegistryKey key, IXtraPropertyCollection props)
        {
            foreach (XtraPropertyInfo info in props)
            {
                this.SerializeProperty(key, info);
            }
        }

        private void SerializeProperty(RegistryKey key, XtraPropertyInfo p)
        {
            RegistryKey key2 = key;
            if (p.IsKey)
            {
                key2 = key.CreateSubKey(p.Name);
            }
            try
            {
                object obj2 = p.Value;
                if ((obj2 != null) && !obj2.GetType().IsPrimitive)
                {
                    obj2 = ObjectConverter.ObjectToString(obj2);
                }
                if (!p.IsKey || (obj2 != null))
                {
                    key2.SetValue(p.IsKey ? null : p.Name, (obj2 == null) ? "~Xtra#NULL" : obj2);
                }
                if (p.IsKey)
                {
                    this.SerializeLevel(key2, p.ChildProperties);
                }
            }
            finally
            {
                if (p.IsKey)
                {
                    key2.Close();
                }
            }
        }
    }
}

