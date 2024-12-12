namespace DevExpress.Export.Xl
{
    using System;

    internal class XlFunctionInfo
    {
        private readonly string name;
        private readonly int minArgumentsCount;
        private readonly int maxArgumentsCount;
        private readonly XlFunctionProperty properties;

        public XlFunctionInfo(string name, int minArgumentsCount, int maxArgumentsCount, XlFunctionProperty properties)
        {
            this.maxArgumentsCount = maxArgumentsCount;
            this.minArgumentsCount = minArgumentsCount;
            this.name = name;
            this.properties = properties;
        }

        public bool IsExcel2003FutureFunction() => 
            this.properties != XlFunctionProperty.Normal;

        public bool IsExcel2010FutureFunction() => 
            this.properties == XlFunctionProperty.Excel2010Future;

        public bool IsExcel2010PredefinedFunction() => 
            (this.properties == XlFunctionProperty.Excel2010Predefined) || (this.properties == XlFunctionProperty.Excel2003Future);

        public bool IsExcel2010PredefinedNonFutureFunction() => 
            this.properties == XlFunctionProperty.Excel2010Predefined;

        public bool HasFixedArgumentsCount =>
            this.minArgumentsCount == this.maxArgumentsCount;

        public string Name =>
            this.name;

        public int MinArgumentsCount =>
            this.minArgumentsCount;

        public int MaxArgumentsCount =>
            this.maxArgumentsCount;

        public XlFunctionProperty Properties =>
            this.properties;
    }
}

