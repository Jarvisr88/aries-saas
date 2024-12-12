namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using DevExpress.Entity.Model;
    using System;
    using System.Runtime.CompilerServices;

    public class LayoutItemInfo : ILayoutElementGenerator
    {
        private readonly IEdmPropertyInfo property;

        public LayoutItemInfo(IEdmPropertyInfo property)
        {
            this.property = property;
            this.PropertyName = property.Name;
        }

        void ILayoutElementGenerator.CreateElement(ILayoutElementFactory factory)
        {
            factory.CreateItem(this.property);
        }

        public string PropertyName { get; private set; }
    }
}

