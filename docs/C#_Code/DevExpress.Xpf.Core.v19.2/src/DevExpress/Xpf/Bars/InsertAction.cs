namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Markup;

    [ContentProperty("Content")]
    public class InsertAction : CollectionAction
    {
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static readonly DependencyProperty ContentProperty;
        public static readonly DependencyProperty InsertAfterProperty;

        static InsertAction();
        public InsertAction();
        public static object GetInsertAfter(DependencyObject element);
        protected virtual void OnContentChanged(DependencyPropertyChangedEventArgs e);
        protected virtual void OnElementChanged(DependencyPropertyChangedEventArgs e);
        public static void SetInsertAfter(DependencyObject element, object value);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public CollectionActionKind Kind { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public object Content { get; set; }

        public object InsertAfter { get; set; }

        protected override IEnumerator LogicalChildren { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly InsertAction.<>c <>9;

            static <>c();
            internal void <.cctor>b__2_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__2_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

