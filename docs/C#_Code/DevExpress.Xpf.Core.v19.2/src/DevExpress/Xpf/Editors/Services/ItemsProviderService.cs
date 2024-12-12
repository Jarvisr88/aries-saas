namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using System;

    public class ItemsProviderService : BaseEditBaseService
    {
        public ItemsProviderService(BaseEdit editor) : base(editor)
        {
        }

        public object GetEditValueFromSelectedIndex(int index) => 
            this.ItemsProvider.GetValueByIndex(index, this.ItemsProvider.CurrentDataViewHandle);

        public IItemsProvider2 ItemsProvider =>
            ((IItemsProviderOwner) base.OwnerEdit.Settings).ItemsProvider;
    }
}

