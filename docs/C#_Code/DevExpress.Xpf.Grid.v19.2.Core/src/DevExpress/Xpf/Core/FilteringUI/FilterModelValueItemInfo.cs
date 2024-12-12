namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Xpf.Core.FilteringUI.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class FilterModelValueItemInfo
    {
        public FilterModelValueItemInfo(Func<AllowedOperandTypes> getAllowedOperandTypes, Func<IReadOnlyCollection<PropertySelectorModelBase>> getProperties, Func<IReadOnlyCollection<string>> getParameters, Func<bool> getShowSearchPanel)
        {
            Func<AllowedOperandTypes> func1 = getAllowedOperandTypes;
            if (getAllowedOperandTypes == null)
            {
                Func<AllowedOperandTypes> local1 = getAllowedOperandTypes;
                func1 = <>c.<>9__0_0;
                if (<>c.<>9__0_0 == null)
                {
                    Func<AllowedOperandTypes> local2 = <>c.<>9__0_0;
                    func1 = <>c.<>9__0_0 = () => AllowedOperandTypes.Value;
                }
            }
            this.<GetAllowedOperandTypes>k__BackingField = func1;
            Func<IReadOnlyCollection<PropertySelectorModelBase>> func2 = getProperties;
            if (getProperties == null)
            {
                Func<IReadOnlyCollection<PropertySelectorModelBase>> local3 = getProperties;
                func2 = <>c.<>9__0_1;
                if (<>c.<>9__0_1 == null)
                {
                    Func<IReadOnlyCollection<PropertySelectorModelBase>> local4 = <>c.<>9__0_1;
                    func2 = <>c.<>9__0_1 = () => new List<PropertySelectorModelBase>().AsReadOnly();
                }
            }
            this.<GetProperties>k__BackingField = func2;
            Func<IReadOnlyCollection<string>> func3 = getParameters;
            if (getParameters == null)
            {
                Func<IReadOnlyCollection<string>> local5 = getParameters;
                func3 = <>c.<>9__0_2;
                if (<>c.<>9__0_2 == null)
                {
                    Func<IReadOnlyCollection<string>> local6 = <>c.<>9__0_2;
                    func3 = <>c.<>9__0_2 = () => new List<string>().AsReadOnly();
                }
            }
            this.<GetParameters>k__BackingField = func3;
            this.<GetShowSearchPanel>k__BackingField = getShowSearchPanel;
        }

        public static FilterModelValueItemInfo CreateDefault() => 
            new FilterModelValueItemInfo(null, null, null, null);

        public Func<AllowedOperandTypes> GetAllowedOperandTypes { get; }

        public Func<IReadOnlyCollection<PropertySelectorModelBase>> GetProperties { get; }

        public Func<IReadOnlyCollection<string>> GetParameters { get; }

        public Func<bool> GetShowSearchPanel { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterModelValueItemInfo.<>c <>9 = new FilterModelValueItemInfo.<>c();
            public static Func<AllowedOperandTypes> <>9__0_0;
            public static Func<IReadOnlyCollection<PropertySelectorModelBase>> <>9__0_1;
            public static Func<IReadOnlyCollection<string>> <>9__0_2;

            internal AllowedOperandTypes <.ctor>b__0_0() => 
                AllowedOperandTypes.Value;

            internal IReadOnlyCollection<PropertySelectorModelBase> <.ctor>b__0_1() => 
                new List<PropertySelectorModelBase>().AsReadOnly();

            internal IReadOnlyCollection<string> <.ctor>b__0_2() => 
                new List<string>().AsReadOnly();
        }
    }
}

