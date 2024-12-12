namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Platform.Win32;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public static class VisualOrderHelper
    {
        public static void BringToFront(this FloatPanePresenter pane, DockLayoutManager container)
        {
            if ((pane != null) && (container != null))
            {
                Panel floatingRoot = GetFloatingRoot(pane);
                if (floatingRoot != null)
                {
                    UpdateVisualOrder(GetFloatingRootChild(pane), GetChildren(floatingRoot));
                    ((IUIElement) container).Children.MakeLast(pane);
                }
            }
        }

        private static UIElement[] GetChildren(Panel floatingRoot)
        {
            UIElement[] array = new UIElement[floatingRoot.Children.Count];
            floatingRoot.Children.CopyTo(array, 0);
            return array;
        }

        private static Panel GetFloatingRoot(FloatPanePresenter element)
        {
            if (element == null)
            {
                return null;
            }
            UIElement reference = element;
            while ((reference != null) && !(reference is GroupPanel))
            {
                reference = VisualTreeHelper.GetParent(reference) as UIElement;
            }
            return (reference as Panel);
        }

        private static UIElement GetFloatingRootChild(FloatPanePresenter element)
        {
            if (element == null)
            {
                return null;
            }
            UIElement reference = element;
            UIElement element3 = null;
            while ((reference != null) && !(reference is GroupPanel))
            {
                element3 = reference;
                reference = VisualTreeHelper.GetParent(reference) as UIElement;
            }
            return element3;
        }

        public static int GetVisualOrder(this UIElement element, DockLayoutManager container)
        {
            int num = -1;
            using (IUIElementEnumerator enumerator = new IUIElementEnumerator(container))
            {
                while (enumerator.MoveNext())
                {
                    num++;
                    if (ReferenceEquals(enumerator.Current, element))
                    {
                        break;
                    }
                }
            }
            switch (num)
            {
                case (num == -1):
                    break;

                case (LayoutGroup _):
                    num += 0x186a0;
                    break;

                case (AutoHideTray _):
                    num += 0x30d40;
                    break;

                case (FloatPanePresenter _):
                {
                    Func<FloatingWindowPresenter, FloatingPaneWindow> evaluator = <>c.<>9__0_0;
                    if (<>c.<>9__0_0 == null)
                    {
                        Func<FloatingWindowPresenter, FloatingPaneWindow> local1 = <>c.<>9__0_0;
                        evaluator = <>c.<>9__0_0 = x => x.Window;
                    }
                    FloatingPaneWindow floatingWindow = (element as FloatingWindowPresenter).With<FloatingWindowPresenter, FloatingPaneWindow>(evaluator);
                    if (floatingWindow == null)
                    {
                        num += 0x493e0;
                    }
                    else
                    {
                        IEnumerable<Window> enumerable1;
                        if (Application.Current != null)
                        {
                            enumerable1 = NativeHelper.SortWindowsTopToBottom(container.GetAffectedWindows());
                        }
                        else
                        {
                            FloatingPaneWindow[] windowArray1 = new FloatingPaneWindow[] { floatingWindow };
                            enumerable1 = windowArray1;
                        }
                        IEnumerable<Window> source = enumerable1;
                        Window mainWindow = WindowServiceHelper.GetWindow(container);
                        if (source.FirstOrDefault<Window>(x => (ReferenceEquals(x, mainWindow) || ReferenceEquals(x, floatingWindow))) == floatingWindow)
                        {
                            num += 0x493e0;
                        }
                    }
                    break;
                }
            }
            return num;
        }

        private static void UpdateVisualOrder(UIElement topChild, UIElement[] children)
        {
            for (int i = 0; i < children.Length; i++)
            {
                if (!ReferenceEquals(topChild, children[i]))
                {
                    Panel.SetZIndex(children[i], i);
                }
            }
            Panel.SetZIndex(topChild, children.Length + 1);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly VisualOrderHelper.<>c <>9 = new VisualOrderHelper.<>c();
            public static Func<FloatingWindowPresenter, FloatingPaneWindow> <>9__0_0;

            internal FloatingPaneWindow <GetVisualOrder>b__0_0(FloatingWindowPresenter x) => 
                x.Window;
        }
    }
}

