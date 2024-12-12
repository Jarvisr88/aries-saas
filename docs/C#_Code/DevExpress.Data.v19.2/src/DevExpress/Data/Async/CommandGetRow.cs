namespace DevExpress.Data.Async
{
    using System;
    using System.Collections;

    public class CommandGetRow : Command
    {
        public readonly int Index;
        public object RowKey;
        public object RowInfo;
        public object Row;

        public CommandGetRow(int index, params DictionaryEntry[] tags);
        public override void Accept(IAsyncCommandVisitor visitor);
    }
}

