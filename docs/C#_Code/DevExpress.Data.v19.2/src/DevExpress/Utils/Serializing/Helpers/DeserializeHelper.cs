namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Data.Internal;
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;

    public class DeserializeHelper : SerializeHelperBase
    {
        internal System.Exception exception;
        private bool resetProperties;
        private ObjectConverterImplementation objectConverterImpl;
        internal static float RoundingConstant = 0.01f;

        public event EventHandler<DeserializeExceptionEventArgs> ExceptionOccurred;

        public DeserializeHelper()
        {
            this.SetDefaultObjectConverterImpl();
        }

        public DeserializeHelper(object rootObject, bool resetProperties = true, SerializationContext context = null) : base(rootObject, context)
        {
            this.resetProperties = resetProperties;
            this.SetDefaultObjectConverterImpl();
        }

        protected static void AddRange(IList<object> where, ICollection what)
        {
            foreach (object obj2 in what)
            {
                where.Add(obj2);
            }
        }

        public virtual void AfterDeserializeRootObject()
        {
            base.Context.AfterDeserializeRootObject();
        }

        internal static void CallEndDeserializing(object obj, string layoutVersion)
        {
            IXtraSerializable serializable = obj as IXtraSerializable;
            if (serializable != null)
            {
                serializable.OnEndDeserializing(layoutVersion);
            }
        }

        internal static bool CallStartDeserializing(object obj, string layoutVersion)
        {
            IXtraSerializable serializable = obj as IXtraSerializable;
            if (serializable == null)
            {
                return false;
            }
            LayoutAllowEventArgs e = new LayoutAllowEventArgs(layoutVersion);
            serializable.OnStartDeserializing(e);
            return !e.Allow;
        }

        private bool CheckIfNoAutoScale(XtraSerializableProperty attr, PropertyDescriptor prop, object val)
        {
            if (attr.IsAutoScaleIgnoreDefault)
            {
                DefaultValueAttribute attribute = prop.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
                try
                {
                    if ((attribute != null) && Equals(attribute.Value, val))
                    {
                        return true;
                    }
                }
                catch
                {
                }
            }
            return false;
        }

        private bool ContainsCollectionItem(XtraSerializableProperty attr, ICollection prevCollection, object item, object owner)
        {
            bool flag;
            object objA = this.GetCollectionItemId(attr, item, owner);
            using (IEnumerator enumerator = prevCollection.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        object current = enumerator.Current;
                        object objB = this.GetCollectionItemId(attr, current, owner);
                        if (!Equals(objA, objB))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        public virtual void DeserializeCollection(XtraSerializableProperty attr, XtraPropertyInfo root, object owner, object collection, OptionsLayoutBase options)
        {
            ICollection what = collection as ICollection;
            if ((what != null) && (owner != null))
            {
                List<object> where = new List<object>();
                if (attr.MergeCollection)
                {
                    AddRange(where, what);
                }
                XtraItemEventArgs e = new XtraItemEventArgs(base.rootObject, owner, collection, root, options);
                base.Context.InvokeBeforeDeserializeCollection(e);
                try
                {
                    if (attr.ClearCollection)
                    {
                        base.Context.InvokeClearCollection(this, e);
                    }
                    int collectionItemsCount = base.Context.GetCollectionItemsCount(root);
                    if (collectionItemsCount >= 1)
                    {
                        int num2 = 1;
                        while (true)
                        {
                            if (num2 >= (collectionItemsCount + 1))
                            {
                                if (attr.IsCollectionContent)
                                {
                                    this.DeserializeCollectionContent(root, collection, options);
                                }
                                if (attr.MergeCollection)
                                {
                                    this.MergeCollection(attr, root, owner, where, what, options);
                                }
                                break;
                            }
                            XtraPropertyInfo item = root.ChildProperties["Item" + num2.ToString()];
                            if (item != null)
                            {
                                this.DeserializeCollectionItem(attr, root, owner, collection, item, num2 - 1, options);
                            }
                            num2++;
                        }
                    }
                }
                finally
                {
                    base.Context.InvokeAfterDeserializeCollection(e);
                }
            }
        }

        private void DeserializeCollectionContent(XtraPropertyInfo root, object collection, OptionsLayoutBase options)
        {
            XtraPropertyInfo info = root.ChildProperties["Content"];
            if (info != null)
            {
                this.DeserializeObject(collection, info.ChildProperties, options);
            }
        }

        protected void DeserializeCollectionItem(XtraSerializableProperty attr, XtraPropertyInfo root, object owner, object collection, XtraPropertyInfo item, int index, OptionsLayoutBase options)
        {
            if (collection is ICollection)
            {
                XtraSetItemIndexEventArgs e = new XtraSetItemIndexEventArgs(base.rootObject, owner, collection, item, index);
                object obj2 = this.TryGetCollectionItemFromCache(attr, root.Name, item);
                if (obj2 != null)
                {
                    item.Value = obj2;
                    this.InvokeSetIndexCollectionItem(root.Name, e);
                }
                else
                {
                    object collItem = null;
                    if (attr.UseFindItem)
                    {
                        collItem = base.Context.InvokeFindCollectionItem(this, root.Name, new XtraItemEventArgs(base.rootObject, owner, collection, item));
                    }
                    bool newItem = false;
                    if ((collItem == null) && attr.UseCreateItem)
                    {
                        collItem = base.Context.InvokeCreateCollectionItem(this, root.Name, new XtraItemEventArgs(base.rootObject, owner, collection, item, options, index));
                        newItem = collItem != null;
                    }
                    if ((collItem == null) && ((attr.Visibility == XtraSerializationVisibility.SimpleCollection) && (item.PropertyType != null)))
                    {
                        try
                        {
                            collItem = Convert.ChangeType(item.Value, item.PropertyType, CultureInfo.InvariantCulture);
                        }
                        catch
                        {
                        }
                    }
                    this.InsertItemIntoCollection(attr, root, collection, item, options, e, collItem, newItem);
                }
            }
        }

        public void DeserializeObject(object obj, IXtraPropertyCollection store, OptionsLayoutBase options)
        {
            this.DeserializeObject(obj, store, XtraSerializationFlags.None, options);
        }

        protected void DeserializeObject(object obj, IXtraPropertyCollection store, XtraSerializationFlags parentFlags, OptionsLayoutBase options)
        {
            if (store != null)
            {
                IXtraSerializable2 serializable = obj as IXtraSerializable2;
                IXtraSerializableLayout layout = obj as IXtraSerializableLayout;
                IXtraSerializableLayout2 layout2 = obj as IXtraSerializableLayout2;
                IXtraSerializableLayoutEx ex = obj as IXtraSerializableLayoutEx;
                if (layout2 != null)
                {
                    options.currentLayoutScaleFactor = new SizeF?(layout2.LayoutScaleFactor);
                }
                SizeF? layoutScaleFactor = GetLayoutScaleFactor(store);
                if (layoutScaleFactor != null)
                {
                    options.storedLayoutScaleFactor = layoutScaleFactor;
                }
                string rootVersion = this.GetRootVersion();
                if (layout != null)
                {
                    rootVersion = GetLayoutVersion(store);
                }
                if (!this.RaiseStartDeserializing(obj, rootVersion))
                {
                    try
                    {
                        if ((ex != null) && this.ResetProperties)
                        {
                            ex.ResetProperties(options);
                        }
                        if (serializable != null)
                        {
                            serializable.Deserialize((IList) store);
                        }
                        else
                        {
                            IXtraPartlyDeserializable deserializable = obj as IXtraPartlyDeserializable;
                            if (deserializable != null)
                            {
                                deserializable.Deserialize(base.RootObject, store);
                            }
                            List<SerializablePropertyDescriptorPair> properties = this.GetProperties(obj, store);
                            if ((properties != null) && (properties.Count != 0))
                            {
                                foreach (SerializablePropertyDescriptorPair pair in this.SortProps(obj, properties))
                                {
                                    PropertyDescriptor property = pair.Property;
                                    try
                                    {
                                        if (base.AllowProperty(obj, property, pair.Attribute, options, false))
                                        {
                                            this.DeserializeProperty(store, obj, pair, parentFlags, options);
                                        }
                                    }
                                    catch (System.Exception exception1)
                                    {
                                        System.Exception exception = XtraSerializationSecurityTrace.XtraSerializationSecurityException.Unwrap(exception1);
                                        this.exception = exception;
                                        this.RaiseExceptionOccurred(exception);
                                        if (exception is XtraSerializationSecurityTrace.XtraSerializationSecurityException)
                                        {
                                            throw;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    finally
                    {
                        this.RaiseEndDeserializing(obj, rootVersion);
                    }
                }
            }
        }

        public void DeserializeObjects(IList objects, IXtraPropertyCollection store, OptionsLayoutBase options)
        {
            base.Context.DeserializeObjectsCore(this, objects, store, options);
        }

        protected void DeserializeProperty(IXtraPropertyCollection store, object obj, SerializablePropertyDescriptorPair pair, XtraSerializationFlags parentFlags, OptionsLayoutBase options)
        {
            PropertyDescriptor prop = pair.Property;
            XtraSerializableProperty attribute = pair.Attribute;
            if ((attribute != null) && (attribute.Serialize && !this.TryGetValueFromCache(attribute, store, obj, prop)))
            {
                base.Context.ResetProperty(this, obj, prop, attribute);
                XtraPropertyInfo propertyInfo = this.FindProperty(store, prop.Name);
                if ((propertyInfo != null) && (base.Context.CanDeserializeProperty(obj, prop) && !base.Context.CustomDeserializeProperty(this, obj, prop, propertyInfo)))
                {
                    if (attribute.SerializeCollection)
                    {
                        object collection = prop.GetValue(obj);
                        this.DeserializeCollection(attribute, propertyInfo, obj, collection, options);
                    }
                    else
                    {
                        object obj3;
                        if ((attribute.Visibility == XtraSerializationVisibility.Content) && (propertyInfo.Value == null))
                        {
                            obj3 = attribute.UseCreateItem ? base.Context.InvokeCreateContentPropertyValueMethod(this, new XtraItemEventArgs(base.rootObject, obj, null, propertyInfo), prop) : prop.GetValue(obj);
                            if (obj3 != null)
                            {
                                this.DeserializeObject(obj3, propertyInfo.ChildProperties, attribute.Flags, options);
                                if (prop.PropertyType.IsValueType)
                                {
                                    prop.SetValue(obj, obj3);
                                }
                                base.Context.InvokeAfterDeserialize(this, obj, propertyInfo, obj3);
                            }
                        }
                        else if (attribute.Visibility != XtraSerializationVisibility.Reference)
                        {
                            System.Type propertyType = prop.PropertyType;
                            if (XtraPropertyInfo.IsPrimitiveOrTag(attribute, prop) && propertyInfo.EnsureIsPrimitive(this.ObjectConverterImpl).IsPrimitive)
                            {
                                propertyType = propertyInfo.PropertyType;
                            }
                            obj3 = this.ValueToObject(propertyInfo, propertyType);
                            if (attribute.IsAutoScale)
                            {
                                obj3 = this.ScaleValue(attribute, prop, obj3, options);
                            }
                            if ((attribute.Flags & XtraSerializationFlags.UseAssign) != XtraSerializationFlags.None)
                            {
                                MethodInfo method = this.GetMethod(obj, this.GetMethodName(prop.Name, "Assign"));
                                if (method != null)
                                {
                                    object[] parameters = new object[] { obj3 };
                                    method.Invoke(obj, parameters);
                                    return;
                                }
                            }
                            if (prop.Name == "Name")
                            {
                                this.RestoreNameProperty(prop, obj, obj3);
                            }
                            else
                            {
                                prop.SetValue(obj, obj3);
                            }
                        }
                    }
                }
            }
        }

        public XtraPropertyInfo FindProperty(IXtraPropertyCollection props, string name) => 
            FindPropertyCore(props, name);

        private static XtraPropertyInfo FindPropertyCore(IXtraPropertyCollection props, string name)
        {
            XtraPropertyInfo info2;
            XtraPropertyCollection propertys = props as XtraPropertyCollection;
            if (propertys != null)
            {
                return propertys[name];
            }
            using (IEnumerator enumerator = props.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        XtraPropertyInfo current = (XtraPropertyInfo) enumerator.Current;
                        if (current.Name != name)
                        {
                            continue;
                        }
                        info2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return info2;
        }

        private object GetCachedValue(string propertyName, int index) => 
            base.RootSerializationObject.GetObjectByIndex(propertyName, index);

        private static int GetCacheIndex(XtraPropertyInfo cacheIndexPropetyInfo)
        {
            int num;
            return (((cacheIndexPropetyInfo == null) || ((cacheIndexPropetyInfo.ChildProperties != null) && (cacheIndexPropetyInfo.ChildProperties.Count > 0))) ? -1 : (!int.TryParse(cacheIndexPropetyInfo.Value.ToString(), out num) ? -1 : num));
        }

        private object GetCollectionItemId(XtraSerializableProperty attr, object item, object owner)
        {
            IXtraSerializationIdProvider provider = owner as IXtraSerializationIdProvider;
            if (provider != null)
            {
                return provider.GetSerializationId(attr, item);
            }
            return item.GetType().GetProperty("Name", BindingFlags.Public | BindingFlags.Instance)?.GetValue(item, null);
        }

        public static SizeF? GetLayoutScaleFactor(IXtraPropertyCollection props)
        {
            XtraPropertyInfo info = FindPropertyCore(props, "#LayoutScaleFactor");
            if ((info != null) && (info.Value != null))
            {
                try
                {
                    object obj2 = info.ValueToObject(typeof(SizeF));
                    if (obj2 is SizeF)
                    {
                        return new SizeF?((SizeF) obj2);
                    }
                }
                catch
                {
                }
            }
            return null;
        }

        public static string GetLayoutVersion(IXtraPropertyCollection props)
        {
            XtraPropertyInfo info = FindPropertyCore(props, "#LayoutVersion");
            return ((info != null) ? (info.Value as string) : null);
        }

        protected virtual string GetRootVersion() => 
            string.Empty;

        private void InsertItemIntoCollection(XtraSerializableProperty attr, XtraPropertyInfo root, object collection, XtraPropertyInfo item, OptionsLayoutBase options, XtraSetItemIndexEventArgs setArgs, object collItem, bool newItem)
        {
            item.Value = collItem;
            if ((collItem != null) || item.IsNull)
            {
                if (attr.Visibility == XtraSerializationVisibility.NameCollection)
                {
                    if (collection is IList)
                    {
                        ((IList) collection).Add(collItem);
                        this.InvokeSetIndexCollectionItem(root.Name, setArgs);
                    }
                }
                else if (attr.Visibility == XtraSerializationVisibility.SimpleCollection)
                {
                    collItem = (collItem == null) ? null : this.ValueToObject(item, collItem.GetType());
                    item.Value = collItem;
                    if (collection is IList)
                    {
                        ((IList) collection).Add(collItem);
                        this.InvokeSetIndexCollectionItem(root.Name, setArgs);
                    }
                }
                else
                {
                    OptionsLayoutBase base2 = newItem ? OptionsLayoutBase.FullLayout : options;
                    if (newItem)
                    {
                        base2.AssignLayoutScaleInfo(options);
                    }
                    if (attr.DeserializeCollectionItemBeforeCallSetIndex)
                    {
                        this.DeserializeObject(collItem, item.ChildProperties, base2);
                    }
                    this.InvokeSetIndexCollectionItem(root.Name, setArgs);
                    if (!attr.DeserializeCollectionItemBeforeCallSetIndex)
                    {
                        this.DeserializeObject(collItem, item.ChildProperties, base2);
                    }
                    if (newItem)
                    {
                        base2.ClearLayoutScaleInfo();
                    }
                }
            }
        }

        private void InvokeRemoveCollectionItem(string propertyName, XtraSetItemIndexEventArgs e)
        {
            base.Context.InvokeRemoveCollectionItem(this, propertyName, e);
        }

        private void InvokeSetIndexCollectionItem(string propertyName, XtraSetItemIndexEventArgs e)
        {
            base.Context.InvokeSetIndexCollectionItem(this, propertyName, e);
        }

        private bool IsNewItem(XtraSerializableProperty attr, XtraPropertyInfo root, ICollection newCollection, object owner, object item)
        {
            object obj2 = this.GetCollectionItemId(attr, item, owner);
            MethodInfo method = this.GetMethod(owner, this.GetMethodName(root.Name, "IsNewItem"));
            if (method == null)
            {
                return false;
            }
            XtraPropertyInfo info1 = new XtraPropertyInfo();
            info1.Value = item;
            XtraNewItemEventArgs args = new XtraNewItemEventArgs(root, owner, newCollection, info1);
            object[] parameters = new object[] { args };
            method.Invoke(owner, parameters);
            return args.NewItem;
        }

        private bool IsOldItem(XtraSerializableProperty attr, XtraPropertyInfo root, ICollection newCollection, object owner, object item)
        {
            object obj2 = this.GetCollectionItemId(attr, item, owner);
            MethodInfo method = this.GetMethod(owner, this.GetMethodName(root.Name, "IsOldItem"));
            if (method == null)
            {
                return false;
            }
            XtraPropertyInfo info1 = new XtraPropertyInfo();
            info1.Value = item;
            XtraOldItemEventArgs args = new XtraOldItemEventArgs(root, owner, newCollection, info1);
            object[] parameters = new object[] { args };
            method.Invoke(owner, parameters);
            return args.OldItem;
        }

        protected void MergeCollection(XtraSerializableProperty attr, XtraPropertyInfo root, object owner, List<object> prevCollection, ICollection newCollection, OptionsLayoutBase options)
        {
            List<object> where = new List<object>();
            AddRange(where, newCollection);
            IEnumerator enumerator = prevCollection.GetEnumerator();
            IXtraCollectionDeserializationOptionsProvider provider = owner as IXtraCollectionDeserializationOptionsProvider;
            if ((provider != null) && provider.AddNewItems)
            {
                while (enumerator.MoveNext())
                {
                    if (this.ContainsCollectionItem(attr, newCollection, enumerator.Current, owner) || !this.IsNewItem(attr, root, newCollection, owner, enumerator.Current))
                    {
                        continue;
                    }
                    XtraPropertyInfo info1 = new XtraPropertyInfo();
                    info1.Value = enumerator.Current;
                    XtraPropertyInfo item = info1;
                    this.InsertItemIntoCollection(attr, root, newCollection, item, options, new XtraSetItemIndexEventArgs(null, owner, newCollection, item, newCollection.Count), enumerator.Current, true);
                }
            }
            if ((provider != null) && provider.RemoveOldItems)
            {
                foreach (object obj2 in where)
                {
                    if (!this.ContainsCollectionItem(attr, prevCollection, obj2, owner) && this.IsOldItem(attr, root, newCollection, owner, obj2))
                    {
                        XtraPropertyInfo info3 = new XtraPropertyInfo();
                        info3.Value = obj2;
                        XtraPropertyInfo item = info3;
                        this.RemoveItemFromCollection(root, new XtraSetItemIndexEventArgs(null, owner, newCollection, item, -1));
                    }
                }
            }
        }

        protected virtual void RaiseEndDeserializing(object obj, string restoredLayoutVersion)
        {
            CallEndDeserializing(obj, restoredLayoutVersion);
        }

        private void RaiseExceptionOccurred(System.Exception exception)
        {
            if (this.ExceptionOccurred == null)
            {
                EventHandler<DeserializeExceptionEventArgs> exceptionOccurred = this.ExceptionOccurred;
            }
            else
            {
                this.ExceptionOccurred(this, new DeserializeExceptionEventArgs(exception));
            }
        }

        protected virtual bool RaiseStartDeserializing(object obj, string restoredLayoutVersion) => 
            CallStartDeserializing(obj, restoredLayoutVersion);

        private void RemoveItemFromCollection(XtraPropertyInfo root, XtraSetItemIndexEventArgs e)
        {
            this.InvokeRemoveCollectionItem(root.Name, e);
        }

        public void RemoveProperty(IList store, string propertyName)
        {
            for (int i = store.Count - 1; i >= 0; i--)
            {
                XtraPropertyInfo info = store as XtraPropertyInfo;
                if ((info != null) && (info.Name == propertyName))
                {
                    store.RemoveAt(i);
                    return;
                }
            }
        }

        private void RestoreNameProperty(PropertyDescriptor descriptor, object component, object val)
        {
            try
            {
                descriptor.SetValue(component, val);
            }
            catch
            {
            }
        }

        public static int Round(double val) => 
            (val < 0.0) ? ((int) ((val - 0.5) + RoundingConstant)) : ((int) ((val + 0.5) - RoundingConstant));

        public static int Round(float val) => 
            (val < 0f) ? ((int) ((val - 0.5f) + RoundingConstant)) : ((int) ((val + 0.5f) - RoundingConstant));

        private double ScaleDouble(double val, float scale, bool scaleX) => 
            val * scale;

        private float ScaleFloat(float val, float scale, bool scaleX) => 
            val * scale;

        private int ScaleHorizontal(int width, float scaleFactor) => 
            ((width == 0) || ((width == 0x7fffffff) || (scaleFactor == 1f))) ? width : Round((float) (scaleFactor * width));

        private int ScaleInt(int val, float scale, bool scaleX) => 
            !scaleX ? this.ScaleVertical(val, scale) : this.ScaleHorizontal(val, scale);

        private Padding ScalePadding(Padding padding, SizeF scale) => 
            new Padding(this.ScaleHorizontal(padding.Left, scale.Width), this.ScaleVertical(padding.Top, scale.Height), this.ScaleHorizontal(padding.Right, scale.Width), this.ScaleHorizontal(padding.Bottom, scale.Height));

        private Point ScalePoint(Point val, SizeF value) => 
            new Point(this.ScaleHorizontal(val.X, value.Width), this.ScaleVertical(val.Y, value.Height));

        private Size ScaleSize(Size val, SizeF scale) => 
            new Size(this.ScaleHorizontal(val.Width, scale.Width), this.ScaleVertical(val.Height, scale.Height));

        private SizeF ScaleSizeF(SizeF val, SizeF scale) => 
            new SizeF(this.ScaleFloat(val.Width, scale.Width, true), this.ScaleFloat(val.Height, scale.Height, false));

        private object ScaleValue(XtraSerializableProperty attr, PropertyDescriptor prop, object val, OptionsLayoutBase options)
        {
            SizeF? layoutScaleFactor = options.LayoutScaleFactor;
            if ((layoutScaleFactor == null) || layoutScaleFactor.Value.IsEmpty)
            {
                return val;
            }
            if (this.CheckIfNoAutoScale(attr, prop, val))
            {
                return val;
            }
            bool scaleX = (attr.Flags & XtraSerializationFlags.AutoScaleY) == XtraSerializationFlags.None;
            float scale = scaleX ? layoutScaleFactor.Value.Width : layoutScaleFactor.Value.Height;
            return (((scale == 1f) || (scale == 0f)) ? val : (!(val is int) ? (!(val is float) ? (!(val is double) ? (!(val is Size) ? (!(val is SizeF) ? (!(val is Padding) ? (!(val is Point) ? val : this.ScalePoint((Point) val, layoutScaleFactor.Value)) : this.ScalePadding((Padding) val, layoutScaleFactor.Value)) : this.ScaleSizeF((SizeF) val, layoutScaleFactor.Value)) : this.ScaleSize((Size) val, layoutScaleFactor.Value)) : this.ScaleDouble((double) val, scale, scaleX)) : this.ScaleFloat((float) val, scale, scaleX)) : this.ScaleInt((int) val, scale, scaleX)));
        }

        private int ScaleVertical(int height, float scaleFactor) => 
            ((height == 0) || ((height == 0x7fffffff) || (scaleFactor == 1f))) ? height : Round((float) (scaleFactor * height));

        private void SetDefaultObjectConverterImpl()
        {
            this.ObjectConverterImpl = ObjectConverter.Instance;
        }

        private object TryGetCollectionItemFromCache(XtraSerializableProperty attr, string name, XtraPropertyInfo item)
        {
            if (base.ShouldNotTryCache(attr))
            {
                return null;
            }
            int cacheIndex = GetCacheIndex(this.FindProperty(item.ChildProperties, name));
            return ((cacheIndex != -1) ? this.GetCachedValue(name, cacheIndex) : null);
        }

        private bool TryGetValueFromCache(XtraSerializableProperty attr, IXtraPropertyCollection store, object obj, PropertyDescriptor prop)
        {
            if (base.ShouldNotTryCache(attr))
            {
                return false;
            }
            int cacheIndex = GetCacheIndex(this.FindProperty(store, prop.Name));
            if (cacheIndex == -1)
            {
                return false;
            }
            prop.SetValue(obj, this.GetCachedValue(prop.Name, cacheIndex));
            return true;
        }

        private object ValueToObject(XtraPropertyInfo prop, System.Type type)
        {
            prop.SetObjectConverterImpl(this.ObjectConverterImpl);
            return prop.ValueToObject(type);
        }

        public ObjectConverterImplementation ObjectConverterImpl
        {
            get => 
                this.objectConverterImpl;
            set => 
                this.objectConverterImpl = value;
        }

        protected System.Exception Exception =>
            this.exception;

        protected bool ResetProperties =>
            this.resetProperties;
    }
}

