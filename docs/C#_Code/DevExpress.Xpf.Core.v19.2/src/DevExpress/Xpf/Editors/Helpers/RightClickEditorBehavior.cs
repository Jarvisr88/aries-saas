namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;

    public class RightClickEditorBehavior : Behavior<BaseEdit>
    {
        public static readonly DependencyProperty IsRightClickEditorBehaviorEnabledProperty = DependencyProperty.RegisterAttached("IsRightClickEditorBehaviorEnabled", typeof(bool), typeof(RightClickEditorBehavior), new PropertyMetadata(true, new PropertyChangedCallback(RightClickEditorBehavior.OnIsRightClickEditorBehaviorEnabledChanged)));
        public static readonly DependencyProperty IsRightClickEnabledProperty = DependencyProperty.Register("IsRightClickEnabled", typeof(bool), typeof(RightClickEditorBehavior), new PropertyMetadata(true));

        public static bool GetIsRightClickEditorBehaviorEnabled(BaseEdit obj) => 
            (bool) obj.GetValue(IsRightClickEditorBehaviorEnabledProperty);

        protected override void OnAttached()
        {
            base.OnAttached();
            base.AssociatedObject.PreviewMouseRightButtonDown += new MouseButtonEventHandler(this.OnEditorPreviewMouseRightButtonDown);
        }

        protected override void OnDetaching()
        {
            base.AssociatedObject.PreviewMouseRightButtonDown -= new MouseButtonEventHandler(this.OnEditorPreviewMouseRightButtonDown);
            base.OnDetaching();
        }

        private void OnEditorPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!this.IsRightClickEnabled)
            {
                e.Handled = true;
            }
        }

        private static void OnIsRightClickEditorBehaviorEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BaseEdit editor = (BaseEdit) d;
            if ((bool) e.NewValue)
            {
                Interaction.GetBehaviors(editor).OfType<RightClickEditorBehavior>().FirstOrDefault<RightClickEditorBehavior>().Do<RightClickEditorBehavior>(x => Interaction.GetBehaviors(editor).Remove(x));
            }
            else
            {
                RightClickEditorBehavior behavior1 = new RightClickEditorBehavior();
                behavior1.IsRightClickEnabled = false;
                Interaction.GetBehaviors(editor).Add(behavior1);
            }
        }

        public static void SetIsRightClickEditorBehaviorEnabled(BaseEdit obj, bool value)
        {
            obj.SetValue(IsRightClickEditorBehaviorEnabledProperty, value);
        }

        public bool IsRightClickEnabled
        {
            get => 
                (bool) base.GetValue(IsRightClickEnabledProperty);
            set => 
                base.SetValue(IsRightClickEnabledProperty, value);
        }
    }
}

