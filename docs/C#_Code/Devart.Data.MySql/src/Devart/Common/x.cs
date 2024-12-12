namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    internal class x : IEnumerable
    {
        private List<Devart.Common.x.b> a = new List<Devart.Common.x.b>(100);
        internal const int b = 1;
        internal const int c = 2;
        internal const int d = 3;

        public IEnumerator a() => 
            new Devart.Common.x.a(this.a);

        public virtual void a(object A_0)
        {
            this.c(A_0);
        }

        protected void a(object A_0, int A_1)
        {
            for (int i = 0; i < this.a.Count; i++)
            {
                Devart.Common.x.b b = this.a[i];
                if (b.a() == null)
                {
                    this.a[i] = new Devart.Common.x.b(A_0, A_1);
                    return;
                }
            }
            this.a.Add(new Devart.Common.x.b(A_0, A_1));
        }

        public void a(int A_0, int A_1, object A_2)
        {
            for (int i = 0; i < this.a.Count; i++)
            {
                this.a[i].a(A_0, A_1, A_2, this);
            }
        }

        protected virtual bool a(int A_0, object A_1, int A_2, object A_3)
        {
            ((IDisposable) A_1).Dispose();
            return false;
        }

        public void b()
        {
            this.a.Clear();
        }

        public void b(object A_0)
        {
            this.b(A_0, 0);
        }

        public virtual void b(object A_0, int A_1)
        {
            this.a(A_0, A_1);
        }

        protected void c(object A_0)
        {
            for (int i = 0; i < this.a.Count; i++)
            {
                Devart.Common.x.b b = this.a[i];
                if (A_0 == b.a())
                {
                    b = new Devart.Common.x.b();
                    this.a[i] = b;
                    return;
                }
            }
        }

        private sealed class a : IEnumerator
        {
            private int a;
            private readonly List<Devart.Common.x.b> b;

            internal a(List<Devart.Common.x.b> A_0)
            {
                this.b = A_0;
                this.a = -1;
            }

            private void a()
            {
                this.a = -1;
            }

            private bool b()
            {
                for (int i = this.a + 1; i < this.b.Count; i++)
                {
                    this.a = i;
                    Devart.Common.x.b b = this.b[i];
                    if (b.c())
                    {
                        return true;
                    }
                }
                return false;
            }

            [__DynamicallyInvokable]
            private object System.Collections.IEnumerator.Current =>
                this.b[this.a].a();
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct b
        {
            private int a;
            private WeakReference b;
            public b(object A_0, int A_1)
            {
                this.a = A_1;
                this.b = null;
                this.a(A_0);
            }

            public bool a(int A_0, int A_1, object A_2, Devart.Common.x A_3)
            {
                if (!this.c())
                {
                    return true;
                }
                if ((A_1 == 0) || (A_1 == this.a))
                {
                    object obj2 = this.a();
                    if ((obj2 != null) && !A_3.a(A_0, obj2, this.a, A_2))
                    {
                        this.a = 0;
                        this.b = null;
                    }
                }
                return false;
            }

            public bool c() => 
                this.b != null;

            public int b() => 
                this.a;

            public void a(int A_0)
            {
                this.a = A_0;
            }

            public object a() => 
                Utils.GetWeakTarget(this.b);

            public void a(object A_0)
            {
                Utils.SetWeakTarget(ref this.b, A_0);
            }
        }
    }
}

