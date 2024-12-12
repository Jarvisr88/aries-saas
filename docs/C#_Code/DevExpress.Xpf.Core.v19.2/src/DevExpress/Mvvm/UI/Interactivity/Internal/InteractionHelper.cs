namespace DevExpress.Mvvm.UI.Interactivity.Internal
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.UI.Interactivity;
    using System;
    using System.Windows;

    public static class InteractionHelper
    {
        public static readonly DependencyProperty EnableBehaviorsInDesignTimeProperty = DependencyProperty.RegisterAttached("EnableBehaviorsInDesignTime", typeof(bool), typeof(InteractionHelper), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));
        public static readonly DependencyProperty BehaviorInDesignModeProperty = DependencyProperty.RegisterAttached("BehaviorInDesignMode", typeof(InteractionBehaviorInDesignMode), typeof(InteractionHelper), new PropertyMetadata(InteractionBehaviorInDesignMode.Default));

        public static InteractionBehaviorInDesignMode GetBehaviorInDesignMode(DependencyObject d) => 
            (InteractionBehaviorInDesignMode) d.GetValue(BehaviorInDesignModeProperty);

        public static bool GetEnableBehaviorsInDesignTime(DependencyObject d) => 
            (d != null) ? ((bool) d.GetValue(EnableBehaviorsInDesignTimeProperty)) : false;

        public static bool IsInDesignMode(DependencyObject obj)
        {
            bool isInDesignMode = ViewModelBase.IsInDesignMode;
            return (!(obj is AttachableObjectBase) ? ((obj == null) ? isInDesignMode : (isInDesignMode && (GetBehaviorInDesignMode(obj) != InteractionBehaviorInDesignMode.AsWellAsNotInDesignMode))) : (isInDesignMode && !((AttachableObjectBase) obj)._AllowAttachInDesignMode));
        }

        public static void SetBehaviorInDesignMode(DependencyObject d, InteractionBehaviorInDesignMode behavior)
        {
            d.SetValue(BehaviorInDesignModeProperty, behavior);
        }

        public static void SetEnableBehaviorsInDesignTime(DependencyObject d, bool value)
        {
            d.SetValue(EnableBehaviorsInDesignTimeProperty, value);
        }
    }
}

