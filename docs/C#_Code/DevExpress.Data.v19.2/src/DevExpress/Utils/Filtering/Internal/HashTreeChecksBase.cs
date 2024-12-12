namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal abstract class HashTreeChecksBase
    {
        protected readonly HashSet<int> checks;
        protected readonly IDictionary<int, ICheckedGroup> delayedChecks = new Dictionary<int, ICheckedGroup>();
        private readonly HashSet<int> delayedCheckedGroups = new HashSet<int>();

        protected HashTreeChecksBase(HashSet<int> checks, IDictionary<int, ICheckedGroup> delayedChecks, HashSet<int> delayedCheckedGroups)
        {
            this.checks = checks;
            this.delayedChecks = delayedChecks;
            this.delayedCheckedGroups = delayedCheckedGroups;
        }

        protected void ClearChecks()
        {
            this.checks.Clear();
            Action<IDictionary<int, ICheckedGroup>> @do = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Action<IDictionary<int, ICheckedGroup>> local1 = <>c.<>9__5_0;
                @do = <>c.<>9__5_0 = x => x.Clear();
            }
            this.delayedChecks.Do<IDictionary<int, ICheckedGroup>>(@do);
            Action<HashSet<int>> action2 = <>c.<>9__5_1;
            if (<>c.<>9__5_1 == null)
            {
                Action<HashSet<int>> local2 = <>c.<>9__5_1;
                action2 = <>c.<>9__5_1 = x => x.Clear();
            }
            this.delayedCheckedGroups.Do<HashSet<int>>(action2);
        }

        protected bool HasDelayedChecks(int group) => 
            ((this.delayedChecks == null) || ((this.delayedChecks.Count <= 0) || !this.delayedChecks.ContainsKey(group))) ? ((this.delayedCheckedGroups != null) && ((this.delayedCheckedGroups.Count > 0) && this.delayedCheckedGroups.Contains(group))) : true;

        protected void InitializeDelayedChecks(ICheckedGroup value)
        {
            if (this.delayedChecks != null)
            {
                int key = -2128831035;
                if (value.Group == key)
                {
                    this.delayedChecks.Add(key, value);
                }
                else
                {
                    for (int i = 0; i < value.Path.Length; i++)
                    {
                        key = FNV1a.Next(key, value.Path[i]);
                        if (key == value.Group)
                        {
                            this.delayedChecks.Add(key, value);
                        }
                        else
                        {
                            this.delayedCheckedGroups.Add(key);
                        }
                    }
                }
            }
        }

        private bool IsChild(ICheckedGroup value, int group)
        {
            int index = 0;
            int hashCode = -2128831035;
            while (index < value.Path.Length)
            {
                if (FNV1a.Next(hashCode, value.Path[index]) == group)
                {
                    return true;
                }
                index++;
            }
            return false;
        }

        private void ObtainDelayedGroups(ICheckedGroup value, HashSet<int> delayedGroups)
        {
            int index = 0;
            int hashCode = -2128831035;
            while (index < value.Path.Length)
            {
                hashCode = FNV1a.Next(hashCode, value.Path[index]);
                if (hashCode != value.Group)
                {
                    delayedGroups.Add(hashCode);
                }
                index++;
            }
        }

        public bool Reset()
        {
            bool flag = this.checks.Count > 0;
            this.ClearChecks();
            return flag;
        }

        protected void ResetDelayedChecks(int group)
        {
            if (this.delayedChecks != null)
            {
                HashSet<int> set = new HashSet<int>();
                foreach (KeyValuePair<int, ICheckedGroup> pair in this.delayedChecks)
                {
                    if (this.IsChild(pair.Value, group))
                    {
                        set.Add(pair.Value.Group);
                    }
                }
                if (set.Count > 0)
                {
                    foreach (int num in set)
                    {
                        this.delayedChecks.Remove(num);
                    }
                    this.UpdateDelayedCheckedGroups();
                }
            }
        }

        protected abstract bool TryGetChildren(int group, out object[] children);
        protected void UpdateDelayedCheckedGroups()
        {
            if (this.delayedChecks != null)
            {
                HashSet<int> delayedGroups = new HashSet<int>();
                foreach (KeyValuePair<int, ICheckedGroup> pair in this.delayedChecks)
                {
                    this.ObtainDelayedGroups(pair.Value, delayedGroups);
                }
                this.delayedCheckedGroups.IntersectWith(delayedGroups);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly HashTreeChecksBase.<>c <>9 = new HashTreeChecksBase.<>c();
            public static Action<IDictionary<int, ICheckedGroup>> <>9__5_0;
            public static Action<HashSet<int>> <>9__5_1;

            internal void <ClearChecks>b__5_0(IDictionary<int, ICheckedGroup> x)
            {
                x.Clear();
            }

            internal void <ClearChecks>b__5_1(HashSet<int> x)
            {
                x.Clear();
            }
        }
    }
}

