namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;

    public abstract class InplaceEditorOwnerBase
    {
        protected readonly FrameworkElement owner;
        private InplaceEditorBase lastMouseDownEditor;
        private InplaceEditorBase currentBeforeMouseDownEditor;
        private IBaseEdit activeEditor;

        protected InplaceEditorOwnerBase(FrameworkElement owner, bool subscribeKeyEvents = true)
        {
            this.owner = owner;
            this.EditorWasClosed = true;
            this.KeyboardLocker = new Locker();
            this.CommitEditorLocker = new Locker();
            if (subscribeKeyEvents)
            {
                owner.KeyUp += new KeyEventHandler(this.OnOwnerKeyUp);
                owner.KeyDown += new KeyEventHandler(this.OnOwnerKeyDown);
            }
        }

        private void ActivateEditorOnMouseUp(MouseButtonEventArgs e)
        {
            if (ReferenceEquals(this.CurrentCellEditor, this.lastMouseDownEditor) && ModifierKeysHelper.NoModifiers(Keyboard.Modifiers))
            {
                this.ActivateOnLeftMouse(e, false);
                this.lastMouseDownEditor = null;
            }
        }

        private void ActivateOnLeftMouse(MouseButtonEventArgs e, bool reRaiseEvent)
        {
            if ((this.CurrentCellEditor != null) && (this.IsChildOfCurrentEditor(e) && this.NeedActivateOnLeftMouseButtonEditor(e.OriginalSource as DependencyObject)))
            {
                this.CurrentCellEditor.ActivateOnLeftMouseButton(e, reRaiseEvent);
            }
        }

        protected abstract bool CommitEditing();
        protected internal virtual bool CommitEditorForce() => 
            this.CommitEditing();

        protected internal abstract void EnqueueImmediateAction(IAction action);
        protected virtual void FocusProcessMouseDown(DependencyObject originalSource)
        {
            if (!this.TopOwner.GetIsKeyboardFocusWithin() && !LayoutHelper.IsChildElementEx(this.ActiveEditor as DependencyObject, originalSource, false))
            {
                KeyboardHelper.Focus((UIElement) this.TopOwner);
            }
        }

        protected internal abstract string GetDisplayText(InplaceEditorBase inplaceEditor, string originalDisplayText, object value);
        protected internal abstract bool? GetDisplayText(InplaceEditorBase inplaceEditor, string originalDisplayText, object value, out string displayText);
        protected internal virtual bool IsActivationAction(ActivationAction action, RoutedEventArgs e, bool defaultValue) => 
            defaultValue;

        public virtual bool IsActiveEditorHaveValidationError() => 
            (this.ActiveEditor != null) ? this.ActiveEditor.HasValidationError : false;

        protected virtual bool IsChildOfCurrentEditor(DependencyObject originalSource) => 
            LayoutHelper.IsChildElement(this.CurrentCellEditor, originalSource);

        protected virtual bool IsChildOfCurrentEditor(MouseButtonEventArgs e) => 
            this.IsChildOfCurrentEditor(e.OriginalSource as DependencyObject);

        protected virtual bool IsNavigationKey(Key key) => 
            (key == Key.Up) || ((key == Key.Down) || ((key == Key.Right) || ((key == Key.Left) || ((key == Key.Tab) || ((key == Key.Home) || ((key == Key.End) || ((key == Key.Next) || ((key == Key.Prior) || ((key == Key.Escape) || ((key == Key.Next) || (key == Key.Prior)))))))))));

        public void MoveFocus(KeyEventArgs e)
        {
            MoveFocusHelper.MoveFocus(this.FocusOwner, ModifierKeysHelper.IsShiftPressed(ModifierKeysHelper.GetKeyboardModifiers(e)));
            e.Handled = true;
        }

        protected virtual bool NeedActivateOnLeftMouseButtonEditor(DependencyObject obj) => 
            true;

        protected internal virtual bool NeedsKey(Key key, ModifierKeys modifiers, bool defaultValue) => 
            defaultValue;

        protected virtual void OnActiveEditorChanged()
        {
        }

        private void OnOwnerKeyDown(object sender, KeyEventArgs e)
        {
            if (!this.KeyboardLocker.IsLocked)
            {
                this.ProcessKeyDown(e);
            }
        }

        private void OnOwnerKeyUp(object sender, KeyEventArgs e)
        {
            this.ProcessKeyUp(e);
        }

        protected abstract bool PerformNavigationOnLeftButtonDown(MouseButtonEventArgs e);
        public void ProcessIsKeyboardFocusWithinChanged()
        {
            if (LayoutHelper.IsChildElement(LayoutHelper.GetTopLevelVisual(this.FocusOwner), FocusHelper.GetFocusedElement() as DependencyObject))
            {
                if (this.FocusOwner.GetIsKeyboardFocusWithin() && !this.owner.GetIsKeyboardFocusWithin())
                {
                    KeyboardHelper.Focus((UIElement) this.owner);
                }
                bool flag = (this.ActiveEditor != null) && ((FrameworkElement) this.ActiveEditor).GetIsKeyboardFocusWithin();
                if (this.owner.GetIsKeyboardFocusWithin() && !flag)
                {
                    Action<InplaceEditorBase> action = <>c.<>9__75_0;
                    if (<>c.<>9__75_0 == null)
                    {
                        Action<InplaceEditorBase> local1 = <>c.<>9__75_0;
                        action = <>c.<>9__75_0 = editor => editor.SetKeyboardFocus();
                    }
                    this.CurrentCellEditor.Do<InplaceEditorBase>(action);
                }
            }
        }

        public abstract void ProcessKeyDown(KeyEventArgs e);
        public virtual bool ProcessKeyForLookUp(KeyEventArgs e)
        {
            Func<bool> fallback = <>c.<>9__77_1;
            if (<>c.<>9__77_1 == null)
            {
                Func<bool> local1 = <>c.<>9__77_1;
                fallback = <>c.<>9__77_1 = () => false;
            }
            return this.CurrentCellEditor.Return<InplaceEditorBase, bool>(x => x.ProcessKeyForLookUp(e), fallback);
        }

        public void ProcessKeyUp(KeyEventArgs e)
        {
            if (!this.KeyboardLocker.IsLocked && this.IsNavigationKey(e.Key))
            {
                DependencyObject originalSource = e.OriginalSource as DependencyObject;
                if (ReferenceEquals(this.TopOwner, LayoutHelper.FindLayoutOrVisualParentObject(originalSource, this.OwnerBaseType, false, null)))
                {
                    this.ShowFocusedCellEditorIfNeeded(e);
                }
            }
        }

        private void ProcessMouseButtonDown(MouseButtonEventArgs e, bool canShowEditor)
        {
            this.currentBeforeMouseDownEditor = this.CurrentCellEditor;
            if (canShowEditor)
            {
                this.EditorShowModeStrategy.OnBeforeProcessLeftButtonDown(this, e);
            }
            DependencyObject originalSource = e.OriginalSource as DependencyObject;
            if (ReferenceEquals(this.TopOwner, LayoutHelper.FindLayoutOrVisualParentObject(originalSource, this.OwnerBaseType, false, null)))
            {
                this.FocusProcessMouseDown(e.OriginalSource as DependencyObject);
                canShowEditor &= this.PerformNavigationOnLeftButtonDown(e);
                if (canShowEditor)
                {
                    this.EditorShowModeStrategy.OnAfterProcessLeftButtonDown(this, e);
                }
                if (this.IsChildOfCurrentEditor(e))
                {
                    this.lastMouseDownEditor = this.CurrentCellEditor;
                }
            }
        }

        public void ProcessMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            this.ProcessMouseButtonDown(e, true);
        }

        public void ProcessMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            this.EditorShowModeStrategy.OnProcessLeftButtonUp(this, e);
        }

        public void ProcessMouseRightButtonDown(MouseButtonEventArgs e)
        {
            this.ProcessMouseButtonDown(e, false);
        }

        public void ProcessPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            if (this.KeyboardLocker.IsLocked && LayoutHelper.IsChildElementEx(this.FocusOwner, e.NewFocus as DependencyObject, false))
            {
                e.Handled = true;
            }
            if ((this.CanCommitEditing && (!LayoutHelper.IsChildElementEx(this.FocusOwner, e.NewFocus as DependencyObject, false) && ((this.CurrentCellEditor == null) || !this.CurrentCellEditor.IsChildElementOrMessageBox(e.NewFocus)))) && ((e.NewFocus == null) || (InplaceEditorBase.CloseEditorOnLostKeyboardFocus || ((this.TopOwner == null) || ReferenceEquals(FocusManager.GetFocusScope((DependencyObject) e.NewFocus), FocusManager.GetFocusScope(this.TopOwner))))))
            {
                if (this.CurrentCellEditor != null)
                {
                    this.CurrentCellEditor.LockEditorFocus();
                }
                bool flag = (this.CurrentCellEditor != null) && this.CurrentCellEditor.IsEditorVisible;
                if (!this.CommitEditing())
                {
                    if (flag && ((this.CurrentCellEditor != null) && !this.CurrentCellEditor.IsEditorVisible))
                    {
                        this.CurrentCellEditor.Edit.SetKeyboardFocus();
                    }
                    e.Handled = true;
                }
                if (this.CurrentCellEditor != null)
                {
                    this.CurrentCellEditor.UnlockEditorFocus();
                    if (!this.CurrentCellEditor.IsEditorVisible)
                    {
                        this.EditorWasClosed = true;
                    }
                }
            }
        }

        public void ProcessStylusUpCore(DependencyObject originalSource)
        {
            if ((this.CurrentCellEditor != null) && this.IsChildOfCurrentEditor(originalSource))
            {
                this.CurrentCellEditor.ActivateOnStylusUp();
            }
            if (ReferenceEquals(this.owner, LayoutHelper.FindLayoutOrVisualParentObject(originalSource, this.OwnerBaseType, false, null)) && !this.owner.GetIsKeyboardFocusWithin())
            {
                KeyboardHelper.Focus((UIElement) this.owner);
            }
        }

        protected internal virtual bool ShouldReraiseActivationAction(ActivationAction action, RoutedEventArgs e, bool defaultValue) => 
            defaultValue;

        protected internal virtual bool ShowEditor(bool selectAll) => 
            (this.CurrentCellEditor != null) && this.CurrentCellEditor.ShowEditorInternal(selectAll);

        private void ShowFocusedCellEditorIfNeeded(KeyEventArgs e)
        {
            if (!this.EditorWasClosed && ((this.CurrentCellEditor != null) && !this.CurrentCellEditor.IsEditorVisible))
            {
                this.CurrentCellEditor.ShowEditorAndSelectAllIfNeeded(e);
            }
        }

        public virtual bool EditorWasClosed { get; set; }

        public InplaceEditorBase CurrentCellEditor { get; set; }

        public virtual FrameworkElement TopOwner =>
            this.owner;

        internal FrameworkElement OwnerElement =>
            this.owner;

        public Locker KeyboardLocker { get; private set; }

        public Locker CommitEditorLocker { get; private set; }

        private EditorShowModeStrategyBase EditorShowModeStrategy
        {
            get
            {
                switch (this.EditorShowMode)
                {
                    case DevExpress.Xpf.Core.EditorShowMode.Default:
                        return (!this.EditorSetInactiveAfterClick ? (!this.UseMouseUpFocusedEditorShowModeStrategy ? ((EditorShowModeStrategyBase) MouseDownEditorShowModeStrategy.Instance) : ((EditorShowModeStrategyBase) MouseUpFocusedEditorShowModeStrategy.Instance)) : ((EditorShowModeStrategyBase) EditorNeverShowModeStrategy.Instance));

                    case DevExpress.Xpf.Core.EditorShowMode.MouseDown:
                        return MouseDownEditorShowModeStrategy.Instance;

                    case DevExpress.Xpf.Core.EditorShowMode.MouseDownFocused:
                        return MouseDownFocusedEditorShowModeStrategy.Instance;

                    case DevExpress.Xpf.Core.EditorShowMode.MouseUp:
                        return MouseUpEditorShowModeStrategy.Instance;

                    case DevExpress.Xpf.Core.EditorShowMode.MouseUpFocused:
                        return MouseUpFocusedEditorShowModeStrategy.Instance;
                }
                throw new Exception();
            }
        }

        protected abstract FrameworkElement FocusOwner { get; }

        public IBaseEdit ActiveEditor
        {
            get => 
                this.activeEditor;
            internal set
            {
                if (!ReferenceEquals(this.activeEditor, value))
                {
                    this.activeEditor = value;
                    this.OnActiveEditorChanged();
                }
            }
        }

        protected abstract DevExpress.Xpf.Core.EditorShowMode EditorShowMode { get; }

        protected abstract bool EditorSetInactiveAfterClick { get; }

        protected abstract Type OwnerBaseType { get; }

        protected virtual bool CanCommitEditing =>
            (this.CurrentCellEditor != null) && (this.CurrentCellEditor.IsEditorVisible && this.CurrentCellEditor.Edit.IsValueChanged);

        protected virtual bool UseMouseUpFocusedEditorShowModeStrategy =>
            false;

        protected internal virtual bool CanFocusEditor =>
            this.TopOwner.GetIsKeyboardFocusWithin();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly InplaceEditorOwnerBase.<>c <>9 = new InplaceEditorOwnerBase.<>c();
            public static Action<InplaceEditorBase> <>9__75_0;
            public static Func<bool> <>9__77_1;

            internal void <ProcessIsKeyboardFocusWithinChanged>b__75_0(InplaceEditorBase editor)
            {
                editor.SetKeyboardFocus();
            }

            internal bool <ProcessKeyForLookUp>b__77_1() => 
                false;
        }

        private class EditorNeverShowModeStrategy : InplaceEditorOwnerBase.EditorShowModeStrategyBase
        {
            public static readonly InplaceEditorOwnerBase.EditorNeverShowModeStrategy Instance = new InplaceEditorOwnerBase.EditorNeverShowModeStrategy();

            private EditorNeverShowModeStrategy()
            {
            }

            public override void OnAfterProcessLeftButtonDown(InplaceEditorOwnerBase inplaceEditorOwner, MouseButtonEventArgs e)
            {
                inplaceEditorOwner.EditorWasClosed = true;
            }
        }

        private abstract class EditorShowModeStrategyBase
        {
            protected EditorShowModeStrategyBase()
            {
            }

            public virtual void OnAfterProcessLeftButtonDown(InplaceEditorOwnerBase inplaceEditorOwner, MouseButtonEventArgs e)
            {
            }

            public virtual void OnBeforeProcessLeftButtonDown(InplaceEditorOwnerBase inplaceEditorOwner, MouseButtonEventArgs e)
            {
            }

            public virtual void OnProcessLeftButtonUp(InplaceEditorOwnerBase inplaceEditorOwner, MouseButtonEventArgs e)
            {
            }
        }

        private class MouseDownEditorShowModeStrategy : InplaceEditorOwnerBase.EditorShowModeStrategyBase
        {
            public static readonly InplaceEditorOwnerBase.MouseDownEditorShowModeStrategy Instance = new InplaceEditorOwnerBase.MouseDownEditorShowModeStrategy();

            private MouseDownEditorShowModeStrategy()
            {
            }

            public override void OnAfterProcessLeftButtonDown(InplaceEditorOwnerBase inplaceEditorOwner, MouseButtonEventArgs e)
            {
                inplaceEditorOwner.ActivateOnLeftMouse(e, true);
            }
        }

        private class MouseDownFocusedEditorShowModeStrategy : InplaceEditorOwnerBase.EditorShowModeStrategyBase
        {
            public static readonly InplaceEditorOwnerBase.MouseDownFocusedEditorShowModeStrategy Instance = new InplaceEditorOwnerBase.MouseDownFocusedEditorShowModeStrategy();

            private MouseDownFocusedEditorShowModeStrategy()
            {
            }

            public override void OnAfterProcessLeftButtonDown(InplaceEditorOwnerBase inplaceEditorOwner, MouseButtonEventArgs e)
            {
                if (inplaceEditorOwner.ActiveEditor == null)
                {
                    inplaceEditorOwner.EditorWasClosed = true;
                }
            }

            public override void OnBeforeProcessLeftButtonDown(InplaceEditorOwnerBase inplaceEditorOwner, MouseButtonEventArgs e)
            {
                inplaceEditorOwner.ActivateOnLeftMouse(e, true);
            }
        }

        private class MouseUpEditorShowModeStrategy : InplaceEditorOwnerBase.EditorShowModeStrategyBase
        {
            public static readonly InplaceEditorOwnerBase.MouseUpEditorShowModeStrategy Instance = new InplaceEditorOwnerBase.MouseUpEditorShowModeStrategy();

            private MouseUpEditorShowModeStrategy()
            {
            }

            public override void OnProcessLeftButtonUp(InplaceEditorOwnerBase inplaceEditorOwner, MouseButtonEventArgs e)
            {
                inplaceEditorOwner.ActivateEditorOnMouseUp(e);
            }
        }

        private class MouseUpFocusedEditorShowModeStrategy : InplaceEditorOwnerBase.EditorShowModeStrategyBase
        {
            public static readonly InplaceEditorOwnerBase.MouseUpFocusedEditorShowModeStrategy Instance = new InplaceEditorOwnerBase.MouseUpFocusedEditorShowModeStrategy();

            private MouseUpFocusedEditorShowModeStrategy()
            {
            }

            public override void OnProcessLeftButtonUp(InplaceEditorOwnerBase inplaceEditorOwner, MouseButtonEventArgs e)
            {
                if (ReferenceEquals(inplaceEditorOwner.CurrentCellEditor, inplaceEditorOwner.currentBeforeMouseDownEditor))
                {
                    inplaceEditorOwner.ActivateEditorOnMouseUp(e);
                }
                else
                {
                    inplaceEditorOwner.EditorWasClosed = true;
                }
            }
        }
    }
}

