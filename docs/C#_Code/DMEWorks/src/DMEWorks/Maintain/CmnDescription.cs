namespace DMEWorks.Maintain
{
    using DMEWorks;
    using System;
    using System.Runtime.CompilerServices;

    public class CmnDescription
    {
        public CmnDescription(DmercType type)
        {
            this._Type = type;
        }

        public static int Compare(CmnDescription x, CmnDescription y)
        {
            int num;
            if (ReferenceEquals(x, y))
            {
                num = 0;
            }
            else if (x == null)
            {
                num = -1;
            }
            else if (y == null)
            {
                num = 1;
            }
            else
            {
                int num2 = string.Compare(DmercHelper.GetStatus(x.Type), DmercHelper.GetStatus(y.Type));
                num2 ??= string.Compare(DmercHelper.GetVersion(x.Type), DmercHelper.GetVersion(y.Type));
                num = num2;
            }
            return num;
        }

        public DmercType Type { get; }

        public string DbType =>
            DmercHelper.Dmerc2String(this.Type);

        public string Description =>
            DmercHelper.GetDescription(this.Type);
    }
}

