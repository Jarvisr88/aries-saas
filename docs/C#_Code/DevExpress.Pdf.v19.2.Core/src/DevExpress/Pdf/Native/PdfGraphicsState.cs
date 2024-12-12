namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfGraphicsState
    {
        private PdfTransformationMatrix transformationMatrix = new PdfTransformationMatrix();
        private PdfColorSpace strokingColorSpace = new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.Gray);
        private PdfColorSpace nonStrokingColorSpace = new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.Gray);
        private PdfColor strokingColor = new PdfColor(new double[1]);
        private PdfColor nonStrokingColor = new PdfColor(new double[1]);
        private PdfTextState textState = new PdfTextState();
        private double lineWidth = 1.0;
        private PdfLineCapStyle lineCap;
        private PdfLineJoinStyle lineJoin;
        private double miterLimit = 10.0;
        private PdfLineStyle lineStyle = PdfLineStyle.CreateSolid();
        private PdfRenderingIntent renderingIntent = PdfRenderingIntent.RelativeColorimetric;
        private bool strokeAdjustment;
        private PdfBlendMode blendMode;
        private PdfSoftMask softMask = PdfEmptySoftMask.Instance;
        private double strokingAlphaConstant = 1.0;
        private double nonStrokingAlphaConstant = 1.0;
        private bool alphaSource;
        private bool strokingOverprint;
        private bool nonStrokingOverprint;
        private PdfOverprintMode overprintMode;
        private PdfFunction blackGenerationFunction = PdfPredefinedFunction.Default;
        private PdfFunction undercolorRemovalFunction = PdfPredefinedFunction.Default;
        private PdfFunction[] transferFunction = new PdfFunction[] { PdfPredefinedFunction.Default };
        private PdfHalftone halftone = PdfDefaultHalftone.Instance;
        private double flatnessTolerance = 1.0;
        private double smoothnessTolerance;
        private bool textKnockout = true;
        private PdfTransformationMatrix softMaskTransformationMatrix;
        private bool isDefaultNonStrokingColor = true;

        private PdfGraphicsStateChange ApplyParameter<T>(ref T parameter, T? value, PdfGraphicsStateChange change) where T: struct
        {
            if (value != null)
            {
                T local = value.Value;
                if (!local.Equals((T) parameter))
                {
                    parameter = local;
                    return change;
                }
            }
            return PdfGraphicsStateChange.None;
        }

        internal PdfGraphicsStateChange ApplyParameters(PdfGraphicsStateParameters parameters)
        {
            PdfGraphicsStateChange change = ((((((((((((((this.ApplyParameter<double>(ref this.lineWidth, parameters.LineWidth, PdfGraphicsStateChange.Pen) | this.ApplyParameter<PdfLineCapStyle>(ref this.lineCap, parameters.LineCap, PdfGraphicsStateChange.Pen)) | this.ApplyParameter<PdfLineJoinStyle>(ref this.lineJoin, parameters.LineJoin, PdfGraphicsStateChange.Pen)) | this.ApplyParameter<double>(ref this.miterLimit, parameters.MiterLimit, PdfGraphicsStateChange.Pen)) | this.ApplyParameter<PdfRenderingIntent>(ref this.renderingIntent, parameters.RenderingIntent, PdfGraphicsStateChange.RenderingIntent)) | this.ApplyParameter<bool>(ref this.strokingOverprint, parameters.StrokingOverprint, PdfGraphicsStateChange.Overprint)) | this.ApplyParameter<bool>(ref this.nonStrokingOverprint, parameters.NonStrokingOverprint, PdfGraphicsStateChange.Overprint)) | this.ApplyParameter<PdfOverprintMode>(ref this.overprintMode, parameters.OverprintMode, PdfGraphicsStateChange.Overprint)) | this.ApplyParameter<double>(ref this.flatnessTolerance, parameters.FlatnessTolerance, PdfGraphicsStateChange.FlatnessTolerance)) | this.ApplyParameter<double>(ref this.smoothnessTolerance, parameters.SmoothnessTolerance, PdfGraphicsStateChange.SmoothnessTolerance)) | this.ApplyParameter<bool>(ref this.strokeAdjustment, parameters.StrokeAdjustment, PdfGraphicsStateChange.StrokeAdjustment)) | this.ApplyParameter<PdfBlendMode>(ref this.blendMode, parameters.BlendMode, PdfGraphicsStateChange.BlendMode)) | this.ApplyParameter<double>(ref this.strokingAlphaConstant, parameters.StrokingAlphaConstant, PdfGraphicsStateChange.Alpha)) | this.ApplyParameter<double>(ref this.nonStrokingAlphaConstant, parameters.NonStrokingAlphaConstant, PdfGraphicsStateChange.Alpha)) | this.ApplyParameter<bool>(ref this.alphaSource, parameters.AlphaSource, PdfGraphicsStateChange.Alpha)) | this.ApplyParameter<bool>(ref this.textKnockout, parameters.TextKnockout, PdfGraphicsStateChange.TextKnockout);
            PdfLineStyle lineStyle = parameters.LineStyle;
            if ((lineStyle != null) && ((this.lineStyle == null) || !this.lineStyle.IsSame(lineStyle)))
            {
                this.lineStyle = lineStyle;
                change |= PdfGraphicsStateChange.Pen;
            }
            PdfFont objA = parameters.Font;
            if ((objA != null) && !ReferenceEquals(objA, this.textState.Font))
            {
                this.textState.Font = objA;
                change |= PdfGraphicsStateChange.Font;
            }
            double? fontSize = parameters.FontSize;
            if (fontSize != null)
            {
                double num = fontSize.Value;
                if (num != this.textState.FontSize)
                {
                    this.textState.FontSize = num;
                    change |= PdfGraphicsStateChange.Font;
                }
            }
            PdfFunction blackGenerationFunction = parameters.BlackGenerationFunction;
            if ((blackGenerationFunction != null) && !blackGenerationFunction.IsSame(this.blackGenerationFunction))
            {
                this.blackGenerationFunction = blackGenerationFunction;
                change |= PdfGraphicsStateChange.BlackGenerationFunction;
            }
            PdfFunction undercolorRemovalFunction = parameters.UndercolorRemovalFunction;
            if ((undercolorRemovalFunction != null) && !undercolorRemovalFunction.IsSame(this.undercolorRemovalFunction))
            {
                this.undercolorRemovalFunction = undercolorRemovalFunction;
                change |= PdfGraphicsStateChange.UndercolorRemovalFunction;
            }
            PdfFunction[] transferFunction = parameters.TransferFunction;
            if (transferFunction != null)
            {
                int length = this.transferFunction.Length;
                bool flag = transferFunction.Length != length;
                if (flag)
                {
                    for (int i = 0; i < length; i++)
                    {
                        if (!transferFunction[i].IsSame(this.transferFunction[i]))
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    this.transferFunction = transferFunction;
                    change |= PdfGraphicsStateChange.TransferFunction;
                }
            }
            PdfHalftone halftone = parameters.Halftone;
            if ((halftone != null) && !halftone.IsSame(this.halftone))
            {
                this.halftone = halftone;
                change |= PdfGraphicsStateChange.Halftone;
            }
            PdfSoftMask softMask = parameters.SoftMask;
            if ((softMask != null) && !softMask.IsSame(this.softMask))
            {
                this.softMask = softMask;
                change |= PdfGraphicsStateChange.SoftMask;
                this.softMaskTransformationMatrix = this.transformationMatrix;
            }
            return change;
        }

        public PdfGraphicsState Clone() => 
            new PdfGraphicsState { 
                transformationMatrix = this.transformationMatrix,
                strokingColorSpace = this.strokingColorSpace,
                nonStrokingColorSpace = this.nonStrokingColorSpace,
                strokingColor = this.strokingColor,
                nonStrokingColor = this.nonStrokingColor,
                textState = this.textState.Clone(),
                lineWidth = this.lineWidth,
                lineCap = this.lineCap,
                lineJoin = this.lineJoin,
                miterLimit = this.miterLimit,
                lineStyle = this.lineStyle,
                renderingIntent = this.renderingIntent,
                strokeAdjustment = this.strokeAdjustment,
                blendMode = this.blendMode,
                softMask = this.softMask,
                strokingAlphaConstant = this.strokingAlphaConstant,
                nonStrokingAlphaConstant = this.nonStrokingAlphaConstant,
                alphaSource = this.alphaSource,
                strokingOverprint = this.strokingOverprint,
                nonStrokingOverprint = this.nonStrokingOverprint,
                overprintMode = this.overprintMode,
                blackGenerationFunction = this.blackGenerationFunction,
                undercolorRemovalFunction = this.undercolorRemovalFunction,
                transferFunction = this.transferFunction,
                halftone = this.halftone,
                flatnessTolerance = this.flatnessTolerance,
                smoothnessTolerance = this.smoothnessTolerance,
                textKnockout = this.textKnockout,
                softMaskTransformationMatrix = this.softMaskTransformationMatrix,
                isDefaultNonStrokingColor = this.isDefaultNonStrokingColor
            };

        public PdfGraphicsState CloneForTransparencyGroupRendering() => 
            new PdfGraphicsState { 
                transformationMatrix = this.transformationMatrix,
                strokingColorSpace = this.strokingColorSpace,
                nonStrokingColorSpace = this.nonStrokingColorSpace,
                strokingColor = this.strokingColor,
                nonStrokingColor = this.nonStrokingColor,
                textState = this.textState.Clone(),
                lineWidth = this.lineWidth,
                lineCap = this.lineCap,
                lineJoin = this.lineJoin,
                miterLimit = this.miterLimit,
                lineStyle = this.lineStyle,
                renderingIntent = this.renderingIntent,
                strokeAdjustment = this.strokeAdjustment,
                blendMode = PdfBlendMode.Normal,
                softMask = PdfEmptySoftMask.Instance,
                strokingAlphaConstant = 1.0,
                nonStrokingAlphaConstant = 1.0,
                alphaSource = this.alphaSource,
                strokingOverprint = this.strokingOverprint,
                nonStrokingOverprint = this.nonStrokingOverprint,
                overprintMode = this.overprintMode,
                blackGenerationFunction = this.blackGenerationFunction,
                undercolorRemovalFunction = this.undercolorRemovalFunction,
                transferFunction = this.transferFunction,
                halftone = this.halftone,
                flatnessTolerance = this.flatnessTolerance,
                smoothnessTolerance = this.smoothnessTolerance,
                textKnockout = this.textKnockout,
                softMaskTransformationMatrix = this.softMaskTransformationMatrix,
                isDefaultNonStrokingColor = this.isDefaultNonStrokingColor
            };

        public PdfTransformationMatrix TransformationMatrix
        {
            get => 
                this.transformationMatrix;
            internal set => 
                this.transformationMatrix = value;
        }

        public PdfColorSpace StrokingColorSpace
        {
            get => 
                this.strokingColorSpace;
            internal set => 
                this.strokingColorSpace = value;
        }

        public PdfColorSpace NonStrokingColorSpace
        {
            get => 
                this.nonStrokingColorSpace;
            internal set => 
                this.nonStrokingColorSpace = value;
        }

        public PdfColor StrokingColor
        {
            get => 
                this.strokingColor;
            internal set => 
                this.strokingColor = value;
        }

        public PdfColor NonStrokingColor
        {
            get => 
                this.nonStrokingColor;
            internal set
            {
                this.isDefaultNonStrokingColor = false;
                this.nonStrokingColor = value;
            }
        }

        public PdfTextState TextState =>
            this.textState;

        public double LineWidth
        {
            get => 
                this.lineWidth;
            internal set => 
                this.lineWidth = value;
        }

        public PdfLineCapStyle LineCap
        {
            get => 
                this.lineCap;
            internal set => 
                this.lineCap = value;
        }

        public PdfLineJoinStyle LineJoin
        {
            get => 
                this.lineJoin;
            set => 
                this.lineJoin = value;
        }

        public double MiterLimit
        {
            get => 
                this.miterLimit;
            internal set => 
                this.miterLimit = value;
        }

        public PdfLineStyle LineStyle
        {
            get => 
                this.lineStyle;
            internal set => 
                this.lineStyle = value;
        }

        public PdfRenderingIntent RenderingIntent
        {
            get => 
                this.renderingIntent;
            internal set => 
                this.renderingIntent = value;
        }

        public bool StrokeAdjustment =>
            this.strokeAdjustment;

        public PdfBlendMode BlendMode =>
            this.blendMode;

        public PdfSoftMask SoftMask =>
            this.softMask;

        public double StrokingAlphaConstant =>
            this.strokingAlphaConstant;

        public double NonStrokingAlphaConstant =>
            this.nonStrokingAlphaConstant;

        public bool AlphaSource =>
            this.alphaSource;

        public bool StrokingOverprint =>
            this.strokingOverprint;

        public bool NonStrokingOverprint =>
            this.nonStrokingOverprint;

        public PdfOverprintMode OverprintMode =>
            this.overprintMode;

        public PdfFunction BlackGenerationFunction =>
            this.blackGenerationFunction;

        public PdfFunction UndercolorRemovalFunction =>
            this.undercolorRemovalFunction;

        public PdfFunction[] TransferFunction =>
            this.transferFunction;

        public PdfHalftone Halftone =>
            this.halftone;

        public double FlatnessTolerance
        {
            get => 
                this.flatnessTolerance;
            internal set => 
                this.flatnessTolerance = value;
        }

        public double SmoothnessTolerance =>
            this.smoothnessTolerance;

        public bool TextKnockout =>
            this.textKnockout;

        public PdfTransformationMatrix SoftMaskTransformationMatrix =>
            this.softMaskTransformationMatrix;

        public bool IsDefaultNonStrokingColor =>
            this.isDefaultNonStrokingColor;
    }
}

