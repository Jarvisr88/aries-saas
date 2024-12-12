namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows;

    public class BaseEditScrollBehavior : NativeScrollBehavior
    {
        public override bool CheckHandlesMouseWheelScrolling(DependencyObject source) => 
            ((BaseEdit) source).EditMode != EditMode.InplaceInactive;
    }
}

