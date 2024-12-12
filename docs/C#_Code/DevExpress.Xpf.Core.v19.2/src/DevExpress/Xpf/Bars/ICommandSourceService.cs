namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Windows.Input;

    public interface ICommandSourceService
    {
        void CommandChanged(ICommandSourceServiceSupport element, ICommand oldValue, ICommand newValue);
        void KeyGestureChanged(ICommandSourceServiceSupport element, KeyGesture oldValue, KeyGesture newValue);
    }
}

