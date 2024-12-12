namespace DevExpress.Data.Helpers
{
    using System;

    public abstract class GenericInvoker<Signature, ImplType> where ImplType: GenericInvoker<Signature, ImplType>
    {
        private static readonly object _Store;

        static GenericInvoker();
        protected GenericInvoker();
        protected abstract Signature CreateInvoker();
        public static Signature GetInvoker(Type genericArg);
        public static Signature GetInvoker(params Type[] genericArgs);
        public static Signature GetInvoker(Type genericArg1, Type genericArg2);
        public static Signature GetInvoker(Type genericArg1, Type genericArg2, Type genericArg3);
    }
}

