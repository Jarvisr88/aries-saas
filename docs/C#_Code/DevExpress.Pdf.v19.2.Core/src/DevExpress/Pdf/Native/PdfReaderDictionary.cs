namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.RegularExpressions;

    public class PdfReaderDictionary : PdfDictionary
    {
        private const string timeZonePattern = "OHH'mm'";
        private readonly PdfObjectCollection objects;
        private int number;
        private int generation;

        public PdfReaderDictionary(PdfObjectCollection objects, int number, int generation)
        {
            this.objects = objects;
            this.number = number;
            this.generation = generation;
        }

        public bool ContainsArrayNamedElement(string key, string name)
        {
            IList<object> array = this.GetArray(key);
            if (array != null)
            {
                using (IEnumerator<object> enumerator = array.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        object current = enumerator.Current;
                        PdfName name2 = current as PdfName;
                        if ((name2 != null) && (name2.Name == name))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static CultureInfo ConvertToCultureInfo(string language)
        {
            if (!string.IsNullOrEmpty(language))
            {
                try
                {
                    return new CultureInfo(language);
                }
                catch
                {
                }
            }
            return CultureInfo.InvariantCulture;
        }

        private static int ConvertToDigit(char chr)
        {
            if (!IsDigit(chr))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return (chr - '0');
        }

        private PdfObjectList<PdfCustomFunction> CreateFunctions(bool mustBeArray, object value)
        {
            PdfObjectList<PdfCustomFunction> list = new PdfObjectList<PdfCustomFunction>(this.Objects);
            IList<object> list2 = value as IList<object>;
            if (list2 == null)
            {
                if (mustBeArray)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                list.Add(PdfCustomFunction.Parse(this.objects, value));
            }
            else
            {
                foreach (object obj2 in list2)
                {
                    list.Add(PdfCustomFunction.Parse(this.objects, obj2));
                }
            }
            return list;
        }

        public PdfAction GetAction(string actionDictionary)
        {
            object obj2;
            return (base.TryGetValue(actionDictionary, out obj2) ? this.Objects.GetAction(obj2) : null);
        }

        public PdfResources GetActualResources(string key, PdfResources parentResources, bool shouldWriteParent, bool shouldBeWritten)
        {
            object obj2;
            return ((!base.TryGetValue(key, out obj2) || (obj2 == null)) ? null : this.objects.GetObject<PdfResources>(obj2, dictionary => new PdfResources(this.objects.DocumentCatalog, parentResources, shouldWriteParent, shouldBeWritten, dictionary)));
        }

        public PdfAdditionalActions GetAdditionalActions(PdfAnnotation parent)
        {
            object obj2;
            if (!base.TryGetValue("AA", out obj2))
            {
                return null;
            }
            Func<PdfReaderDictionary, PdfAdditionalActions> create = <>c.<>9__59_0;
            if (<>c.<>9__59_0 == null)
            {
                Func<PdfReaderDictionary, PdfAdditionalActions> local1 = <>c.<>9__59_0;
                create = <>c.<>9__59_0 = dictionary => new PdfAdditionalActions(dictionary);
            }
            return this.Objects.GetObject<PdfAdditionalActions>(obj2, create);
        }

        public PdfAnnotationHighlightingMode GetAnnotationHighlightingMode()
        {
            string name = this.GetName("H");
            return ((name != null) ? PdfEnumToStringConverter.Parse<PdfAnnotationHighlightingMode>(name, true) : PdfAnnotationHighlightingMode.Invert);
        }

        public PdfCommandList GetAppearance(PdfResources resources)
        {
            byte[] bytes = this.GetBytes("DA");
            if (bytes == null)
            {
                return null;
            }
            if (resources == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return PdfContentStreamParser.GetContent(resources, bytes);
        }

        public IList<object> GetArray(string key) => 
            this.GetArray(key, false);

        public IList<object> GetArray(string key, bool ignoreCollisions) => 
            this.Resolve<IList<object>>(key, ignoreCollisions);

        public IList<T> GetArray<T>(string key, Func<object, T> create) => 
            this.GetArray<T>(key, false, create);

        public IList<T> GetArray<T>(string key, bool ignoreCollisions, Func<object, T> create)
        {
            IList<object> array = this.GetArray(key, ignoreCollisions);
            if (array == null)
            {
                return null;
            }
            List<T> list2 = new List<T>(array.Count);
            foreach (object obj2 in array)
            {
                list2.Add(create(obj2));
            }
            return list2;
        }

        public bool? GetBoolean(string key) => 
            this.Resolve<bool?>(key);

        public bool? GetBoolean(string key, bool ignoreCollisions) => 
            this.Resolve<bool?>(key, ignoreCollisions);

        public byte[] GetBytes(string key) => 
            this.GetBytes(key, false);

        public byte[] GetBytes(string key, bool ignoreCollisions) => 
            this.Resolve<byte[]>(key, ignoreCollisions);

        public PdfColorSpace GetColorSpace(string key)
        {
            object obj2;
            return (!base.TryGetValue(key, out obj2) ? null : ((obj2 == null) ? null : this.Objects.GetColorSpace(obj2)));
        }

        public DateTimeOffset? GetDate(string key)
        {
            DateTimeOffset offset;
            DateTimeOffset? nullable;
            string str = this.GetString(key);
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            CultureInfo invariantCulture = CultureInfo.InvariantCulture;
            if (DateTimeOffset.TryParse(str, invariantCulture, DateTimeStyles.AllowWhiteSpaces, out offset))
            {
                return new DateTimeOffset?(offset);
            }
            string[] strArray = Regex.Split(str, "[0-9][0-9][:][0-9][0-9][:][0-9][0-9]", RegexOptions.CultureInvariant);
            if (strArray.Length == 2)
            {
                if (!string.IsNullOrEmpty(strArray[1]))
                {
                    string str4 = strArray[0];
                    if (str.StartsWith(str4))
                    {
                        string str5 = str.Remove(0, str4.Length);
                        string str6 = strArray[1];
                        if (str.EndsWith(str6))
                        {
                            str5 = str5.Remove(str5.Length - str6.Length);
                            if (DateTimeOffset.TryParse(string.Concat(strArray) + " " + str5, invariantCulture, DateTimeStyles.AllowWhiteSpaces, out offset))
                            {
                                return new DateTimeOffset?(offset);
                            }
                        }
                    }
                }
                else
                {
                    string[] strArray2 = Regex.Split(strArray[0], @"[.\/-]", RegexOptions.CultureInvariant);
                    if (strArray2.Length == 3)
                    {
                        int num2;
                        string str2 = str.Remove(0, strArray[0].Length);
                        string s = strArray2[0];
                        if (int.TryParse(s, NumberStyles.Integer, invariantCulture, out num2) && (num2 > 12))
                        {
                            strArray2[0] = strArray2[1];
                            strArray2[1] = s;
                            if (DateTimeOffset.TryParse(string.Join("/", strArray2) + str2, invariantCulture, DateTimeStyles.AllowWhiteSpaces, out offset))
                            {
                                return new DateTimeOffset?(offset);
                            }
                        }
                    }
                }
            }
            int length = str.Length;
            if (((length > 2) && (str[0] == 'D')) && (str[1] == ':'))
            {
                str = str.Substring(2);
                length -= 2;
            }
            try
            {
                if (length == 6)
                {
                    nullable = new DateTimeOffset?(ParseDate(str.Insert(4, "0").Insert(6, "0")));
                }
                else if (length == 7)
                {
                    nullable = new DateTimeOffset?(ParseDate(str.Insert(4, "0")));
                }
                else
                {
                    if (length == 0x15)
                    {
                        char c = str[0x11];
                        if ((c == '+') || (c == '-'))
                        {
                            return new DateTimeOffset?(ParseDate(str.Remove(0x11, 1).Insert(14, new string(c, 1))));
                        }
                    }
                    nullable = new DateTimeOffset?(ParseDate(str));
                }
                return nullable;
            }
            catch
            {
            }
            try
            {
                nullable = new DateTimeOffset?(ParseDate(str.Insert(6, "0")));
            }
            catch
            {
                return null;
            }
            return nullable;
        }

        private static int GetDateComponent(string str, int offset) => 
            (ConvertToDigit(str[offset]) * 10) + ConvertToDigit(str[offset + 1]);

        public PdfInteractiveFormFieldCollection GetDeferredFormFieldCollection(string key)
        {
            object obj2;
            if (!base.TryGetValue(key, out obj2) || (obj2 == null))
            {
                return null;
            }
            PdfObjectReference reference = obj2 as PdfObjectReference;
            return ((reference == null) ? null : (this.objects.GetDeferredObject(reference.Number) as PdfInteractiveFormFieldCollection));
        }

        public PdfDestinationObject GetDestination(string key)
        {
            object obj2;
            if (!base.TryGetValue(key, out obj2))
            {
                return null;
            }
            object obj3 = this.objects.TryResolve(obj2, null);
            byte[] bytes = obj3 as byte[];
            if (bytes != null)
            {
                return new PdfDestinationObject(new PdfNameTreeEncoding().GetString(bytes, 0, bytes.Length));
            }
            PdfName name = obj3 as PdfName;
            return ((name == null) ? new PdfDestinationObject(this.objects.GetDestination(obj2)) : new PdfDestinationObject(name.Name));
        }

        public PdfReaderDictionary GetDictionary(string key) => 
            this.GetDictionary(key, null);

        public PdfReaderDictionary GetDictionary(string key, string nonEncryptedKey) => 
            this.GetDictionary(key, nonEncryptedKey, false);

        public PdfReaderDictionary GetDictionary(string key, string nonEncryptedKey, bool ignoreCollisions)
        {
            object obj2;
            if (!base.TryGetValue(key, out obj2) || (obj2 == null))
            {
                return null;
            }
            obj2 = this.objects.TryResolve(obj2, nonEncryptedKey);
            PdfReaderDictionary dictionary = obj2 as PdfReaderDictionary;
            if (!ignoreCollisions && ((dictionary == null) && (obj2 != null)))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return dictionary;
        }

        public IList<double> GetDoubleArray(string key)
        {
            IList<object> array = this.GetArray(key);
            if (array == null)
            {
                return null;
            }
            List<double> list2 = new List<double>();
            foreach (object obj2 in array)
            {
                list2.Add(PdfDocumentReader.ConvertToDouble(obj2));
            }
            return list2;
        }

        public T GetEnumName<T>(string key) where T: struct => 
            PdfEnumToStringConverter.Parse<T>(this.GetName(key), true);

        public IList<PdfFilter> GetFilters(string key, string decodeParamsKey)
        {
            object obj2 = this.Resolve(key);
            if (obj2 == null)
            {
                return null;
            }
            List<PdfFilter> list = new List<PdfFilter>();
            IList<object> list2 = obj2 as IList<object>;
            if (list2 != null)
            {
                IList<object> array = this.GetArray(decodeParamsKey);
                for (int i = 0; i < list2.Count; i++)
                {
                    PdfName name = this.objects.TryResolve(list2[i], null) as PdfName;
                    if (name != null)
                    {
                        PdfReaderDictionary parameters = null;
                        if ((array != null) && (i < array.Count))
                        {
                            parameters = this.objects.TryResolve(array[i], null) as PdfReaderDictionary;
                        }
                        list.Add(PdfFilter.Create(name.Name, parameters));
                    }
                }
            }
            else
            {
                PdfReaderDictionary dictionary2;
                PdfName name2 = obj2 as PdfName;
                if (name2 == null)
                {
                    return null;
                }
                obj2 = this.Resolve(decodeParamsKey);
                if (obj2 == null)
                {
                    dictionary2 = null;
                }
                else
                {
                    dictionary2 = obj2 as PdfReaderDictionary;
                    if (dictionary2 == null)
                    {
                        IList<object> list4 = obj2 as IList<object>;
                        if ((list4 != null) && (list4.Count > 0))
                        {
                            dictionary2 = this.objects.TryResolve(list4[0], null) as PdfReaderDictionary;
                        }
                    }
                }
                list.Add(PdfFilter.Create(name2.Name, dictionary2));
            }
            return list;
        }

        public PdfObjectList<PdfCustomFunction> GetFunctions(string functionDictionaryKey, bool mustBeArray)
        {
            object obj2;
            if (!base.TryGetValue(functionDictionaryKey, out obj2))
            {
                return null;
            }
            obj2 = this.objects.TryResolve(obj2, null);
            return this.CreateFunctions(mustBeArray, obj2);
        }

        public PdfGraphicsStateParameters GetGraphicsStateParameters(string key)
        {
            object obj2 = this.Resolve(key);
            Func<PdfReaderDictionary, PdfGraphicsStateParameters> create = <>c.<>9__53_0;
            if (<>c.<>9__53_0 == null)
            {
                Func<PdfReaderDictionary, PdfGraphicsStateParameters> local1 = <>c.<>9__53_0;
                create = <>c.<>9__53_0 = parametersDictionary => new PdfGraphicsStateParameters(parametersDictionary);
            }
            return this.objects.GetObject<PdfGraphicsStateParameters>(obj2, create);
        }

        public PdfHalftone GetHalftone(string key)
        {
            object obj2;
            return (base.TryGetValue(key, out obj2) ? this.objects.GetObject<PdfHalftone>(obj2, new Func<object, PdfHalftone>(PdfHalftone.Parse)) : null);
        }

        public int? GetInteger(string key)
        {
            object obj2;
            if (base.TryGetValue(key, out obj2))
            {
                PdfObjectReference reference = obj2 as PdfObjectReference;
                if ((reference != null) && (reference.Number == this.Number))
                {
                    return null;
                }
                if (this.objects != null)
                {
                    obj2 = this.objects.TryResolve(obj2, null);
                }
                if (obj2 != null)
                {
                    return new int?(PdfDocumentReader.ConvertToInteger(obj2));
                }
            }
            return null;
        }

        public PdfJavaScriptAction GetJavaScriptAction(string key)
        {
            PdfAction action = this.GetAction(key);
            if (action == null)
            {
                return null;
            }
            PdfJavaScriptAction action2 = action as PdfJavaScriptAction;
            if (action2 == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return action2;
        }

        public CultureInfo GetLanguageCulture()
        {
            object obj2;
            if (base.TryGetValue("Lang", out obj2))
            {
                byte[] buffer = this.Objects.TryResolve(obj2, null) as byte[];
                if (buffer != null)
                {
                    return ConvertToCultureInfo(PdfDocumentReader.ConvertToString(buffer));
                }
            }
            return CultureInfo.InvariantCulture;
        }

        public PdfMetadata GetMetadata()
        {
            object obj2;
            if (base.TryGetValue("Metadata", out obj2))
            {
                PdfReaderStream stream = this.objects.TryResolve(obj2, null) as PdfReaderStream;
                if (stream != null)
                {
                    try
                    {
                        return new PdfMetadata(stream);
                    }
                    catch
                    {
                    }
                }
            }
            return null;
        }

        public string GetName(string key)
        {
            PdfName name = this.Resolve<PdfName>(key, true);
            return ((name != null) ? name.Name : this.GetString(key));
        }

        public double? GetNumber(string key)
        {
            object obj2;
            if (base.TryGetValue(key, out obj2))
            {
                if (this.objects != null)
                {
                    obj2 = this.objects.TryResolve(obj2, null);
                }
                if (obj2 is int)
                {
                    return new double?((double) ((int) obj2));
                }
                if (obj2 is double)
                {
                    return new double?((double) obj2);
                }
            }
            return null;
        }

        public T GetObject<T>(string key, Func<PdfReaderDictionary, T> create) where T: PdfObject
        {
            object obj2;
            if (base.TryGetValue(key, out obj2))
            {
                return this.objects.GetObject<T>(obj2, create);
            }
            return default(T);
        }

        public T GetObject<T>(string key, Func<PdfReaderStream, T> create) where T: PdfObject
        {
            object obj2;
            if (base.TryGetValue(key, out obj2))
            {
                return this.objects.GetObject<T>(obj2, delegate (object o) {
                    PdfReaderStream arg = o as PdfReaderStream;
                    if (arg == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    return create(arg);
                });
            }
            return default(T);
        }

        public PdfObjectReference GetObjectReference(string key)
        {
            object obj2;
            if (!base.TryGetValue(key, out obj2) || (obj2 == null))
            {
                return null;
            }
            PdfObjectReference reference = obj2 as PdfObjectReference;
            if (reference == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return reference;
        }

        public PdfOptionalContent GetOptionalContent()
        {
            object obj2;
            return (base.TryGetValue("OC", out obj2) ? this.objects.GetOptionalContent(obj2) : null);
        }

        public PdfOptionalContentIntent GetOptionalContentIntent(string key)
        {
            object obj2;
            if (!base.TryGetValue(key, out obj2))
            {
                return PdfOptionalContentIntent.View;
            }
            obj2 = this.objects.TryResolve(obj2, null);
            PdfName name = obj2 as PdfName;
            if (name != null)
            {
                return PdfEnumToStringConverter.Parse<PdfOptionalContentIntent>(name.Name, true);
            }
            IList<object> list = obj2 as IList<object>;
            if (list == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            PdfOptionalContentIntent intent = 0;
            foreach (object obj3 in list)
            {
                name = obj3 as PdfName;
                if (name == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                intent |= (PdfOptionalContentIntent) PdfEnumToStringConverter.Parse<PdfOptionalContentIntent>(name.Name, true);
            }
            if (intent == 0)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return intent;
        }

        public PdfRectangle GetPadding(PdfRectangle bounds)
        {
            PdfRectangle rectangle = this.GetRectangle("RD");
            if (rectangle == null)
            {
                return null;
            }
            double left = Math.Max(0.0, rectangle.Left);
            double right = Math.Max(0.0, rectangle.Right);
            double num3 = left + right;
            double num4 = num3 - bounds.Width;
            if (num4 > 0.0)
            {
                double num9 = num4 / num3;
                left -= left * num9;
                right -= right * num9;
            }
            double top = Math.Max(0.0, rectangle.Top);
            double bottom = Math.Max(0.0, rectangle.Bottom);
            double num7 = top + bottom;
            double num8 = num7 - bounds.Height;
            if (num8 > 0.0)
            {
                double num10 = num8 / num7;
                top -= top * num10;
                bottom -= bottom * num10;
            }
            return new PdfRectangle(left, bottom, right, top);
        }

        public IList<PdfRange> GetRanges(string key)
        {
            IList<double> doubleArray = this.GetDoubleArray(key);
            if (doubleArray == null)
            {
                return null;
            }
            int count = doubleArray.Count;
            if ((count % 2) > 0)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            List<PdfRange> list2 = new List<PdfRange>();
            int num2 = 0;
            while (num2 < count)
            {
                double min = doubleArray[num2++];
                double max = doubleArray[num2++];
                list2.Add(new PdfRange(min, max));
            }
            return list2;
        }

        public PdfRectangle GetRectangle(string key)
        {
            IList<object> array = this.GetArray(key);
            return ((array == null) ? null : PdfRectangle.Parse(array, this.objects));
        }

        public PdfResources GetResources(string key, PdfResources parentResources, bool shouldWriteParent, bool shouldBeWritten) => 
            this.GetActualResources(key, parentResources, shouldWriteParent, shouldBeWritten) ?? new PdfResources(this.objects.DocumentCatalog, parentResources, shouldWriteParent, shouldBeWritten, null);

        public PdfRichMediaContentType? GetRichMediaContentType()
        {
            string name = this.GetName("Subtype");
            if (name != null)
            {
                return new PdfRichMediaContentType?(PdfEnumToStringConverter.Parse<PdfRichMediaContentType>(name, true));
            }
            return null;
        }

        public PdfReaderStream GetStream(string key) => 
            this.Resolve<PdfReaderStream>(key, true);

        public PdfReaderDictionary GetStreamDictionary(string key)
        {
            object obj2;
            if (!base.TryGetValue(key, out obj2) || (obj2 == null))
            {
                return null;
            }
            PdfReaderStream stream = obj2 as PdfReaderStream;
            if (stream == null)
            {
                object obj3 = this.objects.TryResolve(obj2, null);
                PdfReaderDictionary dictionary = obj3 as PdfReaderDictionary;
                if (dictionary != null)
                {
                    return dictionary;
                }
                stream = obj3 as PdfReaderStream;
                if (stream == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            return stream.Dictionary;
        }

        public string GetString(string key)
        {
            byte[] bytes = this.GetBytes(key, true);
            return ((bytes == null) ? null : PdfDocumentReader.ConvertToString(bytes));
        }

        public string GetStringAdvanced(string key)
        {
            object obj2;
            if (!base.TryGetValue(key, out obj2))
            {
                return null;
            }
            obj2 = this.objects.TryResolve(obj2, null);
            byte[] buffer = obj2 as byte[];
            if (buffer != null)
            {
                return PdfDocumentReader.ConvertToTextString(buffer);
            }
            PdfReaderStream stream = obj2 as PdfReaderStream;
            if (stream == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            byte[] uncompressedData = stream.UncompressedData;
            return Encoding.UTF8.GetString(uncompressedData, 0, uncompressedData.Length);
        }

        public PdfTextJustification GetTextJustification() => 
            PdfEnumToValueConverter.Parse<PdfTextJustification>(this.GetInteger("Q"), 0);

        public string GetTextString(string key)
        {
            byte[] bytes = this.GetBytes(key);
            return ((bytes == null) ? null : PdfDocumentReader.ConvertToTextString(bytes));
        }

        private static int GetTimeComponent(string str, int offset, char delimiter)
        {
            int num = ConvertToDigit(str[offset]);
            char chr = str[offset + 1];
            return ((chr == delimiter) ? num : ((num * 10) + ConvertToDigit(chr)));
        }

        private static bool IsDigit(char chr) => 
            (chr >= '0') && (chr <= '9');

        private static DateTimeOffset ParseDate(string str)
        {
            bool flag;
            char ch;
            int length = str.Length;
            if ((length != 8) && ((length != 12) && (length < 14)))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int year = (((ConvertToDigit(str[0]) * 0x3e8) + (ConvertToDigit(str[1]) * 100)) + (ConvertToDigit(str[2]) * 10)) + ConvertToDigit(str[3]);
            int dateComponent = GetDateComponent(str, 4);
            int day = GetDateComponent(str, 6);
            int hour = 0;
            int minute = 0;
            int second = 0;
            int hours = 0;
            int minutes = 0;
            if (length > 8)
            {
                hour = GetTimeComponent(str, 8, ':');
                minute = GetTimeComponent(str, 10, ':');
                if (length >= 14)
                {
                    second = GetTimeComponent(str, 12, ' ');
                }
                flag = false;
                if ((length > 14) && ((length != 20) || ((str[0x10] != '\'') || (str[0x13] != '\''))))
                {
                    ch = str[14];
                    if (ch > '+')
                    {
                        if (ch == '-')
                        {
                            flag = true;
                            goto TR_001F;
                        }
                        else if (ch != 'Z')
                        {
                            goto TR_0009;
                        }
                    }
                    else if (ch != ' ')
                    {
                        if (ch != '+')
                        {
                            goto TR_0009;
                        }
                        goto TR_001F;
                    }
                    if (length != 15)
                    {
                        if (length == 0x15)
                        {
                            if ((str[15] != '0') || ((str[0x10] != '0') || ((str[0x11] != '\'') && ((str[0x12] != '0') && ((str[0x13] != '0') && (str[20] != '\''))))))
                            {
                                PdfDocumentStructureReader.ThrowIncorrectDataException();
                            }
                        }
                        else
                        {
                            if (length == 0x16)
                            {
                                return ParseDate(str.Remove(14, 1));
                            }
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                    }
                    goto TR_0005;
                }
            }
        TR_0001:
            return new DateTimeOffset(year, dateComponent, day, hour, minute, second, new TimeSpan(hours, minutes, 0));
        TR_0005:
            if ((hours > 14) || ((hours == 14) && (minutes > 0)))
            {
                hours = 0;
                minutes = 0;
            }
            if (flag)
            {
                hours = -hours;
                minutes = -minutes;
            }
            goto TR_0001;
        TR_0009:
            if (str.EndsWith("OHH'mm'"))
            {
                return ParseDate(str.Substring(0, length - "OHH'mm'".Length));
            }
            if ((length != 15) || !IsDigit(str[14]))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            year = (((((ConvertToDigit(str[0]) * 10) + ConvertToDigit(str[1])) + ConvertToDigit(str[2])) * 100) + (ConvertToDigit(str[3]) * 10)) + ConvertToDigit(str[4]);
            dateComponent = GetDateComponent(str, 5);
            day = GetDateComponent(str, 7);
            hour = GetTimeComponent(str, 9, ':');
            minute = GetTimeComponent(str, 11, ':');
            second = GetTimeComponent(str, 13, ' ');
            goto TR_0005;
        TR_000E:
            if (str[0x11] != '\'')
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            hours = ConvertToDigit(str[0x10]);
            char chr = str[15];
            if ((chr != '+') && (chr != '-'))
            {
                hours += ConvertToDigit(chr) * 10;
            }
            goto TR_0005;
        TR_0019:
            minutes = (ConvertToDigit(str[0x12]) * 10) + ConvertToDigit(str[0x13]);
            goto TR_000E;
        TR_001B:
            if (str[20] != '\'')
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            goto TR_0019;
        TR_001F:
            switch (length)
            {
                case 0x12:
                    goto TR_000E;

                case 20:
                    break;

                case 0x15:
                    goto TR_001B;

                case 0x16:
                    if (str[15] != '-')
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    str = str.Remove(15, 1);
                    goto TR_001B;

                default:
                    if ((length <= 0x15) || ((str[0x12] != '0') || (str[length - 1] != '\'')))
                    {
                        goto TR_0005;
                    }
                    else
                    {
                        bool flag2 = false;
                        ch = str[0x13];
                        if (ch != '+')
                        {
                            if (ch == '-')
                            {
                                flag2 = true;
                            }
                            else
                            {
                                PdfDocumentStructureReader.ThrowIncorrectDataException();
                            }
                        }
                        if (!int.TryParse(str.Substring(20, length - 0x15), out minutes))
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        if (flag2)
                        {
                            minutes = -minutes;
                        }
                    }
                    goto TR_000E;
            }
            goto TR_0019;
        }

        private object Resolve(string key)
        {
            object obj2;
            return ((!base.TryGetValue(key, out obj2) || (obj2 == null)) ? null : ((this.objects == null) ? obj2 : this.objects.TryResolve(obj2, null)));
        }

        private T Resolve<T>(string key) => 
            this.Resolve<T>(key, false);

        private T Resolve<T>(string key, bool ignoreCollisions)
        {
            object obj2;
            if (base.TryGetValue(key, out obj2) && (obj2 != null))
            {
                obj2 = (this.objects != null) ? this.objects.TryResolve(obj2, null) : obj2;
                if ((obj2 is T) || (obj2 == null))
                {
                    return (T) obj2;
                }
                if (!ignoreCollisions)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            return default(T);
        }

        public PdfObjectCollection Objects =>
            this.objects;

        public int Number =>
            this.number;

        public int Generation =>
            this.generation;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfReaderDictionary.<>c <>9 = new PdfReaderDictionary.<>c();
            public static Func<PdfReaderDictionary, PdfGraphicsStateParameters> <>9__53_0;
            public static Func<PdfReaderDictionary, PdfAdditionalActions> <>9__59_0;

            internal PdfAdditionalActions <GetAdditionalActions>b__59_0(PdfReaderDictionary dictionary) => 
                new PdfAdditionalActions(dictionary);

            internal PdfGraphicsStateParameters <GetGraphicsStateParameters>b__53_0(PdfReaderDictionary parametersDictionary) => 
                new PdfGraphicsStateParameters(parametersDictionary);
        }
    }
}

