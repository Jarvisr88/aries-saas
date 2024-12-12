namespace ActiproSoftware.Win32
{
    using System;

    public interface IMouseHookCallback
    {
        void OnMouseHookEvent(MouseHookEventArgs e);
    }
}

