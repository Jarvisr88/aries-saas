namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;

    internal class EditorFocusManagement
    {
        public EditorFocusManagement(BaseEdit editor)
        {
            this.Editor = editor;
            Keyboard.AddGotKeyboardFocusHandler(this.Editor, new KeyboardFocusChangedEventHandler(this.GotFocus));
            Keyboard.AddLostKeyboardFocusHandler(this.Editor, new KeyboardFocusChangedEventHandler(this.LostFocus));
            Keyboard.AddPreviewLostKeyboardFocusHandler(this.Editor, new KeyboardFocusChangedEventHandler(this.PreviewLostFocus));
            this.Editor.Loaded += new RoutedEventHandler(this.Editor_Loaded);
            this.SetFocusAction = new PostponedAction(() => !this.Editor.IsLoaded);
        }

        private void Editor_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetFocusAction.Perform();
        }

        private void GotFocus(object sender, KeyboardFocusChangedEventArgs args)
        {
            if (LayoutHelper.IsChildElement(this.Editor, args.NewFocus as DependencyObject) && !this.IsFocusWithin)
            {
                this.IsFocusWithin = true;
                this.Editor.EditStrategy.OnGotFocus();
                this.Editor.EditStrategy.AfterOnGotFocus();
            }
        }

        protected internal virtual bool IsChildElement(DependencyObject element, DependencyObject root = null) => 
            LayoutHelper.IsChildElementEx(root ?? this.Editor, element, true);

        private void LostFocus(object sender, KeyboardFocusChangedEventArgs args)
        {
            if ((this.Editor.EditMode == EditMode.Standalone) && (!this.Editor.IsLoaded && !this.Editor.IsChildElement(args.NewFocus as DependencyObject, null)))
            {
                this.SetFocusAction.PerformPostpone(() => this.Editor.Focus());
            }
            else if ((args.NewFocus != null) && !this.Editor.IsChildElement(args.NewFocus as DependencyObject, null))
            {
                this.IsFocusWithin = false;
                this.Editor.EditStrategy.OnLostFocus();
            }
        }

        private void PreviewLostFocus(object sender, KeyboardFocusChangedEventArgs args)
        {
            this.Editor.EditStrategy.OnPreviewLostFocus((DependencyObject) args.OldFocus, (DependencyObject) args.NewFocus);
            args.Handled = this.Editor.HandlePreviewLostKeyboardFocus((DependencyObject) args.OldFocus, (DependencyObject) args.NewFocus);
        }

        private BaseEdit Editor { get; set; }

        public bool IsFocusWithin { get; private set; }

        private PostponedAction SetFocusAction { get; set; }
    }
}

