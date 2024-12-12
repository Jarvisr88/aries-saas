namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils;
    using System;
    using System.ComponentModel;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    public static class XamlTemplateHelper
    {
        private const string DefaultNamespaces = " xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" ";
        public const string RootName = "Root";

        public static ControlTemplate CreateControlTemplate(Type targetType, Type controlClass)
        {
            StringBuilder namespaces = null;
            string str = GetTypeString("ddd1", targetType, ref namespaces);
            string str2 = GetTypeString("ddd2", controlClass, ref namespaces);
            string[] textArray1 = new string[] { "<ControlTemplate", namespaces.ToString(), "TargetType=\"", str, "\"><", str2, " x:Name=\"Root\" /></ControlTemplate>" };
            return (ControlTemplate) XamlReader.Parse(string.Concat(textArray1));
        }

        public static DataTemplate CreateDataTemplate(Type controlClass)
        {
            StringBuilder namespaces = null;
            string str = GetTypeString("ddd", controlClass, ref namespaces);
            string[] textArray1 = new string[] { "<DataTemplate", namespaces.ToString(), "><", str, " /></DataTemplate>" };
            return (DataTemplate) XamlReader.Parse(string.Concat(textArray1));
        }

        public static T CreateObjectFromTemplate<T>(DataTemplate template) where T: class
        {
            if (template != null)
            {
                DependencyObject obj2 = template.LoadContent();
                if (obj2 == null)
                {
                    return default(T);
                }
                T local = obj2 as T;
                if (local != null)
                {
                    return local;
                }
                ContentControl control = obj2 as ContentControl;
                if (control != null)
                {
                    return (control.Content as T);
                }
                ContentPresenter presenter = obj2 as ContentPresenter;
                if (presenter != null)
                {
                    return (presenter.Content as T);
                }
            }
            return default(T);
        }

        public static object CreateObjectFromXaml(Type targetType, object source)
        {
            if (source == null)
            {
                return null;
            }
            if (source.GetType().IsSubclassOf(targetType))
            {
                return source;
            }
            try
            {
                return TypeDescriptor.GetConverter(targetType).ConvertFromInvariantString(source.ToString());
            }
            catch (Exception)
            {
                return source;
            }
        }

        public static FrameworkElement CreateVisualTree(object content, DataTemplate contentTemplate)
        {
            if (contentTemplate == null)
            {
                return (content as FrameworkElement);
            }
            FrameworkElement element = contentTemplate.LoadContent() as FrameworkElement;
            if (element == null)
            {
                return null;
            }
            element.DataContext = content;
            return element;
        }

        public static string GetTypeString(string prefix, Type type, ref StringBuilder namespaces)
        {
            namespaces ??= new StringBuilder(" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" ");
            namespaces.Append(string.Format(" xmlns:" + prefix + "=\"clr-namespace:{0};assembly={1}\" ", type.Namespace, AssemblyHelper.GetPartialName(type.Assembly)));
            return (prefix + ":" + type.Name);
        }

        public static string GetXamlPath(string path) => 
            path;
    }
}

