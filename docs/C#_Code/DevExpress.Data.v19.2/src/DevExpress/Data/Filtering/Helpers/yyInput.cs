namespace DevExpress.Data.Filtering.Helpers
{
    using System;

    internal interface yyInput
    {
        bool advance();
        int token();
        object value();
    }
}

