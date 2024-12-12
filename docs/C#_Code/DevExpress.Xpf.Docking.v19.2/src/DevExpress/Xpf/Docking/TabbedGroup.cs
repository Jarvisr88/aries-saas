namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class TabbedGroup : LayoutGroup
    {
        public static readonly DependencyProperty ShowTabForSinglePageProperty;
        public static readonly DependencyProperty IsMaximizedProperty;
        protected internal static readonly DependencyPropertyKey IsMaximizedPropertyKey;
        private static readonly DependencyPropertyKey FloatStatePropertyKey;
        public static readonly DependencyProperty FloatStateProperty;

        static TabbedGroup()
        {
            DependencyPropertyRegistrator<TabbedGroup> registrator = new DependencyPropertyRegistrator<TabbedGroup>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.OverrideMetadata<bool>(BaseLayoutItem.AllowFloatProperty, true, null, null);
            registrator.OverrideMetadata<bool>(BaseLayoutItem.AllowSelectionProperty, false, null, null);
            registrator.OverrideMetadata<DataTemplateSelector>(LayoutGroup.GroupTemplateSelectorProperty, new DefaultTemplateSelector(), (dObj, e) => ((TabbedGroup) dObj).OnGroupTemplateChanged(), null);
            registrator.Register<bool>("ShowTabForSinglePage", ref ShowTabForSinglePageProperty, false, null, null);
            registrator.RegisterReadonly<bool>("IsMaximized", ref IsMaximizedPropertyKey, ref IsMaximizedProperty, false, (dObj, e) => ((TabbedGroup) dObj).OnIsMaximizedChanged((bool) e.NewValue), null);
            registrator.RegisterReadonly<DevExpress.Xpf.Docking.FloatState>("FloatState", ref FloatStatePropertyKey, ref FloatStateProperty, DevExpress.Xpf.Docking.FloatState.Normal, (dObj, e) => ((TabbedGroup) dObj).OnFloatStateChanged((DevExpress.Xpf.Docking.FloatState) e.OldValue, (DevExpress.Xpf.Docking.FloatState) e.NewValue), null);
        }

        public TabbedGroup() : this(new BaseLayoutItem[0])
        {
        }

        internal TabbedGroup(params BaseLayoutItem[] items) : base(items)
        {
        }

        protected override Size CalcMaxSizeValue(Size value)
        {
            Size[] sizeArray;
            Size[] sizeArray2;
            base.Items.CollectConstraints(out sizeArray, out sizeArray2);
            return MathHelper.MeasureSize(MathHelper.MeasureMinSize(sizeArray), MathHelper.MeasureMaxSize(sizeArray2), value);
        }

        protected override Size CalcMinSizeValue(Size value)
        {
            Size[] sizeArray;
            Size[] sizeArray2;
            base.Items.CollectConstraints(out sizeArray, out sizeArray2);
            Size size = MathHelper.MeasureMinSize(sizeArray);
            return new Size(Math.Max(size.Width, value.Width), Math.Max(size.Height, value.Height));
        }

        protected override bool CanCreateItemsInternal() => 
            false;

        protected override LayoutGroup GetContainerHost(BaseLayoutItem container)
        {
            LayoutGroup ownerGroup = GetOwnerGroup(this);
            return ((!base.IsAutoHidden || (ownerGroup == null)) ? base.GetContainerHost(container) : ownerGroup);
        }

        protected override bool GetIsAutoHidden() => 
            base.GetIsAutoHidden() || ((base.Parent == null) && (GetOwnerGroup(this) is AutoHideGroup));

        protected override LayoutItemType GetLayoutItemTypeCore() => 
            LayoutItemType.TabPanelGroup;

        protected override bool HasTabHeader() => 
            true;

        protected virtual void OnFloatStateChanged(DevExpress.Xpf.Docking.FloatState oldState, DevExpress.Xpf.Docking.FloatState newState)
        {
            base.SetValue(IsMaximizedPropertyKey, newState == DevExpress.Xpf.Docking.FloatState.Maximized);
            if (base.IsFloatingRootItem)
            {
                base.Items.Accept<BaseLayoutItem>(x => x.SetFloatState(this.FloatState));
            }
        }

        protected override void OnIsFloatingRootItemChanged(bool newValue)
        {
            base.OnIsFloatingRootItemChanged(newValue);
            base.Items.Accept<BaseLayoutItem>(delegate (BaseLayoutItem x) {
                x.SetFloatState(this.FloatState);
                x.UpdateButtons();
            });
            (base.SelectedItem as LayoutPanel).Do<LayoutPanel>(x => x.IsFloatingRootInTabbedGroup = newValue);
        }

        protected virtual void OnIsMaximizedChanged(bool maximized)
        {
        }

        protected override void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsCollectionChanged(sender, e);
            if (base.IsFloatingRootItem)
            {
                base.Items.Accept<BaseLayoutItem>(x => x.SetFloatState(this.FloatState));
            }
        }

        protected override void OnSelectedItemChanged(BaseLayoutItem item, BaseLayoutItem oldItem)
        {
            base.OnSelectedItemChanged(item, oldItem);
            if (base.IsFloatingRootItem)
            {
                Action<LayoutGroup> action1 = <>c.<>9__39_0;
                if (<>c.<>9__39_0 == null)
                {
                    Action<LayoutGroup> local1 = <>c.<>9__39_0;
                    action1 = <>c.<>9__39_0 = delegate (LayoutGroup x) {
                        x.UpdateWindowTaskbarIcon();
                        x.UpdateWindowTaskbarTitle();
                    };
                }
                this.GetRoot().Do<LayoutGroup>(action1);
            }
            Action<LayoutPanel> action = <>c.<>9__39_1;
            if (<>c.<>9__39_1 == null)
            {
                Action<LayoutPanel> local2 = <>c.<>9__39_1;
                action = <>c.<>9__39_1 = x => x.IsFloatingRootInTabbedGroup = false;
            }
            (oldItem as LayoutPanel).Do<LayoutPanel>(action);
            (base.SelectedItem as LayoutPanel).Do<LayoutPanel>(x => x.IsFloatingRootInTabbedGroup = base.IsFloatingRootItem);
        }

        protected override void OnTabHeaderHasScrollChanged(bool hasScroll)
        {
            base.OnLayoutChanged();
        }

        protected override void OnTabHeaderLayoutTypeChanged(TabHeaderLayoutType type)
        {
            base.CoerceValue(LayoutGroup.TabHeadersAutoFillProperty);
            base.OnLayoutChanged();
        }

        protected override void OnTabHeadersAutoFillChanged(bool autoFill)
        {
            base.OnLayoutChanged();
        }

        internal override void PrepareForModification(bool isDeserializing)
        {
            base.PrepareForModification(isDeserializing);
            if (isDeserializing)
            {
                SetOwnerGroup(this, null);
            }
        }

        internal override void SetFloatState(DevExpress.Xpf.Docking.FloatState state)
        {
            if (this.FloatState != state)
            {
                base.SetValue(FloatStatePropertyKey, state);
            }
        }

        public DevExpress.Xpf.Docking.FloatState FloatState
        {
            get => 
                (DevExpress.Xpf.Docking.FloatState) base.GetValue(FloatStateProperty);
            private set => 
                base.SetValue(FloatStatePropertyKey, value);
        }

        [Category("Layout")]
        public bool IsMaximized
        {
            get => 
                (bool) base.GetValue(IsMaximizedProperty);
            internal set => 
                base.SetValue(IsMaximizedPropertyKey, value);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool SerializableIsMaximized
        {
            get => 
                this.IsMaximized;
            set
            {
            }
        }

        [XtraSerializableProperty]
        public bool ShowTabForSinglePage
        {
            get => 
                (bool) base.GetValue(ShowTabForSinglePageProperty);
            set => 
                base.SetValue(ShowTabForSinglePageProperty, value);
        }

        internal override bool SupportsFloatOrMDIState =>
            LayoutItemsHelper.IsFloatingRootItem(this);

        protected internal override bool IgnoreOrientation =>
            true;

        protected internal override bool IsTabHost =>
            true;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabbedGroup.<>c <>9 = new TabbedGroup.<>c();
            public static Action<LayoutGroup> <>9__39_0;
            public static Action<LayoutPanel> <>9__39_1;

            internal void <.cctor>b__5_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((TabbedGroup) dObj).OnGroupTemplateChanged();
            }

            internal void <.cctor>b__5_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((TabbedGroup) dObj).OnIsMaximizedChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__5_2(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((TabbedGroup) dObj).OnFloatStateChanged((FloatState) e.OldValue, (FloatState) e.NewValue);
            }

            internal void <OnSelectedItemChanged>b__39_0(LayoutGroup x)
            {
                x.UpdateWindowTaskbarIcon();
                x.UpdateWindowTaskbarTitle();
            }

            internal void <OnSelectedItemChanged>b__39_1(LayoutPanel x)
            {
                x.IsFloatingRootInTabbedGroup = false;
            }
        }

        private class DefaultTemplateSelector : DefaultItemTemplateSelectorWrapper.DefaultItemTemplateSelector
        {
            public override DataTemplate SelectTemplate(object item, DependencyObject container)
            {
                TabbedPaneContentPresenter presenter = container as TabbedPaneContentPresenter;
                return ((!(item is TabbedGroup) || ((presenter == null) || (presenter.Owner == null))) ? null : presenter.Owner.TabbedTemplate);
            }
        }
    }
}

