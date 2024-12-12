namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Data.Access;
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class SerializationContext
    {
        private readonly Dictionary<int, MethodInfo> methodInfos = new Dictionary<int, MethodInfo>();
        private readonly Dictionary<int, XtraSerializableProperty> xtraSerializablePropertyAttributes = new Dictionary<int, XtraSerializableProperty>();
        private readonly Dictionary<int, int> propertyIds = new Dictionary<int, int>();
        private readonly Dictionary<int, bool> propertySerializationVisibilities = new Dictionary<int, bool>();

        protected internal virtual void AfterDeserializeRootObject()
        {
        }

        protected internal virtual bool AllowProperty(SerializeHelperBase helper, object obj, PropertyDescriptor prop, OptionsLayoutBase options, bool isSerializing)
        {
            IXtraSerializableLayoutEx ex = obj as IXtraSerializableLayoutEx;
            if (ex == null)
            {
                return true;
            }
            int propertyId = this.GetPropertyId(helper, prop);
            return ((propertyId == -1) || ex.AllowProperty(options, prop.Name, propertyId));
        }

        protected internal virtual bool CanDeserializeProperty(object obj, PropertyDescriptor prop) => 
            (obj != null) && (prop != null);

        protected internal virtual bool CustomDeserializeProperty(DeserializeHelper helper, object obj, PropertyDescriptor property, XtraPropertyInfo propertyInfo)
        {
            MethodInfo method = helper.GetMethod(obj, helper.GetMethodName(property.Name, "Deserialize"));
            if (method == null)
            {
                return false;
            }
            object[] parameters = new object[] { new XtraEventArgs(propertyInfo) };
            method.Invoke(obj, parameters);
            return true;
        }

        protected internal virtual void CustomGetSerializableProperties(object obj, List<SerializablePropertyDescriptorPair> pairsList, PropertyDescriptorCollection props)
        {
        }

        protected internal virtual void DeserializeObjectsCore(DeserializeHelper helper, IList objects, IXtraPropertyCollection store, OptionsLayoutBase options)
        {
            objects = SerializeHelperBase.CheckObjects(objects);
            if ((objects != null) && (store != null))
            {
                for (int i = 0; i < objects.Count; i++)
                {
                    XtraObjectInfo objectInfo = (XtraObjectInfo) objects[i];
                    IXtraPropertyCollection list = GetObjectProperties(objectInfo, store, i);
                    if (list != null)
                    {
                        this.OnDeserializeObjectsObject(helper, objectInfo.Instance, list, options);
                        if (ReferenceEquals(list, store))
                        {
                            break;
                        }
                    }
                }
            }
        }

        protected internal virtual int GetCollectionItemsCount(XtraPropertyInfo root) => 
            Convert.ToInt32(root.Value);

        internal MethodInfo GetMethod(object obj, string name)
        {
            if (obj != null)
            {
                MethodInfo method;
                Type baseType = obj.GetType();
                int key = MemberKey.Create(baseType, name);
                if (this.methodInfos.TryGetValue(key, out method))
                {
                    return method;
                }
                while (baseType != null)
                {
                    method = baseType.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                    if (method != null)
                    {
                        this.methodInfos.Add(key, method);
                        return method;
                    }
                    baseType = baseType.GetBaseType();
                }
                this.methodInfos.Add(key, null);
            }
            return null;
        }

        private static IXtraPropertyCollection GetObjectProperties(XtraObjectInfo objectInfo, IXtraPropertyCollection store, int index)
        {
            IXtraPropertyCollection propertys = new XtraPropertyCollection();
            for (int i = 0; i < store.Count; i++)
            {
                XtraPropertyInfo info = store[i];
                if (!info.IsXtraObjectInfo)
                {
                    if (i == 0)
                    {
                        return store;
                    }
                }
                else if (objectInfo.Name == info.Value.ToString())
                {
                    propertys.AddRange(info.ChildProperties);
                    return propertys;
                }
            }
            return ((propertys.Count <= 0) ? null : propertys);
        }

        protected internal virtual PropertyDescriptorCollection GetProperties(object obj) => 
            this.GetProperties(obj, null);

        protected internal virtual PropertyDescriptorCollection GetProperties(object obj, IXtraPropertyCollection store) => 
            TypeDescriptor.GetProperties(obj);

        protected int GetPropertyId(SerializeHelperBase helper, PropertyDescriptor property)
        {
            int propertyId;
            if (property.ComponentType == null)
            {
                return helper.GetPropertyId(property);
            }
            int key = MemberKey.Create(property.ComponentType, property.Name);
            if (!this.propertyIds.TryGetValue(key, out propertyId))
            {
                propertyId = helper.GetPropertyId(property);
                this.propertyIds.Add(key, propertyId);
            }
            return propertyId;
        }

        private IXtraSerializableCollection GetSerializableCollection(XtraItemEventArgs e) => 
            e.Collection as IXtraSerializableCollection;

        protected internal virtual MethodInfo GetShouldSerializeCollectionMethodInfo(SerializeHelper helper, string name, object owner) => 
            helper.GetMethod(owner, helper.GetMethodNameItem(name, "ShouldSerializeCollection"));

        internal virtual XtraSerializableProperty GetXtraSerializableProperty(object obj, PropertyDescriptor property)
        {
            XtraSerializableProperty attribute;
            if (property.ComponentType == null)
            {
                return (property.Attributes[typeof(XtraSerializableProperty)] as XtraSerializableProperty);
            }
            int key = MemberKey.Create(property.ComponentType, property.Name);
            if (!this.xtraSerializablePropertyAttributes.TryGetValue(key, out attribute))
            {
                attribute = property.GetAttribute<XtraSerializableProperty>();
                this.xtraSerializablePropertyAttributes.Add(key, attribute);
            }
            return attribute;
        }

        protected internal virtual void InvokeAfterDeserialize(DeserializeHelper helper, object obj, XtraPropertyInfo bp, object value)
        {
        }

        protected internal virtual void InvokeAfterDeserializeCollection(XtraItemEventArgs e)
        {
            IXtraSupportDeserializeCollection owner = e.Owner as IXtraSupportDeserializeCollection;
            if (owner != null)
            {
                owner.AfterDeserializeCollection(e.Item.Name, e);
            }
            IXtraSerializableCollection serializableCollection = this.GetSerializableCollection(e);
            if (serializableCollection == null)
            {
                IXtraSerializableCollection local1 = serializableCollection;
            }
            else
            {
                serializableCollection.AfterDeserialize(e);
            }
        }

        protected internal virtual void InvokeBeforeDeserializeCollection(XtraItemEventArgs e)
        {
            IXtraSupportDeserializeCollection owner = e.Owner as IXtraSupportDeserializeCollection;
            if (owner != null)
            {
                owner.BeforeDeserializeCollection(e.Item.Name, e);
            }
            IXtraSerializableCollection serializableCollection = this.GetSerializableCollection(e);
            if (serializableCollection == null)
            {
                IXtraSerializableCollection local1 = serializableCollection;
            }
            else
            {
                serializableCollection.BeforeDeserialize(e);
            }
        }

        protected internal virtual void InvokeClearCollection(DeserializeHelper helper, XtraItemEventArgs e)
        {
            IXtraSupportDeserializeCollection owner = e.Owner as IXtraSupportDeserializeCollection;
            if (((owner == null) || !owner.ClearCollection(e.Item.Name, e)) && ((this.GetSerializableCollection(e) == null) || !this.GetSerializableCollection(e).Clear(e)))
            {
                MethodInfo method = helper.GetMethod(e.Owner, helper.GetMethodName(e.Item.Name, "Clear"));
                if (method != null)
                {
                    object[] parameters = new object[] { e };
                    method.Invoke(e.Owner, parameters);
                }
                else if (e.Collection is IList)
                {
                    ((IList) e.Collection).Clear();
                }
            }
        }

        protected internal virtual object InvokeCreateCollectionItem(DeserializeHelper helper, string propertyName, XtraItemEventArgs e)
        {
            IXtraSupportDeserializeCollectionItem owner = e.Owner as IXtraSupportDeserializeCollectionItem;
            if (owner != null)
            {
                return owner.CreateCollectionItem(propertyName, e);
            }
            if (this.GetSerializableCollection(e) != null)
            {
                return this.GetSerializableCollection(e).CreateItem(e);
            }
            MethodInfo method = helper.GetMethod(e.Owner, helper.GetMethodNameItem(propertyName, "Create"));
            if (method == null)
            {
                return null;
            }
            object[] parameters = new object[] { e };
            return method.Invoke(e.Owner, parameters);
        }

        protected internal virtual object InvokeCreateContentPropertyValueMethod(DeserializeHelper helper, XtraItemEventArgs e, PropertyDescriptor prop)
        {
            MethodInfo method = helper.GetMethod(e.Owner, helper.GetMethodName(prop.Name, "Create"));
            object obj2 = null;
            if (method != null)
            {
                object[] parameters = new object[] { e };
                obj2 = method.Invoke(e.Owner, parameters);
                prop.SetValue(e.Owner, obj2);
            }
            return obj2;
        }

        protected internal virtual object InvokeFindCollectionItem(DeserializeHelper helper, string propertyName, XtraItemEventArgs e)
        {
            MethodInfo method = helper.GetMethod(e.Owner, helper.GetMethodNameItem(propertyName, "Find"));
            if (method == null)
            {
                return null;
            }
            object[] parameters = new object[] { e };
            return method.Invoke(e.Owner, parameters);
        }

        protected internal virtual void InvokeRemoveCollectionItem(DeserializeHelper helper, string propertyName, XtraSetItemIndexEventArgs e)
        {
            IXtraSupportDeserializeCollectionItemEx owner = e.Owner as IXtraSupportDeserializeCollectionItemEx;
            if (owner != null)
            {
                owner.RemoveCollectionItem(propertyName, e);
            }
            else if (this.GetSerializableCollection(e) != null)
            {
                this.GetSerializableCollection(e).RemoveItem(e);
            }
            else
            {
                MethodInfo method = helper.GetMethod(e.Owner, helper.GetMethodNameItem(propertyName, "Remove"));
                if (method != null)
                {
                    object[] parameters = new object[] { e };
                    method.Invoke(e.Owner, parameters);
                }
            }
        }

        protected internal virtual void InvokeSetIndexCollectionItem(DeserializeHelper helper, string propertyName, XtraSetItemIndexEventArgs e)
        {
            IXtraSupportDeserializeCollectionItem owner = e.Owner as IXtraSupportDeserializeCollectionItem;
            if (owner != null)
            {
                owner.SetIndexCollectionItem(propertyName, e);
            }
            else if ((this.GetSerializableCollection(e) == null) || !this.GetSerializableCollection(e).SetItemIndex(e))
            {
                MethodInfo method = helper.GetMethod(e.Owner, helper.GetMethodNameItem(propertyName, "SetIndex"));
                if (method != null)
                {
                    object[] parameters = new object[] { e };
                    method.Invoke(e.Owner, parameters);
                }
            }
        }

        protected internal virtual bool InvokeShouldSerialize(SerializeHelper helper, object obj, PropertyDescriptor property)
        {
            string name = property.Name;
            IXtraSupportShouldSerialize serialize = obj as IXtraSupportShouldSerialize;
            if (serialize != null)
            {
                return serialize.ShouldSerialize(name);
            }
            MethodInfo method = helper.GetMethod(obj, helper.GetMethodName(name, "ShouldSerialize"));
            return ((method == null) || ((bool) method.Invoke(obj, null)));
        }

        internal bool IsDesignerSerializationVisible(PropertyDescriptor property)
        {
            bool flag;
            if (property.ComponentType == null)
            {
                return (property.SerializationVisibility == DesignerSerializationVisibility.Visible);
            }
            int key = MemberKey.Create(property.ComponentType, property.Name);
            if (!this.propertySerializationVisibilities.TryGetValue(key, out flag))
            {
                flag = property.SerializationVisibility == DesignerSerializationVisibility.Visible;
                this.propertySerializationVisibilities.Add(key, flag);
            }
            return flag;
        }

        protected virtual void OnDeserializeObjectsObject(DeserializeHelper helper, object obj, IXtraPropertyCollection list, OptionsLayoutBase options)
        {
            helper.DeserializeObject(obj, list, options);
        }

        protected virtual IXtraPropertyCollection OnSerializeObjectsObject(SerializeHelper helper, object obj, OptionsLayoutBase options) => 
            helper.SerializeObject(obj, options);

        protected internal virtual void ResetProperty(DeserializeHelper helper, object obj, PropertyDescriptor property, XtraSerializableProperty attr)
        {
            MethodInfo method = helper.GetMethod(obj, helper.GetMethodName(property.Name, "Reset"));
            if (method != null)
            {
                method.Invoke(obj, new object[0]);
            }
        }

        protected internal virtual IXtraPropertyCollection SerializeObjectsCore(SerializeHelper helper, IList objects, OptionsLayoutBase options)
        {
            IXtraPropertyCollection propertys3;
            objects = SerializeHelperBase.CheckObjects(objects);
            if (objects == null)
            {
                return null;
            }
            IXtraPropertyCollection propertys = new XtraPropertyCollection(objects.Count);
            using (IEnumerator enumerator = objects.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        XtraObjectInfo current = (XtraObjectInfo) enumerator.Current;
                        IXtraPropertyCollection props = this.OnSerializeObjectsObject(helper, current.Instance, options);
                        if (objects.Count > 1)
                        {
                            XtraPropertyInfo prop = new XtraPropertyInfo(current);
                            prop.ChildProperties.AddRange(props);
                            propertys.Add(prop);
                            continue;
                        }
                        propertys3 = props;
                    }
                    else
                    {
                        return propertys;
                    }
                    break;
                }
            }
            return propertys3;
        }

        protected internal virtual bool ShouldSerializeCollectionItem(SerializeHelper helper, object owner, MethodInfo mi, XtraPropertyInfo itemProperty, object item, XtraItemEventArgs e)
        {
            bool flag;
            if (mi == null)
            {
                return true;
            }
            itemProperty.PropertyType = mi.ReturnType;
            itemProperty.Value = item;
            try
            {
                object[] parameters = new object[] { e };
                flag = (bool) mi.Invoke(owner, parameters);
            }
            finally
            {
                itemProperty.Value = null;
            }
            return flag;
        }

        protected internal virtual bool ShouldSerializeProperty(SerializeHelper helper, object obj, PropertyDescriptor prop, XtraSerializableProperty xtraSerializableProperty) => 
            true;

        protected internal virtual IList<SerializablePropertyDescriptorPair> SortProps(object obj, List<SerializablePropertyDescriptorPair> pairsList)
        {
            Func<SerializablePropertyDescriptorPair, SerializablePropertyDescriptorPair> keySelector = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<SerializablePropertyDescriptorPair, SerializablePropertyDescriptorPair> local1 = <>c.<>9__5_0;
                keySelector = <>c.<>9__5_0 = x => x;
            }
            return new DXCollection<SerializablePropertyDescriptorPair>(pairsList.OrderBy<SerializablePropertyDescriptorPair, SerializablePropertyDescriptorPair>(keySelector, DefaultPropertyDescriptorComparer.Instance));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SerializationContext.<>c <>9 = new SerializationContext.<>c();
            public static Func<SerializablePropertyDescriptorPair, SerializablePropertyDescriptorPair> <>9__5_0;

            internal SerializablePropertyDescriptorPair <SortProps>b__5_0(SerializablePropertyDescriptorPair x) => 
                x;
        }

        private static class MemberKey
        {
            private const int Prime = 0x1000193;
            private const int Basis = 0x50c5d1f;

            public static int Create(Type type, string memberName) => 
                ((0x50c5d1f ^ type.GetHashCode()) * 0x1000193) ^ memberName.GetHashCode();
        }
    }
}

