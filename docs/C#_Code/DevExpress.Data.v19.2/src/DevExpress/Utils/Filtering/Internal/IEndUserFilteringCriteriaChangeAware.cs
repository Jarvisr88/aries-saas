namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IEndUserFilteringCriteriaChangeAware
    {
        IDisposable EnterFilterCriteriaChange();
        void QueueFilterCriteriaChange(string path, Action<string> change);
    }
}

