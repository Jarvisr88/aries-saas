namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class SetCurrentValueHelper
    {
        public static void SetCurrentValue(this BaseEdit edit, DependencyProperty property, object value)
        {
            edit.SetCurrentValue(property, value);
        }
    }
}

