namespace DevExpress.Xpf.Editors.Validation
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class MouseEventLockHelper : DependencyObject
    {
        public static readonly DependencyProperty ValidationLockInitializedProperty;
        private static readonly DependencyPropertyKey ValidationLockInitializedPropertyKey;
        public static readonly DependencyProperty IsEnabledProperty;

        static MouseEventLockHelper()
        {
            Type ownerType = typeof(MouseEventLockHelper);
            ValidationLockInitializedPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("ValidationLockInitialized", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None));
            ValidationLockInitializedProperty = ValidationLockInitializedPropertyKey.DependencyProperty;
            IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits));
        }

        private static bool DoValidate(IBaseEdit focused)
        {
            focused.FlushPendingEditActions();
            return focused.DoValidate();
        }

        public static bool GetIsEnabled(DependencyObject d) => 
            (bool) d.GetValue(IsEnabledProperty);

        public static bool GetValidationLockInitialized(DependencyObject d) => 
            (bool) d.GetValue(ValidationLockInitializedProperty);

        private static void root_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = ShouldHandleEvent(e.OriginalSource as DependencyObject);
        }

        private static void root_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = ShouldHandleEvent(e.OriginalSource as DependencyObject);
        }

        private static void root_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = ShouldHandleEvent(e.OriginalSource as DependencyObject);
        }

        public static void SetIsEnabled(DependencyObject d, bool ignoreLock)
        {
            d.SetValue(IsEnabledProperty, ignoreLock);
        }

        internal static void SetValidationLockInitialized(DependencyObject d, bool value)
        {
            d.SetValue(ValidationLockInitializedPropertyKey, value);
        }

        private static bool ShouldHandleEvent(DependencyObject originalSource)
        {
            IBaseEdit objB = LayoutHelper.FindLayoutOrVisualParentObject<IBaseEdit>(FocusHelper.GetFocusedElement() as DependencyObject, true, null);
            return (((objB != null) && (objB.InvalidValueBehavior != InvalidValueBehavior.AllowLeaveEditor)) && (GetIsEnabled(originalSource) ? (!ReferenceEquals(LayoutHelper.FindLayoutOrVisualParentObject<IBaseEdit>(originalSource, true, null), objB) ? (((objB.EditMode == EditMode.Standalone) || !objB.HasValidationError) ? !DoValidate(objB) : true) : false) : false));
        }

        private static void SubscribeEventsForLock(FrameworkElement root)
        {
            root.PreviewMouseDown += new MouseButtonEventHandler(MouseEventLockHelper.root_PreviewMouseDown);
            root.PreviewMouseUp += new MouseButtonEventHandler(MouseEventLockHelper.root_PreviewMouseUp);
            root.PreviewMouseWheel += new MouseWheelEventHandler(MouseEventLockHelper.root_PreviewMouseWheel);
        }

        public static void SubscribeMouseEvents(DependencyObject d)
        {
            FrameworkElement element = LayoutHelper.FindRoot(d, false) as FrameworkElement;
            if ((element != null) && !GetValidationLockInitialized(element))
            {
                SetValidationLockInitialized(element, true);
                SubscribeEventsForLock(element);
            }
        }
    }
}

