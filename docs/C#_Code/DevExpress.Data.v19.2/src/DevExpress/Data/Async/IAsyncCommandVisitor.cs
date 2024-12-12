namespace DevExpress.Data.Async
{
    using System;

    public interface IAsyncCommandVisitor
    {
        void Canceled(Command command);
        void Visit(CommandApply command);
        void Visit(CommandFindIncremental command);
        void Visit(CommandGetAllFilteredAndSortedRows command);
        void Visit(CommandGetGroupInfo command);
        void Visit(CommandGetRow command);
        void Visit(CommandGetRowIndexByKey command);
        void Visit(CommandGetTotals command);
        void Visit(CommandGetUniqueColumnValues command);
        void Visit(CommandLocateByValue command);
        void Visit(CommandPrefetchRows command);
        void Visit(CommandRefresh command);
    }
}

