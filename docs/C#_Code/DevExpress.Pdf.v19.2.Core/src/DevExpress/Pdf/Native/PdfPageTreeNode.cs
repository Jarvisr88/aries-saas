namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class PdfPageTreeNode : PdfPageTreeObject, IEnumerable<PdfPage>, IEnumerable
    {
        internal const string PageTreeNodeType = "Pages";
        internal const string KidsDictionaryKey = "Kids";
        private const string countDictionaryKey = "Count";
        private IList<PdfPageTreeObject> kids;
        private readonly int count;

        internal PdfPageTreeNode(PdfPageTreeNode parent, PdfReaderDictionary dictionary) : base(parent, dictionary)
        {
            IList<object> array = dictionary.GetArray("Kids");
            int? integer = dictionary.GetInteger("Count");
            if ((array == null) || (integer == null))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.count = integer.Value;
            this.kids = new PdfPageTreeObjectList(array, dictionary.Objects, this);
        }

        internal PdfPageTreeNode(PdfDocumentCatalog documentCatalog, PdfRectangle mediaBox, PdfRectangle cropBox, int rotate, IEnumerable<PdfPage> pages) : base(documentCatalog, mediaBox, cropBox, rotate)
        {
            this.kids = new List<PdfPageTreeObject>(pages);
            foreach (PdfPage page in pages)
            {
                page.Parent = this;
            }
            this.count = this.kids.Count;
        }

        internal void RemovePage(PdfPage page)
        {
            this.kids.Remove(page);
        }

        [IteratorStateMachine(typeof(<System-Collections-Generic-IEnumerable<DevExpress-Pdf-PdfPage>-GetEnumerator>d__11))]
        IEnumerator<PdfPage> IEnumerable<PdfPage>.GetEnumerator()
        {
            <System-Collections-Generic-IEnumerable<DevExpress-Pdf-PdfPage>-GetEnumerator>d__11 d__1 = new <System-Collections-Generic-IEnumerable<DevExpress-Pdf-PdfPage>-GetEnumerator>d__11(0);
            d__1.<>4__this = this;
            return d__1;
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            ((IEnumerable<PdfPage>) this).GetEnumerator();

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            object obj2 = base.ToWritableObject(objects);
            PdfWriterDictionary dictionary = obj2 as PdfWriterDictionary;
            if (dictionary != null)
            {
                dictionary.AddName("Type", "Pages");
                dictionary.Add("Count", this.kids.Count);
                dictionary.Add("Kids", new PdfWritableObjectArray((IEnumerable<PdfObject>) this.kids, objects));
            }
            return obj2;
        }

        public int Count =>
            this.count;

        [CompilerGenerated]
        private sealed class <System-Collections-Generic-IEnumerable<DevExpress-Pdf-PdfPage>-GetEnumerator>d__11 : IEnumerator<PdfPage>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private PdfPage <>2__current;
            public PdfPageTreeNode <>4__this;
            private PdfPage <page>5__1;
            private IEnumerator<PdfPageTreeObject> <>7__wrap1;
            private IEnumerator<PdfPage> <>7__wrap2;

            [DebuggerHidden]
            public <System-Collections-Generic-IEnumerable<DevExpress-Pdf-PdfPage>-GetEnumerator>d__11(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                if (this.<>7__wrap1 != null)
                {
                    this.<>7__wrap1.Dispose();
                }
            }

            private void <>m__Finally2()
            {
                this.<>1__state = -3;
                if (this.<>7__wrap2 != null)
                {
                    this.<>7__wrap2.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    switch (this.<>1__state)
                    {
                        case 0:
                            this.<>1__state = -1;
                            this.<>7__wrap1 = this.<>4__this.kids.GetEnumerator();
                            this.<>1__state = -3;
                            break;

                        case 1:
                            this.<>1__state = -4;
                            goto TR_0007;

                        case 2:
                            this.<>1__state = -3;
                            goto TR_0004;

                        default:
                            return false;
                    }
                    goto TR_000F;
                TR_0004:
                    this.<page>5__1 = null;
                    goto TR_000F;
                TR_0007:
                    if (this.<>7__wrap2.MoveNext())
                    {
                        PdfPage current = this.<>7__wrap2.Current;
                        this.<>2__current = current;
                        this.<>1__state = 1;
                        flag = true;
                    }
                    else
                    {
                        this.<>m__Finally2();
                        this.<>7__wrap2 = null;
                        goto TR_0004;
                    }
                    return flag;
                TR_000F:
                    while (true)
                    {
                        if (this.<>7__wrap1.MoveNext())
                        {
                            PdfPageTreeObject current = this.<>7__wrap1.Current;
                            this.<page>5__1 = current as PdfPage;
                            if (this.<page>5__1 != null)
                            {
                                this.<>2__current = this.<page>5__1;
                                this.<>1__state = 2;
                                return true;
                            }
                            else
                            {
                                PdfPageTreeNode node = current as PdfPageTreeNode;
                                if (node == null)
                                {
                                    goto TR_0004;
                                }
                                else
                                {
                                    this.<>7__wrap2 = ((IEnumerable<PdfPage>) node).GetEnumerator();
                                    this.<>1__state = -4;
                                }
                            }
                        }
                        else
                        {
                            this.<>m__Finally1();
                            this.<>7__wrap1 = null;
                            return false;
                        }
                        break;
                    }
                    goto TR_0007;
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                switch (num)
                {
                    case -4:
                    case -3:
                    case 1:
                    case 2:
                        try
                        {
                            if ((num == -4) || (num == 1))
                            {
                                try
                                {
                                }
                                finally
                                {
                                    this.<>m__Finally2();
                                }
                            }
                        }
                        finally
                        {
                            this.<>m__Finally1();
                        }
                        break;

                    case -2:
                    case -1:
                    case 0:
                        break;

                    default:
                        return;
                }
            }

            PdfPage IEnumerator<PdfPage>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

