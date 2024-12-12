namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;

    [TemplatePart(Name="PART_Header", Type=typeof(UIElement))]
    public class MDIDocument : BaseDocument
    {
        public static readonly DependencyProperty IsMaximizedProperty;

        static MDIDocument()
        {
            DependencyPropertyRegistrator<MDIDocument> registrator = new DependencyPropertyRegistrator<MDIDocument>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<bool>("IsMaximized", ref IsMaximizedProperty, false, (dObj, e) => ((MDIDocument) dObj).OnIsMaximizedChanged((bool) e.OldValue, (bool) e.NewValue), null);
            EventManager.RegisterClassHandler(typeof(MDIDocument), AccessKeyManager.AccessKeyPressedEvent, new AccessKeyPressedEventHandler(MDIDocument.OnAccessKeyPressed));
        }

        public MDIDocument()
        {
            KeyboardNavigation.SetTabNavigation(this, KeyboardNavigationMode.Cycle);
            KeyboardNavigation.SetDirectionalNavigation(this, KeyboardNavigationMode.Cycle);
        }

        protected override bool GetIsChildMenuVisible() => 
            this.GetIsChildMenuVisibleForFloatingElement(!this.IsMaximized);

        private static void OnAccessKeyPressed(object sender, AccessKeyPressedEventArgs e)
        {
            MDIDocument document = sender as MDIDocument;
            if (document != null)
            {
                BaseLayoutItem layoutItem = DockLayoutManager.GetLayoutItem(document);
                if ((layoutItem != null) && (!layoutItem.IsActive && LayoutHelper.IsChildElement(document, e.Target)))
                {
                    e.Target = null;
                }
            }
        }

        protected override void OnActualSizeChanged(Size value)
        {
            base.OnActualSizeChanged(value);
            if (!base.IsDisposing)
            {
                DocumentPanel layoutItem = DockLayoutManager.GetLayoutItem(this) as DocumentPanel;
                if (layoutItem != null)
                {
                    layoutItem.MDIDocumentSize = value;
                    DocumentGroup parent = layoutItem.Parent as DocumentGroup;
                    if ((parent != null) && (this.PartHeader != null))
                    {
                        parent.MDIDocumentHeaderHeight = Math.Max(this.PartHeader.RenderSize.Height, parent.MDIDocumentHeaderHeight);
                    }
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.SetConstraintBindings();
            this.PartHeader = base.GetTemplateChild("PART_Header") as UIElement;
            this.UpdateVisualState();
        }

        protected override void OnDispose()
        {
            base.ClearValue(DocumentPanel.MDILocationProperty);
            base.ClearValue(DocumentPanel.MDISizeProperty);
            base.ClearValue(MDIStateHelper.MDIStateProperty);
            base.ClearValue(Panel.ZIndexProperty);
            base.OnDispose();
        }

        protected override void OnIsActiveChanged(bool oldValue, bool newValue)
        {
            base.OnIsActiveChanged(oldValue, newValue);
            this.UpdateMDITitles();
        }

        protected virtual void OnIsMaximizedChanged(bool oldValue, bool newValue)
        {
            this.CheckIsChildMenuVisible();
            this.UpdateMDITitles();
        }

        private void OnItemVisualChanged(object sender, EventArgs e)
        {
            this.UpdateVisualState();
        }

        protected override void ProcessMergeActions()
        {
            if (base.LayoutItem != null)
            {
                bool isMergingTarget;
                if (!base.CanMerge)
                {
                    isMergingTarget = false;
                }
                else
                {
                    switch (MDIControllerHelper.GetActualMDIMergeStyle(base.LayoutItem.GetDockLayoutManager(), base.LayoutItem))
                    {
                        case MDIMergeStyle.Always:
                            isMergingTarget = true;
                            break;

                        case MDIMergeStyle.Never:
                            isMergingTarget = false;
                            break;

                        case MDIMergeStyle.WhenLoadedOrChildActivated:
                            isMergingTarget = base.IsMergingTarget;
                            break;

                        default:
                            isMergingTarget = base.IsActive;
                            break;
                    }
                }
                if (isMergingTarget)
                {
                    base.BeginMerge();
                }
                else
                {
                    base.UnMerge();
                }
            }
        }

        private void SetBindings(DocumentPanel document)
        {
            if (document != null)
            {
                BindingHelper.SetBinding(this, DocumentPanel.MDILocationProperty, document, DocumentPanel.MDILocationProperty, BindingMode.TwoWay);
                BindingHelper.SetBinding(this, DocumentPanel.MDISizeProperty, document, DocumentPanel.MDISizeProperty, BindingMode.TwoWay);
                BindingHelper.SetBinding(this, MDIStateHelper.MDIStateProperty, document, MDIStateHelper.MDIStateProperty, BindingMode.TwoWay);
                BindingHelper.SetBinding(this, DocumentPanel.MDIMergeStyleProperty, document, DocumentPanel.MDIMergeStyleProperty, BindingMode.OneWay);
                BindingHelper.SetBinding(this, IsMaximizedProperty, document, LayoutPanel.IsMaximizedProperty, BindingMode.OneWay);
                BindingHelper.SetBinding(this, Panel.ZIndexProperty, document, Panel.ZIndexProperty, BindingMode.TwoWay);
            }
        }

        private void SetConstraintBindings()
        {
            Binding binding = new Binding("ActualMinSize");
            binding.Converter = new MinSizeConverter(true);
            base.SetBinding(FrameworkElement.MinWidthProperty, binding);
            Binding binding2 = new Binding("ActualMinSize");
            binding2.Converter = new MinSizeConverter(false);
            base.SetBinding(FrameworkElement.MinHeightProperty, binding2);
            Binding binding3 = new Binding("ActualMaxSize");
            binding3.Converter = new MaxSizeConverter(true);
            base.SetBinding(FrameworkElement.MaxWidthProperty, binding3);
            Binding binding4 = new Binding("ActualMaxSize");
            binding4.Converter = new MaxSizeConverter(false);
            base.SetBinding(FrameworkElement.MaxHeightProperty, binding4);
        }

        protected override void Subscribe(BaseLayoutItem item)
        {
            base.Subscribe(item);
            if (item != null)
            {
                item.VisualChanged += new EventHandler(this.OnItemVisualChanged);
                this.SetBindings(item as DocumentPanel);
            }
        }

        protected override void Unsubscribe(BaseLayoutItem item)
        {
            base.Unsubscribe(item);
            if (item != null)
            {
                item.VisualChanged -= new EventHandler(this.OnItemVisualChanged);
            }
        }

        private void UpdateMDITitles()
        {
            if (base.IsActive && this.IsMaximized)
            {
                MDIControllerHelper.MergeMDITitles(this);
            }
        }

        protected virtual void UpdateVisualState()
        {
            if (base.DocumentPanel != null)
            {
                VisualStateManager.GoToState(this, base.DocumentPanel.IsActive ? "Active" : "Inactive", false);
                if (base.DocumentPanel.IsMaximized)
                {
                    VisualStateManager.GoToState(this, "Maximized", false);
                }
                else if (base.DocumentPanel.IsMinimized)
                {
                    VisualStateManager.GoToState(this, "Minimized", false);
                }
                else
                {
                    VisualStateManager.GoToState(this, "EmptyMDIState", false);
                }
            }
        }

        public bool IsMaximized
        {
            get => 
                (bool) base.GetValue(IsMaximizedProperty);
            set => 
                base.SetValue(IsMaximizedProperty, value);
        }

        protected UIElement PartHeader { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MDIDocument.<>c <>9 = new MDIDocument.<>c();

            internal void <.cctor>b__1_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((MDIDocument) dObj).OnIsMaximizedChanged((bool) e.OldValue, (bool) e.NewValue);
            }
        }
    }
}

