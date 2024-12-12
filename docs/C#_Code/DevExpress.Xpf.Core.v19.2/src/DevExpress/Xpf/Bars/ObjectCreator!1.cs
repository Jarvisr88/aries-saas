namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections.Generic;

    public abstract class ObjectCreator<T> where T: class
    {
        private static readonly object olock;
        private Dictionary<Type, BarItemClassInfo<T>> objects;

        static ObjectCreator();
        protected ObjectCreator();
        public virtual T Create(Type baseType, object arg);
        public bool Exists(Type baseType);
        public Type GetItemType(Type baseType);
        protected internal void RegisterObject(Type baseType, BarItemClassInfo<T> classInfo);
        protected abstract void RegisterObjects();

        protected Dictionary<Type, BarItemClassInfo<T>> Storage { get; }
    }
}

