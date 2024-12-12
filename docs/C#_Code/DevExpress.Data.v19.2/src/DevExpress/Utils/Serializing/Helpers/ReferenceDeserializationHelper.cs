namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class ReferenceDeserializationHelper
    {
        private readonly Dictionary<string, object> referencedObjects = new Dictionary<string, object>();
        private readonly List<Pair<object, XtraPropertyInfo>> referencedProperties = new List<Pair<object, XtraPropertyInfo>>();

        public void AddReferencedObject(object obj, string reference)
        {
            if (!Equals(obj, "~Xtra#NULL"))
            {
                this.referencedObjects.Add(reference, obj);
            }
        }

        public void AssignReferencedObjects()
        {
            foreach (Pair<object, XtraPropertyInfo> pair in this.referencedProperties)
            {
                object first = pair.First;
                IXtraSerializable serializable = first as IXtraSerializable;
                if (serializable != null)
                {
                    serializable.OnStartDeserializing(new LayoutAllowEventArgs(string.Empty));
                }
                XtraPropertyInfo second = pair.Second;
                PropertyInfo[] properties = first.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                int index = 0;
                while (true)
                {
                    if (index < properties.Length)
                    {
                        PropertyInfo info2 = properties[index];
                        object obj3 = null;
                        if ((info2.Name != second.Name) || !this.referencedObjects.TryGetValue(ParseReference(second.Value as string), out obj3))
                        {
                            index++;
                            continue;
                        }
                        info2.SetValue(first, obj3, new object[0]);
                    }
                    if (serializable != null)
                    {
                        serializable.OnEndDeserializing(string.Empty);
                    }
                    break;
                }
            }
        }

        public static string ParseReference(string value) => 
            value.Replace("#Ref-", string.Empty);

        public void ProcessProperties(object obj, IXtraPropertyCollection store)
        {
            foreach (XtraPropertyInfo info in store)
            {
                if (info.Name == "Ref")
                {
                    this.AddReferencedObject(obj, (string) info.Value);
                    continue;
                }
                if ((info.Value is string) && ((string) info.Value).StartsWith("#Ref-"))
                {
                    this.referencedProperties.Add(new Pair<object, XtraPropertyInfo>(obj, info));
                }
            }
        }
    }
}

