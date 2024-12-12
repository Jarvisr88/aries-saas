namespace DevExpress.Xpf.Editors.EditStrategy
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class ComboBoxEditStrategy : LookUpEditStrategyBase
    {
        public ComboBoxEditStrategy(ComboBoxEdit editor) : base(editor)
        {
        }

        protected override IEnumerable GetInnerEditorCustomItemsSource()
        {
            List<CustomItem> list = new List<CustomItem>();
            if (this.StyleSettings.ShowCustomItem(this.Editor))
            {
                list.AddRange(base.GetInnerEditorCustomItemsSource().Cast<CustomItem>());
            }
            return list;
        }

        protected override object GetVisibleListSource() => 
            !base.IsAsyncServerMode ? (!base.IsSyncServerMode ? base.GetVisibleListSource() : new SyncServerModeCollectionView((SyncVisibleListWrapper) base.GetVisibleListSource())) : new AsyncServerModeCollectionView((AsyncVisibleListWrapper) base.GetVisibleListSource());

        private ComboBoxEdit Editor =>
            base.Editor as ComboBoxEdit;

        internal BaseComboBoxStyleSettings StyleSettings =>
            (BaseComboBoxStyleSettings) base.StyleSettings;

        protected internal override bool AllowSpin =>
            base.AllowSpin && ((base.ItemsProvider.GetCount(this.CurrentDataViewHandle) > 0) && (base.IsSingleSelection || base.IsInTokenMode));
    }
}

