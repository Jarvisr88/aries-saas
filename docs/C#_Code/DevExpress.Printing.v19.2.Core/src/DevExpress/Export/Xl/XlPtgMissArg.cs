﻿namespace DevExpress.Export.Xl
{
    using System;

    public class XlPtgMissArg : XlPtgBase
    {
        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int TypeCode =>
            0x16;
    }
}

