namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using DevExpress.Utils;
    using System;

    public class ViewItemBase : IViewItem
    {
        private readonly ModelItemBase modelItem;

        public ViewItemBase(ModelItemBase modelItem)
        {
            Guard.ArgumentNotNull(modelItem, "modelItem");
            this.modelItem = modelItem;
        }

        object IViewItem.PlatformObject =>
            this.modelItem.element;
    }
}

