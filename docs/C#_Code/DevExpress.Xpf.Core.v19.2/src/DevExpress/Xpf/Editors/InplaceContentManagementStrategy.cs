namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Media;

    public class InplaceContentManagementStrategy : ContentManagementStrategyBase
    {
        public InplaceContentManagementStrategy(BaseEdit edit) : base(edit)
        {
        }

        public override Size ArrangeOverride(Size arrangeSize) => 
            !base.Edit.BorderRenderer.CanRenderBorder ? base.Edit.ArrangeOverrideInplaceMode(arrangeSize) : base.Edit.BorderRenderer.ArrangeOverride(arrangeSize);

        public override Visual GetVisualChild(int index) => 
            base.Edit.GetVisualChildInplaceMode(index);

        public override Size MeasureOverride(Size constraint) => 
            !base.Edit.BorderRenderer.CanRenderBorder ? base.Edit.MeasureOverrideInplaceMode(constraint) : base.Edit.BorderRenderer.MeasureOverride(constraint);

        public override void OnEditorApplyTemplate()
        {
            base.Edit.EditCore = base.Edit.GetTemplateChildInternal<FrameworkElement>("PART_Editor");
            this.Updater = new DataContextUpdater(base.Edit, base.Edit.EditCore);
            this.Updater.AttachDataContext();
            this.UpdateBorder();
            this.UpdateButtonPanels();
            this.UpdateGlow();
        }

        public void UpdateBorder()
        {
            base.Edit.UpdateBorderInInplaceMode();
        }

        public override void UpdateButtonPanels()
        {
            base.Edit.UpdateButtonPanelsInplaceMode();
        }

        public override void UpdateErrorPresenter()
        {
            base.Edit.UpdateInplaceErrorPresenter();
        }

        public void UpdateGlow()
        {
            base.Edit.UpdateGlowInInplaceMode();
        }

        private DataContextUpdater Updater { get; set; }

        public override int VisualChildrenCount =>
            base.Edit.VisualChildrenCountInplaceMode;

        private class DataContextUpdater : DependencyObject
        {
            public static readonly DependencyProperty DataContextProperty;
            private bool isDataContextChanged;

            static DataContextUpdater()
            {
                Type ownerType = typeof(InplaceContentManagementStrategy.DataContextUpdater);
                DataContextProperty = DependencyProperty.Register("DataContext", typeof(object), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(InplaceContentManagementStrategy.DataContextUpdater.DataContextPropertyChanged)));
            }

            public DataContextUpdater(BaseEdit editor, FrameworkElement target)
            {
                this.Target = target;
                this.Editor = editor;
            }

            private void ApplyBinding()
            {
                Binding binding1 = new Binding();
                binding1.Source = this.Editor;
                binding1.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                binding1.Path = new PropertyPath(FrameworkElement.DataContextProperty);
                Binding binding = binding1;
                this.isDataContextChanged = false;
                BindingOperations.SetBinding(this, DataContextProperty, binding);
                if (!this.isDataContextChanged)
                {
                    this.UpdateDataContext(null);
                }
            }

            public void AttachDataContext()
            {
                this.ApplyBinding();
            }

            private static void DataContextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((InplaceContentManagementStrategy.DataContextUpdater) d).UpdateDataContext(e.NewValue as DependencyObject);
            }

            private void UpdateDataContext(DependencyObject d)
            {
                this.isDataContextChanged = true;
                if (this.Target != null)
                {
                    if (d == null)
                    {
                        this.Target.DataContext = this.Editor;
                    }
                    else
                    {
                        this.Target.ClearValue(FrameworkElement.DataContextProperty);
                    }
                }
                if (d != null)
                {
                    this.Editor.EditStrategy.UpdateDataContext(d);
                }
            }

            public FrameworkElement Target { get; private set; }

            public BaseEdit Editor { get; private set; }
        }
    }
}

