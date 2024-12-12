namespace DevExpress.Text.Fonts
{
    using DevExpress.DirectX.Common.DirectWrite;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DXCluster
    {
        public IList<DXGlyph> Glyphs { get; }
        public StringView Text { get; }
        public float Width { get; }
        public DXLineBreakpoint Breakpoint { get; }
        public byte BidiLevel { get; }
        public bool IsTabCluster { get; }
        public DXCluster(DXGlyph glyph, StringView text, DXLineBreakpoint breakpoint, byte bidiLevel, bool isTabCluster) : this(new OneElementList<DXGlyph>(glyph), text, breakpoint, bidiLevel, isTabCluster)
        {
        }

        public DXCluster(IList<DXGlyph> glyphs, StringView text, DXLineBreakpoint breakpoint, byte bidiLevel, bool isTabCluster = false)
        {
            if (((bidiLevel % 2) != 0) && (glyphs.Count > 1))
            {
                glyphs = glyphs.Reverse<DXGlyph>().ToList<DXGlyph>();
            }
            this.<Glyphs>k__BackingField = glyphs;
            this.<Text>k__BackingField = text;
            this.<Breakpoint>k__BackingField = breakpoint;
            this.<BidiLevel>k__BackingField = bidiLevel;
            int count = glyphs.Count;
            this.<Width>k__BackingField = 0f;
            for (int i = 0; i < count; i++)
            {
                DXGlyph glyph = glyphs[i];
                this.<Width>k__BackingField = this.Width + glyph.Advance;
            }
            this.<IsTabCluster>k__BackingField = isTabCluster;
        }

        public DXCluster(IList<DXGlyph> glyphs, StringView text, DWRITE_LINE_BREAKPOINT breakpoint, byte bidiLevel, bool isTabCluster = false) : this(glyphs, text, new DXLineBreakpoint(breakpoint), bidiLevel, isTabCluster)
        {
        }
        private class OneElementList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
        {
            private readonly T value;

            public OneElementList(T value)
            {
                this.value = value;
            }

            public void Add(T item)
            {
                throw new NotSupportedException();
            }

            public void Clear()
            {
                throw new NotSupportedException();
            }

            public bool Contains(T item) => 
                this.value.Equals(item);

            public void CopyTo(T[] array, int arrayIndex)
            {
                throw new NotSupportedException();
            }

            [IteratorStateMachine(typeof(<GetEnumerator>d__13<>))]
            public IEnumerator<T> GetEnumerator()
            {
                <GetEnumerator>d__13<T> d__1 = new <GetEnumerator>d__13<T>(0);
                d__1.<>4__this = (DXCluster.OneElementList<T>) this;
                return d__1;
            }

            public int IndexOf(T item)
            {
                throw new NotSupportedException();
            }

            public void Insert(int index, T item)
            {
                throw new NotImplementedException();
            }

            public bool Remove(T item)
            {
                throw new NotSupportedException();
            }

            public void RemoveAt(int index)
            {
                throw new NotSupportedException();
            }

            IEnumerator IEnumerable.GetEnumerator() => 
                this.GetEnumerator();

            public int Count =>
                1;

            public bool IsReadOnly =>
                true;

            public T this[int index]
            {
                get => 
                    this.value;
                set
                {
                    throw new NotSupportedException();
                }
            }

            [CompilerGenerated]
            private sealed class <GetEnumerator>d__13 : IEnumerator<T>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private T <>2__current;
                public DXCluster.OneElementList<T> <>4__this;

                [DebuggerHidden]
                public <GetEnumerator>d__13(int <>1__state)
                {
                    this.<>1__state = <>1__state;
                }

                private bool MoveNext()
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<>2__current = this.<>4__this.value;
                        this.<>1__state = 1;
                        return true;
                    }
                    if (num == 1)
                    {
                        this.<>1__state = -1;
                    }
                    return false;
                }

                [DebuggerHidden]
                void IEnumerator.Reset()
                {
                    throw new NotSupportedException();
                }

                [DebuggerHidden]
                void IDisposable.Dispose()
                {
                }

                T IEnumerator<T>.Current =>
                    this.<>2__current;

                object IEnumerator.Current =>
                    this.<>2__current;
            }
        }
    }
}

