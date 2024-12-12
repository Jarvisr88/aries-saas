namespace DevExpress.Xpf.LayoutControl.Serialization
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core.Serialization;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class SerializableItem : ISerializableItem, ISerializableCollectionItem, ISupportInitialize
    {
        public static readonly DependencyProperty TypeNameProperty = DependencyProperty.RegisterAttached("TypeName", typeof(string), typeof(SerializableItem), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty ParentNameProperty = DependencyProperty.RegisterAttached("ParentName", typeof(string), typeof(SerializableItem), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty ParentCollectionNameProperty = DependencyProperty.RegisterAttached("ParentCollectionName", typeof(string), typeof(SerializableItem), new PropertyMetadata(string.Empty));

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }

        [XtraSerializableProperty]
        public static string GetParentCollectionName(DependencyObject obj) => 
            (string) obj.GetValue(ParentCollectionNameProperty);

        [XtraSerializableProperty]
        public static string GetParentName(DependencyObject obj) => 
            (string) obj.GetValue(ParentNameProperty);

        [XtraSerializableProperty, XtraResetProperty(ResetPropertyMode.None)]
        public static string GetTypeName(DependencyObject obj) => 
            (string) obj.GetValue(TypeNameProperty);

        public static void SetParentCollectionName(DependencyObject obj, string value)
        {
            obj.SetValue(ParentCollectionNameProperty, value);
        }

        public static void SetParentName(DependencyObject obj, string value)
        {
            obj.SetValue(ParentNameProperty, value);
        }

        public static void SetTypeName(DependencyObject obj, string value)
        {
            obj.SetValue(TypeNameProperty, value);
        }

        [XtraSerializableProperty]
        public string Name { get; set; }

        [XtraSerializableProperty]
        public string TypeName { get; set; }

        [XtraSerializableProperty]
        public string ParentName { get; set; }

        [XtraSerializableProperty]
        public string ParentCollectionName { get; set; }

        [XtraSerializableProperty]
        public System.Windows.HorizontalAlignment HorizontalAlignment { get; set; }

        [XtraSerializableProperty]
        public System.Windows.VerticalAlignment VerticalAlignment { get; set; }

        [XtraSerializableProperty]
        public double Width { get; set; }

        [XtraSerializableProperty]
        public double Height { get; set; }

        public FrameworkElement Element { get; set; }
    }
}

