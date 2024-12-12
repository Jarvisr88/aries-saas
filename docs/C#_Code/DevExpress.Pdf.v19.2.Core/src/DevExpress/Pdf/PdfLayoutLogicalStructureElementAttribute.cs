namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfLayoutLogicalStructureElementAttribute : PdfLogicalStructureElementAttribute
    {
        internal const string Owner = "Layout";
        private const string placementKey = "Placement";
        private const string writingModeKey = "WritingMode";
        private const string backgroundColorKey = "BackgroundColor";
        private const string borderColorKey = "BorderColor";
        private const string borderStyleKey = "BorderStyle";
        private const string borderThicknessKey = "BorderThickness";
        private const string paddingKey = "Padding";
        private const string colorKey = "Color";
        private readonly PdfLayoutLogicalStructureElementAttributePlacement placement;
        private readonly PdfLayoutLogicalStructureElementAttributeWritingMode writingMode;
        private readonly PdfColor backgroundColor;
        private readonly PdfColor borderColorBefore;
        private readonly PdfColor borderColorAfter;
        private readonly PdfColor borderColorStart;
        private readonly PdfColor borderColorEnd;
        private readonly PdfLayoutLogicalStructureElementAttributeBorderStyle borderStyleBefore;
        private readonly PdfLayoutLogicalStructureElementAttributeBorderStyle borderStyleAfter;
        private readonly PdfLayoutLogicalStructureElementAttributeBorderStyle borderStyleStart;
        private readonly PdfLayoutLogicalStructureElementAttributeBorderStyle borderStyleEnd;
        private readonly double borderThicknessBefore;
        private readonly double borderThicknessAfter;
        private readonly double borderThicknessStart;
        private readonly double borderThicknessEnd;
        private readonly double paddingBefore;
        private readonly double paddingAfter;
        private readonly double paddingStart;
        private readonly double paddingEnd;
        private readonly PdfColor colorText;

        protected PdfLayoutLogicalStructureElementAttribute(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.placement = PdfEnumToStringConverter.Parse<PdfLayoutLogicalStructureElementAttributePlacement>(dictionary.GetName("Placement"), true);
            this.writingMode = PdfEnumToStringConverter.Parse<PdfLayoutLogicalStructureElementAttributeWritingMode>(dictionary.GetName("WritingMode"), true);
            this.backgroundColor = this.ConvertToColor(dictionary.GetDoubleArray("BackgroundColor"));
            IList<PdfColor> edgeColors = this.GetEdgeColors(dictionary, "BorderColor");
            if (edgeColors != null)
            {
                this.borderColorBefore = edgeColors[0];
                this.borderColorAfter = edgeColors[1];
                this.borderColorStart = edgeColors[2];
                this.borderColorEnd = edgeColors[3];
            }
            IList<PdfLayoutLogicalStructureElementAttributeBorderStyle> list2 = this.GetEdgeOptions<PdfLayoutLogicalStructureElementAttributeBorderStyle>(dictionary, "BorderStyle", o => PdfEnumToStringConverter.Parse<PdfLayoutLogicalStructureElementAttributeBorderStyle>(this.ConvertToString(o), true));
            if (list2 != null)
            {
                this.borderStyleBefore = list2[0];
                this.borderStyleAfter = list2[1];
                this.borderStyleStart = list2[2];
                this.borderStyleEnd = list2[3];
            }
            IList<double> list3 = this.GetEdgeOptions<double>(dictionary, "BorderThickness", new Func<object, double>(this.ConvertToDouble));
            if (list3 != null)
            {
                this.borderThicknessBefore = list3[0];
                this.borderThicknessAfter = list3[1];
                this.borderThicknessStart = list3[2];
                this.borderThicknessEnd = list3[3];
            }
            list3 = this.GetEdgeOptions<double>(dictionary, "Padding", new Func<object, double>(this.ConvertToDouble));
            if (list3 != null)
            {
                this.paddingBefore = list3[0];
                this.paddingAfter = list3[1];
                this.paddingStart = list3[2];
                this.paddingEnd = list3[3];
            }
            this.colorText = this.ConvertToColor(dictionary.GetDoubleArray("Color"));
        }

        protected PdfColor ConvertToColor(IList<double> components)
        {
            if (components == null)
            {
                return null;
            }
            if (components.Count != 3)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            foreach (double num in components)
            {
                if ((num < 0.0) || (num > 1.0))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            double[] numArray1 = new double[] { components[0], components[1], components[2] };
            return new PdfColor(numArray1);
        }

        private PdfColor ConvertToColor(IList<object> components)
        {
            IList<double> list = null;
            if (components != null)
            {
                list = new List<double>();
                foreach (object obj2 in components)
                {
                    list.Add(PdfDocumentReader.ConvertToDouble(obj2));
                }
            }
            return this.ConvertToColor(list);
        }

        private double ConvertToDouble(object value)
        {
            double num = PdfDocumentReader.ConvertToDouble(value);
            if (num < 0.0)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return num;
        }

        private string ConvertToString(object value)
        {
            PdfName name = value as PdfName;
            if (name != null)
            {
                return name.Name;
            }
            byte[] buffer = value as byte[];
            if (buffer == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return PdfDocumentReader.ConvertToString(buffer);
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            dictionary.AddName("O", "Layout");
            dictionary.AddEnumName<PdfLayoutLogicalStructureElementAttributePlacement>("Placement", this.placement);
            dictionary.AddEnumName<PdfLayoutLogicalStructureElementAttributeWritingMode>("WritingMode", this.writingMode);
            dictionary.Add("BackgroundColor", this.backgroundColor);
            Func<PdfColor, object> prepareFunction = <>c.<>9__71_0;
            if (<>c.<>9__71_0 == null)
            {
                Func<PdfColor, object> local1 = <>c.<>9__71_0;
                prepareFunction = <>c.<>9__71_0 = o => o.ToWritableObject();
            }
            dictionary.AddIfPresent("BorderColor", this.WriteEdgeParams<PdfColor>(this.borderColorBefore, this.borderColorAfter, this.borderColorStart, this.borderColorEnd, prepareFunction, null));
            Func<PdfLayoutLogicalStructureElementAttributeBorderStyle, object> func2 = <>c.<>9__71_1;
            if (<>c.<>9__71_1 == null)
            {
                Func<PdfLayoutLogicalStructureElementAttributeBorderStyle, object> local2 = <>c.<>9__71_1;
                func2 = <>c.<>9__71_1 = o => new PdfName(PdfEnumToStringConverter.Convert<PdfLayoutLogicalStructureElementAttributeBorderStyle>(o, false));
            }
            object obj2 = this.WriteEdgeParams<PdfLayoutLogicalStructureElementAttributeBorderStyle>(this.borderStyleBefore, this.borderStyleAfter, this.borderStyleStart, this.borderStyleEnd, func2, PdfLayoutLogicalStructureElementAttributeBorderStyle.None);
            dictionary.AddIfPresent("BorderStyle", obj2);
            Func<double, object> func3 = <>c.<>9__71_2;
            if (<>c.<>9__71_2 == null)
            {
                Func<double, object> local3 = <>c.<>9__71_2;
                func3 = <>c.<>9__71_2 = o => o;
            }
            dictionary.AddIfPresent("BorderThickness", this.WriteEdgeParams<double>(this.borderThicknessBefore, this.borderThicknessAfter, this.borderThicknessStart, this.borderThicknessEnd, func3, 0.0));
            Func<double, object> func4 = <>c.<>9__71_3;
            if (<>c.<>9__71_3 == null)
            {
                Func<double, object> local4 = <>c.<>9__71_3;
                func4 = <>c.<>9__71_3 = o => o;
            }
            dictionary.AddIfPresent("Padding", this.WriteEdgeParams<double>(this.paddingBefore, this.paddingAfter, this.paddingStart, this.paddingEnd, func4, 0.0));
            dictionary.Add("Color", this.colorText);
            return dictionary;
        }

        private IList<PdfColor> GetEdgeColors(PdfReaderDictionary dictionary, string key) => 
            this.GetEdgeOptions<PdfColor>(dictionary, key, delegate (object o) {
                if (o == null)
                {
                    return null;
                }
                IList<object> components = o as IList<object>;
                if (components == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                return this.ConvertToColor(components);
            });

        private IList<T> GetEdgeOptions<T>(PdfReaderDictionary dictionary, string key, Func<object, T> convertFunction)
        {
            object obj2;
            if (!dictionary.TryGetValue(key, out obj2))
            {
                return null;
            }
            IList<object> list = obj2 as IList<object>;
            if ((list == null) || (list.Count != 4))
            {
                List<object> list1 = new List<object>();
                list1.Add(obj2);
                list1.Add(obj2);
                list1.Add(obj2);
                list1.Add(obj2);
                list = list1;
            }
            List<T> list2 = new List<T>(4);
            foreach (object obj3 in list)
            {
                list2.Add(convertFunction(dictionary.Objects.TryResolve(obj3, null)));
            }
            return list2;
        }

        internal static PdfLayoutLogicalStructureElementAttribute ParseAttribute(PdfReaderDictionary dictionary)
        {
            PdfLayoutLogicalStructureElementAttribute attribute;
            using (Dictionary<string, object>.Enumerator enumerator = dictionary.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        KeyValuePair<string, object> current = enumerator.Current;
                        if (Array.IndexOf<string>(PdfBLSELayoutLogicalStructureElementAttribute.Keys, current.Key) > -1)
                        {
                            attribute = new PdfBLSELayoutLogicalStructureElementAttribute(dictionary);
                        }
                        else if (Array.IndexOf<string>(PdfILSELayoutLogicalStructureElementAttribute.Keys, current.Key) > -1)
                        {
                            attribute = new PdfILSELayoutLogicalStructureElementAttribute(dictionary);
                        }
                        else
                        {
                            if (Array.IndexOf<string>(PdfColumnLayoutLogicalStructureElementAttribute.Keys, current.Key) <= -1)
                            {
                                continue;
                            }
                            attribute = new PdfColumnLayoutLogicalStructureElementAttribute(dictionary);
                        }
                    }
                    else
                    {
                        return new PdfLayoutLogicalStructureElementAttribute(dictionary);
                    }
                    break;
                }
            }
            return attribute;
        }

        private object WriteEdgeParams<T>(T before, T after, T start, T end, Func<T, object> prepareFunction, T defaultValue)
        {
            if ((before == null) && ((after == null) && ((start == null) && (end == null))))
            {
                return null;
            }
            Func<T, object> func = o => (o == null) ? null : prepareFunction(o);
            if ((before != null) && (before.Equals(after) && (after.Equals(start) && start.Equals(end))))
            {
                return (before.Equals(defaultValue) ? null : func(before));
            }
            return new object[] { func(before), func(after), func(start), func(end) };
        }

        public PdfLayoutLogicalStructureElementAttributePlacement Placement =>
            this.placement;

        public PdfLayoutLogicalStructureElementAttributeWritingMode WritingMode =>
            this.writingMode;

        public PdfColor BackgroundColor =>
            this.backgroundColor;

        public PdfColor BorderColorBefore =>
            this.borderColorBefore;

        public PdfColor BorderColorAfter =>
            this.borderColorAfter;

        public PdfColor BorderColorStart =>
            this.borderColorStart;

        public PdfColor BorderColorEnd =>
            this.borderColorEnd;

        public PdfLayoutLogicalStructureElementAttributeBorderStyle BorderStyleBefore =>
            this.borderStyleBefore;

        public PdfLayoutLogicalStructureElementAttributeBorderStyle BorderStyleAfter =>
            this.borderStyleAfter;

        public PdfLayoutLogicalStructureElementAttributeBorderStyle BorderStyleStart =>
            this.borderStyleStart;

        public PdfLayoutLogicalStructureElementAttributeBorderStyle BorderStyleEnd =>
            this.borderStyleEnd;

        public double BorderThicknessBefore =>
            this.borderThicknessBefore;

        public double BorderThicknessAfter =>
            this.borderThicknessAfter;

        public double BorderThicknessStart =>
            this.borderThicknessStart;

        public double BorderThicknessEnd =>
            this.borderThicknessEnd;

        public double PaddingBefore =>
            this.paddingBefore;

        public double PaddingAfter =>
            this.paddingAfter;

        public double PaddingStart =>
            this.paddingStart;

        public double PaddingEnd =>
            this.paddingEnd;

        public PdfColor ColorText =>
            this.colorText;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfLayoutLogicalStructureElementAttribute.<>c <>9 = new PdfLayoutLogicalStructureElementAttribute.<>c();
            public static Func<PdfColor, object> <>9__71_0;
            public static Func<PdfLayoutLogicalStructureElementAttributeBorderStyle, object> <>9__71_1;
            public static Func<double, object> <>9__71_2;
            public static Func<double, object> <>9__71_3;

            internal object <CreateDictionary>b__71_0(PdfColor o) => 
                o.ToWritableObject();

            internal object <CreateDictionary>b__71_1(PdfLayoutLogicalStructureElementAttributeBorderStyle o) => 
                new PdfName(PdfEnumToStringConverter.Convert<PdfLayoutLogicalStructureElementAttributeBorderStyle>(o, false));

            internal object <CreateDictionary>b__71_2(double o) => 
                o;

            internal object <CreateDictionary>b__71_3(double o) => 
                o;
        }
    }
}

