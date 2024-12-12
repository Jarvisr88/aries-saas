namespace DevExpress.Utils.Design
{
    using Microsoft.Win32;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Reflection;

    public class InheritanceHelper
    {
        private static object disableVisualInheritance;

        public static bool AllowCollectionItemEdit(Component baseComponent, PropertyDescriptor collectionDescriptor, ICollection collection)
        {
            InheritanceAttribute attribute = (InheritanceAttribute) TypeDescriptor.GetAttributes(baseComponent)[typeof(InheritanceAttribute)];
            return (((attribute == null) || !attribute.Equals(InheritanceAttribute.InheritedReadOnly)) ? IsComponentCollection(collection) : false);
        }

        public static bool AllowCollectionItemEdit(Component baseComponent, PropertyDescriptor collectionDescriptor, ICollection collection, object item)
        {
            InheritanceAttribute attribute = (InheritanceAttribute) TypeDescriptor.GetAttributes(item)[typeof(InheritanceAttribute)];
            return (((attribute == null) || (attribute.InheritanceLevel != InheritanceLevel.InheritedReadOnly)) ? IsComponentCollection(collection) : false);
        }

        public static bool AllowCollectionItemRemove(Component baseComponent, PropertyDescriptor collectionDescriptor, ICollection collection, object item)
        {
            if (!AllowCollectionModify(baseComponent, collectionDescriptor, collection))
            {
                return false;
            }
            InheritanceAttribute attribute = (InheritanceAttribute) TypeDescriptor.GetAttributes(item)[typeof(InheritanceAttribute)];
            return ((attribute == null) || (attribute.InheritanceLevel == InheritanceLevel.NotInherited));
        }

        public static bool AllowCollectionModify(Component baseComponent, PropertyDescriptor collectionDescriptor, ICollection collection)
        {
            InheritanceAttribute attribute = (InheritanceAttribute) TypeDescriptor.GetAttributes(baseComponent)[typeof(InheritanceAttribute)];
            if ((attribute != null) && attribute.Equals(InheritanceAttribute.InheritedReadOnly))
            {
                return false;
            }
            if (collectionDescriptor.Converter.GetType().Name == "ReadOnlyCollectionConverter")
            {
                return false;
            }
            DXInheritedPropertyDescriptor descriptor = collectionDescriptor as DXInheritedPropertyDescriptor;
            return ((descriptor == null) ? IsComponentCollection(collection) : (descriptor.IsEmptyOriginalCollection || IsComponentCollection(collection)));
        }

        public static Component GetComponent(object obj)
        {
            IDesigner designer = obj as IDesigner;
            if (designer != null)
            {
                return (designer.Component as Component);
            }
            Component component = obj as Component;
            return ((component == null) ? null : component);
        }

        public static Type GetIndexerPropertyType(ICollection collection)
        {
            if (collection != null)
            {
                PropertyInfo[] properties = collection.GetType().GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    if ("Item".Equals(properties[i].Name) && (properties[i].PropertyType != typeof(object)))
                    {
                        return properties[i].PropertyType;
                    }
                }
            }
            return null;
        }

        public static InheritanceAttribute GetInheritanceAttribute(object obj)
        {
            Component component = GetComponent(obj);
            if (component == null)
            {
                return null;
            }
            return GetInheritanceService(component)?.GetInheritanceAttribute(component);
        }

        public static IInheritanceService GetInheritanceService(object obj) => 
            GetService(obj, typeof(IInheritanceService)) as IInheritanceService;

        public static object GetService(object obj, Type type) => 
            GetServiceProvider(obj).GetService(type);

        public static IServiceProvider GetServiceProvider(object obj)
        {
            Component component = GetComponent(obj);
            if ((component != null) && (component.Site != null))
            {
                return component.Site;
            }
            IServiceProvider provider = obj as IServiceProvider;
            return Provider.NullServiceProvider;
        }

        internal static bool IsComponentCollection(ICollection collection)
        {
            Type indexerPropertyType = GetIndexerPropertyType(collection);
            return ((indexerPropertyType != null) && typeof(IComponent).IsAssignableFrom(indexerPropertyType));
        }

        public static bool DisableVisualInheritance
        {
            get
            {
                if (disableVisualInheritance == null)
                {
                    disableVisualInheritance = true;
                    try
                    {
                        RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Developer Express");
                        if (key != null)
                        {
                            disableVisualInheritance = bool.Parse(key.GetValue("DisableVisualInheritance", "true").ToString());
                        }
                    }
                    catch
                    {
                        disableVisualInheritance = true;
                    }
                }
                return (bool) disableVisualInheritance;
            }
        }

        private class Provider : IServiceProvider
        {
            internal static InheritanceHelper.Provider NullServiceProvider = new InheritanceHelper.Provider();

            object IServiceProvider.GetService(Type serviceType) => 
                null;
        }
    }
}

