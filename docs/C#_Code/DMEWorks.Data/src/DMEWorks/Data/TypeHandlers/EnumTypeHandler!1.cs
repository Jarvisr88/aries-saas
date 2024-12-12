namespace DMEWorks.Data.TypeHandlers
{
    using Dapper;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public abstract class EnumTypeHandler<TEnum> : SqlMapper.TypeHandler<TEnum?> where TEnum: struct
    {
        protected EnumTypeHandler()
        {
        }

        protected abstract string GetText(TEnum value);
        protected abstract IEnumerable<TEnum> GetValues();
        public override TEnum? Parse(object value)
        {
            string str = value as string;
            if (str != null)
            {
                using (IEnumerator<TEnum> enumerator = this.GetValues().GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        TEnum current = enumerator.Current;
                        if (str.Equals(this.GetText(current), StringComparison.OrdinalIgnoreCase))
                        {
                            return new TEnum?(current);
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
    }
}

