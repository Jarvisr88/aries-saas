namespace DevExpress.Data.Linq.Helpers
{
    using System;

    public static class SummaryWorkaroundForMS362794
    {
        public const int MAX_Size = 0x42;
        private static readonly Type[] TypesArray;

        static SummaryWorkaroundForMS362794();
        public static bool CanWorkaround(int genericArgsCount);
        public static Type MakeWorkaroundType(Type[] genericArgs);
    }
}

