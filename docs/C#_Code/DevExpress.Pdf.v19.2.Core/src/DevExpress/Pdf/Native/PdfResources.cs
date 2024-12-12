namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfResources : PdfObject
    {
        private const string graphicsStateParametersDictionaryName = "ExtGState";
        private const string colorSpacesDictionaryName = "ColorSpace";
        private const string patternsDictionaryName = "Pattern";
        private const string shadingsDictionaryName = "Shading";
        private const string xObjectsDictionaryName = "XObject";
        private const string fontsDictionaryName = "Font";
        private const string propertiesDictionaryName = "Properties";
        private readonly PdfDocumentCatalog documentCatalog;
        private readonly PdfResources parentResources;
        private readonly bool shouldWriteParent;
        private readonly bool shouldBeWritten;
        private readonly PdfResourceDictionary graphicsStateParameters;
        private readonly PdfResourceDictionary colorSpaces;
        private readonly PdfResourceDictionary patterns;
        private readonly PdfResourceDictionary shadings;
        private readonly PdfResourceDictionary xObjects;
        private readonly PdfResourceDictionary fonts;
        private readonly PdfResourceDictionary properties;
        private PdfReaderDictionary dictionary;
        private Dictionary<string, Dictionary<string, string>> renamedResources;

        public PdfResources(PdfDocumentCatalog documentCatalog, bool shouldBeWritten) : this(documentCatalog, null, false, shouldBeWritten, null)
        {
        }

        public PdfResources(PdfDocumentCatalog documentCatalog, PdfResources parentResources, bool shouldWriteParent, bool shouldBeWritten, PdfReaderDictionary dictionary) : this((dictionary == null) ? -1 : dictionary.Number)
        {
            this.documentCatalog = documentCatalog;
            this.parentResources = parentResources;
            this.shouldWriteParent = shouldWriteParent;
            this.shouldBeWritten = shouldBeWritten;
            this.dictionary = dictionary;
            this.graphicsStateParameters = new PdfResourceDictionary(this, "ExtGState", "P");
            this.colorSpaces = new PdfResourceDictionary(this, "ColorSpace", "CS");
            this.patterns = new PdfResourceDictionary(this, "Pattern", "Ptrn");
            this.shadings = new PdfResourceDictionary(this, "Shading", "S");
            this.xObjects = new PdfResourceDictionary(this, "XObject", "O");
            this.fonts = new PdfResourceDictionary(this, "Font", "FNT");
            this.properties = new PdfResourceDictionary(this, "Properties", "Prop");
        }

        public string AddColorSpace(PdfColorSpace colorSpace) => 
            this.colorSpaces.Add<PdfColorSpace>(colorSpace, false);

        public string AddFont(PdfFont font) => 
            this.fonts.Add<PdfFont>(font, true);

        public string AddGraphicsStateParameters(PdfGraphicsStateParameters parameters) => 
            this.graphicsStateParameters.Add<PdfGraphicsStateParameters>(parameters, false);

        public string AddPattern(PdfPattern pattern) => 
            this.patterns.Add<PdfPattern>(pattern, false);

        public string AddShading(PdfShading shading) => 
            this.shadings.Add<PdfShading>(shading, false);

        public string AddXObject(int objectNumber) => 
            this.xObjects.Add(objectNumber);

        public void AppendInteractiveFormResources(PdfResources appendedResources)
        {
            Dictionary<string, Dictionary<string, string>> dictionary = new Dictionary<string, Dictionary<string, string>>();
            Guid id = appendedResources.documentCatalog.Objects.Id;
            dictionary.Add("ExtGState", this.graphicsStateParameters.Add<PdfGraphicsStateParameters>(appendedResources.graphicsStateParameters.Names, id, new Func<string, PdfGraphicsStateParameters>(appendedResources.GetGraphicsStateParameters)));
            dictionary.Add("ColorSpace", this.colorSpaces.Add<PdfColorSpace>(appendedResources.colorSpaces.Names, id, new Func<string, PdfColorSpace>(appendedResources.GetColorSpace)));
            dictionary.Add("Pattern", this.patterns.Add<PdfPattern>(appendedResources.patterns.Names, id, new Func<string, PdfPattern>(appendedResources.GetPattern)));
            dictionary.Add("Shading", this.shadings.Add<PdfShading>(appendedResources.shadings.Names, id, new Func<string, PdfShading>(appendedResources.GetShading)));
            dictionary.Add("XObject", this.xObjects.Add<PdfXObject>(appendedResources.xObjects.Names, id, new Func<string, PdfXObject>(appendedResources.GetXObject)));
            dictionary.Add("Font", this.fonts.Add<PdfFont>(appendedResources.fonts.Names, id, new Func<string, PdfFont>(appendedResources.GetFont)));
            dictionary.Add("Properties", this.properties.Add<PdfProperties>(appendedResources.properties.Names, id, new Func<string, PdfProperties>(appendedResources.GetProperties)));
            appendedResources.renamedResources = dictionary;
        }

        public void ClearRenamedResources()
        {
            this.renamedResources = null;
        }

        private PdfWriterDictionary CreateResourceWriterDictionary<T>(PdfObjectCollection objects, Func<PdfResources, PdfResourceDictionary> getResourcesNames, Func<string, T> getResource) where T: PdfObject
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            this.FillResourcesWriterDictionary<T>(dictionary, getResourcesNames, getResource);
            return ((dictionary.Count > 0) ? dictionary : null);
        }

        private void FillResourcesWriterDictionary<T>(PdfWriterDictionary dictionary, Func<PdfResources, PdfResourceDictionary> getResourcesNames, Func<string, T> getResource) where T: PdfObject
        {
            PdfResourceDictionary dictionary2 = getResourcesNames(this);
            if (dictionary2 != null)
            {
                dictionary2.WriteResources<T>(dictionary, getResource);
            }
            if (this.shouldWriteParent && (this.parentResources != null))
            {
                this.parentResources.FillResourcesWriterDictionary<T>(dictionary, getResourcesNames, getResource);
            }
        }

        public void FillWidgetAppearanceResources(PdfResources resources)
        {
            this.ResolveResourcesDictionary();
            this.graphicsStateParameters.CopyTo(resources.graphicsStateParameters);
            this.colorSpaces.CopyTo(resources.colorSpaces);
            this.patterns.CopyTo(resources.patterns);
            this.shadings.CopyTo(resources.shadings);
            this.xObjects.CopyTo(resources.xObjects);
            this.properties.CopyTo(resources.properties);
        }

        public PdfName FindColorSpaceName(PdfColorSpace value) => 
            this.FindResourceName<PdfColorSpace>(value, this.colorSpaces, "ColorSpace");

        public PdfName FindFontName(PdfFont value) => 
            this.FindResourceName<PdfFont>(value, this.fonts, "Font");

        public PdfName FindGraphicsStateParametersName(PdfGraphicsStateParameters value) => 
            this.FindResourceName<PdfGraphicsStateParameters>(value, this.graphicsStateParameters, "ExtGState");

        public PdfName FindPropertiesName(PdfProperties value) => 
            this.FindResourceName<PdfProperties>(value, this.properties, "Properties");

        private PdfName FindResourceName<T>(T value, PdfResourceDictionary resources, string key) where T: PdfObject
        {
            Dictionary<string, string> dictionary;
            string str2;
            if (value == null)
            {
                return null;
            }
            string resourceName = resources.GetResourceName(value.ObjectNumber);
            return (!string.IsNullOrEmpty(resourceName) ? (((this.renamedResources == null) || (!this.renamedResources.TryGetValue(key, out dictionary) || ((dictionary == null) || (!dictionary.TryGetValue(resourceName, out str2) || string.IsNullOrEmpty(str2))))) ? new PdfName(resourceName) : new PdfName(str2)) : this.parentResources?.FindResourceName<T>(value, resources, key));
        }

        public PdfName FindXObjectName(PdfXObject value) => 
            this.FindResourceName<PdfXObject>(value, this.xObjects, "XObject");

        public PdfColorSpace GetColorSpace(string colorSpaceName)
        {
            Func<PdfResources, PdfResourceDictionary> getResources = <>c.<>9__33_0;
            if (<>c.<>9__33_0 == null)
            {
                Func<PdfResources, PdfResourceDictionary> local1 = <>c.<>9__33_0;
                getResources = <>c.<>9__33_0 = rp => rp.colorSpaces;
            }
            Func<PdfObjectCollection, object, PdfColorSpace> create = <>c.<>9__33_1;
            if (<>c.<>9__33_1 == null)
            {
                Func<PdfObjectCollection, object, PdfColorSpace> local2 = <>c.<>9__33_1;
                create = <>c.<>9__33_1 = (o, d) => (d == null) ? null : PdfColorSpace.Parse(o, d);
            }
            PdfColorSpace local3 = this.GetResource<PdfColorSpace>(colorSpaceName, getResources, "ColorSpace", create);
            PdfColorSpace local5 = local3;
            if (local3 == null)
            {
                PdfColorSpace local4 = local3;
                local5 = PdfColorSpace.CreateColorSpace(colorSpaceName);
            }
            return local5;
        }

        public PdfFont GetFont(string fontName)
        {
            Func<PdfResources, PdfResourceDictionary> getResources = <>c.<>9__37_0;
            if (<>c.<>9__37_0 == null)
            {
                Func<PdfResources, PdfResourceDictionary> local1 = <>c.<>9__37_0;
                getResources = <>c.<>9__37_0 = rp => rp.fonts;
            }
            Func<PdfObjectCollection, object, PdfFont> create = <>c.<>9__37_1;
            if (<>c.<>9__37_1 == null)
            {
                Func<PdfObjectCollection, object, PdfFont> local2 = <>c.<>9__37_1;
                create = <>c.<>9__37_1 = delegate (PdfObjectCollection o, object d) {
                    if ((d == null) || (d is PdfFreeObject))
                    {
                        return null;
                    }
                    PdfReaderDictionary fontDictionary = d as PdfReaderDictionary;
                    if (fontDictionary == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    return PdfFont.CreateFont(fontDictionary);
                };
            }
            return this.GetResource<PdfFont>(fontName, getResources, "Font", create);
        }

        public PdfGraphicsStateParameters GetGraphicsStateParameters(string graphicsStateParametersName)
        {
            Func<PdfResources, PdfResourceDictionary> getResources = <>c.<>9__32_0;
            if (<>c.<>9__32_0 == null)
            {
                Func<PdfResources, PdfResourceDictionary> local1 = <>c.<>9__32_0;
                getResources = <>c.<>9__32_0 = rp => rp.graphicsStateParameters;
            }
            Func<PdfObjectCollection, object, PdfGraphicsStateParameters> create = <>c.<>9__32_1;
            if (<>c.<>9__32_1 == null)
            {
                Func<PdfObjectCollection, object, PdfGraphicsStateParameters> local2 = <>c.<>9__32_1;
                create = <>c.<>9__32_1 = delegate (PdfObjectCollection o, object d) {
                    PdfReaderDictionary dictionary = d as PdfReaderDictionary;
                    if (dictionary == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    return new PdfGraphicsStateParameters(dictionary);
                };
            }
            return this.GetResource<PdfGraphicsStateParameters>(graphicsStateParametersName, getResources, "ExtGState", create);
        }

        public PdfPattern GetPattern(string patternName)
        {
            Func<PdfResources, PdfResourceDictionary> getResources = <>c.<>9__34_0;
            if (<>c.<>9__34_0 == null)
            {
                Func<PdfResources, PdfResourceDictionary> local1 = <>c.<>9__34_0;
                getResources = <>c.<>9__34_0 = rp => rp.patterns;
            }
            Func<PdfObjectCollection, object, PdfPattern> create = <>c.<>9__34_1;
            if (<>c.<>9__34_1 == null)
            {
                Func<PdfObjectCollection, object, PdfPattern> local2 = <>c.<>9__34_1;
                create = <>c.<>9__34_1 = (o, d) => PdfPattern.Parse(d);
            }
            return this.GetResource<PdfPattern>(patternName, getResources, "Pattern", create);
        }

        public PdfProperties GetProperties(string propertiesName)
        {
            Func<PdfResources, PdfResourceDictionary> getResources = <>c.<>9__38_0;
            if (<>c.<>9__38_0 == null)
            {
                Func<PdfResources, PdfResourceDictionary> local1 = <>c.<>9__38_0;
                getResources = <>c.<>9__38_0 = rp => rp.properties;
            }
            Func<PdfObjectCollection, object, PdfProperties> create = <>c.<>9__38_1;
            if (<>c.<>9__38_1 == null)
            {
                Func<PdfObjectCollection, object, PdfProperties> local2 = <>c.<>9__38_1;
                create = <>c.<>9__38_1 = delegate (PdfObjectCollection o, object d) {
                    if (d == null)
                    {
                        return null;
                    }
                    PdfReaderDictionary dictionary = d as PdfReaderDictionary;
                    if (dictionary == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    return PdfProperties.ParseProperties(dictionary);
                };
            }
            return this.GetResource<PdfProperties>(propertiesName, getResources, "Properties", create);
        }

        private T GetResource<T>(string resourceName, Func<PdfResources, PdfResourceDictionary> getResources, string resourceKey, Func<PdfObjectCollection, object, T> create) where T: PdfObject
        {
            PdfResourceDictionary dictionary = getResources(this);
            T resource = dictionary.GetResource<T>(resourceName, create);
            if ((resource == null) && (this.parentResources != null))
            {
                resource = this.parentResources.GetResource<T>(resourceName, getResources, resourceKey, create);
                if (resource != null)
                {
                    dictionary.Add(resourceName, resource.ObjectNumber);
                }
            }
            return resource;
        }

        public ICollection<string> GetResourceNames(string resourceKey) => 
            (resourceKey == "ExtGState") ? this.graphicsStateParameters.Names : ((resourceKey == "Shading") ? this.shadings.Names : ((resourceKey == "ColorSpace") ? this.colorSpaces.Names : ((resourceKey == "Pattern") ? this.patterns.Names : ((resourceKey == "XObject") ? this.xObjects.Names : ((resourceKey == "Font") ? this.fonts.Names : this.properties.Names)))));

        public PdfShading GetShading(string shadingName)
        {
            Func<PdfResources, PdfResourceDictionary> getResources = <>c.<>9__35_0;
            if (<>c.<>9__35_0 == null)
            {
                Func<PdfResources, PdfResourceDictionary> local1 = <>c.<>9__35_0;
                getResources = <>c.<>9__35_0 = rp => rp.shadings;
            }
            Func<PdfObjectCollection, object, PdfShading> create = <>c.<>9__35_1;
            if (<>c.<>9__35_1 == null)
            {
                Func<PdfObjectCollection, object, PdfShading> local2 = <>c.<>9__35_1;
                create = <>c.<>9__35_1 = (o, d) => PdfShading.Parse(d);
            }
            return this.GetResource<PdfShading>(shadingName, getResources, "Shading", create);
        }

        public PdfXObject GetXObject(string xObjectName)
        {
            Func<PdfResources, PdfResourceDictionary> getResources = <>c.<>9__36_0;
            if (<>c.<>9__36_0 == null)
            {
                Func<PdfResources, PdfResourceDictionary> local1 = <>c.<>9__36_0;
                getResources = <>c.<>9__36_0 = rp => rp.xObjects;
            }
            return this.GetResource<PdfXObject>(xObjectName, getResources, "XObject", (o, d) => PdfXObject.Parse(d as PdfReaderStream, this, null));
        }

        private void ResolveResourcesDictionary()
        {
            this.graphicsStateParameters.ResolveResource<PdfGraphicsStateParameters>("ExtGState", new Func<string, PdfGraphicsStateParameters>(this.GetGraphicsStateParameters));
            this.colorSpaces.ResolveResource<PdfColorSpace>("ColorSpace", new Func<string, PdfColorSpace>(this.GetColorSpace));
            this.patterns.ResolveResource<PdfPattern>("Pattern", new Func<string, PdfPattern>(this.GetPattern));
            this.shadings.ResolveResource<PdfShading>("Shading", new Func<string, PdfShading>(this.GetShading));
            this.xObjects.ResolveResource<PdfXObject>("XObject", new Func<string, PdfXObject>(this.GetXObject));
            this.fonts.ResolveResource<PdfFont>("Font", new Func<string, PdfFont>(this.GetFont));
            this.properties.ResolveResource<PdfProperties>("Properties", new Func<string, PdfProperties>(this.GetProperties));
            this.dictionary = null;
            if (this.parentResources != null)
            {
                this.parentResources.ResolveResourcesDictionary();
            }
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            this.ResolveResourcesDictionary();
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            Func<PdfResources, PdfResourceDictionary> getResourcesNames = <>c.<>9__59_0;
            if (<>c.<>9__59_0 == null)
            {
                Func<PdfResources, PdfResourceDictionary> local1 = <>c.<>9__59_0;
                getResourcesNames = <>c.<>9__59_0 = r => r.graphicsStateParameters;
            }
            dictionary.Add("ExtGState", this.CreateResourceWriterDictionary<PdfGraphicsStateParameters>(objects, getResourcesNames, new Func<string, PdfGraphicsStateParameters>(this.GetGraphicsStateParameters)), null);
            Func<PdfResources, PdfResourceDictionary> func2 = <>c.<>9__59_1;
            if (<>c.<>9__59_1 == null)
            {
                Func<PdfResources, PdfResourceDictionary> local2 = <>c.<>9__59_1;
                func2 = <>c.<>9__59_1 = r => r.colorSpaces;
            }
            dictionary.Add("ColorSpace", this.CreateResourceWriterDictionary<PdfColorSpace>(objects, func2, new Func<string, PdfColorSpace>(this.GetColorSpace)), null);
            Func<PdfResources, PdfResourceDictionary> func3 = <>c.<>9__59_2;
            if (<>c.<>9__59_2 == null)
            {
                Func<PdfResources, PdfResourceDictionary> local3 = <>c.<>9__59_2;
                func3 = <>c.<>9__59_2 = r => r.patterns;
            }
            dictionary.Add("Pattern", this.CreateResourceWriterDictionary<PdfPattern>(objects, func3, new Func<string, PdfPattern>(this.GetPattern)), null);
            Func<PdfResources, PdfResourceDictionary> func4 = <>c.<>9__59_3;
            if (<>c.<>9__59_3 == null)
            {
                Func<PdfResources, PdfResourceDictionary> local4 = <>c.<>9__59_3;
                func4 = <>c.<>9__59_3 = r => r.shadings;
            }
            dictionary.Add("Shading", this.CreateResourceWriterDictionary<PdfShading>(objects, func4, new Func<string, PdfShading>(this.GetShading)), null);
            Func<PdfResources, PdfResourceDictionary> func5 = <>c.<>9__59_4;
            if (<>c.<>9__59_4 == null)
            {
                Func<PdfResources, PdfResourceDictionary> local5 = <>c.<>9__59_4;
                func5 = <>c.<>9__59_4 = r => r.xObjects;
            }
            dictionary.Add("XObject", this.CreateResourceWriterDictionary<PdfXObject>(objects, func5, new Func<string, PdfXObject>(this.GetXObject)), null);
            Func<PdfResources, PdfResourceDictionary> func6 = <>c.<>9__59_5;
            if (<>c.<>9__59_5 == null)
            {
                Func<PdfResources, PdfResourceDictionary> local6 = <>c.<>9__59_5;
                func6 = <>c.<>9__59_5 = r => r.fonts;
            }
            dictionary.Add("Font", this.CreateResourceWriterDictionary<PdfFont>(objects, func6, new Func<string, PdfFont>(this.GetFont)), null);
            Func<PdfResources, PdfResourceDictionary> func7 = <>c.<>9__59_6;
            if (<>c.<>9__59_6 == null)
            {
                Func<PdfResources, PdfResourceDictionary> local7 = <>c.<>9__59_6;
                func7 = <>c.<>9__59_6 = r => r.properties;
            }
            dictionary.Add("Properties", this.CreateResourceWriterDictionary<PdfProperties>(objects, func7, new Func<string, PdfProperties>(this.GetProperties)), null);
            if (!this.shouldBeWritten && (dictionary.Count == 0))
            {
                return null;
            }
            object[] enumerable = new object[] { new PdfName("PDF"), new PdfName("Text"), new PdfName("ImageB"), new PdfName("ImageC"), new PdfName("ImageI") };
            dictionary.Add("ProcSet", new PdfWritableArray(enumerable));
            return dictionary;
        }

        public PdfDocumentCatalog DocumentCatalog =>
            this.documentCatalog;

        public PdfReaderDictionary Dictionary =>
            this.dictionary;

        public bool ContainsRenamedResources
        {
            get
            {
                if (this.renamedResources != null)
                {
                    using (Dictionary<string, Dictionary<string, string>>.ValueCollection.Enumerator enumerator = this.renamedResources.Values.GetEnumerator())
                    {
                        while (true)
                        {
                            if (!enumerator.MoveNext())
                            {
                                break;
                            }
                            Dictionary<string, string> current = enumerator.Current;
                            if (current.Count > 0)
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
        }

        public IEnumerable<int> ObjectNumbers
        {
            get
            {
                this.ResolveResourcesDictionary();
                List<int> list = new List<int>(this.graphicsStateParameters.ObjectNumbers);
                list.AddRange(this.colorSpaces.ObjectNumbers);
                list.AddRange(this.patterns.ObjectNumbers);
                list.AddRange(this.shadings.ObjectNumbers);
                list.AddRange(this.xObjects.ObjectNumbers);
                list.AddRange(this.fonts.ObjectNumbers);
                list.AddRange(this.properties.ObjectNumbers);
                return list;
            }
        }

        public PdfResources ParentResources =>
            this.parentResources;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfResources.<>c <>9 = new PdfResources.<>c();
            public static Func<PdfResources, PdfResourceDictionary> <>9__32_0;
            public static Func<PdfObjectCollection, object, PdfGraphicsStateParameters> <>9__32_1;
            public static Func<PdfResources, PdfResourceDictionary> <>9__33_0;
            public static Func<PdfObjectCollection, object, PdfColorSpace> <>9__33_1;
            public static Func<PdfResources, PdfResourceDictionary> <>9__34_0;
            public static Func<PdfObjectCollection, object, PdfPattern> <>9__34_1;
            public static Func<PdfResources, PdfResourceDictionary> <>9__35_0;
            public static Func<PdfObjectCollection, object, PdfShading> <>9__35_1;
            public static Func<PdfResources, PdfResourceDictionary> <>9__36_0;
            public static Func<PdfResources, PdfResourceDictionary> <>9__37_0;
            public static Func<PdfObjectCollection, object, PdfFont> <>9__37_1;
            public static Func<PdfResources, PdfResourceDictionary> <>9__38_0;
            public static Func<PdfObjectCollection, object, PdfProperties> <>9__38_1;
            public static Func<PdfResources, PdfResourceDictionary> <>9__59_0;
            public static Func<PdfResources, PdfResourceDictionary> <>9__59_1;
            public static Func<PdfResources, PdfResourceDictionary> <>9__59_2;
            public static Func<PdfResources, PdfResourceDictionary> <>9__59_3;
            public static Func<PdfResources, PdfResourceDictionary> <>9__59_4;
            public static Func<PdfResources, PdfResourceDictionary> <>9__59_5;
            public static Func<PdfResources, PdfResourceDictionary> <>9__59_6;

            internal PdfResourceDictionary <GetColorSpace>b__33_0(PdfResources rp) => 
                rp.colorSpaces;

            internal PdfColorSpace <GetColorSpace>b__33_1(PdfObjectCollection o, object d) => 
                (d == null) ? null : PdfColorSpace.Parse(o, d);

            internal PdfResourceDictionary <GetFont>b__37_0(PdfResources rp) => 
                rp.fonts;

            internal PdfFont <GetFont>b__37_1(PdfObjectCollection o, object d)
            {
                if ((d == null) || (d is PdfFreeObject))
                {
                    return null;
                }
                PdfReaderDictionary fontDictionary = d as PdfReaderDictionary;
                if (fontDictionary == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                return PdfFont.CreateFont(fontDictionary);
            }

            internal PdfResourceDictionary <GetGraphicsStateParameters>b__32_0(PdfResources rp) => 
                rp.graphicsStateParameters;

            internal PdfGraphicsStateParameters <GetGraphicsStateParameters>b__32_1(PdfObjectCollection o, object d)
            {
                PdfReaderDictionary dictionary = d as PdfReaderDictionary;
                if (dictionary == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                return new PdfGraphicsStateParameters(dictionary);
            }

            internal PdfResourceDictionary <GetPattern>b__34_0(PdfResources rp) => 
                rp.patterns;

            internal PdfPattern <GetPattern>b__34_1(PdfObjectCollection o, object d) => 
                PdfPattern.Parse(d);

            internal PdfResourceDictionary <GetProperties>b__38_0(PdfResources rp) => 
                rp.properties;

            internal PdfProperties <GetProperties>b__38_1(PdfObjectCollection o, object d)
            {
                if (d == null)
                {
                    return null;
                }
                PdfReaderDictionary dictionary = d as PdfReaderDictionary;
                if (dictionary == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                return PdfProperties.ParseProperties(dictionary);
            }

            internal PdfResourceDictionary <GetShading>b__35_0(PdfResources rp) => 
                rp.shadings;

            internal PdfShading <GetShading>b__35_1(PdfObjectCollection o, object d) => 
                PdfShading.Parse(d);

            internal PdfResourceDictionary <GetXObject>b__36_0(PdfResources rp) => 
                rp.xObjects;

            internal PdfResourceDictionary <ToWritableObject>b__59_0(PdfResources r) => 
                r.graphicsStateParameters;

            internal PdfResourceDictionary <ToWritableObject>b__59_1(PdfResources r) => 
                r.colorSpaces;

            internal PdfResourceDictionary <ToWritableObject>b__59_2(PdfResources r) => 
                r.patterns;

            internal PdfResourceDictionary <ToWritableObject>b__59_3(PdfResources r) => 
                r.shadings;

            internal PdfResourceDictionary <ToWritableObject>b__59_4(PdfResources r) => 
                r.xObjects;

            internal PdfResourceDictionary <ToWritableObject>b__59_5(PdfResources r) => 
                r.fonts;

            internal PdfResourceDictionary <ToWritableObject>b__59_6(PdfResources r) => 
                r.properties;
        }
    }
}

