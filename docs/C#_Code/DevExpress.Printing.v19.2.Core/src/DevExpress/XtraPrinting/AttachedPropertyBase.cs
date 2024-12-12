namespace DevExpress.XtraPrinting
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public abstract class AttachedPropertyBase
    {
        private static short globalIndex;
        private static object synch = new object();
        private static List<AttachedPropertyBase> indexedProps = new List<AttachedPropertyBase>();

        protected AttachedPropertyBase(string name)
        {
            this.Name = name;
            object synch = AttachedPropertyBase.synch;
            lock (synch)
            {
                this.Index = GetGlobalIndex();
            }
        }

        private static short GetGlobalIndex()
        {
            if (globalIndex >= 0x7fff)
            {
                throw new InvalidOperationException();
            }
            globalIndex = (short) (globalIndex + 1);
            return globalIndex;
        }

        public static AttachedPropertyBase GetProperty(int propertyIndex)
        {
            object synch = AttachedPropertyBase.synch;
            lock (synch)
            {
                return indexedProps[propertyIndex];
            }
        }

        public static AttachedProperty<T> Register<T>(string name)
        {
            object synch = AttachedPropertyBase.synch;
            lock (synch)
            {
                AttachedProperty<T> item = new AttachedProperty<T>(name);
                indexedProps.Add(item);
                if ((indexedProps.Count - 1) != item.Index)
                {
                    throw new InvalidOperationException();
                }
                return item;
            }
        }

        public string Name { get; private set; }

        public short Index { get; private set; }

        public abstract Type PropertyType { get; }
    }
}

