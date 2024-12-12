namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows.Media;

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class PdfMarkupToolsSettings : BindableBase
    {
        public PdfMarkupToolsSettings()
        {
            this.TextHighlightColor = this.FromPdfColor(PdfTextMarkupAnnotationData.HighlightDefaultColor);
            this.TextStrikethroughColor = this.FromPdfColor(PdfTextMarkupAnnotationData.StrikeOutDefaultColor);
            this.TextUnderlineColor = this.FromPdfColor(PdfTextMarkupAnnotationData.UnderlineDefaultColor);
            this.Author = Environment.UserName;
            this.TextHighlightDefaultSubject = PdfTextMarkupAnnotationData.GetSubject(PdfTextMarkupAnnotationType.Highlight);
            this.TextStrikethroughDefaultSubject = PdfTextMarkupAnnotationData.GetSubject(PdfTextMarkupAnnotationType.StrikeOut);
            this.TextUnderlineDefaultSubject = PdfTextMarkupAnnotationData.GetSubject(PdfTextMarkupAnnotationType.Underline);
        }

        private Color FromPdfColor(PdfColor color)
        {
            if (color == null)
            {
                return Colors.Transparent;
            }
            PdfRGBColorData data = new PdfRGBColorData(color);
            return Color.FromArgb(0xff, Convert.ToByte((double) (data.R * 255.0)), Convert.ToByte((double) (data.G * 255.0)), Convert.ToByte((double) (data.B * 255.0)));
        }

        public Color TextHighlightColor
        {
            get => 
                base.GetProperty<Color>(Expression.Lambda<Func<Color>>(Expression.Property(Expression.Constant(this, typeof(PdfMarkupToolsSettings)), (MethodInfo) methodof(PdfMarkupToolsSettings.get_TextHighlightColor)), new ParameterExpression[0]));
            set => 
                base.SetProperty<Color>(Expression.Lambda<Func<Color>>(Expression.Property(Expression.Constant(this, typeof(PdfMarkupToolsSettings)), (MethodInfo) methodof(PdfMarkupToolsSettings.get_TextHighlightColor)), new ParameterExpression[0]), value);
        }

        public Color TextStrikethroughColor
        {
            get => 
                base.GetProperty<Color>(Expression.Lambda<Func<Color>>(Expression.Property(Expression.Constant(this, typeof(PdfMarkupToolsSettings)), (MethodInfo) methodof(PdfMarkupToolsSettings.get_TextStrikethroughColor)), new ParameterExpression[0]));
            set => 
                base.SetProperty<Color>(Expression.Lambda<Func<Color>>(Expression.Property(Expression.Constant(this, typeof(PdfMarkupToolsSettings)), (MethodInfo) methodof(PdfMarkupToolsSettings.get_TextStrikethroughColor)), new ParameterExpression[0]), value);
        }

        public Color TextUnderlineColor
        {
            get => 
                base.GetProperty<Color>(Expression.Lambda<Func<Color>>(Expression.Property(Expression.Constant(this, typeof(PdfMarkupToolsSettings)), (MethodInfo) methodof(PdfMarkupToolsSettings.get_TextUnderlineColor)), new ParameterExpression[0]));
            set => 
                base.SetProperty<Color>(Expression.Lambda<Func<Color>>(Expression.Property(Expression.Constant(this, typeof(PdfMarkupToolsSettings)), (MethodInfo) methodof(PdfMarkupToolsSettings.get_TextUnderlineColor)), new ParameterExpression[0]), value);
        }

        public string Author
        {
            get => 
                base.GetProperty<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(PdfMarkupToolsSettings)), (MethodInfo) methodof(PdfMarkupToolsSettings.get_Author)), new ParameterExpression[0]));
            set => 
                base.SetProperty<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(PdfMarkupToolsSettings)), (MethodInfo) methodof(PdfMarkupToolsSettings.get_Author)), new ParameterExpression[0]), value);
        }

        public string TextHighlightDefaultSubject
        {
            get => 
                base.GetProperty<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(PdfMarkupToolsSettings)), (MethodInfo) methodof(PdfMarkupToolsSettings.get_TextHighlightDefaultSubject)), new ParameterExpression[0]));
            set => 
                base.SetProperty<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(PdfMarkupToolsSettings)), (MethodInfo) methodof(PdfMarkupToolsSettings.get_TextHighlightDefaultSubject)), new ParameterExpression[0]), value);
        }

        public string TextStrikethroughDefaultSubject
        {
            get => 
                base.GetProperty<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(PdfMarkupToolsSettings)), (MethodInfo) methodof(PdfMarkupToolsSettings.get_TextStrikethroughDefaultSubject)), new ParameterExpression[0]));
            set => 
                base.SetProperty<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(PdfMarkupToolsSettings)), (MethodInfo) methodof(PdfMarkupToolsSettings.get_TextStrikethroughDefaultSubject)), new ParameterExpression[0]), value);
        }

        public string TextUnderlineDefaultSubject
        {
            get => 
                base.GetProperty<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(PdfMarkupToolsSettings)), (MethodInfo) methodof(PdfMarkupToolsSettings.get_TextUnderlineDefaultSubject)), new ParameterExpression[0]));
            set => 
                base.SetProperty<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(PdfMarkupToolsSettings)), (MethodInfo) methodof(PdfMarkupToolsSettings.get_TextUnderlineDefaultSubject)), new ParameterExpression[0]), value);
        }
    }
}

