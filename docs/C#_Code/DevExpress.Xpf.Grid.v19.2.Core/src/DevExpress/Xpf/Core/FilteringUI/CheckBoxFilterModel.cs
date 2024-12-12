namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core.FilteringUI.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;

    public sealed class CheckBoxFilterModel : CriteriaConverterFilterModelBase<bool?>
    {
        internal CheckBoxFilterModel(FilterModelClient client) : this(client, new CriteriaConverter<bool?>(func1, <>c.<>9__0_4 ??= (value, propertyName) => ((value == null) ? null : new BinaryOperator(propertyName, value.Value)), <>c.<>9__0_5 ??= restrictions => restrictions.Allow(BinaryOperatorType.Equal)))
        {
            Func<CriteriaOperator, bool?> func1 = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<CriteriaOperator, bool?> local1 = <>c.<>9__0_0;
                func1 = <>c.<>9__0_0 = delegate (CriteriaOperator filter) {
                    BinaryOperatorMapper<bool?> binary = <>c.<>9__0_1;
                    if (<>c.<>9__0_1 == null)
                    {
                        BinaryOperatorMapper<bool?> local1 = <>c.<>9__0_1;
                        binary = <>c.<>9__0_1 = delegate (string name, object value, BinaryOperatorType type) {
                            bool? nullable = value as bool?;
                            if (type == BinaryOperatorType.Equal)
                            {
                                return nullable;
                            }
                            if (type == BinaryOperatorType.NotEqual)
                            {
                                bool? nullable2 = nullable;
                                if (nullable2 != null)
                                {
                                    return new bool?(!nullable2.GetValueOrDefault());
                                }
                            }
                            return null;
                        };
                    }
                    NotOperatorMapper<bool?> not = <>c.<>9__0_2;
                    if (<>c.<>9__0_2 == null)
                    {
                        NotOperatorMapper<bool?> local2 = <>c.<>9__0_2;
                        not = <>c.<>9__0_2 = delegate (bool? x) {
                            bool? nullable = x;
                            if (nullable != null)
                            {
                                return new bool?(!nullable.GetValueOrDefault());
                            }
                            return null;
                        };
                    }
                    return filter.Map<bool?>(binary, null, null, null, null, null, null, not, <>c.<>9__0_3 ??= ((FallbackMapper<bool?>) (_ => null)), null);
                };
            }
            this.<EditSettings>k__BackingField = EditSettingsInfoFactory.Default(base.Column);
        }

        protected internal override bool? CreateConverterValue()
        {
            bool isUserDefined;
            EditSettingsInfo editSettings = this.EditSettings;
            if (editSettings != null)
            {
                isUserDefined = editSettings.IsUserDefined;
            }
            else
            {
                EditSettingsInfo local1 = editSettings;
                isUserDefined = false;
            }
            return (!isUserDefined ? ((bool?) FilterModelHelper.GetEffectiveFilterValue(this.Value, base.Column.Type)) : this.Value);
        }

        protected override void UpdateFromConverterValue(bool? value)
        {
            this.Value = value;
        }

        public bool? Value
        {
            get => 
                base.GetValue<bool?>("Value");
            set => 
                base.SetValue<bool?>(value, new Action(this.ApplyFilter), "Value");
        }

        public EditSettingsInfo EditSettings { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CheckBoxFilterModel.<>c <>9 = new CheckBoxFilterModel.<>c();
            public static BinaryOperatorMapper<bool?> <>9__0_1;
            public static NotOperatorMapper<bool?> <>9__0_2;
            public static FallbackMapper<bool?> <>9__0_3;
            public static Func<CriteriaOperator, bool?> <>9__0_0;
            public static Func<bool?, string, CriteriaOperator> <>9__0_4;
            public static Func<FilterRestrictions, bool> <>9__0_5;

            internal bool? <.ctor>b__0_0(CriteriaOperator filter)
            {
                BinaryOperatorMapper<bool?> binary = <>9__0_1;
                if (<>9__0_1 == null)
                {
                    BinaryOperatorMapper<bool?> local1 = <>9__0_1;
                    binary = <>9__0_1 = delegate (string name, object value, BinaryOperatorType type) {
                        bool? nullable = value as bool?;
                        if (type == BinaryOperatorType.Equal)
                        {
                            return nullable;
                        }
                        if (type == BinaryOperatorType.NotEqual)
                        {
                            bool? nullable2 = nullable;
                            if (nullable2 != null)
                            {
                                return new bool?(!nullable2.GetValueOrDefault());
                            }
                        }
                        return null;
                    };
                }
                NotOperatorMapper<bool?> not = <>9__0_2;
                if (<>9__0_2 == null)
                {
                    NotOperatorMapper<bool?> local2 = <>9__0_2;
                    not = <>9__0_2 = delegate (bool? x) {
                        bool? nullable = x;
                        if (nullable != null)
                        {
                            return new bool?(!nullable.GetValueOrDefault());
                        }
                        return null;
                    };
                }
                return filter.Map<bool?>(binary, null, null, null, null, null, null, not, (<>9__0_3 ??= ((FallbackMapper<bool?>) (_ => null))), null);
            }

            internal bool? <.ctor>b__0_1(string name, object value, BinaryOperatorType type)
            {
                bool? nullable = value as bool?;
                if (type == BinaryOperatorType.Equal)
                {
                    return nullable;
                }
                if (type == BinaryOperatorType.NotEqual)
                {
                    bool? nullable2 = nullable;
                    if (nullable2 != null)
                    {
                        return new bool?(!nullable2.GetValueOrDefault());
                    }
                }
                return null;
            }

            internal bool? <.ctor>b__0_2(bool? x)
            {
                bool? nullable = x;
                if (nullable != null)
                {
                    return new bool?(!nullable.GetValueOrDefault());
                }
                return null;
            }

            internal bool? <.ctor>b__0_3(CriteriaOperator _) => 
                null;

            internal CriteriaOperator <.ctor>b__0_4(bool? value, string propertyName) => 
                (value == null) ? null : new BinaryOperator(propertyName, value.Value);

            internal bool <.ctor>b__0_5(FilterRestrictions restrictions) => 
                restrictions.Allow(BinaryOperatorType.Equal);
        }
    }
}

