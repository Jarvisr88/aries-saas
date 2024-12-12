namespace DevExpress.Data.Helpers
{
    using System;

    public class ServerModeOptimalFetchParam
    {
        public readonly ServerModeServerAndChannelModel Model;
        public readonly int Index;
        public readonly int MinIndex;
        public readonly int MaxIndex;
        public readonly int TotalCount;
        public readonly int BaseCountTakeData;
        public readonly int MaxAllowedTake;
        public readonly double FillTimeMultiplier;
        public readonly double EdgeTimeMultiplier;
        public readonly double MiddleTimeMultiplier;

        public ServerModeOptimalFetchParam(ServerModeServerAndChannelModel model, int index, int minIndex, int maxIndex, int totalCount, int baseCountTakeData, int maxAllowedTake, double fillTimeMultiplier, double edgeTimeMultiplier, double middleTimeMultiplier);
        public int ScanFromBottom(int lastIndexToFetch);
        public int ScanFromTop(int lastIndexToFetch);
        public int SkipFromBottom(int firstIndexToFetch);
        public int SkipFromTop(int firstIndexToFetch);
        public override string ToString();
    }
}

