namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Pdf3dStream : Pdf3dData
    {
        private readonly string subtype;
        private readonly IList<Pdf3dView> views;
        private readonly Pdf3dView defaultView;
        private readonly PdfDeferredSortedDictionary<string, object> resources;
        private readonly Pdf3dAnimationStyle animationStyle;
        private readonly byte[] data;
        private readonly byte[] onInstatinate;

        public Pdf3dStream(PdfReaderStream stream, PdfPage page) : base(stream.Dictionary)
        {
            object obj2;
            PdfReaderDictionary dictionary = stream.Dictionary;
            PdfObjectCollection objects = dictionary.Objects;
            this.subtype = dictionary.GetName("Subtype");
            this.views = dictionary.GetArray<Pdf3dView>("VA", delegate (object value) {
                Func<PdfReaderDictionary, Pdf3dView> <>9__1;
                Func<PdfReaderDictionary, Pdf3dView> create = <>9__1;
                if (<>9__1 == null)
                {
                    Func<PdfReaderDictionary, Pdf3dView> local1 = <>9__1;
                    create = <>9__1 = dict => new Pdf3dView(dict, page);
                }
                return objects.GetObject<Pdf3dView>(value, create);
            });
            this.defaultView = Pdf3dView.GetDefaultView(this, dictionary, page);
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("Resources");
            if (dictionary2 != null)
            {
                PdfCreateTreeElementAction<object> createElement = <>c.<>9__17_2;
                if (<>c.<>9__17_2 == null)
                {
                    PdfCreateTreeElementAction<object> local1 = <>c.<>9__17_2;
                    createElement = <>c.<>9__17_2 = (o, v) => o.TryResolve(v, null);
                }
                this.resources = PdfNameTreeNode<object>.Parse(dictionary2, createElement);
            }
            this.data = stream.UncompressedData;
            PdfReaderStream stream2 = dictionary.GetStream("OnInstantiate");
            if (stream2 != null)
            {
                this.onInstatinate = stream2.UncompressedData;
            }
            if (dictionary.TryGetValue("AN", out obj2))
            {
                Func<PdfReaderDictionary, Pdf3dAnimationStyle> create = <>c.<>9__17_3;
                if (<>c.<>9__17_3 == null)
                {
                    Func<PdfReaderDictionary, Pdf3dAnimationStyle> local2 = <>c.<>9__17_3;
                    create = <>c.<>9__17_3 = dict => new Pdf3dAnimationStyle(dict);
                }
                this.animationStyle = objects.GetObject<Pdf3dAnimationStyle>(obj2, create);
            }
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            dictionary.AddName("Subtype", this.subtype);
            dictionary.AddList<Pdf3dView>("VA", this.views);
            dictionary.Add("DV", this.defaultView);
            dictionary.AddIfPresent("Resources", PdfNameTreeNode<object>.Write(objects, this.resources));
            if (this.onInstatinate != null)
            {
                dictionary.Add("OnInstantiate", objects.AddStream(this.onInstatinate));
            }
            dictionary.Add("AN", this.animationStyle);
            return dictionary;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects) => 
            new PdfArrayCompressedData(this.data).CreateWriterStream(this.CreateDictionary(objects));

        public override Pdf3dDataType Type =>
            Pdf3dDataType.Stream;

        public string Subtype =>
            this.subtype;

        public IList<Pdf3dView> Views =>
            this.views;

        public Pdf3dView DefaultView =>
            this.defaultView;

        public IDictionary<string, object> Resources =>
            this.resources;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Pdf3dStream.<>c <>9 = new Pdf3dStream.<>c();
            public static PdfCreateTreeElementAction<object> <>9__17_2;
            public static Func<PdfReaderDictionary, Pdf3dAnimationStyle> <>9__17_3;

            internal object <.ctor>b__17_2(PdfObjectCollection o, object v) => 
                o.TryResolve(v, null);

            internal Pdf3dAnimationStyle <.ctor>b__17_3(PdfReaderDictionary dict) => 
                new Pdf3dAnimationStyle(dict);
        }
    }
}

