namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal sealed class CreditCardAttribute : DXDataTypeAttribute
    {
        public CreditCardAttribute(Func<string> errorMessageAccessor) : this(errorMessageAccessor, func1, PropertyDataType.Custom)
        {
            Func<string> func1 = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<string> local1 = <>c.<>9__0_0;
                func1 = <>c.<>9__0_0 = () => DataAnnotationsResourcesResolver.CreditCardAttribute_Invalid;
            }
        }

        protected override bool IsValid(object value)
        {
            bool flag2;
            if (value == null)
            {
                return true;
            }
            string str = value as string;
            if (str == null)
            {
                return false;
            }
            int num = 0;
            bool flag = false;
            using (IEnumerator<char> enumerator = str.Replace("-", "").Replace(" ", "").Reverse<char>().GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        char current = enumerator.Current;
                        if ((current >= '0') && (current <= '9'))
                        {
                            int num2 = (current - '0') * (flag ? 2 : 1);
                            flag = !flag;
                            while (num2 > 0)
                            {
                                num += num2 % 10;
                                num2 /= 10;
                            }
                            continue;
                        }
                        flag2 = false;
                    }
                    else
                    {
                        return ((num % 10) == 0);
                    }
                    break;
                }
            }
            return flag2;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CreditCardAttribute.<>c <>9 = new CreditCardAttribute.<>c();
            public static Func<string> <>9__0_0;

            internal string <.ctor>b__0_0() => 
                DataAnnotationsResourcesResolver.CreditCardAttribute_Invalid;
        }
    }
}

