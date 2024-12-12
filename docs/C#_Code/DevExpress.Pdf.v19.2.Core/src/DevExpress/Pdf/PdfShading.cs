namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public abstract class PdfShading : PdfObject
    {
        private const string shadingTypeDictionaryKey = "ShadingType";
        private const string colorSpaceDictionaryKey = "ColorSpace";
        private const string backgroundDictionaryKey = "Background";
        private const string boundingBoxDictionaryKey = "BBox";
        private const string antiAliasDictionaryKey = "AntiAlias";
        private const string functionDictionaryKey = "Function";
        private readonly PdfColorSpace colorSpace;
        private readonly PdfColor background;
        private readonly PdfRectangle boundingBox;
        private readonly bool antiAlias;
        private readonly PdfObjectList<PdfCustomFunction> function;

        protected PdfShading(PdfObjectList<PdfCustomFunction> blendFunctions)
        {
            this.function = blendFunctions;
            this.colorSpace = new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.RGB);
        }

        protected PdfShading(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.colorSpace = dictionary.GetColorSpace("ColorSpace");
            if (this.colorSpace is PdfPatternColorSpace)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            IList<object> array = dictionary.GetArray("Background");
            if (array != null)
            {
                int count = array.Count;
                if (count != this.colorSpace.ComponentsCount)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                double[] components = new double[count];
                int index = 0;
                while (true)
                {
                    if (index >= count)
                    {
                        this.background = new PdfColor(components);
                        break;
                    }
                    components[index] = PdfDocumentReader.ConvertToDouble(array[index]);
                    index++;
                }
            }
            this.boundingBox = dictionary.GetRectangle("BBox");
            bool? boolean = dictionary.GetBoolean("AntiAlias");
            this.antiAlias = (boolean != null) ? boolean.GetValueOrDefault() : false;
            this.function = dictionary.GetFunctions("Function", false);
            if (this.function == null)
            {
                if (this.IsFunctionRequired)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            else
            {
                if (this.colorSpace is PdfIndexedColorSpace)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                int functionDomainDimension = this.FunctionDomainDimension;
                if (this.function.Count == 1)
                {
                    PdfCustomFunction function = this.function[0];
                    if ((function.Domain.Count != functionDomainDimension) || (function.RangeSize != this.colorSpace.ComponentsCount))
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                }
                else
                {
                    if (this.function.Count != this.colorSpace.ComponentsCount)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    foreach (PdfCustomFunction function2 in this.function)
                    {
                        if ((function2.Domain.Count != functionDomainDimension) || (function2.RangeSize != 1))
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                    }
                }
            }
        }

        private static void CheckStreamAbsence(PdfReaderStream stream)
        {
            if (stream != null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }

        private static void CheckStreamPresence(PdfReaderStream stream)
        {
            if (stream == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }

        protected virtual PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.Add("ShadingType", this.ShadingType);
            dictionary.Add("ColorSpace", this.colorSpace.Write(objects));
            if (this.background != null)
            {
                dictionary.Add("Background", this.background.ToWritableObject());
            }
            dictionary.Add("BBox", this.boundingBox);
            dictionary.Add("AntiAlias", this.antiAlias, false);
            if (this.function != null)
            {
                dictionary.Add("Function", (this.function.Count == 1) ? ((PdfObject) this.function[0]) : ((PdfObject) this.function));
            }
            return dictionary;
        }

        internal static PdfShading Parse(object value)
        {
            PdfReaderDictionary dictionary;
            PdfReaderStream stream = value as PdfReaderStream;
            if (stream != null)
            {
                dictionary = stream.Dictionary;
            }
            else
            {
                dictionary = value as PdfReaderDictionary;
                if (dictionary == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            int? integer = dictionary.GetInteger("ShadingType");
            if (integer == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            switch (integer.Value)
            {
                case 1:
                    CheckStreamAbsence(stream);
                    return new PdfFunctionBasedShading(dictionary);

                case 2:
                    CheckStreamAbsence(stream);
                    return new PdfAxialShading(dictionary);

                case 3:
                    CheckStreamAbsence(stream);
                    return new PdfRadialShading(dictionary);

                case 4:
                    CheckStreamPresence(stream);
                    return new PdfFreeFormGouraudShadedTriangleMesh(stream);

                case 5:
                    CheckStreamPresence(stream);
                    return new PdfLatticeFormGouraudShadedTriangleMesh(stream);

                case 6:
                    CheckStreamPresence(stream);
                    return new PdfCoonsPatchMesh(stream);

                case 7:
                    CheckStreamPresence(stream);
                    return new PdfTensorProductPatchMesh(stream);
            }
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            return null;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects) => 
            this.CreateDictionary(objects);

        internal PdfColor TransformFunction(params double[] arguments)
        {
            double[] numArray;
            if (this.function == null)
            {
                return this.colorSpace.Transform(new PdfColor(arguments));
            }
            int count = this.function.Count;
            if (count == 1)
            {
                numArray = this.function[0].Transform(arguments);
            }
            else
            {
                numArray = new double[this.colorSpace.ComponentsCount];
                for (int i = 0; i < count; i++)
                {
                    double[] numArray1 = new double[] { arguments[0] };
                    numArray[i] = this.function[i].Transform(numArray1)[0];
                }
            }
            return this.colorSpace.Transform(new PdfColor(numArray));
        }

        public PdfColorSpace ColorSpace =>
            this.colorSpace;

        public PdfColor Background =>
            this.background;

        public PdfRectangle BoundingBox =>
            this.boundingBox;

        public bool AntiAlias =>
            this.antiAlias;

        public IList<PdfCustomFunction> Function =>
            this.function;

        protected virtual bool IsFunctionRequired =>
            true;

        protected virtual int FunctionDomainDimension =>
            1;

        protected abstract int ShadingType { get; }
    }
}

