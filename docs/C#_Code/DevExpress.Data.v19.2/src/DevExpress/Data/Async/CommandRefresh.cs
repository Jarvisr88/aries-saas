namespace DevExpress.Data.Async
{
    using System;
    using System.Collections;

    public class CommandRefresh : Command
    {
        public CommandRefresh(params DictionaryEntry[] tags);
        public override void Accept(IAsyncCommandVisitor visitor);
    }
}

