namespace DevExpress.Utils.Design
{
    using System;

    public interface ISmartTagActionWithOptions : ISmartTagAction
    {
        object[] Actions { get; }
    }
}

