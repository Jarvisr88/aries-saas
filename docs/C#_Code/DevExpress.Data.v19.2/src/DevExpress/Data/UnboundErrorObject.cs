namespace DevExpress.Data
{
    using System;

    public class UnboundErrorObject
    {
        private static string displayText;
        public static readonly UnboundErrorObject Value;

        static UnboundErrorObject();
        private UnboundErrorObject();
        public override string ToString();

        public static string DisplayText { get; set; }
    }
}

