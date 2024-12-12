namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class DefaultColumnChooser : ColumnChooserBase
    {
        public DefaultColumnChooser(DataViewBase view) : base(view)
        {
            this.UpdateCaption();
        }

        protected override Control CreateContentControl()
        {
            Control element = base.CreateContentControl();
            DataControlBase.SetCurrentViewInternal(element, this.View);
            string name = DataViewBase.ActualColumnChooserTemplateProperty.GetName();
            Binding binding = new Binding(name);
            binding.Source = this.View;
            element.SetBinding(Control.TemplateProperty, binding);
            return element;
        }

        internal void DestroyBase(bool force)
        {
            base.Destroy(force);
        }

        protected override void DestroyContentControl()
        {
            base.DestroyContentControl();
            Control content = base.Container.Content as Control;
            if (content != null)
            {
                DataControlBase.SetCurrentViewInternal(content, null);
            }
        }

        protected override void OnContainerHidden(object sender, RoutedEventArgs e)
        {
            this.View.IsColumnChooserVisible = false;
        }

        internal void UpdateCaption()
        {
            Binding binding = new Binding("LocalizationDescriptor");
            binding.Source = this.View;
            binding.Converter = new ColumnChooserCaptionLocalizationStringConvertor(this.View);
            BindingOperations.SetBinding(this, ColumnChooserBase.CaptionProperty, binding);
        }

        protected DataViewBase View =>
            (DataViewBase) base.Owner;

        protected override ILogicalOwner Owner =>
            this.View.RootView;
    }
}

