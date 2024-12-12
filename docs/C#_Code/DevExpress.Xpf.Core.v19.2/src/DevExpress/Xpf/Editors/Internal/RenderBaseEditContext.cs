namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Validation;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class RenderBaseEditContext : RenderControlBaseContext
    {
        public event EventHandler<EditValueChangedEventArgs> EditValueChanged;

        public RenderBaseEditContext(RenderBaseEdit factory) : base(factory)
        {
        }

        protected internal override void AttachToVisualTree(FrameworkElement root)
        {
            base.AttachToVisualTree(root);
            this.Editor.EditValueChanged += new EditValueChangedEventHandler(this.EditorValueChanged);
            if (root.IsKeyboardFocused && ((IBaseEdit) root).CanAcceptFocus)
            {
                this.Editor.Focus();
            }
        }

        protected internal override void DetachFromVisualTree(FrameworkElement root)
        {
            if (this.Editor.IsKeyboardFocusWithin && ((IBaseEdit) root).CanAcceptFocus)
            {
                root.Focus();
            }
            this.Editor.SetSettings(null);
            base.DetachFromVisualTree(root);
            this.Editor.EditValueChanged -= new EditValueChangedEventHandler(this.EditorValueChanged);
        }

        public bool DoValidate()
        {
            Func<BaseEdit, bool> evaluator = <>c.<>9__49_0;
            if (<>c.<>9__49_0 == null)
            {
                Func<BaseEdit, bool> local1 = <>c.<>9__49_0;
                evaluator = <>c.<>9__49_0 = x => x.DoValidate();
            }
            return this.Editor.Return<BaseEdit, bool>(evaluator, (<>c.<>9__49_1 ??= () => true));
        }

        private void EditorValueChanged(object sender, EditValueChangedEventArgs args)
        {
            this.RaiseEditValueChanged(args);
        }

        public void FlushPendingEditActions()
        {
            BaseEditHelper.FlushPendingEditActions(this.Editor);
        }

        public bool NeedsKey(Key key, ModifierKeys modifiers) => 
            this.Editor.NeedsKey(key, modifiers);

        protected override void OnFlowDirectionChanged(FlowDirection oldValue, FlowDirection newValue)
        {
            base.OnFlowDirectionChanged(oldValue, newValue);
            if (this.Control != null)
            {
                this.Control.FlowDirection = newValue;
            }
        }

        public void ProcessActivatingKey(Key key, ModifierKeys modifiers)
        {
            this.Editor.ProcessActivatingKey(key, modifiers);
        }

        private void RaiseEditValueChanged(EditValueChangedEventArgs args)
        {
            if (this.EditValueChanged != null)
            {
                this.EditValueChanged(this, args);
            }
        }

        public void SelectAll()
        {
            this.Editor.SelectAll();
        }

        public void SetDisplayTextProvider(IDisplayTextProvider provider)
        {
            this.Editor.SetDisplayTextProvider(provider);
        }

        public void SetInplaceEditingProvider(IInplaceEditingProvider provider)
        {
            ((IBaseEdit) this.Editor).SetInplaceEditingProvider(provider);
        }

        private BaseEdit Editor =>
            (BaseEdit) this.Control;

        public bool IsEditorActive =>
            this.Editor.IsEditorActive;

        public object EditValue
        {
            get => 
                this.Editor.EditValue;
            set => 
                this.Editor.EditValue = value;
        }

        public bool IsValueChanged
        {
            get => 
                this.Editor.IsValueChanged;
            set => 
                this.Editor.IsValueChanged = value;
        }

        public BaseValidationError ValidationError
        {
            get => 
                this.Editor.ValidationError;
            set => 
                this.Editor.ValidationError = value;
        }

        public bool IsReadOnly
        {
            get => 
                this.Editor.IsReadOnly;
            set => 
                this.Editor.IsReadOnly = value;
        }

        public bool ShowBorderInInplaceMode
        {
            get => 
                this.Editor.ShowBorderInInplaceMode;
            set => 
                this.Editor.ShowBorderInInplaceMode = value;
        }

        public ControlTemplate EditTemplate
        {
            get => 
                this.Editor.EditTemplate;
            set => 
                this.Editor.EditTemplate = value;
        }

        public ControlTemplate DisplayTemplate
        {
            get => 
                this.Editor.DisplayTemplate;
            set => 
                this.Editor.DisplayTemplate = value;
        }

        public object RealDataContext
        {
            get => 
                this.Editor.DataContext;
            set => 
                this.Editor.DataContext = value;
        }

        public System.Windows.Style Style
        {
            get => 
                this.Editor.Style;
            set => 
                this.Editor.Style = value;
        }

        public DevExpress.Xpf.Editors.EditMode EditMode
        {
            get => 
                this.Editor.EditMode;
            set => 
                this.Editor.EditMode = value;
        }

        public DevExpress.Xpf.Editors.Validation.InvalidValueBehavior InvalidValueBehavior
        {
            get => 
                this.Editor.InvalidValueBehavior;
            set => 
                this.Editor.InvalidValueBehavior = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RenderBaseEditContext.<>c <>9 = new RenderBaseEditContext.<>c();
            public static Func<BaseEdit, bool> <>9__49_0;
            public static Func<bool> <>9__49_1;

            internal bool <DoValidate>b__49_0(BaseEdit x) => 
                x.DoValidate();

            internal bool <DoValidate>b__49_1() => 
                true;
        }
    }
}

