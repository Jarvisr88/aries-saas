namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class LayoutGroupGenerator
    {
        private readonly Dictionary<Type, CreateInstance> initializers = new Dictionary<Type, CreateInstance>();
        private readonly DockLayoutManager owner;

        public LayoutGroupGenerator(DockLayoutManager owner)
        {
            this.owner = owner;
            this.initializers[typeof(LayoutGroup)] = () => owner.CreateLayoutGroup();
            this.initializers[typeof(TabbedGroup)] = (CreateInstance) (() => owner.CreateTabbedGroup());
            this.initializers[typeof(DocumentGroup)] = (CreateInstance) (() => owner.CreateDocumentGroup());
            this.initializers[typeof(AutoHideGroup)] = (CreateInstance) (() => owner.CreateAutoHideGroup());
            this.initializers[typeof(FloatGroup)] = (CreateInstance) (() => owner.CreateFloatGroup());
        }

        public TGroup GetGroup<TGroup>() where TGroup: LayoutGroup
        {
            LayoutGroup group = null;
            CreateInstance instance;
            if (this.initializers.TryGetValue(typeof(TGroup), out instance))
            {
                group = instance();
            }
            TGroup local = group as TGroup;
            if (local == null)
            {
                throw new NotSupportedException("type");
            }
            return local;
        }

        private delegate LayoutGroup CreateInstance();
    }
}

