namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public interface IBarLayoutTableInfo
    {
        event DependencyPropertyChangedEventHandler IsVisibleChanged;

        event EventHandler LayoutPropertyChanged;

        void Arrange(Rect finalRect);
        bool CanDock(Dock dock);
        void InvalidateMeasure();
        bool MakeFloating();
        void Measure(Size constraint);

        DevExpress.Xpf.Bars.Bar Bar { get; }

        bool UseWholeRow { get; }

        double Opacity { get; set; }

        int Row { get; set; }

        int Column { get; set; }

        int CollectionIndex { get; }

        bool CanReduce { get; }

        double Offset { get; set; }

        Size DesiredSize { get; }

        Size RenderSize { get; }

        bool IsVisible { get; }
    }
}

