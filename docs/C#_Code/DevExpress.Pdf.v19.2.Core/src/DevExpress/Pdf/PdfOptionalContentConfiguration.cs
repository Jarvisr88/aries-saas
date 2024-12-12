namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfOptionalContentConfiguration
    {
        private const string nameDictionaryKey = "Name";
        private const string creatorDictionaryKey = "Creator";
        private const string baseStateDictionaryKey = "BaseState";
        private const string onGroupsDictionaryKey = "ON";
        private const string offGroupsDictionaryKey = "OFF";
        private const string intentDictionaryKey = "Intent";
        private const string usageApplicationDictionaryKey = "AS";
        private const string orderDictionaryKey = "Order";
        private const string orderListModeDictionaryKey = "ListMode";
        private const string radioButtonGroupsDictionaryKey = "RBGroups";
        private const string lockedDictionaryKey = "Locked";
        private readonly string name;
        private readonly string creator;
        private readonly PdfOptionalContentState baseState;
        private readonly IList<PdfOptionalContentGroup> onGroups;
        private readonly IList<PdfOptionalContentGroup> offGroups;
        private readonly PdfOptionalContentIntent intent;
        private readonly IList<PdfOptionalContentUsageApplication> usageApplication;
        private readonly PdfOptionalContentOrder order;
        private readonly PdfOptionalContentOrderListMode orderListMode;
        private readonly IList<PdfOptionalContentRadioButtonGroup> radioButtonGroups;
        private readonly IList<PdfOptionalContentGroup> locked;

        internal PdfOptionalContentConfiguration(PdfReaderDictionary dictionary)
        {
            if (dictionary == null)
            {
                this.intent = PdfOptionalContentIntent.View;
                this.onGroups = new List<PdfOptionalContentGroup>();
                this.offGroups = new List<PdfOptionalContentGroup>();
                this.radioButtonGroups = new List<PdfOptionalContentRadioButtonGroup>();
            }
            else
            {
                PdfObjectCollection objects = dictionary.Objects;
                this.name = dictionary.GetTextString("Name");
                this.creator = dictionary.GetTextString("Creator");
                this.baseState = dictionary.GetEnumName<PdfOptionalContentState>("BaseState");
                IList<PdfOptionalContentGroup> array = dictionary.GetArray<PdfOptionalContentGroup>("ON", oc => objects.GetOptionalContentGroup(oc, false));
                IList<PdfOptionalContentGroup> list3 = array;
                if (array == null)
                {
                    IList<PdfOptionalContentGroup> local1 = array;
                    list3 = new List<PdfOptionalContentGroup>();
                }
                this.onGroups = list3;
                IList<PdfOptionalContentGroup> list2 = dictionary.GetArray<PdfOptionalContentGroup>("OFF", oc => objects.GetOptionalContentGroup(oc, false));
                IList<PdfOptionalContentGroup> list4 = list2;
                if (list2 == null)
                {
                    IList<PdfOptionalContentGroup> local2 = list2;
                    list4 = new List<PdfOptionalContentGroup>();
                }
                this.offGroups = list4;
                this.intent = dictionary.GetOptionalContentIntent("Intent");
                this.usageApplication = dictionary.GetArray<PdfOptionalContentUsageApplication>("AS", delegate (object v) {
                    PdfReaderDictionary dictionary = objects.TryResolve(v, null) as PdfReaderDictionary;
                    if (dictionary == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    return new PdfOptionalContentUsageApplication(dictionary);
                });
                IList<object> list = dictionary.GetArray("Order", true);
                if (list != null)
                {
                    this.order = new PdfOptionalContentOrder(objects, list);
                }
                this.orderListMode = dictionary.GetEnumName<PdfOptionalContentOrderListMode>("ListMode");
                this.radioButtonGroups = dictionary.GetArray<PdfOptionalContentRadioButtonGroup>("RBGroups", delegate (object v) {
                    IList<object> list = objects.TryResolve(v, null) as IList<object>;
                    if (list == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    PdfOptionalContentRadioButtonGroup group = new PdfOptionalContentRadioButtonGroup();
                    foreach (object obj2 in list)
                    {
                        group.Add(objects.GetOptionalContentGroup(obj2, false));
                    }
                    return group;
                });
                this.radioButtonGroups ??= new List<PdfOptionalContentRadioButtonGroup>();
                this.locked = dictionary.GetArray<PdfOptionalContentGroup>("Locked", oc => objects.GetOptionalContentGroup(oc, false));
            }
        }

        internal object Write(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.Add("Name", this.name, null);
            dictionary.Add("Creator", this.creator, null);
            dictionary.AddEnumName<PdfOptionalContentState>("BaseState", this.baseState);
            dictionary.AddList<PdfOptionalContentGroup>("ON", this.onGroups);
            dictionary.AddList<PdfOptionalContentGroup>("OFF", this.offGroups);
            dictionary.AddIntent("Intent", this.intent);
            dictionary.AddList<PdfOptionalContentUsageApplication>("AS", this.usageApplication, o => o.Write(objects));
            if (this.order != null)
            {
                dictionary.Add("Order", this.order.Write(objects));
            }
            dictionary.AddEnumName<PdfOptionalContentOrderListMode>("ListMode", this.orderListMode);
            dictionary.AddList<PdfOptionalContentRadioButtonGroup>("RBGroups", this.radioButtonGroups, o => new PdfWritableObjectArray((IEnumerable<PdfObject>) o, objects));
            dictionary.AddList<PdfOptionalContentGroup>("Locked", this.locked);
            return dictionary;
        }

        public string Name =>
            this.name;

        public string Creator =>
            this.creator;

        public PdfOptionalContentState BaseState =>
            this.baseState;

        public IList<PdfOptionalContentGroup> On =>
            this.onGroups;

        public IList<PdfOptionalContentGroup> Off =>
            this.offGroups;

        public PdfOptionalContentIntent Intent =>
            this.intent;

        public IList<PdfOptionalContentUsageApplication> UsageApplication =>
            this.usageApplication;

        public PdfOptionalContentOrder Order =>
            this.order;

        public PdfOptionalContentOrderListMode OrderListMode =>
            this.orderListMode;

        public IList<PdfOptionalContentRadioButtonGroup> RadioButtonGroups =>
            this.radioButtonGroups;

        public IList<PdfOptionalContentGroup> Locked =>
            this.locked;
    }
}

