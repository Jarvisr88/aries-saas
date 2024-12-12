namespace DevExpress.Data.TreeList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public interface ITreeListNodeCollection : IEnumerable
    {
        void Add(TreeListNodeBase node);
        void AddInternal(TreeListNodeBase child);
        void Clear();
        TreeListNodeBase FindNodeById(int id);
        int IndexOf(TreeListNodeBase node);
        void Insert(int index, TreeListNodeBase node);
        void OnNodeIdChanged(TreeListNodeBase node, int oldId, int newId);
        void Remove(TreeListNodeBase node);
        void RemoveInternal(TreeListNodeBase child);
        void SortNodes(IComparer<TreeListNodeBase> comparer);
        void UpdateIndices();

        TreeListNodeBase this[int index] { get; }

        int Count { get; }

        int MinID { get; }

        int MaxID { get; }
    }
}

