namespace ActiproSoftware.ComponentModel
{
    using System;

    public interface IWeakEventListener
    {
        bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e);
    }
}

