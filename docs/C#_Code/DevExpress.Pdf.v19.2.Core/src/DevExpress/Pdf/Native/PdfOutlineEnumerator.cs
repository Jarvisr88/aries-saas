namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class PdfOutlineEnumerator : IEnumerable
    {
        private readonly PdfOutline outline;

        public PdfOutlineEnumerator(PdfOutline outline)
        {
            this.outline = outline;
        }

        [IteratorStateMachine(typeof(<System-Collections-IEnumerable-GetEnumerator>d__2))]
        IEnumerator IEnumerable.GetEnumerator()
        {
            IEnumerator enumerator;
            yield return this.outline;
            PdfOutline outline = this.outline.First;
            if (outline == null)
            {
                goto TR_0004;
            }
            else
            {
                enumerator = ((IEnumerable) new PdfOutlineEnumerator(outline)).GetEnumerator();
            }
        Label_PostSwitchInIterator:;
            if (enumerator.MoveNext())
            {
                PdfOutline current = (PdfOutline) enumerator.Current;
                yield return current;
                goto Label_PostSwitchInIterator;
            }
            else
            {
                enumerator = null;
            }
        TR_0004:
            yield break;
        }

    }
}

