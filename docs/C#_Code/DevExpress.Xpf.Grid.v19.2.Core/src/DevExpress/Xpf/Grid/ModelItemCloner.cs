namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using System;

    internal class ModelItemCloner
    {
        public IModelItem Clone(IModelItem original)
        {
            IModelItem item = original.Context.CreateItem(original.ItemType);
            foreach (IModelProperty property in original.Properties)
            {
                if (property.IsSet && !property.IsReadOnly)
                {
                    item.Properties[property.Name].SetValue(property.Value);
                }
            }
            return item;
        }
    }
}

