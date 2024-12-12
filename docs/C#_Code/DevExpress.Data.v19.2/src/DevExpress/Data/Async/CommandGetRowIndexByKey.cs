namespace DevExpress.Data.Async
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CommandGetRowIndexByKey : Command
    {
        public object Key;
        public int Index;
        public List<CommandGetGroupInfo> Groups;

        public CommandGetRowIndexByKey(object key, params DictionaryEntry[] tags);
        public override void Accept(IAsyncCommandVisitor visitor);
    }
}

