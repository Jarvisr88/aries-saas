namespace DevExpress.Data.PLinq.Helpers
{
    using System;
    using System.Collections;

    public interface IPLinqServerModeFrontEndOwner
    {
        bool IsReadyForTakeOff();

        Type ElementType { get; }

        IEnumerable Source { get; }

        string DefaultSorting { get; }

        int? DegreeOfParallelism { get; }
    }
}

