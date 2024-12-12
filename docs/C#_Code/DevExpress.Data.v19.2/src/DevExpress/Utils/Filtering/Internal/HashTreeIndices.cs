namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal abstract class HashTreeIndices : IHashTree, IHashTreeIndices
    {
        private const int DEFAULT_CAPACITY = 0x7f;
        protected const int EXPANDED = 1;
        protected const int EXPANDED_BY_FILTER = 2;
        private readonly IDictionary<int, int> keyToIndicesMap;
        private readonly IDictionary<int, int> keyToParentsMap;
        private Func<bool> rootVisibility;
        private int _count;
        private int[] indices;
        protected Entry[] entries;
        private int RootSize;
        private int? visibleCount;
        private readonly IDictionary<int, int> indicesToVisibleIndicesMap;
        private readonly IDictionary<int, int> visibleIndicesToIndicesMap;
        private int[] visibleIndices;

        protected HashTreeIndices(bool rootVisible)
        {
            this.keyToIndicesMap = new Dictionary<int, int>(0x7f);
            this.keyToParentsMap = new Dictionary<int, int>(0x7f);
            this._count = 1;
            this.indices = new int[0x7f];
            this.entries = new Entry[0x7f];
            this.indicesToVisibleIndicesMap = new Dictionary<int, int>(0x7f);
            this.visibleIndicesToIndicesMap = new Dictionary<int, int>(0x7f);
            this.rootVisibility = () => rootVisible;
            int index = this.EnsureRootIndex(rootVisible);
            this.entries[0] = new Entry(-2128831035, index, 1);
        }

        protected HashTreeIndices(Func<bool> rootVisibility = null)
        {
            this.keyToIndicesMap = new Dictionary<int, int>(0x7f);
            this.keyToParentsMap = new Dictionary<int, int>(0x7f);
            this._count = 1;
            this.indices = new int[0x7f];
            this.entries = new Entry[0x7f];
            this.indicesToVisibleIndicesMap = new Dictionary<int, int>(0x7f);
            this.visibleIndicesToIndicesMap = new Dictionary<int, int>(0x7f);
            Func<bool> func1 = rootVisibility;
            if (rootVisibility == null)
            {
                Func<bool> local1 = rootVisibility;
                func1 = <>c.<>9__9_0;
                if (<>c.<>9__9_0 == null)
                {
                    Func<bool> local2 = <>c.<>9__9_0;
                    func1 = <>c.<>9__9_0 = () => false;
                }
            }
            this.rootVisibility = func1;
            this.indices[0] = -1;
            this.entries[0] = new Entry(-2128831035, -1, 1);
        }

        protected void AddEntry(int index, int key, int parent, int state = 0)
        {
            this.keyToParentsMap.Add(key, parent);
            this.keyToIndicesMap.Add(key, index - 1);
            this.indices[this._count] = index - 1;
            this.entries[this._count] = new Entry(key, index, state);
            this._count++;
            if ((this._count + 1) > this.indices.Length)
            {
                this.EnsureCapacity(this._count + 1);
            }
            this.Offset(parent);
        }

        protected void AddNotLoaded(int index)
        {
            this.AddEntry(index, FNV1a.NotLoaded, -2128831035, 0);
        }

        protected void AddNullObject(int index)
        {
            this.AddEntry(index, FNV1a.NullObject, -2128831035, 0);
        }

        protected int BinarySearchIndex(int index) => 
            this.BinarySearchIndex(0, this._count - 1, index);

        private int BinarySearchIndex(int lo, int hi, int index)
        {
            while (lo <= hi)
            {
                int num = lo + ((hi - lo) >> 1);
                int num2 = this.indices[num] - index;
                if (num2 == 0)
                {
                    return num;
                }
                if (num2 < 0)
                {
                    lo = num + 1;
                    continue;
                }
                hi = num - 1;
            }
            return ~lo;
        }

        protected void EnsureCapacity(int value)
        {
            int num = this.indices.Length << 1;
            if (num < value)
            {
                num = value;
            }
            int[] destinationArray = new int[num];
            Entry[] entryArray = new Entry[num];
            if (this._count > 0)
            {
                Array.Copy(this.indices, 0, destinationArray, 0, this._count);
                Array.Copy(this.entries, 0, entryArray, 0, this._count);
            }
            this.indices = destinationArray;
            this.entries = entryArray;
        }

        private void EnsureCapacity(int index, int value)
        {
            if ((this._count + value) >= this.indices.Length)
            {
                this.EnsureCapacity((this._count + value) + 1);
            }
            if (index < this._count)
            {
                Array.Copy(this.indices, index, this.indices, index + value, this._count - index);
                Array.Copy(this.entries, index, this.entries, index + value, this._count - index);
            }
        }

        public bool EnsureExpanded(int group)
        {
            int num;
            if (!this.TryGetEntryIndex(group, out num))
            {
                return false;
            }
            if (this.GetIsExpanded(this.entries[num]))
            {
                return false;
            }
            if (this.entries[num].Expand())
            {
                this.OnExpanded();
            }
            return true;
        }

        protected void EnsureRootEntry()
        {
            if (!this.keyToIndicesMap.ContainsKey(-2128831035))
            {
                this.EnsureRootEntryCore();
            }
        }

        private bool EnsureRootEntryCore()
        {
            this.ResetVisibleIndicesMapping();
            int index = this.EnsureRootIndex(this.rootVisibility());
            return this.entries[0].Reset(index);
        }

        private int EnsureRootIndex(bool rootVisible)
        {
            this.RootSize = rootVisible ? 1 : 0;
            int num = this.RootSize - 1;
            this.keyToIndicesMap[-2128831035] = num;
            this.indices[0] = num;
            return num;
        }

        public bool EnsureVisibleIndex(int index, out int value)
        {
            if (this.VisibleCount == 0)
            {
                value = 0;
            }
            else
            {
                int num;
                if (this.indicesToVisibleIndicesMap.TryGetValue(index, out num))
                {
                    value = index;
                }
                else
                {
                    int[] array = this.EnsureVisibleIndices();
                    value = array[Math.Min(~Array.BinarySearch<int>(array, index), array.Length - 1)];
                }
            }
            return (value != index);
        }

        private int[] EnsureVisibleIndices()
        {
            if (this.visibleIndices == null)
            {
                this.visibleIndices = new int[this.VisibleCount];
                this.indicesToVisibleIndicesMap.Keys.CopyTo(this.visibleIndices, 0);
                Array.Sort<int>(this.visibleIndices);
            }
            return this.visibleIndices;
        }

        private int EnsureVisibleIndicesMapping()
        {
            int num2;
            using (IEnumerator<int> enumerator = this.GetVisibleIndicesEnumerator())
            {
                int num = 0;
                while (true)
                {
                    if (!enumerator.MoveNext())
                    {
                        num2 = num;
                        break;
                    }
                    this.visibleIndicesToIndicesMap[num] = enumerator.Current;
                    this.indicesToVisibleIndicesMap[enumerator.Current] = num;
                    num++;
                }
            }
            return num2;
        }

        public bool Expand(int group)
        {
            int num;
            if (!this.TryGetEntryIndex(group, out num))
            {
                return false;
            }
            bool flag = false;
            if (flag = this.entries[num].Expand())
            {
                this.OnExpanded();
            }
            return flag;
        }

        protected bool ExpandRange(Func<Entry, bool> rangeFilter)
        {
            bool flag = false;
            for (int i = 0; i < this._count; i++)
            {
                if (rangeFilter(this.entries[i]))
                {
                    flag |= this.entries[i].Expand();
                }
            }
            if (flag)
            {
                this.OnExpanded();
            }
            return flag;
        }

        private int FindIndex(int lo, int hi, int index)
        {
            while (lo <= hi)
            {
                int num = lo + ((hi - lo) >> 1);
                int num2 = this.indices[num] - index;
                if (num2 == 0)
                {
                    return num;
                }
                if (num2 < 0)
                {
                    lo = num + 1;
                    continue;
                }
                hi = num - 1;
            }
            return lo;
        }

        [IteratorStateMachine(typeof(<GetFilteringEnumerator>d__67))]
        protected IEnumerator<int> GetFilteringEnumerator()
        {
            int <i>5__3;
            int <next>5__5;
            int num3;
            int index = 1;
        TR_0003:
            if (index >= this._count)
            {
            }
            int <currentIndex>5__2 = this.indices[index];
            yield return <currentIndex>5__2;
            Entry <entry>5__4 = this.entries[index];
            if (<entry>5__4.Length <= 0)
            {
                num3 = index;
                index = num3 + 1;
                goto TR_0005;
            }
            else
            {
                int hi = Math.Min((int) (this._count - 1), (int) (index + <entry>5__4.Length));
                <next>5__5 = this.BinarySearchIndex(index, hi, <entry>5__4.Lo);
                if (<next>5__5 >= 0)
                {
                    index = <next>5__5;
                    goto TR_0005;
                }
                else
                {
                    <i>5__3 = 1;
                }
            }
        Label_PostSwitchInIterator:;
            if (<i>5__3 <= <entry>5__4.Length)
            {
                yield return (<currentIndex>5__2 + <i>5__3);
                num3 = <i>5__3;
                <i>5__3 = num3 + 1;
                goto Label_PostSwitchInIterator;
            }
            index = ~<next>5__5;
        TR_0005:
            <entry>5__4 = null;
            goto TR_0003;
        }

        [IteratorStateMachine(typeof(<GetGroupValuesEnumerator>d__73))]
        protected internal IEnumerator<int> GetGroupValuesEnumerator()
        {
            int <i>5__4;
            int <next>5__5;
            int num3;
            if (this.RootSize == 1)
            {
                yield return 0;
            }
            int index = 1;
        TR_0004:
            if (index >= this._count)
            {
            }
            Entry <entry>5__1 = this.entries[index];
            int <currentIndex>5__3 = this.indices[index];
            yield return <currentIndex>5__3;
            if (<entry>5__1.Length <= 0)
            {
                num3 = index;
                index = num3 + 1;
                goto TR_0009;
            }
            else
            {
                int hi = Math.Min((int) (this._count - 1), (int) (index + <entry>5__1.Length));
                <next>5__5 = this.BinarySearchIndex(index, hi, <entry>5__1.Lo);
                if (<next>5__5 >= 0)
                {
                    index = <next>5__5;
                    goto TR_0009;
                }
                else
                {
                    <i>5__4 = 1;
                }
            }
        Label_PostSwitchInIterator:;
            if (<i>5__4 <= <entry>5__1.Length)
            {
                yield return (<currentIndex>5__3 + <i>5__4);
                num3 = <i>5__4;
                <i>5__4 = num3 + 1;
                goto Label_PostSwitchInIterator;
            }
            index = ~<next>5__5;
        TR_0009:
            <entry>5__1 = null;
            goto TR_0004;
        }

        [IteratorStateMachine(typeof(<GetGroupValuesEnumerator>d__72))]
        protected internal IEnumerator<int> GetGroupValuesEnumerator(Func<int, int, int, int> getGroupKey, Func<int, int, int, int> getValueKey, Func<int, int, bool> match)
        {
            Entry <entry>5__2;
            int <i>5__6;
            int <next>5__7;
            bool <includeChildren>5__4;
            int <currentIndex>5__5;
            int num5;
            int index = 1;
            HashSet<int> <branches>5__1 = new HashSet<int>();
            goto TR_0013;
        TR_0002:
            <entry>5__2 = null;
        TR_0013:
            while (true)
            {
                int num3;
                if (index >= this._count)
                {
                }
                <entry>5__2 = this.entries[index];
                <currentIndex>5__5 = this.indices[index];
                int num2 = getGroupKey(<entry>5__2.Key, num3 = this.keyToParentsMap[<entry>5__2.Key], <currentIndex>5__5);
                if (<branches>5__1.Contains(num3) || match(num2, <currentIndex>5__5))
                {
                    <branches>5__1.Add(num2);
                    yield return <currentIndex>5__5;
                }
                break;
            }
            if (<entry>5__2.Length <= 0)
            {
                num5 = index;
                index = num5 + 1;
                goto TR_0002;
            }
            else
            {
                int hi = Math.Min((int) (this._count - 1), (int) (index + <entry>5__2.Length));
                <next>5__7 = this.BinarySearchIndex(index, hi, <entry>5__2.Lo);
                if (<next>5__7 >= 0)
                {
                    index = <next>5__7;
                    goto TR_0002;
                }
                else
                {
                    <includeChildren>5__4 = <branches>5__1.Contains(<entry>5__2.Key);
                    <i>5__6 = 1;
                }
            }
            while (true)
            {
                if (<i>5__6 > <entry>5__2.Length)
                {
                    index = ~<next>5__7;
                    break;
                }
                if (<includeChildren>5__4 || match(getValueKey(<entry>5__2.Key, <currentIndex>5__5 + <i>5__6, <i>5__6 - 1), <currentIndex>5__5 + <i>5__6))
                {
                    yield return (<currentIndex>5__5 + <i>5__6);
                }
                num5 = <i>5__6;
                <i>5__6 = num5 + 1;
            }
            goto TR_0002;
        }

        public int GetIndex(int visibleIndex) => 
            this.visibleIndicesToIndicesMap[visibleIndex];

        protected virtual bool GetIsExpanded(Entry entry) => 
            entry.IsExpanded;

        protected virtual bool GetIsVisible(Entry entry, int index) => 
            true;

        protected int GetNextVisibleIndex(int index)
        {
            this.visibleIndices = this.EnsureVisibleIndices();
            for (int i = this.visibleIndices.Length - 1; i >= 0; i--)
            {
                if ((this.visibleIndices[i] <= index) && (i == (this.visibleIndices.Length - 1)))
                {
                    return this.indicesToVisibleIndicesMap[this.visibleIndices[i]];
                }
                if (this.visibleIndices[i] <= index)
                {
                    return this.indicesToVisibleIndicesMap[this.visibleIndices[i + 1]];
                }
            }
            return this.indicesToVisibleIndicesMap[this.visibleIndices.Length - 1];
        }

        protected int GetPrevVisibleIndex(int index)
        {
            this.visibleIndices = this.EnsureVisibleIndices();
            for (int i = 0; i < this.visibleIndices.Length; i++)
            {
                if (this.visibleIndices[i] == index)
                {
                    return this.indicesToVisibleIndicesMap[index];
                }
                if ((this.visibleIndices[i] > index) && (i > 1))
                {
                    return this.indicesToVisibleIndicesMap[this.visibleIndices[i - 1]];
                }
            }
            return this.indicesToVisibleIndicesMap[this.visibleIndices[0]];
        }

        [IteratorStateMachine(typeof(<GetValuesEnumerator>d__71))]
        protected IEnumerator<int> GetValuesEnumerator(Func<int, int, int> getValueKey, Func<int, int, bool> match)
        {
            Entry <entry>5__2;
            int <next>5__7;
            int <i>5__6;
            int <currentIndex>5__5;
            int <parentKey>5__4;
            int num2;
            int num3;
            int index = 1;
            HashSet<int> <branches>5__1 = new HashSet<int>();
            goto TR_0014;
        TR_0003:
            <entry>5__2 = null;
            goto TR_0014;
        TR_0004:
            index = ~<next>5__7;
            goto TR_0003;
        TR_0014:
            while (true)
            {
                if (index >= this._count)
                {
                }
                <entry>5__2 = this.entries[index];
                <currentIndex>5__5 = this.indices[index];
                <parentKey>5__4 = num3 = this.keyToParentsMap[<entry>5__2.Key];
                num2 = getValueKey(num3, <currentIndex>5__5);
                if (!<branches>5__1.Contains(<entry>5__2.Key) && (!<branches>5__1.Contains(<parentKey>5__4) && match(num2, <currentIndex>5__5)))
                {
                    <branches>5__1.Add(num2);
                    yield return <currentIndex>5__5;
                }
                break;
            }
            if (<entry>5__2.Length <= 0)
            {
                num3 = index;
                index = num3 + 1;
                goto TR_0003;
            }
            else
            {
                int hi = Math.Min((int) (this._count - 1), (int) (index + <entry>5__2.Length));
                <next>5__7 = this.BinarySearchIndex(index, hi, <entry>5__2.Lo);
                if (<next>5__7 >= 0)
                {
                    index = <next>5__7;
                    goto TR_0003;
                }
                else if (<branches>5__1.Contains(<entry>5__2.Key) || <branches>5__1.Contains(<parentKey>5__4))
                {
                    goto TR_0004;
                }
                else
                {
                    <i>5__6 = 1;
                }
            }
            while (true)
            {
                while (true)
                {
                    if (<i>5__6 <= <entry>5__2.Length)
                    {
                        if (match(num2 = getValueKey(<entry>5__2.Key, <currentIndex>5__5 + <i>5__6), <currentIndex>5__5 + <i>5__6))
                        {
                            yield return (<currentIndex>5__5 + <i>5__6);
                            break;
                        }
                    }
                    else
                    {
                        goto TR_0004;
                    }
                    break;
                }
                num3 = <i>5__6;
                <i>5__6 = num3 + 1;
            }
        }

        public int GetVisibleIndex(int index) => 
            this.indicesToVisibleIndicesMap[index];

        [IteratorStateMachine(typeof(<GetVisibleIndices>d__29))]
        public IEnumerable<int> GetVisibleIndices(IEnumerable<int> indexes)
        {
            <GetVisibleIndices>d__29 d__1 = new <GetVisibleIndices>d__29(-2);
            d__1.<>4__this = this;
            d__1.<>3__indexes = indexes;
            return d__1;
        }

        [IteratorStateMachine(typeof(<GetVisibleIndices>d__27))]
        public IEnumerator<int> GetVisibleIndices(int index)
        {
            <GetVisibleIndices>d__27 d__1 = new <GetVisibleIndices>d__27(0);
            d__1.<>4__this = this;
            d__1.index = index;
            return d__1;
        }

        [IteratorStateMachine(typeof(<GetVisibleIndicesEnumerator>d__33))]
        private IEnumerator<int> GetVisibleIndicesEnumerator()
        {
            Entry <entry>5__1;
            int <next>5__5;
            int <i>5__4;
            int <currentIndex>5__3;
            int num2;
            int index = 0;
            goto TR_0014;
        TR_0003:
            <entry>5__1 = null;
            goto TR_0014;
        TR_0004:
            index = ~<next>5__5;
            goto TR_0003;
        TR_0014:
            while (true)
            {
                if (index >= this._count)
                {
                }
                <currentIndex>5__3 = this.indices[index];
                <entry>5__1 = this.entries[index];
                if ((index == 0) ? this.rootVisibility() : this.GetIsVisible(<entry>5__1, <currentIndex>5__3))
                {
                    yield return <currentIndex>5__3;
                }
                break;
            }
            if (<entry>5__1.Length <= 0)
            {
                num2 = index;
                index = num2 + 1;
                goto TR_0003;
            }
            else
            {
                <next>5__5 = this.NextIndex(index, <entry>5__1);
                if (<next>5__5 >= 0)
                {
                    index = <next>5__5;
                    goto TR_0003;
                }
                else if (!this.GetIsExpanded(<entry>5__1))
                {
                    goto TR_0004;
                }
                else
                {
                    <i>5__4 = 1;
                }
            }
            while (true)
            {
                while (true)
                {
                    if (<i>5__4 <= <entry>5__1.Length)
                    {
                        if (this.GetIsVisible(null, <currentIndex>5__3 + <i>5__4))
                        {
                            yield return (<currentIndex>5__3 + <i>5__4);
                            break;
                        }
                    }
                    else
                    {
                        goto TR_0004;
                    }
                    break;
                }
                num2 = <i>5__4;
                <i>5__4 = num2 + 1;
            }
        }

        [IteratorStateMachine(typeof(<GetVisibleIndicesInverted>d__28))]
        public IEnumerator<int> GetVisibleIndicesInverted(int index)
        {
            <GetVisibleIndicesInverted>d__28 d__1 = new <GetVisibleIndicesInverted>d__28(0);
            d__1.<>4__this = this;
            d__1.index = index;
            return d__1;
        }

        protected internal int IndexOf(int key)
        {
            int num;
            return (this.keyToIndicesMap.TryGetValue(key, out num) ? num : -1);
        }

        public bool IsExpanded(int key)
        {
            int num;
            return (this.TryGetEntryIndex(key, out num) && this.GetIsExpanded(this.entries[num]));
        }

        protected void LoadLevel(int key, int index, object[] level, Action<int, int, int> onEntryCreated)
        {
            int num = this.FindIndex(0, this._count - 1, index);
            if (level.Length != 0)
            {
                this.EnsureCapacity(num, level.Length);
            }
            for (int i = 0; i < level.Length; i++)
            {
                int num3 = FNV1a.Next(key, level[i]);
                this.SetEntry(num3, key, num + i, index + i);
                onEntryCreated(i, num3, key);
            }
            this._count += level.Length;
        }

        protected void LoadTree(object[] values, Action<object[], bool> addAllEntries)
        {
            this.EnsureRootEntry();
            if ((values.Length == 0) || (addAllEntries == null))
            {
                this.Reset();
            }
            else
            {
                addAllEntries(values, this.RootSize > 0);
                this.ResetVisibleIndicesMapping();
            }
        }

        private int NextIndex(int index, Entry entry)
        {
            int hi = Math.Min((int) (this._count - 1), (int) (index + entry.Length));
            return this.BinarySearchIndex(index, hi, this.GetIsExpanded(entry) ? entry.Lo : entry.Hi);
        }

        protected void Offset(int parentKey)
        {
            int hi = this._count - 1;
            while (parentKey != -2128831035)
            {
                int index = this.keyToIndicesMap[parentKey];
                Entry entry1 = this.entries[this.FindIndex(0, hi, index)];
                entry1.Hi++;
                hi = Math.Min(index, this._count - 1);
                parentKey = this.keyToParentsMap[parentKey];
            }
            if (parentKey == -2128831035)
            {
                this.entries[0].SetRange(this.RootSize, this.entries[0].Length + 1);
            }
        }

        protected unsafe void Offset(int startPosition, int offset)
        {
            for (int i = this._count - 1; i > startPosition; i--)
            {
                Entry entry = this.entries[i];
                this.keyToIndicesMap[entry.Key] = entry.Offset(offset);
                int* numPtr1 = &(this.indices[i]);
                numPtr1[0] += offset;
            }
            if (startPosition > 0)
            {
                int num4;
                int key = this.entries[startPosition].Key;
                for (int j = startPosition; key != -2128831035; j = Math.Min(num4, this._count - 1))
                {
                    num4 = this.keyToIndicesMap[this.keyToParentsMap[key]];
                    Entry entry2 = this.entries[this.FindIndex(0, j, num4)];
                    entry2.Hi += offset;
                    key = entry2.Key;
                }
            }
        }

        protected virtual void OnExpanded()
        {
            this.ResetVisibleIndicesMapping();
        }

        protected virtual void OnSetExpandedByFilter(int index)
        {
        }

        protected internal int ParentOf(int key) => 
            this.keyToParentsMap[key];

        protected void RemoveEntry(int key, int position)
        {
            this.keyToParentsMap.Remove(key);
            this.keyToIndicesMap.Remove(key);
            this.indices[position] = 0;
            this.entries[position] = null;
        }

        protected void RemoveLevel(Entry entry, Action<int> onEntryRemoved)
        {
            int lo = this.BinarySearchIndex(0, this._count - 1, entry.Lo);
            if (lo > 0)
            {
                int sourceIndex = this.FindIndex(lo, this._count - 1, entry.Hi);
                int index = lo;
                while (true)
                {
                    if (index >= sourceIndex)
                    {
                        this._count -= sourceIndex - lo;
                        if (lo < this._count)
                        {
                            Array.Copy(this.indices, sourceIndex, this.indices, lo, this._count - lo);
                            Array.Copy(this.entries, sourceIndex, this.entries, lo, this._count - lo);
                        }
                        break;
                    }
                    int key = this.entries[index].Key;
                    this.RemoveEntry(key, index);
                    onEntryRemoved(key);
                    index++;
                }
            }
        }

        protected void RemoveTree(Action<int> onEntryRemoved)
        {
            for (int i = 1; i < this._count; i++)
            {
                int key = this.entries[i].Key;
                this.RemoveEntry(key, i);
                onEntryRemoved(key);
            }
            this._count = 1;
        }

        public bool Reset()
        {
            this.ResetVisibleIndicesMapping();
            this.keyToParentsMap.Clear();
            this.keyToIndicesMap.Clear();
            Array.Clear(this.indices, 1, this._count);
            Array.Clear(this.entries, 1, this._count);
            this._count = 1;
            return this.EnsureRootEntryCore();
        }

        public bool Reset(bool? rootVisibility)
        {
            if (rootVisibility != null)
            {
                this.rootVisibility = () => rootVisibility.Value;
            }
            return this.Reset();
        }

        protected virtual void ResetExpandedByFilter()
        {
            for (int i = 0; i < this._count; i++)
            {
                this.entries[i].ResetExpandedByFilter();
            }
        }

        protected void ResetVisibleIndicesMapping()
        {
            this.visibleCount = null;
            this.visibleIndices = null;
            this.indicesToVisibleIndicesMap.Clear();
            this.visibleIndicesToIndicesMap.Clear();
        }

        protected void SetEntry(int key, int parent, int position, int index)
        {
            this.keyToParentsMap.Add(key, parent);
            this.keyToIndicesMap.Add(key, index);
            this.indices[position] = index;
            this.entries[position] = new Entry(key, index, 0);
        }

        protected void SetExpandedByFilter(int parentKey)
        {
            int hi = this._count - 1;
            while (parentKey != -2128831035)
            {
                int index = this.keyToIndicesMap[parentKey];
                this.OnSetExpandedByFilter(index);
                this.entries[this.FindIndex(0, hi, index)].SetExpandedByFilter();
                hi = Math.Min(index, this._count - 1);
                parentKey = this.keyToParentsMap[parentKey];
            }
        }

        protected bool TryGetEntryIndex(int key, out int entryIndex)
        {
            int num;
            entryIndex = -1;
            if (!this.keyToIndicesMap.TryGetValue(key, out num))
            {
                return false;
            }
            entryIndex = this.FindIndex(0, this._count - 1, num);
            return (entryIndex != -1);
        }

        protected internal bool TryGetIndex(int key, out int index) => 
            this.keyToIndicesMap.TryGetValue(key, out index);

        protected internal bool TryGetParent(int key, out int parent) => 
            this.keyToParentsMap.TryGetValue(key, out parent);

        public int Count =>
            this.entries[0].Length + (((this._count > 2) || (this.entries[0].Length > 1)) ? this.RootSize : 0);

        public int VisibleCount
        {
            get
            {
                if (this.visibleCount == null)
                {
                    this.visibleCount = new int?(this.EnsureVisibleIndicesMapping());
                }
                return this.visibleCount.Value;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly HashTreeIndices.<>c <>9 = new HashTreeIndices.<>c();
            public static Func<bool> <>9__9_0;

            internal bool <.ctor>b__9_0() => 
                false;
        }





        [CompilerGenerated]
        private sealed class <GetVisibleIndices>d__27 : IEnumerator<int>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private int <>2__current;
            public int index;
            public HashTreeIndices <>4__this;
            private int[] <visibleIndices>5__1;
            private int <i>5__2;

            [DebuggerHidden]
            public <GetVisibleIndices>d__27(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    if ((this.index < 0) || (this.index >= this.<>4__this.Count))
                    {
                        return false;
                    }
                    this.<visibleIndices>5__1 = this.<>4__this.EnsureVisibleIndices();
                    this.<i>5__2 = 0;
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    goto TR_0006;
                }
            TR_0004:
                if (this.<i>5__2 >= this.<visibleIndices>5__1.Length)
                {
                    return false;
                }
                if (this.<visibleIndices>5__1[this.<i>5__2] >= this.index)
                {
                    this.<>2__current = this.<visibleIndices>5__1[this.<i>5__2];
                    this.<>1__state = 1;
                    return true;
                }
            TR_0006:
                while (true)
                {
                    int num2 = this.<i>5__2;
                    this.<i>5__2 = num2 + 1;
                    break;
                }
                goto TR_0004;
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            int IEnumerator<int>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <GetVisibleIndices>d__29 : IEnumerable<int>, IEnumerable, IEnumerator<int>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private int <>2__current;
            private int <>l__initialThreadId;
            private IEnumerable<int> indexes;
            public IEnumerable<int> <>3__indexes;
            public HashTreeIndices <>4__this;
            private IEnumerator<int> <>7__wrap1;

            [DebuggerHidden]
            public <GetVisibleIndices>d__29(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                if (this.<>7__wrap1 != null)
                {
                    this.<>7__wrap1.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        if ((this.indexes == null) || (this.<>4__this.VisibleCount == 0))
                        {
                            return false;
                        }
                        else
                        {
                            this.<>7__wrap1 = this.indexes.GetEnumerator();
                            this.<>1__state = -3;
                        }
                    }
                    else if (num == 1)
                    {
                        this.<>1__state = -3;
                    }
                    else
                    {
                        return false;
                    }
                    while (true)
                    {
                        if (!this.<>7__wrap1.MoveNext())
                        {
                            this.<>m__Finally1();
                            this.<>7__wrap1 = null;
                            flag = false;
                        }
                        else
                        {
                            int num3;
                            int current = this.<>7__wrap1.Current;
                            if (!this.<>4__this.indicesToVisibleIndicesMap.TryGetValue(current, out num3))
                            {
                                continue;
                            }
                            this.<>2__current = num3;
                            this.<>1__state = 1;
                            flag = true;
                        }
                        break;
                    }
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<int> IEnumerable<int>.GetEnumerator()
            {
                HashTreeIndices.<GetVisibleIndices>d__29 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new HashTreeIndices.<GetVisibleIndices>d__29(0) {
                        <>4__this = this.<>4__this
                    };
                }
                d__.indexes = this.<>3__indexes;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<System.Int32>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if ((num == -3) || (num == 1))
                {
                    try
                    {
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            int IEnumerator<int>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }


        [CompilerGenerated]
        private sealed class <GetVisibleIndicesInverted>d__28 : IEnumerator<int>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private int <>2__current;
            public int index;
            public HashTreeIndices <>4__this;
            private int[] <visibleIndices>5__1;
            private int <i>5__2;

            [DebuggerHidden]
            public <GetVisibleIndicesInverted>d__28(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    if ((this.index < 0) || (this.index >= this.<>4__this.Count))
                    {
                        return false;
                    }
                    this.<visibleIndices>5__1 = this.<>4__this.EnsureVisibleIndices();
                    this.<i>5__2 = this.<visibleIndices>5__1.Length - 1;
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    goto TR_0006;
                }
            TR_0004:
                if (this.<i>5__2 < 0)
                {
                    return false;
                }
                if (this.<visibleIndices>5__1[this.<i>5__2] <= this.index)
                {
                    this.<>2__current = this.<visibleIndices>5__1[this.<i>5__2];
                    this.<>1__state = 1;
                    return true;
                }
            TR_0006:
                while (true)
                {
                    int num2 = this.<i>5__2;
                    this.<i>5__2 = num2 - 1;
                    break;
                }
                goto TR_0004;
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            int IEnumerator<int>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        protected sealed class Entry : IEquatable<HashTreeIndices.Entry>
        {
            public readonly int Key;
            public int Lo;
            public int Hi;
            private int State;

            internal Entry(int key, int index, int state = 0)
            {
                this.Key = key;
                this.Lo = this.Hi = index;
                this.State = state;
            }

            public bool CheckState(int state) => 
                (this.State & state) == state;

            public bool Equals(HashTreeIndices.Entry entry) => 
                this.Key == entry.Key;

            public sealed override bool Equals(object obj)
            {
                HashTreeIndices.Entry entry = obj as HashTreeIndices.Entry;
                return ((entry != null) ? (this.Key == entry.Key) : false);
            }

            public bool Expand()
            {
                if (this.Key == -2128831035)
                {
                    return false;
                }
                if ((this.State & 2) != 2)
                {
                    this.State ^= 1;
                }
                else
                {
                    this.State &= -3;
                    if ((this.State & 1) == 1)
                    {
                        this.State ^= 1;
                    }
                }
                return true;
            }

            public sealed override int GetHashCode() => 
                this.Key;

            internal int Offset(int offset)
            {
                this.Lo += offset;
                this.Hi += offset;
                return ((this.Lo == this.Hi) ? this.Lo : (this.Lo - 1));
            }

            internal bool Reset(int index)
            {
                int num = this.Hi - this.Lo;
                this.Lo = this.Hi = index;
                return (num > 0);
            }

            internal void ResetExpandedByFilter()
            {
                this.State &= -3;
            }

            internal void SetExpandedByFilter()
            {
                this.State |= 2;
            }

            internal int SetRange(object[] level) => 
                ((level == null) || (level.Length == 0)) ? -this.Length : this.SetRange(this.Lo + 1, level.Length);

            internal int SetRange(int start, int length)
            {
                int num = this.Hi - this.Lo;
                this.Lo = start;
                this.Hi = this.Lo + length;
                return (length - num);
            }

            public sealed override string ToString()
            {
                string[] textArray1 = new string[5];
                string[] textArray2 = new string[5];
                textArray2[0] = this.IsExpanded ? "[+] " : "[-] ";
                string[] local1 = textArray2;
                local1[1] = this.Key.ToString();
                local1[2] = " ";
                local1[3] = this.Lo.ToString();
                local1[4] = (this.Lo == this.Hi) ? string.Empty : ("-" + this.Hi.ToString());
                return string.Concat(local1);
            }

            public int Length =>
                this.Hi - this.Lo;

            public bool IsExpanded =>
                (this.State & 3) != 0;
        }
    }
}

