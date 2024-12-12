namespace DevExpress.Data.Linq.Helpers
{
    using System;

    internal class GetQueryableNotHandledEntityMessenger
    {
        public static string MessageText;

        static GetQueryableNotHandledEntityMessenger();

        public string Message { get; }
    }
}

