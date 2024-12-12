namespace Devart.Common
{
    using System;

    internal sealed class n
    {
        private n()
        {
        }

        internal static string a(object A_0)
        {
            string str;
            Utils.CheckArgumentNull(A_0, "value");
            try
            {
                str = ((IConvertible) A_0).ToString(null);
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException(g.a("ConvertFailed", A_0.GetType(), typeof(string)), exception);
            }
            return str;
        }

        internal static int b(object A_0)
        {
            int num;
            Utils.CheckArgumentNull(A_0, "value");
            try
            {
                num = ((IConvertible) A_0).ToInt32(null);
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException(g.a("ConvertFailed", A_0.GetType(), typeof(int)), exception);
            }
            return num;
        }

        internal static bool c(object A_0)
        {
            Utils.CheckArgumentNull(A_0, "value");
            string str = A_0 as string;
            if (str == null)
            {
                bool flag;
                try
                {
                    flag = ((IConvertible) A_0).ToBoolean(null);
                }
                catch (Exception exception)
                {
                    throw new InvalidOperationException(g.a("ConvertFailed", A_0.GetType(), typeof(bool)), exception);
                }
                return flag;
            }
            if (Utils.CompareInvariant(str, "true") || Utils.CompareInvariant(str, "yes"))
            {
                return true;
            }
            if (Utils.CompareInvariant(str, "false") || Utils.CompareInvariant(str, "no"))
            {
                return false;
            }
            string str2 = str.Trim();
            return (Utils.CompareInvariant(str2, "true") || (Utils.CompareInvariant(str2, "yes") || (!Utils.CompareInvariant(str2, "false") && (!Utils.CompareInvariant(str2, "no") && bool.Parse(str)))));
        }
    }
}

