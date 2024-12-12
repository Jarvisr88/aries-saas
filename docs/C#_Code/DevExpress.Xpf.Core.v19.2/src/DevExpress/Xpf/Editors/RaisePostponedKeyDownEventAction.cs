namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class RaisePostponedKeyDownEventAction : RaisePosponedEventAction<KeyEventArgs>
    {
        public RaisePostponedKeyDownEventAction(InplaceEditorBase editor, KeyEventArgs e) : base(editor, e, null)
        {
        }

        protected override KeyEventArgs CloneEventArgs(KeyEventArgs posponedEventArgs) => 
            new KeyEventArgs(posponedEventArgs.KeyboardDevice, posponedEventArgs.InputSource, posponedEventArgs.Timestamp, BaseEditHelper.GetKey(posponedEventArgs));

        protected override UIElement GetElement(KeyEventArgs posponedEventArgs) => 
            (UIElement) Keyboard.FocusedElement;

        protected override RoutedEvent BubblingEvent =>
            UIElement.KeyDownEvent;

        protected override RoutedEvent TunnelingEvent =>
            UIElement.PreviewKeyDownEvent;
    }
}

