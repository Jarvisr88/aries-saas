namespace DevExpress.Xpf.Layout.Core.Base
{
    using System;
    using System.Diagnostics;

    public class AssertionException : BaseException
    {
        public static readonly string AssertionFails = "Assertion Fails";

        public AssertionException(string message) : base(message)
        {
        }

        [Conditional("DEBUG"), Conditional("DEBUGTEST")]
        public static void AreEqual<T>(T expected, T actual)
        {
            if (expected == null)
            {
                if (actual != null)
                {
                    throw new AssertionException(AssertionFails);
                }
            }
            else if (!expected.Equals(actual))
            {
                throw new AssertionException(AssertionFails);
            }
        }

        [Conditional("DEBUG"), Conditional("DEBUGTEST")]
        public static void Is<T>(object obj)
        {
            if (!(obj is T))
            {
                throw new AssertionException(AssertionFails);
            }
        }

        [Conditional("DEBUG"), Conditional("DEBUGTEST")]
        public static void Is<T>(object obj, string message)
        {
            if (!(obj is T))
            {
                throw new AssertionException(message);
            }
        }

        [Conditional("DEBUG"), Conditional("DEBUGTEST")]
        public static void IsFalse(bool condition)
        {
            if (condition)
            {
                throw new AssertionException(AssertionFails);
            }
        }

        [Conditional("DEBUG"), Conditional("DEBUGTEST")]
        public static void IsNotDefault<T>(T value) where T: struct
        {
            T local = default(T);
            if (value.Equals(local))
            {
                throw new AssertionException(AssertionFails);
            }
        }

        [Conditional("DEBUG"), Conditional("DEBUGTEST")]
        public static void IsNotNull(object obj)
        {
            if (obj == null)
            {
                throw new AssertionException(AssertionFails);
            }
        }

        [Conditional("DEBUG"), Conditional("DEBUGTEST")]
        public static void IsNotNull(object obj, string message)
        {
            if (obj == null)
            {
                throw new AssertionException(message);
            }
        }

        [Conditional("DEBUG"), Conditional("DEBUGTEST")]
        public static void IsNull(object obj)
        {
            if (obj != null)
            {
                throw new AssertionException(AssertionFails);
            }
        }

        [Conditional("DEBUG"), Conditional("DEBUGTEST")]
        public static void IsNull(object obj, string message)
        {
            if (obj != null)
            {
                throw new AssertionException(message);
            }
        }

        [Conditional("DEBUG"), Conditional("DEBUGTEST")]
        public static void IsTrue(bool condition)
        {
            if (!condition)
            {
                throw new AssertionException(AssertionFails);
            }
        }

        [Conditional("DEBUG"), Conditional("DEBUGTEST")]
        public static void IsTrue(bool condition, string message)
        {
            if (!condition)
            {
                throw new AssertionException(message);
            }
        }
    }
}

