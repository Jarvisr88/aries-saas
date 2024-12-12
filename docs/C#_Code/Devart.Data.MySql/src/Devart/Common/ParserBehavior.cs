namespace Devart.Common
{
    using System;

    [Flags]
    public enum ParserBehavior
    {
        None = 0,
        Columns = 1,
        Tables = 2,
        Where = 4,
        GroupBy = 8,
        Having = 0x10,
        OrderBy = 0x20,
        All = 0x3f
    }
}

