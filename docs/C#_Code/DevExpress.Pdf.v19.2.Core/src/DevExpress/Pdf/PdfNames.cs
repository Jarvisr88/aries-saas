namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfNames
    {
        private const string pageDestinationKey = "Dests";
        private const string annotationAppearanceKey = "AP";
        private const string javaScriptKey = "JavaScript";
        private const string pageNamesKey = "Pages";
        private const string idsKey = "IDS";
        private const string urlsKey = "URLS";
        private const string embeddedKey = "EmbeddedFiles";
        private readonly PdfNameTree<PdfDestination> pageDestinations;
        private readonly PdfDeferredSortedDictionary<string, PdfAnnotationAppearances> annotationAppearances;
        private readonly PdfDeferredSortedDictionary<string, PdfJavaScriptAction> javaScriptActions;
        private readonly PdfDeferredSortedDictionary<string, PdfPage> pageNames;
        private readonly PdfDeferredSortedDictionary<string, PdfSpiderSet> webCaptureContentSetsIds;
        private readonly PdfDeferredSortedDictionary<string, PdfSpiderSet> webCaptureContentSetsUrls;
        private readonly PdfDeferredSortedDictionary<string, PdfFileSpecification> embeddedFiles;
        private List<PdfDestination> unresolvedInternalDestinations;

        internal PdfNames(PdfReaderDictionary dictionary)
        {
            if (dictionary != null)
            {
                PdfCreateTreeElementAction<PdfDestination> createElement = <>c.<>9__30_0;
                if (<>c.<>9__30_0 == null)
                {
                    PdfCreateTreeElementAction<PdfDestination> local1 = <>c.<>9__30_0;
                    createElement = <>c.<>9__30_0 = (o, v) => o.GetDestination(v);
                }
                this.pageDestinations = new PdfNameTree<PdfDestination>(dictionary.GetDictionary("Dests"), createElement);
                PdfCreateTreeElementAction<PdfAnnotationAppearances> action2 = <>c.<>9__30_1;
                if (<>c.<>9__30_1 == null)
                {
                    PdfCreateTreeElementAction<PdfAnnotationAppearances> local2 = <>c.<>9__30_1;
                    action2 = <>c.<>9__30_1 = (o, v) => o.GetAnnotationAppearances(v, null);
                }
                this.annotationAppearances = PdfNameTreeNode<PdfAnnotationAppearances>.Parse(dictionary.GetDictionary("AP"), action2);
                PdfCreateTreeElementAction<PdfJavaScriptAction> action3 = <>c.<>9__30_2;
                if (<>c.<>9__30_2 == null)
                {
                    PdfCreateTreeElementAction<PdfJavaScriptAction> local3 = <>c.<>9__30_2;
                    action3 = <>c.<>9__30_2 = delegate (PdfObjectCollection o, object v) {
                        Func<PdfReaderDictionary, PdfJavaScriptAction> create = <>c.<>9__30_3;
                        if (<>c.<>9__30_3 == null)
                        {
                            Func<PdfReaderDictionary, PdfJavaScriptAction> local1 = <>c.<>9__30_3;
                            create = <>c.<>9__30_3 = d => new PdfJavaScriptAction(d);
                        }
                        return o.GetObject<PdfJavaScriptAction>(v, create);
                    };
                }
                this.javaScriptActions = PdfNameTreeNode<PdfJavaScriptAction>.Parse(dictionary.GetDictionary("JavaScript"), action3);
                PdfCreateTreeElementAction<PdfPage> action4 = <>c.<>9__30_4;
                if (<>c.<>9__30_4 == null)
                {
                    PdfCreateTreeElementAction<PdfPage> local4 = <>c.<>9__30_4;
                    action4 = <>c.<>9__30_4 = (o, v) => o.DocumentCatalog.FindPage(v);
                }
                this.pageNames = PdfNameTreeNode<PdfPage>.Parse(dictionary.GetDictionary("Pages"), action4);
                PdfCreateTreeElementAction<PdfSpiderSet> action5 = <>c.<>9__30_5;
                if (<>c.<>9__30_5 == null)
                {
                    PdfCreateTreeElementAction<PdfSpiderSet> local5 = <>c.<>9__30_5;
                    action5 = <>c.<>9__30_5 = (o, v) => PdfSpiderSet.Create(o, v);
                }
                this.webCaptureContentSetsIds = PdfNameTreeNode<PdfSpiderSet>.Parse(dictionary.GetDictionary("IDS"), action5);
                PdfCreateTreeElementAction<PdfSpiderSet> action6 = <>c.<>9__30_6;
                if (<>c.<>9__30_6 == null)
                {
                    PdfCreateTreeElementAction<PdfSpiderSet> local6 = <>c.<>9__30_6;
                    action6 = <>c.<>9__30_6 = (o, v) => PdfSpiderSet.Create(o, v);
                }
                this.webCaptureContentSetsUrls = PdfNameTreeNode<PdfSpiderSet>.Parse(dictionary.GetDictionary("URLS"), action6);
                PdfCreateTreeElementAction<PdfFileSpecification> action7 = <>c.<>9__30_7;
                if (<>c.<>9__30_7 == null)
                {
                    PdfCreateTreeElementAction<PdfFileSpecification> local7 = <>c.<>9__30_7;
                    action7 = <>c.<>9__30_7 = (o, v) => o.GetFileSpecification(v);
                }
                PdfDeferredSortedDictionary<string, PdfFileSpecification> dictionary1 = PdfNameTreeNode<PdfFileSpecification>.Parse(dictionary.GetDictionary("EmbeddedFiles"), action7);
                PdfDeferredSortedDictionary<string, PdfFileSpecification> dictionary2 = dictionary1;
                if (dictionary1 == null)
                {
                    PdfDeferredSortedDictionary<string, PdfFileSpecification> local8 = dictionary1;
                    dictionary2 = new PdfDeferredSortedDictionary<string, PdfFileSpecification>();
                }
                this.embeddedFiles = dictionary2;
            }
            this.pageDestinations ??= new PdfNameTree<PdfDestination>();
            this.embeddedFiles ??= new PdfDeferredSortedDictionary<string, PdfFileSpecification>();
            this.javaScriptActions ??= new PdfDeferredSortedDictionary<string, PdfJavaScriptAction>();
        }

        internal void AddDeferredDestination(string destinationName, PdfObject value, Func<object, PdfDestination> create)
        {
            this.pageDestinations.AddDeferred(destinationName, value, create);
        }

        internal string AddDestination(PdfDestination destination)
        {
            this.unresolvedInternalDestinations ??= new List<PdfDestination>();
            string key = NewKey<PdfDestination>(this.PageDestinations);
            this.PageDestinations.Add(key, destination);
            this.unresolvedInternalDestinations.Add(destination);
            return key;
        }

        internal static string NewKey<Q>(IDictionary<string, Q> source) where Q: class
        {
            string key = Guid.NewGuid().ToString();
            while (source.ContainsKey(key))
            {
                key = Guid.NewGuid().ToString();
            }
            return key;
        }

        internal void RemoveDestinations(Func<PdfDestination, bool> condition)
        {
            this.pageDestinations.RemoveAll(condition);
        }

        internal PdfWriterDictionary Write(PdfObjectCollection collection)
        {
            if (this.unresolvedInternalDestinations != null)
            {
                foreach (PdfDestination destination in this.unresolvedInternalDestinations)
                {
                    destination.ResolveInternalPage();
                }
                this.unresolvedInternalDestinations = null;
            }
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            if ((this.PageDestinations != null) && (this.PageDestinations.Count > 0))
            {
                dictionary.AddIfPresent("Dests", this.pageDestinations.Write(collection));
            }
            dictionary.AddIfPresent("AP", PdfNameTreeNode<PdfAnnotationAppearances>.Write(collection, this.annotationAppearances));
            if ((this.javaScriptActions != null) && (this.javaScriptActions.Count > 0))
            {
                dictionary.AddIfPresent("JavaScript", PdfNameTreeNode<PdfJavaScriptAction>.Write(collection, this.javaScriptActions));
            }
            dictionary.AddIfPresent("Pages", PdfNameTreeNode<PdfPage>.Write(collection, this.pageNames));
            dictionary.AddIfPresent("IDS", PdfNameTreeNode<PdfSpiderSet>.Write(collection, this.webCaptureContentSetsIds));
            dictionary.AddIfPresent("URLS", PdfNameTreeNode<PdfSpiderSet>.Write(collection, this.webCaptureContentSetsUrls));
            if ((this.embeddedFiles != null) && (this.embeddedFiles.Count > 0))
            {
                dictionary.AddIfPresent("EmbeddedFiles", PdfNameTreeNode<PdfFileSpecification>.Write(collection, this.embeddedFiles));
            }
            return dictionary;
        }

        public IDictionary<string, PdfDestination> PageDestinations =>
            this.pageDestinations;

        public IDictionary<string, PdfAnnotationAppearances> AnnotationAppearances =>
            this.annotationAppearances;

        public IDictionary<string, PdfJavaScriptAction> JavaScriptActions =>
            this.javaScriptActions;

        public IDictionary<string, PdfPage> PageNames =>
            this.pageNames;

        public IDictionary<string, PdfSpiderSet> WebCaptureContentSetsIds =>
            this.webCaptureContentSetsIds;

        public IDictionary<string, PdfSpiderSet> WebCaptureContentSetsUrls =>
            this.webCaptureContentSetsUrls;

        public IDictionary<string, PdfFileSpecification> EmbeddedFiles =>
            this.embeddedFiles;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfNames.<>c <>9 = new PdfNames.<>c();
            public static PdfCreateTreeElementAction<PdfDestination> <>9__30_0;
            public static PdfCreateTreeElementAction<PdfAnnotationAppearances> <>9__30_1;
            public static Func<PdfReaderDictionary, PdfJavaScriptAction> <>9__30_3;
            public static PdfCreateTreeElementAction<PdfJavaScriptAction> <>9__30_2;
            public static PdfCreateTreeElementAction<PdfPage> <>9__30_4;
            public static PdfCreateTreeElementAction<PdfSpiderSet> <>9__30_5;
            public static PdfCreateTreeElementAction<PdfSpiderSet> <>9__30_6;
            public static PdfCreateTreeElementAction<PdfFileSpecification> <>9__30_7;

            internal PdfDestination <.ctor>b__30_0(PdfObjectCollection o, object v) => 
                o.GetDestination(v);

            internal PdfAnnotationAppearances <.ctor>b__30_1(PdfObjectCollection o, object v) => 
                o.GetAnnotationAppearances(v, null);

            internal PdfJavaScriptAction <.ctor>b__30_2(PdfObjectCollection o, object v)
            {
                Func<PdfReaderDictionary, PdfJavaScriptAction> create = <>9__30_3;
                if (<>9__30_3 == null)
                {
                    Func<PdfReaderDictionary, PdfJavaScriptAction> local1 = <>9__30_3;
                    create = <>9__30_3 = d => new PdfJavaScriptAction(d);
                }
                return o.GetObject<PdfJavaScriptAction>(v, create);
            }

            internal PdfJavaScriptAction <.ctor>b__30_3(PdfReaderDictionary d) => 
                new PdfJavaScriptAction(d);

            internal PdfPage <.ctor>b__30_4(PdfObjectCollection o, object v) => 
                o.DocumentCatalog.FindPage(v);

            internal PdfSpiderSet <.ctor>b__30_5(PdfObjectCollection o, object v) => 
                PdfSpiderSet.Create(o, v);

            internal PdfSpiderSet <.ctor>b__30_6(PdfObjectCollection o, object v) => 
                PdfSpiderSet.Create(o, v);

            internal PdfFileSpecification <.ctor>b__30_7(PdfObjectCollection o, object v) => 
                o.GetFileSpecification(v);
        }
    }
}

