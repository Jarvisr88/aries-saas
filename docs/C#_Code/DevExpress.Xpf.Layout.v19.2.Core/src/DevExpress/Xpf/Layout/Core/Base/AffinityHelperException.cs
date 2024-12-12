namespace DevExpress.Xpf.Layout.Core.Base
{
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.Platform;
    using System;

    public class AffinityHelperException : BaseException
    {
        public static readonly string BaseTypeIsWrong = "Element Type Is Not Supported";

        protected AffinityHelperException(string message) : base(message)
        {
        }

        public static bool Assert(ILayoutElement element)
        {
            if (!(element is BaseLayoutElement))
            {
                throw new AffinityHelperException(BaseTypeIsWrong);
            }
            return true;
        }

        public static bool Assert(IView element)
        {
            if (!(element is BaseView))
            {
                throw new AffinityHelperException(BaseTypeIsWrong);
            }
            return true;
        }
    }
}

