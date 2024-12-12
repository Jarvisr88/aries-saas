namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class FormatEditorFontViewModel : FormatEditorItemViewModel
    {
        protected FormatEditorFontViewModel(IDialogContext column) : base(column)
        {
            this.AllowTextDecorations = true;
            this.FontSizes = this.CreateFontSizes();
            this.FontStyles = this.CreateFontStyles();
            this.FontWeights = this.CreateFontWeights();
        }

        public override void Clear()
        {
            this.FontName = string.Empty;
            this.FontStyle = System.Windows.FontStyles.Normal;
            this.FontWeight = System.Windows.FontWeights.Normal;
            this.FontSize = null;
            this.FontColor = null;
            this.Strikethrough = false;
            this.Underline = false;
            base.HasChanged = false;
        }

        private FontFamily CreateFontFamily(string fontName) => 
            string.IsNullOrEmpty(fontName) ? null : new FontFamily(fontName);

        private double CreateFontSize(double? size) => 
            (size != null) ? size.Value : 0.0;

        private IEnumerable<double> CreateFontSizes() => 
            new double[] { 8.0, 9.0, 10.0, 11.0, 12.0, 14.0, 16.0, 18.0, 20.0, 22.0, 24.0, 26.0, 28.0, 36.0, 48.0, 72.0 };

        [IteratorStateMachine(typeof(<CreateFontStyles>d__54))]
        private IEnumerable<System.Windows.FontStyle> CreateFontStyles()
        {
            yield return System.Windows.FontStyles.Normal;
            yield return System.Windows.FontStyles.Italic;
            yield return System.Windows.FontStyles.Oblique;
        }

        [IteratorStateMachine(typeof(<CreateFontWeights>d__53))]
        private IEnumerable<System.Windows.FontWeight> CreateFontWeights()
        {
            yield return System.Windows.FontWeights.Normal;
            yield return System.Windows.FontWeights.Bold;
            yield return System.Windows.FontWeights.Thin;
        }

        private TextDecorationCollection CreateTextDecorations()
        {
            if (!this.Strikethrough && !this.Underline)
            {
                return null;
            }
            TextDecorationCollection decorations = new TextDecorationCollection();
            if (this.Strikethrough)
            {
                decorations.Add(TextDecorations.Strikethrough[0]);
            }
            if (this.Underline)
            {
                decorations.Add(TextDecorations.Underline[0]);
            }
            return decorations;
        }

        public override void InitFromFormat(Format format)
        {
            FontFamily fontFamily = format.FontFamily;
            if ((fontFamily != null) && (Fonts.SystemFontFamilies.Contains(fontFamily) && !string.IsNullOrEmpty(fontFamily.ToString())))
            {
                this.FontName = format.FontFamily.ToString();
            }
            System.Windows.FontStyle fontStyle = format.FontStyle;
            if (this.FontStyles.Contains<System.Windows.FontStyle>(fontStyle))
            {
                this.FontStyle = fontStyle;
            }
            System.Windows.FontWeight fontWeight = format.FontWeight;
            if (this.FontWeights.Contains<System.Windows.FontWeight>(fontWeight))
            {
                this.FontWeight = fontWeight;
            }
            double fontSize = format.FontSize;
            if (this.FontSizes.Contains<double>(fontSize))
            {
                this.FontSize = new double?(fontSize);
            }
            Color? brushColor = base.GetBrushColor(format.Foreground);
            if (brushColor != null)
            {
                this.FontColor = new Color?(brushColor.Value);
            }
            TextDecorationCollection textDecorations = format.TextDecorations;
            if (textDecorations != null)
            {
                this.Strikethrough = textDecorations.Contains(TextDecorations.Strikethrough[0]);
                this.Underline = textDecorations.Contains(TextDecorations.Underline[0]);
            }
        }

        public override void SetFormatProperties(Format format)
        {
            ManagerHelperBase.SetProperty(format, Format.TextDecorationsProperty, this.CreateTextDecorations());
            ManagerHelperBase.SetProperty(format, Format.FontFamilyProperty, this.CreateFontFamily(this.FontName));
            ManagerHelperBase.SetProperty(format, Format.ForegroundProperty, base.CreateBrush(this.FontColor));
            ManagerHelperBase.SetProperty(format, Format.FontStyleProperty, this.FontStyle);
            ManagerHelperBase.SetProperty(format, Format.FontWeightProperty, this.FontWeight);
            ManagerHelperBase.SetProperty(format, Format.FontSizeProperty, this.CreateFontSize(this.FontSize));
        }

        public static Func<IDialogContext, FormatEditorFontViewModel> Factory
        {
            get
            {
                ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(IDialogContext), "x");
                System.Linq.Expressions.Expression[] expressionArray1 = new System.Linq.Expressions.Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return ViewModelSource.Factory<IDialogContext, FormatEditorFontViewModel>(System.Linq.Expressions.Expression.Lambda<Func<IDialogContext, FormatEditorFontViewModel>>(System.Linq.Expressions.Expression.New((ConstructorInfo) methodof(FormatEditorFontViewModel..ctor), (IEnumerable<System.Linq.Expressions.Expression>) expressionArray1), parameters));
            }
        }

        [BindableProperty(OnPropertyChangedMethodName="OnChanged")]
        public virtual string FontName { get; set; }

        public IEnumerable<System.Windows.FontStyle> FontStyles { get; private set; }

        [BindableProperty(OnPropertyChangedMethodName="OnChanged")]
        public virtual System.Windows.FontStyle FontStyle { get; set; }

        public IEnumerable<System.Windows.FontWeight> FontWeights { get; private set; }

        [BindableProperty(OnPropertyChangedMethodName="OnChanged")]
        public virtual System.Windows.FontWeight FontWeight { get; set; }

        public IEnumerable<double> FontSizes { get; private set; }

        [BindableProperty(OnPropertyChangedMethodName="OnChanged")]
        public virtual double? FontSize { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="OnChanged")]
        public virtual Color? FontColor { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="OnChanged")]
        public virtual bool Strikethrough { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="OnChanged")]
        public virtual bool Underline { get; set; }

        public virtual bool AllowTextDecorations { get; set; }

        public override string Description =>
            base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_Font);


    }
}

