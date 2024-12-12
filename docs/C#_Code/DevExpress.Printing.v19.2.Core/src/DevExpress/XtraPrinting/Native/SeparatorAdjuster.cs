namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections;

    public abstract class SeparatorAdjuster
    {
        protected SeparatorAdjuster();
        public void Adjust(IList sourceItems);
        protected virtual IList GetItems(object item);
        private IList GetVisibleItems(IList sourceItems);
        protected abstract bool IsSeparator(object item);
        protected abstract bool IsVisible(object item);
        private void RemoveDoubleSeparators(IList items);
        private void SetSeparatorVisisbility(object item, bool visible);
        protected abstract void SetVisibility(object item, bool visible);
    }
}

