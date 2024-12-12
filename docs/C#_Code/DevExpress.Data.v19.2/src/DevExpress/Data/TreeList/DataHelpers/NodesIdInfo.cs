namespace DevExpress.Data.TreeList.DataHelpers
{
    using System;

    internal class NodesIdInfo
    {
        private int startId;
        private int length;
        private NodesIdInfo next;

        public NodesIdInfo();
        public NodesIdInfo(int startId, NodesIdInfo next);
        public void Add(int id);
        public bool Contains(int id);
        public NodesIdInfo FindNearestInfo(int id);
        private bool InInterval(int value, int min, int max);

        public int StartId { get; }

        public int EndId { get; }

        public int Length { get; set; }

        public NodesIdInfo Next { get; set; }
    }
}

