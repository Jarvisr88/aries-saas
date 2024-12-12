namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public static class MoveFocusHelper
    {
        public static void MoveFocus(FrameworkElement focusOwner, bool isReverse)
        {
            if (isReverse)
            {
                focusOwner.MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
            }
            else
            {
                FrameworkElement lastElement = focusOwner;
                LayoutHelper.ForEachElement(focusOwner, element => lastElement = element);
                lastElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }
    }
}

