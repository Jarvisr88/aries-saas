namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class RotatableLayoutPanel : Grid
    {
        public static readonly DependencyProperty LocationProperty;
        public static readonly DependencyProperty LayoutPositionProperty;
        private ObservableCollection<RowDefinition> layoutDefinitions;

        static RotatableLayoutPanel();
        public RotatableLayoutPanel();
        public static int GetLayoutPosition(DependencyObject obj);
        protected override void OnInitialized(EventArgs e);
        protected virtual void OnLoaded(object sender, RoutedEventArgs e);
        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved);
        public static void SetLayoutPosition(DependencyObject obj, int value);
        private static IList<ColumnDefinition> TransformRowDefinitions(IList<RowDefinition> rows);
        protected virtual void Update();
        private void UpdateChildrenPosition();
        private void UpdateLayoutDefinitions();

        public Dock Location { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public ColumnDefinitionCollection ColumnDefinitions { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public RowDefinitionCollection RowDefinitions { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<RowDefinition> LayoutDefinitions { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RotatableLayoutPanel.<>c <>9;
            public static Func<UIElement, int> <>9__20_0;

            static <>c();
            internal void <.cctor>b__22_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal int <UpdateChildrenPosition>b__20_0(UIElement x);
        }
    }
}

