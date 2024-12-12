namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Mvvm.UI.Interactivity;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [TargetType(typeof(UserControl)), TargetType(typeof(Window))]
    public class DXMessageBoxService : ServiceBase, IMessageBoxService
    {
        public static readonly DependencyProperty SetMessageBoxOwnerProperty = DependencyProperty.Register("SetMessageBoxOwner", typeof(bool), typeof(DXMessageBoxService), new PropertyMetadata(true));

        MessageResult IMessageBoxService.Show(string messageBoxText, string caption, MessageButton button, MessageIcon icon, MessageResult defaultResult) => 
            !CompatibilitySettings.UseThemedMessageBoxInServices ? this.ShowDXMessageBox(messageBoxText, caption, button, icon, defaultResult) : this.ShowThemedMessageBox(messageBoxText, caption, button, icon, defaultResult);

        private MessageResult ShowDXMessageBox(string messageBoxText, string caption, MessageButton button, MessageIcon icon, MessageResult defaultResult)
        {
            FrameworkElement owner = null;
            if (this.SetMessageBoxOwner)
            {
                Func<FrameworkElement, Window> evaluator = <>c.<>9__5_0;
                if (<>c.<>9__5_0 == null)
                {
                    Func<FrameworkElement, Window> local1 = <>c.<>9__5_0;
                    evaluator = <>c.<>9__5_0 = x => Window.GetWindow(x);
                }
                Window local2 = base.AssociatedObject.With<FrameworkElement, Window>(evaluator);
                Window associatedObject = local2;
                if (local2 == null)
                {
                    Window local3 = local2;
                    associatedObject = base.AssociatedObject;
                }
                owner = associatedObject;
            }
            return DXMessageBox.Show(owner, messageBoxText, caption, button.ToMessageBoxButton(), icon.ToMessageBoxImage(), defaultResult.ToMessageBoxResult()).ToMessageResult();
        }

        private MessageResult ShowThemedMessageBox(string messageBoxText, string caption, MessageButton button, MessageIcon icon, MessageResult defaultResult)
        {
            Window owner = null;
            if (this.SetMessageBoxOwner && (base.AssociatedObject != null))
            {
                owner = (base.AssociatedObject as Window) ?? Window.GetWindow(base.AssociatedObject);
            }
            MessageBoxImage image = icon.ToMessageBoxImage();
            bool? showActivated = null;
            return ThemedMessageBox.Show(owner, caption, messageBoxText, button.ToMessageBoxButton(), new MessageBoxResult?(defaultResult.ToMessageBoxResult()), image, false, MessageBoxOptions.None, WindowStartupLocation.CenterOwner, WindowTitleAlignment.Left, showActivated).ToMessageResult();
        }

        public bool SetMessageBoxOwner
        {
            get => 
                (bool) base.GetValue(SetMessageBoxOwnerProperty);
            set => 
                base.SetValue(SetMessageBoxOwnerProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXMessageBoxService.<>c <>9 = new DXMessageBoxService.<>c();
            public static Func<FrameworkElement, Window> <>9__5_0;

            internal Window <ShowDXMessageBox>b__5_0(FrameworkElement x) => 
                Window.GetWindow(x);
        }
    }
}

