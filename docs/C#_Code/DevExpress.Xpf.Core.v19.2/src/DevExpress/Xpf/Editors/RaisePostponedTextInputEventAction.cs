namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;

    public class RaisePostponedTextInputEventAction : RaisePosponedEventAction<TextCompositionEventArgs>
    {
        public RaisePostponedTextInputEventAction(InplaceEditorBase editor, TextCompositionEventArgs e, Func<bool> condition = null) : base(editor, e, condition)
        {
        }

        protected override TextCompositionEventArgs CloneEventArgs(TextCompositionEventArgs posponedEventArgs) => 
            new TextCompositionEventArgs(posponedEventArgs.Device, posponedEventArgs.TextComposition);

        protected override UIElement GetElement(TextCompositionEventArgs posponedEventArgs) => 
            (UIElement) FocusHelper.GetFocusedElement();

        protected override RoutedEvent BubblingEvent =>
            UIElement.TextInputEvent;

        protected override RoutedEvent TunnelingEvent =>
            UIElement.PreviewTextInputEvent;
    }
}

