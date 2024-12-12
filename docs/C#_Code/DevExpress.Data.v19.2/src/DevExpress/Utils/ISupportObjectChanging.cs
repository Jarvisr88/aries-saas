namespace DevExpress.Utils
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public interface ISupportObjectChanging
    {
        event CancelEventHandler Changing;
    }
}

