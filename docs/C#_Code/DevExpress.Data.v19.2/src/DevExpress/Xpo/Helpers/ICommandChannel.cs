namespace DevExpress.Xpo.Helpers
{
    using System;

    public interface ICommandChannel
    {
        object Do(string command, object args);
    }
}

