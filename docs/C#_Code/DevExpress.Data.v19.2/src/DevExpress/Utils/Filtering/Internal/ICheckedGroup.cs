namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface ICheckedGroup
    {
        int Group { get; }

        object[] Path { get; }

        object[] Values { get; }

        bool IsInverted { get; }
    }
}

