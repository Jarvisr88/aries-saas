namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class OrientableLayoutPanel : Grid
    {
        public static readonly DependencyProperty OrientationProperty;
        public static readonly DependencyProperty HorizontalLayoutProperty;
        public static readonly DependencyProperty VerticalLayoutProperty;
        public static readonly DependencyProperty HRowProperty;
        public static readonly DependencyProperty HColumnProperty;
        public static readonly DependencyProperty VRowProperty;
        public static readonly DependencyProperty VColumnProperty;

        static OrientableLayoutPanel();
        public OrientableLayoutPanel();
        public static int GetHColumn(DependencyObject obj);
        public static int GetHRow(DependencyObject obj);
        public static int GetVColumn(DependencyObject obj);
        public static int GetVRow(DependencyObject obj);
        protected override void OnInitialized(EventArgs e);
        protected virtual void OnLoaded(object sender, RoutedEventArgs e);
        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved);
        public static void SetHColumn(DependencyObject obj, int value);
        public static void SetHRow(DependencyObject obj, int value);
        public static void SetVColumn(DependencyObject obj, int value);
        public static void SetVRow(DependencyObject obj, int value);
        protected virtual void Update();
        private void UpdateChildrenPosition();
        private void UpdateLayoutDefinitions();

        [EditorBrowsable(EditorBrowsableState.Never)]
        public ColumnDefinitionCollection ColumnDefinitions { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public RowDefinitionCollection RowDefinitions { get; }

        public System.Windows.Controls.Orientation Orientation { get; set; }

        public OrientableLayout HorizontalLayout { get; set; }

        public OrientableLayout VerticalLayout { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly OrientableLayoutPanel.<>c <>9;

            static <>c();
            internal void <.cctor>b__35_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__35_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__35_2(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

