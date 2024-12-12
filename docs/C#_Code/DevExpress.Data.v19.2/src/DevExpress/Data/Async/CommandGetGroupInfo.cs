namespace DevExpress.Data.Async
{
    using DevExpress.Data;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CommandGetGroupInfo : Command
    {
        public ListSourceGroupInfo ParentGroup;
        public List<ListSourceGroupInfo> ChildrenGroups;

        public CommandGetGroupInfo(ListSourceGroupInfo parentGroup, params DictionaryEntry[] tags);
        public override void Accept(IAsyncCommandVisitor visitor);
    }
}

