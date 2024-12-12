namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public abstract class Either<TLeft, TRight>
    {
        private Either()
        {
        }

        public override bool Equals(object obj)
        {
            Either<TLeft, TRight> e = obj as Either<TLeft, TRight>;
            return ((e == null) ? false : this.Match<bool>(delegate (TLeft x) {
                Func<TRight, bool> right = <>c<TLeft, TRight>.<>9__15_2;
                if (<>c<TLeft, TRight>.<>9__15_2 == null)
                {
                    Func<TRight, bool> local1 = <>c<TLeft, TRight>.<>9__15_2;
                    right = <>c<TLeft, TRight>.<>9__15_2 = _ => false;
                }
                return e.Match<bool>(y => EqualityComparer<TLeft>.Default.Equals(x, y), right);
            }, delegate (TRight x) {
                Func<TLeft, bool> left = <>c<TLeft, TRight>.<>9__15_4;
                if (<>c<TLeft, TRight>.<>9__15_4 == null)
                {
                    Func<TLeft, bool> local1 = <>c<TLeft, TRight>.<>9__15_4;
                    left = <>c<TLeft, TRight>.<>9__15_4 = _ => false;
                }
                return e.Match<bool>(left, y => EqualityComparer<TRight>.Default.Equals(x, y));
            }));
        }

        public override int GetHashCode()
        {
            Func<TLeft, int> left = <>c<TLeft, TRight>.<>9__12_0;
            if (<>c<TLeft, TRight>.<>9__12_0 == null)
            {
                Func<TLeft, int> local1 = <>c<TLeft, TRight>.<>9__12_0;
                left = <>c<TLeft, TRight>.<>9__12_0 = x => (x == null) ? 0 : x.GetHashCode();
            }
            return this.Match<int>(left, <>c<TLeft, TRight>.<>9__12_1 ??= x => ((x == null) ? 0 : x.GetHashCode()));
        }

        public bool IsLeft() => 
            this is LeftValue<TLeft, TRight>;

        public bool IsRight() => 
            this is RightValue<TLeft, TRight>;

        public static Either<TLeft, TRight> Left(TLeft value) => 
            new LeftValue<TLeft, TRight>(value);

        public void Match(Action<TLeft> left, Action<TRight> right)
        {
            LeftValue<TLeft, TRight> value2 = this as LeftValue<TLeft, TRight>;
            if (value2 != null)
            {
                left(value2.Value);
            }
            else
            {
                right((this as RightValue<TLeft, TRight>).Value);
            }
        }

        public T Match<T>(Func<TLeft, T> left, Func<TRight, T> right)
        {
            LeftValue<TLeft, TRight> value2 = this as LeftValue<TLeft, TRight>;
            return ((value2 == null) ? right((this as RightValue<TLeft, TRight>).Value) : left(value2.Value));
        }

        public static bool operator ==(Either<TLeft, TRight> a, Either<TLeft, TRight> b)
        {
            bool flag = ReferenceEquals(a, null);
            bool flag2 = ReferenceEquals(b, null);
            if (flag & flag2)
            {
                return true;
            }
            if (!(flag | flag2))
            {
                throw new NotSupportedException("Use Equals or ReferenceEquals");
            }
            return false;
        }

        public static implicit operator Either<TLeft, TRight>(TLeft val) => 
            new LeftValue<TLeft, TRight>(val);

        public static implicit operator Either<TLeft, TRight>(TRight val) => 
            new RightValue<TLeft, TRight>(val);

        public static bool operator !=(Either<TLeft, TRight> a, Either<TLeft, TRight> b) => 
            !(a == b);

        public static Either<TLeft, TRight> Right(TRight value) => 
            new RightValue<TLeft, TRight>(value);

        public override string ToString()
        {
            Func<TLeft, string> left = <>c<TLeft, TRight>.<>9__11_0;
            if (<>c<TLeft, TRight>.<>9__11_0 == null)
            {
                Func<TLeft, string> local1 = <>c<TLeft, TRight>.<>9__11_0;
                left = <>c<TLeft, TRight>.<>9__11_0 = x => "Left: " + ((x == null) ? "NULL" : x.ToString());
            }
            return this.Match<string>(left, <>c<TLeft, TRight>.<>9__11_1 ??= x => ("Right: " + ((x == null) ? "NULL" : x.ToString())));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Either<TLeft, TRight>.<>c <>9;
            public static Func<TLeft, string> <>9__11_0;
            public static Func<TRight, string> <>9__11_1;
            public static Func<TLeft, int> <>9__12_0;
            public static Func<TRight, int> <>9__12_1;
            public static Func<TRight, bool> <>9__15_2;
            public static Func<TLeft, bool> <>9__15_4;

            static <>c()
            {
                Either<TLeft, TRight>.<>c.<>9 = new Either<TLeft, TRight>.<>c();
            }

            internal bool <Equals>b__15_2(TRight _) => 
                false;

            internal bool <Equals>b__15_4(TLeft _) => 
                false;

            internal int <GetHashCode>b__12_0(TLeft x) => 
                (x == null) ? 0 : x.GetHashCode();

            internal int <GetHashCode>b__12_1(TRight x) => 
                (x == null) ? 0 : x.GetHashCode();

            internal string <ToString>b__11_0(TLeft x) => 
                "Left: " + ((x == null) ? "NULL" : x.ToString());

            internal string <ToString>b__11_1(TRight x) => 
                "Right: " + ((x == null) ? "NULL" : x.ToString());
        }

        internal class LeftValue : Either<TLeft, TRight>
        {
            internal readonly TLeft Value;

            internal LeftValue(TLeft value)
            {
                this.Value = value;
            }
        }

        internal class RightValue : Either<TLeft, TRight>
        {
            internal readonly TRight Value;

            internal RightValue(TRight value)
            {
                this.Value = value;
            }
        }
    }
}

