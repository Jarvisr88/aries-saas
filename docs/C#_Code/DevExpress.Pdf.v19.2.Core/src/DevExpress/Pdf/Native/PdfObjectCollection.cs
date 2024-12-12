namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Localization;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class PdfObjectCollection
    {
        private const string fieldNamePrefix = "Field";
        internal const string TrailerSizeKey = "Size";
        internal const string TrailerInfoKey = "Info";
        internal const string TrailerRootKey = "Root";
        private readonly Guid id;
        private readonly IDictionary<int, PdfDocumentItem> collection;
        private readonly Dictionary<int, WeakReference> resolvedObjects;
        private readonly HashSet<PdfObject> resolvedObjectsReferences;
        private readonly Dictionary<int, PdfInteractiveFormField> resolvedInteractiveFormFields;
        private readonly Dictionary<IPdfObjectId, int> writtenObjects;
        private readonly HashSet<IPdfObjectId> clonedPageIds;
        private readonly HashSet<IPdfObjectId> clonedDirectObjects;
        private readonly Dictionary<string, string> renamedDestinations;
        private readonly Dictionary<string, string> renamedFormField;
        private readonly Dictionary<IPdfObjectId, PdfObject> indirectDeferredObjects;
        private readonly Dictionary<int, PdfObject> deferredObjects;
        private PdfDocumentCatalog documentCatalog;
        private PdfEncryptionInfo encryptionInfo;
        private PdfDocumentStream documentStream;
        private int lastObjectNumber;
        private Func<PdfObjectContainer, PdfObjectPointer> writeIndirectObject;
        private bool wasClonedWidget;
        private int lastRenamedFormFieldNumber;
        private HashSet<string> savedDestinationNames;
        private Guid foreignCollectionId;
        private bool isFinalizing;

        public event EventHandler ElementWriting;

        public PdfObjectCollection(PdfDocumentStream documentStream)
        {
            this.id = Guid.NewGuid();
            this.collection = new Dictionary<int, PdfDocumentItem>();
            this.resolvedObjects = new Dictionary<int, WeakReference>();
            this.resolvedObjectsReferences = new HashSet<PdfObject>();
            this.resolvedInteractiveFormFields = new Dictionary<int, PdfInteractiveFormField>();
            this.writtenObjects = new Dictionary<IPdfObjectId, int>();
            this.clonedPageIds = new HashSet<IPdfObjectId>();
            this.clonedDirectObjects = new HashSet<IPdfObjectId>();
            this.renamedDestinations = new Dictionary<string, string>();
            this.renamedFormField = new Dictionary<string, string>();
            this.indirectDeferredObjects = new Dictionary<IPdfObjectId, PdfObject>();
            this.deferredObjects = new Dictionary<int, PdfObject>();
            this.documentStream = documentStream;
        }

        public PdfObjectCollection(PdfDocumentStream documentStream, Func<PdfObjectContainer, PdfObjectPointer> writeIndirectObject) : this(documentStream)
        {
            this.writeIndirectObject = writeIndirectObject;
        }

        private bool AddClonedPage(PdfPage page, Guid pageCollectionId) => 
            this.clonedPageIds.Add(CreateObjectId(page, pageCollectionId));

        private PdfObject AddDeferredObject(PdfObject value, Guid valueCollectionID)
        {
            PdfObject deferredSavedObject;
            bool isCloning = this.IsCloning;
            if (value.ObjectNumber == -1)
            {
                deferredSavedObject = value.GetDeferredSavedObject(this, isCloning);
                this.deferredObjects[deferredSavedObject.ObjectNumber] = deferredSavedObject;
                this.AddResolvedObject(deferredSavedObject.ObjectNumber, deferredSavedObject);
            }
            else
            {
                PdfIndirectObjectId key = new PdfIndirectObjectId(valueCollectionID, value.ObjectNumber);
                if (this.indirectDeferredObjects.TryGetValue(key, out deferredSavedObject))
                {
                    if (isCloning)
                    {
                        value.UpdateObject(deferredSavedObject);
                    }
                }
                else
                {
                    deferredSavedObject = value.GetDeferredSavedObject(this, isCloning);
                    int objectNumber = deferredSavedObject.ObjectNumber;
                    this.indirectDeferredObjects.Add(key, deferredSavedObject);
                    this.deferredObjects[deferredSavedObject.ObjectNumber] = deferredSavedObject;
                    if (isCloning || !this.resolvedObjects.ContainsKey(objectNumber))
                    {
                        this.AddResolvedObject(objectNumber, deferredSavedObject);
                    }
                }
            }
            this.AddToWrittenObject(deferredSavedObject);
            return deferredSavedObject;
        }

        public PdfObjectReference AddDictionary(PdfDictionary dictionary) => 
            this.AddObject(dictionary);

        public void AddFreeObject(int number, int generation)
        {
            if ((number == 0) || !this.collection.ContainsKey(number))
            {
                this.collection[number] = new PdfFreeObject(number, generation);
                this.LastObjectNumber = number;
            }
        }

        public void AddItem(PdfDocumentItem obj, bool force)
        {
            int objectNumber = obj.ObjectNumber;
            if (force || !this.collection.ContainsKey(objectNumber))
            {
                this.collection[objectNumber] = obj;
                this.LastObjectNumber = objectNumber;
            }
        }

        public PdfObjectReference AddObject(PdfObject value) => 
            (value == null) ? null : this.WriteExternalObject(this.IsCloning ? this.foreignCollectionId : this.id, value);

        private PdfObjectReference AddObject(object value)
        {
            int num2 = this.LastObjectNumber + 1;
            this.LastObjectNumber = num2;
            int number = num2;
            if (this.writeIndirectObject == null)
            {
                this.AddItem(new PdfObjectContainer(number, 0, value), true);
            }
            else
            {
                this.writeIndirectObject(new PdfObjectContainer(number, 0, value));
            }
            this.RaiseElementWritingEvent();
            return new PdfObjectReference(number);
        }

        private PdfObjectReference AddObjectClone(PdfObject value, Guid foreignCollectionId)
        {
            PdfObjectReference reference;
            Func<PdfObjectContainer, PdfObjectPointer> writeIndirectObject = this.writeIndirectObject;
            try
            {
                this.foreignCollectionId = foreignCollectionId;
                if (writeIndirectObject != null)
                {
                    this.writeIndirectObject = delegate (PdfObjectContainer container) {
                        PdfObjectPointer pointer = writeIndirectObject(container);
                        pointer.ApplyEncryption = this.encryptionInfo != null;
                        this.AddItem(pointer, false);
                        this.AddPointer(pointer);
                        return pointer;
                    };
                    reference = this.AddObject(value);
                }
                else
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        PdfDocumentStream documentStream = PdfDocumentStream.CreateStreamForMerging(stream);
                        PdfObjectWriter writer = new PdfObjectWriter(documentStream);
                        this.writeIndirectObject = delegate (PdfObjectContainer container) {
                            documentStream.Reset();
                            PdfIndirectObject obj2 = writer.WriteIndirectObject(container) as PdfIndirectObject;
                            if (obj2 != null)
                            {
                                obj2.ApplyEncryption = false;
                                this.AddItem(obj2, false);
                            }
                            return obj2;
                        };
                        reference = this.AddObject(value);
                    }
                }
            }
            finally
            {
                this.foreignCollectionId = Guid.Empty;
                this.writeIndirectObject = writeIndirectObject;
            }
            return reference;
        }

        public PdfObjectReference AddObjectToWrite(int objectNumber, Func<PdfObject> getObject)
        {
            Guid collectionId = this.IsCloning ? this.foreignCollectionId : this.id;
            int indirectWrittenObjectNumber = this.GetIndirectWrittenObjectNumber(new PdfIndirectObjectId(collectionId, objectNumber));
            return ((indirectWrittenObjectNumber != -1) ? new PdfObjectReference(indirectWrittenObjectNumber) : this.AddObject(getObject()));
        }

        private void AddPointer(PdfObjectPointer slot)
        {
            int objectNumber = slot.ObjectNumber;
            this.writtenObjects.Add(new PdfIndirectObjectId(this.id, objectNumber), objectNumber);
        }

        private void AddRelatedObjects(PdfObject value, int number)
        {
            this.AddToClonedDirectObjects(new PdfDirectObjectId(value));
            PdfInteractiveFormField field = value as PdfInteractiveFormField;
            if ((field != null) && (field.Widget != null))
            {
                PdfDirectObjectId key = new PdfDirectObjectId(field.Widget);
                this.writtenObjects.Add(key, number);
                this.AddToClonedDirectObjects(key);
            }
            else
            {
                PdfWidgetAnnotation annotation = value as PdfWidgetAnnotation;
                if ((annotation != null) && (annotation.InteractiveFormField != null))
                {
                    PdfDirectObjectId key = new PdfDirectObjectId(annotation.InteractiveFormField);
                    this.writtenObjects.Add(key, number);
                    this.AddToClonedDirectObjects(key);
                }
                else
                {
                    PdfAnnotationAppearances appearances = value as PdfAnnotationAppearances;
                    if ((appearances != null) && (appearances.Form != null))
                    {
                        PdfDirectObjectId key = new PdfDirectObjectId(appearances.Form);
                        this.writtenObjects.Add(key, number);
                        this.AddToClonedDirectObjects(key);
                    }
                }
            }
        }

        public int AddResolvedObject(PdfObject value) => 
            this.AddResolvedObject(value, false);

        public int AddResolvedObject(PdfObject value, bool force)
        {
            if (value == null)
            {
                return 0;
            }
            int objectNumber = value.ObjectNumber;
            if (!this.resolvedObjectsReferences.Contains(value) && ((objectNumber == -1) || !this.collection.ContainsKey(objectNumber)))
            {
                int num2 = this.LastObjectNumber + 1;
                this.LastObjectNumber = num2;
                objectNumber = num2;
                value.ObjectNumber = objectNumber;
                if (!force && (this.writeIndirectObject != null))
                {
                    this.AddObject(value);
                }
                else
                {
                    this.AddResolvedObject(objectNumber, value);
                    this.resolvedObjectsReferences.Add(value);
                }
            }
            return objectNumber;
        }

        private void AddResolvedObject(int objectNumber, PdfObject value)
        {
            this.resolvedObjects.Add(objectNumber, new WeakReference(value));
        }

        public void AddSavedDestinationName(string name)
        {
            if ((this.savedDestinationNames != null) && (!string.IsNullOrEmpty(name) && !this.savedDestinationNames.Contains(name)))
            {
                this.savedDestinationNames.Add(name);
            }
        }

        public PdfObjectReference AddStream(byte[] data) => 
            this.AddStream(new PdfWriterDictionary(this), data);

        public PdfObjectReference AddStream(PdfStream stream) => 
            this.AddObject(stream);

        public PdfObjectReference AddStream(PdfWriterDictionary dictionary, byte[] data)
        {
            this.RaiseElementWritingEvent();
            return this.AddObject(new PdfArrayCompressedData(data).CreateWriterStream(dictionary));
        }

        private void AddToClonedDirectObjects(PdfDirectObjectId id)
        {
            if (this.IsCloning)
            {
                this.clonedDirectObjects.Add(id);
            }
        }

        private int AddToWrittenObject(PdfObject value)
        {
            int objectNumber;
            IPdfObjectId key = CreateObjectId(value, this.id);
            if (!this.writtenObjects.TryGetValue(key, out objectNumber))
            {
                objectNumber = value.ObjectNumber;
                if (objectNumber == -1)
                {
                    objectNumber = this.GetNextObjectNumber();
                }
                this.writtenObjects.Add(key, objectNumber);
            }
            return objectNumber;
        }

        public int CloneObject(PdfObject value, Guid foreignCollectionId)
        {
            PdfObjectReference reference = this.AddObjectClone(value, foreignCollectionId);
            return ((reference == null) ? -1 : reference.Number);
        }

        private void CloneOutlines(Guid foreignCollectionId, IList<PdfBookmark> bookmarks, IList<PdfBookmark> foreignBookmarks)
        {
            foreach (PdfBookmark bookmark in new List<PdfBookmark>(foreignBookmarks))
            {
                PdfDestinationObject destinationObject = bookmark.DestinationObject;
                if (destinationObject != null)
                {
                    string destinationName = destinationObject.DestinationName;
                    if (!string.IsNullOrEmpty(destinationName))
                    {
                        this.AddSavedDestinationName(destinationName);
                        destinationObject = new PdfDestinationObject(this.GetDestinationName(destinationName));
                    }
                    else
                    {
                        PdfDestination destination = bookmark.Destination;
                        destinationObject = (destination == null) ? null : new PdfDestinationObject(this.AddDeferredObject(destination, foreignCollectionId) as PdfDestination);
                    }
                }
                PdfAction action = bookmark.Action;
                if (action != null)
                {
                    action = this.GetAction(this.AddObjectClone(action, foreignCollectionId));
                }
                PdfBookmark bookmark1 = new PdfBookmark(bookmark, destinationObject, action);
                bookmark1.Parent = this.documentCatalog;
                PdfBookmark item = bookmark1;
                bookmarks.Add(item);
                this.CloneOutlines(foreignCollectionId, item.Children, bookmark.Children);
            }
        }

        public IList<PdfPage> ClonePages(IList<PdfPage> pages, bool cloneNonPageContentElements)
        {
            PdfCompatibility compatibility = this.documentCatalog.CreationOptions.Compatibility;
            if ((compatibility != PdfCompatibility.Pdf) && !this.documentCatalog.CreationOptions.MergePdfADocuments)
            {
                throw new NotSupportedException(string.Format(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncompatibleOperationWithCurrentDocumentFormat), compatibility));
            }
            List<PdfPage> list = new List<PdfPage>();
            if ((pages != null) && (pages.Count > 0))
            {
                PdfDocumentCatalog documentCatalog = pages[0].DocumentCatalog;
                HashSet<PdfIndirectObjectId> fakeObjects = new HashSet<PdfIndirectObjectId>();
                try
                {
                    this.PrepareToClone(documentCatalog);
                    Guid fakeCollectionId = Guid.NewGuid();
                    Guid id = documentCatalog.Objects.id;
                    bool flag = id == this.id;
                    PdfPageTreeNode node = this.documentCatalog.Pages.GetPageNode(this, false);
                    foreach (PdfPage page in pages)
                    {
                        Guid guid3;
                        Func<PdfReaderDictionary, PdfPage> <>9__0;
                        if (!flag && this.AddClonedPage(page, id))
                        {
                            guid3 = id;
                        }
                        else
                        {
                            guid3 = fakeCollectionId;
                            foreach (int num in page.Resources.ObjectNumbers)
                            {
                                fakeObjects.Add(this.RegisterFakeObject(fakeCollectionId, id, num));
                            }
                        }
                        this.wasClonedWidget = false;
                        Func<PdfReaderDictionary, PdfPage> create = <>9__0;
                        if (<>9__0 == null)
                        {
                            Func<PdfReaderDictionary, PdfPage> local1 = <>9__0;
                            create = <>9__0 = dictionary => new PdfPage(node, dictionary);
                        }
                        PdfPage item = this.GetObject<PdfPage>(this.AddObjectClone(page, guid3), create);
                        if (this.wasClonedWidget)
                        {
                            foreach (PdfAnnotation annotation in item.Annotations)
                            {
                                PdfWidgetAnnotation annotation2 = annotation as PdfWidgetAnnotation;
                                if (annotation2 != null)
                                {
                                    this.documentCatalog.GetExistingOrCreateNewInteractiveForm().AddInteractiveFormField(annotation2.InteractiveFormField);
                                }
                            }
                        }
                        list.Add(item);
                    }
                    if (cloneNonPageContentElements)
                    {
                        this.CloneOutlines(id, this.documentCatalog.Bookmarks, documentCatalog.Bookmarks);
                        PdfDeferredSortedDictionary<string, PdfFileSpecification> embeddedFiles = (PdfDeferredSortedDictionary<string, PdfFileSpecification>) this.documentCatalog.Names.EmbeddedFiles;
                        foreach (PdfFileSpecification specification in documentCatalog.Names.EmbeddedFiles.Values)
                        {
                            embeddedFiles.AddDeferred(PdfNames.NewKey<PdfFileSpecification>(embeddedFiles), this.AddObjectClone(specification, id), new Func<object, PdfFileSpecification>(this.GetFileSpecification));
                        }
                    }
                    this.FinalizeClone(documentCatalog.Destinations, id);
                }
                finally
                {
                    this.RemoveUnnecessaryObjects(fakeObjects);
                    this.renamedDestinations.Clear();
                    this.renamedFormField.Clear();
                    PdfInteractiveForm acroForm = documentCatalog.AcroForm;
                    if (acroForm != null)
                    {
                        acroForm.Resources.ClearRenamedResources();
                    }
                    this.savedDestinationNames = null;
                }
            }
            return list;
        }

        private static IPdfObjectId CreateObjectId(PdfObject value, Guid collectionId)
        {
            if (value == null)
            {
                return null;
            }
            int objectNumber = value.ObjectNumber;
            return ((objectNumber != -1) ? ((IPdfObjectId) new PdfIndirectObjectId(collectionId, objectNumber)) : ((IPdfObjectId) new PdfDirectObjectId(value)));
        }

        public PdfEncryptionInfo EnsureEncryptionInfo(object value, byte[][] id, PdfGetPasswordAction getPasswordAction)
        {
            if (this.encryptionInfo == null)
            {
                object obj2 = this.TryResolve(value, null);
                if (obj2 == null)
                {
                    return null;
                }
                PdfReaderDictionary dictionary = obj2 as PdfReaderDictionary;
                if (dictionary == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.encryptionInfo = new PdfEncryptionInfo(dictionary, id, getPasswordAction);
                if (this.documentStream != null)
                {
                    this.documentStream.EncryptionInfo = this.encryptionInfo;
                }
            }
            return this.encryptionInfo;
        }

        private void FinalizeClone(IDictionary<string, PdfDestination> foreignDestinations, Guid foreignCollectionId)
        {
            this.documentCatalog.FileAttachments.InvalidateSearchedFileAnnotations();
            PdfNames names = this.documentCatalog.Names;
            foreach (string str in this.savedDestinationNames)
            {
                PdfDestination destination;
                if (foreignDestinations.TryGetValue(str, out destination) && (destination != null))
                {
                    Func<object, PdfDestination> create = <>c.<>9__125_0;
                    if (<>c.<>9__125_0 == null)
                    {
                        Func<object, PdfDestination> local1 = <>c.<>9__125_0;
                        create = <>c.<>9__125_0 = o => o as PdfDestination;
                    }
                    names.AddDeferredDestination(this.GetDestinationName(str), this.AddDeferredObject(destination, foreignCollectionId), create);
                }
            }
            this.NotifyMergeCompleted(this.deferredObjects.Values);
        }

        private void FinalizeWriting()
        {
            this.writtenObjects.Clear();
            this.clonedPageIds.Clear();
        }

        public void FinalizeWritingAndClearWriteParameters()
        {
            this.isFinalizing = true;
            try
            {
                foreach (PdfObject obj2 in this.deferredObjects.Values)
                {
                    this.WriteObject(obj2, obj2.ObjectNumber);
                }
                this.FinalizeWriting();
                this.indirectDeferredObjects.Clear();
                this.deferredObjects.Clear();
                this.writeIndirectObject = null;
            }
            finally
            {
                this.isFinalizing = false;
            }
        }

        public PdfAction GetAction(object value) => 
            this.GetObject<PdfAction>(value, new Func<PdfReaderDictionary, PdfAction>(PdfAction.Parse));

        public PdfAnnotation GetAnnotation(PdfPage page, object value) => 
            this.GetObject<PdfAnnotation>(value, dictionary => PdfAnnotation.Parse(page, dictionary));

        public PdfAnnotationAppearances GetAnnotationAppearances(object value, PdfRectangle parentBBox)
        {
            if (value == null)
            {
                return null;
            }
            PdfReaderDictionary dictionary = value as PdfReaderDictionary;
            if (dictionary != null)
            {
                return new PdfAnnotationAppearances(dictionary, parentBBox);
            }
            PdfReaderStream stream = value as PdfReaderStream;
            if (stream != null)
            {
                return new PdfAnnotationAppearances(PdfForm.Create(stream, null));
            }
            PdfObjectReference reference = value as PdfObjectReference;
            if (reference == null)
            {
                return null;
            }
            int number = reference.Number;
            PdfAnnotationAppearances resolvedObject = this.GetResolvedObject<PdfAnnotationAppearances>(number);
            if (resolvedObject == null)
            {
                object obj2 = this.TryResolve(reference, null);
                if (obj2 is PdfReaderStream)
                {
                    return new PdfAnnotationAppearances(this.GetForm(reference));
                }
                dictionary = obj2 as PdfReaderDictionary;
                if (dictionary == null)
                {
                    return null;
                }
                resolvedObject = new PdfAnnotationAppearances(dictionary, parentBBox);
                this.AddResolvedObject(number, resolvedObject);
            }
            return resolvedObject;
        }

        public PdfColorSpace GetColorSpace(object value) => 
            this.GetObject<PdfColorSpace>(value, obj => PdfColorSpace.Parse(this, obj));

        public PdfObject GetDeferredObject(int objectNumber)
        {
            PdfObject obj2;
            return (!this.deferredObjects.TryGetValue(objectNumber, out obj2) ? null : obj2);
        }

        public PdfDestination GetDestination(object value)
        {
            PdfReaderDictionary dictionary = this.TryResolve(value, null) as PdfReaderDictionary;
            if ((dictionary == null) || (dictionary.GetName("S") == null))
            {
                return this.GetObject<PdfDestination>(value, o => PdfDestination.Parse(this.documentCatalog, o));
            }
            PdfJumpAction action = this.GetAction(value) as PdfJumpAction;
            if (action == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return action.Destination;
        }

        public string GetDestinationName(string currentName) => 
            this.GetRenamedName(this.renamedDestinations, currentName);

        public double GetDouble(object value) => 
            PdfDocumentReader.ConvertToDouble(this.TryResolve(value, null));

        public PdfFileSpecification GetFileSpecification(object value) => 
            this.GetObject<PdfFileSpecification>(value, new Func<object, PdfFileSpecification>(PdfFileSpecification.Parse));

        public PdfForm GetForm(object value)
        {
            Func<object, PdfForm> create = <>c.<>9__70_0;
            if (<>c.<>9__70_0 == null)
            {
                Func<object, PdfForm> local1 = <>c.<>9__70_0;
                create = <>c.<>9__70_0 = delegate (object o) {
                    PdfReaderStream stream = o as PdfReaderStream;
                    if (stream == null)
                    {
                        return null;
                    }
                    string name = stream.Dictionary.GetName("Type");
                    return ((name == null) || (name == "XObject")) ? new PdfForm(stream, null) : null;
                };
            }
            return this.GetObject<PdfForm>(value, create);
        }

        public string GetFormFieldName(string currentName) => 
            this.GetRenamedName(this.renamedFormField, currentName);

        public PdfIndirectObject GetIndirectObject(int number)
        {
            PdfDocumentItem item;
            if (!this.collection.TryGetValue(number, out item))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            PdfIndirectObject obj2 = item as PdfIndirectObject;
            if (obj2 == null)
            {
                PdfObjectSlot slot = item as PdfObjectSlot;
                if (slot == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                obj2 = this.ReadIndirectObject(slot);
                obj2.ApplyEncryption = slot.ApplyEncryption;
            }
            return obj2;
        }

        private int GetIndirectWrittenObjectNumber(PdfIndirectObjectId id)
        {
            int num;
            return (this.writtenObjects.TryGetValue(id, out num) ? num : -1);
        }

        public PdfInteractiveFormField GetInteractiveFormField(PdfInteractiveForm form, PdfInteractiveFormField parent, object value)
        {
            PdfInteractiveFormField field;
            PdfObjectReference reference = value as PdfObjectReference;
            if (reference == null)
            {
                return null;
            }
            int number = reference.Number;
            if (!this.resolvedInteractiveFormFields.TryGetValue(number, out field))
            {
                value = this.TryResolve(value, null);
                if (value == null)
                {
                    return null;
                }
                PdfReaderDictionary dictionary = value as PdfReaderDictionary;
                if (dictionary == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                field = PdfInteractiveFormField.Parse(form, parent, dictionary, reference);
                if (field != null)
                {
                    this.resolvedInteractiveFormFields.Add(number, field);
                }
            }
            return field;
        }

        public PdfLogicalStructureItem GetLogicalStructureItem(PdfLogicalStructure logicalStructure, PdfLogicalStructureEntry parent, object value) => 
            this.GetObject<PdfLogicalStructureItem>(value, o => PdfLogicalStructureItem.Parse(logicalStructure, parent, this.TryResolve(o, null)));

        public int GetNextObjectNumber()
        {
            int num = this.LastObjectNumber + 1;
            this.LastObjectNumber = num;
            return num;
        }

        public T GetObject<T>(object value, Func<PdfReaderDictionary, T> create) where T: PdfObject
        {
            Func<object, T> func = delegate (object o) {
                PdfReaderDictionary arg = o as PdfReaderDictionary;
                if (arg != null)
                {
                    return create(arg);
                }
                return default(T);
            };
            return this.GetObject<T>(value, func);
        }

        public T GetObject<T>(object value, Func<object, T> create) where T: PdfObject
        {
            WeakReference reference;
            int number;
            if (value == null)
            {
                return default(T);
            }
            PdfObjectReference reference1 = value as PdfObjectReference;
            if (reference1 != null)
            {
                number = reference1.Number;
            }
            else
            {
                PdfObjectReference local1 = reference1;
                PdfReaderDictionary dictionary1 = value as PdfReaderDictionary;
                if (dictionary1 != null)
                {
                    number = dictionary1.Number;
                }
                else
                {
                    PdfReaderDictionary local4 = dictionary1;
                    PdfReaderStream stream1 = value as PdfReaderStream;
                    if (stream1 != null)
                    {
                        number = stream1.Dictionary.Number;
                    }
                    else
                    {
                        PdfReaderStream local5 = stream1;
                        number = -1;
                    }
                }
            }
            int key = number;
            if (key == -1)
            {
                return create(value);
            }
            if (this.resolvedObjects.TryGetValue(key, out reference))
            {
                T target = reference.Target as T;
                if (reference.IsAlive && (target != null))
                {
                    return target;
                }
                this.resolvedObjects.Remove(key);
            }
            value = this.TryResolve(value, null);
            if (value == null)
            {
                return default(T);
            }
            T local2 = create(value);
            if (local2 == null)
            {
                return default(T);
            }
            local2.ObjectNumber = key;
            this.AddResolvedObject(key, local2);
            return local2;
        }

        private object GetObject(int objectNumber, HashSet<int> resolvedReferences, string nonEncryptedDictionaryKey = null)
        {
            if (resolvedReferences.Contains(objectNumber))
            {
                return null;
            }
            resolvedReferences.Add(objectNumber);
            object obj2 = this.ResolveObject(objectNumber);
            PdfObjectContainer container = obj2 as PdfObjectContainer;
            if (container != null)
            {
                return container.Value;
            }
            PdfIndirectObject indirectObject = obj2 as PdfIndirectObject;
            if (indirectObject != null)
            {
                return this.ParseIndirectObject(indirectObject, nonEncryptedDictionaryKey);
            }
            PdfObjectStreamElement element = obj2 as PdfObjectStreamElement;
            if (element == null)
            {
                if ((obj2 != null) && (!(obj2 is PdfFreeObject) && !(obj2 is PdfObjectSlot)))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                return null;
            }
            int objectStreamNumber = element.ObjectStreamNumber;
            PdfObjectStream stream = this.ResolveObject(objectStreamNumber) as PdfObjectStream;
            if (stream == null)
            {
                PdfReaderStream stream2 = this.GetObject(objectStreamNumber, resolvedReferences, null) as PdfReaderStream;
                if (stream2 == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                stream = new PdfObjectStream(stream2);
                this.ReplaceCollectionItem(stream);
            }
            return stream.Objects[element.ElementIndex];
        }

        public PdfIndirectObjectId GetObjectId(int objectNumber) => 
            new PdfIndirectObjectId(this.IsCloning ? this.foreignCollectionId : this.id, objectNumber);

        public PdfOptionalContent GetOptionalContent(object value)
        {
            Func<object, PdfOptionalContent> create = <>c.<>9__76_0;
            if (<>c.<>9__76_0 == null)
            {
                Func<object, PdfOptionalContent> local1 = <>c.<>9__76_0;
                create = <>c.<>9__76_0 = o => PdfOptionalContent.ParseOptionalContent(o as PdfReaderDictionary);
            }
            return this.GetObject<PdfOptionalContent>(value, create);
        }

        public PdfOptionalContentGroup GetOptionalContentGroup(object value, bool ignoreCollisions = false)
        {
            if (value == null)
            {
                return null;
            }
            PdfOptionalContentGroup optionalContent = this.GetOptionalContent(value) as PdfOptionalContentGroup;
            if ((optionalContent == null) && !ignoreCollisions)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return optionalContent;
        }

        public PdfPage GetPage(int objectNumber) => 
            this.GetResolvedObject<PdfPage>(objectNumber) ?? this.documentCatalog.Pages.FindPage(objectNumber);

        private string GetRenamedName(Dictionary<string, string> names, string currentName)
        {
            string str = currentName;
            if ((currentName != null) && ((names.Count > 0) && !names.TryGetValue(currentName, out str)))
            {
                str = currentName;
            }
            return str;
        }

        public PdfInteractiveFormField GetResolvedInteractiveFormField(PdfObjectReference reference)
        {
            PdfInteractiveFormField field;
            if (reference == null)
            {
                return null;
            }
            if (!this.resolvedInteractiveFormFields.TryGetValue(reference.Number, out field))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return field;
        }

        public T GetResolvedObject<T>(int objectNumber) where T: PdfObject
        {
            WeakReference reference;
            if (this.resolvedObjects.TryGetValue(objectNumber, out reference))
            {
                object target = reference.Target;
                if (reference.IsAlive)
                {
                    return (target as T);
                }
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return default(T);
        }

        public PdfObjectReference GetSavedObjectReference(PdfIndirectObjectId objectId)
        {
            int indirectWrittenObjectNumber = this.GetIndirectWrittenObjectNumber(objectId);
            if (indirectWrittenObjectNumber != -1)
            {
                return new PdfObjectReference(indirectWrittenObjectNumber);
            }
            PdfObjectCollection objA = this.documentCatalog.Objects;
            return (ReferenceEquals(objA, this) ? null : objA.GetSavedObjectReference(objectId));
        }

        public PdfXObject GetXObject(object value, PdfResources parentResources, string defaultSubtype) => 
            this.GetObject<PdfXObject>(value, o => PdfXObject.Parse(o as PdfReaderStream, parentResources, defaultSubtype));

        public int MarkObjectAsWritten(PdfObject value) => 
            (value == null) ? -1 : this.AddToWrittenObject(value);

        private void NotifyMergeCompleted(IEnumerable<PdfObject> objects)
        {
            foreach (PdfObject obj2 in objects)
            {
                if (obj2.SupportMergeCompletedNotifications)
                {
                    obj2.NotifyMergeCompleted(this);
                }
            }
        }

        public object ParseIndirectObject(PdfIndirectObject indirectObject, string nonEncryptedDictionaryKey = null) => 
            ((!indirectObject.ApplyEncryption || (this.encryptionInfo == null)) ? new PdfDocumentParser(this, indirectObject.ObjectNumber, indirectObject.ObjectGeneration, indirectObject.Stream) : new PdfEncryptedDocumentParser(this, indirectObject.ObjectNumber, indirectObject.ObjectGeneration, indirectObject.Stream, nonEncryptedDictionaryKey)).ReadObject(false, true);

        private void PrepareToClone(PdfDocumentCatalog foreignCatalog)
        {
            this.savedDestinationNames = new HashSet<string>();
            IDictionary<string, PdfDestination> destinations = this.documentCatalog.Destinations;
            foreach (string str in foreignCatalog.Destinations.Keys)
            {
                if (destinations.ContainsKey(str))
                {
                    this.renamedDestinations.Add(str, PdfNames.NewKey<PdfDestination>(destinations));
                }
            }
            PdfInteractiveForm acroForm = foreignCatalog.AcroForm;
            if ((acroForm != null) && ((acroForm.Fields != null) && (acroForm.Fields.Count > 0)))
            {
                PdfInteractiveForm existingOrCreateNewInteractiveForm = this.documentCatalog.GetExistingOrCreateNewInteractiveForm();
                existingOrCreateNewInteractiveForm.Resources.AppendInteractiveFormResources(acroForm.Resources);
                IList<PdfInteractiveFormField> fields = existingOrCreateNewInteractiveForm.Fields;
                if (fields.Count > 0)
                {
                    HashSet<string> set = new HashSet<string>();
                    foreach (PdfInteractiveFormField field in fields)
                    {
                        set.Add(field.Name);
                    }
                    foreach (PdfInteractiveFormField field2 in acroForm.Fields)
                    {
                        string name = field2.Name;
                        if (!string.IsNullOrEmpty(name) && set.Contains(name))
                        {
                            string item = "Field" + this.lastRenamedFormFieldNumber.ToString();
                            while (true)
                            {
                                if (!set.Contains(item))
                                {
                                    if (!this.renamedFormField.ContainsKey(name))
                                    {
                                        this.renamedFormField.Add(name, item);
                                        set.Add(item);
                                    }
                                    break;
                                }
                                int num = this.lastRenamedFormFieldNumber + 1;
                                this.lastRenamedFormFieldNumber = num;
                                item = "Field" + num;
                            }
                        }
                    }
                }
            }
        }

        public void PrepareToWrite(PdfDocumentCatalog documentCatalog)
        {
            if (documentCatalog != null)
            {
                this.LastObjectNumber = documentCatalog.Objects.LastObjectNumber;
                this.DocumentCatalog = documentCatalog;
            }
        }

        private void RaiseElementWritingEvent()
        {
            if (this.ElementWriting != null)
            {
                this.ElementWriting(this, EventArgs.Empty);
            }
        }

        private PdfIndirectObject ReadIndirectObject(PdfObjectSlot slot)
        {
            PdfIndirectObject obj2 = this.documentStream.ReadStreamBasedIndirectObject(slot.Offset);
            int objectNumber = slot.ObjectNumber;
            if (obj2.ObjectNumber == objectNumber)
            {
                return obj2;
            }
            PdfDocumentReader.FindObjects(this, this.documentStream);
            slot = this.collection[objectNumber] as PdfObjectSlot;
            if (slot == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return this.documentStream.ReadStreamBasedIndirectObject(slot.Offset);
        }

        private PdfIndirectObjectId RegisterFakeObject(Guid fakeCollectionId, Guid foreignCollectionId, int objectNumber)
        {
            PdfIndirectObjectId key = new PdfIndirectObjectId(fakeCollectionId, objectNumber);
            if (!this.writtenObjects.ContainsKey(key))
            {
                int num;
                PdfIndirectObjectId id2 = new PdfIndirectObjectId(foreignCollectionId, objectNumber);
                if (!this.writtenObjects.TryGetValue(id2, out num))
                {
                    num = objectNumber;
                }
                this.writtenObjects.Add(key, num);
            }
            return key;
        }

        public void RemoveCorruptedObjects()
        {
            foreach (int num in new List<int>(this.collection.Keys))
            {
                if (!(this.collection[num] is PdfObjectStreamElement))
                {
                    this.collection.Remove(num);
                }
            }
        }

        private void RemoveUnnecessaryObjects(IEnumerable<PdfIndirectObjectId> fakeObjects)
        {
            foreach (PdfIndirectObjectId id in fakeObjects)
            {
                this.writtenObjects.Remove(id);
            }
            foreach (PdfDirectObjectId id2 in this.clonedDirectObjects)
            {
                this.writtenObjects.Remove(id2);
            }
            this.clonedDirectObjects.Clear();
        }

        private void ReplaceCollectionItem(PdfDocumentItem obj)
        {
            PdfDocumentItem item;
            int objectNumber = obj.ObjectNumber;
            if (!this.collection.TryGetValue(objectNumber, out item))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.collection[objectNumber] = obj;
        }

        public void ResolveAllSlots()
        {
            foreach (int num in new List<int>(this.collection.Keys))
            {
                PdfDocumentItem item = this.collection[num];
                PdfObjectSlot slot = item as PdfObjectSlot;
                if (slot != null)
                {
                    item = this.ResolveSlot(slot, actualSlot => this.documentStream.ReadArrayBasedIndirectObject(actualSlot.Offset));
                    if ((item == null) || (item.ObjectNumber != num))
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                }
                if (item != null)
                {
                    this.ReplaceCollectionItem(item);
                }
            }
            this.documentStream = null;
        }

        private PdfDocumentItem ResolveObject(int number)
        {
            PdfDocumentItem item;
            return (this.collection.TryGetValue(number, out item) ? (this.ResolveSlot(item as PdfObjectSlot, new Func<PdfObjectSlot, PdfIndirectObject>(this.ReadIndirectObject)) ?? item) : null);
        }

        private PdfDocumentItem ResolveSlot(PdfObjectSlot slot, Func<PdfObjectSlot, PdfIndirectObject> readObject)
        {
            if (slot != null)
            {
                try
                {
                    PdfDocumentItem item;
                    if (slot.Offset == 0)
                    {
                        PdfFreeObject obj2 = new PdfFreeObject(slot.ObjectNumber, slot.ObjectGeneration);
                        this.ReplaceCollectionItem(obj2);
                        item = obj2;
                    }
                    else
                    {
                        PdfIndirectObject obj3 = readObject(slot);
                        obj3.ApplyEncryption = slot.ApplyEncryption;
                        item = obj3;
                    }
                    return item;
                }
                catch
                {
                }
            }
            return null;
        }

        public object TryResolve(object value, string nonEncryptedKey = null) => 
            this.TryResolve(value, nonEncryptedKey, new HashSet<int>());

        private object TryResolve(object value, string nonEncryptedKey, HashSet<int> resolvedReferences)
        {
            PdfObjectReference reference = value as PdfObjectReference;
            if (reference == null)
            {
                return value;
            }
            int number = reference.Number;
            object obj2 = this.GetObject(number, resolvedReferences, nonEncryptedKey);
            return this.TryResolve(obj2, nonEncryptedKey, resolvedReferences);
        }

        public void UpdateStream(Stream stream)
        {
            this.documentStream = PdfDocumentStream.CreateStreamForReading(new BufferedStream(stream));
            this.documentStream.EncryptionInfo = this.encryptionInfo;
        }

        private PdfObjectReference WriteExternalObject(Guid valueCollectionId, PdfObject value)
        {
            int num2;
            int objectNumber = value.ObjectNumber;
            IPdfObjectId key = CreateObjectId(value, valueCollectionId);
            if (!this.writtenObjects.TryGetValue(key, out num2))
            {
                bool isCloning = this.IsCloning;
                if (!this.isFinalizing && value.IsDeferredObject(isCloning))
                {
                    num2 = this.AddDeferredObject(value, valueCollectionId).ObjectNumber;
                }
                else
                {
                    num2 = (isCloning || (objectNumber == -1)) ? this.GetNextObjectNumber() : objectNumber;
                    this.writtenObjects.Add(key, num2);
                    if (objectNumber == -1)
                    {
                        this.AddRelatedObjects(value, num2);
                    }
                    if (!this.WriteObject(value, num2))
                    {
                        return null;
                    }
                }
            }
            return new PdfObjectReference(num2);
        }

        public bool WriteObject(PdfObject value, int number)
        {
            object obj2 = value.ToWritableObject(this);
            if (obj2 == null)
            {
                return false;
            }
            this.wasClonedWidget |= this.IsCloning && (value is PdfWidgetAnnotation);
            PdfObjectContainer container = new PdfObjectContainer(number, 0, obj2);
            if (this.writeIndirectObject == null)
            {
                this.AddItem(container, true);
            }
            else
            {
                this.writeIndirectObject(container);
            }
            this.RaiseElementWritingEvent();
            return true;
        }

        public Guid Id =>
            this.id;

        public int Count =>
            this.collection.Count;

        public PdfDocumentCatalog DocumentCatalog
        {
            get => 
                this.documentCatalog;
            set
            {
                int lastObjectNumber = this.LastObjectNumber;
                this.documentCatalog = value;
                this.documentCatalog.LastObjectNumber = lastObjectNumber;
            }
        }

        public PdfEncryptionInfo EncryptionInfo
        {
            get => 
                this.encryptionInfo;
            set
            {
                this.encryptionInfo = value;
                if (this.documentStream != null)
                {
                    this.documentStream.EncryptionInfo = value;
                }
            }
        }

        public PdfDocumentStream DocumentStream =>
            this.documentStream;

        public int LastObjectNumber
        {
            get => 
                (this.documentCatalog == null) ? this.lastObjectNumber : this.documentCatalog.LastObjectNumber;
            set
            {
                if (this.documentCatalog == null)
                {
                    this.lastObjectNumber = Math.Max(this.lastObjectNumber, value);
                }
                else
                {
                    this.documentCatalog.LastObjectNumber = value;
                }
            }
        }

        public int WrittenObjectsCount =>
            this.writtenObjects.Count;

        public IEnumerator<PdfObjectContainer> EnumeratorOfContainers =>
            new <get_EnumeratorOfContainers>d__45(0) { <>4__this=this };

        public bool IsCloning =>
            this.foreignCollectionId != Guid.Empty;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfObjectCollection.<>c <>9 = new PdfObjectCollection.<>c();
            public static Func<object, PdfForm> <>9__70_0;
            public static Func<object, PdfOptionalContent> <>9__76_0;
            public static Func<object, PdfDestination> <>9__125_0;

            internal PdfDestination <FinalizeClone>b__125_0(object o) => 
                o as PdfDestination;

            internal PdfForm <GetForm>b__70_0(object o)
            {
                PdfReaderStream stream = o as PdfReaderStream;
                if (stream == null)
                {
                    return null;
                }
                string name = stream.Dictionary.GetName("Type");
                return (((name == null) || (name == "XObject")) ? new PdfForm(stream, null) : null);
            }

            internal PdfOptionalContent <GetOptionalContent>b__76_0(object o) => 
                PdfOptionalContent.ParseOptionalContent(o as PdfReaderDictionary);
        }

        [CompilerGenerated]
        private sealed class <get_EnumeratorOfContainers>d__45 : IEnumerator<PdfObjectContainer>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private PdfObjectContainer <>2__current;
            public PdfObjectCollection <>4__this;
            private IEnumerator<int> <>7__wrap1;

            [DebuggerHidden]
            public <get_EnumeratorOfContainers>d__45(int <>1__state)
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

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<>7__wrap1 = this.<>4__this.collection.Keys.GetEnumerator();
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
                    while (true)
                    {
                        if (!this.<>7__wrap1.MoveNext())
                        {
                            this.<>m__Finally1();
                            this.<>7__wrap1 = null;
                            flag = false;
                        }
                        else
                        {
                            int current = this.<>7__wrap1.Current;
                            PdfObjectContainer container = this.<>4__this.collection[current] as PdfObjectContainer;
                            if (container == null)
                            {
                                continue;
                            }
                            this.<>2__current = container;
                            this.<>1__state = 1;
                            flag = true;
                        }
                        break;
                    }
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

            PdfObjectContainer IEnumerator<PdfObjectContainer>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

