namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;

    public class EditorCustomItem : DependencyObject, ICustomItem
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static DependencyProperty EditValueProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static DependencyProperty DisplayValueProperty;

        static EditorCustomItem()
        {
            Type ownerType = typeof(EditorCustomItem);
            EditValueProperty = DependencyPropertyManager.Register("EditValue", typeof(object), ownerType);
            DisplayValueProperty = DependencyPropertyManager.Register("DisplayValue", typeof(object), ownerType);
        }

        public object EditValue
        {
            get => 
                base.GetValue(EditValueProperty);
            set => 
                base.SetValue(EditValueProperty, value);
        }

        public object DisplayValue
        {
            get => 
                base.GetValue(DisplayValueProperty);
            set => 
                base.SetValue(DisplayValueProperty, value);
        }
    }
}

