namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Pdf3dView : PdfObject
    {
        private readonly string externalName;
        private readonly string internalName;
        private readonly string matrixSource;
        private readonly object u3dPath;
        private readonly IList<object> c2w;
        private readonly double? distanceToCenterOfOrbit;
        private readonly PdfForm markupForm;
        private readonly Pdf3dProjection projection;
        private readonly Pdf3dBackground background;
        private readonly Pdf3dRenderMode renderMode;
        private readonly Pdf3dLightingScheme lightingScheme;
        private readonly IList<Pdf3dCrossSection> crossSections;
        private readonly IList<Pdf3dNode> nodes;
        private readonly IList<Pdf3dMeasurement> measurements;
        private readonly bool resetNode;

        public Pdf3dView(PdfReaderDictionary dictionary, PdfPage page) : base(dictionary.Number)
        {
            this.externalName = dictionary.GetTextString("XN");
            this.internalName = dictionary.GetTextString("IN");
            this.matrixSource = dictionary.GetName("MS");
            this.c2w = dictionary.GetArray("C2W");
            if (dictionary.TryGetValue("U3DPath", out this.u3dPath))
            {
                this.u3dPath = dictionary.Objects.TryResolve(this.u3dPath, null);
            }
            if ((this.u3dPath is PdfReaderDictionary) || (this.u3dPath is PdfStream))
            {
                this.u3dPath = null;
            }
            this.distanceToCenterOfOrbit = dictionary.GetNumber("CO");
            this.markupForm = dictionary.GetObject<PdfForm>("O", value => new PdfForm(value, page.Resources));
            Func<PdfReaderDictionary, Pdf3dProjection> create = <>c.<>9__44_1;
            if (<>c.<>9__44_1 == null)
            {
                Func<PdfReaderDictionary, Pdf3dProjection> local1 = <>c.<>9__44_1;
                create = <>c.<>9__44_1 = value => new Pdf3dProjection(value);
            }
            this.projection = dictionary.GetObject<Pdf3dProjection>("P", create);
            Func<PdfReaderDictionary, Pdf3dBackground> func2 = <>c.<>9__44_2;
            if (<>c.<>9__44_2 == null)
            {
                Func<PdfReaderDictionary, Pdf3dBackground> local2 = <>c.<>9__44_2;
                func2 = <>c.<>9__44_2 = value => new Pdf3dBackground(value);
            }
            this.background = dictionary.GetObject<Pdf3dBackground>("BG", func2);
            Func<PdfReaderDictionary, Pdf3dRenderMode> func3 = <>c.<>9__44_3;
            if (<>c.<>9__44_3 == null)
            {
                Func<PdfReaderDictionary, Pdf3dRenderMode> local3 = <>c.<>9__44_3;
                func3 = <>c.<>9__44_3 = value => new Pdf3dRenderMode(value);
            }
            this.renderMode = dictionary.GetObject<Pdf3dRenderMode>("RM", func3);
            Func<PdfReaderDictionary, Pdf3dLightingScheme> func4 = <>c.<>9__44_4;
            if (<>c.<>9__44_4 == null)
            {
                Func<PdfReaderDictionary, Pdf3dLightingScheme> local4 = <>c.<>9__44_4;
                func4 = <>c.<>9__44_4 = value => new Pdf3dLightingScheme(value);
            }
            this.lightingScheme = dictionary.GetObject<Pdf3dLightingScheme>("LS", func4);
            PdfObjectCollection objects = dictionary.Objects;
            this.crossSections = dictionary.GetArray<Pdf3dCrossSection>("SA", delegate (object value) {
                Func<PdfReaderDictionary, Pdf3dCrossSection> func1 = <>c.<>9__44_6;
                if (<>c.<>9__44_6 == null)
                {
                    Func<PdfReaderDictionary, Pdf3dCrossSection> local1 = <>c.<>9__44_6;
                    func1 = <>c.<>9__44_6 = dict => new Pdf3dCrossSection(dict);
                }
                return objects.GetObject<Pdf3dCrossSection>(value, func1);
            });
            this.nodes = dictionary.GetArray<Pdf3dNode>("NA", delegate (object value) {
                Func<PdfReaderDictionary, Pdf3dNode> func1 = <>c.<>9__44_8;
                if (<>c.<>9__44_8 == null)
                {
                    Func<PdfReaderDictionary, Pdf3dNode> local1 = <>c.<>9__44_8;
                    func1 = <>c.<>9__44_8 = dict => new Pdf3dNode(dict);
                }
                return objects.GetObject<Pdf3dNode>(value, func1);
            });
            this.measurements = dictionary.GetArray<Pdf3dMeasurement>("MA", delegate (object value) {
                Func<PdfReaderDictionary, Pdf3dMeasurement> <>9__10;
                Func<PdfReaderDictionary, Pdf3dMeasurement> func2 = <>9__10;
                if (<>9__10 == null)
                {
                    Func<PdfReaderDictionary, Pdf3dMeasurement> local1 = <>9__10;
                    func2 = <>9__10 = dict => Pdf3dMeasurement.Parse(page, dict);
                }
                return objects.GetObject<Pdf3dMeasurement>(value, func2);
            });
            bool? boolean = dictionary.GetBoolean("NR");
            this.resetNode = (boolean != null) ? boolean.GetValueOrDefault() : false;
        }

        public static Pdf3dView GetDefaultView(Pdf3dData data, PdfReaderDictionary dictionary, PdfPage page)
        {
            object obj2;
            Pdf3dStream stream = data as Pdf3dStream;
            if (stream == null)
            {
                Pdf3dReference reference = data as Pdf3dReference;
                if (reference != null)
                {
                    stream = reference.Stream;
                }
            }
            IList<Pdf3dView> views = stream?.Views;
            if (!dictionary.TryGetValue("3DV", out obj2))
            {
                return null;
            }
            obj2 = dictionary.Objects.TryResolve(obj2, null);
            if ((views != null) && (views.Count > 0))
            {
                int? nullable = obj2 as int?;
                if (nullable != null)
                {
                    int? nullable2 = nullable;
                    int count = 0;
                    if ((nullable2.GetValueOrDefault() > count) ? (nullable2 != null) : false)
                    {
                        nullable2 = nullable;
                        count = views.Count;
                        if ((nullable2.GetValueOrDefault() < count) ? (nullable2 != null) : false)
                        {
                            return views[nullable.Value];
                        }
                    }
                }
                byte[] buffer = obj2 as byte[];
                if (buffer != null)
                {
                    using (IEnumerator<Pdf3dView> enumerator = views.GetEnumerator())
                    {
                        while (true)
                        {
                            if (!enumerator.MoveNext())
                            {
                                break;
                            }
                            Pdf3dView current = enumerator.Current;
                            if (current.internalName == PdfDocEncoding.GetString(buffer))
                            {
                                return current;
                            }
                        }
                    }
                }
                PdfName name = obj2 as PdfName;
                if (name != null)
                {
                    if (name.Name == "F")
                    {
                        return views[0];
                    }
                    if (name.Name == "L")
                    {
                        return views[views.Count - 1];
                    }
                }
            }
            return dictionary.Objects.GetObject<Pdf3dView>(obj2, val => new Pdf3dView(val, page));
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddName("Type", "3DView");
            dictionary.AddIfPresent("XN", this.externalName);
            dictionary.AddIfPresent("IN", this.internalName);
            dictionary.AddName("MS", this.matrixSource);
            dictionary.AddIfPresent("C2W", this.c2w);
            dictionary.AddIfPresent("U3DPath", this.u3dPath);
            dictionary.AddIfPresent("CO", this.distanceToCenterOfOrbit);
            dictionary.Add("O", this.markupForm);
            dictionary.Add("P", this.projection);
            dictionary.Add("BG", this.background);
            dictionary.Add("RM", this.renderMode);
            dictionary.Add("LS", this.lightingScheme);
            dictionary.AddList<Pdf3dCrossSection>("SA", this.crossSections);
            dictionary.AddList<Pdf3dNode>("NA", this.nodes);
            dictionary.AddList<Pdf3dMeasurement>("MA", this.measurements);
            dictionary.Add("NR", this.resetNode, false);
            return dictionary;
        }

        public string ExternalName =>
            this.externalName;

        public string InternalName =>
            this.internalName;

        public string MatrixSource =>
            this.matrixSource;

        public object U3dPath =>
            this.u3dPath;

        public IList<object> C2w =>
            this.c2w;

        public double? DistanceToCenterOfOrbit =>
            this.distanceToCenterOfOrbit;

        public PdfForm MarkupForm =>
            this.markupForm;

        public Pdf3dProjection Projection =>
            this.projection;

        public Pdf3dBackground Background =>
            this.background;

        public Pdf3dRenderMode RenderMode =>
            this.renderMode;

        public Pdf3dLightingScheme LightingScheme =>
            this.lightingScheme;

        public IList<Pdf3dCrossSection> CrossSections =>
            this.crossSections;

        public IList<Pdf3dNode> Nodes =>
            this.nodes;

        public bool ResetNode =>
            this.resetNode;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Pdf3dView.<>c <>9 = new Pdf3dView.<>c();
            public static Func<PdfReaderDictionary, Pdf3dProjection> <>9__44_1;
            public static Func<PdfReaderDictionary, Pdf3dBackground> <>9__44_2;
            public static Func<PdfReaderDictionary, Pdf3dRenderMode> <>9__44_3;
            public static Func<PdfReaderDictionary, Pdf3dLightingScheme> <>9__44_4;
            public static Func<PdfReaderDictionary, Pdf3dCrossSection> <>9__44_6;
            public static Func<PdfReaderDictionary, Pdf3dNode> <>9__44_8;

            internal Pdf3dProjection <.ctor>b__44_1(PdfReaderDictionary value) => 
                new Pdf3dProjection(value);

            internal Pdf3dBackground <.ctor>b__44_2(PdfReaderDictionary value) => 
                new Pdf3dBackground(value);

            internal Pdf3dRenderMode <.ctor>b__44_3(PdfReaderDictionary value) => 
                new Pdf3dRenderMode(value);

            internal Pdf3dLightingScheme <.ctor>b__44_4(PdfReaderDictionary value) => 
                new Pdf3dLightingScheme(value);

            internal Pdf3dCrossSection <.ctor>b__44_6(PdfReaderDictionary dict) => 
                new Pdf3dCrossSection(dict);

            internal Pdf3dNode <.ctor>b__44_8(PdfReaderDictionary dict) => 
                new Pdf3dNode(dict);
        }
    }
}

