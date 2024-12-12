namespace DevExpress.Data.Helpers
{
    using DevExpress.Data.Filtering;
    using System;

    public static class ServerModeCommonParameters
    {
        private static int? complexGroupingOperationTimeout;
        private static int? complexGroupingOperationEtcTimeout;
        private static bool? useEtcTimeoutForGroupingOperation;

        public static BinaryOperator FixServerModeEtcValue(BinaryOperator theOperator);

        public static int ComplexGroupingOperationTimeout { get; set; }

        public static TimeSpan ComplexGroupingOperationTimeoutTimeSpan { get; }

        public static int ComplexGroupingOperationEtcTimeout { get; set; }

        public static TimeSpan ComplexGroupingOperationEtcTimeoutTimeSpan { get; }

        public static bool UseEtcTimeoutForGroupingOperation { get; set; }
    }
}

