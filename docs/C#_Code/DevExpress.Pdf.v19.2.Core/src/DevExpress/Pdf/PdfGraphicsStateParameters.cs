namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfGraphicsStateParameters : PdfObject
    {
        private const string dictionaryType = "ExtGState";
        private const string lineWidthDictionaryKey = "LW";
        private const string lineCapDictionaryKey = "LC";
        private const string lineJoinDictionaryKey = "LJ";
        private const string miterLimitDictionaryKey = "ML";
        private const string dashPatternDictionaryKey = "D";
        private const string renderingIntentDictionaryKey = "RI";
        private const string strokingOverprintDictionaryKey = "OP";
        private const string nonStrokingOverprintDictionaryKey = "op";
        private const string overprintModeDictionaryKey = "OPM";
        private const string fontDictionaryKey = "Font";
        private const string blackGenerationFunctionDictionaryKey = "BG2";
        private const string undercolorRemovalFunctionDictionaryKey = "UCR2";
        private const string transferFunctionDictionaryKey = "TR2";
        private const string halftoneDictionaryKey = "HT";
        private const string flatnessToleranceDictionaryKey = "FL";
        private const string smoothnessToleranceDictionaryKey = "SM";
        private const string strokeAdjustmentDictionaryKey = "SA";
        private const string softMaskDictionaryKey = "SMask";
        private const string strokingAlphaConstantDictionaryKey = "CA";
        private const string nonStrokingAlphaConstantDictionaryKey = "ca";
        private const string alphaSourceDictionaryKey = "AIS";
        private const string textKnockoutDictionaryKey = "TK";
        private const string blendModeDictionaryKey = "BM";
        private double? lineWidth;
        private PdfLineCapStyle? lineCap;
        private PdfLineJoinStyle? lineJoin;
        private double? miterLimit;
        private PdfLineStyle lineStyle;
        private PdfRenderingIntent? renderingIntent;
        private bool? strokingOverprint;
        private bool? nonStrokingOverprint;
        private PdfOverprintMode? overprintMode;
        private PdfFont font;
        private double? fontSize;
        private PdfFunction blackGenerationFunction;
        private PdfFunction undercolorRemovalFunction;
        private PdfFunction[] transferFunction;
        private PdfHalftone halftone;
        private double? flatnessTolerance;
        private double? smoothnessTolerance;
        private bool? strokeAdjustment;
        private PdfBlendMode? blendMode;
        private PdfSoftMask softMask;
        private double? strokingAlphaConstant;
        private double? nonStrokingAlphaConstant;
        private bool? alphaSource;
        private bool? textKnockout;

        internal PdfGraphicsStateParameters()
        {
        }

        internal PdfGraphicsStateParameters(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            object obj2;
            PdfObjectCollection objects = dictionary.Objects;
            this.lineWidth = dictionary.GetNumber("LW");
            int? integer = dictionary.GetInteger("LC");
            if (integer != null)
            {
                this.lineCap = new PdfLineCapStyle?(PdfSetLineCapStyleCommand.ConvertToLineCapStyle(integer.Value));
            }
            int? nullable2 = dictionary.GetInteger("LJ");
            if (nullable2 != null)
            {
                this.lineJoin = new PdfLineJoinStyle?(PdfSetLineJoinStyleCommand.ConvertToLineJoinStyle(nullable2.Value));
            }
            this.miterLimit = dictionary.GetNumber("ML");
            IList<object> array = dictionary.GetArray("D");
            if (array != null)
            {
                this.lineStyle = PdfLineStyle.Parse(array);
            }
            string name = dictionary.GetName("RI");
            if (name != null)
            {
                this.renderingIntent = new PdfRenderingIntent?(PdfEnumToStringConverter.Parse<PdfRenderingIntent>(name, true));
            }
            this.strokingOverprint = dictionary.GetBoolean("OP", true);
            this.nonStrokingOverprint = dictionary.GetBoolean("op", true);
            int? nullable3 = dictionary.GetInteger("OPM");
            if (nullable3 != null)
            {
                PdfOverprintMode? defaultValue = null;
                this.overprintMode = new PdfOverprintMode?(PdfEnumToValueConverter.Parse<PdfOverprintMode>(new int?(nullable3.Value), defaultValue));
            }
            IList<object> list2 = dictionary.GetArray("Font");
            if (list2 != null)
            {
                if (list2.Count != 2)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.font = objects.GetObject<PdfFont>(list2[0], new Func<PdfReaderDictionary, PdfFont>(PdfFont.CreateFont));
                if (this.font == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.fontSize = new double?(PdfDocumentReader.ConvertToDouble(list2[1]));
            }
            PdfFunction function1 = ResolveFunction(dictionary, "BG2");
            PdfFunction function3 = function1;
            if (function1 == null)
            {
                PdfFunction local1 = function1;
                function3 = ResolveFunction(dictionary, "BG");
            }
            this.blackGenerationFunction = function3;
            PdfFunction function2 = ResolveFunction(dictionary, "UCR2");
            PdfFunction function4 = function2;
            if (function2 == null)
            {
                PdfFunction local2 = function2;
                function4 = ResolveFunction(dictionary, "UCR");
            }
            this.undercolorRemovalFunction = function4;
            PdfFunction[] functionArray1 = ResolveFunctions(dictionary, "TR2");
            PdfFunction[] functionArray2 = functionArray1;
            if (functionArray1 == null)
            {
                PdfFunction[] local3 = functionArray1;
                functionArray2 = ResolveFunctions(dictionary, "TR");
            }
            this.transferFunction = functionArray2;
            this.halftone = dictionary.GetHalftone("HT");
            this.flatnessTolerance = dictionary.GetNumber("FL");
            this.smoothnessTolerance = dictionary.GetNumber("SM");
            this.strokeAdjustment = dictionary.GetBoolean("SA", true);
            string str = dictionary.GetName("BM");
            if (str != null)
            {
                this.blendMode = PdfEnumToStringConverter.Parse<PdfBlendMode>(str, false, true);
            }
            if (dictionary.TryGetValue("SMask", out obj2))
            {
                this.softMask = PdfSoftMask.Create(objects, obj2);
            }
            this.strokingAlphaConstant = dictionary.GetNumber("CA");
            this.nonStrokingAlphaConstant = dictionary.GetNumber("ca");
            this.alphaSource = dictionary.GetBoolean("AIS", true);
            this.textKnockout = dictionary.GetBoolean("TK", true);
        }

        private static PdfFunction ResolveFunction(PdfReaderDictionary dictionary, string key)
        {
            object obj2;
            return (dictionary.TryGetValue(key, out obj2) ? PdfFunction.Parse(dictionary.Objects, obj2, true) : null);
        }

        private static PdfFunction[] ResolveFunctions(PdfReaderDictionary dictionary, string key)
        {
            object obj2;
            if (!dictionary.TryGetValue(key, out obj2))
            {
                return null;
            }
            PdfObjectCollection objects = dictionary.Objects;
            IList<object> list = obj2 as IList<object>;
            if (list == null)
            {
                return new PdfFunction[] { PdfFunction.Parse(objects, obj2, true) };
            }
            int count = list.Count;
            PdfFunction[] functionArray = new PdfFunction[count];
            for (int i = 0; i < count; i++)
            {
                functionArray[i] = PdfFunction.Parse(objects, list[i], true);
            }
            return functionArray;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.Add("Type", new PdfName("ExtGState"));
            dictionary.AddNullable<double>("LW", this.lineWidth);
            if (this.lineCap != null)
            {
                dictionary.Add("LC", (int) this.lineCap.Value);
            }
            if (this.lineJoin != null)
            {
                dictionary.Add("LJ", (int) this.lineJoin.Value);
            }
            dictionary.AddNullable<double>("ML", this.miterLimit);
            if (this.lineStyle != null)
            {
                dictionary.Add("D", this.lineStyle.Data);
            }
            if (this.renderingIntent != null)
            {
                dictionary.AddEnumName<PdfRenderingIntent>("RI", this.renderingIntent.Value, false);
            }
            dictionary.AddNullable<bool>("OP", this.strokingOverprint);
            dictionary.AddNullable<bool>("op", this.nonStrokingOverprint);
            if (this.overprintMode != null)
            {
                dictionary.Add("OPM", (int) this.overprintMode.Value);
            }
            if (this.font != null)
            {
                List<object> list1 = new List<object>();
                list1.Add(objects.AddObject((PdfObject) this.font));
                list1.Add(this.fontSize.Value);
                dictionary.Add("Font", list1);
            }
            if (this.blackGenerationFunction != null)
            {
                dictionary.Add("BG2", this.blackGenerationFunction.Write(objects));
            }
            if (this.undercolorRemovalFunction != null)
            {
                dictionary.Add("UCR2", this.undercolorRemovalFunction.Write(objects));
            }
            if (this.transferFunction != null)
            {
                if (this.transferFunction.Length == 1)
                {
                    dictionary.Add("TR2", this.transferFunction[0].Write(objects));
                }
                else
                {
                    dictionary.AddList<PdfFunction>("TR2", this.transferFunction, value => value.Write(objects));
                }
            }
            if (this.halftone != null)
            {
                dictionary.Add("HT", this.halftone.CreateWriteableObject(objects));
            }
            dictionary.AddNullable<double>("FL", this.flatnessTolerance);
            dictionary.AddNullable<double>("SM", this.smoothnessTolerance);
            dictionary.AddNullable<bool>("SA", this.strokeAdjustment);
            if (this.blendMode != null)
            {
                dictionary.AddEnumName<PdfBlendMode>("BM", this.blendMode.Value);
            }
            if (this.softMask != null)
            {
                dictionary.Add("SMask", this.softMask.Write(objects));
            }
            dictionary.AddNullable<double>("CA", this.strokingAlphaConstant);
            dictionary.AddNullable<double>("ca", this.nonStrokingAlphaConstant);
            dictionary.AddNullable<bool>("AIS", this.alphaSource);
            dictionary.AddNullable<bool>("TK", this.textKnockout);
            return dictionary;
        }

        public double? LineWidth
        {
            get => 
                this.lineWidth;
            set => 
                this.lineWidth = value;
        }

        public PdfLineCapStyle? LineCap
        {
            get => 
                this.lineCap;
            set => 
                this.lineCap = value;
        }

        public PdfLineJoinStyle? LineJoin
        {
            get => 
                this.lineJoin;
            set => 
                this.lineJoin = value;
        }

        public double? MiterLimit
        {
            get => 
                this.miterLimit;
            set => 
                this.miterLimit = value;
        }

        public PdfLineStyle LineStyle
        {
            get => 
                this.lineStyle;
            set => 
                this.lineStyle = value;
        }

        public PdfRenderingIntent? RenderingIntent
        {
            get => 
                this.renderingIntent;
            set => 
                this.renderingIntent = value;
        }

        public bool? StrokingOverprint
        {
            get => 
                this.strokingOverprint;
            set => 
                this.strokingOverprint = value;
        }

        public bool? NonStrokingOverprint
        {
            get => 
                this.nonStrokingOverprint;
            set => 
                this.nonStrokingOverprint = value;
        }

        public PdfOverprintMode? OverprintMode
        {
            get => 
                this.overprintMode;
            set => 
                this.overprintMode = value;
        }

        public PdfFont Font
        {
            get => 
                this.font;
            set => 
                this.font = value;
        }

        public double? FontSize
        {
            get => 
                this.fontSize;
            set => 
                this.fontSize = value;
        }

        public PdfFunction BlackGenerationFunction
        {
            get => 
                this.blackGenerationFunction;
            set => 
                this.blackGenerationFunction = value;
        }

        public PdfFunction UndercolorRemovalFunction
        {
            get => 
                this.undercolorRemovalFunction;
            set => 
                this.undercolorRemovalFunction = value;
        }

        public PdfFunction[] TransferFunction
        {
            get => 
                this.transferFunction;
            set => 
                this.transferFunction = value;
        }

        public PdfHalftone Halftone
        {
            get => 
                this.halftone;
            set => 
                this.halftone = value;
        }

        public double? FlatnessTolerance
        {
            get => 
                this.flatnessTolerance;
            set => 
                this.flatnessTolerance = value;
        }

        public double? SmoothnessTolerance
        {
            get => 
                this.smoothnessTolerance;
            set => 
                this.smoothnessTolerance = value;
        }

        public bool? StrokeAdjustment
        {
            get => 
                this.strokeAdjustment;
            set => 
                this.strokeAdjustment = value;
        }

        public PdfBlendMode? BlendMode
        {
            get => 
                this.blendMode;
            set => 
                this.blendMode = value;
        }

        public PdfSoftMask SoftMask
        {
            get => 
                this.softMask;
            set => 
                this.softMask = value;
        }

        public double? StrokingAlphaConstant
        {
            get => 
                this.strokingAlphaConstant;
            set => 
                this.strokingAlphaConstant = value;
        }

        public double? NonStrokingAlphaConstant
        {
            get => 
                this.nonStrokingAlphaConstant;
            set => 
                this.nonStrokingAlphaConstant = value;
        }

        public bool? AlphaSource
        {
            get => 
                this.alphaSource;
            set => 
                this.alphaSource = value;
        }

        public bool? TextKnockout
        {
            get => 
                this.textKnockout;
            set => 
                this.textKnockout = value;
        }
    }
}

