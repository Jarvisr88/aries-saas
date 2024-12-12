namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    internal abstract class HashTreeChecks : HashTreeChecksBase, IHashTreeChecks
    {
        protected HashTreeChecks(HashSet<int> checks, IDictionary<int, ICheckedGroup> delayedChecks, HashSet<int> delayedCheckedGroups) : base(checks, delayedChecks, delayedCheckedGroups)
        {
        }

        private bool AreAnyChildrenChecked(int group, int depthLimit)
        {
            if (depthLimit >= 0)
            {
                object[] objArray;
                if (!this.TryGetChildren(group, out objArray) || UniqueValues.AreNotLoaded(objArray))
                {
                    return base.HasDelayedChecks(group);
                }
                for (int i = 0; i < objArray.Length; i++)
                {
                    int item = FNV1a.Next(group, objArray[i]);
                    if (base.checks.Contains(item) || this.AreAnyChildrenChecked(item, depthLimit - 1))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void CheckLevel(int key)
        {
            object[] objArray;
            if (this.TryGetChildren(key, out objArray))
            {
                for (int i = 0; i < objArray.Length; i++)
                {
                    base.checks.Add(FNV1a.Next(key, objArray[i]));
                }
            }
        }

        void IHashTreeChecks.Initialize(ICheckedGroup value)
        {
            object[] objArray;
            if (!this.TryGetChildren(value.Group, out objArray) || UniqueValues.AreNotLoaded(objArray))
            {
                base.InitializeDelayedChecks(value);
            }
            else if (value.IsInverted)
            {
                this.InitializeInvertedChecks(value.Group, value.Values, objArray);
            }
            else
            {
                this.InitializeChecks(value.Group, value.Values);
            }
        }

        void IHashTreeChecks.Initialize(object[] level, int key)
        {
            if ((level != null) && !UniqueValues.AreNotLoaded(level))
            {
                ICheckedGroup group;
                if ((base.delayedChecks != null) && base.delayedChecks.TryGetValue(key, out group))
                {
                    if (group.Group == key)
                    {
                        if (group.IsInverted)
                        {
                            this.InitializeInvertedChecks(key, group.Values, level);
                        }
                        else
                        {
                            this.InitializeChecks(key, group.Values);
                        }
                    }
                    base.delayedChecks.Remove(key);
                }
                base.UpdateDelayedCheckedGroups();
            }
        }

        bool IHashTreeChecks.Invert()
        {
            if (base.checks.Count == 0)
            {
                return base.checks.Add(-2128831035);
            }
            if (!base.checks.Remove(-2128831035) || (base.checks.Count != 0))
            {
                HashSet<int> invertedChecks = new HashSet<int>();
                this.InvertLevel(-2128831035, invertedChecks, this.GetDepth());
                this.InvertChecks(invertedChecks);
            }
            return true;
        }

        bool? IHashTreeChecks.IsChecked(int key, int valueKey)
        {
            if (base.checks.Contains(valueKey))
            {
                return true;
            }
            if (valueKey == -2128831035)
            {
                if ((base.checks.Count <= 0) && ((base.delayedChecks == null) || (base.delayedChecks.Count <= 0)))
                {
                    return false;
                }
                return null;
            }
            if (this.IsParentChecked(key))
            {
                return true;
            }
            if (!this.AreAnyChildrenChecked(valueKey, this.GetDepth() - 1))
            {
                return false;
            }
            return null;
        }

        void IHashTreeChecks.Toggle(int key, int valueKey)
        {
            if (!this.ToggleRoot(valueKey))
            {
                object[] objArray;
                if ((base.checks.Count > 0) && this.TryGetPath(key, out objArray))
                {
                    int hashCode = -2128831035;
                    for (int i = 0; i < objArray.Length; i++)
                    {
                        hashCode = FNV1a.Next(hashCode, objArray[i]);
                        if (base.checks.Remove(hashCode))
                        {
                            this.CheckLevel(hashCode);
                        }
                    }
                }
                base.ResetDelayedChecks(valueKey);
                if (!base.checks.Remove(valueKey))
                {
                    base.checks.Add(valueKey);
                    this.EnsureLevelClear(valueKey);
                    this.EnsureLevelChecked(key);
                }
            }
        }

        bool IHashTreeChecks.ToggleAll()
        {
            bool flag = base.checks.Contains(-2128831035);
            base.ClearChecks();
            base.checks.Add(-2128831035);
            return flag;
        }

        protected void EnsureLevelChecked(HashSet<int> parents)
        {
            foreach (int num in parents)
            {
                this.EnsureLevelChecked(num);
            }
        }

        private void EnsureLevelChecked(int group)
        {
            object[] objArray;
            if (this.TryGetChildren(group, out objArray) && this.EnsureLevelChecked(objArray, group))
            {
                base.checks.Add(group);
                if (this.TryGetParent(group, out group))
                {
                    this.EnsureLevelChecked(group);
                }
            }
        }

        private bool EnsureLevelChecked(object[] level, int group)
        {
            List<int> list = new List<int>(level.Length);
            for (int i = 0; i < level.Length; i++)
            {
                int item = FNV1a.Next(group, level[i]);
                if (!base.checks.Contains(item))
                {
                    return false;
                }
                list.Add(item);
            }
            for (int j = 0; j < list.Count; j++)
            {
                base.checks.Remove(list[j]);
            }
            return true;
        }

        private void EnsureLevelClear(int group)
        {
            object[] objArray;
            if (this.TryGetChildren(group, out objArray))
            {
                for (int i = 0; i < objArray.Length; i++)
                {
                    int item = FNV1a.Next(group, objArray[i]);
                    base.checks.Remove(item);
                    this.EnsureLevelClear(item);
                }
            }
        }

        protected abstract int GetDepth();
        private void InitializeChecks(int group, object[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                base.checks.Add(FNV1a.Next(group, values[i]));
            }
        }

        private void InitializeInvertedChecks(int group, object[] values, object[] level)
        {
            int index = 0;
            int num2 = 0;
            while (index < level.Length)
            {
                if ((num2 < values.Length) && Equals(level[index], values[num2]))
                {
                    num2++;
                }
                else
                {
                    base.checks.Add(FNV1a.Next(group, level[index]));
                }
                index++;
            }
        }

        private void InvertChecks(HashSet<int> inverted)
        {
            base.checks.Clear();
            foreach (int num in inverted)
            {
                base.checks.Add(num);
            }
        }

        private void InvertLevel(int key, HashSet<int> invertedChecks, int depthLimit)
        {
            object[] objArray;
            if (this.TryGetChildren(key, out objArray))
            {
                for (int i = 0; i < objArray.Length; i++)
                {
                    int item = FNV1a.Next(key, objArray[i]);
                    if (!base.checks.Contains(item))
                    {
                        if (!this.AreAnyChildrenChecked(item, depthLimit))
                        {
                            invertedChecks.Add(item);
                        }
                        else
                        {
                            this.InvertLevel(item, invertedChecks, depthLimit - 1);
                        }
                    }
                }
            }
        }

        private bool IsParentChecked(int key)
        {
            while (true)
            {
                if (key != -2128831035)
                {
                    if (base.checks.Contains(key))
                    {
                        return true;
                    }
                    if (this.TryGetParent(key, out key))
                    {
                        continue;
                    }
                }
                return base.checks.Contains(key);
            }
        }

        private bool ToggleRoot(int valueKey)
        {
            if (!base.checks.Remove(-2128831035))
            {
                if (valueKey == -2128831035)
                {
                    base.ClearChecks();
                    return base.checks.Add(valueKey);
                }
            }
            else
            {
                base.ClearChecks();
                if (valueKey == -2128831035)
                {
                    return true;
                }
                this.CheckLevel(-2128831035);
            }
            return false;
        }

        protected abstract bool TryGetParent(int key, out int parentKey);
        protected abstract bool TryGetPath(int group, out object[] path);
    }
}

