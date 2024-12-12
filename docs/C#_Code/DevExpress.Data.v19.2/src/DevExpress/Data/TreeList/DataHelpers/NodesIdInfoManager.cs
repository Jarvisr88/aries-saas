namespace DevExpress.Data.TreeList.DataHelpers
{
    using System;

    internal class NodesIdInfoManager
    {
        private NodesIdInfo head;

        public NodesIdInfoManager();
        public void Add(int id);
        public void BuildReversibleList();
        private void CheckUnion(NodesIdInfo prev, NodesIdInfo next);
        public int GetSumLength(int id);
        private void Union(NodesIdInfo prev, NodesIdInfo next);

        public NodesIdInfo Head { get; }
    }
}

