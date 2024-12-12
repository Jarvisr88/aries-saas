namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfPagePresentation
    {
        private const string dictionaryName = "Trans";
        private const string transitionStyleKey = "S";
        private const string durationKey = "D";
        private const string dimensionKey = "Dm";
        private const string motionDirectionKey = "M";
        private const string transitionDirectionKey = "Di";
        private const string scaleKey = "SS";
        private const string isRectAndOpaqueKey = "B";
        private readonly PdfTransitionStyle transitionStyle;
        private readonly double duration;
        private readonly PdfTransitionDimension dimension;
        private readonly PdfMotionDirection motionDirection;
        private readonly PdfTransitionDirection transitionDirection;
        private readonly PdfRange changesScale;
        private readonly bool isRectAndOpaque;

        internal PdfPagePresentation(PdfReaderDictionary dictionary)
        {
            double? number = dictionary.GetNumber("D");
            this.duration = (number != null) ? number.GetValueOrDefault() : 1.0;
            this.transitionStyle = dictionary.GetEnumName<PdfTransitionStyle>("S");
            this.dimension = dictionary.GetEnumName<PdfTransitionDimension>("Dm");
            this.motionDirection = dictionary.GetEnumName<PdfMotionDirection>("M");
            this.transitionDirection = this.GetTransitionDirection(dictionary);
            number = dictionary.GetNumber("SS");
            double min = (number != null) ? number.GetValueOrDefault() : 1.0;
            if (this.motionDirection == PdfMotionDirection.Inward)
            {
                this.changesScale = new PdfRange(min, 1.0);
            }
            if (this.motionDirection == PdfMotionDirection.Outward)
            {
                this.changesScale = new PdfRange(1.0, min);
            }
            bool? boolean = dictionary.GetBoolean("B");
            this.isRectAndOpaque = (boolean != null) ? boolean.GetValueOrDefault() : false;
        }

        private PdfTransitionDirection GetTransitionDirection(PdfReaderDictionary dictionary)
        {
            object obj2;
            if (!dictionary.TryGetValue("Di", out obj2))
            {
                return PdfTransitionDirection.LeftToRight;
            }
            obj2 = dictionary.Objects.TryResolve(obj2, null);
            if (obj2 == null)
            {
                return PdfTransitionDirection.LeftToRight;
            }
            PdfName name = obj2 as PdfName;
            if (name != null)
            {
                if (name.Name != "None")
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                return PdfTransitionDirection.None;
            }
            int num2 = (int) PdfDocumentReader.ConvertToDouble(obj2);
            if (num2 <= 90)
            {
                if (num2 == 0)
                {
                    return PdfTransitionDirection.LeftToRight;
                }
                if (num2 == 90)
                {
                    return PdfTransitionDirection.BottomToTop;
                }
            }
            else
            {
                if (num2 == 180)
                {
                    return PdfTransitionDirection.RightToLeft;
                }
                if (num2 == 270)
                {
                    return PdfTransitionDirection.TopToBottom;
                }
                if (num2 == 0x13b)
                {
                    return PdfTransitionDirection.TopLeftToBottomRight;
                }
            }
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            return PdfTransitionDirection.None;
        }

        private object TransitionDirectionToWritableObject()
        {
            switch (this.transitionDirection)
            {
                case PdfTransitionDirection.None:
                    return new PdfName("None");

                case PdfTransitionDirection.BottomToTop:
                    return (int) 90;

                case PdfTransitionDirection.RightToLeft:
                    return 180;

                case PdfTransitionDirection.TopToBottom:
                    return 270;

                case PdfTransitionDirection.TopLeftToBottomRight:
                    return 0x13d;
            }
            return 0;
        }

        internal PdfDictionary Write()
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(null);
            dictionary.Add("Type", new PdfName("Trans"));
            dictionary.AddEnumName<PdfTransitionStyle>("S", this.transitionStyle);
            dictionary.Add("D", this.duration, 1.0);
            dictionary.AddEnumName<PdfTransitionDimension>("Dm", this.dimension);
            dictionary.AddEnumName<PdfMotionDirection>("M", this.motionDirection);
            if (this.transitionDirection != PdfTransitionDirection.LeftToRight)
            {
                dictionary.Add("Di", this.TransitionDirectionToWritableObject());
            }
            if (this.motionDirection == PdfMotionDirection.Inward)
            {
                dictionary.Add("SS", this.changesScale.Min);
            }
            if (this.motionDirection == PdfMotionDirection.Outward)
            {
                dictionary.Add("SS", this.changesScale.Max);
            }
            dictionary.Add("B", this.isRectAndOpaque, false);
            return dictionary;
        }

        public PdfTransitionStyle TransitionStyle =>
            this.transitionStyle;

        public double Duration =>
            this.duration;

        public PdfTransitionDimension Dimension =>
            this.dimension;

        public PdfMotionDirection MotionDirection =>
            this.motionDirection;

        public PdfTransitionDirection TransitionDirection =>
            this.transitionDirection;

        public PdfRange ChangesScale =>
            this.changesScale;

        public bool IsRectAndOpaque =>
            this.isRectAndOpaque;
    }
}

