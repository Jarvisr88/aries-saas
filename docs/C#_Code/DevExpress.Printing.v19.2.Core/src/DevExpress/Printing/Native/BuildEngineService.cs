namespace DevExpress.Printing.Native
{
    using System;
    using System.Runtime.CompilerServices;

    internal class BuildEngineService : IBuildEngineService
    {
        public const int IdDefault = 0;
        public const int IdExport = 1;

        public int BuildEngineID { get; set; }
    }
}

