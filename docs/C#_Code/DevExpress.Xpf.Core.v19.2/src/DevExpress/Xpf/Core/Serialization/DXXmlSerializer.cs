namespace DevExpress.Xpf.Core.Serialization
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class DXXmlSerializer : XmlXtraSerializer
    {
        private static IDictionary<string, Type> typeCache = new Dictionary<string, Type>();

        static DXXmlSerializer()
        {
            ObjectConverterImplementation instance = DevExpress.Utils.Serializing.Helpers.ObjectConverter.Instance;
            instance.RegisterConverter(BrushConverter<Brush>.Instance);
            instance.RegisterConverter(BrushConverter<LinearGradientBrush>.Instance);
            instance.RegisterConverter(BrushConverter<RadialGradientBrush>.Instance);
            instance.RegisterConverter(BrushConverter<ImageBrush>.Instance);
            instance.RegisterConverter(BrushConverter<DrawingBrush>.Instance);
            instance.RegisterConverter(TextDecorationsConverter.Instance);
            instance.RegisterConverter(new PatchingImageSourceConverter());
            instance.RegisterConverter(new DrawingImageConverter());
            instance.RegisterConverter(new BitmapImageConverter());
        }

        protected override XtraPropertyInfo CreateXtraPropertyInfo(string name, Type propType, bool isKey, Dictionary<string, string> attributes)
        {
            string str;
            string str2;
            if (!attributes.TryGetValue("OwnerType", out str) || !attributes.TryGetValue("DependencyPropertyType", out str2))
            {
                return base.CreateXtraPropertyInfo(name, propType, isKey, attributes);
            }
            return new AttachedPropertyInfo(name, propType, GetType(str2), GetType(str), null, isKey);
        }

        private static Assembly[] GetAssemblies() => 
            AppDomain.CurrentDomain.GetAssemblies();

        protected override Dictionary<string, string> GetAttributes(XtraPropertyInfo pInfo)
        {
            Dictionary<string, string> attributes = base.GetAttributes(pInfo);
            AttachedPropertyInfo info = pInfo as AttachedPropertyInfo;
            if (info != null)
            {
                attributes.Add("OwnerType", info.OwnerType.FullName);
                attributes.Add("DependencyPropertyType", info.DependencyPropertyType.FullName);
            }
            return attributes;
        }

        private static Type GetType(string typeName)
        {
            Type type;
            if (!typeCache.TryGetValue(typeName, out type) && TryGetTypeFromLoadedAssemblies(typeName, out type))
            {
                typeCache.Add(typeName, type);
            }
            return type;
        }

        private static bool TryGetTypeFromLoadedAssemblies(string typeName, out Type type)
        {
            type = Type.GetType(typeName, false);
            if (type == null)
            {
                Assembly[] assemblies = GetAssemblies();
                for (int i = 0; i < assemblies.Length; i++)
                {
                    type = assemblies[i].GetType(typeName, false);
                    if (type != null)
                    {
                        break;
                    }
                }
            }
            return (type != null);
        }
    }
}

