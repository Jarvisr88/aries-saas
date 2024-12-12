namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public abstract class PdfDestination : PdfObject
    {
        private readonly PdfDocumentCatalog documentCatalog;
        private readonly PdfIndirectObjectId? pageId;
        private object pageObject;
        private PdfPage page;
        private int pageIndex;

        protected PdfDestination(PdfPage page)
        {
            this.pageIndex = -1;
            if (page == null)
            {
                throw new ArgumentNullException("page");
            }
            this.page = page;
            this.documentCatalog = page.DocumentCatalog;
            this.pageIndex = this.CalculatePageIndex(this.documentCatalog.Pages);
        }

        protected PdfDestination(PdfDocumentCatalog documentCatalog, object pageObject)
        {
            this.pageIndex = -1;
            this.documentCatalog = documentCatalog;
            this.pageObject = pageObject;
        }

        protected PdfDestination(PdfDestination destination, int objectNumber) : base(objectNumber)
        {
            this.pageIndex = -1;
            if (destination.pageId != null)
            {
                this.pageId = destination.pageId;
            }
            else
            {
                PdfPage page = destination.page;
                if (page != null)
                {
                    PdfDocumentCatalog documentCatalog = destination.documentCatalog;
                    if (documentCatalog != null)
                    {
                        PdfObjectCollection objects = documentCatalog.Objects;
                        int number = page.ObjectNumber;
                        if (number > 0)
                        {
                            this.pageId = new PdfIndirectObjectId(objects.Id, number);
                        }
                    }
                }
            }
            this.pageIndex = destination.pageIndex;
        }

        protected static void AddParameter(IList<object> parameters, double? parameter)
        {
            parameters.Add(parameter?.Value);
        }

        protected abstract void AddWriteableParameters(IList<object> parameters);
        protected int CalculatePageIndex(IList<PdfPage> pages) => 
            (this.Page == null) ? this.pageIndex : pages.IndexOf(this.page);

        protected abstract PdfDestination CreateDuplicate(int objectNumber);
        protected internal abstract PdfTarget CreateTarget(IList<PdfPage> pages);
        protected internal override PdfObject GetDeferredSavedObject(PdfObjectCollection objects, bool isClonning)
        {
            this.ResolvePage();
            return this.CreateDuplicate(objects.GetNextObjectNumber());
        }

        private static double? GetSingleValue(IList<object> array)
        {
            if (array.Count >= 3)
            {
                object obj2 = array[2];
                if (obj2 != null)
                {
                    return new double?(PdfDocumentReader.ConvertToDouble(obj2));
                }
            }
            return null;
        }

        protected internal override bool IsDeferredObject(bool isCloning) => 
            isCloning;

        internal static PdfDeferredSortedDictionary<string, PdfDestination> Parse(PdfReaderDictionary dictionary)
        {
            PdfObjectCollection objects = dictionary.Objects;
            try
            {
                object obj2;
                if ((dictionary.Count == 1) && dictionary.TryGetValue("Names", out obj2))
                {
                    IList<object> list = obj2 as IList<object>;
                    if (list != null)
                    {
                        int count = list.Count;
                        if ((count > 0) && ((count % 2) == 0))
                        {
                            PdfDeferredSortedDictionary<string, PdfDestination> dictionary4;
                            PdfDeferredSortedDictionary<string, PdfDestination> dictionary3 = new PdfDeferredSortedDictionary<string, PdfDestination>();
                            int num2 = 0;
                            while (true)
                            {
                                if (num2 >= count)
                                {
                                    dictionary4 = dictionary3;
                                    break;
                                }
                                byte[] buffer = list[num2++] as byte[];
                                if (buffer == null)
                                {
                                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                                }
                                dictionary3.Add(PdfDocumentReader.ConvertToString(buffer), objects.GetDestination(list[num2++]));
                            }
                            return dictionary4;
                        }
                    }
                }
            }
            catch
            {
            }
            PdfDeferredSortedDictionary<string, PdfDestination> dictionary2 = new PdfDeferredSortedDictionary<string, PdfDestination>();
            foreach (KeyValuePair<string, object> pair in dictionary)
            {
                dictionary2.AddDeferred(pair.Key, pair.Value, new Func<object, PdfDestination>(objects.GetDestination));
            }
            return dictionary2;
        }

        internal static PdfDestination Parse(PdfDocumentCatalog documentCatalog, object value)
        {
            PdfName name;
            object obj3;
            object obj4;
            object obj5;
            double? nullable;
            double? nullable1;
            double? nullable2;
            double? nullable3;
            IList<object> array = value as IList<object>;
            if (array == null)
            {
                PdfReaderDictionary dictionary = value as PdfReaderDictionary;
                if (dictionary == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                array = dictionary.GetArray("D");
                if (array == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            int count = array.Count;
            if (count < 1)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            object pageObject = array[0];
            if (count == 1)
            {
                name = new PdfName("XYZ");
            }
            else
            {
                name = array[1] as PdfName;
                name = new PdfName("XYZ");
            }
            string s = name.Name;
            uint num2 = <PrivateImplementationDetails>.ComputeStringHash(s);
            if (num2 > 0x116f13cc)
            {
                if (num2 <= 0xa2fd3254)
                {
                    if (num2 != 0x88fd0966)
                    {
                        if ((num2 == 0xa2fd3254) && (s == "FitBV"))
                        {
                            return new PdfFitBBoxVerticallyDestination(documentCatalog, pageObject, GetSingleValue(array));
                        }
                    }
                    else if (s == "FitBH")
                    {
                        return new PdfFitBBoxHorizontallyDestination(documentCatalog, pageObject, GetSingleValue(array));
                    }
                }
                else if (num2 != 0xf5dbb8cc)
                {
                    if ((num2 == 0xffdbc88a) && (s == "FitB"))
                    {
                        return new PdfFitBBoxDestination(documentCatalog, pageObject);
                    }
                }
                else if (s == "FitH")
                {
                    return new PdfFitHorizontallyDestination(documentCatalog, pageObject, GetSingleValue(array));
                }
                goto TR_0001;
            }
            else if (num2 > 0xbdbdb6e)
            {
                if (num2 != 0xfdbe1ba)
                {
                    if ((num2 == 0x116f13cc) && (s == "Fit"))
                    {
                        return new PdfFitDestination(documentCatalog, pageObject);
                    }
                }
                else if (s == "FitR")
                {
                    if (count < 6)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    object[] objArray1 = new object[] { array[2], array[3], array[4], array[5] };
                    return new PdfFitRectangleDestination(documentCatalog, pageObject, PdfRectangle.Parse(objArray1, documentCatalog.Objects));
                }
                goto TR_0001;
            }
            else
            {
                if (num2 == 0x77c0000)
                {
                    if (s == "XYZ")
                    {
                        obj5 = null;
                        switch (count)
                        {
                            case 1:
                            case 2:
                                obj3 = null;
                                obj4 = null;
                                goto TR_000C;

                            case 3:
                                obj3 = null;
                                obj4 = array[2];
                                goto TR_000C;

                            case 4:
                                break;

                            default:
                                obj5 = array[4];
                                break;
                        }
                        obj3 = array[2];
                        obj4 = array[3];
                        goto TR_000C;
                    }
                }
                else if ((num2 == 0xbdbdb6e) && (s == "FitV"))
                {
                    return new PdfFitVerticallyDestination(documentCatalog, pageObject, GetSingleValue(array));
                }
                goto TR_0001;
            }
            goto TR_000C;
        TR_0001:
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            return null;
        TR_000C:
            if (obj3 != null)
            {
                nullable1 = new double?(PdfDocumentReader.ConvertToDouble(obj3));
            }
            else
            {
                nullable = null;
                nullable1 = nullable;
            }
            if (obj4 != null)
            {
                nullable2 = new double?(PdfDocumentReader.ConvertToDouble(obj4));
            }
            else
            {
                nullable = null;
                nullable2 = nullable;
            }
            if (obj5 != null)
            {
                nullable3 = new double?(PdfDocumentReader.ConvertToDouble(obj5));
            }
            else
            {
                nullable = null;
                nullable3 = nullable;
            }
            return new PdfXYZDestination(documentCatalog, pageObject, nullable1, nullable2, nullable3);
        }

        internal void ResolveInternalPage()
        {
            this.ResolvePage();
            if ((this.page == null) && ((this.documentCatalog != null) && (this.pageIndex >= 0)))
            {
                IList<PdfPage> pages = this.documentCatalog.Pages;
                if (this.pageIndex < pages.Count)
                {
                    this.page = pages[this.pageIndex];
                    this.pageIndex = -1;
                }
            }
        }

        private void ResolvePage()
        {
            if (this.pageObject != null)
            {
                PdfObjectReference pageObject = this.pageObject as PdfObjectReference;
                if (pageObject != null)
                {
                    this.page = this.documentCatalog.Objects.GetPage(pageObject.Number);
                }
                else if (this.pageObject is int)
                {
                    this.pageIndex = (int) this.pageObject;
                }
                this.pageObject = null;
            }
        }

        protected internal override object ToWritableObject(PdfObjectCollection collection)
        {
            List<object> parameters = new List<object>();
            this.ResolvePage();
            if (this.pageId != null)
            {
                PdfObjectReference savedObjectReference = collection.GetSavedObjectReference(this.pageId.Value);
                if (savedObjectReference == null)
                {
                    return null;
                }
                parameters.Add(savedObjectReference);
            }
            else if (this.page == null)
            {
                parameters.Add((this.pageIndex == -1) ? null : ((object) this.pageIndex));
            }
            else
            {
                parameters.Add(collection.AddObject((PdfObject) this.page));
            }
            this.AddWriteableParameters(parameters);
            return new PdfWritableArray(parameters);
        }

        protected double? ValidateVerticalCoordinate(double? top)
        {
            if ((top != null) && (this.page != null))
            {
                top = new double?(PdfMathUtils.Min(top.Value, this.page.CropBox.Height));
            }
            return top;
        }

        public PdfPage Page
        {
            get
            {
                this.ResolvePage();
                return this.page;
            }
        }

        public int PageIndex
        {
            get
            {
                this.ResolvePage();
                return ((this.pageIndex < 0) ? this.CalculatePageIndex(this.documentCatalog.Pages) : this.pageIndex);
            }
        }

        internal PdfDocumentCatalog DocumentCatalog =>
            this.documentCatalog;
    }
}

