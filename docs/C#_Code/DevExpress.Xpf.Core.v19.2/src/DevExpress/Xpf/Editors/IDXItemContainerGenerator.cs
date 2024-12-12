namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;

    public interface IDXItemContainerGenerator
    {
        void ClearItemContainer(int index, UIElement container);
        UIElement Generate(int index, out bool isNew);
        UIElement GetContainer(int index);
        int GetItemsCount();
        void PrepareItemContainer(int index, UIElement container);
        void RemoveItems();
        void StartAt(int index);
        void StartManipulation();
        void Stop();
        void StopManipulation();
    }
}

