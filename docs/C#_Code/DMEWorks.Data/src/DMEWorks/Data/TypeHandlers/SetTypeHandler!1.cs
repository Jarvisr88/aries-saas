namespace DMEWorks.Data.TypeHandlers
{
    using Dapper;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class SetTypeHandler<TEnum> : SqlMapper.TypeHandler<TEnum?> where TEnum: struct
    {
        protected SetTypeHandler()
        {
        }

        protected abstract IEnumerable<FlagDescription<TEnum>> GetFlags();
        protected abstract string GetText(TEnum value);
        public override TEnum? Parse(object value)
        {
            string str = value as string;
            if (str != null)
            {
                using (IEnumerator<FlagDescription<TEnum>> enumerator = this.GetFlags().GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        FlagDescription<TEnum> current = enumerator.Current;
                        if (str.Equals(this.GetText(current.Flag), StringComparison.OrdinalIgnoreCase))
                        {
                            return new TEnum?(current.Flag);
                        }
                    }
                }
            }
            return null;
        }

        public override void SetValue(IDbDataParameter parameter, TEnum? value)
        {
            if (value != null)
            {
                parameter.Value = this.GetText(value.Value);
            }
            else
            {
                parameter.Value = DBNull.Value;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        protected struct FlagDescription
        {
            public FlagDescription(TEnum flag, string text)
            {
                this.<Flag>k__BackingField = flag;
                if (text == null)
                {
                    string local1 = text;
                    throw new ArgumentNullException("text");
                }
                this.<Text>k__BackingField = text;
            }

            public TEnum Flag { get; }
            public string Text { get; }
        }
    }
}

