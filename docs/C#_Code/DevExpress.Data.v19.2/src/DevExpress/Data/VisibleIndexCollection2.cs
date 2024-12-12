namespace DevExpress.Data
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class VisibleIndexCollection2 : CollectionBase
    {
        private DataController controller;

        public VisibleIndexCollection2(DataController controller);
        public void Add(int controllerRowHandle);
        public bool Contains(int controllerRowHandle);
        public int GetHandle(int visibleIndex);
        public int IndexOf(int controllerRowHandle);

        protected DataController Controller { get; }

        public int this[int visibleIndex] { get; }

        public bool IsEmpty { get; }
    }
}

