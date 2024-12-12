namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class EditorMarginCorrector : DependencyObject
    {
        public static readonly DependencyProperty ActualMarginProperty;
        public static readonly DependencyProperty CorrectorProperty;
        public static readonly DependencyProperty ErrorMarginProperty;
        public static readonly DependencyProperty MarginProperty;
        private static readonly DependencyPropertyKey ActualMarginPropertyKey;
        private DevExpress.Xpf.Editors.EditMode editMode;
        private BaseEdit editor;
        private bool hasValidationError;
        private bool showBorder;
        private bool showError;
        private WeakReference target;

        static EditorMarginCorrector()
        {
            ActualMarginPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualMargin", typeof(Thickness), typeof(EditorMarginCorrector), new PropertyMetadata((d, e) => ((EditorMarginCorrector) d).PropertyChangedActualMargin()));
            ActualMarginProperty = ActualMarginPropertyKey.DependencyProperty;
            CorrectorProperty = DependencyPropertyManager.RegisterAttached("Corrector", typeof(EditorMarginCorrector), typeof(EditorMarginCorrector), new PropertyMetadata(new PropertyChangedCallback(EditorMarginCorrector.PropertyChangedCorrector)));
            ErrorMarginProperty = DependencyPropertyManager.Register("ErrorMargin", typeof(Thickness), typeof(EditorMarginCorrector), new PropertyMetadata((d, e) => ((EditorMarginCorrector) d).UpdateActualMargin()));
            MarginProperty = DependencyPropertyManager.Register("Margin", typeof(Thickness), typeof(EditorMarginCorrector), new PropertyMetadata((d, e) => ((EditorMarginCorrector) d).UpdateActualMargin()));
        }

        public EditorMarginCorrector()
        {
            this.UpdateActualMargin();
        }

        public static EditorMarginCorrector GetCorrector(DependencyObject obj) => 
            (EditorMarginCorrector) obj.GetValue(CorrectorProperty);

        private static void InitializeCorrector(BaseEdit editor, EditorMarginCorrector corrector)
        {
            corrector.editMode = editor.EditMode;
            corrector.hasValidationError = editor.HasValidationError;
            corrector.showBorder = editor.ShowBorder;
            corrector.showError = editor.ShowError;
            corrector.Editor = editor;
            editor.MarginCorrector = corrector;
        }

        private void PropertyChangedActualMargin()
        {
            if (this.Target != null)
            {
                this.UpdateTargetMargin();
            }
        }

        private static void PropertyChangedCorrector(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EditorMarginCorrector newValue = (EditorMarginCorrector) e.NewValue;
            BaseEdit editor = TryFindEditor((FrameworkElement) d);
            if (editor != null)
            {
                InitializeCorrector(editor, newValue);
            }
            if (d is FrameworkElement)
            {
                newValue.Target = d as FrameworkElement;
                newValue.UpdateTargetMargin();
                (d as FrameworkElement).Loaded += new RoutedEventHandler(EditorMarginCorrector.TargetLoaded);
            }
        }

        public static void SetCorrector(DependencyObject obj, EditorMarginCorrector value)
        {
            obj.SetValue(CorrectorProperty, value);
        }

        private static void TargetLoaded(object sender, RoutedEventArgs e)
        {
            FrameworkElement target = (FrameworkElement) sender;
            target.Loaded -= new RoutedEventHandler(EditorMarginCorrector.TargetLoaded);
            BaseEdit editor = TryFindEditor(target);
            EditorMarginCorrector corrector = GetCorrector(target);
            if (editor != null)
            {
                InitializeCorrector(editor, corrector);
            }
            else
            {
                corrector.UpdateTargetMargin();
            }
        }

        private static BaseEdit TryFindEditor(FrameworkElement target) => 
            (target.DataContext is BaseEdit) ? ((BaseEdit) target.DataContext) : LayoutHelper.FindParentObject<BaseEdit>(target);

        private void UpdateActualMargin()
        {
            if (this.Editor == null)
            {
                this.ActualMargin = this.Margin;
            }
            else if (this.HasValidationError && (this.ShowError || (this.EditMode != DevExpress.Xpf.Editors.EditMode.Standalone)))
            {
                this.ActualMargin = this.ErrorMargin;
            }
            else
            {
                this.ActualMargin = this.Margin;
            }
        }

        private void UpdateTargetMargin()
        {
            if (this.Target.Margin != this.ActualMargin)
            {
                this.Target.Margin = this.ActualMargin;
            }
        }

        public Thickness ActualMargin
        {
            get => 
                (Thickness) base.GetValue(ActualMarginProperty);
            private set => 
                base.SetValue(ActualMarginPropertyKey, value);
        }

        public Thickness ErrorMargin
        {
            get => 
                (Thickness) base.GetValue(ErrorMarginProperty);
            set => 
                base.SetValue(ErrorMarginProperty, value);
        }

        public Thickness Margin
        {
            get => 
                (Thickness) base.GetValue(MarginProperty);
            set => 
                base.SetValue(MarginProperty, value);
        }

        internal DevExpress.Xpf.Editors.EditMode EditMode
        {
            get => 
                this.editMode;
            set
            {
                if (value != this.editMode)
                {
                    this.editMode = value;
                    this.UpdateActualMargin();
                }
            }
        }

        private BaseEdit Editor
        {
            get => 
                this.editor;
            set
            {
                if (!ReferenceEquals(value, this.editor))
                {
                    this.editor = value;
                    this.UpdateActualMargin();
                }
            }
        }

        internal bool HasValidationError
        {
            get => 
                this.hasValidationError;
            set
            {
                if (value != this.hasValidationError)
                {
                    this.hasValidationError = value;
                    this.UpdateActualMargin();
                }
            }
        }

        internal bool ShowBorder
        {
            get => 
                this.showBorder;
            set
            {
                if (value != this.showBorder)
                {
                    this.showBorder = value;
                    this.UpdateActualMargin();
                }
            }
        }

        internal bool ShowError
        {
            get => 
                this.showError;
            set
            {
                if (value != this.showError)
                {
                    this.showError = value;
                    this.UpdateActualMargin();
                }
            }
        }

        private FrameworkElement Target
        {
            get => 
                ((this.target == null) || !this.target.IsAlive) ? null : ((FrameworkElement) this.target.Target);
            set => 
                this.target = (value != null) ? new WeakReference(value) : null;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EditorMarginCorrector.<>c <>9 = new EditorMarginCorrector.<>c();

            internal void <.cctor>b__5_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((EditorMarginCorrector) d).PropertyChangedActualMargin();
            }

            internal void <.cctor>b__5_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((EditorMarginCorrector) d).UpdateActualMargin();
            }

            internal void <.cctor>b__5_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((EditorMarginCorrector) d).UpdateActualMargin();
            }
        }
    }
}

