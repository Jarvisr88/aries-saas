namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class EditItemContentPresenter : ContentPresenter
    {
        public static readonly DependencyProperty VerticalContentAlignmentProperty = DependencyProperty.Register("VerticalContentAlignment", typeof(VerticalAlignment), typeof(EditItemContentPresenter), new PropertyMetadata(VerticalAlignment.Center));

        static EditItemContentPresenter()
        {
            FrameworkElementFactory factory = new FrameworkElementFactory {
                Type = typeof(TextBlock)
            };
            factory.SetValue(FrameworkElement.VerticalAlignmentProperty, new TemplateBindingExtension(VerticalContentAlignmentProperty));
            Binding binding = new Binding();
            binding.RelativeSource = RelativeSource.TemplatedParent;
            binding.Path = new PropertyPath(ContentPresenter.ContentProperty);
            binding.Converter = ContentTemplateConverter.Instance;
            factory.SetBinding(TextBlock.TextProperty, binding);
            DataTemplate template1 = new DataTemplate();
            template1.VisualTree = factory;
            StringContentTemplate = template1;
            StringContentTemplate.Seal();
        }

        protected override DataTemplate ChooseTemplate()
        {
            DataTemplate template = base.ChooseTemplate();
            return ((template.GetType().Name == "DefaultTemplate") ? StringContentTemplate : template);
        }

        public VerticalAlignment VerticalContentAlignment
        {
            get => 
                (VerticalAlignment) base.GetValue(VerticalContentAlignmentProperty);
            set => 
                base.SetValue(VerticalContentAlignmentProperty, value);
        }

        private static DataTemplate StringContentTemplate { get; set; }
    }
}

