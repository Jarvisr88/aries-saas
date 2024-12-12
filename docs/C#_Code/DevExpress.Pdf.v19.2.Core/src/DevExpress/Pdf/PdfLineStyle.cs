namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfLineStyle
    {
        private readonly double[] dashPattern;
        private readonly double dashPhase;

        private PdfLineStyle(double[] dashPattern, double dashPhase)
        {
            this.dashPattern = dashPattern;
            this.dashPhase = dashPhase;
        }

        public static PdfLineStyle CreateDashed(double[] dashPattern, double dashPhase)
        {
            if (dashPattern == null)
            {
                throw new ArgumentNullException("dashPattern");
            }
            if (dashPattern.Length == 0)
            {
                throw new ArgumentOutOfRangeException("dashPattern", PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectDashPatternArraySize));
            }
            double num = 0.0;
            foreach (double num3 in dashPattern)
            {
                num += num3;
            }
            if (num == 0.0)
            {
                throw new ArgumentOutOfRangeException("dashPattern", PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectDashPattern));
            }
            return new PdfLineStyle(dashPattern, dashPhase);
        }

        public static PdfLineStyle CreateDashed(double dashLength, double gapLength, double dashPhase)
        {
            if (dashLength < 0.0)
            {
                throw new ArgumentOutOfRangeException("dashLength", PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectDashLength));
            }
            if (gapLength < 0.0)
            {
                throw new ArgumentOutOfRangeException("gapLength", PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectGapLength));
            }
            if ((dashLength + gapLength) == 0.0)
            {
                throw new ArgumentOutOfRangeException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectDashPattern));
            }
            double[] dashPattern = new double[] { dashLength, gapLength };
            return new PdfLineStyle(dashPattern, dashPhase);
        }

        public static PdfLineStyle CreateSolid() => 
            new PdfLineStyle(null, 0.0);

        internal bool IsSame(PdfLineStyle lineStyle)
        {
            double[] dashPattern = lineStyle.dashPattern;
            if (this.dashPattern == null)
            {
                return (dashPattern == null);
            }
            int length = this.dashPattern.Length;
            if ((dashPattern == null) || ((dashPattern.Length != length) || (this.dashPhase != lineStyle.dashPhase)))
            {
                return false;
            }
            for (int i = 0; i < length; i++)
            {
                if (this.dashPattern[i] != dashPattern[i])
                {
                    return false;
                }
            }
            return true;
        }

        internal static PdfLineStyle Parse(IList<object> parameters)
        {
            if (parameters.Count != 2)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            IList<object> dashArray = parameters[0] as IList<object>;
            if (dashArray == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return ((dashArray.Count == 0) ? CreateSolid() : CreateDashed(ParseDashPattern(dashArray), PdfDocumentReader.ConvertToDouble(parameters[1])));
        }

        internal static double[] ParseDashPattern(IList<object> dashArray)
        {
            double num = 0.0;
            int count = dashArray.Count;
            double[] numArray = new double[count];
            for (int i = 0; i < count; i++)
            {
                double num4 = PdfDocumentReader.ConvertToDouble(dashArray[i]);
                num += num4;
                numArray[i] = num4;
            }
            if (num == 0.0)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return numArray;
        }

        public bool IsDashed =>
            this.dashPattern != null;

        public double[] DashPattern =>
            this.dashPattern;

        public double DashPhase =>
            this.dashPhase;

        internal IList<object> Data
        {
            get
            {
                int capacity = (this.dashPattern == null) ? 0 : this.dashPattern.Length;
                List<object> item = new List<object>(capacity);
                for (int i = 0; i < capacity; i++)
                {
                    item.Add(this.dashPattern[i]);
                }
                List<object> list1 = new List<object>();
                list1.Add(item);
                list1.Add(this.dashPhase);
                return list1;
            }
        }
    }
}

