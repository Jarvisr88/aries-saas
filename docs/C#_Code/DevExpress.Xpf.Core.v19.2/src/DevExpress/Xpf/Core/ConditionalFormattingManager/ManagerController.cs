namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ManagerController
    {
        private IDialogContext context;
        private ManagerItemsCollection items;
        private IConditionModelItemsBuilder builder;

        public ManagerController(IDialogContext context)
        {
            this.context = context;
            this.builder = context.Builder;
            this.items = new ManagerItemsCollection();
            this.AssignItems();
        }

        public ManagerItemViewModel Add(BaseEditUnit editUnit)
        {
            ManagerItemViewModel item = ManagerItemViewModel.Factory(null);
            item.SetEditUnit(editUnit);
            this.items.Add(item);
            return item;
        }

        private void AddNewItems(IModelItemCollection modelItems, IndexedItem[] indexedItems)
        {
            IModelItem[] itemArray = modelItems.ToArray<IModelItem>();
            foreach (IndexedItem item in indexedItems)
            {
                if (item.IsNew)
                {
                    modelItems.Insert(item.Index, item.Unit.BuildCondition(this.builder));
                }
                else if (itemArray.Length <= item.Index)
                {
                    ISupportManager objA = item.Value;
                    if (!ReferenceEquals(objA, item.Unit.BuildCondition(this.builder).GetCurrentValue()))
                    {
                        modelItems.Remove(objA);
                        modelItems.Insert(item.Index, objA);
                    }
                }
            }
        }

        public void ApplyChanges()
        {
            IModelItem rootModelItem = this.context.GetRootModelItem();
            ((ILockable) this.Conditions.ComputedValue).BeginUpdate();
            try
            {
                using (IModelEditingScope scope = rootModelItem.BeginEdit("Update format conditions from manager"))
                {
                    this.UpdateModelItems(rootModelItem);
                    scope.Complete();
                }
            }
            finally
            {
                ILockable lockable;
                lockable.EndUpdate();
            }
            this.AssignItems();
        }

        private void AssignItems()
        {
            IModelItemCollection collection = this.Conditions.Collection;
            Func<IModelItem, ManagerItemViewModel> selector = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Func<IModelItem, ManagerItemViewModel> local1 = <>c.<>9__6_0;
                selector = <>c.<>9__6_0 = x => ManagerItemViewModel.Factory((ISupportManager) x.GetCurrentValue());
            }
            this.items.Assign(collection.Select<IModelItem, ManagerItemViewModel>(selector).ToList<ManagerItemViewModel>());
        }

        public void Edit(ManagerItemViewModel item, BaseEditUnit unit)
        {
            item.SetEditUnit(unit);
        }

        private void EditModelItems(IModelItemCollection modelItems, IndexedItem[] indexedItems)
        {
            Func<IndexedItem, bool> predicate = <>c.<>9__17_0;
            if (<>c.<>9__17_0 == null)
            {
                Func<IndexedItem, bool> local1 = <>c.<>9__17_0;
                predicate = <>c.<>9__17_0 = x => !x.IsNew;
            }
            foreach (IndexedItem item in indexedItems.Where<IndexedItem>(predicate).ToArray<IndexedItem>())
            {
                item.Unit.BuildCondition(this.builder, modelItems[item.Index]);
            }
        }

        public IList<ManagerItemViewModel> GetDisplayItems(string fieldName)
        {
            Func<ManagerItemViewModel, bool> searchPredicate = null;
            if (!string.IsNullOrEmpty(fieldName))
            {
                searchPredicate = x => x.EditUnit.FieldName == fieldName;
            }
            else
            {
                Func<ManagerItemViewModel, bool> func1 = <>c.<>9__11_0;
                if (<>c.<>9__11_0 == null)
                {
                    Func<ManagerItemViewModel, bool> local1 = <>c.<>9__11_0;
                    func1 = <>c.<>9__11_0 = _ => true;
                }
                searchPredicate = func1;
            }
            return (from x in this.items
                where searchPredicate(x) && x.AllowUserCustomization
                select x).ToList<ManagerItemViewModel>();
        }

        public void Remove(ManagerItemViewModel item)
        {
            this.items.Remove(item);
        }

        private void RemoveItems(IModelItemCollection modelItems, IndexedItem[] indexedItems)
        {
            Func<IndexedItem, bool> predicate = <>c.<>9__14_0;
            if (<>c.<>9__14_0 == null)
            {
                Func<IndexedItem, bool> local1 = <>c.<>9__14_0;
                predicate = <>c.<>9__14_0 = x => !x.IsNew;
            }
            Func<IndexedItem, ISupportManager> selector = <>c.<>9__14_1;
            if (<>c.<>9__14_1 == null)
            {
                Func<IndexedItem, ISupportManager> local2 = <>c.<>9__14_1;
                selector = <>c.<>9__14_1 = y => y.Value;
            }
            ISupportManager[] source = indexedItems.Where<IndexedItem>(predicate).Select<IndexedItem, ISupportManager>(selector).ToArray<ISupportManager>();
            List<IModelItem> list = new List<IModelItem>();
            foreach (IModelItem item in modelItems)
            {
                ISupportManager currentValue = item.GetCurrentValue() as ISupportManager;
                if ((currentValue == null) || !source.Contains<ISupportManager>(currentValue))
                {
                    list.Add(item);
                }
            }
            foreach (IModelItem item2 in list)
            {
                modelItems.Remove(item2);
            }
        }

        private void ReplaceModelItems(IModelItemCollection modelItems, IndexedItem[] indexedItems)
        {
            Func<IndexedItem, bool> predicate = <>c.<>9__16_0;
            if (<>c.<>9__16_0 == null)
            {
                Func<IndexedItem, bool> local1 = <>c.<>9__16_0;
                predicate = <>c.<>9__16_0 = x => !x.IsNew;
            }
            foreach (IndexedItem item in indexedItems.Where<IndexedItem>(predicate).ToArray<IndexedItem>())
            {
                IModelItem item2 = modelItems[item.Index];
                ISupportManager objA = item.Value;
                if (!ReferenceEquals(objA, item2.GetCurrentValue()))
                {
                    modelItems.Remove(objA);
                    modelItems.Insert(item.Index, objA);
                }
            }
        }

        public void Swap(ManagerItemViewModel first, ManagerItemViewModel second)
        {
            this.items.Move(this.items.IndexOf(first), this.items.IndexOf(second));
        }

        private void UpdateModelItems(IModelItem dataControl)
        {
            IModelItemCollection collection = this.Conditions.Collection;
            Func<ManagerItemViewModel, int, IndexedItem> selector = <>c.<>9__13_0;
            if (<>c.<>9__13_0 == null)
            {
                Func<ManagerItemViewModel, int, IndexedItem> local1 = <>c.<>9__13_0;
                selector = <>c.<>9__13_0 = (x, i) => new IndexedItem(x, i);
            }
            IndexedItem[] indexedItems = this.items.Select<ManagerItemViewModel, IndexedItem>(selector).ToArray<IndexedItem>();
            this.RemoveItems(collection, indexedItems);
            this.AddNewItems(collection, indexedItems);
            this.ReplaceModelItems(collection, indexedItems);
            this.EditModelItems(collection, indexedItems);
        }

        private IModelProperty Conditions =>
            this.context.Conditions;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ManagerController.<>c <>9 = new ManagerController.<>c();
            public static Func<IModelItem, ManagerItemViewModel> <>9__6_0;
            public static Func<ManagerItemViewModel, bool> <>9__11_0;
            public static Func<ManagerItemViewModel, int, ManagerController.IndexedItem> <>9__13_0;
            public static Func<ManagerController.IndexedItem, bool> <>9__14_0;
            public static Func<ManagerController.IndexedItem, ISupportManager> <>9__14_1;
            public static Func<ManagerController.IndexedItem, bool> <>9__16_0;
            public static Func<ManagerController.IndexedItem, bool> <>9__17_0;

            internal ManagerItemViewModel <AssignItems>b__6_0(IModelItem x) => 
                ManagerItemViewModel.Factory((ISupportManager) x.GetCurrentValue());

            internal bool <EditModelItems>b__17_0(ManagerController.IndexedItem x) => 
                !x.IsNew;

            internal bool <GetDisplayItems>b__11_0(ManagerItemViewModel _) => 
                true;

            internal bool <RemoveItems>b__14_0(ManagerController.IndexedItem x) => 
                !x.IsNew;

            internal ISupportManager <RemoveItems>b__14_1(ManagerController.IndexedItem y) => 
                y.Value;

            internal bool <ReplaceModelItems>b__16_0(ManagerController.IndexedItem x) => 
                !x.IsNew;

            internal ManagerController.IndexedItem <UpdateModelItems>b__13_0(ManagerItemViewModel x, int i) => 
                new ManagerController.IndexedItem(x, i);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct IndexedItem
        {
            private readonly int index;
            private readonly BaseEditUnit unit;
            private readonly ISupportManager value;
            public int Index =>
                this.index;
            public BaseEditUnit Unit =>
                this.unit;
            public ISupportManager Value =>
                this.value;
            public IndexedItem(ManagerItemViewModel vm, int index)
            {
                this.index = index;
                this.unit = vm.EditUnit;
                this.value = vm.Value;
            }

            public bool IsNew =>
                ReferenceEquals(this.Value, null);
        }
    }
}

