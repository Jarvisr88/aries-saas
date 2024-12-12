namespace DevExpress.Data.Async
{
    using System;
    using System.Collections;

    public class CommandGetAllFilteredAndSortedRows : Command
    {
        public IList RowsInfo;
        public IList Rows;

        public CommandGetAllFilteredAndSortedRows(params DictionaryEntry[] tags);
        public override void Accept(IAsyncCommandVisitor visitor);
    }
}

