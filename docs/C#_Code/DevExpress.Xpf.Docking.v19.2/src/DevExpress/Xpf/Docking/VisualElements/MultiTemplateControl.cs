namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class MultiTemplateControl : psvControl
    {
        public static readonly DependencyProperty EmptyTemplateProperty;
        public static readonly DependencyProperty PanelTemplateProperty;
        public static readonly DependencyProperty ControlItemTemplateProperty;
        public static readonly DependencyProperty DocumentTemplateProperty;
        public static readonly DependencyProperty GroupTemplateProperty;
        public static readonly DependencyProperty TabPanelTemplateProperty;
        public static readonly DependencyProperty DocumentPanelTemplateProperty;
        public static readonly DependencyProperty MDIDocumentTemplateProperty;
        public static readonly DependencyProperty FloatDocumentTemplateProperty;
        public static readonly DependencyProperty FloatingWindowTemplateProperty;
        public static readonly DependencyProperty FloatingAdornerTemplateProperty;
        public static readonly DependencyProperty SplitterControlTemplateProperty;
        public static readonly DependencyProperty EmptySpaceControlTemplateProperty;
        public static readonly DependencyProperty LabelControlTemplateProperty;
        public static readonly DependencyProperty SeparatorControlTemplateProperty;
        public static readonly DependencyProperty AutoHideGroupTemplateProperty;
        public static readonly DependencyProperty LayoutItemProperty;

        static MultiTemplateControl()
        {
            DependencyPropertyRegistrator<MultiTemplateControl> registrator = new DependencyPropertyRegistrator<MultiTemplateControl>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<BaseLayoutItem>("LayoutItem", ref LayoutItemProperty, null, (dObj, e) => ((MultiTemplateControl) dObj).OnLayoutItemChanged((BaseLayoutItem) e.NewValue), null);
            registrator.Register<ControlTemplate>("EmptyTemplate", ref EmptyTemplateProperty, null, null, null);
            registrator.Register<ControlTemplate>("ControlItemTemplate", ref ControlItemTemplateProperty, null, null, null);
            registrator.Register<ControlTemplate>("PanelTemplate", ref PanelTemplateProperty, null, null, null);
            registrator.Register<ControlTemplate>("DocumentTemplate", ref DocumentTemplateProperty, null, null, null);
            registrator.Register<ControlTemplate>("MDIDocumentTemplate", ref MDIDocumentTemplateProperty, null, null, null);
            registrator.Register<ControlTemplate>("FloatDocumentTemplate", ref FloatDocumentTemplateProperty, null, null, null);
            registrator.Register<ControlTemplate>("GroupTemplate", ref GroupTemplateProperty, null, null, null);
            registrator.Register<ControlTemplate>("TabPanelTemplate", ref TabPanelTemplateProperty, null, null, null);
            registrator.Register<ControlTemplate>("DocumentPanelTemplate", ref DocumentPanelTemplateProperty, null, null, null);
            registrator.Register<ControlTemplate>("FloatingWindowTemplate", ref FloatingWindowTemplateProperty, null, null, null);
            registrator.Register<ControlTemplate>("FloatingAdornerTemplate", ref FloatingAdornerTemplateProperty, null, null, null);
            registrator.Register<ControlTemplate>("SplitterControlTemplate", ref SplitterControlTemplateProperty, null, null, null);
            registrator.Register<ControlTemplate>("EmptySpaceControlTemplate", ref EmptySpaceControlTemplateProperty, null, null, null);
            registrator.Register<ControlTemplate>("LabelControlTemplate", ref LabelControlTemplateProperty, null, null, null);
            registrator.Register<ControlTemplate>("SeparatorControlTemplate", ref SeparatorControlTemplateProperty, null, null, null);
            registrator.Register<ControlTemplate>("AutoHideGroupTemplate", ref AutoHideGroupTemplateProperty, null, null, null);
        }

        public void ClearTemplateIfNeeded(BaseLayoutItem item)
        {
            if (!ReferenceEquals(this.SelectTemplateCore(item), base.Template))
            {
                this.LayoutItem = null;
            }
        }

        private ControlTemplate GetDocumentTemplate()
        {
            DocumentPanel layoutItem = (DocumentPanel) this.LayoutItem;
            return (!layoutItem.IsFloatingRootItem ? (layoutItem.IsMDIChild ? this.MDIDocumentTemplate : this.DocumentTemplate) : this.FloatDocumentTemplate);
        }

        private ControlTemplate GetFloatGroupTemplate()
        {
            ControlTemplate template = null;
            DockLayoutManager manager = DockLayoutManager.Ensure(this, false);
            if (manager != null)
            {
                template = (manager.GetRealFloatingMode() == DevExpress.Xpf.Core.FloatingMode.Window) ? this.FloatingWindowTemplate : this.FloatingAdornerTemplate;
            }
            return template;
        }

        protected override void OnDispose()
        {
            Ref.Dispose<IDisposable>(ref LayoutItemsHelper.GetChild<DependencyObject>(this) as IDisposable);
            base.ClearValue(LayoutItemProperty);
            base.ClearValue(EmptyTemplateProperty);
            base.ClearValue(ControlItemTemplateProperty);
            base.ClearValue(PanelTemplateProperty);
            base.ClearValue(DocumentTemplateProperty);
            base.ClearValue(MDIDocumentTemplateProperty);
            base.ClearValue(FloatDocumentTemplateProperty);
            base.ClearValue(GroupTemplateProperty);
            base.ClearValue(TabPanelTemplateProperty);
            base.ClearValue(DocumentPanelTemplateProperty);
            base.ClearValue(FloatingWindowTemplateProperty);
            base.ClearValue(FloatingAdornerTemplateProperty);
            base.ClearValue(SplitterControlTemplateProperty);
            base.ClearValue(EmptySpaceControlTemplateProperty);
            base.ClearValue(LabelControlTemplateProperty);
            base.ClearValue(SeparatorControlTemplateProperty);
            base.ClearValue(AutoHideGroupTemplateProperty);
            base.ClearValue(Control.TemplateProperty);
            DockLayoutManager.Release(this);
            base.OnDispose();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.SelectTemplate(this.LayoutItem);
        }

        protected virtual void OnLayoutItemChanged(BaseLayoutItem item)
        {
            DependencyObject child = LayoutItemsHelper.GetChild<DependencyObject>(this);
            if (item != null)
            {
                base.SetValue(DockLayoutManager.LayoutItemProperty, item);
                this.SelectTemplate(item);
            }
            else
            {
                if (child is IDisposable)
                {
                    ((IDisposable) child).Dispose();
                }
                base.ClearValue(Control.TemplateProperty);
                DockLayoutManager.Release(this);
                if (child != null)
                {
                    DockLayoutManager.Release(child);
                }
            }
        }

        protected override void OnPrepareContainerForItemComplete()
        {
            base.OnPrepareContainerForItemComplete();
            this.SelectTemplate(this.LayoutItem);
        }

        protected virtual void SelectTemplate(BaseLayoutItem item)
        {
            if (!base.IsDisposing && base.IsInitialized)
            {
                base.Template = this.SelectTemplateCore(item);
            }
        }

        private ControlTemplate SelectTemplateCore(BaseLayoutItem item)
        {
            if (item == null)
            {
                return this.EmptyTemplate;
            }
            ControlTemplate panelTemplate = null;
            switch (item.ItemType)
            {
                case LayoutItemType.Panel:
                    panelTemplate = this.PanelTemplate;
                    break;

                case LayoutItemType.Group:
                    panelTemplate = this.GroupTemplate;
                    break;

                case LayoutItemType.TabPanelGroup:
                    panelTemplate = this.TabPanelTemplate;
                    break;

                case LayoutItemType.FloatGroup:
                    panelTemplate = this.GetFloatGroupTemplate();
                    break;

                case LayoutItemType.Document:
                    panelTemplate = this.GetDocumentTemplate();
                    break;

                case LayoutItemType.DocumentPanelGroup:
                    panelTemplate = this.DocumentPanelTemplate;
                    break;

                case LayoutItemType.AutoHideGroup:
                    panelTemplate = this.AutoHideGroupTemplate;
                    break;

                case LayoutItemType.ControlItem:
                    panelTemplate = this.ControlItemTemplate;
                    break;

                case LayoutItemType.LayoutSplitter:
                    panelTemplate = this.SplitterControlTemplate;
                    break;

                case LayoutItemType.EmptySpaceItem:
                    panelTemplate = this.EmptySpaceControlTemplate;
                    break;

                case LayoutItemType.Separator:
                    panelTemplate = this.SeparatorControlTemplate;
                    break;

                case LayoutItemType.Label:
                    panelTemplate = this.LabelControlTemplate;
                    break;

                default:
                    break;
            }
            return panelTemplate;
        }

        public ControlTemplate EmptyTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(EmptyTemplateProperty);
            set => 
                base.SetValue(EmptyTemplateProperty, value);
        }

        public ControlTemplate ControlItemTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(ControlItemTemplateProperty);
            set => 
                base.SetValue(ControlItemTemplateProperty, value);
        }

        public ControlTemplate PanelTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(PanelTemplateProperty);
            set => 
                base.SetValue(PanelTemplateProperty, value);
        }

        public ControlTemplate DocumentTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(DocumentTemplateProperty);
            set => 
                base.SetValue(DocumentTemplateProperty, value);
        }

        public ControlTemplate MDIDocumentTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(MDIDocumentTemplateProperty);
            set => 
                base.SetValue(MDIDocumentTemplateProperty, value);
        }

        public ControlTemplate FloatDocumentTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(FloatDocumentTemplateProperty);
            set => 
                base.SetValue(FloatDocumentTemplateProperty, value);
        }

        public ControlTemplate GroupTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(GroupTemplateProperty);
            set => 
                base.SetValue(GroupTemplateProperty, value);
        }

        public ControlTemplate TabPanelTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(TabPanelTemplateProperty);
            set => 
                base.SetValue(TabPanelTemplateProperty, value);
        }

        public ControlTemplate DocumentPanelTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(DocumentPanelTemplateProperty);
            set => 
                base.SetValue(DocumentPanelTemplateProperty, value);
        }

        public ControlTemplate FloatingWindowTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(FloatingWindowTemplateProperty);
            set => 
                base.SetValue(FloatingWindowTemplateProperty, value);
        }

        public ControlTemplate FloatingAdornerTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(FloatingAdornerTemplateProperty);
            set => 
                base.SetValue(FloatingAdornerTemplateProperty, value);
        }

        public ControlTemplate SplitterControlTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(SplitterControlTemplateProperty);
            set => 
                base.SetValue(SplitterControlTemplateProperty, value);
        }

        public ControlTemplate EmptySpaceControlTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(EmptySpaceControlTemplateProperty);
            set => 
                base.SetValue(EmptySpaceControlTemplateProperty, value);
        }

        public ControlTemplate LabelControlTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(LabelControlTemplateProperty);
            set => 
                base.SetValue(LabelControlTemplateProperty, value);
        }

        public ControlTemplate SeparatorControlTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(SeparatorControlTemplateProperty);
            set => 
                base.SetValue(SeparatorControlTemplateProperty, value);
        }

        public ControlTemplate AutoHideGroupTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(AutoHideGroupTemplateProperty);
            set => 
                base.SetValue(AutoHideGroupTemplateProperty, value);
        }

        public BaseLayoutItem LayoutItem
        {
            get => 
                (BaseLayoutItem) base.GetValue(LayoutItemProperty);
            set => 
                base.SetValue(LayoutItemProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MultiTemplateControl.<>c <>9 = new MultiTemplateControl.<>c();

            internal void <.cctor>b__17_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((MultiTemplateControl) dObj).OnLayoutItemChanged((BaseLayoutItem) e.NewValue);
            }
        }
    }
}

