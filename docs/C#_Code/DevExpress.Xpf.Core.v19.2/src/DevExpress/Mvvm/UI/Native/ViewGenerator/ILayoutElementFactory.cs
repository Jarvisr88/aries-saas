namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using DevExpress.Entity.Model;
    using System;

    public interface ILayoutElementFactory
    {
        void CreateGroup(LayoutGroupInfo groupInfo);
        void CreateItem(IEdmPropertyInfo propertyInfo);
    }
}

