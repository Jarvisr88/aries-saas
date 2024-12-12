namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Generic;

    internal abstract class HashTreeRadios : HashTreeChecksBase, IHashTreeChecks
    {
        protected HashTreeRadios(HashSet<int> checks, IDictionary<int, ICheckedGroup> delayedChecks, HashSet<int> delayedCheckedGroups) : base(checks, delayedChecks, delayedCheckedGroups)
        {
        }

        void IHashTreeChecks.Initialize(ICheckedGroup value)
        {
            object[] objArray;
            if (this.TryGetChildren(value.Group, out objArray) && !UniqueValues.AreNotLoaded(objArray))
            {
                this.InitializeCheck(value);
            }
            else
            {
                base.InitializeDelayedChecks(value);
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
                        this.InitializeCheck(group);
                    }
                    base.delayedChecks.Remove(key);
                }
                base.UpdateDelayedCheckedGroups();
            }
        }

        bool IHashTreeChecks.Invert() => 
            false;

        bool? IHashTreeChecks.IsChecked(int key, int valueKey) => 
            new bool?(base.checks.Contains(valueKey));

        void IHashTreeChecks.Toggle(int key, int valueKey)
        {
            if (!base.checks.Remove(valueKey))
            {
                base.checks.Clear();
                base.checks.Add(valueKey);
            }
        }

        bool IHashTreeChecks.ToggleAll() => 
            false;

        private void InitializeCheck(ICheckedGroup value)
        {
            base.checks.Add(FNV1a.Next(value.Group, value.Values[0]));
        }
    }
}

