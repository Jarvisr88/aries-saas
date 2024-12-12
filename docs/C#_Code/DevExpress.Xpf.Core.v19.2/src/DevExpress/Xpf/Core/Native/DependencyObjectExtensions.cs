namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Xml;

    public static class DependencyObjectExtensions
    {
        public static readonly DependencyProperty DataContextProperty;

        static DependencyObjectExtensions();
        public static T FindElementByTypeInParents<T>(this DependencyObject element, DependencyObject stopElement) where T: FrameworkElement;
        public static bool FindIsInParents(this DependencyObject child, DependencyObject parent);
        public static object GetCoerceOldValue(this DependencyObject o, DependencyProperty dp);
        public static object GetDataContext(DependencyObject obj);
        public static UIElement GetElementByName(this DependencyObject owner, string elementName);
        public static bool HasDefaultValue(this DependencyObject o, DependencyProperty property);
        public static bool IsInDesignTool(this DependencyObject o);
        public static bool IsPropertyAssigned(this DependencyObject o, DependencyProperty property);
        public static bool IsPropertySet(this DependencyObject o, DependencyProperty property);
        public static void ReadPropertyFromXML(this DependencyObject o, XmlReader xml, DependencyProperty property, string propertyName, Type propertyType);
        public static void RestorePropertyValue(this DependencyObject o, DependencyProperty property, object storedInfo);
        public static void SetBinding(this FrameworkElement o, DependencyObject bindingSource, string bindingPath, DependencyProperty property);
        public static void SetCurrentValueIfDefault(this DependencyObject o, DependencyProperty property, object value);
        public static void SetDataContext(DependencyObject obj, object value);
        public static void SetValueIfDefault(this DependencyObject o, DependencyProperty property, object value);
        public static void SetValueIfNotDefault(this DependencyObject o, DependencyProperty property, object value);
        public static object StoreAndAssignPropertyValue(this DependencyObject o, DependencyProperty property);
        public static object StorePropertyValue(this DependencyObject o, DependencyProperty property);
        public static void WritePropertyToXML(this DependencyObject o, XmlWriter xml, DependencyProperty property, string propertyName);

        [StructLayout(LayoutKind.Sequential)]
        private struct DependencyPropertyValueInfo
        {
            private object _LocalValue;
            private object _Value;
            public DependencyPropertyValueInfo(DependencyObject o, DependencyProperty property);
            public override bool Equals(object obj);
            public override int GetHashCode();
            public void RestorePropertyValue(DependencyObject o, DependencyProperty property);
            private bool IsAssigned { get; }
        }
    }
}

