namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class VmlIntermediateColors : List<OfficeShadeColor>, ISupportsCopyFrom<VmlIntermediateColors>
    {
        public void CopyFrom(VmlIntermediateColors source)
        {
            Guard.ArgumentNotNull(source, "source");
            base.Clear();
            foreach (OfficeShadeColor color in source)
            {
                base.Add(new OfficeShadeColor(color.ColorRecord.Color, color.Position));
            }
        }

        [IteratorStateMachine(typeof(<OfficeShadeColorsToStrings>d__6))]
        private IEnumerable<string> OfficeShadeColorsToStrings()
        {
            <OfficeShadeColorsToStrings>d__6 d__1 = new <OfficeShadeColorsToStrings>d__6(-2);
            d__1.<>4__this = this;
            return d__1;
        }

        public override string ToString() => 
            string.Join(",", this.OfficeShadeColorsToStrings());

        public OfficeShadeColor First =>
            (base.Count == 0) ? null : base[0];

        public OfficeShadeColor Last =>
            (base.Count == 0) ? null : base[base.Count - 1];

        [CompilerGenerated]
        private sealed class <OfficeShadeColorsToStrings>d__6 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private string <>2__current;
            private int <>l__initialThreadId;
            public VmlIntermediateColors <>4__this;
            private List<OfficeShadeColor>.Enumerator <>7__wrap1;

            [DebuggerHidden]
            public <OfficeShadeColorsToStrings>d__6(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                this.<>7__wrap1.Dispose();
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<>7__wrap1 = this.<>4__this.GetEnumerator();
                        this.<>1__state = -3;
                    }
                    else if (num == 1)
                    {
                        this.<>1__state = -3;
                    }
                    else
                    {
                        return false;
                    }
                    if (!this.<>7__wrap1.MoveNext())
                    {
                        this.<>m__Finally1();
                        this.<>7__wrap1 = new List<OfficeShadeColor>.Enumerator();
                        flag = false;
                    }
                    else
                    {
                        OfficeShadeColor current = this.<>7__wrap1.Current;
                        string str = ((current.Position == 0.0) || (current.Position == 1.0)) ? ((int) current.Position).ToString(CultureInfo.InvariantCulture) : (((int) Math.Round((double) (current.Position * 65536.0))).ToString(CultureInfo.InvariantCulture) + "f");
                        this.<>2__current = str + " " + DXColor.ToHtml(current.ColorRecord.Color);
                        this.<>1__state = 1;
                        flag = true;
                    }
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<string> IEnumerable<string>.GetEnumerator()
            {
                VmlIntermediateColors.<OfficeShadeColorsToStrings>d__6 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new VmlIntermediateColors.<OfficeShadeColorsToStrings>d__6(0) {
                        <>4__this = this.<>4__this
                    };
                }
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
                int num = this.<>1__state;
                if ((num == -3) || (num == 1))
                {
                    try
                    {
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            string IEnumerator<string>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

