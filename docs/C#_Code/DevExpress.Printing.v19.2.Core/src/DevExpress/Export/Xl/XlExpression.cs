namespace DevExpress.Export.Xl
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class XlExpression : List<XlPtgBase>
    {
        public override string ToString() => 
            this.ToString(null);

        public string ToString(IXlExpressionContext context)
        {
            XlExpressionStringBuilder builder = new XlExpressionStringBuilder {
                Context = context
            };
            return builder.BuildString(this);
        }

        internal bool Volatile { get; set; }
    }
}

