namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public interface IScreen
    {
        event Action WorkingAreaChanged;

        Rect GetWorkingArea(Point point);
    }
}

