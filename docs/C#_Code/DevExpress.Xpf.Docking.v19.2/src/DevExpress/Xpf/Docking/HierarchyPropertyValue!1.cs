namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;

    public class HierarchyPropertyValue<T>
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private DependencyProperty property;
        private readonly T DefaultValue;

        public HierarchyPropertyValue(DependencyProperty property, T defaultValue)
        {
            this.Property = property;
            this.DefaultValue = defaultValue;
        }

        internal T Get(BaseLayoutItem item) => 
            this.Get(item, item.GetValue(this.Property));

        internal T Get(BaseLayoutItem item, object value) => 
            (!Equals(value, this.DefaultValue) || (item.Parent == null)) ? ((T) value) : this.Get(item.Parent);

        public DependencyProperty Property
        {
            get => 
                this.property;
            set => 
                this.property = value;
        }
    }
}

