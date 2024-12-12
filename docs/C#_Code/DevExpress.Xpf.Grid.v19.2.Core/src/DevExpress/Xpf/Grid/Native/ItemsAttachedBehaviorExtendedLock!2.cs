namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class ItemsAttachedBehaviorExtendedLock<TContainer, TItem> : ItemsAttachedBehaviorCore<TContainer, TItem> where TContainer: DependencyObject where TItem: DependencyObject
    {
        private ItemsAttachedBehaviorExtendedLock(Func<TContainer, IList> getTargetFunction, Func<TContainer, TItem> createItemDelegate, DependencyProperty itemGeneratorTemplateProperty, DependencyProperty itemGeneratorTemplateSelectorProperty, DependencyProperty itemGeneratorStyleProperty, IEnumerable source, Action<TItem> setDefaultBindingAction, bool useDefaultTemplateSelector, bool useDefaultTemplateValidation, Func<TItem, bool> customClear, bool forceBindingsProcessing) : base(getTargetFunction, createItemDelegate, itemGeneratorTemplateProperty, itemGeneratorTemplateSelectorProperty, itemGeneratorStyleProperty, null, source, setDefaultBindingAction, useDefaultTemplateSelector, useDefaultTemplateValidation, customClear, forceBindingsProcessing)
        {
        }

        public static void OnItemsSourceExtLockPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e, DependencyProperty itemsAttachedBehaviorStoreProperty, DependencyProperty itemGeneratorTemplateProperty, DependencyProperty itemGeneratorTemplateSelectorProperty, DependencyProperty itemGeneratorStyleProperty, Func<TContainer, IList> getTargetFunction, Func<TContainer, TItem> createItemDelegate, Action<int, object> insertItemAction = null, Action<TItem> setDefaultBindingAction = null, ISupportInitialize supportInitialize = null, Action<TItem, object> linkItemWithSourceAction = null, bool useDefaultTemplateSelector = true, bool useDefaultTemplateValidation = true, Func<TItem, bool> customClear = null, bool forceBindingsProcessing = false)
        {
            BehaviorDestroy(itemsAttachedBehaviorStoreProperty, d);
            BehaviorInit(new ItemsAttachedBehaviorExtendedLock<TContainer, TItem>(getTargetFunction, createItemDelegate, itemGeneratorTemplateProperty, itemGeneratorTemplateSelectorProperty, itemGeneratorStyleProperty, e.NewValue as IEnumerable, setDefaultBindingAction, useDefaultTemplateSelector, useDefaultTemplateValidation, customClear, forceBindingsProcessing), itemsAttachedBehaviorStoreProperty, insertItemAction, supportInitialize, linkItemWithSourceAction, d);
        }

        public void SetLockSynchronization(bool lockSynchronization)
        {
            base.lockSynchronization = lockSynchronization;
        }
    }
}

