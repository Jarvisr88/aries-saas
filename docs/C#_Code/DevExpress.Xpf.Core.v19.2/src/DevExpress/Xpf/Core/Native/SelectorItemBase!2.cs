namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public abstract class SelectorItemBase<TContainer, TItem> : ContentControl where TContainer: SelectorBase<TContainer, TItem> where TItem: SelectorItemBase<TContainer, TItem>
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty IsSelectedProperty;
        private static readonly DependencyPropertyKey OwnerPropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty OwnerProperty;
        private bool? definedIsEnabled;
        private List<DependencyObject> logicalChildren;
        private bool lockIsSelectedChanged;

        static SelectorItemBase();
        public SelectorItemBase();
        protected void AddLocalLogicalChild(DependencyObject child);
        internal void Assign(TContainer owner);
        protected virtual object CoerceIsEnabled(bool value);
        protected virtual object CoerceIsSelected(bool value);
        protected virtual object CoerceVisibility(Visibility value);
        protected override void OnAccessKey(AccessKeyEventArgs e);
        private static void OnAccessKeyPressed(object sender, AccessKeyPressedEventArgs e);
        protected override void OnContentChanged(object oldContent, object newContent);
        protected override void OnContentStringFormatChanged(string oldContentStringFormat, string newContentStringFormat);
        protected override void OnContentTemplateChanged(DataTemplate oldContentTemplate, DataTemplate newContentTemplate);
        protected override void OnContentTemplateSelectorChanged(DataTemplateSelector oldContentTemplateSelector, DataTemplateSelector newContentTemplateSelector);
        protected virtual void OnIsEnabledChanged(bool oldValue, bool newValue);
        protected virtual void OnIsSelectedChanged(bool oldValue, bool newValue);
        protected virtual void OnLoaded(object sender, RoutedEventArgs e);
        protected virtual void OnOwnerChanged(TContainer oldValue, TContainer newValue);
        protected virtual void OnVisibilityChanged(Visibility oldValue, Visibility newValue);
        protected void RemoveLocalLogicalChild(DependencyObject child);
        internal void SetIsSelectedInternal(bool value, bool lockIsSelectedChanged);
        protected void UpdateOwnerSelectionProperties();

        public bool IsSelected { get; set; }

        public TContainer Owner { get; private set; }

        internal bool DefinedIsEnabled { get; private set; }

        protected override IEnumerator LogicalChildren { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SelectorItemBase<TContainer, TItem>.<>c <>9;
            public static Action<TContainer> <>9__28_1;
            public static Action<TContainer> <>9__37_0;

            static <>c();
            internal void <.cctor>b__18_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal object <.cctor>b__18_1(DependencyObject d, object e);
            internal void <.cctor>b__18_2(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal object <.cctor>b__18_3(DependencyObject d, object e);
            internal void <.cctor>b__18_4(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal object <.cctor>b__18_5(DependencyObject d, object e);
            internal void <.cctor>b__18_6(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <OnIsSelectedChanged>b__28_1(TContainer x);
            internal void <UpdateOwnerSelectionProperties>b__37_0(TContainer x);
        }
    }
}

