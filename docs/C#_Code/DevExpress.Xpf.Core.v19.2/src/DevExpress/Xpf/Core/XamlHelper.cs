namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    public static class XamlHelper
    {
        public static readonly DependencyProperty NameProperty = DependencyProperty.RegisterAttached("Name", typeof(string), typeof(XamlHelper), new PropertyMetadata(null, new PropertyChangedCallback(XamlHelper.OnNameChanged)));
        private const string DefaultNamespaces = "xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"";
        private static string namespaces = "xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"";

        public static ControlTemplate GetControlTemplate(string template) => 
            LoadObjectCore(GetDefaultDisplayTemplateText(template, "ControlTemplate")) as ControlTemplate;

        private static string GetDefaultDisplayTemplateText(string template, string templateTypeName) => 
            GetDefaultDisplayTemplateText(template, templateTypeName, string.Empty);

        private static string GetDefaultDisplayTemplateText(string template, string templateTypeName, string additionalAttributes) => 
            string.Format(GetDefaultTemplateFormatText(templateTypeName, additionalAttributes), template);

        private static string GetDefaultTemplateFormatText(string templateTypeName) => 
            GetDefaultTemplateFormatText(templateTypeName, string.Empty);

        private static string GetDefaultTemplateFormatText(string templateTypeName, string additionalAttributes)
        {
            string[] textArray1 = new string[9];
            textArray1[0] = "<";
            textArray1[1] = templateTypeName;
            textArray1[2] = " ";
            textArray1[3] = namespaces;
            textArray1[4] = " ";
            textArray1[5] = additionalAttributes;
            textArray1[6] = ">{0}</";
            textArray1[7] = templateTypeName;
            textArray1[8] = ">";
            return string.Concat(textArray1);
        }

        public static ItemsPanelTemplate GetItemsPanelTemplate(string template) => 
            LoadObjectCore(GetDefaultDisplayTemplateText(template, "ItemsPanelTemplate")) as ItemsPanelTemplate;

        public static string GetName(DependencyObject d) => 
            (string) d.GetValue(NameProperty);

        public static object GetObject(string template) => 
            LoadObjectCore($"<{template} {namespaces}/>");

        public static DataTemplate GetTemplate(string template) => 
            LoadObjectCore(GetDefaultDisplayTemplateText(template, "DataTemplate")) as DataTemplate;

        public static T LoadContent<T>(string template) where T: DependencyObject => 
            GetTemplate(template).LoadContent() as T;

        public static T LoadObject<T>(string xamlContent, string additionalAttributes) => 
            (T) LoadObjectCore(GetDefaultDisplayTemplateText(xamlContent, typeof(T).Name, additionalAttributes));

        public static object LoadObjectCore(string xaml) => 
            XamlReader.Parse(xaml);

        private static void OnNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!d.IsPropertySet(FrameworkElement.NameProperty))
            {
                d.SetValue(FrameworkElement.NameProperty, e.NewValue);
            }
        }

        public static void SetLocalNamespace(string name)
        {
            namespaces = "xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" xmlns:local=\"" + name + "\"";
        }

        public static void SetName(DependencyObject d, string name)
        {
            d.SetValue(NameProperty, name);
        }
    }
}

