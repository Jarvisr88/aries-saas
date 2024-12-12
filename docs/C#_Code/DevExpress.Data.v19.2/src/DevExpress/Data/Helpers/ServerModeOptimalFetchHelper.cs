namespace DevExpress.Data.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public static class ServerModeOptimalFetchHelper
    {
        public static ServerModeOptimalFetchResult CalculateOptimalFetchResult(ServerModeOptimalFetchParam resParam);
        private static int? CalculateTakeFromFixedTimeAndScan(ServerModeServerAndChannelModel model, double targetTime, int scan);
        private static int? CalculateTakeFromFixedTimeAndSkip(ServerModeServerAndChannelModel model, double targetTime, int skip);
        private static InvalidOperationException CreateGeneralException(ServerModeOptimalFetchParam resParam, Exception e);
        private static ServerModeOptimalFetchHelper.Solution OptimalResultCore(ServerModeOptimalFetchParam p);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ServerModeOptimalFetchHelper.<>c <>9;
            public static Func<double, double, double> <>9__0_0;

            static <>c();
            internal double <CalculateOptimalFetchResult>b__0_0(double x, double y);
        }

        private class Solution
        {
            public readonly int Skip;
            public readonly int Take;
            public readonly bool IsFromEnd;
            private readonly ServerModeOptimalFetchParam Params;

            private Solution(int skip, int take, bool isFromEnd, ServerModeOptimalFetchParam _params);
            public static ServerModeOptimalFetchHelper.Solution FromI1I2(ServerModeOptimalFetchParam _params, bool isFromEnd, int i1, int i2);
            public static ServerModeOptimalFetchHelper.Solution FromSkipTake(ServerModeOptimalFetchParam _params, bool isFromEnd, int skip, int take);

            public int Scan { get; }

            public double Time { get; }
        }
    }
}

