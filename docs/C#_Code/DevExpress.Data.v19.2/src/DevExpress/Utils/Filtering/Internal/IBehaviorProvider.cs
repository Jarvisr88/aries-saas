namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IBehaviorProvider
    {
        bool GetIsEnabled(string name);
        bool GetIsVisible(string name);
    }
}

