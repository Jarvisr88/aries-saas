namespace DevExpress.XtraReports.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

    internal static class StringCombiner
    {
        private const string separator = "|";

        public static string Combine(IEnumerable<string> parts)
        {
            List<string> list = new List<string>();
            foreach (string str in parts)
            {
                list.Add(EscapeString(str));
            }
            return string.Join("|", list.ToArray());
        }

        private static string EscapeString(string source) => 
            source.Replace(@"\", @"\\").Replace("|", @"\|");

        [IteratorStateMachine(typeof(<Split>d__3))]
        public static IEnumerable<string> Split(string value)
        {
            <Split>d__3 d__1 = new <Split>d__3(-2);
            d__1.<>3__value = value;
            return d__1;
        }

        private static string UnescapeString(string source) => 
            source.Replace(@"\|", "|").Replace(@"\\", @"\");

        [CompilerGenerated]
        private sealed class <Split>d__3 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private string <>2__current;
            private int <>l__initialThreadId;
            private string value;
            public string <>3__value;
            private string[] <>7__wrap1;
            private int <>7__wrap2;

            [DebuggerHidden]
            public <Split>d__3(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<>7__wrap1 = Regex.Split(this.value, @"(?<![^\\](\\\\)*\\)\|");
                    this.<>7__wrap2 = 0;
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    this.<>7__wrap2++;
                }
                if (this.<>7__wrap2 >= this.<>7__wrap1.Length)
                {
                    this.<>7__wrap1 = null;
                    return false;
                }
                string source = this.<>7__wrap1[this.<>7__wrap2];
                this.<>2__current = StringCombiner.UnescapeString(source);
                this.<>1__state = 1;
                return true;
            }

            [DebuggerHidden]
            IEnumerator<string> IEnumerable<string>.GetEnumerator()
            {
                StringCombiner.<Split>d__3 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new StringCombiner.<Split>d__3(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                d__.value = this.<>3__value;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<System.String>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            string IEnumerator<string>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

