namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;

    public static class BarManagerCustomization
    {
        public static readonly DependencyProperty TemplateProperty = DependencyProperty.RegisterAttached("Template", typeof(DataTemplate), typeof(BarManagerCustomization), new PropertyMetadata(null, new PropertyChangedCallback(BarManagerCustomization.TemplateChangedCallback)));
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty TemplatesCollectorProperty = DependencyProperty.RegisterAttached("TemplatesCollector", typeof(List<DataTemplate>), typeof(BarManagerCustomization), new PropertyMetadata(new List<DataTemplate>()));

        private static void AddAndExecute(BarManager manager, DataTemplate template)
        {
            if (!manager.Controllers.OfType<TemplatedBarManagerController>().Any<TemplatedBarManagerController>(x => ReferenceEquals(x.Template, template)))
            {
                TemplatedBarManagerController controller1 = new TemplatedBarManagerController();
                controller1.Template = template;
                TemplatedBarManagerController item = controller1;
                manager.Controllers.Add(item);
                item.Execute(null);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal static void ApplyTemplate(DocumentPreview preview)
        {
            if ((preview != null) && (preview.BarManager != null))
            {
                List<DataTemplate> templatesCollector = GetTemplatesCollector(preview);
                foreach (DataTemplate template2 in templatesCollector)
                {
                    AddAndExecute(preview.BarManager, template2);
                }
                DataTemplate item = GetTemplate(preview);
                if (!templatesCollector.Contains(item))
                {
                    AddAndExecute(preview.BarManager, item);
                }
                templatesCollector.Clear();
            }
        }

        public static DataTemplate GetTemplate(DependencyObject obj) => 
            (DataTemplate) obj.GetValue(TemplateProperty);

        private static List<DataTemplate> GetTemplatesCollector(DependencyObject obj) => 
            (List<DataTemplate>) obj.GetValue(TemplatesCollectorProperty);

        public static void SetTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(TemplateProperty, value);
        }

        private static void TemplateChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((d != null) && (e.NewValue != null))
            {
                DocumentPreview preview = d as DocumentPreview;
                if (preview == null)
                {
                    throw new NotSupportedException("Dependency Property can by applied only to the DevExpress.Xpf.Printing.DocumentPreview");
                }
                if (preview.BarManager == null)
                {
                    GetTemplatesCollector(d).Add((DataTemplate) e.NewValue);
                }
                ApplyTemplate(preview);
            }
        }
    }
}

