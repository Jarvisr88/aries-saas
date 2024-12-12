namespace DevExpress.Xpf.Core.Serialization
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.Xpf.Core.Serialization.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Data;

    public class DXSerializationContext : SerializationContext
    {
        protected override bool AllowProperty(SerializeHelperBase helper, object obj, PropertyDescriptor prop, OptionsLayoutBase options, bool isSerializing)
        {
            SerializationProviderWrapper serializationProviderWrapper = GetSerializationProviderWrapper(helper, obj);
            if (serializationProviderWrapper == null)
            {
                return base.AllowProperty(helper, obj, prop, options, isSerializing);
            }
            int propertyId = base.GetPropertyId(helper, prop);
            return ((propertyId == -1) || serializationProviderWrapper.AllowProperty(obj, prop, propertyId, isSerializing));
        }

        protected override bool CanDeserializeProperty(object obj, PropertyDescriptor prop)
        {
            DependencyObject dependencyObject = obj as DependencyObject;
            DependencyPropertyDescriptor descriptor = DependencyPropertyDescriptor.FromProperty(prop);
            if ((dependencyObject != null) && (descriptor != null))
            {
                BindingExpression expression = dependencyObject.ReadLocalValue(descriptor.DependencyProperty) as BindingExpression;
                if ((expression != null) && (expression.ParentBinding != null))
                {
                    BindingMode mode = expression.ParentBinding.Mode;
                    if (mode == BindingMode.Default)
                    {
                        FrameworkPropertyMetadata metadata = descriptor.DependencyProperty.GetMetadata(dependencyObject) as FrameworkPropertyMetadata;
                        mode = ((metadata == null) || !metadata.BindsTwoWayByDefault) ? BindingMode.OneWay : BindingMode.TwoWay;
                    }
                    return ((mode == BindingMode.TwoWay) || (mode == BindingMode.OneWayToSource));
                }
            }
            return base.CanDeserializeProperty(obj, prop);
        }

        protected override bool CustomDeserializeProperty(DeserializeHelper helper, object obj, PropertyDescriptor property, XtraPropertyInfo propertyInfo)
        {
            SerializationProviderWrapper serializationProviderWrapper = GetSerializationProviderWrapper(helper, obj);
            return ((serializationProviderWrapper == null) ? base.CustomDeserializeProperty(helper, obj, property, propertyInfo) : serializationProviderWrapper.CustomDeserializeProperty(obj, property, propertyInfo));
        }

        protected override void CustomGetSerializableProperties(object obj, List<SerializablePropertyDescriptorPair> pairsList, PropertyDescriptorCollection props)
        {
            DependencyObject dObj = obj as DependencyObject;
            if (dObj != null)
            {
                DXSerializer.GetSerializationProvider(dObj).OnCustomGetSerializableProperties(dObj, new CustomGetSerializablePropertiesEventArgs(obj, pairsList, props));
            }
        }

        protected override PropertyDescriptorCollection GetProperties(object obj, IXtraPropertyCollection store)
        {
            List<PropertyDescriptor> list = new List<PropertyDescriptor>(base.GetProperties(obj, store).Cast<PropertyDescriptor>());
            if ((store != null) && (obj != null))
            {
                foreach (XtraPropertyInfo info in store)
                {
                    AttachedPropertyInfo info2 = info as AttachedPropertyInfo;
                    if (info2 != null)
                    {
                        DependencyPropertyDescriptor item = DependencyPropertyDescriptor.FromName(info2.DependencyPropertyName, info2.OwnerType, obj.GetType());
                        if (item != null)
                        {
                            list.Add(item);
                        }
                    }
                }
            }
            return new PropertyDescriptorCollection(list.ToArray(), true);
        }

        private static SerializationProviderWrapper GetSerializationProviderWrapper(SerializeHelperBase helper, object obj)
        {
            DXSerializer.DXDeserializeHelper helper2 = helper as DXSerializer.DXDeserializeHelper;
            if (helper2 != null)
            {
                return (helper2.GetDXObj(obj) as SerializationProviderWrapper);
            }
            DXSerializer.DXSerializeHelper helper3 = helper as DXSerializer.DXSerializeHelper;
            return ((helper3 == null) ? null : (helper3.GetDXObj(obj) as SerializationProviderWrapper));
        }

        protected override void InvokeClearCollection(DeserializeHelper helper, XtraItemEventArgs e)
        {
            SerializationProviderWrapper serializationProviderWrapper = GetSerializationProviderWrapper(helper, e.Owner);
            if (serializationProviderWrapper != null)
            {
                serializationProviderWrapper.OnClearCollection(e);
            }
            else
            {
                base.InvokeClearCollection(helper, e);
            }
        }

        protected override object InvokeCreateCollectionItem(DeserializeHelper helper, string propertyName, XtraItemEventArgs e)
        {
            SerializationProviderWrapper serializationProviderWrapper = GetSerializationProviderWrapper(helper, e.Owner);
            return ((serializationProviderWrapper == null) ? base.InvokeCreateCollectionItem(helper, propertyName, e) : serializationProviderWrapper.OnCreateCollectionItem(propertyName, e));
        }

        protected override object InvokeCreateContentPropertyValueMethod(DeserializeHelper helper, XtraItemEventArgs e, PropertyDescriptor prop)
        {
            SerializationProviderWrapper serializationProviderWrapper = GetSerializationProviderWrapper(helper, e.Owner);
            return ((serializationProviderWrapper == null) ? base.InvokeCreateContentPropertyValueMethod(helper, e, prop) : serializationProviderWrapper.OnCreateContentPropertyValue(prop, e));
        }

        protected override object InvokeFindCollectionItem(DeserializeHelper helper, string propertyName, XtraItemEventArgs e)
        {
            SerializationProviderWrapper serializationProviderWrapper = GetSerializationProviderWrapper(helper, e.Owner);
            return ((serializationProviderWrapper == null) ? base.InvokeFindCollectionItem(helper, propertyName, e) : serializationProviderWrapper.OnFindCollectionItem(propertyName, e));
        }

        protected override void ResetProperty(DeserializeHelper helper, object obj, PropertyDescriptor prop, XtraSerializableProperty attr)
        {
            SerializationProviderWrapper serializationProviderWrapper = GetSerializationProviderWrapper(helper, obj);
            if (serializationProviderWrapper != null)
            {
                serializationProviderWrapper.ResetProperty(obj, prop, attr);
            }
            else
            {
                base.ResetProperty(helper, obj, prop, attr);
            }
        }

        protected override bool ShouldSerializeCollectionItem(SerializeHelper helper, object owner, MethodInfo mi, XtraPropertyInfo itemProperty, object item, XtraItemEventArgs e)
        {
            SerializationProviderWrapper serializationProviderWrapper = GetSerializationProviderWrapper(helper, e.Owner);
            return ((serializationProviderWrapper == null) ? base.ShouldSerializeCollectionItem(helper, owner, mi, itemProperty, item, e) : serializationProviderWrapper.OnShouldSerailizaCollectionItem(e, item));
        }

        internal static bool ShouldSerializeDependencyProeprty(DependencyPropertyDescriptor dependencyPropertyDescriptor, object obj) => 
            (obj is DependencyObject) && (DependencyPropertyHelper.GetValueSource((DependencyObject) obj, dependencyPropertyDescriptor.DependencyProperty).BaseValueSource >= BaseValueSource.ParentTemplate);

        protected override bool ShouldSerializeProperty(SerializeHelper helper, object obj, PropertyDescriptor prop, XtraSerializableProperty xtraSerializableProperty)
        {
            SerializationProviderWrapper serializationProviderWrapper = GetSerializationProviderWrapper(helper, obj);
            if (serializationProviderWrapper != null)
            {
                bool? nullable = serializationProviderWrapper.CustomShouldSerializeProperty(obj, prop);
                return ((nullable == null) ? serializationProviderWrapper.OnShouldSerializeProperty(obj, prop, xtraSerializableProperty) : nullable.Value);
            }
            DependencyPropertyDescriptor dependencyPropertyDescriptor = DependencyPropertyDescriptor.FromProperty(prop);
            return ((dependencyPropertyDescriptor == null) ? base.ShouldSerializeProperty(helper, obj, prop, xtraSerializableProperty) : ShouldSerializeDependencyProeprty(dependencyPropertyDescriptor, obj));
        }
    }
}

