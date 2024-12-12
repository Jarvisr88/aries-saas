namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Windows;

    public static class ElementHelper
    {
        public static Rect GetRect(ILayoutElement element) => 
            new Rect(element.Location, element.Size);

        public static ILayoutElement GetRoot(ILayoutElement element)
        {
            if (element == null)
            {
                return null;
            }
            if (element.Parent == null)
            {
                return element;
            }
            ILayoutElement parent = element;
            while (parent.Parent != null)
            {
                parent = parent.Parent;
            }
            return parent;
        }

        public static IUIElement GetRoot(IUIElement element)
        {
            if (element == null)
            {
                return null;
            }
            if (element.Scope == null)
            {
                return element;
            }
            IUIElement scope = element;
            while (scope.Scope != null)
            {
                scope = scope.Scope;
            }
            return scope;
        }

        public static Rect GetScreenRect(ILayoutElementHost host)
        {
            ILayoutElement layoutRoot = host.LayoutRoot;
            return new Rect(host.ClientToScreen(layoutRoot.Location), layoutRoot.Size);
        }

        public static Rect GetScreenRect(ILayoutElementHost host, ILayoutElement element) => 
            new Rect(host.ClientToScreen(element.Location), element.Size);
    }
}

