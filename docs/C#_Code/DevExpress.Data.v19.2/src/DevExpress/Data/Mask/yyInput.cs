namespace DevExpress.Data.Mask
{
    using System;

    internal interface yyInput
    {
        bool advance();
        int token();
        object value();
    }
}

