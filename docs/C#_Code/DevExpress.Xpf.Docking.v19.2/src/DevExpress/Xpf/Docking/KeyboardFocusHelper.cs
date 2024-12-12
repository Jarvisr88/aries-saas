namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking.Platform;
    using DevExpress.Xpf.Ribbon;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Threading;

    internal static class KeyboardFocusHelper
    {
        private static bool CanFocusResultCallback(DependencyObject e)
        {
            if (e is BarManager)
            {
                return false;
            }
            IInputElement element = e as IInputElement;
            return ((element != null) && element.Focusable);
        }

        private static bool CanFocusSkipCallback(DependencyObject d)
        {
            UIElement element = d as UIElement;
            return (((element == null) || element.IsVisible) ? ((d is Menu) || ((d is BarItemLinkInfo) || ((d is ToolBar) || ((d is IRibbonStatusBarControl) || (d is IRibbonControl))))) : true);
        }

        public static IInputElement Convert(UIElement element) => 
            element;

        public static bool Focusable(UIElement element) => 
            element.Focusable;

        public static void FocusElement(UIElement element, bool ignoreCurrentFocusState = false)
        {
            if ((element != null) && (ignoreCurrentFocusState || !IsKeyboardFocusWithin(element)))
            {
                if (InvokePostFocusRequired(element))
                {
                    object[] args = new object[] { element };
                    DevExpress.Xpf.Docking.InvokeHelper.BeginInvoke(element, new Action<UIElement>(KeyboardFocusHelper.PostFocus), DispatcherPriority.Loaded, args);
                }
                else
                {
                    PostFocus(element);
                }
            }
        }

        private static DependencyObject GetElementToFocus(UIElement element)
        {
            DependencyObject root = element;
            DependencyObject focusedElement = FocusManager.GetFocusedElement(FocusManager.GetIsFocusScope(element) ? element : FocusManager.GetFocusScope(element)) as DependencyObject;
            if ((focusedElement != null) && LayoutHelper.IsChildElement(element, focusedElement))
            {
                FrameworkElement element2 = focusedElement as FrameworkElement;
                if ((element2 == null) || (element2.IsVisible && element2.IsLoaded))
                {
                    root = focusedElement;
                }
            }
            if (root != null)
            {
                root = LayoutItemsHelper.FindElementInVisualTree(root, new Predicate<DependencyObject>(KeyboardFocusHelper.CanFocusSkipCallback), new Predicate<DependencyObject>(KeyboardFocusHelper.CanFocusResultCallback));
            }
            if (root == null)
            {
                BaseLayoutItem layoutItem = DockLayoutManager.GetLayoutItem(element);
                if (layoutItem != null)
                {
                    root = layoutItem.GetUIElement<psvSelectorItem>();
                }
            }
            return root;
        }

        private static bool InvokePostFocusRequired(DependencyObject dObj) => 
            WindowHelper.IsXBAP ? (LayoutHelper.FindParentObject<Page>(dObj) == null) : InvokePostFocusRequiredCore(dObj);

        private static bool InvokePostFocusRequiredCore(DependencyObject dObj) => 
            ReferenceEquals(PresentationSource.FromDependencyObject(dObj), null);

        public static bool IsKeyboardFocused(UIElement element) => 
            element.IsKeyboardFocused;

        public static bool IsKeyboardFocusWithin(UIElement element) => 
            element.IsKeyboardFocusWithin;

        private static void PostFocus(UIElement element)
        {
            Func<UIElement, bool> evaluator = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<UIElement, bool> local1 = <>c.<>9__5_0;
                evaluator = <>c.<>9__5_0 = x => x.IsKeyboardFocusWithin;
            }
            if (!element.Return<UIElement, bool>(evaluator, (<>c.<>9__5_1 ??= () => false)))
            {
                DependencyObject elementToFocus = GetElementToFocus(element);
                if (elementToFocus != null)
                {
                    PostFocusCore(elementToFocus);
                }
            }
        }

        private static void PostFocusCore(DependencyObject elementToFocus)
        {
            UIElement element = elementToFocus as UIElement;
            IInputElement input = elementToFocus as IInputElement;
            if (element != null)
            {
                if (!element.IsKeyboardFocusWithin && element.IsVisible)
                {
                    element.Focus();
                }
            }
            else
            {
                Func<IInputElement, bool> evaluator = <>c.<>9__8_0;
                if (<>c.<>9__8_0 == null)
                {
                    Func<IInputElement, bool> local1 = <>c.<>9__8_0;
                    evaluator = <>c.<>9__8_0 = x => !x.IsKeyboardFocusWithin;
                }
                Action<IInputElement> action = <>c.<>9__8_1;
                if (<>c.<>9__8_1 == null)
                {
                    Action<IInputElement> local2 = <>c.<>9__8_1;
                    action = <>c.<>9__8_1 = x => Keyboard.Focus(x);
                }
                input.If<IInputElement>(evaluator).Do<IInputElement>(action);
            }
        }

        public static void RestoreKeyboardFocus(FrameworkElement element)
        {
            IInputElement focusedElement = FocusManager.GetFocusedElement(FocusManager.GetIsFocusScope(element) ? element : FocusManager.GetFocusScope(element));
            if (focusedElement != null)
            {
                FocusElement(focusedElement as UIElement, false);
            }
        }

        public static void SetFocus(UIElement element)
        {
            element.Focus();
        }

        public static DependencyObject FocusedElement =>
            Keyboard.FocusedElement as DependencyObject;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly KeyboardFocusHelper.<>c <>9 = new KeyboardFocusHelper.<>c();
            public static Func<UIElement, bool> <>9__5_0;
            public static Func<bool> <>9__5_1;
            public static Func<IInputElement, bool> <>9__8_0;
            public static Action<IInputElement> <>9__8_1;

            internal bool <PostFocus>b__5_0(UIElement x) => 
                x.IsKeyboardFocusWithin;

            internal bool <PostFocus>b__5_1() => 
                false;

            internal bool <PostFocusCore>b__8_0(IInputElement x) => 
                !x.IsKeyboardFocusWithin;

            internal void <PostFocusCore>b__8_1(IInputElement x)
            {
                Keyboard.Focus(x);
            }
        }
    }
}

