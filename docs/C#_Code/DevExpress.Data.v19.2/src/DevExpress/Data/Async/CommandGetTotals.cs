namespace DevExpress.Data.Async
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CommandGetTotals : Command
    {
        public int Count;
        public List<object> TotalSummary;

        public CommandGetTotals(params DictionaryEntry[] tags);
        public override void Accept(IAsyncCommandVisitor visitor);
    }
}

