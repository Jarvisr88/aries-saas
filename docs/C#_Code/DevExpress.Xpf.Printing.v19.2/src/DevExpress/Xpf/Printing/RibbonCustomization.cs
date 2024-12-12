namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Ribbon;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;

    public static class RibbonCustomization
    {
        public static readonly DependencyProperty TemplateProperty = DependencyProperty.RegisterAttached("Template", typeof(DataTemplate), typeof(RibbonCustomization), new PropertyMetadata(null, new PropertyChangedCallback(RibbonCustomization.TemplateChangedCallback)));
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty TemplatesCollectorProperty = DependencyProperty.RegisterAttached("TemplatesCollector", typeof(List<DataTemplate>), typeof(RibbonCustomization), new PropertyMetadata(new List<DataTemplate>()));

        private static void AddAndExecute(RibbonControl ribbon, DataTemplate template)
        {
            if (!ribbon.Controllers.OfType<TemplatedRibbonController>().Any<TemplatedRibbonController>(x => ReferenceEquals(x.Template, template)))
            {
                TemplatedRibbonController controller1 = new TemplatedRibbonController();
                controller1.Template = template;
                TemplatedRibbonController item = controller1;
                ribbon.Controllers.Add(item);
                item.Execute(null);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal static void ApplyTemplate(RibbonDocumentPreview preview)
        {
            if ((preview != null) && (preview.Ribbon != null))
            {
                List<DataTemplate> templatesCollector = GetTemplatesCollector(preview);
                foreach (DataTemplate template2 in templatesCollector)
                {
                    AddAndExecute(preview.Ribbon, template2);
                }
                DataTemplate item = GetTemplate(preview);
                if (!templatesCollector.Contains(item))
                {
                    AddAndExecute(preview.Ribbon, item);
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
                RibbonDocumentPreview preview = d as RibbonDocumentPreview;
                if (preview == null)
                {
                    throw new NotSupportedException("Dependency Property can by applied only to the DevExpress.Xpf.Printing.RibbonDocumentPreview");
                }
                if (preview.Ribbon == null)
                {
                    GetTemplatesCollector(d).Add((DataTemplate) e.NewValue);
                }
                ApplyTemplate(preview);
            }
        }
    }
}

