namespace DevExpress.Xpf.Bars
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class RuntimeCollectionCustomization : RuntimeCustomization
    {
        private DependencyObject forcedContainer;

        public RuntimeCollectionCustomization();
        public RuntimeCollectionCustomization(DependencyObject forcedTarget, DependencyObject forcedContainer);
        protected virtual DependencyObject FindContainer();
        protected override bool TryOverwriteOverride(RuntimeCustomization second);

        [XtraSerializableProperty]
        public string ContainerName { get; set; }
    }
}

