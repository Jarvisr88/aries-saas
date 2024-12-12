namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;

    public interface IGroupValuesParser : IClientCriteriaVisitor, ICriteriaVisitor
    {
        void Accept(Action<ICheckedGroup> visit);

        bool Invalid { get; }
    }
}

