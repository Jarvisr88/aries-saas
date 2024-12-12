namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using System;

    public interface IEditingContext
    {
        IModelItem CreateItem(DXTypeIdentifier typeIdentifier);
        IModelItem CreateItem(Type type);
        IModelItem CreateItem(DXTypeIdentifier typeIdentifier, bool useDefaultInitializer);
        IModelItem CreateItem(Type type, bool useDefaultInitializer);
        IModelItem CreateStaticMemberItem(Type type, string memberName);

        IServiceProvider Services { get; }
    }
}

