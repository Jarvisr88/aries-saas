namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    public abstract class BrushEditingWrapper
    {
        private FrameworkElement contentElement;
        private System.Windows.Media.Brush brush;

        protected BrushEditingWrapper(IBrushPicker editor)
        {
            this.Picker = editor;
        }

        public static BrushEditingWrapper Create(IBrushPicker picker, BrushType brushType) => 
            (brushType != BrushType.None) ? ((brushType != BrushType.SolidColorBrush) ? null : ((BrushEditingWrapper) new SolidColorBrushWrapper(picker))) : ((BrushEditingWrapper) new NoneBrushWrapper(picker));

        protected virtual FrameworkElement FindContentElement() => 
            LayoutHelper.FindElementByName((FrameworkElement) this.Picker, "PART_Content");

        public void FocusPickerIfNeeded()
        {
            if (this.Picker.HasFocus)
            {
                FrameworkElement contentElement = this.ContentElement;
                if (contentElement != null)
                {
                    contentElement.Focus();
                }
            }
        }

        public abstract System.Windows.Media.Brush GetBrush(object baseValue);
        public virtual void GotFocus(RoutedEventArgs e)
        {
            FrameworkElement contentElement = this.ContentElement;
            if ((contentElement != null) && !LayoutHelper.IsChildElementEx(contentElement, (DependencyObject) e.OriginalSource, false))
            {
                contentElement.Focus();
            }
        }

        public virtual void LostFocus(RoutedEventArgs e)
        {
        }

        public virtual bool NeedsKey(Key key, ModifierKeys modifiers) => 
            false;

        public virtual void SetBrush(object editValue)
        {
            this.brush = this.GetBrush(editValue);
        }

        public abstract void Subscribe();
        public virtual void Sync()
        {
        }

        public abstract void Unsubscribe();

        protected FrameworkElement ContentElement
        {
            get
            {
                FrameworkElement contentElement = this.contentElement;
                if (this.contentElement == null)
                {
                    FrameworkElement local1 = this.contentElement;
                    contentElement = this.contentElement = this.FindContentElement();
                }
                return contentElement;
            }
        }

        protected IBrushPicker Picker { get; private set; }

        public virtual System.Windows.Media.Brush Brush =>
            this.brush;
    }
}

