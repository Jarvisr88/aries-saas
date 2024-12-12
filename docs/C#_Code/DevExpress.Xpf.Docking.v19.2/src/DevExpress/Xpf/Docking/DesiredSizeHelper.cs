namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Ribbon;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    internal static class DesiredSizeHelper
    {
        private static readonly Dictionary<Type, bool> standardControlsList = new Dictionary<Type, bool>();
        private static readonly bool defaultValue = false;

        static DesiredSizeHelper()
        {
            InitControlsList();
        }

        public static bool CanUseDesiredSizeAsMinSize(Type type)
        {
            KeyValuePair<Type, bool> pair = standardControlsList.FirstOrDefault<KeyValuePair<Type, bool>>(pair => Equals(pair.Key, type) || pair.Key.IsAssignableFrom(type));
            return ((pair.Key == null) ? defaultValue : pair.Value);
        }

        public static bool CanUseDesiredSizeAsMinSize(UIElement control) => 
            (control != null) ? CanUseDesiredSizeAsMinSize(control.GetType()) : defaultValue;

        private static void InitControlsList()
        {
            standardControlsList.Add(typeof(ItemsControl), false);
            standardControlsList.Add(typeof(Image), false);
            standardControlsList.Add(typeof(TextBlock), true);
            standardControlsList.Add(typeof(TextBox), true);
            standardControlsList.Add(typeof(ImageEdit), false);
            standardControlsList.Add(typeof(DockLayoutManager), false);
            standardControlsList.Add(typeof(IRibbonControl), false);
        }
    }
}

