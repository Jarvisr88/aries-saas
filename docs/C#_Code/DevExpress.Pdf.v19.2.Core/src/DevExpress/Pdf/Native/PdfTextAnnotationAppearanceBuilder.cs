namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Text;

    public class PdfTextAnnotationAppearanceBuilder : PdfMarkupAnnotationAppearanceBuilder<PdfTextAnnotation>
    {
        private const string defaultIconName = "Note";
        private const double defaultAppearanceAlphaConstant = 0.6;
        private static readonly Encoding encoding = Encoding.UTF8;
        private static readonly byte[] startApperanceData = encoding.GetBytes("q 1 1 1 rg 0 i 1 w 4 M 1 j 0 J []0 d ");

        public PdfTextAnnotationAppearanceBuilder(PdfTextAnnotation annotation) : base(annotation)
        {
        }

        private static PdfGraphicsStateParameters CreateGraphicsStateParameters()
        {
            PdfGraphicsStateParameters parameters1 = new PdfGraphicsStateParameters();
            parameters1.StrokingAlphaConstant = 0.6;
            parameters1.NonStrokingAlphaConstant = 0.6;
            return parameters1;
        }

        protected override void RebuildAppearance(PdfFormCommandConstructor constructor)
        {
            base.RebuildAppearance(constructor);
            PdfTextAnnotation annotation = base.Annotation;
            PdfColor color1 = annotation.Color;
            PdfColor color2 = color1;
            if (color1 == null)
            {
                PdfColor local1 = color1;
                double[] components = new double[] { 1.0 };
                color2 = new PdfColor(components);
            }
            PdfColor color = color2;
            string iconName = annotation.IconName;
            if (iconName == "Comment")
            {
                constructor.AddCommands(startApperanceData);
                constructor.SetGraphicsStateParameters(CreateGraphicsStateParameters());
                constructor.AddCommands(encoding.GetBytes("1 0 0 1 9 5.0908 cm 7.74 12.616 m -7.74 12.616 l -8.274 12.616 -8.707 12.184 -8.707 11.649 c -8.707 -3.831 l -8.707 -4.365 -8.274 -4.798 -7.74 -4.798 c 7.74 -4.798 l 8.274 -4.798 8.707 -4.365 8.707 -3.831 c 8.707 11.649 l 8.707 12.184 8.274 12.616 7.74 12.616 c h f Q 0 G "));
                constructor.SetColorForNonStrokingOperations(color);
                constructor.AddCommands(encoding.GetBytes("0 i 0.59 w 4 M 1 j 0 J []0 d  1 0 0 1 9 5.0908 cm 0 0 m -0.142 0 -0.28 0.008 -0.418 0.015 c -2.199 -1.969 -5.555 -2.242 -4.642 -1.42 c -4.024 -0.862 -3.916 0.111 -3.954 0.916 c -5.658 1.795 -6.772 3.222 -6.772 4.839 c -6.772 7.509 -3.74 9.674 0 9.674 c 3.74 9.674 6.772 7.509 6.772 4.839 c 6.772 2.167 3.74 0 0 0 c 7.74 12.616 m -7.74 12.616 l -8.274 12.616 -8.707 12.184 -8.707 11.649 c -8.707 -3.831 l -8.707 -4.365 -8.274 -4.798 -7.74 -4.798 c 7.74 -4.798 l 8.274 -4.798 8.707 -4.365 8.707 -3.831 c 8.707 11.649 l 8.707 12.184 8.274 12.616 7.74 12.616 c b"));
            }
            else if (iconName == "Key")
            {
                constructor.AddCommands(startApperanceData);
                constructor.SetGraphicsStateParameters(CreateGraphicsStateParameters());
                constructor.AddCommands(encoding.GetBytes("1 0 0 1 6.5 12.6729 cm 0.001 5.138 m -2.543 5.138 -4.604 3.077 -4.604 0.534 c -4.604 -1.368 -3.449 -3.001 -1.802 -3.702 c -1.802 -4.712 l -0.795 -5.719 l -1.896 -6.82 l -0.677 -8.039 l -1.595 -8.958 l -0.602 -9.949 l -1.479 -10.829 l -0.085 -12.483 l 1.728 -10.931 l 1.728 -3.732 l 1.737 -3.728 1.75 -3.724 1.76 -3.721 c 3.429 -3.03 4.604 -1.385 4.604 0.534 c 4.604 3.077 2.542 5.138 0.001 5.138 c f Q "));
                constructor.SetColorForNonStrokingOperations(color);
                constructor.AddCommands(encoding.GetBytes("0 G 0 i 0.59 w 4 M 1 j 0 J []0 d  1 0 0 1 6.5 12.6729 cm 0 0 m -1.076 0 -1.95 0.874 -1.95 1.95 c -1.95 3.028 -1.076 3.306 0 3.306 c 1.077 3.306 1.95 3.028 1.95 1.95 c 1.95 0.874 1.077 0 0 0 c 0.001 5.138 m -2.543 5.138 -4.604 3.077 -4.604 0.534 c -4.604 -1.368 -3.449 -3.001 -1.802 -3.702 c -1.802 -4.712 l -0.795 -5.719 l -1.896 -6.82 l -0.677 -8.039 l -1.595 -8.958 l -0.602 -9.949 l -1.479 -10.829 l -0.085 -12.483 l 1.728 -10.931 l 1.728 -3.732 l 1.737 -3.728 1.75 -3.724 1.76 -3.721 c 3.429 -3.03 4.604 -1.385 4.604 0.534 c 4.604 3.077 2.542 5.138 0.001 5.138 c b"));
            }
            else if (iconName == "Help")
            {
                constructor.AddCommands(startApperanceData);
                constructor.SetGraphicsStateParameters(CreateGraphicsStateParameters());
                constructor.AddCommands(encoding.GetBytes("1 0 0 1 12.1465 10.5137 cm -2.146 9.403 m -7.589 9.403 -12.001 4.99 -12.001 -0.453 c -12.001 -5.895 -7.589 -10.309 -2.146 -10.309 c 3.296 -10.309 7.709 -5.895 7.709 -0.453 c 7.709 4.99 3.296 9.403 -2.146 9.403 c h f Q "));
                constructor.SetColorForNonStrokingOperations(color);
                constructor.AddCommands(encoding.GetBytes("0 G 0 i 0.59 w 4 M 1 j 0 J []0 d  1 0 0 1 12.1465 10.5137 cm 0 0 m -0.682 -0.756 -0.958 -1.472 -0.938 -2.302 c -0.938 -2.632 l -3.385 -2.632 l -3.403 -2.154 l -3.459 -1.216 -3.147 -0.259 -2.316 0.716 c -1.729 1.433 -1.251 2.022 -1.251 2.647 c -1.251 3.291 -1.674 3.715 -2.594 3.751 c -3.202 3.751 -3.937 3.531 -4.417 3.2 c -5.041 5.205 l -4.361 5.591 -3.274 5.959 -1.968 5.959 c 0.46 5.959 1.563 4.616 1.563 3.089 c 1.563 1.691 0.699 0.771 0 0 c -2.227 -6.863 m -2.245 -6.863 l -3.202 -6.863 -3.864 -6.146 -3.864 -5.189 c -3.864 -4.196 -3.182 -3.516 -2.227 -3.516 c -1.233 -3.516 -0.589 -4.196 -0.57 -5.189 c -0.57 -6.146 -1.233 -6.863 -2.227 -6.863 c -2.146 9.403 m -7.589 9.403 -12.001 4.99 -12.001 -0.453 c -12.001 -5.895 -7.589 -10.309 -2.146 -10.309 c 3.296 -10.309 7.709 -5.895 7.709 -0.453 c 7.709 4.99 3.296 9.403 -2.146 9.403 c b"));
            }
            else if (iconName == "NewParagraph")
            {
                constructor.SetColorForNonStrokingOperations(color);
                constructor.AddCommands(encoding.GetBytes("0 G 0 i 0.58 w 4 M 0 j 0 J []0 d "));
                constructor.SetColorForNonStrokingOperations(color);
                constructor.AddCommands(encoding.GetBytes("0 G 0 i 0.59 w 4 M 1 j 0 J []0 d  q 1 0 0 1 6.4995 20 cm 0 0 m -6.205 -12.713 l 6.205 -12.713 l h b Q q 1 0 0 1 1.1909 6.2949 cm 0 0 m 1.278 0 l 1.353 0 1.362 -0.02 1.391 -0.066 c 2.128 -1.363 3.78 -4.275 3.966 -4.713 c 3.985 -4.713 l 3.976 -4.453 3.957 -3.91 3.957 -3.137 c 3.957 -0.076 l 3.957 -0.02 3.976 0 4.041 0 c 4.956 0 l 5.021 0 5.04 -0.029 5.04 -0.084 c 5.04 -6.049 l 5.04 -6.113 5.021 -6.133 4.947 -6.133 c 3.695 -6.133 l 3.621 -6.133 3.611 -6.113 3.574 -6.066 c 3.052 -4.955 1.353 -2.063 0.971 -1.186 c 0.961 -1.186 l 0.999 -1.68 0.999 -2.146 1.008 -3.025 c 1.008 -6.049 l 1.008 -6.104 0.989 -6.133 0.933 -6.133 c 0.009 -6.133 l -0.046 -6.133 -0.075 -6.123 -0.075 -6.049 c -0.075 -0.066 l -0.075 -0.02 -0.056 0 0 0 c f Q q 1 0 0 1 9.1367 3.0273 cm 0 0 m 0.075 0 0.215 -0.008 0.645 -0.008 c 1.4 -0.008 2.119 0.281 2.119 1.213 c 2.119 1.969 1.633 2.381 0.737 2.381 c 0.354 2.381 0.075 2.371 0 2.361 c h -1.146 3.201 m -1.146 3.238 -1.129 3.268 -1.082 3.268 c -0.709 3.275 0.02 3.285 0.729 3.285 c 2.613 3.285 3.248 2.314 3.258 1.232 c 3.258 -0.27 2.007 -0.914 0.607 -0.914 c 0.327 -0.914 0.057 -0.914 0 -0.904 c 0 -2.789 l 0 -2.836 -0.019 -2.865 -0.074 -2.865 c -1.082 -2.865 l -1.119 -2.865 -1.146 -2.846 -1.146 -2.799 c h f Q"));
            }
            else if (iconName == "Paragraph")
            {
                constructor.AddCommands(startApperanceData);
                constructor.SetGraphicsStateParameters(CreateGraphicsStateParameters());
                constructor.AddCommands(encoding.GetBytes("1 0 0 1 19.6973 10.0005 cm 0 0 m 0 -5.336 -4.326 -9.662 -9.663 -9.662 c -14.998 -9.662 -19.324 -5.336 -19.324 0 c -19.324 5.335 -14.998 9.662 -9.663 9.662 c -4.326 9.662 0 5.335 0 0 c h f Q "));
                constructor.SetColorForNonStrokingOperations(color);
                constructor.AddCommands(encoding.GetBytes("0 G 0 i 0.59 w 4 M 1 j 0 J []0 d  q 1 0 0 1 19.6973 10.0005 cm 0 0 m 0 -5.336 -4.326 -9.662 -9.663 -9.662 c -14.998 -9.662 -19.324 -5.336 -19.324 0 c -19.324 5.335 -14.998 9.662 -9.663 9.662 c -4.326 9.662 0 5.335 0 0 c h S Q q 1 0 0 1 11.6787 2.6582 cm 0 0 m -1.141 0 l -1.227 0 -1.244 0.052 -1.227 0.139 c -0.656 1.157 -0.52 2.505 -0.52 3.317 c -0.52 3.594 l -2.833 3.783 -5.441 4.838 -5.441 8.309 c -5.441 10.778 -3.714 12.626 -0.57 13.024 c -0.535 13.508 -0.381 14.129 -0.242 14.389 c -0.207 14.44 -0.174 14.475 -0.104 14.475 c 1.088 14.475 l 1.156 14.475 1.191 14.458 1.175 14.372 c 1.105 14.095 0.881 13.127 0.881 12.402 c 0.881 9.431 0.932 7.324 0.95 4.06 c 0.95 2.298 0.708 0.813 0.189 0.07 c 0.155 0.034 0.103 0 0 0 c b Q"));
            }
            else if (iconName != "Insert")
            {
                constructor.SetColorForNonStrokingOperations(color);
                constructor.AddCommands(encoding.GetBytes("0 G 0 i 0.61 w 4 M 0 j 0 J []0 d  q 1 0 0 1 16.959 1.3672 cm 0 0 m 0 -0.434 -0.352 -0.785 -0.784 -0.785 c -14.911 -0.785 l -15.345 -0.785 -15.696 -0.434 -15.696 0 c -15.696 17.266 l -15.696 17.699 -15.345 18.051 -14.911 18.051 c -0.784 18.051 l -0.352 18.051 0 17.699 0 17.266 c h b Q q 1 0 0 1 4.4023 13.9243 cm 0 0 m 9.418 0 l S Q q 1 0 0 1 4.4019 11.2207 cm 0 0 m 9.418 0 l S Q q 1 0 0 1 4.4023 8.5176 cm 0 0 m 9.418 0 l S Q q 1 0 0 1 4.4023 5.8135 cm 0 0 m 9.418 0 l S Q"));
            }
            else
            {
                constructor.AddCommands(encoding.GetBytes("0 G "));
                constructor.SetColorForNonStrokingOperations(color);
                constructor.AddCommands(encoding.GetBytes("0 i 0.59 w 4 M 0 j 0 J []0 d  1 0 0 1 8.5386 19.8545 cm 0 0 m -8.39 -19.719 l 8.388 -19.719 l h B"));
            }
        }
    }
}

