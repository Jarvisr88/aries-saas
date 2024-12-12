namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public abstract class LocalCurrentDataView : CurrentDataView
    {
        private readonly bool lazyInitialization;

        protected LocalCurrentDataView(bool selectNullValue, object view, object handle, string valueMember, string displayMember, bool lazyInitialization) : base(selectNullValue, view, handle, valueMember, displayMember)
        {
            this.lazyInitialization = lazyInitialization;
        }

        protected override DataProxyViewCache CreateDataProxyViewCache(object source) => 
            this.lazyInitialization ? new LazyLocalDataProxyViewCache(base.DataAccessor, (IList) source) : base.CreateDataProxyViewCache(source);

        protected override object CreateVisibleListWrapper()
        {
            Type sourceGenericType = this.GetSourceGenericType();
            if (sourceGenericType == null)
            {
                sourceGenericType = typeof(object);
            }
            Type[] typeArguments = new Type[] { sourceGenericType };
            object[] args = new object[] { this };
            return (Activator.CreateInstance(typeof(LocalVisibleListWrapper<>).MakeGenericType(typeArguments), args) as LocalVisibleListWrapper);
        }

        public Type FindGenericType(Type sourceType)
        {
            Type type3;
            using (IEnumerator<Type> enumerator = this.GetTypeHierarchy(sourceType).GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Type[] interfaces = enumerator.Current.GetInterfaces();
                        Type collectionLikeGenericTypeFromInterfaces = this.GetCollectionLikeGenericTypeFromInterfaces(interfaces);
                        if (collectionLikeGenericTypeFromInterfaces == null)
                        {
                            continue;
                        }
                        type3 = collectionLikeGenericTypeFromInterfaces;
                    }
                    else
                    {
                        if (!sourceType.IsGenericType)
                        {
                            return null;
                        }
                        Type[] genericArguments = sourceType.GetGenericArguments();
                        return ((genericArguments.Length == 1) ? genericArguments[0] : null);
                    }
                    break;
                }
            }
            return type3;
        }

        private Type GetCollectionLikeGenericTypeFromInterfaces(IEnumerable<Type> interfaces)
        {
            Type type2;
            using (IEnumerator<Type> enumerator = interfaces.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Type current = enumerator.Current;
                        if (!current.IsGenericType)
                        {
                            continue;
                        }
                        Type[] genericArguments = current.GetGenericArguments();
                        if ((genericArguments.Length > 1) || !(typeof(IEnumerable<>).MakeGenericType(genericArguments) == current))
                        {
                            continue;
                        }
                        type2 = genericArguments[0];
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return type2;
        }

        private Type GetSourceGenericType()
        {
            Type sourceType = null;
            object listSource = this.ListSource.ListSource;
            BindingListAdapter adapter = listSource as BindingListAdapter;
            if ((adapter != null) && (adapter.OriginalDataSource != null))
            {
                sourceType = adapter.OriginalDataSource.GetType();
            }
            else if (listSource != null)
            {
                sourceType = listSource.GetType();
            }
            return ((sourceType != null) ? this.FindGenericType(sourceType) : null);
        }

        private IEnumerable<Type> GetTypeHierarchy(Type type)
        {
            IList<Type> source = new List<Type>();
            for (Type type2 = type; type2.BaseType != null; type2 = type2.BaseType)
            {
                source.Add(type2);
            }
            return source.Reverse<Type>();
        }

        protected override void InitializeView(object source)
        {
            this.Wrapper = new DefaultDataViewAsListWrapper((DefaultDataView) source);
            base.SetView(this.CreateDataProxyViewCache(this.Wrapper));
        }

        public override bool ProcessAddItem(int index)
        {
            DataProxy item = base.DataAccessor.CreateProxy(this.Wrapper[index], index);
            base.View.Add(index, item);
            base.ItemsCache.UpdateItemOnAdding(index);
            return true;
        }

        public override bool ProcessChangeItem(int index)
        {
            DataProxy item = base.DataAccessor.CreateProxy(this.ListSource.GetItemByIndex(index), index);
            base.View.Replace(index, item);
            base.ItemsCache.UpdateItem(index);
            return true;
        }

        public override bool ProcessDeleteItem(int index)
        {
            base.View.Remove(index);
            base.ItemsCache.UpdateItemOnDeleting(index);
            return true;
        }

        public override bool ProcessMoveItem(int oldIndex, int newIndex)
        {
            DataProxy item = base.View[oldIndex];
            base.View.Remove(oldIndex);
            base.View.Add(newIndex, item);
            base.ItemsCache.UpdateItemOnMoving(oldIndex, newIndex);
            return true;
        }

        public override bool ProcessReset()
        {
            base.ItemsCache.Reset();
            this.Initialize();
            return true;
        }

        private DefaultDataViewAsListWrapper Wrapper { get; set; }

        private DefaultDataView ListSource =>
            base.ListSource as DefaultDataView;
    }
}

