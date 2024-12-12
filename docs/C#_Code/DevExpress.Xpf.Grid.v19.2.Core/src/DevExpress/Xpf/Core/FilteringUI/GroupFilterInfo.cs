namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class GroupFilterInfo
    {
        private readonly List<object> values;
        private readonly GroupFilterInfoValuesType type;
        public readonly string PropertyName;
        private readonly List<GroupFilterIndeterminateInfo> indeterminate;

        public GroupFilterInfo(string propertyName, List<object> values, GroupFilterInfoValuesType type, List<GroupFilterIndeterminateInfo> indeterminate)
        {
            this.PropertyName = propertyName;
            this.values = values;
            this.type = type;
            this.indeterminate = indeterminate;
        }

        public Either<GroupFilterInfo, bool> GetState(object value)
        {
            GroupFilterIndeterminateInfo local2;
            if (this.indeterminate != null)
            {
                local2 = this.indeterminate.FirstOrDefault<GroupFilterIndeterminateInfo>(x => Equals(x.Value, value));
            }
            else
            {
                List<GroupFilterIndeterminateInfo> indeterminate = this.indeterminate;
                local2 = null;
            }
            GroupFilterIndeterminateInfo info = local2;
            if (info != null)
            {
                return info.FilterInfo;
            }
            bool flag = this.values.Contains(value);
            return ((this.type == GroupFilterInfoValuesType.Checked) ? flag : !flag);
        }

        public GroupFilterInfo Invert()
        {
            if (this.indeterminate != null)
            {
                throw new InvalidOperationException();
            }
            return new GroupFilterInfo(this.PropertyName, this.values, (this.type == GroupFilterInfoValuesType.Checked) ? GroupFilterInfoValuesType.Unchecked : GroupFilterInfoValuesType.Checked, null);
        }

        public GroupFilterInfo MakeIndeterminate(GroupFilterInfo info)
        {
            if ((this.values.Count != 1) || (this.type != GroupFilterInfoValuesType.Checked))
            {
                throw new InvalidOperationException();
            }
            List<GroupFilterIndeterminateInfo> indeterminate = new List<GroupFilterIndeterminateInfo>();
            indeterminate.Add(new GroupFilterIndeterminateInfo(this.values.Single<object>(), info));
            return new GroupFilterInfo(this.PropertyName, new List<object>(), GroupFilterInfoValuesType.Checked, indeterminate);
        }

        public static GroupFilterInfo Merge(GroupFilterInfo[] infos)
        {
            string propertyName = infos.First<GroupFilterInfo>().PropertyName;
            if (!infos.All<GroupFilterInfo>(x => (x.PropertyName == propertyName)))
            {
                throw new InvalidOperationException();
            }
            List<object> determinateValues = new List<object>();
            GroupFilterInfoValuesType? determinateType = null;
            Func<GroupFilterInfo, IEnumerable<GroupFilterIndeterminateInfo>> selector = <>c.<>9__8_2;
            if (<>c.<>9__8_2 == null)
            {
                Func<GroupFilterInfo, IEnumerable<GroupFilterIndeterminateInfo>> local1 = <>c.<>9__8_2;
                selector = <>c.<>9__8_2 = x => x.indeterminate;
            }
            List<GroupFilterIndeterminateInfo> source = infos.Where<GroupFilterInfo>(delegate (GroupFilterInfo x) {
                if (x.indeterminate == null)
                {
                    determinateValues.AddRange(x.values);
                    if ((determinateType != null) && (((GroupFilterInfoValuesType) determinateType.Value) != x.type))
                    {
                        throw new InvalidOperationException();
                    }
                    determinateType = new GroupFilterInfoValuesType?(x.type);
                }
                return (x.indeterminate != null);
            }).SelectMany<GroupFilterInfo, GroupFilterIndeterminateInfo>(selector).ToList<GroupFilterIndeterminateInfo>();
            GroupFilterInfoValuesType? nullable = determinateType;
            return new GroupFilterInfo(propertyName, determinateValues, (nullable != null) ? nullable.GetValueOrDefault() : GroupFilterInfoValuesType.Checked, source.Any<GroupFilterIndeterminateInfo>() ? source : null);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GroupFilterInfo.<>c <>9 = new GroupFilterInfo.<>c();
            public static Func<GroupFilterInfo, IEnumerable<GroupFilterIndeterminateInfo>> <>9__8_2;

            internal IEnumerable<GroupFilterIndeterminateInfo> <Merge>b__8_2(GroupFilterInfo x) => 
                x.indeterminate;
        }
    }
}

