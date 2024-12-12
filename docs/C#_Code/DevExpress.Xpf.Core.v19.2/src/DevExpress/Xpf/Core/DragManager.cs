namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public static class DragManager
    {
        public static readonly DependencyProperty IsStartDragPlaceProperty = DependencyPropertyManager.RegisterAttached("IsStartDragPlace", typeof(bool), typeof(DragManager), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits));
        public static readonly DependencyProperty DropTargetFactoryProperty = DependencyPropertyManager.RegisterAttached("DropTargetFactory", typeof(IDropTargetFactory), typeof(DragManager), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty IsDraggingProperty = DependencyPropertyManager.RegisterAttached("IsDragging", typeof(bool), typeof(DragManager), new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty AllowMouseMoveSelectionFuncProperty = DependencyPropertyManager.RegisterAttached("AllowMouseMoveSelectionFunc", typeof(Func<MouseEventArgs, bool>), typeof(DragManager), new FrameworkPropertyMetadata(null));

        public static Func<MouseEventArgs, bool> GetAllowMouseMoveSelectionFunc(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (Func<MouseEventArgs, bool>) element.GetValue(AllowMouseMoveSelectionFuncProperty);
        }

        public static IDropTargetFactory GetDropTargetFactory(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (IDropTargetFactory) element.GetValue(DropTargetFactoryProperty);
        }

        public static bool GetIsDragging(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (bool) element.GetValue(IsDraggingProperty);
        }

        public static bool GetIsStartDragPlace(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (bool) element.GetValue(IsStartDragPlaceProperty);
        }

        public static void RemoveAllowMouseMoveSelectionFunc(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.ClearValue(AllowMouseMoveSelectionFuncProperty);
        }

        public static void SetAllowMouseMoveSelectionFunc(DependencyObject element, Func<MouseEventArgs, bool> value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(AllowMouseMoveSelectionFuncProperty, value);
        }

        public static void SetDropTargetFactory(DependencyObject element, IDropTargetFactory value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(DropTargetFactoryProperty, value);
        }

        public static void SetIsDragging(DependencyObject element, bool value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(IsDraggingProperty, value);
        }

        public static void SetIsStartDragPlace(DependencyObject element, bool value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(IsStartDragPlaceProperty, value);
        }
    }
}

