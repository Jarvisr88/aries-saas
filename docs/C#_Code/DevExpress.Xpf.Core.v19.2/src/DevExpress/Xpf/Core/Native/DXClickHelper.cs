namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Input;

    public class DXClickHelper
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty HandleMouseLeftButtonProperty;
        private UIElement root;
        private UIElement clickedElem;
        private ClickHelperDelegate clickDelegate;

        static DXClickHelper();
        public DXClickHelper(UIElement root, ClickHelperDelegate clickDelegate);
        public static UIElement GetChildElement(MouseEventArgs args);
        internal static UIElement GetChildElementCore(UIElement source, Point pt);
        public static UIElement GetCommonParent(UIElement elem1, UIElement elem2, UIElement root);
        private static List<DependencyObject> GetParents(UIElement elem, UIElement root);
        private void OnLostMouseCapture(object sender, MouseEventArgs e);
        private void OnMouseDown(object sender, MouseButtonEventArgs e);
        private void OnMouseUp(object sender, MouseButtonEventArgs e);
        private bool RaiseClick(UIElement clickedElem, MouseButton button);

        public UIElement Root { get; }

        public ClickHelperDelegate ClickDelegate { get; set; }

        public bool HandleMouseButton { get; set; }
    }
}

