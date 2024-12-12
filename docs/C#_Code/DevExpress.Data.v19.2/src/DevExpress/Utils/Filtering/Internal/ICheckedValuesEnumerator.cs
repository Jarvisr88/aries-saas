namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface ICheckedValuesEnumerator : IEnumerator<object>, IDisposable, IEnumerator
    {
        int Group { get; }
    }
}

