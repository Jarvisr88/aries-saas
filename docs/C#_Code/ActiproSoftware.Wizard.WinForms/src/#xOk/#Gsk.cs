namespace #xOk
{
    using #H;
    using ActiproSoftware.ComponentModel;
    using ActiproSoftware.WinUICore.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    internal class #Gsk : DisposableObject
    {
        private Dictionary<Color, SolidBrush> #isk;
        private Dictionary<Color, Pen> #jsk;
        private WeakReference #ksk;
        private #Tsk<Color> #msk = new #Tsk<Color>();
        private #Tsk<string> #nsk = new #Tsk<string>();
        private Dictionary<#juk, #vDd> #vOk = new Dictionary<#juk, #vDd>();
        private #Tsk<float> #osk = new #Tsk<float>();

        public void #6e()
        {
            this.#ssk();
            this.#rsk();
            this.#vsk();
        }

        public string #Ask(int #ahb) => 
            this.#nsk[#ahb];

        public int #Bsk(string #S7b) => 
            !string.IsNullOrEmpty(#S7b) ? this.#nsk.#FOk(#S7b) : 0;

        public float #Csk(int #ahb) => 
            this.#osk[#ahb];

        public int #Dsk(float #KK)
        {
            #KK *= this.FontScale;
            return ((#KK > 0.0) ? this.#osk.#FOk(#KK) : 0);
        }

        private void #rsk()
        {
            if (this.#isk != null)
            {
                using (Dictionary<Color, SolidBrush>.ValueCollection.Enumerator enumerator = this.#isk.Values.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        enumerator.Current.Dispose();
                    }
                }
                this.#isk.Clear();
            }
        }

        private void #ssk()
        {
            if (this.#jsk != null)
            {
                using (Dictionary<Color, Pen>.ValueCollection.Enumerator enumerator = this.#jsk.Values.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        enumerator.Current.Dispose();
                    }
                }
                this.#jsk.Clear();
            }
        }

        private void #vsk()
        {
            using (Dictionary<#juk, #vDd>.ValueCollection.Enumerator enumerator = this.#vOk.Values.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    enumerator.Current.Dispose();
                }
            }
            this.#vOk.Clear();
            this.#msk.#6e();
            this.#nsk.#6e();
            this.#osk.#6e();
        }

        public #vDd #wOk(#juk #zD)
        {
            #vDd dd;
            #zD = #zD.#cuk();
            if (!this.#vOk.TryGetValue(#zD, out dd))
            {
                float fontSize = this.#Csk(#zD.FontSizeIndex);
                dd = new #vDd(this.#Ask(#zD.FontFamilyIndex), fontSize, (#zD.IsItalic ? FontStyle.Italic : FontStyle.Regular) | (#zD.IsBold ? FontStyle.Bold : FontStyle.Regular));
                this.#vOk[#zD] = dd;
            }
            return dd;
        }

        public Color #ysk(int #ahb) => 
            this.#msk[#ahb];

        public unsafe int #zsk(Color? #hwf) => 
            (&#hwf != null) ? this.#msk.#FOk(#hwf.Value) : 0;

        public #Gsk(ICanvas canvas, float fontScale)
        {
            if (canvas == null)
            {
                throw new ArgumentNullException(#G.#eg(0x60b));
            }
            this.#ksk = new WeakReference(canvas);
            this.FontScale = fontScale;
        }

        protected override void Dispose(bool #Fee)
        {
            if (#Fee)
            {
                this.#6e();
            }
            base.Dispose(#Fee);
        }

        public SolidBrush GetSolidColorBrush(Color #eUb)
        {
            SolidBrush brush;
            if (this.#isk == null)
            {
                this.#isk = new Dictionary<Color, SolidBrush>();
            }
            if (!this.#isk.TryGetValue(#eUb, out brush))
            {
                brush = new SolidBrush(#eUb);
                this.#isk[#eUb] = brush;
            }
            return brush;
        }

        public Pen GetSquiggleLinePen(Color #eUb)
        {
            Pen pen;
            if (this.#jsk == null)
            {
                this.#jsk = new Dictionary<Color, Pen>();
            }
            if (!this.#jsk.TryGetValue(#eUb, out pen))
            {
                pen = new Pen(this.GetSolidColorBrush(#eUb), 0.8f) {
                    LineJoin = LineJoin.Round,
                    MiterLimit = 10f,
                    StartCap = LineCap.Flat,
                    EndCap = LineCap.Flat
                };
                this.#jsk[#eUb] = pen;
            }
            return pen;
        }

        internal float FontScale { get; private set; }

        public ICanvas Canvas
        {
            get
            {
                if (this.#ksk != null)
                {
                    if (this.#ksk.IsAlive)
                    {
                        return (this.#ksk.Target as ICanvas);
                    }
                    this.#ksk = null;
                }
                return null;
            }
        }
    }
}

