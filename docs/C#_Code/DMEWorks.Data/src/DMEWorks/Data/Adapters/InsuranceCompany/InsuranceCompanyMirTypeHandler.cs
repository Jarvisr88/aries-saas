namespace DMEWorks.Data.Adapters.InsuranceCompany
{
    using Dapper;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class InsuranceCompanyMirTypeHandler : SqlMapper.TypeHandler<InsuranceCompanyMir?>
    {
        private static readonly InsuranceCompanyMir[] values = ((InsuranceCompanyMir[]) Enum.GetValues(typeof(InsuranceCompanyMir)));

        private static string DbName(InsuranceCompanyMir value)
        {
            if (value != InsuranceCompanyMir.MedicareNumber)
            {
                throw new ArgumentOutOfRangeException();
            }
            return "MedicareNumber";
        }

        public override InsuranceCompanyMir? Parse(object value)
        {
            string str = value as string;
            if (str == null)
            {
                return null;
            }
            InsuranceCompanyMir mir = (InsuranceCompanyMir) 0;
            char[] separator = new char[] { ',' };
            string[] strArray = str.Split(separator);
            int index = 0;
            while (index < strArray.Length)
            {
                string text1 = strArray[index];
                InsuranceCompanyMir[] values = InsuranceCompanyMirTypeHandler.values;
                int num2 = 0;
                while (true)
                {
                    if (num2 >= values.Length)
                    {
                        index++;
                        break;
                    }
                    InsuranceCompanyMir mir2 = values[num2];
                    if (str.Equals(DbName(mir2), StringComparison.OrdinalIgnoreCase))
                    {
                        mir |= mir2;
                    }
                    num2++;
                }
            }
            return new InsuranceCompanyMir?(mir);
        }

        public override void SetValue(IDbDataParameter parameter, InsuranceCompanyMir? value)
        {
            if (value == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                Func<InsuranceCompanyMir, string> selector = <>c.<>9__4_1;
                if (<>c.<>9__4_1 == null)
                {
                    Func<InsuranceCompanyMir, string> local1 = <>c.<>9__4_1;
                    selector = <>c.<>9__4_1 = v => DbName(v);
                }
                IEnumerable<string> values = (from v in InsuranceCompanyMirTypeHandler.values
                    where (v != ((InsuranceCompanyMir) 0)) && ((((InsuranceCompanyMir) value.Value) & v) == v)
                    select v).Select<InsuranceCompanyMir, string>(selector);
                parameter.Value = string.Join(",", values);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly InsuranceCompanyMirTypeHandler.<>c <>9 = new InsuranceCompanyMirTypeHandler.<>c();
            public static Func<InsuranceCompanyMir, string> <>9__4_1;

            internal string <SetValue>b__4_1(InsuranceCompanyMir v) => 
                InsuranceCompanyMirTypeHandler.DbName(v);
        }
    }
}

