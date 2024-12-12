namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public interface IManipulationClient
    {
        IInputElement GetManipulationContainer();
        Vector GetMaxScrollValue();
        Vector GetMinScrollValue();
        Vector GetScrollValue();
        void Scroll(Vector scrollValue);
    }
}

